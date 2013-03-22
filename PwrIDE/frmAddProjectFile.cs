using System;
using System.Text;
using System.Windows.Forms;

namespace PwrIDE
{
  public partial class frmAddProjectFile : Form
  {
    //========================================================================
    public string               FileName;
    public ProjectFile.FileType FileType;
    public Symitar.SymFile.Type SymFileType;
    //========================================================================
    public frmAddProjectFile()
    {
      InitializeComponent();
      comboType.SelectedIndex = 0;
      txtFilename.Focus();
      txtFilename.SelectAll();
    }
    //========================================================================
    private void frmAddProjectFile_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void txtFilename_KeyPress(object sender, KeyPressEventArgs e)
    {
      if(e.KeyChar == '\r')
      {
        e.Handled = true;
        btnOkay_Click(sender, new EventArgs());
      }
    }
    //========================================================================
    private void btnCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void btnOkay_Click(object sender, EventArgs e)
    {
      FileName = txtFilename.Text;
      if ((string)comboType.SelectedItem == "PowerPlus")
      {
        FileType = ProjectFile.FileType.PWRPLS;
        SymFileType = Symitar.SymFile.Type.PWRPLS;
      }
      else if ((string)comboType.SelectedItem == "RepGen")
      {
        FileType = ProjectFile.FileType.REPGEN;
        SymFileType = Symitar.SymFile.Type.REPGEN;
      }
      else if ((string)comboType.SelectedItem == "Letterfile")
      {
        FileType = ProjectFile.FileType.LETTER;
        SymFileType = Symitar.SymFile.Type.LETTER;
      }
      DialogResult = DialogResult.OK;
    }
    //========================================================================
  }
}
