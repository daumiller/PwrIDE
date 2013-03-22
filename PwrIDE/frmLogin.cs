using System;
using System.Text;
using System.Windows.Forms;

namespace PwrIDE
{
  public partial class frmLogin : Form
  {
    //========================================================================
    public frmLogin(string Server, string Sym, string User, string Pass, string ID)
    {
      InitializeComponent();
      lblWhere.Text = "Server \""+Server+"\", Sym "+Sym;
      txtUser.Text = User;
      txtPass.Text = Pass;
      txtID.Text   = ID;
    }
    //========================================================================
    public string User { get { return txtUser.Text; } }
    public string Pass { get { return txtPass.Text; } }
    public string ID   { get { return txtID.Text;   } }
    //========================================================================
    private void frmLogin_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
    {
      if(e.KeyChar == '\r')
        txtPass.Focus();
    }
    //------------------------------------------------------------------------
    private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
    {
      if(e.KeyChar == '\r')
        txtID.Focus();
    }
    //------------------------------------------------------------------------
    private void txtID_KeyPress(object sender, KeyPressEventArgs e)
    {
      if(e.KeyChar == '\r')
        btnOkay_Click(sender, new EventArgs());
    }
    //========================================================================
    private void txtUser_Enter(object sender, EventArgs e)
    {
      txtUser.SelectAll();
    }
    //------------------------------------------------------------------------
    private void txtPass_Enter(object sender, EventArgs e)
    {
      txtPass.SelectAll();
    }
    //------------------------------------------------------------------------
    private void txtID_Enter(object sender, EventArgs e)
    {
      txtID.SelectAll();
    }
    //========================================================================
    private void btnCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
    //------------------------------------------------------------------------
    private void btnOkay_Click(object sender, EventArgs e)
    {
    	DialogResult = DialogResult.OK;
    }
    //========================================================================
  }
}
