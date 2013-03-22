namespace PwrIDE
{
  partial class frmAddSym
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddSym));
      this.txtSym = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOkay = new System.Windows.Forms.Button();
      this.btnTest = new System.Windows.Forms.Button();
      this.txtID = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.chkRemember = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // txtSym
      // 
      this.txtSym.Location = new System.Drawing.Point(86, 12);
      this.txtSym.Name = "txtSym";
      this.txtSym.Size = new System.Drawing.Size(165, 20);
      this.txtSym.TabIndex = 1;
      this.txtSym.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtSym.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSym_KeyPress);
      this.txtSym.Enter += new System.EventHandler(this.txtSym_Enter);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(37, 15);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(43, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Sym  #:";
      // 
      // btnCancel
      // 
      this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
      this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnCancel.Location = new System.Drawing.Point(186, 100);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOkay
      // 
      this.btnOkay.Image = ((System.Drawing.Image)(resources.GetObject("btnOkay.Image")));
      this.btnOkay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnOkay.Location = new System.Drawing.Point(122, 100);
      this.btnOkay.Name = "btnOkay";
      this.btnOkay.Size = new System.Drawing.Size(58, 23);
      this.btnOkay.TabIndex = 5;
      this.btnOkay.Text = "&Okay";
      this.btnOkay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnOkay.UseVisualStyleBackColor = true;
      this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
      // 
      // btnTest
      // 
      this.btnTest.Image = ((System.Drawing.Image)(resources.GetObject("btnTest.Image")));
      this.btnTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnTest.Location = new System.Drawing.Point(12, 100);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(56, 23);
      this.btnTest.TabIndex = 4;
      this.btnTest.Text = "&Test";
      this.btnTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
      // 
      // txtID
      // 
      this.txtID.Location = new System.Drawing.Point(86, 38);
      this.txtID.Name = "txtID";
      this.txtID.PasswordChar = '*';
      this.txtID.Size = new System.Drawing.Size(165, 20);
      this.txtID.TabIndex = 2;
      this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtID.UseSystemPasswordChar = true;
      this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
      this.txtID.Enter += new System.EventHandler(this.txtID_Enter);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(37, 41);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(44, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "User ID:";
      // 
      // chkRemember
      // 
      this.chkRemember.AutoSize = true;
      this.chkRemember.Checked = true;
      this.chkRemember.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkRemember.Location = new System.Drawing.Point(171, 64);
      this.chkRemember.Name = "chkRemember";
      this.chkRemember.Size = new System.Drawing.Size(83, 17);
      this.chkRemember.TabIndex = 3;
      this.chkRemember.Text = "Remember ID";
      this.chkRemember.UseVisualStyleBackColor = true;
      // 
      // frmAddSym
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(263, 131);
      this.Controls.Add(this.chkRemember);
      this.Controls.Add(this.txtID);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnTest);
      this.Controls.Add(this.txtSym);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOkay);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAddSym";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add Sym";
      this.Load += new System.EventHandler(this.frmAddSym_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAddSym_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtSym;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOkay;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.TextBox txtID;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox chkRemember;
  }
}