namespace PwrIDE
{
  partial class frmRunRep
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRunRep));
      this.label1 = new System.Windows.Forms.Label();
      this.txtFile = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtServer = new System.Windows.Forms.TextBox();
      this.txtSym = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtQueue = new System.Windows.Forms.TextBox();
      this.chkQueueAlways = new System.Windows.Forms.CheckBox();
      this.optUserSelect = new System.Windows.Forms.RadioButton();
      this.optFirstEmpty = new System.Windows.Forms.RadioButton();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.lblStatus = new System.Windows.Forms.Label();
      this.progStatus = new System.Windows.Forms.ProgressBar();
      this.btnRun = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(26, 13);
      this.label1.TabStop = false;
      this.label1.Text = "File:";
      // 
      // txtFile
      // 
      this.txtFile.Location = new System.Drawing.Point(44, 6);
      this.txtFile.Name = "txtFile";
      this.txtFile.ReadOnly = true;
      this.txtFile.Size = new System.Drawing.Size(210, 20);
      this.txtFile.TabStop = false;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(411, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(30, 13);
      this.label2.TabStop = false;
      this.label2.Text = "Sym:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(260, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(41, 13);
      this.label3.TabStop = false;
      this.label3.Text = "Server:";
      // 
      // txtServer
      // 
      this.txtServer.Location = new System.Drawing.Point(307, 6);
      this.txtServer.Name = "txtServer";
      this.txtServer.ReadOnly = true;
      this.txtServer.Size = new System.Drawing.Size(98, 20);
      this.txtServer.TabStop = false;
      // 
      // txtSym
      // 
      this.txtSym.Location = new System.Drawing.Point(447, 6);
      this.txtSym.Name = "txtSym";
      this.txtSym.ReadOnly = true;
      this.txtSym.Size = new System.Drawing.Size(49, 20);
      this.txtSym.TabStop = false;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtQueue);
      this.groupBox1.Controls.Add(this.chkQueueAlways);
      this.groupBox1.Controls.Add(this.optUserSelect);
      this.groupBox1.Controls.Add(this.optFirstEmpty);
      this.groupBox1.Location = new System.Drawing.Point(12, 32);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(162, 125);
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Queue Selection";
      // 
      // txtQueue
      // 
      this.txtQueue.Location = new System.Drawing.Point(7, 73);
      this.txtQueue.Name = "txtQueue";
      this.txtQueue.Size = new System.Drawing.Size(149, 20);
      this.txtQueue.TabStop = false;
      this.txtQueue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // chkQueueAlways
      // 
      this.chkQueueAlways.AutoSize = true;
      this.chkQueueAlways.Location = new System.Drawing.Point(26, 99);
      this.chkQueueAlways.Name = "chkQueueAlways";
      this.chkQueueAlways.Size = new System.Drawing.Size(130, 17);
      this.chkQueueAlways.TabIndex = 5;
      this.chkQueueAlways.Text = "always use this queue";
      this.chkQueueAlways.UseVisualStyleBackColor = true;
      // 
      // optUserSelect
      // 
      this.optUserSelect.AutoSize = true;
      this.optUserSelect.Location = new System.Drawing.Point(6, 50);
      this.optUserSelect.Name = "optUserSelect";
      this.optUserSelect.Size = new System.Drawing.Size(101, 17);
      this.optUserSelect.TabIndex = 3;
      this.optUserSelect.Text = "Specific Queue:";
      this.optUserSelect.UseVisualStyleBackColor = true;
      // 
      // optFirstEmpty
      // 
      this.optFirstEmpty.AutoSize = true;
      this.optFirstEmpty.Checked = true;
      this.optFirstEmpty.Location = new System.Drawing.Point(6, 25);
      this.optFirstEmpty.Name = "optFirstEmpty";
      this.optFirstEmpty.Size = new System.Drawing.Size(111, 17);
      this.optFirstEmpty.TabIndex = 2;
      this.optFirstEmpty.TabStop = true;
      this.optFirstEmpty.Text = "First Empty Queue";
      this.optFirstEmpty.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.lblStatus);
      this.groupBox2.Controls.Add(this.progStatus);
      this.groupBox2.Location = new System.Drawing.Point(180, 32);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(316, 125);
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Status";
      // 
      // lblStatus
      // 
      this.lblStatus.Location = new System.Drawing.Point(6, 30);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(304, 51);
      this.lblStatus.TabStop = false;
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // progStatus
      // 
      this.progStatus.Location = new System.Drawing.Point(6, 84);
      this.progStatus.Name = "progStatus";
      this.progStatus.Size = new System.Drawing.Size(304, 23);
      this.progStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progStatus.TabStop = false;
      // 
      // btnRun
      // 
      this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
      this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnRun.Location = new System.Drawing.Point(352, 163);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(69, 27);
      this.btnRun.TabIndex = 0;
      this.btnRun.Text = "   &Run   ";
      this.btnRun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
      this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnCancel.Location = new System.Drawing.Point(427, 163);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(69, 27);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // frmRunRep
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(506, 197);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnRun);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.txtSym);
      this.Controls.Add(this.txtServer);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtFile);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.Name = "frmRunRep";
      this.Text = "Run Report";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRunRep_FormClosing);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRunRep_KeyDown);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtFile;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtServer;
    private System.Windows.Forms.TextBox txtSym;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkQueueAlways;
    private System.Windows.Forms.RadioButton optUserSelect;
    private System.Windows.Forms.RadioButton optFirstEmpty;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.ProgressBar progStatus;
    private System.Windows.Forms.Button btnRun;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtQueue;
  }
}