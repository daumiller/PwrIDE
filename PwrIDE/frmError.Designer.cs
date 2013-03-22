namespace PwrIDE
{
  partial class frmError
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmError));
      this.lstErrors = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // lstErrors
      // 
      this.lstErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstErrors.FullRowSelect = true;
      this.lstErrors.GridLines = true;
      this.lstErrors.Location = new System.Drawing.Point(0, 0);
      this.lstErrors.Name = "lstErrors";
      this.lstErrors.ShowGroups = false;
      this.lstErrors.Size = new System.Drawing.Size(526, 57);
      this.lstErrors.TabIndex = 0;
      this.lstErrors.UseCompatibleStateImageBehavior = false;
      this.lstErrors.View = System.Windows.Forms.View.Details;
      this.lstErrors.DoubleClick += new System.EventHandler(this.lstErrors_DoubleClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "File";
      this.columnHeader1.Width = 142;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Line";
      this.columnHeader2.Width = 34;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Description";
      this.columnHeader3.Width = 346;
      // 
      // frmError
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(526, 57);
      this.Controls.Add(this.lstErrors);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmError";
      this.Text = "Errors";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmError_FormClosing);
      this.Resize += new System.EventHandler(this.frmError_Resize);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lstErrors;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
  }
}