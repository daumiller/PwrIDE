namespace PwrIDE
{
  partial class frmProject
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProject));
      this.stripTool = new System.Windows.Forms.ToolStrip();
      this.toolAddServer = new System.Windows.Forms.ToolStripButton();
      this.toolAddSym = new System.Windows.Forms.ToolStripButton();
      this.toolAddProject = new System.Windows.Forms.ToolStripButton();
      this.toolAddFile = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolAddLocal = new System.Windows.Forms.ToolStripButton();
      this.treeProj = new System.Windows.Forms.TreeView();
      this.icons = new System.Windows.Forms.ImageList(this.components);
      this.mnuServer = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuServerAddSym = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuServerSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuServerRemove = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSym = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSymAddProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSymNewFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuSymOpenFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSymRunFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuSymConnect = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuSymSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSymRemove = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuProject = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuProjectAddExisting = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuProjectAddNew = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuProjectRename = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuProjectRemove = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuProjectDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFile = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuFileInstall = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileRun = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFileRename = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFileRemove = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuLocalNewProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuLocalNewFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuLocalOpenFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuLocalSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuLocalRemove = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuLocalDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.stripTool.SuspendLayout();
      this.mnuServer.SuspendLayout();
      this.mnuSym.SuspendLayout();
      this.mnuProject.SuspendLayout();
      this.mnuFile.SuspendLayout();
      this.mnuLocal.SuspendLayout();
      this.SuspendLayout();
      // 
      // stripTool
      // 
      this.stripTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.stripTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddServer,
            this.toolAddSym,
            this.toolAddProject,
            this.toolAddFile,
            this.toolStripSeparator1,
            this.toolAddLocal});
      this.stripTool.Location = new System.Drawing.Point(0, 0);
      this.stripTool.Name = "stripTool";
      this.stripTool.Size = new System.Drawing.Size(243, 25);
      this.stripTool.TabIndex = 0;
      this.stripTool.Text = "stripTool";
      // 
      // toolAddServer
      // 
      this.toolAddServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolAddServer.Image = ((System.Drawing.Image)(resources.GetObject("toolAddServer.Image")));
      this.toolAddServer.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolAddServer.Name = "toolAddServer";
      this.toolAddServer.Size = new System.Drawing.Size(23, 22);
      this.toolAddServer.Text = "toolStripButton1";
      this.toolAddServer.ToolTipText = "Add Server";
      this.toolAddServer.Click += new System.EventHandler(this.toolAddServer_Click);
      // 
      // toolAddSym
      // 
      this.toolAddSym.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolAddSym.Enabled = false;
      this.toolAddSym.Image = ((System.Drawing.Image)(resources.GetObject("toolAddSym.Image")));
      this.toolAddSym.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolAddSym.Name = "toolAddSym";
      this.toolAddSym.Size = new System.Drawing.Size(23, 22);
      this.toolAddSym.Text = "toolStripButton2";
      this.toolAddSym.ToolTipText = "Add Sym";
      this.toolAddSym.Click += new System.EventHandler(this.toolAddSym_Click);
      // 
      // toolAddProject
      // 
      this.toolAddProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolAddProject.Enabled = false;
      this.toolAddProject.Image = ((System.Drawing.Image)(resources.GetObject("toolAddProject.Image")));
      this.toolAddProject.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolAddProject.Name = "toolAddProject";
      this.toolAddProject.Size = new System.Drawing.Size(23, 22);
      this.toolAddProject.Text = "toolStripButton3";
      this.toolAddProject.ToolTipText = "Add Project";
      this.toolAddProject.Click += new System.EventHandler(this.toolAddProject_Click);
      // 
      // toolAddFile
      // 
      this.toolAddFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolAddFile.Enabled = false;
      this.toolAddFile.Image = ((System.Drawing.Image)(resources.GetObject("toolAddFile.Image")));
      this.toolAddFile.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolAddFile.Name = "toolAddFile";
      this.toolAddFile.Size = new System.Drawing.Size(23, 22);
      this.toolAddFile.Text = "toolStripButton4";
      this.toolAddFile.ToolTipText = "Add File";
      this.toolAddFile.Click += new System.EventHandler(this.toolAddFile_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolAddLocal
      // 
      this.toolAddLocal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolAddLocal.Image = ((System.Drawing.Image)(resources.GetObject("toolAddLocal.Image")));
      this.toolAddLocal.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolAddLocal.Name = "toolAddLocal";
      this.toolAddLocal.Size = new System.Drawing.Size(23, 22);
      this.toolAddLocal.Text = "toolStripButton5";
      this.toolAddLocal.ToolTipText = "Add Local Directory";
      this.toolAddLocal.Click += new System.EventHandler(this.toolAddLocal_Click);
      // 
      // treeProj
      // 
      this.treeProj.AllowDrop = true;
      this.treeProj.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeProj.ImageIndex = 0;
      this.treeProj.ImageList = this.icons;
      this.treeProj.Location = new System.Drawing.Point(0, 25);
      this.treeProj.Name = "treeProj";
      this.treeProj.SelectedImageIndex = 0;
      this.treeProj.Size = new System.Drawing.Size(243, 291);
      this.treeProj.TabIndex = 1;
      this.treeProj.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeProj_NodeMouseDoubleClick);
      this.treeProj.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeProj_AfterCollapse);
      this.treeProj.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeProj_DragDrop);
      this.treeProj.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeProj_AfterSelect);
      this.treeProj.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeProj_DragEnter);
      this.treeProj.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeProj_NodeMouseClick);
      this.treeProj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeProj_KeyDown);
      this.treeProj.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeProj_AfterExpand);
      this.treeProj.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeProj_ItemDrag);
      this.treeProj.DragOver += new System.Windows.Forms.DragEventHandler(this.treeProj_DragOver);
      // 
      // icons
      // 
      this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
      this.icons.TransparentColor = System.Drawing.Color.Transparent;
      this.icons.Images.SetKeyName(0, "SERVER");
      this.icons.Images.SetKeyName(1, "SYM");
      this.icons.Images.SetKeyName(2, "PROJECT");
      this.icons.Images.SetKeyName(3, "PWR");
      this.icons.Images.SetKeyName(4, "REP");
      this.icons.Images.SetKeyName(5, "LTR");
      this.icons.Images.SetKeyName(6, "LOCAL");
      // 
      // mnuServer
      // 
      this.mnuServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuServerAddSym,
            this.toolStripMenuItem1,
            this.mnuServerSettings,
            this.mnuServerRemove});
      this.mnuServer.Name = "mnuServer";
      this.mnuServer.Size = new System.Drawing.Size(153, 76);
      // 
      // mnuServerAddSym
      // 
      this.mnuServerAddSym.Image = ((System.Drawing.Image)(resources.GetObject("mnuServerAddSym.Image")));
      this.mnuServerAddSym.Name = "mnuServerAddSym";
      this.mnuServerAddSym.Size = new System.Drawing.Size(152, 22);
      this.mnuServerAddSym.Text = "&Add Sym";
      this.mnuServerAddSym.Click += new System.EventHandler(this.mnuServerAddSym_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
      // 
      // mnuServerSettings
      // 
      this.mnuServerSettings.Image = ((System.Drawing.Image)(resources.GetObject("mnuServerSettings.Image")));
      this.mnuServerSettings.Name = "mnuServerSettings";
      this.mnuServerSettings.Size = new System.Drawing.Size(152, 22);
      this.mnuServerSettings.Text = "Server Settings";
      this.mnuServerSettings.Click += new System.EventHandler(this.mnuServerSettings_Click);
      // 
      // mnuServerRemove
      // 
      this.mnuServerRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuServerRemove.Image")));
      this.mnuServerRemove.Name = "mnuServerRemove";
      this.mnuServerRemove.Size = new System.Drawing.Size(152, 22);
      this.mnuServerRemove.Text = "Remove Server";
      this.mnuServerRemove.Click += new System.EventHandler(this.mnuServerRemove_Click);
      // 
      // mnuSym
      // 
      this.mnuSym.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSymAddProject,
            this.mnuSymNewFile,
            this.toolStripMenuItem3,
            this.mnuSymOpenFile,
            this.mnuSymRunFile,
            this.toolStripMenuItem2,
            this.mnuSymConnect,
            this.toolStripMenuItem10,
            this.mnuSymSettings,
            this.mnuSymRemove});
      this.mnuSym.Name = "mnuSym";
      this.mnuSym.Size = new System.Drawing.Size(144, 176);
      // 
      // mnuSymAddProject
      // 
      this.mnuSymAddProject.Enabled = false;
      this.mnuSymAddProject.Image = ((System.Drawing.Image)(resources.GetObject("mnuSymAddProject.Image")));
      this.mnuSymAddProject.Name = "mnuSymAddProject";
      this.mnuSymAddProject.Size = new System.Drawing.Size(143, 22);
      this.mnuSymAddProject.Text = "&New Project";
      this.mnuSymAddProject.Click += new System.EventHandler(this.mnuSymAddProject_Click);
      // 
      // mnuSymNewFile
      // 
      this.mnuSymNewFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuSymNewFile.Image")));
      this.mnuSymNewFile.Name = "mnuSymNewFile";
      this.mnuSymNewFile.Size = new System.Drawing.Size(143, 22);
      this.mnuSymNewFile.Text = "&New File";
      this.mnuSymNewFile.Click += new System.EventHandler(this.mnuSymNewFile_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(140, 6);
      // 
      // mnuSymOpenFile
      // 
      this.mnuSymOpenFile.Enabled = false;
      this.mnuSymOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuSymOpenFile.Image")));
      this.mnuSymOpenFile.Name = "mnuSymOpenFile";
      this.mnuSymOpenFile.Size = new System.Drawing.Size(143, 22);
      this.mnuSymOpenFile.Text = "&Open File";
      this.mnuSymOpenFile.Click += new System.EventHandler(this.mnuSymOpenFile_Click);
      // 
      // mnuSymRunFile
      // 
      this.mnuSymRunFile.Enabled = false;
      this.mnuSymRunFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuSymRunFile.Image")));
      this.mnuSymRunFile.Name = "mnuSymRunFile";
      this.mnuSymRunFile.Size = new System.Drawing.Size(143, 22);
      this.mnuSymRunFile.Text = "&Run File";
      this.mnuSymRunFile.Click += new System.EventHandler(this.mnuSymRunFile_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(140, 6);
      // 
      // mnuSymConnect
      // 
      this.mnuSymConnect.Image = ((System.Drawing.Image)(resources.GetObject("mnuSymConnect.Image")));
      this.mnuSymConnect.Name = "mnuSymConnect";
      this.mnuSymConnect.Size = new System.Drawing.Size(143, 22);
      this.mnuSymConnect.Text = "&Connect";
      this.mnuSymConnect.Click += new System.EventHandler(this.mnuSymConnect_Click);
      // 
      // toolStripMenuItem10
      // 
      this.toolStripMenuItem10.Name = "toolStripMenuItem10";
      this.toolStripMenuItem10.Size = new System.Drawing.Size(140, 6);
      // 
      // mnuSymSettings
      // 
      this.mnuSymSettings.Image = ((System.Drawing.Image)(resources.GetObject("mnuSymSettings.Image")));
      this.mnuSymSettings.Name = "mnuSymSettings";
      this.mnuSymSettings.Size = new System.Drawing.Size(143, 22);
      this.mnuSymSettings.Text = "Sym Settings";
      this.mnuSymSettings.Click += new System.EventHandler(this.mnuSymSettings_Click);
      // 
      // mnuSymRemove
      // 
      this.mnuSymRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuSymRemove.Image")));
      this.mnuSymRemove.Name = "mnuSymRemove";
      this.mnuSymRemove.Size = new System.Drawing.Size(143, 22);
      this.mnuSymRemove.Text = "Remove Sym";
      this.mnuSymRemove.Click += new System.EventHandler(this.mnuSymRemove_Click);
      // 
      // mnuProject
      // 
      this.mnuProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProjectAddExisting,
            this.mnuProjectAddNew,
            this.toolStripMenuItem4,
            this.mnuProjectRename,
            this.toolStripMenuItem5,
            this.mnuProjectRemove,
            this.mnuProjectDelete});
      this.mnuProject.Name = "mnuProject";
      this.mnuProject.Size = new System.Drawing.Size(161, 126);
      // 
      // mnuProjectAddExisting
      // 
      this.mnuProjectAddExisting.Image = ((System.Drawing.Image)(resources.GetObject("mnuProjectAddExisting.Image")));
      this.mnuProjectAddExisting.Name = "mnuProjectAddExisting";
      this.mnuProjectAddExisting.Size = new System.Drawing.Size(160, 22);
      this.mnuProjectAddExisting.Text = "&Add Existing File";
      this.mnuProjectAddExisting.Click += new System.EventHandler(this.mnuProjectAddExisting_Click);
      // 
      // mnuProjectAddNew
      // 
      this.mnuProjectAddNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuProjectAddNew.Image")));
      this.mnuProjectAddNew.Name = "mnuProjectAddNew";
      this.mnuProjectAddNew.Size = new System.Drawing.Size(160, 22);
      this.mnuProjectAddNew.Text = "Add &New File";
      this.mnuProjectAddNew.Click += new System.EventHandler(this.mnuProjectAddNew_Click);
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(157, 6);
      // 
      // mnuProjectRename
      // 
      this.mnuProjectRename.Image = ((System.Drawing.Image)(resources.GetObject("mnuProjectRename.Image")));
      this.mnuProjectRename.Name = "mnuProjectRename";
      this.mnuProjectRename.Size = new System.Drawing.Size(160, 22);
      this.mnuProjectRename.Text = "&Rename";
      this.mnuProjectRename.Click += new System.EventHandler(this.mnuProjectRename_Click);
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new System.Drawing.Size(157, 6);
      // 
      // mnuProjectRemove
      // 
      this.mnuProjectRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuProjectRemove.Image")));
      this.mnuProjectRemove.Name = "mnuProjectRemove";
      this.mnuProjectRemove.Size = new System.Drawing.Size(160, 22);
      this.mnuProjectRemove.Text = "Remove";
      this.mnuProjectRemove.Click += new System.EventHandler(this.mnuProjectRemove_Click);
      // 
      // mnuProjectDelete
      // 
      this.mnuProjectDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuProjectDelete.Image")));
      this.mnuProjectDelete.Name = "mnuProjectDelete";
      this.mnuProjectDelete.Size = new System.Drawing.Size(160, 22);
      this.mnuProjectDelete.Text = "Delete All";
      this.mnuProjectDelete.Click += new System.EventHandler(this.mnuProjectDelete_Click);
      // 
      // mnuFile
      // 
      this.mnuFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileInstall,
            this.mnuFileRun,
            this.toolStripMenuItem6,
            this.mnuFileRename,
            this.toolStripMenuItem7,
            this.mnuFileRemove,
            this.mnuFileDelete});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(118, 126);
      // 
      // mnuFileInstall
      // 
      this.mnuFileInstall.Enabled = false;
      this.mnuFileInstall.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileInstall.Image")));
      this.mnuFileInstall.Name = "mnuFileInstall";
      this.mnuFileInstall.Size = new System.Drawing.Size(117, 22);
      this.mnuFileInstall.Text = "&Install";
      this.mnuFileInstall.Click += new System.EventHandler(this.mnuFileInstall_Click);
      // 
      // mnuFileRun
      // 
      this.mnuFileRun.Enabled = false;
      this.mnuFileRun.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileRun.Image")));
      this.mnuFileRun.Name = "mnuFileRun";
      this.mnuFileRun.Size = new System.Drawing.Size(117, 22);
      this.mnuFileRun.Text = "&Run";
      this.mnuFileRun.Click += new System.EventHandler(this.mnuFileRun_Click);
      // 
      // toolStripMenuItem6
      // 
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      this.toolStripMenuItem6.Size = new System.Drawing.Size(114, 6);
      // 
      // mnuFileRename
      // 
      this.mnuFileRename.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileRename.Image")));
      this.mnuFileRename.Name = "mnuFileRename";
      this.mnuFileRename.Size = new System.Drawing.Size(117, 22);
      this.mnuFileRename.Text = "Rename";
      this.mnuFileRename.Click += new System.EventHandler(this.mnuFileRename_Click);
      // 
      // toolStripMenuItem7
      // 
      this.toolStripMenuItem7.Name = "toolStripMenuItem7";
      this.toolStripMenuItem7.Size = new System.Drawing.Size(114, 6);
      // 
      // mnuFileRemove
      // 
      this.mnuFileRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileRemove.Image")));
      this.mnuFileRemove.Name = "mnuFileRemove";
      this.mnuFileRemove.Size = new System.Drawing.Size(117, 22);
      this.mnuFileRemove.Text = "Remove";
      this.mnuFileRemove.Click += new System.EventHandler(this.mnuFileRemove_Click);
      // 
      // mnuFileDelete
      // 
      this.mnuFileDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileDelete.Image")));
      this.mnuFileDelete.Name = "mnuFileDelete";
      this.mnuFileDelete.Size = new System.Drawing.Size(117, 22);
      this.mnuFileDelete.Text = "Delete";
      this.mnuFileDelete.Click += new System.EventHandler(this.mnuFileDelete_Click);
      // 
      // mnuLocal
      // 
      this.mnuLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLocalNewProject,
            this.mnuLocalNewFile,
            this.toolStripMenuItem8,
            this.mnuLocalOpenFile,
            this.toolStripMenuItem9,
            this.mnuLocalSettings,
            this.mnuLocalRemove,
            this.mnuLocalDelete});
      this.mnuLocal.Name = "mnuLocal";
      this.mnuLocal.Size = new System.Drawing.Size(149, 148);
      // 
      // mnuLocalNewProject
      // 
      this.mnuLocalNewProject.Image = ((System.Drawing.Image)(resources.GetObject("mnuLocalNewProject.Image")));
      this.mnuLocalNewProject.Name = "mnuLocalNewProject";
      this.mnuLocalNewProject.Size = new System.Drawing.Size(148, 22);
      this.mnuLocalNewProject.Text = "&New Project";
      this.mnuLocalNewProject.Click += new System.EventHandler(this.mnuLocalNewProject_Click);
      // 
      // mnuLocalNewFile
      // 
      this.mnuLocalNewFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuLocalNewFile.Image")));
      this.mnuLocalNewFile.Name = "mnuLocalNewFile";
      this.mnuLocalNewFile.Size = new System.Drawing.Size(148, 22);
      this.mnuLocalNewFile.Text = "&New File";
      this.mnuLocalNewFile.Click += new System.EventHandler(this.mnuLocalNewFile_Click);
      // 
      // toolStripMenuItem8
      // 
      this.toolStripMenuItem8.Name = "toolStripMenuItem8";
      this.toolStripMenuItem8.Size = new System.Drawing.Size(145, 6);
      // 
      // mnuLocalOpenFile
      // 
      this.mnuLocalOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuLocalOpenFile.Image")));
      this.mnuLocalOpenFile.Name = "mnuLocalOpenFile";
      this.mnuLocalOpenFile.Size = new System.Drawing.Size(148, 22);
      this.mnuLocalOpenFile.Text = "&Open File";
      this.mnuLocalOpenFile.Click += new System.EventHandler(this.mnuLocalOpenFile_Click);
      // 
      // toolStripMenuItem9
      // 
      this.toolStripMenuItem9.Name = "toolStripMenuItem9";
      this.toolStripMenuItem9.Size = new System.Drawing.Size(145, 6);
      // 
      // mnuLocalSettings
      // 
      this.mnuLocalSettings.Image = ((System.Drawing.Image)(resources.GetObject("mnuLocalSettings.Image")));
      this.mnuLocalSettings.Name = "mnuLocalSettings";
      this.mnuLocalSettings.Size = new System.Drawing.Size(148, 22);
      this.mnuLocalSettings.Text = "Local &Settings";
      this.mnuLocalSettings.Click += new System.EventHandler(this.mnuLocalSettings_Click);
      // 
      // mnuLocalRemove
      // 
      this.mnuLocalRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuLocalRemove.Image")));
      this.mnuLocalRemove.Name = "mnuLocalRemove";
      this.mnuLocalRemove.Size = new System.Drawing.Size(148, 22);
      this.mnuLocalRemove.Text = "Remove Local";
      this.mnuLocalRemove.Click += new System.EventHandler(this.mnuLocalRemove_Click);
      // 
      // mnuLocalDelete
      // 
      this.mnuLocalDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuLocalDelete.Image")));
      this.mnuLocalDelete.Name = "mnuLocalDelete";
      this.mnuLocalDelete.Size = new System.Drawing.Size(148, 22);
      this.mnuLocalDelete.Text = "Delete All";
      this.mnuLocalDelete.Click += new System.EventHandler(this.mnuLocalDelete_Click);
      // 
      // frmProject
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(243, 316);
      this.Controls.Add(this.treeProj);
      this.Controls.Add(this.stripTool);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmProject";
      this.Text = "Project Explorer";
      this.Load += new System.EventHandler(this.frmProject_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProject_FormClosing);
      this.stripTool.ResumeLayout(false);
      this.stripTool.PerformLayout();
      this.mnuServer.ResumeLayout(false);
      this.mnuSym.ResumeLayout(false);
      this.mnuProject.ResumeLayout(false);
      this.mnuFile.ResumeLayout(false);
      this.mnuLocal.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip stripTool;
    private System.Windows.Forms.ToolStripButton toolAddServer;
    private System.Windows.Forms.TreeView treeProj;
    private System.Windows.Forms.ToolStripButton toolAddSym;
    private System.Windows.Forms.ToolStripButton toolAddProject;
    private System.Windows.Forms.ToolStripButton toolAddFile;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toolAddLocal;
    private System.Windows.Forms.ImageList icons;
    private System.Windows.Forms.ContextMenuStrip mnuServer;
    private System.Windows.Forms.ToolStripMenuItem mnuServerAddSym;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem mnuServerRemove;
    private System.Windows.Forms.ContextMenuStrip mnuSym;
    private System.Windows.Forms.ToolStripMenuItem mnuSymAddProject;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem mnuSymOpenFile;
    private System.Windows.Forms.ToolStripMenuItem mnuSymRunFile;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem mnuSymRemove;
    private System.Windows.Forms.ToolStripMenuItem mnuServerSettings;
    private System.Windows.Forms.ContextMenuStrip mnuProject;
    private System.Windows.Forms.ToolStripMenuItem mnuProjectAddExisting;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem mnuProjectRename;
    private System.Windows.Forms.ToolStripMenuItem mnuSymSettings;
    private System.Windows.Forms.ToolStripMenuItem mnuProjectRemove;
    private System.Windows.Forms.ToolStripMenuItem mnuProjectDelete;
    private System.Windows.Forms.ToolStripMenuItem mnuProjectAddNew;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    private System.Windows.Forms.ContextMenuStrip mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileRename;
    private System.Windows.Forms.ToolStripMenuItem mnuFileRemove;
    private System.Windows.Forms.ToolStripMenuItem mnuFileInstall;
    private System.Windows.Forms.ToolStripMenuItem mnuFileRun;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
    private System.Windows.Forms.ToolStripMenuItem mnuFileDelete;
    private System.Windows.Forms.ContextMenuStrip mnuLocal;
    private System.Windows.Forms.ToolStripMenuItem mnuLocalNewProject;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
    private System.Windows.Forms.ToolStripMenuItem mnuLocalOpenFile;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
    private System.Windows.Forms.ToolStripMenuItem mnuLocalSettings;
    private System.Windows.Forms.ToolStripMenuItem mnuLocalRemove;
    private System.Windows.Forms.ToolStripMenuItem mnuLocalDelete;
    private System.Windows.Forms.ToolStripMenuItem mnuSymConnect;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
    private System.Windows.Forms.ToolStripMenuItem mnuSymNewFile;
    private System.Windows.Forms.ToolStripMenuItem mnuLocalNewFile;
  }
}