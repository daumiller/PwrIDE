namespace PwrIDE
{
  partial class frmFind
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFind));
      this.lblFind = new System.Windows.Forms.Label();
      this.lblSep = new System.Windows.Forms.Label();
      this.lblReplace = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lblReplaceLeft = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkRegex = new System.Windows.Forms.CheckBox();
      this.chkCase = new System.Windows.Forms.CheckBox();
      this.btnFind = new System.Windows.Forms.Button();
      this.btnReplace = new System.Windows.Forms.Button();
      this.btnReplaceAll = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cmbFind = new System.Windows.Forms.ComboBox();
      this.cmbReplace = new System.Windows.Forms.ComboBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblFind
      // 
      this.lblFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblFind.Image = ((System.Drawing.Image)(resources.GetObject("lblFind.Image")));
      this.lblFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblFind.Location = new System.Drawing.Point(12, 9);
      this.lblFind.Name = "lblFind";
      this.lblFind.Size = new System.Drawing.Size(72, 23);
      this.lblFind.TabIndex = 0;
      this.lblFind.Text = "Find";
      this.lblFind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblFind.Click += new System.EventHandler(this.lblFind_Click);
      // 
      // lblSep
      // 
      this.lblSep.BackColor = System.Drawing.Color.Silver;
      this.lblSep.Location = new System.Drawing.Point(9, 35);
      this.lblSep.Name = "lblSep";
      this.lblSep.Size = new System.Drawing.Size(302, 1);
      this.lblSep.TabIndex = 2;
      // 
      // lblReplace
      // 
      this.lblReplace.Image = ((System.Drawing.Image)(resources.GetObject("lblReplace.Image")));
      this.lblReplace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblReplace.Location = new System.Drawing.Point(90, 9);
      this.lblReplace.Name = "lblReplace";
      this.lblReplace.Size = new System.Drawing.Size(72, 23);
      this.lblReplace.TabIndex = 3;
      this.lblReplace.Text = "Replace";
      this.lblReplace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lblReplace.Click += new System.EventHandler(this.lblReplace_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(29, 52);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(30, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Find:";
      // 
      // lblReplaceLeft
      // 
      this.lblReplaceLeft.AutoSize = true;
      this.lblReplaceLeft.Enabled = false;
      this.lblReplaceLeft.Location = new System.Drawing.Point(9, 84);
      this.lblReplaceLeft.Name = "lblReplaceLeft";
      this.lblReplaceLeft.Size = new System.Drawing.Size(50, 13);
      this.lblReplaceLeft.TabIndex = 7;
      this.lblReplaceLeft.Text = "Replace:";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chkRegex);
      this.groupBox1.Controls.Add(this.chkCase);
      this.groupBox1.Location = new System.Drawing.Point(14, 108);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(297, 48);
      this.groupBox1.TabIndex = 8;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Options";
      // 
      // chkRegex
      // 
      this.chkRegex.AutoSize = true;
      this.chkRegex.Location = new System.Drawing.Point(151, 19);
      this.chkRegex.Name = "chkRegex";
      this.chkRegex.Size = new System.Drawing.Size(117, 17);
      this.chkRegex.TabIndex = 7;
      this.chkRegex.Text = "Regular Expression";
      this.chkRegex.UseVisualStyleBackColor = true;
      // 
      // chkCase
      // 
      this.chkCase.AutoSize = true;
      this.chkCase.Location = new System.Drawing.Point(29, 19);
      this.chkCase.Name = "chkCase";
      this.chkCase.Size = new System.Drawing.Size(96, 17);
      this.chkCase.TabIndex = 6;
      this.chkCase.Text = "Case Sensitive";
      this.chkCase.UseVisualStyleBackColor = true;
      // 
      // btnFind
      // 
      this.btnFind.Location = new System.Drawing.Point(332, 13);
      this.btnFind.Name = "btnFind";
      this.btnFind.Size = new System.Drawing.Size(89, 23);
      this.btnFind.TabIndex = 3;
      this.btnFind.Text = "&Find Next";
      this.btnFind.UseVisualStyleBackColor = true;
      this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
      // 
      // btnReplace
      // 
      this.btnReplace.Enabled = false;
      this.btnReplace.Location = new System.Drawing.Point(332, 52);
      this.btnReplace.Name = "btnReplace";
      this.btnReplace.Size = new System.Drawing.Size(89, 23);
      this.btnReplace.TabIndex = 4;
      this.btnReplace.Text = "&Replace";
      this.btnReplace.UseVisualStyleBackColor = true;
      this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
      // 
      // btnReplaceAll
      // 
      this.btnReplaceAll.Location = new System.Drawing.Point(332, 81);
      this.btnReplaceAll.Name = "btnReplaceAll";
      this.btnReplaceAll.Size = new System.Drawing.Size(89, 23);
      this.btnReplaceAll.TabIndex = 5;
      this.btnReplaceAll.Text = "Replace &All";
      this.btnReplaceAll.UseVisualStyleBackColor = true;
      this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(332, 133);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(89, 23);
      this.btnCancel.TabIndex = 11;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // cmbFind
      // 
      this.cmbFind.FormattingEnabled = true;
      this.cmbFind.Location = new System.Drawing.Point(65, 49);
      this.cmbFind.Name = "cmbFind";
      this.cmbFind.Size = new System.Drawing.Size(246, 21);
      this.cmbFind.TabIndex = 1;
      this.cmbFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFind_KeyDown);
      // 
      // cmbReplace
      // 
      this.cmbReplace.Enabled = false;
      this.cmbReplace.FormattingEnabled = true;
      this.cmbReplace.Location = new System.Drawing.Point(65, 81);
      this.cmbReplace.Name = "cmbReplace";
      this.cmbReplace.Size = new System.Drawing.Size(246, 21);
      this.cmbReplace.TabIndex = 2;
      this.cmbReplace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbReplace_KeyDown);
      // 
      // frmFind
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(432, 167);
      this.Controls.Add(this.cmbReplace);
      this.Controls.Add(this.cmbFind);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnReplaceAll);
      this.Controls.Add(this.btnReplace);
      this.Controls.Add(this.btnFind);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.lblReplaceLeft);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblReplace);
      this.Controls.Add(this.lblSep);
      this.Controls.Add(this.lblFind);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmFind";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Search";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFind_KeyDown);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblFind;
    private System.Windows.Forms.Label lblSep;
    private System.Windows.Forms.Label lblReplace;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblReplaceLeft;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkRegex;
    private System.Windows.Forms.CheckBox chkCase;
    private System.Windows.Forms.Button btnFind;
    private System.Windows.Forms.Button btnReplace;
    private System.Windows.Forms.Button btnReplaceAll;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ComboBox cmbFind;
    private System.Windows.Forms.ComboBox cmbReplace;

  }
}