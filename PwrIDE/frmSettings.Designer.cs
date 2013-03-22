namespace PwrIDE
{
  partial class frmSettings
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
      this.icons = new System.Windows.Forms.ImageList(this.components);
      this.colorPicker = new System.Windows.Forms.ColorDialog();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnApply = new System.Windows.Forms.Button();
      this.btnOkay = new System.Windows.Forms.Button();
      this.lstFontFamily = new System.Windows.Forms.ComboBox();
      this.lstSyntaxType = new System.Windows.Forms.ComboBox();
      this.lstFontSize = new System.Windows.Forms.ComboBox();
      this.lstFileType = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.chkTabSpaces = new System.Windows.Forms.CheckBox();
      this.chkHighlight = new System.Windows.Forms.CheckBox();
      this.chkItalic = new System.Windows.Forms.CheckBox();
      this.chkBold = new System.Windows.Forms.CheckBox();
      this.lblColor = new System.Windows.Forms.Label();
      this.label24 = new System.Windows.Forms.Label();
      this.label16 = new System.Windows.Forms.Label();
      this.label25 = new System.Windows.Forms.Label();
      this.label32 = new System.Windows.Forms.Label();
      this.txtTabWidth = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // icons
      // 
      this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
      this.icons.TransparentColor = System.Drawing.Color.Transparent;
      this.icons.Images.SetKeyName(0, "SERVER");
      this.icons.Images.SetKeyName(1, "SYM");
      // 
      // colorPicker
      // 
      this.colorPicker.AnyColor = true;
      this.colorPicker.FullOpen = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
      this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnCancel.Location = new System.Drawing.Point(396, 168);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(67, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnApply
      // 
      this.btnApply.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.Image")));
      this.btnApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnApply.Location = new System.Drawing.Point(329, 168);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new System.Drawing.Size(61, 23);
      this.btnApply.TabIndex = 2;
      this.btnApply.Text = "&Apply";
      this.btnApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      // 
      // btnOkay
      // 
      this.btnOkay.Image = ((System.Drawing.Image)(resources.GetObject("btnOkay.Image")));
      this.btnOkay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnOkay.Location = new System.Drawing.Point(248, 168);
      this.btnOkay.Name = "btnOkay";
      this.btnOkay.Size = new System.Drawing.Size(75, 23);
      this.btnOkay.TabIndex = 3;
      this.btnOkay.Text = "&OK";
      this.btnOkay.UseVisualStyleBackColor = true;
      this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
      // 
      // lstFontFamily
      // 
      this.lstFontFamily.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.lstFontFamily.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.lstFontFamily.FormattingEnabled = true;
      this.lstFontFamily.Location = new System.Drawing.Point(100, 46);
      this.lstFontFamily.Name = "lstFontFamily";
      this.lstFontFamily.Size = new System.Drawing.Size(210, 21);
      this.lstFontFamily.TabIndex = 4;
      this.lstFontFamily.SelectedIndexChanged += new System.EventHandler(this.lstFontFamily_SelectedIndexChanged);
      // 
      // lstSyntaxType
      // 
      this.lstSyntaxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.lstSyntaxType.FormattingEnabled = true;
      this.lstSyntaxType.Items.AddRange(new object[] {
            "Comment",
            "Condition/Loop Keyword",
            "Data Type",
            "Date",
            "Digit",
            "Include/Import",
            "Logic Keyword",
            "Punctuation",
            "Record/Field Name",
            "Section",
            "String",
            "Symitar Procedure",
            "Symitar Variable"});
      this.lstSyntaxType.Location = new System.Drawing.Point(100, 116);
      this.lstSyntaxType.Name = "lstSyntaxType";
      this.lstSyntaxType.Size = new System.Drawing.Size(179, 21);
      this.lstSyntaxType.TabIndex = 5;
      this.lstSyntaxType.SelectedIndexChanged += new System.EventHandler(this.lstSyntaxType_SelectedIndexChanged);
      // 
      // lstFontSize
      // 
      this.lstFontSize.FormattingEnabled = true;
      this.lstFontSize.Items.AddRange(new object[] {
            "8.0",
            "9.0",
            "10.0",
            "11.0",
            "12.0",
            "15.0",
            "16.0",
            "24.0",
            "32.0"});
      this.lstFontSize.Location = new System.Drawing.Point(366, 46);
      this.lstFontSize.Name = "lstFontSize";
      this.lstFontSize.Size = new System.Drawing.Size(85, 21);
      this.lstFontSize.TabIndex = 14;
      this.lstFontSize.Text = "8.5";
      this.lstFontSize.TextChanged += new System.EventHandler(this.lstFontSize_TextChanged);
      // 
      // lstFileType
      // 
      this.lstFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.lstFileType.FormattingEnabled = true;
      this.lstFileType.Items.AddRange(new object[] {
            "PowerPlus",
            "RepGen",
            "Letterfile"});
      this.lstFileType.Location = new System.Drawing.Point(100, 12);
      this.lstFileType.Name = "lstFileType";
      this.lstFileType.Size = new System.Drawing.Size(210, 21);
      this.lstFileType.TabIndex = 13;
      this.lstFileType.SelectedIndexChanged += new System.EventHandler(this.lstFileType_SelectedIndexChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(326, 47);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(34, 16);
      this.label3.TabIndex = 12;
      this.label3.Text = "Size";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(57, 47);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(37, 16);
      this.label7.TabIndex = 11;
      this.label7.Text = "Font:";
      // 
      // chkTabSpaces
      // 
      this.chkTabSpaces.AutoSize = true;
      this.chkTabSpaces.Checked = true;
      this.chkTabSpaces.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkTabSpaces.Location = new System.Drawing.Point(198, 79);
      this.chkTabSpaces.Name = "chkTabSpaces";
      this.chkTabSpaces.Size = new System.Drawing.Size(103, 17);
      this.chkTabSpaces.TabIndex = 22;
      this.chkTabSpaces.Text = "Tabs as Spaces";
      this.chkTabSpaces.UseVisualStyleBackColor = true;
      this.chkTabSpaces.CheckedChanged += new System.EventHandler(this.chkTabSpaces_CheckedChanged);
      // 
      // chkHighlight
      // 
      this.chkHighlight.AutoSize = true;
      this.chkHighlight.Location = new System.Drawing.Point(324, 80);
      this.chkHighlight.Name = "chkHighlight";
      this.chkHighlight.Size = new System.Drawing.Size(127, 17);
      this.chkHighlight.TabIndex = 23;
      this.chkHighlight.Text = "Highlight Current Line";
      this.chkHighlight.UseVisualStyleBackColor = true;
      this.chkHighlight.CheckedChanged += new System.EventHandler(this.chkHighlight_CheckedChanged);
      // 
      // chkItalic
      // 
      this.chkItalic.AutoSize = true;
      this.chkItalic.Location = new System.Drawing.Point(417, 118);
      this.chkItalic.Name = "chkItalic";
      this.chkItalic.Size = new System.Drawing.Size(48, 17);
      this.chkItalic.TabIndex = 27;
      this.chkItalic.Text = "Italic";
      this.chkItalic.UseVisualStyleBackColor = true;
      this.chkItalic.CheckedChanged += new System.EventHandler(this.chkItalic_CheckStateChanged);
      // 
      // chkBold
      // 
      this.chkBold.AutoSize = true;
      this.chkBold.Location = new System.Drawing.Point(366, 118);
      this.chkBold.Name = "chkBold";
      this.chkBold.Size = new System.Drawing.Size(47, 17);
      this.chkBold.TabIndex = 26;
      this.chkBold.Text = "Bold";
      this.chkBold.UseVisualStyleBackColor = true;
      this.chkBold.CheckedChanged += new System.EventHandler(this.chkBold_CheckStateChanged);
      // 
      // lblColor
      // 
      this.lblColor.BackColor = System.Drawing.Color.Black;
      this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblColor.Location = new System.Drawing.Point(329, 113);
      this.lblColor.Name = "lblColor";
      this.lblColor.Size = new System.Drawing.Size(24, 24);
      this.lblColor.TabIndex = 25;
      this.lblColor.Click += new System.EventHandler(this.PickColor);
      // 
      // label24
      // 
      this.label24.AutoSize = true;
      this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label24.Location = new System.Drawing.Point(26, 13);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(68, 16);
      this.label24.TabIndex = 30;
      this.label24.Text = "File Type:";
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label16.Location = new System.Drawing.Point(21, 79);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(73, 16);
      this.label16.TabIndex = 31;
      this.label16.Text = "Tab Width:";
      // 
      // label25
      // 
      this.label25.AutoSize = true;
      this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label25.Location = new System.Drawing.Point(8, 117);
      this.label25.Name = "label25";
      this.label25.Size = new System.Drawing.Size(86, 16);
      this.label25.TabIndex = 32;
      this.label25.Text = "Syntax Type:";
      // 
      // label32
      // 
      this.label32.AutoSize = true;
      this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label32.Location = new System.Drawing.Point(285, 117);
      this.label32.Name = "label32";
      this.label32.Size = new System.Drawing.Size(43, 16);
      this.label32.TabIndex = 33;
      this.label32.Text = "Color:";
      // 
      // txtTabWidth
      // 
      this.txtTabWidth.Location = new System.Drawing.Point(100, 77);
      this.txtTabWidth.Name = "txtTabWidth";
      this.txtTabWidth.Size = new System.Drawing.Size(78, 20);
      this.txtTabWidth.TabIndex = 34;
      this.txtTabWidth.Text = "2";
      this.txtTabWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtTabWidth.TextChanged += new System.EventHandler(this.txtTabWidth_TextChanged);
      // 
      // frmSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(475, 198);
      this.Controls.Add(this.txtTabWidth);
      this.Controls.Add(this.label32);
      this.Controls.Add(this.label25);
      this.Controls.Add(this.label16);
      this.Controls.Add(this.label24);
      this.Controls.Add(this.chkItalic);
      this.Controls.Add(this.chkBold);
      this.Controls.Add(this.lblColor);
      this.Controls.Add(this.chkHighlight);
      this.Controls.Add(this.chkTabSpaces);
      this.Controls.Add(this.lstFontSize);
      this.Controls.Add(this.lstFileType);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.lstSyntaxType);
      this.Controls.Add(this.lstFontFamily);
      this.Controls.Add(this.btnOkay);
      this.Controls.Add(this.btnApply);
      this.Controls.Add(this.btnCancel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSettings";
      this.Text = "Settings";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSettings_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ColorDialog colorPicker;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnApply;
    private System.Windows.Forms.Button btnOkay;
    private System.Windows.Forms.ImageList icons;
    private System.Windows.Forms.ComboBox lstFontFamily;
    private System.Windows.Forms.ComboBox lstSyntaxType;
    private System.Windows.Forms.ComboBox lstFontSize;
    private System.Windows.Forms.ComboBox lstFileType;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.CheckBox chkTabSpaces;
    private System.Windows.Forms.CheckBox chkHighlight;
    private System.Windows.Forms.CheckBox chkItalic;
    private System.Windows.Forms.CheckBox chkBold;
    private System.Windows.Forms.Label lblColor;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label label25;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.TextBox txtTabWidth;
  }
}