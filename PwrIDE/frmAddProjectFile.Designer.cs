namespace PwrIDE
{
  partial class frmAddProjectFile
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddProjectFile));
      this.txtFilename = new System.Windows.Forms.TextBox();
      this.comboType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOkay = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtFilename
      // 
      this.txtFilename.Location = new System.Drawing.Point(12, 25);
      this.txtFilename.Name = "txtFilename";
      this.txtFilename.Size = new System.Drawing.Size(264, 20);
      this.txtFilename.TabIndex = 0;
      this.txtFilename.Text = "New File";
      this.txtFilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtFilename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilename_KeyPress);
      // 
      // comboType
      // 
      this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboType.FormattingEnabled = true;
      this.comboType.Items.AddRange(new object[] {
            "RepGen",
            "PowerPlus",
            "Letterfile"});
      this.comboType.Location = new System.Drawing.Point(291, 24);
      this.comboType.Name = "comboType";
      this.comboType.Size = new System.Drawing.Size(94, 21);
      this.comboType.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "File Name:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(313, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(53, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "File Type:";
      // 
      // btnCancel
      // 
      this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
      this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnCancel.Location = new System.Drawing.Point(320, 63);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 23);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOkay
      // 
      this.btnOkay.Image = ((System.Drawing.Image)(resources.GetObject("btnOkay.Image")));
      this.btnOkay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnOkay.Location = new System.Drawing.Point(256, 63);
      this.btnOkay.Name = "btnOkay";
      this.btnOkay.Size = new System.Drawing.Size(58, 23);
      this.btnOkay.TabIndex = 6;
      this.btnOkay.Text = "&Okay";
      this.btnOkay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnOkay.UseVisualStyleBackColor = true;
      this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
      // 
      // frmAddProjectFile
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(394, 94);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOkay);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.comboType);
      this.Controls.Add(this.txtFilename);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAddProjectFile";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "New Project File";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAddProjectFile_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtFilename;
    private System.Windows.Forms.ComboBox comboType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOkay;
  }
}