namespace PwrIDE
{
  partial class frmFileOpen
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileOpen));
      this.treeSym = new System.Windows.Forms.TreeView();
      this.icons = new System.Windows.Forms.ImageList(this.components);
      this.txtFilename = new System.Windows.Forms.TextBox();
      this.comboType = new System.Windows.Forms.ComboBox();
      this.btnLocal = new System.Windows.Forms.Button();
      this.btnOpen = new System.Windows.Forms.Button();
      this.lstResults = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // treeSym
      // 
      this.treeSym.ImageIndex = 0;
      this.treeSym.ImageList = this.icons;
      this.treeSym.Location = new System.Drawing.Point(12, 12);
      this.treeSym.Name = "treeSym";
      this.treeSym.SelectedImageIndex = 0;
      this.treeSym.Size = new System.Drawing.Size(150, 329);
      this.treeSym.TabIndex = 0;
      this.treeSym.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSym_AfterSelect);
      // 
      // icons
      // 
      this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
      this.icons.TransparentColor = System.Drawing.Color.Transparent;
      this.icons.Images.SetKeyName(0, "SERVER");
      this.icons.Images.SetKeyName(1, "SYM");
      this.icons.Images.SetKeyName(2, "PWR");
      this.icons.Images.SetKeyName(3, "REP");
      this.icons.Images.SetKeyName(4, "LTR");
      this.icons.Images.SetKeyName(5, "LOCAL");
      // 
      // txtFilename
      // 
      this.txtFilename.Enabled = false;
      this.txtFilename.Location = new System.Drawing.Point(179, 12);
      this.txtFilename.Name = "txtFilename";
      this.txtFilename.Size = new System.Drawing.Size(288, 20);
      this.txtFilename.TabIndex = 1;
      this.txtFilename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilename_KeyPress);
      // 
      // comboType
      // 
      this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboType.FormattingEnabled = true;
      this.comboType.Items.AddRange(new object[] {
            "REPGEN",
            "LETTER"});
      this.comboType.Location = new System.Drawing.Point(473, 11);
      this.comboType.Name = "comboType";
      this.comboType.Size = new System.Drawing.Size(88, 21);
      this.comboType.TabIndex = 2;
      // 
      // btnLocal
      // 
      this.btnLocal.Image = ((System.Drawing.Image)(resources.GetObject("btnLocal.Image")));
      this.btnLocal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnLocal.Location = new System.Drawing.Point(32, 347);
      this.btnLocal.Name = "btnLocal";
      this.btnLocal.Size = new System.Drawing.Size(115, 23);
      this.btnLocal.TabIndex = 4;
      this.btnLocal.Text = "Open &Local File";
      this.btnLocal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnLocal.UseVisualStyleBackColor = true;
      this.btnLocal.Click += new System.EventHandler(this.btnLocal_Click);
      // 
      // btnOpen
      // 
      this.btnOpen.Enabled = false;
      this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
      this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnOpen.Location = new System.Drawing.Point(473, 347);
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(88, 23);
      this.btnOpen.TabIndex = 5;
      this.btnOpen.Text = "&Open";
      this.btnOpen.UseVisualStyleBackColor = true;
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // lstResults
      // 
      this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.lstResults.FullRowSelect = true;
      this.lstResults.Location = new System.Drawing.Point(179, 38);
      this.lstResults.Name = "lstResults";
      this.lstResults.ShowGroups = false;
      this.lstResults.Size = new System.Drawing.Size(382, 303);
      this.lstResults.SmallImageList = this.icons;
      this.lstResults.TabIndex = 6;
      this.lstResults.UseCompatibleStateImageBehavior = false;
      this.lstResults.View = System.Windows.Forms.View.Details;
      this.lstResults.SelectedIndexChanged += new System.EventHandler(this.lstResults_SelectedIndexChanged);
      this.lstResults.DoubleClick += new System.EventHandler(this.lstResults_DoubleClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Name";
      this.columnHeader1.Width = 227;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Modified";
      this.columnHeader2.Width = 76;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Size";
      this.columnHeader3.Width = 68;
      // 
      // frmFileOpen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(573, 377);
      this.Controls.Add(this.lstResults);
      this.Controls.Add(this.btnOpen);
      this.Controls.Add(this.btnLocal);
      this.Controls.Add(this.comboType);
      this.Controls.Add(this.txtFilename);
      this.Controls.Add(this.treeSym);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmFileOpen";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Open File";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFileOpen_KeyDown);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmFileOpen_KeyPress);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TreeView treeSym;
    private System.Windows.Forms.TextBox txtFilename;
    private System.Windows.Forms.ComboBox comboType;
    private System.Windows.Forms.Button btnLocal;
    private System.Windows.Forms.Button btnOpen;
    private System.Windows.Forms.ListView lstResults;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ImageList icons;
  }
}
