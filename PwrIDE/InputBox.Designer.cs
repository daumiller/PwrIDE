namespace System.Windows.Forms
{
  partial class InputBox
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
      this.lblPrompt = new System.Windows.Forms.Label();
      this.txtInput = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // lblPrompt
      // 
      this.lblPrompt.Location = new System.Drawing.Point(12, 9);
      this.lblPrompt.Name = "lblPrompt";
      this.lblPrompt.Size = new System.Drawing.Size(336, 43);
      this.lblPrompt.TabIndex = 0;
      this.lblPrompt.Text = resources.GetString("lblPrompt.Text");
      // 
      // txtInput
      // 
      this.txtInput.Location = new System.Drawing.Point(15, 55);
      this.txtInput.Name = "txtInput";
      this.txtInput.Size = new System.Drawing.Size(333, 20);
      this.txtInput.TabIndex = 1;
      this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_KeyPress);
      // 
      // InputBox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(360, 87);
      this.Controls.Add(this.txtInput);
      this.Controls.Add(this.lblPrompt);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InputBox";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "InputBox";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblPrompt;
    private System.Windows.Forms.TextBox txtInput;
  }
}