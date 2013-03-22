using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace PwrIDE
{
  public class Project
  {
    //========================================================================
    //  members
    //========================================================================
    public string            Name;
    public bool              Local;
    public Local             ParentLocal = null;
    public SymInst           ParentSym   = null;
    public List<ProjectFile> Files       = new List<ProjectFile>();
    //------------------------------------------------------------------------
    private static Regex RegProj = new Regex(@"^PROJECT\s(.+)$");
    private static Regex RegFile = new Regex(@"^FILE\s([A-Z]+)\s(.+)$");
    //========================================================================
    //  constructors
    //========================================================================
    public Project()
    {
      Name        = "";
      Local       = true;
    }
    //------------------------------------------------------------------------
    public Project(string inp, Local parent)
    {
      Local       = true;
      ParentLocal = parent;
      Read(inp);
    }
    //------------------------------------------------------------------------
    public Project(string inp, SymInst parent)
    {
      Local       = false;
      ParentSym   = parent;
      Read(inp);
    }
    //------------------------------------------------------------------------
    public static Project CreateProject(string name, Local parent)
    {
      Project prj     = new Project();
      prj.Name        = name;
      prj.Local       = true;
      prj.ParentLocal = parent;
      parent.ProjectAdd(prj);
      return prj;
    }
    //------------------------------------------------------------------------
    public static Project CreateProject(string name, SymInst parent)
    {
      Project prj   = new Project();
      prj.Name      = name;
      prj.Local     = false;
      prj.ParentSym = parent;
      parent.ProjectAdd(prj);
      return prj;
    }
    //========================================================================
    // adding sources
    //========================================================================
    public ProjectFile AddCompiledSource(ProjectFile original, string compiled)
    {
      string newName;
      int lastDot = original.Name.LastIndexOf('.');
      if (lastDot == -1)
        newName = original.Name + ".REP";
      else
      {
        string guessBase = original.Name.Substring(0, lastDot);
        string guessExt  = original.Name.Substring(lastDot + 1).ToUpper();
        newName = (guessExt == "PWR") ? guessBase + ".REP" : original.Name + ".REP";
      }
      
      ProjectFile returnFile = new ProjectFile(this, newName, ProjectFile.FileType.REPGEN);
      bool overwrite = false;
      for (int i = 0; i < Files.Count; i++)
      {
        if (Files[i].Name == newName)
        {
          overwrite = true;
          returnFile = Files[i];
        }
      }
      try
      {
        FileWrite(returnFile, compiled);
      }
      catch(Exception ex)
      {
        System.Windows.Forms.MessageBox.Show("Error Adding Compiled Source to Project\n"+ex.Message, "PwrIDE", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        return null;
      }
      if(!overwrite)
      {
      	Files.Add(returnFile);
      	Util.MainForm.Explorer.ProjectAddCompiled(this, returnFile);
      }
      return returnFile;
    }
    //========================================================================
    // file i/o
    //========================================================================
    public string FileRead(ProjectFile file)
    {
      if(Local)
        return Util.FileReadLocal(ParentLocal.Path+'\\'+file.Name);
      else
        return Util.FileReadSym(ParentSym, file.Name, file.TypeConvert());
    }
    //------------------------------------------------------------------------
    public void FileWrite(ProjectFile file, string content)
    {
      if(Local)
        Util.FileWriteLocal(ParentLocal.Path+'\\'+file.Name, content);
      else
        Util.FileWriteSym(ParentSym, file.Name, file.TypeConvert(), content);
    }
    //------------------------------------------------------------------------
    public void FileRename(ProjectFile file, string name)
    {
      if(Local)
        Util.FileRenameLocal(ParentLocal.Path+'\\'+file.Name, name);
      else
        Util.FileRenameSym(ParentSym, file.Name, file.TypeConvert(), name);
    	file.Name = name;
    }
    //------------------------------------------------------------------------
    public void FileDelete(ProjectFile file)
    {
      Files.Remove(file);
      if(Local)
        Util.FileDeleteLocal(ParentLocal.Path + '\\' + file.Name);
      else
        Util.FileDeleteSym(ParentSym, file.Name, file.TypeConvert());
    }
    //------------------------------------------------------------------------
    public void FileDeleteAll()
    {
      while(Files.Count>0)
        FileDelete(Files[0]);
    }
    //------------------------------------------------------------------------
    public void FileAdd(string Name, ProjectFile.FileType Type)
    {
      string UpperName = Name.ToUpper();
      for(int i=0; i<Files.Count; i++)
        if((Files[i].Name.ToUpper() == UpperName) && (Files[i].Type == Type))
        {
          MessageBox.Show("File '"+Name+"' Already Exists in Project '"+this.Name+"'", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      Files.Add(new ProjectFile(this, Name, Type));
    }
    //------------------------------------------------------------------------
    public void FileRemove(ProjectFile file)
    {
      Files.Remove(file);
    }
    //------------------------------------------------------------------------
    public string FileDisplayName(ProjectFile file)
    {
      if(Local) return file.Name;
      return '['+ParentSym.SymDir+"] "+file.Name;
    }
    //------------------------------------------------------------------------
    public ProjectFile FileCreate(string name, ProjectFile.FileType fileType, Symitar.SymFile.Type symType)
    {
      if(Local)
      {
        Util.FileWriteLocal(ParentLocal.Path+'\\'+name, "");
        ProjectFile file = new ProjectFile(this, name, fileType);
        string upperName = name.ToUpper();
        for(int i=0; i<Files.Count; i++)
        	if((Files[i].Name.ToUpper() == upperName) && (Files[i].Type == fileType))
        		return file;
        Files.Add(file);
        return file;
      }
      else
      {
        Util.FileWriteSym(ParentSym, name, symType, "");
        ProjectFile file = new ProjectFile(this, name, fileType);
        string upperName = name.ToUpper();
        for(int i=0; i<Files.Count; i++)
        	if((Files[i].Name.ToUpper() == upperName) && (Files[i].Type == fileType))
        		return file;
        Files.Add(file);
        return file;
      }
    }
    //========================================================================
    // to/from project file listings
    //========================================================================
    public string Write()
    {
      StringBuilder outp = new StringBuilder();
      outp.Append("PROJECT "+Name+'\n');
      for(int i=0; i<Files.Count; i++)
        outp.Append("FILE "+Files[i].Type.ToString()+' '+Files[i].Name+'\n');
      outp.Append('\n');
      return outp.ToString();
    }
    //------------------------------------------------------------------------
    private void Read(string inp)
    {
      inp = inp.Replace("\r", "");
      string[] lines = inp.Split(new char[] { '\n' });
      Match match = RegProj.Match(lines[0].Trim());
      Name = match.Groups[1].Value;
      for(int i=1; i<lines.Length; i++)
      {
        if(lines[i].Trim().Length==0) break;
        match = RegFile.Match(lines[i].Trim());
        Files.Add(new ProjectFile(this, match.Groups[2].Value, (ProjectFile.FileType)Enum.Parse(typeof(ProjectFile.FileType), match.Groups[1].Value)));
      }
    }
    //========================================================================
    public ProjectFile GetFile(string filename)
    {
    	for(int i=0; i<Files.Count; i++)
    		if(Files[i].Name.ToUpper() == filename.ToUpper())
    			return Files[i];
    	return null;
    }
    //========================================================================
    // Copy this Project to
    //========================================================================
    public bool CopyTo(Local locl)
    {
    	if(Local) if(ParentLocal==locl) return false;
    	string upperName = this.Name.ToUpper();
    	for(int i=0; i<locl.Projects.Count; i++) if(locl.Projects[i].Name.ToUpper()==upperName)
    	{
    		MessageBox.Show("A Project Named '"+this.Name+"' Already Exists Here", "PwrIDE - Copy Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
    		return false;
    	}
    	Project copyOf = CreateProject(this.Name, locl);
    	for(int i=0; i<this.Files.Count; i++)
    	{
    		try
    		{
    			ProjectFile curr = copyOf.FileCreate(this.Files[i].Name, this.Files[i].Type, this.Files[i].TypeConvert());
    			curr.Write(this.Files[i].Read());
    		}
    		catch(Exception ex)
    		{
    			MessageBox.Show("Error Copying File '"+this.Files[i].Name+"' from Project '"+this.Name+"'\nError: "+ex.Message, "PwrIDE - Copy Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
    		}
    	}
    	try
    	{
    		locl.ProjectsSave();
    	}
    	catch(Exception ex)
    	{
    		MessageBox.Show("Error Saving Projects Listing for Local '"+locl.Name+"'\nError: "+ex.Message, "PwrIDE - Copy Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
    		return false;
    	}
    	return true;
    }
    //------------------------------------------------------------------------
    public bool CopyTo(SymInst sym)
    {
    	if(!Local) if(ParentSym==sym) return false;
    	string upperName = this.Name.ToUpper();
    	for(int i=0; i<sym.Projects.Count; i++) if(sym.Projects[i].Name.ToUpper()==upperName)
    	{
    		MessageBox.Show("A Project Named '"+this.Name+"' Already Exists Here", "PwrIDE - Copy Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
    		return false;
    	}
    	Project copyOf = CreateProject(this.Name, sym);
    	for(int i=0; i<this.Files.Count; i++)
    	{
    		try
    		{
    			ProjectFile curr = copyOf.FileCreate(this.Files[i].Name, this.Files[i].Type, this.Files[i].TypeConvert());
    			curr.Write(this.Files[i].Read());
    		}
    		catch(Exception ex)
    		{
    			MessageBox.Show("Error Copying File '"+this.Files[i].Name+"' from Project '"+this.Name+"'\nError: "+ex.Message, "PwrIDE - Copy Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
    		}
    	}
    	try
    	{
    		sym.ProjectsSave();
    	}
    	catch(Exception ex)
    	{
    		MessageBox.Show("Error Saving Projects Listing for Sym '"+sym.SymDir+"'\nError: "+ex.Message, "PwrIDE - Copy Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
    		return false;
    	}
    	return true;
    }
    //========================================================================
  }
}
