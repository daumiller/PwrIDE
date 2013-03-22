namespace PwrIDE
{
  partial class frmAddServer
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddServer));
      this.btnOkay = new System.Windows.Forms.Button();
      this.txtIP = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.txtPort = new System.Windows.Forms.TextBox();
      this.txtUser = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtPass = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.btnTest = new System.Windows.Forms.Button();
      this.chkRemember = new System.Windows.Forms.CheckBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOkay
      // 
      this.btnOkay.Image = ((System.Drawing.Image)(resources.GetObject("btnOkay.Image")));
      this.btnOkay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnOkay.Location = new System.Drawing.Point(112, 175);
      this.btnOkay.Name = "btnOkay";
      this.btnOkay.Size = new System.Drawing.Size(58, 23);
      this.btnOkay.TabIndex = 8;
      this.btnOkay.Text = "&Okay";
      this.btnOkay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnOkay.UseVisualStyleBackColor = true;
      this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
      // 
      // txtIP
      // 
      this.txtIP.Location = new System.Drawing.Point(89, 38);
      this.txtIP.Name = "txtIP";
      this.txtIP.Size = new System.Drawing.Size(152, 20);
      this.txtIP.TabIndex = 2;
      this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_KeyPress);
      this.txtIP.Enter += new System.EventHandler(this.txtIP_Enter);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(63, 41);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(20, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "IP:";
      // 
      // btnCancel
      // 
      this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
      this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnCancel.Location = new System.Drawing.Point(176, 175);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 23);
      this.btnCancel.TabIndex = 9;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 68);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(29, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Port (usually 23):";
      // 
      // txtPort
      // 
      this.txtPort.Location = new System.Drawing.Point(89, 65);
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new System.Drawing.Size(152, 20);
      this.txtPort.TabIndex = 3;
      this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
      this.txtPort.Enter += new System.EventHandler(this.txtPort_Enter);
      // 
      // txtUser
      // 
      this.txtUser.Location = new System.Drawing.Point(89, 91);
      this.txtUser.Name = "txtUser";
      this.txtUser.Size = new System.Drawing.Size(152, 20);
      this.txtUser.TabIndex = 4;
      this.txtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
      this.txtUser.Enter += new System.EventHandler(this.txtUser_Enter);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(7, 94);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(78, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "AIX Username:";
      // 
      // txtPass
      // 
      this.txtPass.Location = new System.Drawing.Point(89, 117);
      this.txtPass.Name = "txtPass";
      this.txtPass.PasswordChar = '*';
      this.txtPass.Size = new System.Drawing.Size(152, 20);
      this.txtPass.TabIndex = 5;
      this.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtPass.UseSystemPasswordChar = true;
      this.txtPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPass_KeyPress);
      this.txtPass.Enter += new System.EventHandler(this.txtPass_Enter);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(7, 120);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(76, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "AIX Password:";
      // 
      // btnTest
      // 
      this.btnTest.Image = ((System.Drawing.Image)(resources.GetObject("btnTest.Image")));
      this.btnTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnTest.Location = new System.Drawing.Point(8, 175);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(56, 23);
      this.btnTest.TabIndex = 7;
      this.btnTest.Text = "&Test";
      this.btnTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
      // 
      // chkRemember
      // 
      this.chkRemember.AutoSize = true;
      this.chkRemember.Checked = true;
      this.chkRemember.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkRemember.Location = new System.Drawing.Point(123, 143);
      this.chkRemember.Name = "chkRemember";
      this.chkRemember.Size = new System.Drawing.Size(118, 17);
      this.chkRemember.TabIndex = 6;
      this.chkRemember.Text = "Remember Password";
      this.chkRemember.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(45, 15);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(38, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Name:";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(89, 12);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(152, 20);
      this.txtName.TabIndex = 1;
      this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
      this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
      // 
      // frmAddServer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(253, 207);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.chkRemember);
      this.Controls.Add(this.btnTest);
      this.Controls.Add(this.txtPass);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtUser);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtPort);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtIP);
      this.Controls.Add(this.btnOkay);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAddServer";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add Server";
      this.Load += new System.EventHandler(this.frmAddServer_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAddServer_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOkay;
    private System.Windows.Forms.TextBox txtIP;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtPort;
    private System.Windows.Forms.TextBox txtUser;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtPass;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.CheckBox chkRemember;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtName;
  }
}