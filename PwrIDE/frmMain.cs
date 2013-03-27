using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using System.Text.RegularExpressions;
using Symitar;

namespace PwrIDE
{
  public partial class frmMain : Form
  {
    //========================================================================
    //  members
    //========================================================================
    public frmSource   ActiveSource = null;
    public frmProject  Explorer;
    public frmError    Errors;
    public frmSettings Settings;
    //========================================================================
    //  constructor/load/close
    //========================================================================
    public frmMain()
    {
      InitializeComponent();

			Util.MainForm = this;
      Config.Init();
      Config.Load(Application.StartupPath + "\\config.xml");
      Left   = Config.GetInt("Window_Left"  );
      Top    = Config.GetInt("Window_Top"   );
      Width  = Config.GetInt("Window_Width" );
      Height = Config.GetInt("Window_Height");
      if(Config.GetBool("Window_Maximized")) WindowState=FormWindowState.Maximized;
      Config.LoadSyntax();

      Explorer = new frmProject();
      Errors   = new frmError();
      Settings = new frmSettings();
    }
    //------------------------------------------------------------------------
    private void frmMain_Load(object sender, EventArgs e)
    {
      stripToolbar.Renderer = new ToolStripProfessionalRenderer(new SkinColorTable());
      stripMenu.Renderer = new ToolStripProfessionalRenderer(new SkinColorTable());

      string position;
      Errors.Show(dockPanel, DockState.DockBottomAutoHide);
      position = Config.GetString("Errors_State");
      Errors.DockState = (DockState)Enum.Parse(typeof(DockState), position);
      try
      {
             if(position.Contains("Left"  )) dockPanel.DockLeftPortion   = (double)Config.GetInt("Errors_Size");
        else if(position.Contains("Right" )) dockPanel.DockRightPortion  = (double)Config.GetInt("Errors_Size");
        else if(position.Contains("Top"   )) dockPanel.DockTopPortion    = (double)Config.GetInt("Errors_Size");
        else if(position.Contains("Bottom")) dockPanel.DockBottomPortion = (double)Config.GetInt("Errors_Size");
      }
      catch(Exception) { }

      Explorer.Show(dockPanel, DockState.DockRightAutoHide);
      position = Config.GetString("Explorer_State");
      Explorer.DockState = (DockState)Enum.Parse(typeof(DockState), position);
      try
      {
             if(position.Contains("Left"  )) dockPanel.DockLeftPortion   = (double)Config.GetInt("Explorer_Size");
        else if(position.Contains("Right" )) dockPanel.DockRightPortion  = (double)Config.GetInt("Explorer_Size");
        else if(position.Contains("Top"   )) dockPanel.DockTopPortion    = (double)Config.GetInt("Explorer_Size");
        else if(position.Contains("Bottom")) dockPanel.DockBottomPortion = (double)Config.GetInt("Explorer_Size");
      }
      catch(Exception) { }

      string[] args = Environment.GetCommandLineArgs();
      if(args.Length > 1)
      {
        for(int i=1; i<args.Length; i++)
        {
          frmSource passedIn = new frmSource(args[i]);
          passedIn.Show(dockPanel);
        }
      }
    }
    //------------------------------------------------------------------------
    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      IDockContent[] docs = dockPanel.DocumentsToArray();
      for (int i = 0; i < docs.Length; i++)
      {
        if (((frmSource)docs[i]).AbortClose())
        {
          e.Cancel = true;
          return;
        }
      }

      if(WindowState == FormWindowState.Maximized)
        Config.SetValue("Window_Maximized", "true", true);
      else
      {
        Config.SetValue("Window_Left"  , Left.ToString()  , true);
        Config.SetValue("Window_Top"   , Top.ToString()   , true);
        Config.SetValue("Window_Width" , Width.ToString() , true);
        Config.SetValue("Window_Height", Height.ToString(), true);
        Config.SetValue("Window_Maximized", "false", true);
      }

