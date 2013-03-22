namespace PwrIDE
{
  partial class frmImportProjects
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportProjects));
      this.treeSyms = new System.Windows.Forms.TreeView();
      this.lstProjects = new System.Windows.Forms.ListView();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOkay = new System.Windows.Forms.Button();
      this.colProject = new System.Windows.Forms.ColumnHeader();
      this.coldFiles = new System.Windows.Forms.ColumnHeader();
      this.icons = new System.Windows.Forms.ImageList(this.components);
      this.btnlList = new System.Windows.Forms.Button();
      this.mnuList = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.invertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuList.SuspendLayout();
      this.SuspendLayout();
      // 
      // treeSyms
      // 
      this.treeSyms.HideSelection = false;
      this.treeSyms.ImageIndex = 0;
      this.treeSyms.ImageList = this.icons;
      this.treeSyms.Location = new System.Drawing.Point(12, 12);
      this.treeSyms.Name = "treeSyms";
      this.treeSyms.SelectedImageIndex = 0;
      this.treeSyms.ShowPlusMinus = false;
      this.treeSyms.ShowRootLines = false;
      this.treeSyms.Size = new System.Drawing.Size(158, 330);
      this.treeSyms.TabIndex = 0;
      this.treeSyms.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSyms_AfterSelect);
      // 
      // lstProjects
      // 
      this.lstProjects.CheckBoxes = true;
      this.lstProjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colProject,
            this.coldFiles});
      this.lstProjects.FullRowSelect = true;
      this.lstProjects.GridLines = true;
      this.lstProjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lstProjects.HideSelection = false;
      this.lstProjects.Location = new System.Drawing.Point(176, 12);
      this.lstProjects.Name = "lstProjects";
      this.lstProjects.ShowGroups = false;
      this.lstProjects.Size = new System.Drawing.Size(357, 330);
      this.lstProjects.SmallImageList = this.icons;
      this.lstProjects.TabIndex = 1;
      this.lstProjects.UseCompatibleStateImageBehavior = false;
      this.lstProjects.View = System.Windows.Forms.View.Details;
      this.lstProjects.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstProjects_ItemChecked);
      this.lstProjects.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstProjects_MouseClick);
      // 
      // btnCancel
      // 
      this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
      this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnCancel.Location = new System.Drawing.Point(468, 362);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 23);
      this.btnCancel.TabIndex = 9;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOkay
      // 
      this.btnOkay.Enabled = false;
      this.btnOkay.Image = ((System.Drawing.Image)(resources.GetObject("btnOkay.Image")));
      this.btnOkay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnOkay.Location = new System.Drawing.Point(399, 362);
      this.btnOkay.Name = "btnOkay";
      this.btnOkay.Size = new System.Drawing.Size(63, 23);
      this.btnOkay.TabIndex = 8;
      this.btnOkay.Text = "&Import";
      this.btnOkay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnOkay.UseVisualStyleBackColor = true;
      this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
      // 
      // colProject
      // 
      this.colProject.Text = "Project";
      this.colProject.Width = 293;
      // 
      // coldFiles
      // 
      this.coldFiles.Text = "Files";
      // 
      // icons
      // 
      this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
      this.icons.TransparentColor = System.Drawing.Color.Transparent;
      this.icons.Images.SetKeyName(0, "SERVER");
      this.icons.Images.SetKeyName(1, "SYM");
      this.icons.Images.SetKeyName(2, "PROJECT");
      // 
      // btnlList
      // 
      this.btnlList.Enabled = false;
      this.btnlList.Location = new System.Drawing.Point(12, 348);
      this.btnlList.Name = "btnlList";
      this.btnlList.Size = new System.Drawing.Size(158, 23);
      this.btnlList.TabIndex = 10;
      this.btnlList.Text = "&List Projects";
      this.btnlList.UseVisualStyleBackColor = true;
      this.btnlList.Click += new System.EventHandler(this.btnlList_Click);
      // 
      // mnuList
      // 
      this.mnuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.deselectAllToolStripMenuItem,
            this.toolStripMenuItem1,
            this.invertSelectionToolStripMenuItem});
      this.mnuList.Name = "mnuList";
      this.mnuList.Size = new System.Drawing.Size(156, 76);
      // 
      // selectAllToolStripMenuItem
      // 
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.selectAllToolStripMenuItem.Text = "Select &All";
      this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
      // 
      // deselectAllToolStripMenuItem
      // 
      this.deselectAllToolStripMenuItem.Name = "deselectAllToolStripMenuItem";
      this.deselectAllToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.deselectAllToolStripMenuItem.Text = "&Deselect All";
      this.deselectAllToolStripMenuItem.Click += new System.EventHandler(this.deselectAllToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
      // 
      // invertSelectionToolStripMenuItem
      // 
      this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
      this.invertSelectionToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.invertSelectionToolStripMenuItem.Text = "&Invert Selection";
      this.invertSelectionToolStripMenuItem.Click += new System.EventHandler(this.invertSelectionToolStripMenuItem_Click);
      // 
      // frmImportProjects
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(545, 397);
      this.Controls.Add(this.btnlList);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOkay);
      this.Controls.Add(this.lstProjects);
      this.Controls.Add(this.treeSyms);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmImportProjects";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "frmImportProjects";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmImportProjects_KeyDown);
      this.mnuList.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TreeView treeSyms;
    private System.Windows.Forms.ListView lstProjects;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOkay;
    private System.Windows.Forms.ColumnHeader colProject;
    private System.Windows.Forms.ColumnHeader coldFiles;
    private System.Windows.Forms.ImageList icons;
    private System.Windows.Forms.Button btnlList;
    private System.Windows.Forms.ContextMenuStrip mnuList;
    private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deselectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem invertSelectionToolStripMenuItem;
  }
}