namespace PwrIDE
{
  partial class frmLogin
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
      this.lblWhere = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.txtUser = new System.Windows.Forms.TextBox();
      this.txtPass = new System.Windows.Forms.TextBox();
      this.txtID = new System.Windows.Forms.TextBox();
      this.btnOkay = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblWhere
      // 
      this.lblWhere.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblWhere.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblWhere.Location = new System.Drawing.Point(0, 0);
      this.lblWhere.Name = "lblWhere";
      this.lblWhere.Size = new System.Drawing.Size(284, 21);
      this.lblWhere.TabIndex = 0;
      this.lblWhere.Text = "Server \"SomeServerName\", Sym 670";
      this.lblWhere.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "AIX Username:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(76, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "AIX Password:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(17, 82);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(73, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Sym Login ID:";
      // 
      // btnCancel
      // 
      this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
      this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnCancel.Location = new System.Drawing.Point(204, 116);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(68, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // txtUser
      // 
      this.txtUser.Location = new System.Drawing.Point(96, 27);
      this.txtUser.Name = "txtUser";
      this.txtUser.Size = new System.Drawing.Size(176, 20);
      this.txtUser.TabIndex = 5;
      this.txtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
      this.txtUser.Enter += new System.EventHandler(this.txtUser_Enter);
      // 
      // txtPass
      // 
      this.txtPass.Location = new System.Drawing.Point(96, 53);
      this.txtPass.Name = "txtPass";
      this.txtPass.PasswordChar = '*';
      this.txtPass.Size = new System.Drawing.Size(176, 20);
      this.txtPass.TabIndex = 6;
      this.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtPass.UseSystemPasswordChar = true;
      this.txtPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPass_KeyPress);
      this.txtPass.Enter += new System.EventHandler(this.txtPass_Enter);
      // 
      // txtID
      // 
      this.txtID.Location = new System.Drawing.Point(96, 79);
      this.txtID.Name = "txtID";
      this.txtID.PasswordChar = '*';
      this.txtID.Size = new System.Drawing.Size(176, 20);
      this.txtID.TabIndex = 7;
      this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtID.UseSystemPasswordChar = true;
      this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
      this.txtID.Enter += new System.EventHandler(this.txtID_Enter);
      // 
      // btnOkay
      // 
      this.btnOkay.Image = ((System.Drawing.Image)(resources.GetObject("btnOkay.Image")));
      this.btnOkay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnOkay.Location = new System.Drawing.Point(140, 116);
      this.btnOkay.Name = "btnOkay";
      this.btnOkay.Size = new System.Drawing.Size(58, 23);
      this.btnOkay.TabIndex = 8;
      this.btnOkay.Text = "&Okay";
      this.btnOkay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnOkay.UseVisualStyleBackColor = true;
      this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
      // 
      // frmLogin
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 144);
      this.Controls.Add(this.btnOkay);
      this.Controls.Add(this.txtID);
      this.Controls.Add(this.txtPass);
      this.Controls.Add(this.txtUser);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblWhere);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmLogin";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Login";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogin_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblWhere;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtUser;
    private System.Windows.Forms.TextBox txtPass;
    private System.Windows.Forms.TextBox txtID;
    private System.Windows.Forms.Button btnOkay;
  }
}