using System;
using System.Text;
using System.Windows.Forms;
using Symitar;

namespace PwrIDE
{
  public partial class frmAddSym : Form
  {
    //========================================================================
    private SymServer Server;
    private SymInst   SymInst;
    //========================================================================
    public frmAddSym(SymServer Server)
    {
      InitializeComponent();
      this.Server  = Server;
      this.SymInst = null;
    }
    //------------------------------------------------------------------------
    public frmAddSym(SymInst Sym)
    {
      InitializeComponent();
      this.Server  = Sym.Parent;
      this.SymInst = Sym;
      txtSym.Text         = SymInst.SymDir;
      txtID.Text          = SymInst.SymId;
      chkRemember.Checked = SymInst.Remember;
    }
    //------------------------------------------------------------------------
    private void frmAddSym_Load(object sender, EventArgs e)
    {
      txtSym.Focus();
    }
    //========================================================================
    public string Sym    { get { return txtSym.Text;         } }
    public string ID     { get { return txtID.Text;          } }
    public bool Remember { get { return chkRemember.Checked; } }
    //========================================================================
    private void frmAddSym_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void btnOkay_Click(object sender, EventArgs e)
    {
      if(SymInst == null)
      {
        for(int i=0; i<Server.Syms.Count; i++)
        {
          if(Server.Syms[i].SymDir == txtSym.Text)
          {
            MessageBox.Show("Sym "+txtSym.Text+" Already Exists on Server \""+Server.Alias+'\"');
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
    //------------------------------------------------------------------------
    private void btnTest_Click(object sender, EventArgs e)
    {
      SymSession test = new SymSession();
      try
      {
        if(!test.Connect(Server.IP, Server.Port))
        {
          MessageBox.Show(test.error, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        string aixPwd = Server.AixPwd;
        if(aixPwd.Trim().Length == 0)
        {
          InputBox inp = new InputBox("Test Sym Connection", "Please Enter AIX Password for Server \"" + Server.Alias + "\".", "", true);
          if (inp.ShowDialog(this) == DialogResult.Cancel) return;
          aixPwd = inp.Input;
          inp.Dispose();
        }
        
       	bool retry = true;
       	bool result = test.Login(Server.AixUsr, aixPwd, txtSym.Text, txtID.Text);
       	while(retry && (!result))
       	{
       		retry = false;
        	if(result == false)
        	{
        		if(test.error == "Invalid AIX Login")
        		{
        			InputBox inp = new InputBox("Test Sym Connection", "Invalid AIX Login.\nPlease Re-Enter or Cancel (ESC).", "", true);
        			if(inp.ShowDialog(this) == DialogResult.Cancel) return;
        			aixPwd = inp.Input;
        			inp.Dispose();
        			result = test.Login(Server.AixUsr, aixPwd, txtSym.Text, txtID.Text, 1);
        			retry = true;
        		}
        		else if(test.error == "Invalid Sym User")
        		{
        			InputBox inp = new InputBox("Test Sym Connection", "Invalid Sym User ID.\nPlease Re-Enter or Cancel (ESC).", "", true);
        			if(inp.ShowDialog(this) == DialogResult.Cancel) return;
        			txtID.Text = inp.Input;
        			inp.Dispose();
        			result = test.Login(Server.AixUsr, aixPwd, txtSym.Text, txtID.Text, 2);
        			retry = true;
        		}
        		else
        			retry = (MessageBox.Show("Error Connecting to Sym\nError: "+test.error+"\n\nRetry?", "Sym Connection Test", MessageBoxButtons.YesNo, MessageBoxIcon.Error)==DialogResult.Yes);
        		if((!retry) && (!result))
        		{
        			test.Disconnect();
        			return;
        		}
        	}
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error During Login Test\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      test.Disconnect();
      MessageBox.Show("Sym Connected Okay!", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.None);
    }
    //========================================================================
    private void txtSym_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        txtID.Focus();
    }
    //------------------------------------------------------------------------
    private void txtID_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
        btnOkay_Click(sender, new EventArgs());
    }
    //========================================================================
    private void txtSym_Enter(object sender, EventArgs e)
    {
      txtSym.SelectAll();
    }
    //------------------------------------------------------------------------
    private void txtID_Enter(object sender, EventArgs e)
    {
      txtID.SelectAll();
    }
    //========================================================================
  }
}
