namespace PwrIDE
{
  partial class frmReport
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.lstFiles = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
      this.txtDisplay = new System.Windows.Forms.TextBox();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.lstFiles);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.txtDisplay);
      this.splitContainer1.Size = new System.Drawing.Size(655, 557);
      this.splitContainer1.SplitterDistance = 82;
      this.splitContainer1.TabIndex = 1;
      // 
      // lstFiles
      // 
      this.lstFiles.AutoArrange = false;
      this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstFiles.FullRowSelect = true;
      this.lstFiles.GridLines = true;
      this.lstFiles.HideSelection = false;
      this.lstFiles.Location = new System.Drawing.Point(0, 0);
      this.lstFiles.Name = "lstFiles";
      this.lstFiles.ShowGroups = false;
      this.lstFiles.Size = new System.Drawing.Size(655, 82);
      this.lstFiles.TabIndex = 1;
      this.lstFiles.UseCompatibleStateImageBehavior = false;
      this.lstFiles.View = System.Windows.Forms.View.Details;
      this.lstFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseUp);
      this.lstFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstFiles_ItemSelectionChanged);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Sequence";
      this.columnHeader1.Width = 69;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Title";
      this.columnHeader2.Width = 442;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Pages";
      this.columnHeader3.Width = 74;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Run FM";
      this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader4.Width = 59;
      // 
      // txtDisplay
      // 
      this.txtDisplay.BackColor = System.Drawing.Color.LightGoldenrodYellow;
      this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDisplay.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtDisplay.Location = new System.Drawing.Point(0, 0);
      this.txtDisplay.Multiline = true;
      this.txtDisplay.Name = "txtDisplay";
      this.txtDisplay.ReadOnly = true;
      this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtDisplay.Size = new System.Drawing.Size(655, 471);
      this.txtDisplay.TabIndex = 0;
      this.txtDisplay.WordWrap = false;
      // 
      // frmReport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(655, 557);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmReport";
      this.Text = "Output of ";
      this.Load += new System.EventHandler(this.frmReport_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReport_FormClosing);
      this.Resize += new System.EventHandler(this.frmReport_Resize);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ListView lstFiles;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.TextBox txtDisplay;
    private System.Windows.Forms.ColumnHeader columnHeader4;
  }
}