using System;
using System.Text;
using System.Collections.Generic;
using Symitar;

namespace PwrIDE
{
  public class ProjectFile
  {
    //========================================================================
    //  structs/enums
    //========================================================================
    public enum FileType
    {
      PWRPLS = 0,
      REPGEN = 1,
      LETTER = 2
    }
    //------------------------------------------------------------------------
    public SymFile.Type TypeConvert()
    {
      return (Symitar.SymFile.Type)Enum.Parse(typeof(Symitar.SymFile.Type), Type.ToString());
    }
    //========================================================================
    //  members
    //========================================================================
    public string   Name;
    public FileType Type;
    public string   DisplayName { get { return Parent.FileDisplayName(this); } }
    public Project  Parent;
    //========================================================================
    //  constructors
    //========================================================================
    public ProjectFile(Project Parent, string Name, FileType Type)
    {
      this.Parent = Parent;
      this.Name   = Name;
      this.Type   = Type;
    }
    //========================================================================
    //  i/o - project proxy functions
    //========================================================================
    public string Read()                { return Parent.FileRead(this);    }
    public void   Write(string content) { Parent.FileWrite(this, content); }
    public void   Rename(string name)   { Parent.FileRename(this, name);   }
    public void   Delete()              { Parent.FileDelete(this);         }
    public void   Remove()              { Parent.FileRemove(this);         }
    //========================================================================
    public SymFile ToSymFile(SymInst inst)
    {
      return new SymFile(inst.Parent.IP, inst.SymDir, Name, DateTime.Now, 0, TypeConvert());
    }
    //========================================================================
  }
}
