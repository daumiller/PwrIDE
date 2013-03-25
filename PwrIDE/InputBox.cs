using System;
using System.Windows.Forms;

namespace System.Windows.Forms
{
  public partial class InputBox : Form
  {
    //========================================================================
    public InputBox(string Title, string Prompt, string Default, bool Mask)
    {
      InitializeComponent();
      Text = Title;
      lblPrompt.Text = Prompt;
      txtInput.Text = Default;
      if (Mask)
      {
        txtInput.PasswordChar = '*';
        txtInput.UseSystemPasswordChar = true;
      }
      txtInput.Focus();
      txtInput.SelectAll();
    }
    //========================================================================
    public string Input { get { return txtInput.Text; } }
    public int MaxLength
    {
      get { return txtInput.MaxLength; }
      set { txtInput.MaxLength = value; }
    }
    //========================================================================
    private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        DialogResult = DialogResult.OK;
      }
    }
    //========================================================================
    private void InputBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
  }
}