      if(Explorer.DockState.ToString().Substring(0,4) == "Dock")
      {
        string position = Explorer.DockState.ToString();
        Config.SetValue("Explorer_State" , position, true);
             if(position.Contains("Left"  )) Config.SetValue("Explorer_Size", Explorer.Width.ToString() , true);
        else if(position.Contains("Right" )) Config.SetValue("Explorer_Size", Explorer.Width.ToString() , true);
        else if(position.Contains("Top"   )) Config.SetValue("Explorer_Size", Explorer.Height.ToString(), true);
        else if(position.Contains("Bottom")) Config.SetValue("Explorer_Size", Explorer.Height.ToString(), true);
      }

      if(Errors.DockState.ToString().Substring(0,4) == "Dock")
      {
        string position = Errors.DockState.ToString();
        Config.SetValue("Errors_State" , position, true);
             if(position.Contains("Left"  )) Config.SetValue("Errors_Size", Errors.Width.ToString() , true);
        else if(position.Contains("Right" )) Config.SetValue("Errors_Size", Errors.Width.ToString() , true);
        else if(position.Contains("Top"   )) Config.SetValue("Errors_Size", Errors.Height.ToString(), true);
        else if(position.Contains("Bottom")) Config.SetValue("Errors_Size", Errors.Height.ToString(), true);
      }

