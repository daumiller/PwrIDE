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
  public partial class frmRunFM : Form
  {
    //========================================================================
    private string           repTitle;
    private string           fmTitle;
    private SymServer        server;
    private SymInst          inst;
    private SymSession       session;
    private bool             running;
    private BackgroundWorker checker;
    private int              sequence;
    private int              mod;
    //========================================================================
    public frmRunFM(string repTitle, SymServer server, SymInst inst)
    {
      InitializeComponent();

      cmbFmFile.SelectedIndex = 0;
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

      this.repTitle = repTitle;
      this.server   = server;
      this.inst     = inst;
      session       = Util.TrySymNewConnect(inst);
      if(session == null) Close(); //user gets Retry prompt if this fails
      running = false;
      
      txtFile.Text   = repTitle;
      txtServer.Text = server.Alias;
      txtSym.Text    = inst.SymDir;

      checker = new BackgroundWorker();
      checker.DoWork             += new DoWorkEventHandler(CheckerWork);
      checker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CheckerDone);
      checker.ProgressChanged    += new ProgressChangedEventHandler(CheckerProgress);
      checker.WorkerReportsProgress      = true;
      checker.WorkerSupportsCancellation = true;
    }
    //========================================================================
    private void frmRunFM_FormClosing(object sender, FormClosingEventArgs e)
    {
      if(running)
        checker.CancelAsync();
      if(session != null)
        session.Disconnect();
    }
    //------------------------------------------------------------------------
    private void frmRunFM_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        if(btnCancel.Enabled)
          btnCancel_Click(sender, new EventArgs());
    }
    //------------------------------------------------------------------------
    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }
    //========================================================================
    private void btnRun_Click(object sender, EventArgs e)
    {
      int queue = -1;
      if(optUserSelect.Checked)
        queue = int.Parse(txtQueue.Text);
      if(chkQueueAlways.Checked == true)
        Config.SetValue("Default_Queue", txtQueue.Text, true);

      SymSession.FMRunNfo fmrn;
			btnRun.Enabled = false;
      try
      {
        fmrn = session.FMRun(repTitle, (SymSession.FMType)cmbFmFile.SelectedIndex, StatusUpdate, queue);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Running FM\nError: \""+ex.Message+'"', "PwrIDE - Run FM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Close();
        return;
      }

      sequence = fmrn.sequence;
      fmTitle  = fmrn.title;
      running  = true;
      mod      = 0;
      checker.RunWorkerAsync();
    }
    //========================================================================
    private void cmbFmFile_SelectedIndexChanged(object sender, EventArgs e) { /*why did i add this event?*/ }
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
    private void CheckerDone(object sender, RunWorkerCompletedEventArgs e)
    {
      if(e.Result != null)
      {
        session.Disconnect();
        MessageBox.Show("Error Running FM\n" + (string)e.Result, "PwrIDE - Run FM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Close();
        return;
      }

      StatusUpdate(10, "Report Finished\nReading Results");
      int repSeq = session.GetFMSequence(fmTitle);
      if(repSeq == -1)
      {
        MessageBox.Show("Couldn't Find Finished FM Sequence!");
        Close();
        return;
      }
      
      Regex CrLf = new Regex(@"(^\n|[^\r]\n)");
      List<SymFile> reports = new List<SymFile>();
      List<string> contents = new List<string>();
      reports.Add(new SymFile(session.server, session.sym, repSeq.ToString(), DateTime.Now, 0,  SymFile.Type.REPORT));
      string listing = session.FileRead(reports[0]);
      reports[0].sequence = repSeq;
      reports[0].pages = 1;
      reports[0].title = "Batch Output for MISCFMPOST";
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
        match = repReg.Match(listing);
      }

      session.Disconnect();
      frmReport viewer = new frmReport(session.sym, "FM: "+repTitle, reports, contents, server, inst, true);
      viewer.Show();
      Close();
    }
    //------------------------------------------------------------------------
    private void CheckerProgress(object sender, ProgressChangedEventArgs e)
    {
      StatusUpdate(9, "Still Running");
    }
    //------------------------------------------------------------------------
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
