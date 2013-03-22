using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PwrIDE
{
  public class Local
  {
    //========================================================================
    public string Name;
    public string Path;
    public bool   Expanded = false;
    //------------------------------------------------------------------------
    public List<Project> Projects = new List<Project>();
    //========================================================================
    public Local(string Name, string Path)
    {
      this.Name = Name;
      this.Path = Path;
    }
    //------------------------------------------------------------------------
    public Local(string Name, string Path, bool Expanded)
    {
      this.Name     = Name;
      this.Path     = Path;
      this.Expanded = Expanded;
    }
    //========================================================================
    public void ProjectsLoad()
    {
    	Projects = new List<Project>();
      Regex regProj = new Regex(@"^PROJECT[^\n]+\n(FILE[^\n]+\n)*");
      string contents;
      try
      {
        contents = File.ReadAllText(Path + "\\pwrpls.projects");
      }
      catch(FileNotFoundException)
      {
        return; //ignore this, no projects for this user; but pass along any other exception types
      }
      catch(DirectoryNotFoundException)
      {
      	return; //same as above;
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
    public void ProjectsSave()
    {
      StringBuilder outp = new StringBuilder();
      for(int i=0; i<Projects.Count; i++)
        outp.Append(Projects[i].Write());
      outp.Append('\n');
      File.WriteAllText(Path+"\\pwrpls.projects", outp.ToString());
    }
    //------------------------------------------------------------------------
    public void ProjectAdd(Project prj)
    {
      for(int i=0; i<Projects.Count; i++)
        if(Projects[i].Name == prj.Name)
          throw new Exception("A Project Named \""+prj.Name+"\" Already Exists Here");
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
