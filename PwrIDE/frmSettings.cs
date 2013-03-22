using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PwrIDE
{
  public partial class frmSettings : Form
  {
    //========================================================================
    private Dictionary<string, string> ConfigCache = new Dictionary<string, string>();
    //========================================================================
    public frmSettings()
    {
      InitializeComponent();

      //load fonts
      FontFamily[] fonts = FontFamily.Families;
      foreach (FontFamily font in fonts)
        lstFontFamily.Items.Add(font.Name);

      //load settings
      ReadConfig();

      lstFileType.SelectedIndex = 0;
      lstSyntaxType.SelectedIndex = 0;
    }
    //------------------------------------------------------------------------
    private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      btnCancel_Click(sender, EventArgs.Empty);
    }
    //========================================================================
    private void frmSettings_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        btnCancel_Click(sender, EventArgs.Empty);
    }
    //========================================================================
    private void PickColor(Object sender, EventArgs e)
    {
      Label who = (Label)sender;
      if(who.Enabled == false) return;
      colorPicker.Color = who.BackColor;
      if(colorPicker.ShowDialog(this) == DialogResult.OK)
      {
        who.BackColor = colorPicker.Color;
        string prefix = "Editor_Letter_";
             if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
        else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
        if(prefix == "Editor_Letter_") return;
        prefix += ListToRuleset(lstSyntaxType.Text);
        prefix += "_Color";
        ConfigCache[prefix] = who.BackColor.R.ToString()+','+who.BackColor.G.ToString()+','+who.BackColor.B.ToString();
      }
    }
    //========================================================================    
    private void btnCancel_Click(object sender, EventArgs e)
    {
      ReadConfig();
      Hide();
    }
    //------------------------------------------------------------------------
    private void btnApply_Click(object sender, EventArgs e)
    {
    	WriteConfig();
    }
    //------------------------------------------------------------------------
    private void btnOkay_Click(object sender, EventArgs e)
    {
      WriteConfig();
      Hide();
    }
    //========================================================================
    private void ReadConfig()
    {
      string a = "Editor_";
      string[] b = new string[]{ "PwrPls_", "RepGen_", "Letter_" };
      string[] c = new string[]{ "Font", "Size", "Tabs", "Spaces", "Highlight" };
      string[] d = new string[]{ "BuiltIns_", "Comment_", "Control_", "DataType_", "Date_", "Digits_", "Include_", "Logic_", "Punctuation_", "Fields_", "Section_", "String_", "SysVar_"};
      string[] e = new string[]{ "Color", "Bold", "Italic" };

      ConfigCache.Clear();
      foreach(string B in b)
      {
        foreach(string C in c)
          ConfigCache.Add(a+B+C, Config.GetString(a+B+C));
        if(B != "Letter_")
          foreach(string D in d)
            foreach(string E in e)
              ConfigCache.Add(a+B+D+E, Config.GetString(a+B+D+E));
      }
    }
    //========================================================================
    private void WriteConfig()
    {
      foreach(KeyValuePair<string, string> kvp in ConfigCache)
        Config.SetValue(kvp.Key, kvp.Value, true);
      Config.UpdateSyntax();
    }
    //========================================================================
    private void lstFileType_SelectedIndexChanged(object sender, EventArgs e)
    {
      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      lstFontFamily.Text    = ConfigCache[prefix + "Font"     ];
      lstFontSize.Text      = ConfigCache[prefix + "Size"     ];
      txtTabWidth.Text      = ConfigCache[prefix + "Tabs"     ];
      chkTabSpaces.Checked  = ConfigCache[prefix + "Spaces"   ] == "True";
      chkHighlight.Checked  = ConfigCache[prefix + "Highlight"] == "True";
      lstSyntaxType.Enabled = lblColor.Enabled = chkBold.Enabled = chkItalic.Enabled = (prefix != "Editor_Letter_");
      lstSyntaxType_SelectedIndexChanged(sender, e);
    }
    //------------------------------------------------------------------------
    private void lstFontFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      ConfigCache[prefix + "Font"] = lstFontFamily.Text;
    }
    //------------------------------------------------------------------------
    private void lstFontSize_TextChanged(object sender, EventArgs e)
    {
      try
      {
        float fl = float.Parse(lstFontSize.Text);
      }
      catch (Exception)
      {
        MessageBox.Show("Please enter a valid (integer/floating point) Font Size", "PwrIDE - Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
        lstFontSize.SelectAll();
        lstFontSize.Focus();
      }

      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      ConfigCache[prefix + "Size"] = lstFontSize.Text;
    }
    //------------------------------------------------------------------------
    private void txtTabWidth_TextChanged(object sender, EventArgs e)
    {
      try
      {
        float fl = float.Parse(lstFontSize.Text);
      }
      catch (Exception)
      {
        MessageBox.Show("Please enter a valid (integer) Tab Size", "PwrIDE - Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtTabWidth.SelectAll();
        txtTabWidth.Focus();
      }

      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      ConfigCache[prefix + "Tabs"] = txtTabWidth.Text;
    }
    //------------------------------------------------------------------------
    private void chkTabSpaces_CheckedChanged(object sender, EventArgs e)
    {
      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      ConfigCache[prefix + "Spaces"] = chkTabSpaces.Checked.ToString();
    }
    //------------------------------------------------------------------------
    private void chkHighlight_CheckedChanged(object sender, EventArgs e)
    {
      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      ConfigCache[prefix + "Highlight"] = chkHighlight.Checked.ToString();
    }
    //------------------------------------------------------------------------
    private void lstSyntaxType_SelectedIndexChanged(object sender, EventArgs e)
    {
      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      
      if(prefix == "Editor_Letter_") return;
      if(lstSyntaxType.Text == "") return;
      prefix += ListToRuleset(lstSyntaxType.Text);

      chkBold.Checked   = ConfigCache[prefix + "_Bold"  ] == "True";
      chkItalic.Checked = ConfigCache[prefix + "_Italic"] == "True";
      string[] rgb      = ConfigCache[prefix + "_Color" ].Split(new char[] { ',' });
      lblColor.BackColor = System.Drawing.Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
    }
    //------------------------------------------------------------------------
    private void chkBold_CheckStateChanged(object sender, EventArgs e)
    {
      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      if(prefix == "Editor_Letter_") return;
      prefix += ListToRuleset(lstSyntaxType.Text);
      prefix += "_Bold";
      ConfigCache[prefix] = chkBold.Checked.ToString();
    }
    //------------------------------------------------------------------------
    private void chkItalic_CheckStateChanged(object sender, EventArgs e)
    {
      string prefix = "Editor_Letter_";
           if((string)lstFileType.SelectedItem == "PowerPlus") prefix = "Editor_PwrPls_";
      else if((string)lstFileType.SelectedItem == "RepGen"   ) prefix = "Editor_RepGen_";
      if(prefix == "Editor_Letter_") return;
      prefix += ListToRuleset(lstSyntaxType.Text);
      prefix += "_Italic";
      ConfigCache[prefix] = chkItalic.Checked.ToString();
    }
    //========================================================================
    private string ListToRuleset(string lst)
    {
      switch (lst)
      {
        case "Comment":                return "Comment";
        case "Condition/Loop Keyword": return "Control";
        case "Data Type":              return "DataType";
        case "Date":                   return "Date";
        case "Digit":                  return "Digits";
        case "Include/Import":         return "Include";
        case "Logic Keyword":          return "Logic";
        case "Punctuation":            return "Punctuation";
        case "Record/Field Name":      return "Fields";
        case "Section":                return "Section";
        case "String":                 return "String";
        case "Symitar Procedure":      return "BuiltIns";
        case "Symitar Variable":       return "SysVar";
      }
      throw new Exception("ListToRuleset Exception: Unknown List Type");
    }
    //========================================================================
  }
}
