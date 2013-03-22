namespace PwrIDE
{
  partial class frmSource
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSource));
      this.icons = new System.Windows.Forms.ImageList(this.components);
      this.icsContext = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.icsContextCut = new System.Windows.Forms.ToolStripMenuItem();
      this.icsContextCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.icsContextPaste = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.icsContextUndo = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.icsContextRedo = new System.Windows.Forms.ToolStripMenuItem();
      this.icsContextSelectAll = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.icsContextBookmark = new System.Windows.Forms.ToolStripMenuItem();
      this.icsContext.SuspendLayout();
      this.SuspendLayout();
      // 
      // icons
      // 
      this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
      this.icons.TransparentColor = System.Drawing.Color.Transparent;
      this.icons.Images.SetKeyName(0, "PWR");
      this.icons.Images.SetKeyName(1, "REP");
      this.icons.Images.SetKeyName(2, "LTR");
      // 
      // icsContext
      // 
      this.icsContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.icsContextCut,
            this.icsContextCopy,
            this.icsContextPaste,
            this.toolStripMenuItem1,
            this.icsContextUndo,
            this.icsContextRedo,
            this.toolStripMenuItem2,
            this.icsContextSelectAll,
            this.toolStripMenuItem3,
            this.icsContextBookmark});
      this.icsContext.Name = "icsContext";
      this.icsContext.Size = new System.Drawing.Size(169, 198);
      // 
      // icsContextCut
      // 
      this.icsContextCut.Image = ((System.Drawing.Image)(resources.GetObject("icsContextCut.Image")));
      this.icsContextCut.Name = "icsContextCut";
      this.icsContextCut.Size = new System.Drawing.Size(168, 22);
      this.icsContextCut.Text = "Cu&t";
      this.icsContextCut.Click += new System.EventHandler(this.icsContextCut_Click);
      // 
      // icsContextCopy
      // 
      this.icsContextCopy.Image = ((System.Drawing.Image)(resources.GetObject("icsContextCopy.Image")));
      this.icsContextCopy.Name = "icsContextCopy";
      this.icsContextCopy.Size = new System.Drawing.Size(168, 22);
      this.icsContextCopy.Text = "&Copy";
      this.icsContextCopy.Click += new System.EventHandler(this.icsContextCopy_Click);
      // 
      // icsContextPaste
      // 
      this.icsContextPaste.Image = ((System.Drawing.Image)(resources.GetObject("icsContextPaste.Image")));
      this.icsContextPaste.Name = "icsContextPaste";
      this.icsContextPaste.Size = new System.Drawing.Size(168, 22);
      this.icsContextPaste.Text = "&Paste";
      this.icsContextPaste.Click += new System.EventHandler(this.icsContextPaste_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 6);
      // 
      // icsContextUndo
      // 
      this.icsContextUndo.Image = ((System.Drawing.Image)(resources.GetObject("icsContextUndo.Image")));
      this.icsContextUndo.Name = "icsContextUndo";
      this.icsContextUndo.Size = new System.Drawing.Size(168, 22);
      this.icsContextUndo.Text = "&Undo";
      this.icsContextUndo.Click += new System.EventHandler(this.icsContextUndo_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 6);
      // 
      // icsContextRedo
      // 
      this.icsContextRedo.Image = ((System.Drawing.Image)(resources.GetObject("icsContextRedo.Image")));
      this.icsContextRedo.Name = "icsContextRedo";
      this.icsContextRedo.Size = new System.Drawing.Size(168, 22);
      this.icsContextRedo.Text = "&Redo";
      this.icsContextRedo.Click += new System.EventHandler(this.icsContextRedo_Click);
      // 
      // icsContextSelectAll
      // 
      this.icsContextSelectAll.Name = "icsContextSelectAll";
      this.icsContextSelectAll.Size = new System.Drawing.Size(168, 22);
      this.icsContextSelectAll.Text = "&Select All";
      this.icsContextSelectAll.Click += new System.EventHandler(this.icsContextSelectAll_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(165, 6);
      // 
      // icsContextBookmark
      // 
      this.icsContextBookmark.Image = ((System.Drawing.Image)(resources.GetObject("icsContextBookmark.Image")));
      this.icsContextBookmark.Name = "icsContextBookmark";
      this.icsContextBookmark.Size = new System.Drawing.Size(168, 22);
      this.icsContextBookmark.Text = "Toggle &Bookmark";
      this.icsContextBookmark.Click += new System.EventHandler(this.icsContextBookmark_Click);
      // 
      // frmSource
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 262);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmSource";
      this.Load += new System.EventHandler(this.frmSource_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSource_FormClosing);
      this.icsContext.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ImageList icons;
    private System.Windows.Forms.ContextMenuStrip icsContext;
    private System.Windows.Forms.ToolStripMenuItem icsContextCut;
    private System.Windows.Forms.ToolStripMenuItem icsContextCopy;
    private System.Windows.Forms.ToolStripMenuItem icsContextPaste;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem icsContextUndo;
    private System.Windows.Forms.ToolStripMenuItem icsContextRedo;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem icsContextSelectAll;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem icsContextBookmark;
  }
}