      Config.Save(Application.StartupPath + "\\config.xml");
      Util.DisconnectAll();
    }
    //========================================================================
    //  helpers
    //========================================================================
    public void OpenLocalFile(string filename)
    {
      new frmSource(filename).Show(dockPanel);
    }
    //------------------------------------------------------------------------
    public void OpenSymitarFile(SymInst inst, SymFile file)
    {
    	new frmSource(inst, file).Show(dockPanel);
    }
    //========================================================================
    //  dockpanel events
    //========================================================================
    private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
    {
      if (ActiveSource != null) ActiveSource.TabDeactivated();
      ActiveSource = (frmSource)dockPanel.ActiveDocument;
      if (ActiveSource != null) ActiveSource.TabActivated();
      
      if(ActiveSource == null) Text = "PwrIDE";
    }
    //========================================================================
    //  menu/toolbar events
    //========================================================================
    private void mnuHiddenNewTab_Click(object sender, EventArgs e)
    {
      frmFileNew newFile = new frmFileNew();
      newFile.ShowDialog(this);
      newFile.Dispose();
      if(ActiveSource != null) ActiveSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuHiddenNextTab_Click(object sender, EventArgs e)
    {
      IDockContent[] docs = dockPanel.DocumentsToArray();
      if (docs.Length < 2) return;
      int index = 0;
      for (int i = 0; i < docs.Length; i++)
        if ((frmSource)docs[i] == ActiveSource)
          index = i;
      ((frmSource)docs[index]).TabDeactivated();
      index++; if (index == docs.Length) index = 0;
      ((frmSource)docs[index]).Activate();
      if (ActiveSource != null) ActiveSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuHiddenPrevTab_Click(object sender, EventArgs e)
    {
      IDockContent[] docs = dockPanel.DocumentsToArray();
      if (docs.Length < 2) return;
      int index = 0;
      for (int i = 0; i < docs.Length; i++)
        if ((frmSource)docs[i] == ActiveSource)
          index = i;
      ((frmSource)docs[index]).TabDeactivated();
      index--; if (index == -1) index = (docs.Length-1);
      ((frmSource)docs[index]).Activate();
      if (ActiveSource != null) ActiveSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuHiddenOpenFile_Click(object sender, EventArgs e)
    {
      frmFileOpen openFile = new frmFileOpen();
      DialogResult result = openFile.ShowDialog(this);
      openFile.Dispose();
      if (result == DialogResult.No)
        mnuFileOpenLocal_Click(sender, e);
      if (ActiveSource != null) ActiveSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuHiddenNewFile_Click(object sender, EventArgs e)
    {
      frmFileNew newFile = new frmFileNew();
      newFile.ShowDialog(this);
      newFile.Dispose();
      if (ActiveSource != null) ActiveSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    public void mnuFileNewRep_Click(object sender, EventArgs e)
    {
      frmSource src = new frmSource(ProjectFile.FileType.REPGEN);
      src.Show(dockPanel);
      src.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    public void mnuFileNewLtr_Click(object sender, EventArgs e)
    {
      frmSource src = new frmSource(ProjectFile.FileType.LETTER);
      src.Show(dockPanel);
      src.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuFileOpenSym_Click(object sender, EventArgs e)
    {
      frmFileOpen openFile = new frmFileOpen();
      DialogResult result = openFile.ShowDialog(this);
      openFile.Dispose();
      if (result == DialogResult.OK)
        mnuFileOpenLocal_Click(sender, e);
      if (ActiveSource != null) ActiveSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuFileOpenLocal_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Multiselect = true;
      ofd.Filter = "RepGen Files (*.rep)|*.rep|Letterfiles (*.ltr)|*.ltr|All Files|*.*";
      if (ofd.ShowDialog(this) == DialogResult.OK)
      {
        for (int i = 0; i < ofd.FileNames.Length; i++)
          OpenLocalFile(ofd.FileNames[i]);
      }
      if (ActiveSource != null) ActiveSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuFileSave_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        ActiveSource.FileSave();
    }
    //------------------------------------------------------------------------
    private void mnuFileSaveAsSym_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        ActiveSource.FileSaveAs("SYM");
    }
    //------------------------------------------------------------------------
    private void mnuFileSaveAsLocal_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        ActiveSource.FileSaveAs("LOCAL");
    }
    //------------------------------------------------------------------------
    private void mnuFileSaveAll_Click(object sender, EventArgs e)
    {
      IDockContent[] docs = dockPanel.DocumentsToArray();
      for (int i = 0; i < docs.Length; i++)
      {
        if (((frmSource)docs[i]).modified == true)
          ((frmSource)docs[i]).FileSave();
      }
    }
    //------------------------------------------------------------------------
    private void mnuFileClose_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        ActiveSource.Close();
      if (ActiveSource != null) ActiveSource.icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    private void mnuFileExit_Click(object sender, EventArgs e)
    {
      Close();
      Application.Exit();
    }
    //------------------------------------------------------------------------
    private void mnuVersionControl_Click(object sender, EventArgs e)
    {
    }
    //------------------------------------------------------------------------
    private void mnuVersionAdd_Click(object sender, EventArgs e)
    {
    }
    //------------------------------------------------------------------------
    private void mnuVersionRemove_Click(object sender, EventArgs e)
    {
    }
    //------------------------------------------------------------------------
    private void mnuVersionCheckout_Click(object sender, EventArgs e)
    {
    }
    //------------------------------------------------------------------------
    private void mnuVersionCommit_Click(object sender, EventArgs e)
    {
    }
    //=========================================================================
    private void mnuUtilInstall_Click(object sender, EventArgs e)
    {
      if(ActiveSource != null)
        ActiveSource.InstallRep();
    }
    //------------------------------------------------------------------------
    private void mnuUtilCheck_Click(object sender, EventArgs e)
    {
    	if(ActiveSource != null)
    		ActiveSource.CheckRep();
    }
    //------------------------------------------------------------------------
    private void mnuUtilRun_Click(object sender, EventArgs e)
    {
      if(ActiveSource != null)
        ActiveSource.RunRep();
    }
    //------------------------------------------------------------------------
    private void mnuUtilImport_Click(object sender, EventArgs e)
    {
      frmImportProjects fip = new frmImportProjects();
      fip.ShowDialog(this);
    }
    //------------------------------------------------------------------------
    private void mnuEditCut_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        new ICSharpCode.TextEditor.Actions.Cut().Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void mnuEditCopy_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        new ICSharpCode.TextEditor.Actions.Copy().Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void mnuEditPaste_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        new ICSharpCode.TextEditor.Actions.Paste().Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void mnuEditUndo_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        ActiveSource.icsEditor.Document.UndoStack.Undo();
    }
    //------------------------------------------------------------------------
    private void mnuEditRedo_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        ActiveSource.icsEditor.Document.UndoStack.Redo();
    }
    //------------------------------------------------------------------------
    private void mnuEditSelectAll_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        new ICSharpCode.TextEditor.Actions.SelectWholeDocument().Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void mnuEditFind_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
      {
        frmFind finder = new frmFind('F');
        finder.Show(this);
      }
    }
    //------------------------------------------------------------------------
    private void mnuEditFindNext_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        frmFind.FindNext();
    }
    //------------------------------------------------------------------------
    private void mnuEditReplace_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
      {
        frmFind finder = new frmFind('R');
        finder.Show(this);
      }
    }
    //------------------------------------------------------------------------
    private void mnuEditMarksToggle_Click(object sender, EventArgs e)
    {
      if(ActiveSource != null)
      {
        new ICSharpCode.TextEditor.Actions.ToggleBookmark().Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
        ActiveSource.icsEditor.IsIconBarVisible = (ActiveSource.icsEditor.Document.BookmarkManager.Marks.Count > 0);
      }
    }
    //------------------------------------------------------------------------
    private void mnuEditMarksNext_Click(object sender, EventArgs e)
    {
      if(ActiveSource != null)
        new ICSharpCode.TextEditor.Actions.GotoNextBookmark(bookmark => true).Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void mnuEditMarksPrev_Click(object sender, EventArgs e)
    {
      if(ActiveSource != null)
        new ICSharpCode.TextEditor.Actions.GotoPrevBookmark(bookmark => true).Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void mnuEditFoldExpand_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        new ICSharpCode.TextEditor.Actions.ExpandTopLevel().Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void mnuEditFoldCollapse_Click(object sender, EventArgs e)
    {
      if (ActiveSource != null)
        new ICSharpCode.TextEditor.Actions.CollapseTopLevel().Execute(ActiveSource.icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void mnuEditGoto_Click(object sender, EventArgs e)
    {
      if(ActiveSource == null) return;
      InputBox wndGoto = new InputBox("Goto Line", "Line Number:", "", false);
      if (wndGoto.ShowDialog(this) == DialogResult.OK)
      {
        try
        {
          int line = int.Parse(wndGoto.Input);
          ActiveSource.MoveToLine(line);
        }
        catch(Exception)
        {
        }
      }
    }
    //------------------------------------------------------------------------
    private void mnuHelpHelp_Click(object sender, EventArgs e)
    {
      //MessageBox.Show("Sorry... Helpfile not yet available!", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Information);
      Util.ShowEDocs();
    }
    //------------------------------------------------------------------------
    private void mnuHelpAbout_Click(object sender, EventArgs e)
    {
      frmAbout about = new frmAbout();
      about.ShowDialog(this);
    }
    //------------------------------------------------------------------------
    private void mnuViewProject_Click(object sender, EventArgs e)
    {
      if(Explorer.Visible)
        Explorer.Hide();
      else
        Explorer.Show();
    }
    //------------------------------------------------------------------------
    private void mnuViewErrors_Click(object sender, EventArgs e)
    {
      if(Errors.Visible)
        Errors.Hide();
      else
        Errors.Show();
    }
    //------------------------------------------------------------------------
    private void mnuViewSettings_Click(object sender, EventArgs e)
    {
      Settings.Show();
    }
    //------------------------------------------------------------------------
    private void toolProcedures_SelectedIndexChanged(object sender, EventArgs e)
    {
    	if(ActiveSource != null)
    		ActiveSource.MoveToProcedure((string)toolProcedures.Items[toolProcedures.SelectedIndex]);
    }
    //========================================================================
  }
}
