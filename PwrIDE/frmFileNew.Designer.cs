namespace PwrIDE
{
  partial class frmFileNew
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileNew));
      this.lblPwr = new System.Windows.Forms.Label();
      this.lblRep = new System.Windows.Forms.Label();
      this.lblLtr = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblPwr
      // 
      this.lblPwr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lblPwr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblPwr.Image = ((System.Drawing.Image)(resources.GetObject("lblPwr.Image")));
      this.lblPwr.Location = new System.Drawing.Point(12, 9);
      this.lblPwr.Name = "lblPwr";
      this.lblPwr.Size = new System.Drawing.Size(67, 65);
      this.lblPwr.TabIndex = 3;
      this.lblPwr.Text = "PwrPlus";
      this.lblPwr.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.lblPwr.Click += new System.EventHandler(this.lblPwr_Click);
      // 
      // lblRep
      // 
      this.lblRep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lblRep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblRep.Image = ((System.Drawing.Image)(resources.GetObject("lblRep.Image")));
      this.lblRep.Location = new System.Drawing.Point(94, 9);
      this.lblRep.Name = "lblRep";
      this.lblRep.Size = new System.Drawing.Size(67, 65);
      this.lblRep.TabIndex = 4;
      this.lblRep.Text = "RepGen";
      this.lblRep.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.lblRep.Click += new System.EventHandler(this.lblRep_Click);
      // 
      // lblLtr
      // 
      this.lblLtr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.lblLtr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblLtr.Image = ((System.Drawing.Image)(resources.GetObject("lblLtr.Image")));
      this.lblLtr.Location = new System.Drawing.Point(177, 9);
      this.lblLtr.Name = "lblLtr";
      this.lblLtr.Size = new System.Drawing.Size(67, 65);
      this.lblLtr.TabIndex = 5;
      this.lblLtr.Text = "Letter";
      this.lblLtr.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.lblLtr.Click += new System.EventHandler(this.lblLtr_Click);
      // 
      // frmFileNew
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(256, 83);
      this.Controls.Add(this.lblLtr);
      this.Controls.Add(this.lblRep);
      this.Controls.Add(this.lblPwr);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmFileNew";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "New File";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFileNew_KeyDown);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblPwr;
    private System.Windows.Forms.Label lblRep;
    private System.Windows.Forms.Label lblLtr;
  }
}