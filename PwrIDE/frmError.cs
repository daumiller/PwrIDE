using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace PwrIDE
{
  public partial class frmError : DockContent
  {
    //========================================================================
    //  constructor/load/close
    //========================================================================
    public frmError()
    {
      InitializeComponent();
    }
    //------------------------------------------------------------------------
    private void frmError_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }
    //========================================================================
    //  publics
    //========================================================================
    public void SetError(string file, int line, string description)
    {
      ClearErrors();
      AddError(file, line, description);
    }
    //------------------------------------------------------------------------
    public void ClearErrors()
    {
      lstErrors.Items.Clear();
    }
    //------------------------------------------------------------------------
    public void AddError(string file, int line, string description)
    {
      lstErrors.Items.Add(new ListViewItem(new string[]{ file, line.ToString(), description }));
    }
    //========================================================================
    //  events
    //========================================================================
    private void frmError_Resize(object sender, EventArgs e)
    {
      columnHeader1.Width = (int)((lstErrors.Width - 4) * 0.27f);
      columnHeader2.Width = (int)((lstErrors.Width - 4) * 0.07f);
      columnHeader3.Width = (int)((lstErrors.Width - 4) * 0.66f);
    }
    //------------------------------------------------------------------------
    private void lstErrors_DoubleClick(object sender, EventArgs e)
    {
    	if(Util.MainForm.ActiveSource != null)
    	{
    		((frmSource)Util.MainForm.ActiveSource).MoveToError();
    		((frmSource)Util.MainForm.ActiveSource).Focus();
    	}
    }
    //========================================================================
  }
}
