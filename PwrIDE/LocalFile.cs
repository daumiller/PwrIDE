using System;
using System.IO;
using System.Text;
using Symitar;

namespace PwrIDE
{
	public class LocalFile
	{
    //========================================================================
		public string       name = "";
		public DateTime     date = DateTime.Now;
		public int          size = 0;
		public SymFile.Type type = SymFile.Type.REPGEN;
    //========================================================================
    public LocalFile()
    {
    }
    //------------------------------------------------------------------------
    public LocalFile(Local local, string filename)
    {
      FileInfo nfo = new FileInfo(local.Path + '\\' + filename);
      if(!nfo.Exists) throw new Exception("Tried to Construct LocalFile for Non-Existant File\nThis Shouldn't Happen");
      name = nfo.Name;
      date = nfo.LastWriteTime;
      size = (int)nfo.Length;
      if(name.Substring(name.LastIndexOf('.')+1).ToUpper() == "REP") type = SymFile.Type.REPGEN;
      else                                                           type = SymFile.Type.LETTER;
    }
    //------------------------------------------------------------------------
    public LocalFile(string Name, string Date, string Time, int Size, SymFile.Type Type)
    {
      name = Name;
      date = ParseDate(Date, Time);
      size = Size;
      type = Type;
    }
    //------------------------------------------------------------------------
    public LocalFile(string Name, DateTime Date, int Size, SymFile.Type Type)
    {
      name = Name;
      date = Date;
      size = Size;
      type = Type;
    }
    //========================================================================
		public DateTime ParseDate(string Date, string Time)
		{
			while(Time.Length < 4) Time='0'+Time;
			while(Date.Length < 8) Date='0'+Date;
			return new DateTime(
				int.Parse(Date.Substring(4,4)),
				int.Parse(Date.Substring(0,2)),
				int.Parse(Date.Substring(2,2)),
				int.Parse(Time.Substring(0,2)),
				int.Parse(Time.Substring(2,2)),
				0);
		}
    //------------------------------------------------------------------------
		public string TypeString()
		{
      return SymFile.TypeString(type);
		}
    //========================================================================
    public ProjectFile.FileType GetProjectFileType()
    {
      if(type == SymFile.Type.REPGEN)
        return ProjectFile.FileType.REPGEN;
      return ProjectFile.FileType.LETTER;
    }
    //========================================================================
	}
}
