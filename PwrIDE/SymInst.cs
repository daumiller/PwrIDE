using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Symitar;

namespace PwrIDE
{
	public class SymInst
	{
    //========================================================================
		public SymServer Parent;
    public SymSession Connection;
    public string SymDir;
		public string SymId;
    public bool   Remember;
    //------------------------------------------------------------------------
    public List<Project> Projects = new List<Project>();
    //========================================================================
    public SymInst(SymServer Parent)
    {
      this.Parent     = Parent;
      this.SymDir     = "";
      this.SymId      = "";
      this.Connection = null;
      this.Remember   = true;
    }
		public SymInst(SymServer Parent, string SymDir, string SymId, bool Remember)
		{
			this.Parent     = Parent;
			this.SymDir     = SymDir;
			this.SymId      = SymId;
      this.Connection = null;
      this.Remember   = Remember;
		}
    //========================================================================
    public string GetUserNumber()
    {
      string working = SymId.Trim();
      if(working.Length==0) return "000";
      return int.Parse(new Regex(@"[0-9]+").Match(working).Value).ToString("D3");
    }
    //========================================================================
		public bool Connect()
		{
			if(Connection != null)
				if(Connection.isLoggedIn)
					return true;

			Connection = new SymSession();
			if(!Connection.Connect(Parent.IP, Parent.Port)) return false;
			if(Connection.Login(Parent.AixUsr, Parent.AixPwd, SymDir, SymId)==false)
			{
				Connection.Disconnect();
				Connection = null;
				return false;
			}
			return true;
		}
		//========================================================================
		public void Disconnect()
		{
			if(Connection == null)
				return;
			Connection.Disconnect();
			Connection = null;
		}
    //========================================================================
		public bool Connected()
		{
			if(Connection == null)
				return false;
			if(Connection.LockTest()) return Connection.isLoggedIn;
			if(Connection.LockTest()) return Connection.isLoggedIn;
			Disconnect();
			return false;
		}
    //========================================================================
		public string Error()
		{
			if(Connection == null)
				return "";
			return Connection.error;
		}
    //========================================================================
    public void ProjectsLoad()
    {
    	Projects = new List<Project>();
      Regex regProj = new Regex(@"^PROJECT[^\n]+\n(FILE[^\n]+\n)*");
      string contents;
      try
      {
        contents = Util.FileReadSym(this, "pwrpls."+GetUserNumber()+"projects",SymFile.Type.LETTER);
        if(contents.Length >= 0) contents=contents.Replace("\n","\r\n");
      }
      catch(FileNotFoundException)
      {
        return; //ignore this, no projects for this user; but pass along any other exception types
      }

      while(contents.Length > 0)
      {
        contents = contents.TrimStart();
        if(contents.Length == 0) break;
        string projTxt = regProj.Match(contents).Value;
        contents = contents.Replace(projTxt, "");
        Projects.Add(new Project(projTxt, this));
        if(contents.Trim().Length == 0) break;
      }
    }
    //------------------------------------------------------------------------
    public List<Project> ProjectsImport()
    {
      List<Project> returns = new List<Project>();

    	Regex regProj = new Regex(@"^PROJECT[^\n]+\n(FILE[^\n]+\n)*");
      string contents;
      try
      {
        contents = Util.FileReadSym(this, "repdev."+GetUserNumber()+"projects",SymFile.Type.REPGEN);
        if(contents.Length >= 0) contents=contents.Replace("\n","\r\n");
      }
      catch(FileNotFoundException)
      {
        return null; //ignore this, no projects for this user; but pass along any other exception types
      }

      while(contents.Length > 0)
      {
        contents = contents.TrimStart();
        if(contents.Length == 0) break;
        string projTxt = regProj.Match(contents).Value;
        contents = contents.Replace(projTxt, "");
        returns.Add(new Project(projTxt, this));
        if(contents.Trim().Length == 0) break;
      }
      
      return returns;
    }
    //------------------------------------------------------------------------
    public void ProjectsSave()
    {
      StringBuilder outp = new StringBuilder();
      for (int i = 0; i < Projects.Count; i++)
        outp.Append(Projects[i].Write());
      outp.Append('\n');
      Util.FileWriteSym(this, "pwrpls." + GetUserNumber() + "projects", SymFile.Type.LETTER, outp.ToString());
    }
    //------------------------------------------------------------------------
    public void ProjectAdd(Project prj)
    {
      for (int i = 0; i < Projects.Count; i++)
        if (Projects[i].Name == prj.Name)
          throw new Exception("A Project Named \"" + prj.Name + "\" Already Exists Here");
      Projects.Add(prj);
    }
    //------------------------------------------------------------------------
    public void ProjectRemove(Project prj)
    {
      Projects.Remove(prj);
    }
    //------------------------------------------------------------------------
    public void ProjectDelete(Project prj)
    {
      prj.FileDeleteAll();
      Projects.Remove(prj);
    }
    //========================================================================
	}
}
