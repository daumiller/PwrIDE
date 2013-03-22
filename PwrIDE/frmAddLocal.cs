using System;
using System.Text;
using System.Windows.Forms;

namespace PwrIDE
{
  public partial class frmAddLocal : Form
  {
    //========================================================================
    private Local Local;
    //========================================================================
    public frmAddLocal()
    {
      InitializeComponent();
    }
    //------------------------------------------------------------------------
    public frmAddLocal(Local Local)
    {
      InitializeComponent();
      this.Local = Local;
      txtName.Text = Local.Name;
      txtPath.Text = Local.Path;
    }
    //------------------------------------------------------------------------
    private void frmAddLocal_Load(object sender, EventArgs e)
    {
      txtName.Focus();
    }
    //========================================================================
    public string Alias { get { return txtName.Text; } }
    public string Path  { get { return txtPath.Text; } }
    //========================================================================
    private void frmAddLocal_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void txtName_Enter(object sender, EventArgs e)
    {
      txtName.SelectAll();
    }
    //------------------------------------------------------------------------
    private void txtPath_Enter(object sender, EventArgs e)
    {
      txtPath.SelectAll();
    }
    //========================================================================
    private void txtName_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        txtPath.Focus();
    }
    //------------------------------------------------------------------------
    private void txtPath_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        btnOkay_Click(sender, new EventArgs());
    }
    //========================================================================
    private void btnBrowse_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      fbd.ShowNewFolderButton = true;
      if(fbd.ShowDialog(Util.MainForm) == DialogResult.OK)
        txtPath.Text = fbd.SelectedPath;
    }
    //------------------------------------------------------------------------
    private void btnOkay_Click(object sender, EventArgs e)
    {
      if(Local == null)
      {
        for(int i=0; i<Config.Locals.Count; i++)
        {
          if(Config.Locals[i].Name == txtName.Text)
          {
            MessageBox.Show("A Local Directory Named \"" + txtName.Text + "\" Already Exists");
            return;
          }
        }
      }
      DialogResult = DialogResult.OK;
    }
    //------------------------------------------------------------------------
    private void btnCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
    //========================================================================
  }
}
