using System;
using System.Text;
using System.Windows.Forms;
using Symitar;

namespace PwrIDE
{
  public partial class frmAddServer : Form
  {
    //========================================================================
    private SymServer Server;
    //========================================================================
    public frmAddServer()
    {
      InitializeComponent();
    }
    //------------------------------------------------------------------------
    public frmAddServer(SymServer Server)
    {
      InitializeComponent();
      this.Server = Server;
      txtName.Text = Server.Alias;
      txtIP.Text   = Server.IP;
      txtPort.Text = Server.Port.ToString();
      txtUser.Text = Server.AixUsr;
      txtPass.Text = Server.AixPwd;
    }
    //========================================================================
    public string Alias    { get { return txtName.Text;        } }
    public string IP       { get { return txtIP.Text;          } }
    public string Port     { get { return txtPort.Text;        } }
    public string User     { get { return txtUser.Text;        } }
    public string Pass     { get { return txtPass.Text;        } }
    public bool   Remember { get { return chkRemember.Checked; } }
    //========================================================================
    private void frmAddServer_Load(object sender, EventArgs e)
    {
      txtName.Focus();
    }
    //========================================================================
    private void frmAddServer_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void btnCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
    //------------------------------------------------------------------------
    private void btnOkay_Click(object sender, EventArgs e)
    {
      if(Server == null)
      {
        for(int i=0; i<Config.Servers.Count; i++)
        {
          if(Config.Servers[i].Alias == txtName.Text)
          {
            MessageBox.Show("A Server Named \""+txtName.Text+"\" Already Exists");
            return;
          }
        }
      }
      DialogResult = DialogResult.OK;
    }
    //------------------------------------------------------------------------
    private void btnTest_Click(object sender, EventArgs e)
    {
      SymSession test = new SymSession();
      try
      {
        if (!test.Connect(txtIP.Text, int.Parse(txtPort.Text)))
        {
          MessageBox.Show(test.error, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        if (!test.AIXTest(txtUser.Text, txtPass.Text))
        {
          MessageBox.Show(test.error, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error During Login\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      test.Disconnect();
      MessageBox.Show("Server Connected Okay!", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.None);
    }
    //========================================================================
    private void txtName_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        txtIP.Focus();
    }
    //------------------------------------------------------------------------
    private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        txtPort.Focus();
    }
    //------------------------------------------------------------------------
    private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        txtUser.Focus();
    }
    //------------------------------------------------------------------------
    private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        txtPass.Focus();
    }
    //------------------------------------------------------------------------
    private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        btnOkay_Click(sender, new EventArgs());
    }
    //========================================================================
    private void txtName_Enter(object sender, EventArgs e)
    {
      txtName.SelectAll();
    }
    //------------------------------------------------------------------------
    private void txtIP_Enter(object sender, EventArgs e)
    {
      txtIP.SelectAll();
    }
    //------------------------------------------------------------------------
    private void txtPort_Enter(object sender, EventArgs e)
    {
      txtPort.SelectAll();
    }
    //------------------------------------------------------------------------
    private void txtUser_Enter(object sender, EventArgs e)
    {
      txtUser.SelectAll();
    }
    //------------------------------------------------------------------------
    private void txtPass_Enter(object sender, EventArgs e)
    {
      txtPass.SelectAll();
    }
    //========================================================================
  }
}
