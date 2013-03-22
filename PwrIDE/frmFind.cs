using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ICSharpCode.TextEditor;

namespace PwrIDE
{
  public partial class frmFind : Form
  {
    //========================================================================
    public struct PreviousFind
    {
      public string search;
      public bool   sensitive;
      public bool   regular;
      public int    index;
      public bool   empty;
      public bool   restart;
      public Match  match;
      public string replace;
      public bool   replacing;

      public PreviousFind(bool dummy)
      {
        index  = -1;
        empty  = true;
        search = "";
        sensitive = true;
        regular = restart = false;
        match = Match.Empty;
        replace = "";
        replacing = false;
      }

      public void Assign(string Search, bool Sensitive, bool Regular)
      {
        search    = Search;
        sensitive = Sensitive;
        regular   = Regular;
        restart   = false;
        empty     = false;
        match     = Match.Empty;
        replace   = "";
        replacing = false;
        
        if((frmFind.prevSearches.Count == 0) || (frmFind.prevSearches[frmFind.prevSearches.Count - 1] != Search))
          frmFind.prevSearches.Add(Search);
      }
      public void Assign(string Search, bool Sensitive, bool Regular, string Replace)
      {
        search = Search;
        sensitive = Sensitive;
        regular = Regular;
        restart = false;
        empty = false;
        match = Match.Empty;
        replace = Replace;
        replacing = true;

        if((frmFind.prevSearches.Count == 0) || (frmFind.prevSearches[frmFind.prevSearches.Count - 1] != Search))
          frmFind.prevSearches.Add(Search);
        if(frmFind.prevSearches.Count > 10) frmFind.prevSearches.RemoveAt(0);

        if((frmFind.prevReplaces.Count == 0) || (frmFind.prevReplaces[frmFind.prevReplaces.Count - 1] != Replace))
          frmFind.prevReplaces.Add(Replace);
        if(frmFind.prevReplaces.Count > 10) frmFind.prevReplaces.RemoveAt(0);
      }
    }
    //========================================================================
    public static List<string> prevSearches = new List<string>();
    public static List<string> prevReplaces = new List<string>();
    public static PreviousFind prevFind     = new PreviousFind(false);
    //========================================================================
    public frmFind(char mode)
    {
      InitializeComponent();
      SetMode(mode);
      for(int i=(prevSearches.Count-1); i>-1; i--) cmbFind.Items.Add(   prevSearches[i]);
      for(int i=(prevReplaces.Count-1); i>-1; i--) cmbReplace.Items.Add(prevReplaces[i]);
    }
    //========================================================================
    private void frmFind_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        btnCancel_Click(sender, e);
    }
    //------------------------------------------------------------------------
    private void cmbFind_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Enter)
      {
        btnFind_Click(sender, e);
        Close();
      }
    }
    //------------------------------------------------------------------------
    private void cmbReplace_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Enter)
        btnReplace_Click(sender, e);
    }
    //------------------------------------------------------------------------
    private void lblFind_Click(object sender, EventArgs e)
    {
      SetMode('F');
    }
    //------------------------------------------------------------------------
    private void lblReplace_Click(object sender, EventArgs e)
    {
      SetMode('R');
    }
    //------------------------------------------------------------------------
    private void btnFind_Click(object sender, EventArgs e)
    {
      prevFind.Assign(cmbFind.Text, chkCase.Checked, chkRegex.Checked);
      FindNext();
    }
    //------------------------------------------------------------------------
    private void btnReplace_Click(object sender, EventArgs e)
    {
      prevFind.Assign(cmbFind.Text, chkCase.Checked, chkRegex.Checked, cmbReplace.Text);

      TextEditorControl ics = Util.MainForm.ActiveSource.icsEditor;
      //provide initial find, if needed
      if(!ics.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
      {
        FindNext();
        return; //let use see selection before replacing
      }
      
      if(ics.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
      {
        DoReplace();
        FindNext();
      }
      else
        MessageBox.Show("No Match Found to Replace", "PwrIDE - Replace", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    //------------------------------------------------------------------------
    private void btnReplaceAll_Click(object sender, EventArgs e)
    {
    	TextEditorControl ics = Util.MainForm.ActiveSource.icsEditor;
      prevFind.Assign(cmbFind.Text, chkCase.Checked, chkRegex.Checked, cmbReplace.Text);
      prevFind.restart = true;

			ics.Document.UndoStack.StartUndoGroup();
			while(FindNext())
				DoReplace();
			ics.Document.UndoStack.EndUndoGroup();
			
			ics.Refresh();
    }
    //------------------------------------------------------------------------
    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }
    //========================================================================
    private void SetMode(char modeChar)
    {
      bool enRep = (modeChar == 'R');

      lblReplaceLeft.Enabled = cmbReplace.Enabled = btnReplace.Enabled = btnReplaceAll.Enabled = enRep;

      if(enRep)
      {
        lblFind.BorderStyle = BorderStyle.None;
        lblReplace.BorderStyle = BorderStyle.FixedSingle;
      }
      else
      {
        lblFind.BorderStyle = BorderStyle.FixedSingle;
        lblReplace.BorderStyle = BorderStyle.None;
      }
    }
    //========================================================================
    public static bool FindNext()
    {
      if(Util.MainForm.ActiveSource == null) return false;

      TextEditorControl ics = Util.MainForm.ActiveSource.icsEditor;
      if(prevFind.empty)
      {
        frmFind finder = new frmFind('F');
        finder.Show(Util.MainForm);
        return false;
      }

      //find next occurence of prevFind
      string toSearch = ics.Document.TextContent;
      bool wasFirst = (prevFind.index == -1);
      if (prevFind.restart)
      {
        prevFind.restart = false;
        prevFind.index = 0;
      }
      else if(wasFirst)
      	prevFind.index = 0;
      else if(!prevFind.replacing) //don't change index if in replacing mode
      {
        if(prevFind.index == ics.ActiveTextAreaControl.Caret.Offset)
          prevFind.index++;
        else
          prevFind.index = ics.ActiveTextAreaControl.Caret.Offset;
      }
      int findLength = 0;
      if(!prevFind.regular)
      {
        StringComparison comparer = prevFind.sensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
        prevFind.index = toSearch.IndexOf(prevFind.search, prevFind.index, comparer);
        findLength = prevFind.search.Length;
      }
      else
      {
        RegexOptions comparer = prevFind.sensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
        try
        {
          prevFind.match = new Regex(prevFind.search, comparer | RegexOptions.Multiline).Match(toSearch, prevFind.index);
          if (prevFind.match.Success)
          {
            prevFind.index = prevFind.match.Index;
            findLength = prevFind.match.Value.Length;
          }
          else
            prevFind.index = -1;
        }
        catch(Exception ex)
        {
          MessageBox.Show("Error Creating Search (Bad RegEx String?)\nError: \""+ex.Message+"\"", "PwrIDE - Find", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return false;
        }
      }

      //found anything?
      if (prevFind.index == -1)
      {
        if(wasFirst)
          MessageBox.Show("\""+prevFind.search+"\" not found.", "PwrIDE - Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
        else
          MessageBox.Show("Reached end of document.", "PwrIDE - Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ics.ActiveTextAreaControl.SelectionManager.ClearSelection();
        prevFind.restart = true;
        return false;
      }
      else
      {
        //get starting position
        int line=0, idx, col, lasthit=0;
        for(idx=0; idx<prevFind.index; idx++) if(toSearch[idx]=='\n'){ line++; lasthit=idx+1;}
        col = prevFind.index - lasthit;
        TextLocation tlStart = new TextLocation(col, line);

        //get ending position
        int hit=-1; col += findLength;
        for(idx=1; idx<findLength; idx++)
          if(toSearch[idx+prevFind.index]=='\n')
            hit=idx;
        if(hit > -1) col -= hit;
        TextLocation tlEnd = new TextLocation(col, line);
        
        //select found area
        ics.ActiveTextAreaControl.Caret.Position = tlStart;
        ics.ActiveTextAreaControl.SelectionManager.SetSelection(tlStart, tlEnd);
        //prevFind.index += prevFind.search.Length;
        return true;
      }
    }
    //========================================================================
    private void DoReplace()
    {
      if(!prevFind.regular)
      {
        Util.MainForm.ActiveSource.icsEditor.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
        Util.MainForm.ActiveSource.icsEditor.ActiveTextAreaControl.Document.Insert(prevFind.index, prevFind.replace);
        prevFind.index += prevFind.replace.Length;
      }
      else
      {
        RegexOptions comparer = prevFind.sensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
        string orig = Util.MainForm.ActiveSource.icsEditor.ActiveTextAreaControl.SelectionManager.SelectedText;
        Util.MainForm.ActiveSource.icsEditor.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
        string repl = Regex.Replace(orig, prevFind.search, prevFind.replace, comparer);
        Util.MainForm.ActiveSource.icsEditor.ActiveTextAreaControl.Document.Insert(prevFind.index, repl);
        prevFind.index += repl.Length;
      }
      Util.MainForm.ActiveSource.icsEditor.ActiveTextAreaControl.SelectionManager.ClearSelection();
    }
    //========================================================================
  }
}
