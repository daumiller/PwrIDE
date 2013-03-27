using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Symitar;

namespace PwrIDE
{
  public static class Util
  {
    //========================================================================
    // project-wide frmMain referece
    //========================================================================
  	public static frmMain MainForm;
    //========================================================================
    //  sym Connections
    //========================================================================
    public static SymSession TrySymNewConnect(SymInst inst)
    {
      SymSession session = new SymSession();
      
  		bool prompt = false;
  		string user = inst.Parent.AixUsr;
  		string pass = inst.Parent.AixPwd;
  		string id   = inst.SymId;
  		if(user == "") prompt = true;
  		if(pass == "") prompt = true;
  		if(id   == "") prompt = true;
      bool success = false;

      if(!prompt)
      {
        success = session.Connect(inst.Parent.IP, inst.Parent.Port);
        if(success) success = session.Login(user, pass, inst.SymDir, id);
        while(!success)
        {
          if(MessageBox.Show("Error Connecting to Sym\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
            break;
          success = session.Connect(inst.Parent.IP, inst.Parent.Port);
          if(success) success = session.Login(user, pass, inst.SymDir, id);
        }
        if(!success) return null;
        return session;
      }

      while(!success)
      {
        frmLogin login = new frmLogin(inst.Parent.IP, inst.SymDir, user, pass, id);
        if(login.ShowDialog(MainForm) == DialogResult.Cancel)
          break;
        inst.Parent.AixUsr = user = login.User;
        inst.Parent.AixPwd = pass = login.Pass;
        inst.SymId         = id   = login.ID;
        login.Dispose();
        success = inst.Connect();
        if(success) success = session.Login(user, pass, inst.SymDir, id);
        if(!success)
          if(MessageBox.Show("Error Connecting to Sym\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
            break;
      }
      if(!success) return null;
      return session;
    }
    //------------------------------------------------------------------------
  	public static bool TrySymConnect(SymInst inst)
  	{
      if(inst.Connected())
        return true;

  		bool prompt = false;
  		string user = inst.Parent.AixUsr;
  		string pass = inst.Parent.AixPwd;
  		string id   = inst.SymId;
  		if(user == "") prompt = true;
  		if(pass == "") prompt = true;
  		if(id   == "") prompt = true;
      bool success = false;

      if(!prompt)
      {
        success = inst.Connect();
        while(!success)
        {
          if(MessageBox.Show("Error Connecting to Sym\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
            break;
          success = inst.Connect();
        }
        return success;
      }

      while(!success)
      {
        frmLogin login = new frmLogin(inst.Parent.IP, inst.SymDir, user, pass, id);
        if(login.ShowDialog(MainForm) == DialogResult.Cancel)
          break;
        inst.Parent.AixUsr = user = login.User;
        inst.Parent.AixPwd = pass = login.Pass;
        inst.SymId         = id   = login.ID;
        login.Dispose();
        success = inst.Connect();
        if(!success)
          if(MessageBox.Show("Error Connecting to Sym\nRetry?", "PwrIDE", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
            break;
      }
      return success;
  	}
    //------------------------------------------------------------------------
  	public static void DisconnectAll()
  	{
  		for(int i=0; i<Config.Servers.Count; i++)
  			Config.Servers[i].Disconnect();
  	}
    //========================================================================
    //  file i/o - Local
    //========================================================================
    public static void FileExistsTestLocal(string path)
    {
      FileInfo nfo = new FileInfo(path);
      if (!nfo.Exists)
        throw new FileNotFoundException("File \""+path+"\" Not Found");
    }
    //------------------------------------------------------------------------
    public static bool FileExistsLocal(string path)
    {
      FileInfo nfo = new FileInfo(path);
      return nfo.Exists;
    }
    //------------------------------------------------------------------------
    public static string FileBaseNameLocal(string path)
    {
      FileInfo nfo = new FileInfo(path);
      if(!nfo.Exists)
        throw new FileNotFoundException("File \""+path+"\" Not Found");
      return nfo.Name;
    }
    //------------------------------------------------------------------------
    public static string FileDirectoryPathLocal(string path)
    {
      FileInfo nfo = new FileInfo(path);
      if (!nfo.Exists)
        throw new FileNotFoundException("File \"" + path + "\" Not Found");
      return nfo.DirectoryName;
    }
    //------------------------------------------------------------------------
    public static string FileReadLocal(string path)
    {
      FileExistsTestLocal(path);
      return File.ReadAllText(path);
    }
    //------------------------------------------------------------------------
    public static void FileWriteLocal(string path, string content)
    {
      //FileExistsTestLocal(path);
      File.WriteAllText(path, content);
    }
    //------------------------------------------------------------------------
    public static void FileRenameLocal(string path, string name)
    {
      FileInfo nfo = new FileInfo(path);
      if(!nfo.Exists) throw new FileNotFoundException("File \"" + path + "\" Not Found");
      File.Move(path, nfo.DirectoryName + '\\' + name);
    }
    //------------------------------------------------------------------------
    public static void FileDeleteLocal(string path)
    {
      File.Delete(path);
    }
    //------------------------------------------------------------------------
    public static List<LocalFile> FileListLocal(Local local, string pattern)
    {
      List<LocalFile> files = new List<LocalFile>();
      Regex filter = new Regex(pattern.Replace(".","\\.").Replace("$","\\$").Replace("(","\\(").Replace(")","\\)").Replace("[","\\[").Replace("]","\\]").Replace("+", ".+"));
      try
      {
        string[] allFiles = Directory.GetFiles(local.Path);
        for (int i = 0; i < allFiles.Length; i++)
          if (filter.IsMatch(allFiles[i]))
            files.Add(new LocalFile(local, FileBaseNameLocal(allFiles[i])));
      }
      catch(Exception) { }
      return files;
    }
    //========================================================================
    //  file i/o - Sym
    //========================================================================
    public static bool FileExistsSym(SymInst inst, string name, SymFile.Type type)
    {
      if (!TrySymConnect(inst))
        return false;
      return inst.Connection.FileExists(name, type);
    }
    public static bool FileExistsSym(SymInst inst, SymFile file) { return FileExistsSym(inst, file.name, file.type); }
    //------------------------------------------------------------------------
    public static string FileReadSym(SymInst inst, string name, SymFile.Type type)
    {
      if(!TrySymConnect(inst))
        throw new Exception("Not Connected to Sym");
      return inst.Connection.FileRead(name, type);
    }
    public static string FileReadSym(SymInst inst, SymFile file) { return FileReadSym(inst, file.name, file.type); }
    //------------------------------------------------------------------------
    public static void FileWriteSym(SymInst inst, string name, SymFile.Type type, string content)
    {
      if(!TrySymConnect(inst))
        throw new Exception("Not Connected to Sym");
      inst.Connection.FileWrite(name, type, content);
    }
    public static void FileWriteSym(SymInst inst, SymFile file, string content) { FileWriteSym(inst, file.name, file.type, content); }
    //------------------------------------------------------------------------
    public static void FileRenameSym(SymInst inst, string namePrev, SymFile.Type type, string nameNew)
    {
      if(!TrySymConnect(inst))
        throw new Exception("Not Connected to Sym");
      inst.Connection.FileRename(namePrev, type, nameNew);
    }
    public static void FileRenameSym(SymInst inst, SymFile file, string nameNew) { FileRenameSym(inst, file.name, file.type, nameNew); }
    //------------------------------------------------------------------------
    public static void FileDeleteSym(SymInst inst, string name, SymFile.Type type)
    {
      if(!TrySymConnect(inst))
        throw new Exception("Not Connected to Sym");
      inst.Connection.FileDelete(name, type);
    }
    public static void FileDeleteSym(SymInst inst, SymFile file) { FileDeleteSym(inst, file.name, file.type); }
    //------------------------------------------------------------------------
    public static List<SymFile> FileListSym(SymInst inst, string pattern, SymFile.Type type)
    {
      if(!TrySymConnect(inst))
        throw new Exception("Not Connected to Sym");
      return inst.Connection.FileList(pattern, type);
    }
    //========================================================================
    //  general helpers
    //========================================================================
    public static string FormatBytes(int inp)
    {
      float size = (float)inp;
      int magnitude = 0;
      while (size > 1024.0f)
      {
        size /= 1024.0f;
        magnitude++;
      }
      string unit = " B";
           if(magnitude == 1) unit=" KB";
      else if(magnitude == 2) unit=" MB";
      else if(magnitude == 3) unit=" GB";
      return size.ToString("#,##0.##") + unit;
    }
    //------------------------------------------------------------------------
    public static string FormatBytes(string inp) { return FormatBytes(int.Parse(inp)); }
    //========================================================================
    public static ProjectFile.FileType GetSymProjectFileType(SymFile file) //this is here so we don't have to reference PwrIDE from Symitar
    {
      if(file.type == SymFile.Type.REPGEN)
        return ProjectFile.FileType.REPGEN;
      return ProjectFile.FileType.LETTER;
    }
    //========================================================================
    private static uint HH_DISPLAY_TOPIC  =  0;
    private static uint HH_DISPLAY_TOC    =  1;
    private static uint HH_DISPLAY_INDEX  =  2;
    private static uint HH_DISPLAY_SEARCH =  3;
    private static uint HH_KEYWORD_LOOKUP = 13;
    private static uint HH_HELP_CONTEXT   = 15;
    private static uint HH_CLOSE_ALL      = 18;
    [DllImport("hhctrl.ocx")] private static extern IntPtr HtmlHelp(IntPtr hwnd, string file, UInt32 command, IntPtr data);
    public static void ShowEDocs()
    {
      string helpFile = Config.GetString("Help_File");
      if(File.Exists(helpFile))
        HtmlHelp(IntPtr.Zero, helpFile, HH_DISPLAY_TOPIC, IntPtr.Zero);
    }
    public static void SearchEDocs()
    {

    }

  }
}
