using System;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using Symitar;

namespace PwrIDE
{
  public partial class frmReport : Form
  {
    //========================================================================
    private List<string>  reports;
    private SymServer     server;
    private SymInst       inst;
    private bool          isFM;
    //========================================================================
    public frmReport(string sym, string source, List<SymFile> files, List<string> contents, SymServer server, SymInst inst, bool isFM)
    {
      InitializeComponent();
      this.server = server;
      this.inst   = inst;
      this.isFM   = isFM;
      Text = '['+sym+"] Output of " + source;
      this.reports = contents;
      lstFiles.Items.Clear();
      for(int i=0; i<files.Count; i++)
      {
        string doFM = (isFM || (i==0)) ? "" : "Run FM";
        lstFiles.Items.Add(new ListViewItem(new string[]{ files[i].sequence.ToString(), files[i].title, files[i].pages.ToString(), doFM }));
        if((i!=0) && (isFM==false))
        {
          lstFiles.Items[i].UseItemStyleForSubItems = false;
          lstFiles.Items[i].SubItems[3].Font = new System.Drawing.Font(lstFiles.Items[i].SubItems[3].Font, System.Drawing.FontStyle.Underline);
          lstFiles.Items[i].SubItems[3].ForeColor = System.Drawing.Color.Blue;
        }
      }
      if(lstFiles.Items.Count > 0)
        lstFiles.Items[0].Selected = true;
    }
    //========================================================================
    private void frmReport_Load(object sender, EventArgs e)
    {
      Width  = Config.GetInt("Report_Width");
      Height = Config.GetInt("Report_Height");
      splitContainer1.SplitterDistance = Config.GetInt("Report_Split");
    }
    //========================================================================
    private void frmReport_Resize(object sender, EventArgs e)
    {
      columnHeader2.Width = lstFiles.Width - columnHeader1.Width - columnHeader3.Width - columnHeader4.Width - 8;
    }
    //========================================================================
    private void lstFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      txtDisplay.Text = reports[e.ItemIndex];
    }
    //========================================================================
    private void lstFiles_MouseUp(object sender, MouseEventArgs e)
    {
      ListViewHitTestInfo hit = lstFiles.HitTest(e.Location);
      if(hit.Item != null)
        if(hit.SubItem != null)
          if(hit.SubItem.ForeColor == System.Drawing.Color.Blue)
          {
          	frmRunFM fm = new frmRunFM(hit.Item.SubItems[1].Text, server, inst);
          	fm.Show();
          	fm.Focus();
          }
    }
    //========================================================================
    private void frmReport_FormClosing(object sender, FormClosingEventArgs e)
    {
      Config.SetValue("Report_Width" , Width.ToString() , true);
      Config.SetValue("Report_Height", Height.ToString(), true);
      Config.SetValue("Report_Split" , splitContainer1.SplitterDistance.ToString());
    }
    //========================================================================
  }
}
