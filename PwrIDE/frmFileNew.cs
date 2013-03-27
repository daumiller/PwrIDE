using System;
using System.Text;
using System.Windows.Forms;

namespace PwrIDE
{
  public partial class frmFileNew : Form
  {
    //========================================================================
    public frmFileNew()
    {
      InitializeComponent();
    }
    //========================================================================
    private void frmFileNew_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void lblRep_Click(object sender, EventArgs e)
    {
      Util.MainForm.mnuFileNewRep_Click(sender, e);
      DialogResult = DialogResult.OK;
    }
    private void lblLtr_Click(object sender, EventArgs e)
    {
      Util.MainForm.mnuFileNewLtr_Click(sender, e);
      DialogResult = DialogResult.OK;
    }
    //========================================================================
  }
}
