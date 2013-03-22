using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Symitar;

namespace PwrIDE
{
  public partial class frmFileOpen : Form
  {
    //========================================================================
    public bool          saveAsIsLocal { get { return isLocal;                } }
    public SymInst       saveAsInst    { get { return currInst;               } }
    public Local         saveAsLocal   { get { return currLocal;              } }
    public string        saveAsName    { get { return txtFilename.Text;       } }
    public SymFile.Type  saveAsType    { get { return SymFileTypeFromCombo(); } }
    //------------------------------------------------------------------------
    public List<SymFile>   retrieveSym   { get { return symFiles;   } }
    public List<LocalFile> retrieveLocal { get { return localFiles; } }
    //------------------------------------------------------------------------
    private bool            isSaveType = false;
    private bool            isLocal    = false;
    private bool            retrieve   = false;
    private SymInst         currInst   = null;
    private Local           currLocal  = null;
    private List<SymFile>   symFiles   = null;
    private List<LocalFile> localFiles = null;
    //========================================================================
    //Open File Constructor
		public frmFileOpen()
		{
      InitializeComponent();
      comboType.SelectedItem = comboType.Items[0];
      
      treeSym.Nodes.Clear();
      for(int i=0; i<Config.Servers.Count; i++)
      {
      	TreeNode serv = new TreeNode(Config.Servers[i].Alias);
      	serv.ImageIndex = serv.SelectedImageIndex = 0;
      	for(int c=0; c<Config.Servers[i].Syms.Count; c++)
      	{
      		TreeNode sym = new TreeNode("Sym "+Config.Servers[i].Syms[c].SymDir);
      		sym.ImageIndex = sym.SelectedImageIndex = 1;
      		serv.Nodes.Add(sym);
      	}
      	serv.Expand();
      	treeSym.Nodes.Add(serv);
      }
      for(int i=0; i<Config.Locals.Count; i++)
      {
        TreeNode local   = new TreeNode(Config.Locals[i].Name);
        local.ImageIndex = local.SelectedImageIndex = 5;
        treeSym.Nodes.Add(local);
      }
		}
    //------------------------------------------------------------------------
    //Open from Specified Sym Constructor
    public frmFileOpen(SymInst sym)
    {
      InitializeComponent();
      comboType.SelectedItem = comboType.Items[0];
      txtFilename.Enabled = true;

      int newLeft = treeSym.Left;
      int difLeft = txtFilename.Left - newLeft;
      treeSym.Visible  = false;
      btnLocal.Visible = false;
      txtFilename.Left = newLeft;
      lstResults.Left  = newLeft;
      comboType.Left  -= difLeft;
      btnOpen.Left    -= difLeft;
      Width           -= difLeft;
      currInst         = sym;
    }
    //------------------------------------------------------------------------
    //Open From Specified Local Constructor
    public frmFileOpen(Local local)
    {
      InitializeComponent();
      comboType.SelectedItem = comboType.Items[0];
      txtFilename.Enabled = true;

      int newLeft = treeSym.Left;
      int difLeft = txtFilename.Left - newLeft;
      treeSym.Visible  = false;
      btnLocal.Visible = false;
      txtFilename.Left = newLeft;
      lstResults.Left  = newLeft;
      comboType.Left  -= difLeft;
      btnOpen.Left    -= difLeft;
      Width           -= difLeft;
      isLocal          = true;
      currLocal        = local;
    }
    //------------------------------------------------------------------------
    //Retrive File Objects from Sym Constructor
    public frmFileOpen(SymInst sym, bool GetFileObj)
    {
      InitializeComponent();
      comboType.SelectedItem = comboType.Items[0];
      txtFilename.Enabled = true;

      retrieve    = true;
      int newLeft = treeSym.Left;
      int difLeft = txtFilename.Left - newLeft;
      treeSym.Visible  = false;
      btnLocal.Visible = false;
      txtFilename.Left = newLeft;
      lstResults.Left  = newLeft;
      comboType.Left  -= difLeft;
      btnOpen.Left    -= difLeft;
      Width           -= difLeft;
      currInst         = sym;
    }
    //------------------------------------------------------------------------
    //Retrive File Objects from Local Constructor
    public frmFileOpen(Local local, bool GetFileObj)
    {
      InitializeComponent();
      comboType.SelectedItem = comboType.Items[0];
      txtFilename.Enabled = true;

      retrieve    = true;
      int newLeft = treeSym.Left;
      int difLeft = txtFilename.Left - newLeft;
      treeSym.Visible  = false;
      btnLocal.Visible = false;
      txtFilename.Left = newLeft;
      lstResults.Left  = newLeft;
      comboType.Left  -= difLeft;
      btnOpen.Left    -= difLeft;
      Width           -= difLeft;
      isLocal          = true;
      currLocal        = local;
    }
    //------------------------------------------------------------------------
		//Save As Constructor
    public frmFileOpen(string saveName)
    {
      InitializeComponent();
      comboType.SelectedItem = comboType.Items[0];

    	isSaveType = true;
      Text = "Save File As";
      btnOpen.Text = "&Save";
      btnLocal.Text = "Save File &Locally";
      txtFilename.Text = saveName;
      
      treeSym.Nodes.Clear();
      for(int i=0; i<Config.Servers.Count; i++)
      {
      	TreeNode serv = new TreeNode(Config.Servers[i].Alias);
      	serv.ImageIndex = serv.SelectedImageIndex = 0;
      	for(int c=0; c<Config.Servers[i].Syms.Count; c++)
      	{
      		TreeNode sym = new TreeNode("Sym "+Config.Servers[i].Syms[c].SymDir);
      		sym.ImageIndex = sym.SelectedImageIndex = 1;
      		serv.Nodes.Add(sym);
      	}
      	serv.Expand();
      	treeSym.Nodes.Add(serv);
      }
      for(int i=0; i<Config.Locals.Count; i++)
      {
        TreeNode local   = new TreeNode(Config.Locals[i].Name);
        local.ImageIndex = local.SelectedImageIndex = 5;
        treeSym.Nodes.Add(local);
      }
    }
    //========================================================================
    private void frmFileOpen_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void frmFileOpen_KeyPress(object sender, KeyPressEventArgs e)
    {
    	if(e.KeyChar == '\r')
    		txtFilename_KeyPress(sender, e);
    }
    //========================================================================
    private void btnLocal_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.No;
    }
    //========================================================================
    private void btnOpen_Click(object sender, EventArgs e)
    {
    	if(isSaveType)
    	{
        if(saveAsIsLocal)
        {
          bool exists = Util.FileExistsLocal(currLocal.Path+'\\'+txtFilename.Text);
          if(exists)
            if(MessageBox.Show("File \""+txtFilename.Text+"\" Already Exists\nOverwrite?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.No)
              return;
          DialogResult = DialogResult.Yes;
        }
        else
        {
          bool exists = Util.FileExistsSym(currInst, txtFilename.Text, SymFileTypeFromCombo());
          if(exists)
            if(MessageBox.Show("File \""+txtFilename.Text+"\" Already Exists\nOverwrite?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.No)
              return;
          DialogResult = DialogResult.Yes;
        }
      }
    	
    	if(lstResults.SelectedItems.Count > 0)
    	{
    		DialogResult = DialogResult.Cancel;
        if (isLocal)
        {
          if(retrieve)
          {
            List<LocalFile> results = new List<LocalFile>();
            for(int i=0; i<lstResults.SelectedItems.Count; i++)
              results.Add(localFiles[GetLocalFilesIndex(lstResults.SelectedItems[i].Text)]);
            localFiles = results;
          }
          else
            for(int i=0; i<lstResults.SelectedItems.Count; i++)
              Util.MainForm.OpenLocalFile(currLocal.Path + '\\' + lstResults.SelectedItems[i].Text);
        }
        else
        {
          if(retrieve)
          {
            List<SymFile> results = new List<SymFile>();
            for(int i=0; i<lstResults.SelectedItems.Count; i++)
              results.Add(symFiles[GetSymFilesIndex(lstResults.SelectedItems[i].Text)]);
            symFiles = results;
          }
          else
            for(int i=0; i<lstResults.SelectedItems.Count; i++)
    			    Util.MainForm.OpenSymitarFile(currInst, symFiles[GetSymFilesIndex(lstResults.SelectedItems[i].Text)]);
        }
        DialogResult = DialogResult.Yes;
    	}
    }
    //========================================================================
    private void txtFilename_KeyPress(object sender, KeyPressEventArgs e)
    {
    	if(isSaveType)
    	{
    		if(txtFilename.Text.Trim().Length > 0)
    			btnOpen.Enabled = true;
    		else
    			btnOpen.Enabled = false;
    		
    		if(e.KeyChar == '\r')
    		{
    			e.Handled = true;
    			if(btnOpen.Enabled == true)
    				btnOpen_Click(sender, e);
    		}
    		
    		return;
    	}

    	if(e.KeyChar == '\r')
    	{
    		e.Handled = true;

				lstResults.Items.Clear();
				btnOpen.Enabled = false;

        if(isLocal)
        {
          localFiles = Util.FileListLocal(currLocal, txtFilename.Text);
          if(localFiles.Count == 0)
          {
            MessageBox.Show("No Files Found", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
          }
          if((localFiles.Count == 1) && (!retrieve))
          {
            DialogResult = DialogResult.Cancel;
            Util.MainForm.OpenLocalFile(currLocal.Path+'\\'+localFiles[0].name);
            Close();
            return;
          }
          for(int i=0; i<localFiles.Count; i++)
          {
            string name = localFiles[i].name;
            string date = localFiles[i].date.ToString("MM/dd/yyyy");
            string size = Util.FormatBytes(localFiles[i].size);
            int iconIdx = 4;
            if(localFiles[i].type == SymFile.Type.REPGEN)
            {
              iconIdx = 3;
              if(name.Length > 3)
              	if(name.Substring(name.Length-4).ToUpper() == ".PWR")
                	iconIdx = 2;
            }
            lstResults.Items.Add(new ListViewItem(new string[] { name, date, size }, iconIdx));
          }
        }
        else
        {
          try
          {
            symFiles = Util.FileListSym(currInst, txtFilename.Text, SymFileTypeFromCombo());
          }
          catch(Exception ex)
          {
            MessageBox.Show("Error Retrieving File List\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
          if(symFiles.Count == 0)
          {
            MessageBox.Show("No Files Found", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
          }
          if((symFiles.Count == 1) && (!retrieve))
          {
            DialogResult = DialogResult.Cancel;
            Util.MainForm.OpenSymitarFile(currInst, symFiles[0]);
            Close();
            return;
          }
          for(int i=0; i<symFiles.Count; i++)
          {
            string name = symFiles[i].name;
            string date = symFiles[i].date.ToString("MM/dd/yyyy");
            string size = Util.FormatBytes(symFiles[i].size);
            int iconIdx = 4;
            if(symFiles[i].type == SymFile.Type.REPGEN)
            {
              iconIdx = 3;
              if(name.Length > 3)
              	if(name.Substring(name.Length-4).ToUpper() == ".PWR")
                	iconIdx = 2;
            }
            lstResults.Items.Add(new ListViewItem(new string[]{ name, date, size }, iconIdx));
          }
        }
    	}
    }
    //========================================================================
    private void treeSym_AfterSelect(object sender, TreeViewEventArgs e)
    {    	
    	if(e.Node.ImageIndex==1)
    	{
    		string serverName = e.Node.Parent.Text;
    		string symDir = e.Node.Text.Replace("Sym ", "");
    		currInst = Config.GetSym(serverName, symDir);
        isLocal = false;
    	}
      else if (e.Node.ImageIndex == 5)
      {
        currLocal = Config.GetLocal(e.Node.Text);
        isLocal = true;
      }
      else
      {
        currInst  = null;
        currLocal = null;
      }
    	
    	if(currInst != null)
    	{
    		txtFilename.Enabled = true;
    		if((txtFilename.Text != "") && (isSaveType))
    			btnOpen.Enabled = true;
    	}
      else if(currLocal != null)
      {
        txtFilename.Enabled = true;
        if((txtFilename.Text != "") && (isSaveType))
          btnOpen.Enabled = true;
      }
    	else
    	{
    		txtFilename.Enabled = false;
    		btnOpen.Enabled = false;
    		lstResults.Items.Clear();
    	}
    }
    //========================================================================
    private void lstResults_SelectedIndexChanged(object sender, EventArgs e)
    {
    	if(lstResults.SelectedItems.Count > 0)
    		btnOpen.Enabled = true;
    	else
    		btnOpen.Enabled = false;
    }
    //------------------------------------------------------------------------
    private void lstResults_DoubleClick(object sender, EventArgs e)
    {
    	if(lstResults.SelectedItems.Count > 0)
    	{
        if(isLocal)
        {
        	if(retrieve)
        	{
        		List<LocalFile> result = new List<LocalFile>();
        		result.Add(localFiles[GetLocalFilesIndex(lstResults.SelectedItems[0].Text)]);
        		localFiles = result;
        		DialogResult = DialogResult.OK;
        		return;
        	}
          DialogResult = DialogResult.Cancel;
          Util.MainForm.OpenLocalFile(currLocal.Path+'\\'+lstResults.SelectedItems[0].Text);
          Close();
          return;
        }
        else
        {
        	if(retrieve)
        	{
        		List<SymFile> result = new List<SymFile>();
        		result.Add(symFiles[GetSymFilesIndex(lstResults.SelectedItems[0].Text)]);
        		symFiles = result;
        		DialogResult = DialogResult.OK;
        		return;
        	}
          DialogResult = DialogResult.Cancel;
          Util.MainForm.OpenSymitarFile(currInst, symFiles[GetSymFilesIndex(lstResults.SelectedItems[0].Text)]);
          Close();
          return;
        }
    	}
    }
    //========================================================================
    private int GetSymFilesIndex(string name)
    {
    	for(int i=0; i<symFiles.Count; i++)
        if(symFiles[i].name == name)
    			return i;
    	throw new Exception("Results List Item Not Found in Current Sym Files List\nThis Shouldn't Happen");
    }
    //------------------------------------------------------------------------
    private int GetLocalFilesIndex(string name)
    {
    	for(int i=0; i<localFiles.Count; i++)
        if(localFiles[i].name == name)
    			return i;
    	throw new Exception("Results List Item Not Found in Current Local Files List\nThis Shouldn't Happen");
    }
    //========================================================================
    private SymFile.Type SymFileTypeFromCombo()
    {
      if ((string)comboType.SelectedItem == "REPGEN")
        return SymFile.Type.REPGEN;
      else
        return SymFile.Type.LETTER;
    }
    //========================================================================
  }
}
