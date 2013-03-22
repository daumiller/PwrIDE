using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Symitar;

namespace PwrIDE
{
  public partial class frmRunRep : Form
  {
    //========================================================================
    private SymFile    specfile;
    private SymSession session;
    private SymServer  server;
    private SymInst    originalInst;
    private bool       running;
    private int        sequence;
    private int        time;
    private int        mod;
    private BackgroundWorker checker;
    //========================================================================
    public frmRunRep(SymFile specfile, SymServer server, SymInst clone)
    {
      InitializeComponent();
      this.server    = server;
      this.specfile  = specfile;
      originalInst   = clone;
      txtFile.Text   = specfile.name;
      txtServer.Text = server.Alias;
      txtSym.Text    = specfile.sym;
      session        = Util.TrySymNewConnect(clone);
      if(session == null) Close(); //user gets Retry prompt if this fails
      running = false;
      
      checker = new BackgroundWorker();
      checker.DoWork             += new DoWorkEventHandler(CheckerWork);
      checker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CheckerDone);
      checker.ProgressChanged    += new ProgressChangedEventHandler(CheckerProgress);
      checker.WorkerReportsProgress      = true;
      checker.WorkerSupportsCancellation = true;

      int defQueue = Config.GetInt("Default_Queue");
      if(defQueue > -1)
      {
        optFirstEmpty.Checked  = false;
        optUserSelect.Checked  = true;
        chkQueueAlways.Checked = true;
        txtQueue.Text          = defQueue.ToString();
      }
      else
      {
        optFirstEmpty.Checked  = true;
        optUserSelect.Checked  = false;
        chkQueueAlways.Checked = false;
        txtQueue.Text          = "";
      }
      btnRun.Focus();
    }
    //========================================================================
    private void frmRunRep_FormClosing(object sender, FormClosingEventArgs e)
    {
      if(running)
        checker.CancelAsync();
      if(session != null)
        session.Disconnect();
    }
    //========================================================================
    private void frmRunRep_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        if(btnCancel.Enabled)
          btnCancel_Click(sender, new EventArgs());
    }
    //========================================================================
    private void btnRun_Click(object sender, EventArgs e)
    {
      int queue = -1;
      if(optUserSelect.Checked)
        queue = int.Parse(txtQueue.Text);
      if(chkQueueAlways.Checked == true)
        Config.SetValue("Default_Queue", txtQueue.Text, true);

			btnRun.Enabled = false;
      RepRunErr rre = session.FileRun(specfile, StatusUpdate, PromptInput, queue);
      switch(rre.code)
      {
        case RepRunErr.Status.CANCELLED:
          Close();
          return;
        case RepRunErr.Status.NOTFOUND:
          MessageBox.Show("Error Running File \""+specfile.name+"\"\nFile Not Found", "Run Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
          Close();
          return;
        case RepRunErr.Status.ERRORED:
          MessageBox.Show("Error Running File \""+specfile.name+"\"\nError: \""+rre.err+'\"', "Run Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
          Close();
          return;
      }

      //RepRunErr.Status.OKAY:
      sequence = rre.sequence;
      time = rre.time;
      running = true;
      mod = 0;
      checker.RunWorkerAsync();
    }
    //========================================================================
    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }
    //========================================================================
    public void StatusUpdate(int code, string status)
    {
      if(code == 9)
      {
        mod++;
        if(mod == 4) mod=0;
        for(int i=0; i<mod; i++)
          status += '.';
      }
      progStatus.Value = code * 10;
      lblStatus.Text   = status;
      Refresh();
    }
    //========================================================================
    public string PromptInput(string prompt)
    {
      InputBox inp = new InputBox("Run Report", prompt, "", false);
      if(inp.ShowDialog(this) == DialogResult.Cancel)
      {
        inp.Dispose();
        return null;
      }

      string result = inp.Input;
      inp.Dispose();
      return result;
    }
    //========================================================================
    private void CheckerDone(object sender, RunWorkerCompletedEventArgs e)
    {
      if(e.Result != null)
      {
        session.Disconnect();
        MessageBox.Show("Error Running File\n" + (string)e.Result, "Run Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Close();
        return;
      }

      StatusUpdate(10, "Report Finished\nReading Results");
      int repSeq = session.GetReportSequence(specfile.name, time);
      if(repSeq == -1)
      {
        MessageBox.Show("Couldn't Find Finished Report Sequence!");
        Close();
        return;
      }

      List<SymFile> reports = new List<SymFile>();
      List<string> contents = new List<string>();
      reports.Add(new SymFile(session.server, session.sym, repSeq.ToString(), DateTime.Now, 0,  SymFile.Type.REPORT));
      string listing = session.FileRead(reports[0]);
      reports[0].sequence = repSeq;
      reports[0].pages = 1;
      reports[0].title = "Batch Output for REPWRITER";
      contents.Add(listing.Replace("\n", "\r\n"));

      Regex repReg = new Regex(@"Seq:\s+([0-9]{6})\s+Pages:\s+([0-9]+)\s+Title:\s+([^\n]+)\n");
      Match match = repReg.Match(listing);
      while(match.Success)
      {
        listing = listing.Replace(match.Value, "");
        SymFile currRep = new SymFile(session.server, session.sym, (int.Parse(match.Groups[1].Value)).ToString(), DateTime.Now, 0, SymFile.Type.REPORT);
        currRep.sequence = int.Parse(match.Groups[1].Value);
        currRep.pages    = int.Parse(match.Groups[2].Value);
        currRep.title    = match.Groups[3].Value;
        reports.Add(currRep);
        contents.Add(session.FileRead(currRep).Replace("\n","\r\n"));
        //MessageBox.Show('\"' + match.Groups[1].Value + "\"\n\"" + match.Groups[2].Value + "\"\n\"" + match.Groups[3].Value + '\"');
        match = repReg.Match(listing);
      }
      session.Disconnect();
      
      frmReport viewer = new frmReport(specfile.sym, specfile.name, reports, contents, server, originalInst, false);
      viewer.Show();
      Close();
    }
    //========================================================================
    private void CheckerProgress(object sender, ProgressChangedEventArgs e)
    {
      StatusUpdate(9, "Still Running");
    }
    //========================================================================
    private void CheckerWork(object sender, DoWorkEventArgs e)
    {
      while(running)
      {
        try
        {
          running = session.IsFileRunning(sequence);
          if(running)
          {
            checker.ReportProgress(9);
            Thread.Sleep(1000);
          }
        }
        catch(Exception ex)
        {
          running = false;
          e.Result = ex.Message;
          return;
        }
      }
      e.Result = null;
    }
    //========================================================================
  }
}
