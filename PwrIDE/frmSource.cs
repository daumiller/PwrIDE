using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using Symitar;

namespace PwrIDE
{
  public partial class frmSource : DockContent
  {
    //========================================================================
    // structs/enums
    //========================================================================
    public enum Origin
    {
      PROJECT = 0,
      LOCAL   = 1,
      SYM     = 2,
      NEW     = 3
    }
    //========================================================================
    //  members
    //========================================================================
    //shared by all origins
    public TextEditorControl    icsEditor;
    public bool                 modified;
    private int                 modifiedUndoStackPosition;
    public string               fileName;
    public Origin               fileOrigin;
    public ProjectFile.FileType fileType;
    private RepErr              errRep = RepErr.None();
    private ICompletionDataProvider completer;
    private IFoldingStrategy        folder;
    private Dictionary<string, int> procedureList = new Dictionary<string, int>();
    private bool procedureList_UserGenerated = true;
    //origin specific
    public ProjectFile fileProject = null;
    private string     fileLocal   = null;
    private SymFile    fileSym     = null;
    private SymInst    instSym     = null;
    //other
    private bool _skipSavePrompt = false;
    //========================================================================
    //  Constructors
    //========================================================================
    //Common Constructor Helper
    private void _initPre(string name, ProjectFile.FileType type)
    {
      InitializeComponent();

      icsEditor = new TextEditorControl();
      icsEditor.Dock = DockStyle.Fill;
      icsEditor.Document.DocumentChanged += icsEditor_DocumentChanged;
      icsEditor.ActiveTextAreaControl.TextArea.MouseDown += new MouseEventHandler(icsEditor_MouseDown);
      icsEditor.VRulerRow = 132;
      icsEditor.IndentStyle = IndentStyle.Smart;
      
      if(type == ProjectFile.FileType.REPGEN)
      {
        icsEditor.SetHighlighting("RepGen");
        completer = new RepGenComplete();
        folder    = new RepGenFold();
        icsEditor.Document.FoldingManager.FoldingStrategy = folder;
      }
      Controls.Add(icsEditor);

      DockAreas = DockAreas.Document;
      Text = fileName = name;
      Icon = IconFromType(type);
      fileType = type;
    }
    //------------------------------------------------------------------------
    //Common Contstructor Helper
    private void _initPost(string content)
    {
    	//LF w/o CR causes issues with ics.te's fold marks (doubles onto following lines)
    	if(content.IndexOf('\u000D')==-1)
    		content = content.Replace("\u000A", "\u000D\u000A");
      icsEditor.Text = content;
      SetModified(false, true);
      Config.RegisterUpdateHandler(UpdateSyntax);
      UpdateSyntax();
      icsEditor.Focus();
    }
    //------------------------------------------------------------------------
    //New Document
    public frmSource(ProjectFile.FileType type)
    {
      _initPre("New File", type);
      fileOrigin = Origin.NEW;
      _initPost("");
    }
    //------------------------------------------------------------------------
    //Compiled Non-Project Document
    public frmSource(string pwrFilename, string srcCompiled)
    {
      int lastDot = pwrFilename.LastIndexOf('.');
      if(lastDot == -1)
        _initPre(pwrFilename+".REP", ProjectFile.FileType.REPGEN);
      else
      {
        string guessBase = pwrFilename.Substring(0,lastDot);
        string guessExt  = pwrFilename.Substring(lastDot+1).ToUpper();
        string compiledFilename = (guessExt=="PWR") ? guessBase+".REP" : pwrFilename+".REP";
        _initPre(compiledFilename, ProjectFile.FileType.REPGEN);
      }
      fileOrigin = Origin.NEW;
      _initPost(srcCompiled);
    }
    //------------------------------------------------------------------------
    //Existing Local Document
    public frmSource(string localFilePath)
    {
      fileLocal = localFilePath;
      _initPre(Util.FileBaseNameLocal(localFilePath), FileTypeFromExtension(localFilePath));
      string content;
      try
      {
        content = Util.FileReadLocal(localFilePath);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Opening File \"" + localFilePath + "\"\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _skipSavePrompt = true;
        Close();
        return;
      }
      fileOrigin = Origin.LOCAL;
      _initPost(content);
    }
    //------------------------------------------------------------------------
    //Existing Symitar Document
    public frmSource(SymInst inst, SymFile file)
    {
      instSym = inst;
      fileSym = file;
      _initPre(file.name, FileTypeFromSymFile(file));
      string content;
      try
      {
        content = Util.FileReadSym(inst, file);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Opening File \"" + file.name + "\"\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _skipSavePrompt = true;
        Close();
        return;
      }
      fileOrigin = Origin.SYM;
      _initPost(content);
      Text = '['+inst.SymDir+"] "+file.name; //override this setting from _initPre to include Sym Number
    }
    //------------------------------------------------------------------------
    //Project Document
    public frmSource(ProjectFile file)
    {
      fileProject = file;
      _initPre(file.Name, file.Type);
      string content;
      try
      {
        content = file.Read();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Opening File \"" + file.Name + "\"\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _skipSavePrompt = true;
        Close();
        return;
      }
      fileOrigin = Origin.PROJECT;
      _initPost(content);
      if(file.Parent.Local == false) Text='['+file.Parent.ParentSym.SymDir+"] "+fileName;
    }
    //========================================================================
    // reload an existing PowerPlus->RepGen compiled source
    //========================================================================
    public bool ReloadProjectFile()
    {
      if(fileOrigin != Origin.PROJECT) throw new Exception("Called ReloadProjectFile on a Non-Project Document\nThis Shouldn't Happen");

      string content;
      try
      {
        content = fileProject.Read();
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Reloading Compiled Document \""+fileName+"\"\n"+ex.Message);
        _skipSavePrompt = true;
        Close();
        return false;
      }
      icsEditor.Text = content;
      SetModified(false, true);
      icsEditor.Focus();
      return true;
    }
    //========================================================================
    //  project file renamed
    //========================================================================
    public void ProjectFileRenamed()
    {
      if(fileProject.Parent.Local)
        Text = fileProject.Name;
      else
        Text = '['+fileProject.Parent.ParentSym.SymDir+"] "+fileProject.Name;
    }
    //========================================================================
    //  more constructor helpers
    //========================================================================
    private ProjectFile.FileType FileTypeFromSymFile(SymFile file)
    {
      if(file.type != SymFile.Type.REPGEN) return ProjectFile.FileType.LETTER;
      int lastDot = file.name.LastIndexOf('.');
      if (lastDot == -1)
        return ProjectFile.FileType.REPGEN;
      string extension = file.name.Substring(lastDot + 1);
      return ProjectFile.FileType.REPGEN;
    }
    //------------------------------------------------------------------------
    private ProjectFile.FileType FileTypeFromExtension(string NameOrPath)
    {
      int lastDot = NameOrPath.LastIndexOf('.');
      if(lastDot == -1)
        return ProjectFile.FileType.LETTER;
      string extension = NameOrPath.Substring(lastDot+1);
      if(extension.ToUpper() == "REP") return ProjectFile.FileType.REPGEN;
      return ProjectFile.FileType.LETTER;
    }
    //------------------------------------------------------------------------
    private string ExtensionFromType(ProjectFile.FileType type)
    {
      if(type == ProjectFile.FileType.REPGEN) return ".REP";
      if(type == ProjectFile.FileType.LETTER) return ".LTR";
      return "";
    }
    //------------------------------------------------------------------------
    private System.Drawing.Icon IconFromType(ProjectFile.FileType type)
    {
      if(type == ProjectFile.FileType.REPGEN) return Icon.FromHandle(((Bitmap)icons.Images["REP"]).GetHicon());
      if(type == ProjectFile.FileType.LETTER) return Icon.FromHandle(((Bitmap)icons.Images["LTR"]).GetHicon());
      throw new Exception("IconFromType: Undefined ProjectFile.FileType Passed\nThis Shouldn't Happen");
    }
    //========================================================================
    // load/unload/close
    //========================================================================
    private void frmSource_Load(object sender, EventArgs e)
    {
    }
    //------------------------------------------------------------------------
    private void frmSource_FormClosing(object sender, FormClosingEventArgs e)
    {
      if(_skipSavePrompt) return;

      if (AbortClose())
      {
        e.Cancel = true;
        return;
      }
      Config.RemoveUpdateHandler(UpdateSyntax);
    }
    //------------------------------------------------------------------------
    public bool AbortClose()
    {
      if (!modified)
        return false;

      switch (MessageBox.Show("Save File \"" + fileName + "\" Before Closing?", "PwrIDE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
      {
        case DialogResult.Yes:
          bool tryAgain = true;
          while (tryAgain)
          {
            tryAgain = false;
            try { FileSave(); }
            catch (Exception ex)
            {
              switch (MessageBox.Show(ex.Message + "\n\nRetry", "PwrIDE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
              {
                case DialogResult.Yes:
                  tryAgain = true;
                  break;
                case DialogResult.No:
                  break;
                case DialogResult.Cancel:
                  return true;
              }
            }
          }
          break;
        case DialogResult.No:
          break;
        case DialogResult.Cancel:
          return true;
      }
      return false;
    }
    //========================================================================
    //  ui proxy functions
    //========================================================================
    public void FileSave()
    {
      switch(fileOrigin)
      {
        case Origin.NEW    : SaveAsSymitar(); break;
        case Origin.LOCAL  : SaveLocal();     break;
        case Origin.SYM    : SaveSymitar();   break;
        case Origin.PROJECT: SaveProject();   break;
      }
    }
    //------------------------------------------------------------------------
    public void FileSaveAs(string which)
    {
      if(which == "SYM")
        SaveAsSymitar();
      else
        SaveAsLocal();
    }
    //------------------------------------------------------------------------
    public void CheckRep()
    {
      if(fileOrigin == Origin.NEW)   return;
      if(fileOrigin == Origin.LOCAL) return;
      if(fileType   != ProjectFile.FileType.REPGEN) return;
      if(fileOrigin == Origin.PROJECT) if(fileProject.Parent.Local) return;

      SymInst inst = (fileOrigin==Origin.PROJECT) ? fileProject.Parent.ParentSym : Config.GetSymIP(fileSym.server, fileSym.sym);
      string  name = (fileOrigin==Origin.PROJECT) ? fileProject.Name             : fileSym.name;
      SymFile file = (fileOrigin==Origin.PROJECT) ? fileProject.ToSymFile(inst)  : fileSym;
      
      if(inst == null)
        throw new Exception("Error Checking File\nFile's SymSession Instance Not Found\n(this shouldn't happen)");

      if(modified)
        FileSave();

      RepErr err = RepErr.None();
      bool completed = false, tryAgain = true;
      while (tryAgain)
      {
        tryAgain = false;
        if (Util.TrySymConnect(inst))
        {
          try
          {
            err = inst.Connection.FileCheck(file);
            completed = true;
          }
          catch (FileNotFoundException ex)
          {
            if (MessageBox.Show("Error Checking File \"" + name + "\"\n" + ex.Message + "\n\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
              tryAgain = true;
            inst.Disconnect();
          }
        }
      }
      if (completed)
      {
        SetRepError(err);
        if(err.any == false) MessageBox.Show("No Errors!");
      }
    }
    //------------------------------------------------------------------------
    public void InstallRep()
    {
      if(fileOrigin == Origin.NEW)   return;
      if(fileOrigin == Origin.LOCAL) return;
      if(fileType   != ProjectFile.FileType.REPGEN) return;
      if(fileOrigin == Origin.PROJECT) if(fileProject.Parent.Local) return;

      SymInst inst = (fileOrigin==Origin.PROJECT) ? fileProject.Parent.ParentSym : Config.GetSymIP(fileSym.server, fileSym.sym);
      string  name = (fileOrigin==Origin.PROJECT) ? fileProject.Name             : fileSym.name;
      SymFile file = (fileOrigin==Origin.PROJECT) ? fileProject.ToSymFile(inst)  : fileSym;
      
      if(inst == null)
        throw new Exception("Error Installing File\nSymFile's Instance Not Found in Config\n(this shouldn't happen)");

      if(modified)
        FileSave();

      RepErr err = RepErr.None();
      bool completed = false, tryAgain = true;
      while(tryAgain)
      {
        tryAgain = false;
        if(Util.TrySymConnect(inst))
        {
          try
          {
            err = inst.Connection.FileInstall(file);
            completed = true;
          }
          catch (Exception ex)
          {
            if (MessageBox.Show("Error Installing File \"" + name + "\"\n" + ex.Message + "\n\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
              tryAgain = true;
            inst.Disconnect();
          }
        }
      }
      if(completed)
      {
        SetRepError(err);
        if(err.any == false) MessageBox.Show("Installed \"" + name + "\"\nSize: " + Util.FormatBytes(err.installedSize));
      }
    }
    //------------------------------------------------------------------------
    public void RunRep()
    {
      if(fileOrigin == Origin.NEW)   return;
      if(fileOrigin == Origin.LOCAL) return;
      if(fileType   != ProjectFile.FileType.REPGEN) return;
      if(fileOrigin == Origin.PROJECT) if(fileProject.Parent.Local) return;

      SymInst inst = (fileOrigin==Origin.PROJECT) ? fileProject.Parent.ParentSym : Config.GetSymIP(fileSym.server, fileSym.sym);
      SymFile file = (fileOrigin==Origin.PROJECT) ? fileProject.ToSymFile(inst)  : fileSym;
      
      if(inst == null)
        throw new Exception("Error Running File\nSymFile's Instance Not Found in Config\n(this shouldn't happen)");

      if(modified)
        FileSave();

      frmRunRep runner = new frmRunRep(file, inst.Parent, inst);
      runner.Show();
    }
    //------------------------------------------------------------------------
    public void SetRepError(RepErr error)
    {
      errRep = error;
      if(errRep.any)
      {
        Util.MainForm.Errors.SetError(error.fileErr, error.line, error.error);
        Util.MainForm.Errors.Activate();
      }
      else
        Util.MainForm.Errors.ClearErrors();
    }
    //------------------------------------------------------------------------
    public void MoveToError()
    {
    	if(errRep.any)
    	{
    		if(errRep.fileSrc.name == errRep.fileErr) //error was actually in this file, not an included one
    		{
          icsEditor.ActiveTextAreaControl.Caret.Position = new TextLocation(0, errRep.line - 1);
          icsEditor.Focus();
	   		}
    		/*else
    			//TODO: //open source file (if not already open), setRepError on it, and MoveToLine() it*/
    	}
    }
    //------------------------------------------------------------------------
    public void MoveToProcedure(string procStr)
    {
    	if(!procedureList_UserGenerated)
    		return;
    	if(procedureList.ContainsKey(procStr))
    	{
    		icsEditor.ActiveTextAreaControl.Caret.Position = new TextLocation(0, procedureList[procStr]);
    		icsEditor.Focus();
    	}
    }
    //------------------------------------------------------------------------
    public void MoveToLine(int line)
    {
      if(line < 1) return;
      if(line > icsEditor.Document.LineSegmentCollection.Count) return;
      icsEditor.ActiveTextAreaControl.Caret.Position = new TextLocation(0, line-1);
    }
    //------------------------------------------------------------------------
    public Importer GetImporter()
    {
      if(fileOrigin == Origin.PROJECT) return new Importer(fileProject.Parent);
      if(fileOrigin == Origin.SYM    ) return new Importer(instSym);
      if(fileOrigin == Origin.LOCAL  ) return new Importer(Util.FileDirectoryPathLocal(fileLocal));
      return new Importer();
    }
    //========================================================================
    //  file i/o
    //========================================================================
    private bool SaveLocal()
    {
      try
      {
        File.WriteAllText(fileLocal, icsEditor.Text);
        SetModified(false, true);
        return true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Saving File\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }
    //------------------------------------------------------------------------
    private bool SaveSymitar()
    {
    	SymInst inst = Config.GetSymIP(fileSym.server, fileSym.sym);
    	if(inst == null)
    		throw new Exception("Error Saving File\nSymFile's Instance Not Found in Config\n(this shouldn't happen)");
    	
  		bool saved=false, tryAgain=true;
    	while(tryAgain)
    	{
    		tryAgain = false;
    		if(Util.TrySymConnect(inst))
    		{
	    		try
	    		{
            inst.Connection.FileWrite(fileSym, icsEditor.Text);
	    			saved=true;
	    		}
	    		catch(Exception ex)
	    		{
	    			if(MessageBox.Show("Error Saving File \""+fileSym.name+"\"\n"+ex.Message+"\n\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error)==DialogResult.Yes)
	    				tryAgain = true;
	    			inst.Disconnect();
	    		}
    		}
    	}
    	if(saved)
      {
        SetModified(false, true);
        return true;
      }
      return false;
    }
    //------------------------------------------------------------------------
    private void SaveProject()
    {
      bool saved=false, tryAgain=true;
      while(tryAgain)
      {
        tryAgain = false;
        try
        {
          fileProject.Write(icsEditor.Text);
          saved = true;
        }
        catch(Exception ex)
        {
          if(MessageBox.Show("Error Saving File \""+fileSym.name+"\"\n"+ex.Message+"\n\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error)==DialogResult.Yes)
	    			tryAgain = true;
          if(fileProject.Parent.Local) fileProject.Parent.ParentSym.Disconnect();
        }
      }
      if(saved)
        SetModified(false, true);
    }
    //------------------------------------------------------------------------
    private void SaveAsSymitar()
    {
    	frmFileOpen saveAs  = new frmFileOpen(fileName);
    	DialogResult result = saveAs.ShowDialog(Util.MainForm);
    	if(result == DialogResult.Cancel)
    	{
    		saveAs.Dispose();
    		return;
    	}
    	if(result == DialogResult.No)
    	{
    		saveAs.Dispose();
    		SaveAsLocal();
    		return;
    	}
      if(saveAs.saveAsIsLocal) //not a Local Filesystem file, but a LOCAL mounted Filesystem file
      {
        fileLocal = saveAs.saveAsLocal.Path + '\\' + saveAs.saveAsName;
        if(SaveLocal())
        {
          fileOrigin = Origin.LOCAL;
          fileType   = FileTypeFromExtension(fileLocal);
          Icon       = IconFromType(fileType);
        }
      }
      else
      {
        SymInst inst = saveAs.saveAsInst;
        fileSym = new SymFile(inst.Parent.IP, inst.SymDir, saveAs.saveAsName, DateTime.Now, icsEditor.Text.Length, saveAs.saveAsType);
        fileName = saveAs.saveAsName;
        if(SaveSymitar())
        {
          fileOrigin = Origin.SYM;
          fileType   = FileTypeFromSymFile(fileSym);
          Icon       = IconFromType(fileType);
          if(fileType == ProjectFile.FileType.REPGEN)
		      {
		        icsEditor.SetHighlighting("RepGen");
		        completer = new RepGenComplete();
		        folder    = new RepGenFold();
		        icsEditor.Document.FoldingManager.FoldingStrategy = folder;
		      }
		      else if(fileType == ProjectFile.FileType.LETTER)
		      {
		      	icsEditor.SetHighlighting("Default");
		      	icsEditor.Document.FoldingManager.FoldingStrategy = new ICSharpCode.TextEditor.Document.IndentFoldingStrategy();
		      }
          SetModified(false, true);
        }
      }
    }
    //------------------------------------------------------------------------
    private void SaveAsLocal()
    {
      SaveFileDialog sfd = new SaveFileDialog();
      sfd.DefaultExt       = ExtensionFromType(fileType);
      sfd.Filter           = "All Files (*.*)|*.*";
      sfd.FileName         = fileName;
      sfd.Title            = "Save As";
      if (fileLocal != null)
        sfd.InitialDirectory = Util.FileDirectoryPathLocal(fileLocal);
      if(sfd.ShowDialog() == DialogResult.OK)
      {
        try
        {
          Util.FileWriteLocal(sfd.FileName, icsEditor.Text);
          fileLocal  = sfd.FileName;
          fileName   = Util.FileBaseNameLocal(fileLocal);
          fileOrigin = Origin.LOCAL;
          fileType   = FileTypeFromExtension(fileLocal);
          Icon       = IconFromType(fileType);
          if(fileType == ProjectFile.FileType.REPGEN)
		      {
		        icsEditor.SetHighlighting("RepGen");
		        completer = new RepGenComplete();
		        folder    = new RepGenFold();
		        icsEditor.Document.FoldingManager.FoldingStrategy = folder;
		      }
		      else if(fileType == ProjectFile.FileType.LETTER)
		      {
		      	icsEditor.SetHighlighting("Default");
		      	icsEditor.Document.FoldingManager.FoldingStrategy = new ICSharpCode.TextEditor.Document.IndentFoldingStrategy();
		      }
          SetModified(false, true);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error Saving File\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      sfd.Dispose();
    }
    //========================================================================
    //  events
    //========================================================================
    public void TabActivated()
    {
      Util.MainForm.Text = Text + " - PwrIDE";

      Util.MainForm.Errors.ClearErrors();
      if(errRep.any) Util.MainForm.Errors.SetError(errRep.fileErr, errRep.line, errRep.error);
      
      procedureList_UserGenerated = false;
      if(Util.MainForm.toolProcedures.Items.Count>0)
        Util.MainForm.toolProcedures.SelectedIndex = 0;
    	UpdateProcedureList();
    	procedureList_UserGenerated = true;
    }
    public void TabDeactivated()
    {
    }
    //------------------------------------------------------------------------
    private void icsEditor_DocumentChanged(object sender, DocumentEventArgs e)
    {
      if(icsEditor.Document.UndoStack.UndoItemCount == 0)
        SetModified(false, false);
      else
        if(!modified)
          SetModified(true, false);

      if((completer != null) && (e.Text != null))
      {
        if((fileType == ProjectFile.FileType.REPGEN) && (e.Text==":"))
        {
          ICSharpCode.TextEditor.Gui.CompletionWindow.CodeCompletionWindow.ShowCompletionWindow(this,icsEditor,"filename",completer, e.Text[0]);
          icsEditor.Refresh();
          return;
        }
      }
      
      if(folder != null)
      {
        //special key (backspace/delete/enter)
        if((e.Text == null) && (e.Length==1))
        {
        	UpdateFoldings();
        	return;
        }
        
        //Comments (can easily effect blocking)
        if((fileType == ProjectFile.FileType.REPGEN) && (e.Text != null) && (e.Text.Length == 1))
          if((e.Text == "[") || (e.Text == "]"))
          {
        		UpdateFoldings();
        		return;
        	}
          	
        //length > 1, probably a paste, we'll go ahead & update foldings
        if((e.Text != null) && (e.Text.Length > 1))
        {
        	UpdateFoldings();
        	return;
        }
        	
        //look for END or RETURN in current line
        string updatedLine = icsEditor.Document.GetText(icsEditor.Document.GetLineSegmentForOffset(e.Offset)).ToUpper();
        if(updatedLine.Contains("END") || updatedLine.Contains("RETURN"))
        {
        	UpdateFoldings();
        	return;
        }
      }
      
      icsEditor.Refresh();
    }
    //========================================================================
    void icsEditor_MouseDown(object sender, MouseEventArgs e)
    {
      if(e.Button == MouseButtons.Right)
        icsContext.Show(Cursor.Position);
    }
    //========================================================================
    //  Source Context Menu
    //========================================================================
    private void icsContextCut_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Cut().Execute(icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void icsContextCopy_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Copy().Execute(icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void icsContextPaste_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Paste().Execute(icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void icsContextUndo_Click(object sender, EventArgs e)
    {
      icsEditor.Document.UndoStack.Undo();
    }
    //------------------------------------------------------------------------
    private void icsContextRedo_Click(object sender, EventArgs e)
    {
      icsEditor.Document.UndoStack.Redo();
    }
    //------------------------------------------------------------------------
    private void icsContextSelectAll_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.SelectWholeDocument().Execute(icsEditor.ActiveTextAreaControl.TextArea);
    }
    //------------------------------------------------------------------------
    private void icsContextBookmark_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.ToggleBookmark().Execute(icsEditor.ActiveTextAreaControl.TextArea);
      icsEditor.IsIconBarVisible = (icsEditor.Document.BookmarkManager.Marks.Count > 0);
    }
    //========================================================================
    //  helpers
    //========================================================================
    private void UpdateFoldings()
    {
    	procedureList_UserGenerated = false;
    	procedureList.Clear();
    	icsEditor.Document.FoldingManager.UpdateFoldings(null, procedureList);
    	icsEditor.Refresh();
    	UpdateProcedureList();
    	procedureList_UserGenerated = true;
    }
    //------------------------------------------------------------------------
    private void UpdateProcedureList()
    {
    	int currSel = Util.MainForm.toolProcedures.SelectedIndex;
    	if(currSel<0) currSel=0;
    		
    	Util.MainForm.toolProcedures.Items.Clear();
    	if(procedureList.Count > 0)
    	{
    		foreach(KeyValuePair<string, int> kvp in procedureList)
    			Util.MainForm.toolProcedures.Items.Add(kvp.Key);
    		if(currSel > (Util.MainForm.toolProcedures.Items.Count-1)) currSel=0;
    		Util.MainForm.toolProcedures.SelectedIndex = currSel;
    	}
    }
    //------------------------------------------------------------------------
    private void SetModified(bool mod, bool clearUndo)
    {
      if(mod == false)
      	if(clearUndo == true)
        	icsEditor.Document.UndoStack.ClearAll();

      modified = mod;

      string pre = "";
      if (fileOrigin == Origin.SYM) pre = '[' + fileSym.sym + "] ";
      if (fileOrigin == Origin.PROJECT) if (!fileProject.Parent.Local) pre = '[' + fileProject.Parent.ParentSym.SymDir + "] ";

      if(modified)
      {
        Text = pre + fileName + '*';
        Util.MainForm.Text = Text + " - PwrIDE";
      }
      else
      {
        modifiedUndoStackPosition = icsEditor.Document.UndoStack.UndoItemCount;
        Text = pre + fileName;
        Util.MainForm.Text = Text + " - PwrIDE";
      }
    }
    //------------------------------------------------------------------------
    private void UpdateSyntax()
    {
      string prefix = "Editor_Letter_";
      if(fileType == ProjectFile.FileType.REPGEN) prefix = "Editor_RepGen_";

      icsEditor.Font = new Font(Config.GetString(prefix+"Font"), Config.GetFloat(prefix+"Size"));
      icsEditor.TabIndent = icsEditor.Document.TextEditorProperties.IndentationSize = Config.GetInt(prefix + "Tabs");
      icsEditor.ConvertTabsToSpaces = Config.GetBool(prefix + "Spaces");
      icsEditor.LineViewerStyle = (Config.GetBool(prefix + "Highlight")) ? LineViewerStyle.FullRow : LineViewerStyle.None;

      icsEditor.Refresh();
    }
    //========================================================================
  }
}
