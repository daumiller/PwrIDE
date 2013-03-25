using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using PwrPlus;
using Symitar;

namespace PwrIDE
{
  public partial class frmProject : DockContent
  {
    //========================================================================
    //  constructor/load/close
    //========================================================================
    public frmProject()
    {
      InitializeComponent();
    }
    //------------------------------------------------------------------------
    private void frmProject_Load(object sender, EventArgs e)
    {
      LoadRoots();
    }
    //------------------------------------------------------------------------
    private void frmProject_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }
    //========================================================================
    //  toolbar buttons
    //========================================================================
    private void toolAddServer_Click(object sender, EventArgs e)
    {
      frmAddServer fas = new frmAddServer();
      if (fas.ShowDialog(Util.MainForm) == DialogResult.OK)
      {
        SymServer added = new SymServer(fas.IP, int.Parse(fas.Port), fas.Alias, fas.User, fas.Pass, fas.Remember);
        Config.Servers.Add(added);
        int index = treeProj.Nodes.Count;
        for (int i = 0; i < treeProj.Nodes.Count; i++)
        {
          if (treeProj.Nodes[i].ImageIndex == 6)
          {
            index = i;
            break;
          }
        }
        TreeNode serv = new TreeNode(added.Alias);
        serv.ImageIndex = serv.SelectedImageIndex = 0;
        treeProj.Nodes.Insert(index, serv);
      }
      fas.Dispose();
    }
    //------------------------------------------------------------------------
    private void toolAddLocal_Click(object sender, EventArgs e)
    {
      frmAddLocal fal = new frmAddLocal();
      if (fal.ShowDialog(Util.MainForm) == DialogResult.OK)
      {
        Local added = new Local(fal.Alias, fal.Path);
        Config.Locals.Add(added);
        TreeNode locl = new TreeNode(added.Name);
        locl.ImageIndex = locl.SelectedImageIndex = 6;
        treeProj.Nodes.Add(locl);
        PopulateLocal(locl, added);
      }
      fal.Dispose();
    }
    //------------------------------------------------------------------------
    private void toolAddProject_Click(object sender, EventArgs e)
    {
      switch (treeProj.SelectedNode.ImageIndex)
      {
        case 1: //sym
          mnuSymAddProject_Click(sender, e);
          return;
        case 6: //local
          mnuLocalNewProject_Click(sender, e);
          return;
      }
    }
    //------------------------------------------------------------------------
    private void toolAddSym_Click(object sender, EventArgs e)
    {
      mnuServerAddSym_Click(sender, e);
    }
    //------------------------------------------------------------------------
    private void toolAddFile_Click(object sender, EventArgs e)
    {
      switch(treeProj.SelectedNode.ImageIndex)
      {
        case 2: //Project
          mnuProjectAddNew_Click(sender, e);
          break;
        case 1: //Sym
          mnuSymNewFile_Click(sender, e);
          break;
        case 6: //Local
          mnuLocalNewFile_Click(sender, e);
          break;
      }
    }
    //========================================================================
    //  context menus
    //========================================================================
    private void mnuServerAddSym_Click(object sender, EventArgs e)
    {
      SymServer server = ServerFromNode(treeProj.SelectedNode);
      frmAddSym fas = new frmAddSym(server);
      if (fas.ShowDialog(Util.MainForm) == DialogResult.OK)
      {
        SymInst added = new SymInst(server, fas.Sym, fas.ID, fas.Remember);
        server.Syms.Add(added);
        TreeNode node = new TreeNode("Sym "+added.SymDir);
        node.ImageIndex = node.SelectedImageIndex = 1;
        treeProj.SelectedNode.Nodes.Add(node);
        PopulateSym(node, added);
      }
      fas.Dispose();
    }
    //------------------------------------------------------------------------
    private void mnuServerSettings_Click(object sender, EventArgs e)
    {
      SymServer server = ServerFromNode(treeProj.SelectedNode);
      frmAddServer fas = new frmAddServer(server);
      if(fas.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        fas.Dispose();
        return;
      }
      server.Disconnect();
      server.Alias    = fas.Alias;
      server.IP       = fas.IP;
      server.Port     = int.Parse(fas.Port);
      server.AixUsr   = fas.User;
      server.AixPwd   = fas.Pass;
      server.Remember = fas.Remember;
      fas.Dispose();
      treeProj.SelectedNode.Text = server.Alias;
    }
    //------------------------------------------------------------------------
    private void mnuServerRemove_Click(object sender, EventArgs e)
    {
      SymServer server = ServerFromNode(treeProj.SelectedNode);
      treeProj.Nodes.Remove(treeProj.SelectedNode);
      Config.Servers.Remove(server);
    }
    //------------------------------------------------------------------------
    private void mnuSymAddProject_Click(object sender, EventArgs e)
    {
      SymInst inst = SymFromNode(treeProj.SelectedNode);
      InputBox inp = new InputBox("New Project", "Please Enter Project Name", "New Project", false);
      if(inp.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        inp.Dispose();
        return;
      }
      Project newProj = Project.CreateProject(inp.Input, inst);
      inp.Dispose();
      try
      {
        inst.ProjectsSave();
        TreeNode prjNode = new TreeNode(newProj.Name);
        prjNode.ImageIndex = prjNode.SelectedImageIndex = 2;
        treeProj.SelectedNode.Nodes.Add(prjNode);
        treeProj.SelectedNode.Expand();
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Saving Updated Project Listing to Sym\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        inst.Projects.Remove(newProj);
      }
    }
    //------------------------------------------------------------------------
    private void mnuSymNewFile_Click(object sender, EventArgs e)
    {
      SymInst inst = SymFromNode(treeProj.SelectedNode);

      frmAddProjectFile    fapf        = new frmAddProjectFile();
      string               fileName    = "";
      ProjectFile.FileType fileType    = ProjectFile.FileType.PWRPLS;
      SymFile.Type         fileSymType = SymFile.Type.PWRPLS;

      bool exists = true;
      while(exists)
      {
        if(fapf.ShowDialog(Util.MainForm) == DialogResult.Cancel)
        {
          fapf.Dispose();
          return;
        }
        fileName    = fapf.FileName;
        fileType    = fapf.FileType;
        fileSymType = fapf.SymFileType;
        try
        {
          exists = Util.FileExistsSym(inst, fileName, fileSymType);
        }
        catch(Exception ex)
        {
          fapf.Dispose();
          MessageBox.Show("Error Checking Whether File Exists\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      fapf.Dispose();

      try
      {
        Util.FileWriteSym(inst, fileName, fileSymType, "");
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Creating New File \""+fileName+"\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      frmSource newSource = new frmSource(inst, new SymFile(inst.Parent.IP, inst.SymDir, fileName, DateTime.Now, 0, fileSymType));
      newSource.Show(Util.MainForm.dockPanel);
      newSource.Activate();
      newSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuSymOpenFile_Click(object sender, EventArgs e)
    {
      SymInst inst = SymFromNode(treeProj.SelectedNode);
      frmFileOpen opener = new frmFileOpen(inst);
      opener.ShowDialog(Util.MainForm);
      opener.Dispose();
    }
    //------------------------------------------------------------------------
    private void mnuSymRunFile_Click(object sender, EventArgs e)
    {
      SymInst inst = SymFromNode(treeProj.SelectedNode);
      frmFileOpen opener = new frmFileOpen(inst, true);
      if(opener.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        opener.Dispose();
        return;
      }
      for(int i=0; i<opener.retrieveSym.Count; i++)
      {
        if(opener.retrieveSym[i].type == SymFile.Type.REPGEN)
        {
          frmRunRep runner = new frmRunRep(opener.retrieveSym[i], inst.Parent, inst);
          runner.Show();
        }
      }
      opener.Dispose();
    }
    //------------------------------------------------------------------------
    private void mnuSymConnect_Click(object sender, EventArgs e)
    {
      if(mnuSymConnect.Text == "&Connect")
      {
        SymInst inst = SymFromNode(treeProj.SelectedNode);
        if(Util.TrySymConnect(inst))
          PopulateSym(treeProj.SelectedNode, inst);
        treeProj.SelectedNode.NodeFont = new System.Drawing.Font(treeProj.Font, System.Drawing.FontStyle.Bold);
        treeProj.SelectedNode.Text = treeProj.SelectedNode.Text;
      }
      else
      {
        SymFromNode(treeProj.SelectedNode).Disconnect();
        treeProj.SelectedNode.Nodes.Clear();
        treeProj.SelectedNode.NodeFont = new System.Drawing.Font(treeProj.Font, System.Drawing.FontStyle.Regular);
        treeProj.SelectedNode.Text = treeProj.SelectedNode.Text;
      }
    }
    //------------------------------------------------------------------------
    private void mnuSymSettings_Click(object sender, EventArgs e)
    {
      SymInst inst = SymFromNode(treeProj.SelectedNode);
      frmAddSym fas = new frmAddSym(inst);
      if(fas.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        fas.Dispose();
        return;
      }
      string oldDir = inst.SymDir;
      string oldID  = inst.SymId;
      inst.SymDir   = fas.Sym;
      inst.SymId    = fas.ID;
      inst.Remember = fas.Remember;
      fas.Dispose();
      treeProj.SelectedNode.Text = "Sym "+inst.SymDir;
      if((oldDir != inst.SymDir) || (oldID != inst.SymId))
      {
      	treeProj.SelectedNode.Nodes.Clear();
      	inst.Disconnect();
      	if(Util.TrySymConnect(inst))
        	PopulateSym(treeProj.SelectedNode, inst);
      }
    }
    //------------------------------------------------------------------------
    private void mnuSymRemove_Click(object sender, EventArgs e)
    {
      SymInst sym = SymFromNode(treeProj.SelectedNode);
      sym.Disconnect();
      sym.Parent.Syms.Remove(sym);
      treeProj.SelectedNode.Parent.Nodes.Remove(treeProj.SelectedNode);
    }
    //------------------------------------------------------------------------
    private void mnuProjectAddExisting_Click(object sender, EventArgs e)
    {
      Project prj = ProjectFromNode(treeProj.SelectedNode);
      frmFileOpen open;
      if(prj.Local)
        open = new frmFileOpen(prj.ParentLocal, true);
      else
        open = new frmFileOpen(prj.ParentSym, true);
      if(open.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        open.Dispose();
        return;
      }
      if(prj.Local)
      {
        for(int i=0; i<open.retrieveLocal.Count; i++)
        {
        	int oldCount = prj.Files.Count;
          ProjectFile.FileType type = open.retrieveLocal[i].GetProjectFileType();
          prj.FileAdd(open.retrieveLocal[i].name, type);
          if(oldCount != prj.Files.Count) //really was added. This should be a call to PopulateProject, but...
          {
	          TreeNode fileNode = new TreeNode(open.retrieveLocal[i].name);
	          fileNode.ImageIndex = fileNode.SelectedImageIndex = IconIndexFromFileType(type);
	          treeProj.SelectedNode.Nodes.Add(fileNode);
	          treeProj.SelectedNode.Expand();
        	}
        }
        ProjectUpdateSave(prj);
      }
      else
      {
        for(int i=0; i<open.retrieveSym.Count; i++)
        {
        	int oldCount = prj.Files.Count;
          ProjectFile.FileType type = Util.GetSymProjectFileType(open.retrieveSym[i]);
          prj.FileAdd(open.retrieveSym[i].name, type);
          if(oldCount != prj.Files.Count) //really was added
          {
          	TreeNode fileNode = new TreeNode(open.retrieveSym[i].name);
          	fileNode.ImageIndex = fileNode.SelectedImageIndex = IconIndexFromFileType(type);
          	treeProj.SelectedNode.Nodes.Add(fileNode);
          	treeProj.SelectedNode.Expand();
          }
        }
        ProjectUpdateSave(prj);
      }
      open.Dispose();
    }
    //------------------------------------------------------------------------
    private void mnuProjectAddNew_Click(object sender, EventArgs e)
    {
      Project prj = ProjectFromNode(treeProj.SelectedNode);
      frmAddProjectFile    fapf        = new frmAddProjectFile();
      string               fileName    = "";
      ProjectFile.FileType fileType    = ProjectFile.FileType.PWRPLS;
      SymFile.Type         fileSymType = SymFile.Type.PWRPLS;

      bool exists=true;
      while(exists)
      {
        if(fapf.ShowDialog(Util.MainForm) == DialogResult.Cancel)
        {
          fapf.Dispose();
          return;
        }
        fileName    = fapf.FileName;
        fileType    = fapf.FileType;
        fileSymType = fapf.SymFileType;

        try
        {
          if (prj.Local)
            exists = Util.FileExistsLocal(prj.ParentLocal.Path + '\\' + fileName);
          else
            exists = Util.FileExistsSym(prj.ParentSym, fileName, fapf.SymFileType);
        }
        catch(Exception ex)
        {
          fapf.Dispose();
          MessageBox.Show("Error Checking Whether File Exists\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        if(exists)
        	MessageBox.Show("File \""+fileName+"\" Already Exists Here", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      fapf.Dispose();

      ProjectFile newPrjFile=null;
      try
      {
        newPrjFile = prj.FileCreate(fileName, fileType, fileSymType);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Creating New File \""+fileName+"\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

			ProjectUpdateSave(prj);

      TreeNode newNode = new TreeNode(fileName);
      newNode.ImageIndex = newNode.SelectedImageIndex = IconIndexFromFileType(fileType);
      treeProj.SelectedNode.Nodes.Add(newNode);
      treeProj.SelectedNode.Expand();

      frmSource newSource = new frmSource(newPrjFile);
      newSource.Show(Util.MainForm.dockPanel);
      newSource.Activate();
      newSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuProjectRename_Click(object sender, EventArgs e)
    {
      Project prj = ProjectFromNode(treeProj.SelectedNode);
      InputBox inp = new InputBox("Rename", "Enter New Project Name", prj.Name, false);
      if(inp.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        inp.Dispose();
        return;
      }
      treeProj.SelectedNode.Text = inp.Input;
      prj.Name = inp.Input;
      ProjectUpdateSave(prj);
    }
    //------------------------------------------------------------------------
    private void mnuProjectRemove_Click(object sender, EventArgs e)
    {
      Project prj = ProjectFromNode(treeProj.SelectedNode);
      treeProj.SelectedNode.Remove();
      if(prj.Local) prj.ParentLocal.ProjectRemove(prj);
      else          prj.ParentSym.ProjectRemove(prj);
      ProjectUpdateSave(prj);
    }
    //------------------------------------------------------------------------
    private void mnuProjectDelete_Click(object sender, EventArgs e)
    {
      Project prj = ProjectFromNode(treeProj.SelectedNode);
      if(MessageBox.Show("Delete Project \""+prj.Name+"\" and All Included Files?", "Delete Project", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        return;
      treeProj.SelectedNode.Parent.Nodes.Remove(treeProj.SelectedNode);
      try
      {
        if(prj.Local) prj.ParentLocal.ProjectDelete(prj);
        else          prj.ParentSym.ProjectDelete(prj);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Deleting Project Files\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      ProjectUpdateSave(prj);
    }
    //------------------------------------------------------------------------
    private void mnuFileCompile_Click(object sender, EventArgs e)
    {
      Project prj = new Project();
      ProjectFile file = ProjectFileFromNode(treeProj.SelectedNode, ref prj);
      if(file.Type != ProjectFile.FileType.PWRPLS) return;
      
      string input;
      try
      {
        input = file.Read();
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Reading File \""+file.Name+"\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      string output = Util.MainForm.Compile(input, file.Name, Util.MainForm.onCompileError, Util.MainForm.onCompileException, new Importer(prj));
      if(output == null) return;

      ProjectFile compiled = prj.AddCompiledSource(file, output);
      if(compiled == null)
      {
        frmSource src = new frmSource(file.Name, output);
        src.Show(Util.MainForm.dockPanel);
        src.Activate();
        src.icsEditor.Focus();
        return;
      }
      ProjectUpdateSave(prj);
      
      //see if this source is already open, or we need to open it
      frmSource update=null;
      IDockContent[] docs = Util.MainForm.dockPanel.DocumentsToArray();
      for(int i=0; i<docs.Length; i++)
      {
        frmSource curr = (frmSource)docs[i];
        if(curr.fileOrigin == frmSource.Origin.PROJECT)
        {
          if(curr.fileProject == compiled)
          {
            update = curr;
            break;
          }
        }
      }
      if(update == null)
      {
        frmSource src = new frmSource(compiled);
        src.Show(Util.MainForm.dockPanel);
        src.Activate();
        src.icsEditor.Focus();
      }
      else
      {
        if(update.ReloadProjectFile())
        {
          update.Activate();
          update.icsEditor.Focus();
        }
      }
    }
    //------------------------------------------------------------------------
    private void mnuFileInstall_Click(object sender, EventArgs e)
    {
      Project prj = new Project();
      ProjectFile file = ProjectFileFromNode(treeProj.SelectedNode, ref prj);
      if(prj.Local) return;
      if(file.Type != ProjectFile.FileType.REPGEN) return;
      
      RepErr err = RepErr.None();
      bool completed=false, tryAgain=true;
      while(tryAgain)
      {
        tryAgain = false;
        if(Util.TrySymConnect(prj.ParentSym))
        {
          try
          {
            err = prj.ParentSym.Connection.FileInstall(file.ToSymFile(prj.ParentSym));
            completed = true;
          }
          catch(Exception ex)
          {
            if (MessageBox.Show("Error Install File \""+file.Name+"\"\n"+ex.Message+"\n\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
              tryAgain = true;
            prj.ParentSym.Disconnect();
          }
        }
      }
      if(completed)
      {
        if(err.any == false)
          MessageBox.Show("Installed \""+file.Name+"\"\nSize: "+Util.FormatBytes(err.installedSize));
        else
          MessageBox.Show("FILE: "+err.fileErr+"\nLINE: "+err.line.ToString()+"\nCOL : "+err.column.ToString()+"\nERR : "+err.error.ToString());
      }
    }
    //------------------------------------------------------------------------
    private void mnuFileRun_Click(object sender, EventArgs e)
    {
      Project prj = new Project();
      ProjectFile file = ProjectFileFromNode(treeProj.SelectedNode, ref prj);
      if(prj.Local) return;
      if(file.Type != ProjectFile.FileType.REPGEN) return;

      frmRunRep runner = new frmRunRep(file.ToSymFile(prj.ParentSym), prj.ParentSym.Parent, prj.ParentSym);
      runner.Show();
    }
    //------------------------------------------------------------------------
    private void mnuFileRename_Click(object sender, EventArgs e)
    {
      Project prj = new Project();
      ProjectFile file = ProjectFileFromNode(treeProj.SelectedNode, ref prj);
      InputBox inp = new InputBox("Rename File", "Enter New Filename", file.Name, false);
      inp.MaxLength = 31;
      
      if(inp.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        inp.Dispose();
        return;
      }
      string newName = inp.Input;
      inp.Dispose();
      try
      {
        prj.FileRename(file, newName);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Renaming File \""+file.Name+"\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      ProjectUpdateSave(prj);
      treeProj.SelectedNode.Text = newName;

      //set title of any open copies of this source
      IDockContent[] docs = Util.MainForm.dockPanel.DocumentsToArray();
      for(int i=0; i<docs.Length; i++)
      {
        frmSource curr = (frmSource)docs[i];
        if(curr.fileOrigin == frmSource.Origin.PROJECT)
        {
          if(curr.fileProject == file)
            curr.ProjectFileRenamed();
          if(Util.MainForm.ActiveSource == curr)
            Util.MainForm.Text = curr.Text + " - PwrIDE";
        }
      }
    }
    //------------------------------------------------------------------------
    private void mnuFileRemove_Click(object sender, EventArgs e)
    {
      Project prj = new Project();
      ProjectFile file = ProjectFileFromNode(treeProj.SelectedNode, ref prj);
      treeProj.SelectedNode.Remove();
      prj.FileRemove(file);
      ProjectUpdateSave(prj);
    }
    //------------------------------------------------------------------------
    private void mnuFileDelete_Click(object sender, EventArgs e)
    {
      if(MessageBox.Show("Delete File \""+treeProj.SelectedNode.Text+"\"?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        return;
      Project prj = new Project();
      ProjectFile file = ProjectFileFromNode(treeProj.SelectedNode, ref prj);
      try
      {
        prj.FileDelete(file);
        treeProj.SelectedNode.Parent.Nodes.Remove(treeProj.SelectedNode);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Deleting File \""+file.Name+"\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      ProjectUpdateSave(prj);
    }
    //------------------------------------------------------------------------
    private void mnuLocalNewProject_Click(object sender, EventArgs e)
    {
      Local local = LocalFromNode(treeProj.SelectedNode);
      InputBox inp = new InputBox("New Project", "Please Enter Project Name", "New Project", false);
      if(inp.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        inp.Dispose();
        return;
      }
      Project newProj = Project.CreateProject(inp.Input, local);
      inp.Dispose();
      try
      {
        local.ProjectsSave();
        TreeNode prjNode = new TreeNode(newProj.Name);
        prjNode.ImageIndex = prjNode.SelectedImageIndex = 2;
        treeProj.SelectedNode.Nodes.Add(prjNode);
        treeProj.SelectedNode.Expand();
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Saving Updated Project Listing\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        local.Projects.Remove(newProj);
      }
    }
    //------------------------------------------------------------------------
    private void mnuLocalNewFile_Click(object sender, EventArgs e)
    {
      Local local = LocalFromNode(treeProj.SelectedNode);

      frmAddProjectFile    fapf        = new frmAddProjectFile();
      string               fileName    = "";
      ProjectFile.FileType fileType    = ProjectFile.FileType.PWRPLS;
      SymFile.Type         fileSymType = SymFile.Type.PWRPLS;

      bool exists = true;
      while(exists)
      {
        if(fapf.ShowDialog(Util.MainForm) == DialogResult.Cancel)
        {
          fapf.Dispose();
          return;
        }
        fileName    = fapf.FileName;
        fileType    = fapf.FileType;
        fileSymType = fapf.SymFileType;
        try
        {
          exists = Util.FileExistsLocal(local.Path+'\\'+fileName);
        }
        catch(Exception ex)
        {
          fapf.Dispose();
          MessageBox.Show("Error Checking Whether File Exists\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      fapf.Dispose();

      try
      {
        Util.FileWriteLocal(local.Path+'\\'+fileName, "");
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Creating New File \""+fileName+"\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      frmSource newSource = new frmSource(local.Path+'\\'+fileName);
      newSource.Show(Util.MainForm.dockPanel);
      newSource.Activate();
      newSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuLocalOpenFile_Click(object sender, EventArgs e)
    {
      Local local = LocalFromNode(treeProj.SelectedNode);
      frmFileOpen opener = new frmFileOpen(local);
      opener.ShowDialog(Util.MainForm);
      opener.Dispose();
    }
    //------------------------------------------------------------------------
    private void mnuLocalSettings_Click(object sender, EventArgs e)
    {
      Local local = LocalFromNode(treeProj.SelectedNode);
      frmAddLocal fal = new frmAddLocal(local);
      if(fal.ShowDialog(Util.MainForm) == DialogResult.Cancel)
      {
        fal.Dispose();
        return;
      }
      string oldPath = local.Path;
      local.Name = fal.Alias;
      local.Path = fal.Path;
      fal.Dispose();
      treeProj.SelectedNode.Text = local.Name;
      if(oldPath != local.Path)
      	PopulateLocal(treeProj.SelectedNode, local);
    }
    //------------------------------------------------------------------------
    private void mnuLocalRemove_Click(object sender, EventArgs e)
    {
      Local local = LocalFromNode(treeProj.SelectedNode);
      Config.Locals.Remove(local);
      treeProj.Nodes.Remove(treeProj.SelectedNode);
    }
    //------------------------------------------------------------------------
    private void mnuLocalDelete_Click(object sender, EventArgs e)
    {
      Local local = LocalFromNode(treeProj.SelectedNode);
      if(MessageBox.Show("Delete Directory \""+local.Path+"\" and All Included Files?", "Delete Local Directory", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        return;
      Config.Locals.Remove(local);
      try
      {
        Directory.Delete(local.Path, true);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Deleting Directory \"" + local.Path + "\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    //========================================================================
    //  treeView events
    //========================================================================
    private void treeProj_AfterSelect(object sender, TreeViewEventArgs e)
    {
      switch(e.Node.ImageIndex)
      {
        case 0: //Server
          toolAddProject.Enabled = false;
          toolAddFile.Enabled = false;
          toolAddSym.Enabled = true;
          break;
        default:
          toolAddProject.Enabled = true;
          toolAddFile.Enabled = true;
          toolAddSym.Enabled = false;
          break;
      }
      if((e.Node.ImageIndex==3) || (e.Node.ImageIndex==4) || (e.Node.ImageIndex==5))
      {
        Project prj = new Project();
        ProjectFile file = ProjectFileFromNode(e.Node, ref prj);

        if(file.Type != ProjectFile.FileType.PWRPLS)
          mnuFileCompile.Enabled = false;
        else
          mnuFileCompile.Enabled = true;

        if((!prj.Local) && (file.Type == ProjectFile.FileType.REPGEN))
          mnuFileInstall.Enabled = mnuFileRun.Enabled = true;
        else
          mnuFileInstall.Enabled = mnuFileRun.Enabled = false;
      }
    }
    //------------------------------------------------------------------------
    private void treeProj_AfterExpand(object sender, TreeViewEventArgs e)
    {
    	if(e.Node.ImageIndex==0)
    	{
    		SymServer serv = ServerFromNode(e.Node);
    		serv.Expanded = true;
    	}
    	else if(e.Node.ImageIndex==6)
    	{
    		Local locl = LocalFromNode(e.Node);
    		locl.Expanded = true;
    	}
    }
    //------------------------------------------------------------------------
    private void treeProj_AfterCollapse(object sender, TreeViewEventArgs e)
    {
    	if(e.Node.ImageIndex==0)
    	{
    		SymServer serv = ServerFromNode(e.Node);
    		serv.Expanded = false;
    	}
    	else if(e.Node.ImageIndex==6)
    	{
    		Local locl = LocalFromNode(e.Node);
    		locl.Expanded = false;
    	}
    }
    //------------------------------------------------------------------------
    private void treeProj_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        treeProj.SelectedNode = e.Node;
        switch(e.Node.ImageIndex)
        {
          case 0: //Server
            mnuServer.Show(MousePosition);
            break;
          case 1: //Sym
            if(SymFromNode(e.Node).Connected())
            {
              mnuSymConnect.Text = "&Disconnect";
              mnuSymAddProject.Enabled = true;
              mnuSymOpenFile.Enabled = true;
              mnuSymRunFile.Enabled = true;
            }
            else
            {
              mnuSymConnect.Text = "&Connect";
              mnuSymAddProject.Enabled = false;
              mnuSymOpenFile.Enabled = false;
              mnuSymRunFile.Enabled = false;
            }
            mnuSym.Show(MousePosition);
            break;
          case 2: //Project
            mnuProject.Show(MousePosition);
            break;
          case 3: //PwrPlus
            mnuFileCompile.Enabled = true;
            mnuFileInstall.Enabled = false;
            mnuFileRun.Enabled     = false;
            mnuFile.Show(MousePosition);
            break;
          case 4: //RepGen
            mnuFileCompile.Enabled = false;
            mnuFileInstall.Enabled = false;
            mnuFileRun.Enabled     = false;
            if(e.Node.Parent.ImageIndex == 1) //sym
            {
              mnuFileInstall.Enabled = true;
              mnuFileRun.Enabled     = true;
            }
            if(e.Node.Parent.Parent.ImageIndex == 1) //sym
            {
              mnuFileInstall.Enabled = true;
              mnuFileRun.Enabled     = true;
            }
            mnuFile.Show(MousePosition);
            break;
          case 5: //Letter
            mnuFileCompile.Enabled = false;
            mnuFileInstall.Enabled = false;
            mnuFileRun.Enabled     = false;
            mnuFile.Show(MousePosition);
            break;
          case 6: //Local
            mnuLocal.Show(MousePosition);
            break;
        }
      }
    }
    //------------------------------------------------------------------------
    private void treeProj_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
    	treeProj.SelectedNode = e.Node;
      switch(e.Node.ImageIndex)
      {
        case 1: //Sym
          SymInst inst = SymFromNode(e.Node);
          if(inst.Connected()==false)
          	mnuSymConnect_Click(sender, new EventArgs());
          return;
      }
      if((e.Node.ImageIndex==3) || (e.Node.ImageIndex==4) || (e.Node.ImageIndex==5))
      {
      	Project prj = new Project();
      	ProjectFile file = ProjectFileFromNode(e.Node, ref prj);
      	frmSource src = new frmSource(file);
        src.Show(Util.MainForm.dockPanel);
        src.Activate();
        src.icsEditor.Focus();
      }
    }
    //------------------------------------------------------------------------
    private void treeProj_KeyDown(object sender, KeyEventArgs e)
    {
    	if(e.KeyCode == Keys.F2)
    	{
    		switch(treeProj.SelectedNode.ImageIndex)
    		{
    			case 0: mnuServerSettings_Click(sender, new EventArgs()); break;
    			case 1: mnuSymSettings_Click   (sender, new EventArgs()); break;
    			case 2: mnuProjectRename_Click (sender, new EventArgs()); break;
    			case 3: mnuFileRename_Click    (sender, new EventArgs()); break;
    			case 4: mnuFileRename_Click    (sender, new EventArgs()); break;
    			case 5: mnuFileRename_Click    (sender, new EventArgs()); break;
    			case 6: mnuLocalSettings_Click (sender, new EventArgs()); break;
    		}
    	}
    	else if(e.KeyCode == Keys.Delete)
    	{
    		if(e.Shift)
    		{
    			switch(treeProj.SelectedNode.ImageIndex)
    			{
	    			case 0: mnuServerRemove_Click (sender, new EventArgs()); break;
	    			case 1: mnuSymRemove_Click    (sender, new EventArgs()); break;
	    			case 2: mnuProjectDelete_Click(sender, new EventArgs()); break;
	    			case 3: mnuFileDelete_Click   (sender, new EventArgs()); break;
	    			case 4: mnuFileDelete_Click   (sender, new EventArgs()); break;
	    			case 5: mnuFileDelete_Click   (sender, new EventArgs()); break;
	    			case 6: mnuLocalDelete_Click  (sender, new EventArgs()); break;
    			}
    		}
    		else
    		{
    			switch(treeProj.SelectedNode.ImageIndex)
    			{
	    			case 0: mnuServerRemove_Click (sender, new EventArgs()); break;
	    			case 1: mnuSymRemove_Click    (sender, new EventArgs()); break;
	    			case 2: mnuProjectRemove_Click(sender, new EventArgs()); break;
	    			case 3: mnuFileRemove_Click   (sender, new EventArgs()); break;
	    			case 4: mnuFileRemove_Click   (sender, new EventArgs()); break;
	    			case 5: mnuFileRemove_Click   (sender, new EventArgs()); break;
	    			case 6: mnuLocalRemove_Click  (sender, new EventArgs()); break;
    			}
    		}
    	}
    }
    //------------------------------------------------------------------------
    private void treeProj_ItemDrag(object sender, ItemDragEventArgs e)
    {
      TreeNode src=(TreeNode)e.Item; treeProj.Tag = null;
      Project prj=null; ProjectFile prjFile=null;

      switch(src.ImageIndex)
      {
        case 2: prj = ProjectFromNode(src); break;                  //Project
        case 3: prjFile = ProjectFileFromNode(src, ref prj); break; //PwrPlus
        case 4: prjFile = ProjectFileFromNode(src, ref prj); break; //RepGen
        case 5: prjFile = ProjectFileFromNode(src, ref prj); break; //Letterfile
      }
      if(prj==null) return;
      treeProj.Tag = prj;

      //Dragging a Whole Project
      if(prjFile == null)
      {
        DoDragDrop("@ValidTreeDrag", DragDropEffects.Copy | DragDropEffects.Scroll);
        return;
      }
      
      //Dragging a Single File
      int i;
      for(i=0; i<prj.Files.Count; i++)
        if(prj.Files[i] == prjFile)
          break;
      DoDragDrop("@ValidTreeDrag:"+i.ToString(), DragDropEffects.Copy | DragDropEffects.Scroll);
    }
    //------------------------------------------------------------------------
    private void treeProj_DragEnter(object sender, DragEventArgs e)
    {
      if(e.Data.GetDataPresent(DataFormats.Text))
        if(((string)e.Data.GetData("Text")).StartsWith("@ValidTreeDrag"))
          e.Effect = (DragDropEffects.Copy | DragDropEffects.Scroll);
    }
    //------------------------------------------------------------------------
    [System.Runtime.InteropServices.DllImportAttribute("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    private void treeProj_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.None;
      
      //allow Drag only to Sym, Project, or Local nodes
      System.Drawing.Point pt = treeProj.PointToClient(new System.Drawing.Point(e.X, e.Y));
      TreeNode dest = treeProj.GetNodeAt(pt);
      treeProj.SelectedNode = dest;
      if(e.Data.GetDataPresent(DataFormats.Text))
        if(((string)e.Data.GetData("Text")).StartsWith("@ValidTreeDrag"))
          if((dest.ImageIndex == 1) || (dest.ImageIndex == 2) || (dest.ImageIndex == 6)) //Sym, Project, or Local
            if((((string)e.Data.GetData("Text")).IndexOf(':')!=-1) || (dest.ImageIndex != 2)) //no Project->Project copy
              e.Effect = DragDropEffects.Copy;

      //Handle Drag-Scrolling
      if(pt.Y < 20) SendMessage(treeProj.Handle, 277, 0, 0);
      if((pt.Y + 20) > treeProj.Height) SendMessage(treeProj.Handle, 277, 1, 0);
    }
    //------------------------------------------------------------------------
    private void treeProj_DragDrop(object sender, DragEventArgs e)
    {
      System.Drawing.Point pt = treeProj.PointToClient(new System.Drawing.Point(e.X, e.Y));
      TreeNode dest = treeProj.GetNodeAt(pt);
      Project prj=null; ProjectFile prjFile=null;

      if(e.Data.GetDataPresent(DataFormats.Text))
      {
        if(((string)e.Data.GetData("Text")).StartsWith("@ValidTreeDrag"))
        {
          if((dest.SelectedImageIndex == 1) || (dest.SelectedImageIndex == 2) || (dest.SelectedImageIndex == 6)) //Sym, Project, or Local
          {
            string key = (string)e.Data.GetData("Text");
            prj = (Project)treeProj.Tag;
            
            if (key.IndexOf(':') > -1) //copying a specific file
            {
              int idx = int.Parse(key.Substring(key.IndexOf(':') + 1));
              treeProj.Tag = null;
      				if(prj == null) return;
              prjFile = prj.Files[idx];
              
              if(dest.ImageIndex == 1) //copy to Sym
              {
              	SymInst inst = SymFromNode(dest);
              	SymFile sfil = prjFile.ToSymFile(inst);
              	             	
              	if(Util.FileExistsSym(inst, sfil))
              		if(MessageBox.Show("File '"+prjFile.Name+"' Already Exists.\nOverwrite?","File Copy",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
              			return;
              	try
              	{
              		Util.FileWriteSym(inst, sfil, prjFile.Read());
              	}
              	catch(Exception ex)
              	{
              		MessageBox.Show("Error Copying File '"+prjFile.Name+"'\nError: \""+ex.Message+'"', "File Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
              		return;
              	}
              }
              else if(dest.ImageIndex == 6) //copy to Local
              {
              	Local locl = LocalFromNode(dest);
              	if(Util.FileExistsLocal(locl.Path+"\\"+prjFile.Name))
              		if(MessageBox.Show("File '"+prjFile.Name+"' Already Exists.\nOverwrite?","File Copy",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
              			return;
              	try
              	{
              		Util.FileWriteLocal(locl.Path+"\\"+prjFile.Name, prjFile.Read());
              	}
              	catch(Exception ex)
              	{
              		MessageBox.Show("Error Copying File '"+prjFile.Name+"'\nError: \""+ex.Message+'"', "File Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
              		return;
              	}
              }
              else if(dest.ImageIndex == 2) //copy to Project
              {
              	Project prjDest = ProjectFromNode(dest);
              	if(
              		(( prjDest.Local) && (Util.FileExistsLocal(prjDest.ParentLocal.Path+"\\"+prjFile.Name))) ||
              		((!prjDest.Local) && (Util.FileExistsSym(  prjDest.ParentSym, prjFile.ToSymFile(prjDest.ParentSym))))
              		)
              		if(MessageBox.Show("File '"+prjFile.Name+"' Already Exists.\nOverwrite?","File Copy",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
              			return;
              	try
              	{
              		int oldCount = prjDest.Files.Count;
              		string content = prjFile.Read();
              		ProjectFile filn = prjDest.FileCreate(prjFile.Name, prjFile.Type, prjFile.TypeConvert());
              		filn.Write(content);
              		if(oldCount != prjDest.Files.Count)
              		{
              			TreeNode fileNode = new TreeNode(prjFile.Name);
				          	fileNode.ImageIndex = fileNode.SelectedImageIndex = IconIndexFromFileType(prjFile.Type);
				          	dest.Nodes.Add(fileNode);
				          	dest.Expand();
				          	ProjectUpdateSave(prjDest);
				          }
              	}
              	catch(Exception ex)
              	{
              		MessageBox.Show("Error Copying File '"+prjFile.Name+"'\nError: \""+ex.Message+'"', "File Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
              		return;
              	}
              }              
            }
            else //Copying an entire project
            {
            	treeProj.Tag = null;
      				if(prj == null) return;
      				
            	if(dest.SelectedImageIndex == 1) //Sym
            	{
            		SymInst cSym = SymFromNode(dest);
            		if(prj.CopyTo(cSym))
            			PopulateSym(dest, cSym);
            	}
            	else if(dest.SelectedImageIndex == 6) //Local
            	{
            		Local cLcl = LocalFromNode(dest);
            		if(prj.CopyTo(cLcl))
            			PopulateLocal(dest, cLcl);
            	}
            }
          }
        }
      }
    }
    //========================================================================
    //  Project Compiled Source Adding Events
    //========================================================================
    public void ProjectAddCompiled(Project proj, ProjectFile file)
    {
      TreeNode projNode = FindProjectNode(proj);
      TreeNode fileNode = new TreeNode(file.Name);
      fileNode.ImageIndex = fileNode.SelectedImageIndex = 4;
      projNode.Nodes.Add(fileNode);
    }
    //========================================================================
    //  helpers
    //========================================================================
    private TreeNode FindProjectNode(Project proj)
    {
      if(proj.Local)
      {
        for(int i=0; i<treeProj.Nodes.Count; i++)
          if(treeProj.Nodes[i].ImageIndex == 6)
            if(treeProj.Nodes[i].Text == proj.ParentLocal.Name)
              for(int c=0; c<treeProj.Nodes[i].Nodes.Count; c++)
                if(treeProj.Nodes[i].Nodes[c].ImageIndex==2)
                  if(treeProj.Nodes[i].Nodes[c].Text == proj.Name)
                    return treeProj.Nodes[i].Nodes[c];
      }
      else
      {
        for(int i=0; i<treeProj.Nodes.Count; i++)
          if(treeProj.Nodes[i].ImageIndex == 0)
            if(treeProj.Nodes[i].Text == proj.ParentSym.Parent.Alias)
              for(int c=0; c<treeProj.Nodes[i].Nodes.Count; c++)
                if(treeProj.Nodes[i].Nodes[c].ImageIndex == 1)
                  if(treeProj.Nodes[i].Nodes[c].Text == "Sym "+proj.ParentSym.SymDir)
                    for(int k=0; k<treeProj.Nodes[i].Nodes[c].Nodes.Count; k++)
                      if(treeProj.Nodes[i].Nodes[c].Nodes[k].ImageIndex == 2)
                        if(treeProj.Nodes[i].Nodes[c].Nodes[k].Text == proj.Name)
                          return treeProj.Nodes[i].Nodes[c].Nodes[k];
      }
      throw new Exception("Project Node Not Found\nThis Shouldn't Happen");
    }
    //------------------------------------------------------------------------
    private SymServer ServerFromNode(TreeNode tn)
    {
      for(int i=0; i<Config.Servers.Count; i++)
        if(Config.Servers[i].Alias == tn.Text)
          return Config.Servers[i];
      throw new Exception("Project Server Not Found in Config.Servers\nThis Shouldn't Happen");
    }
    //------------------------------------------------------------------------
    private SymInst SymFromNode(TreeNode tn)
    {
      string symDir = tn.Text.Substring(4);
      for(int i=0; i<Config.Servers.Count; i++)
      {
        if(Config.Servers[i].Alias == tn.Parent.Text)
        {
          for(int c=0; c<Config.Servers[i].Syms.Count; c++)
            if(Config.Servers[i].Syms[c].SymDir == symDir)
              return Config.Servers[i].Syms[c];
        }
      }
      throw new Exception("Project Sym Not Found in Config.Servers\nThis Shouldn't Happen");
    }
    //------------------------------------------------------------------------
    private Local LocalFromNode(TreeNode tn)
    {
      for(int i=0; i<Config.Locals.Count; i++)
        if(Config.Locals[i].Name == tn.Text)
          return Config.Locals[i];
      throw new Exception("Project Local Not Found in Config.Locals\nThis Shouldn't Happen");
    }
    //------------------------------------------------------------------------
    private Project ProjectFromNode(TreeNode tn)
    {
      //get parent SymInst/Local
      SymInst inst=null;
      Local  local=null;
      try
      {
        inst = SymFromNode(tn.Parent);
      }
      catch(Exception)
      {
        local = LocalFromNode(tn.Parent);
      }
      if(inst != null)
        for(int i=0; i<inst.Projects.Count; i++)
          if(inst.Projects[i].Name == tn.Text)
            return inst.Projects[i];
      for(int i=0; i<local.Projects.Count; i++)
        if(local.Projects[i].Name == tn.Text)
          return local.Projects[i];
      throw new Exception("Fell Through ProjectFromNode\nThis Shouldn't Happen");
    }
    //------------------------------------------------------------------------
    private ProjectFile ProjectFileFromNode(TreeNode tn, ref Project prj)
    {
      ProjectFile.FileType type = FileTypeFromIconIndex(tn.ImageIndex);
      prj = ProjectFromNode(tn.Parent);
      for(int i=0; i<prj.Files.Count; i++)
        if(prj.Files[i].Type == type)
          if(prj.Files[i].Name == tn.Text)
            return prj.Files[i];
      throw new Exception("Project File Not Found in ProjectFileFromNode\nThis Shouldn't Happen");
    }
    //------------------------------------------------------------------------
    public int IconIndexFromFileType(ProjectFile.FileType type)
    {
      if (type == ProjectFile.FileType.PWRPLS) return 3;
      if (type == ProjectFile.FileType.REPGEN) return 4;
      return 5;
    }
    //------------------------------------------------------------------------
    public ProjectFile.FileType FileTypeFromIconIndex(int idx)
    {
      if(idx==3) return ProjectFile.FileType.PWRPLS;
      if(idx==4) return ProjectFile.FileType.REPGEN;
      return ProjectFile.FileType.LETTER;
    }
    //------------------------------------------------------------------------
    private void LoadRoots()
    {
      treeProj.Nodes.Clear();
      //Load Servers
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
        treeProj.Nodes.Add(serv);
        if(Config.Servers[i].Expanded)
        	serv.Expand();
      }
      //Load Local Directories
      for(int i=0; i<Config.Locals.Count; i++)
      {
        TreeNode locl = new TreeNode(Config.Locals[i].Name);
        locl.ImageIndex = locl.SelectedImageIndex = 6;
        treeProj.Nodes.Add(locl);
        PopulateLocal(locl, Config.Locals[i]);
        if(Config.Locals[i].Expanded)
        	locl.Expand();
      }
    }
    //------------------------------------------------------------------------
    private void PopulateSym(TreeNode node, SymInst inst)
    {
      node.Nodes.Clear(); 

      //load projects
      try
      {
        inst.ProjectsLoad();
        for(int c=0; c<inst.Projects.Count; c++)
        {
          TreeNode proj = new TreeNode(inst.Projects[c].Name);
          proj.ImageIndex = proj.SelectedImageIndex = 2;
          for(int k=0; k<inst.Projects[c].Files.Count; k++)
          {
            TreeNode file = new TreeNode(inst.Projects[c].Files[k].Name);
            file.ImageIndex = file.SelectedImageIndex = IconIndexFromFileType(inst.Projects[c].Files[k].Type);
            proj.Nodes.Add(file);
          }
          node.Nodes.Add(proj);
        }
        node.Expand();
      }
      catch(FileNotFoundException)
      {
      	//assume there isn't one
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Loading Projects File for Sym \""+inst.SymDir+"\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    //------------------------------------------------------------------------
    private void PopulateLocal(TreeNode node, Local local)
    {
      node.Nodes.Clear();
      try
      {
        local.ProjectsLoad();
        for(int c=0; c<local.Projects.Count; c++)
        {
          TreeNode proj = new TreeNode(local.Projects[c].Name);
          proj.ImageIndex = proj.SelectedImageIndex = 2;
          for(int k=0; k<local.Projects[c].Files.Count; k++)
          {
            TreeNode file = new TreeNode(local.Projects[c].Files[k].Name);
            file.ImageIndex = file.SelectedImageIndex = IconIndexFromFileType(local.Projects[c].Files[k].Type);
            proj.Nodes.Add(file);
          }
          node.Nodes.Add(proj);
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Loading Projects File for Local Directory \""+local.Name+"\"\n"+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    //------------------------------------------------------------------------
    public void ProjectUpdateSave(Project proj)
    {
      if (proj.Local)
      {
        try
        {
          proj.ParentLocal.ProjectsSave();
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error Saving Updated Project Listing\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      else
      {
        try
        {
          proj.ParentSym.ProjectsSave();
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error Saving Updated Project Listing\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }
    //========================================================================
    public void ProjectsImported(SymInst inst)
    {
      //repopulate sym after RepDev projects import
      for(int i=0; i<treeProj.Nodes.Count; i++)
        if(treeProj.Nodes[i].Text == inst.Parent.Alias)
          for(int x=0; x<treeProj.Nodes[i].Nodes.Count; x++)
            if(treeProj.Nodes[i].Nodes[x].Text == "Sym "+inst.SymDir)
              PopulateSym(treeProj.Nodes[i].Nodes[x], inst);
    }
    //========================================================================
  }
}
