using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PwrPlus
{
  public static class PreProcessor
  {
    //========================================================================
  	public delegate string Import(string filename);
    //========================================================================
  	private static Regex RegImport = new Regex("^#IMPORT\\s+\"([^\"]+)\"");
    //========================================================================
  	public static string PreProcess(string source, string filename, Import importer)
  	{
  		List<string>   impName    = new List<string>();
      List<string[]> impContent = new List<string[]>();

      //find #IMPORT Directives
			int i=0;
      string[] lines = SplitAndComment(source, filename);
      string curr = lines[i].TrimStart();
  		while((curr.Length==0) || (curr[0]=='#'))
  		{
  			Match testRI = RegImport.Match(lines[i].ToUpper());
  			if(!testRI.Success)
  			{
  				PwrError.SetFatal("Invalid # Preprocessor Directive", i, 0x040); 
  				return null;
  			}
  			else
        {
  				impName.Add(testRI.Groups[1].Value);
          lines[i] = lines[i].Replace(testRI.Value, "");
        }

        i++;
        if(i >= lines.Length) break;
        curr = lines[i].TrimStart();
  		}

      //none found?
  		if(impName.Count == 0)
  			return CombineLines(lines);
  		
      //use callback to load Imported files
      for(i=0; i<impName.Count; i++)
      {
        string fullSrc = importer(impName[i]);
        if(fullSrc == null) //file wasn't found
        {
          PwrError.SetFatal("Couldn't Load Include File "+impName[i], 0, 0x041);
          return null;
        }
        impContent.Add(SplitAndComment(fullSrc, impName[i]));
      }

      //combine our imported sections
      StringBuilder sbImpGlob = new StringBuilder();
      StringBuilder sbImpProc = new StringBuilder();
      for(i=0; i<impContent.Count; i++)
      {
        int x;
        int globStart = -1;
        int globEnd   = -1;
        for(x=0; x<impContent[i].Length; x++)
        {
          if(impContent[i][x].TrimStart().Substring(0,6).ToUpper() == "GLOBAL")
          {
            globStart=x;
            break;
          }
        }
        if(globStart == -1) //no global section found, just append this text
          sbImpProc.Append(CombineLines(impContent[i]));
        else
        {
          for(x=globStart; x<impContent[i].Length; x++)
          {
            if(impContent[i][x].TrimStart().Substring(0,3).ToUpper() == "END")
            {
              globEnd = x;
              break;
            }
          }
          if(globEnd == -1)
          {
            PwrError.SetFatal("Unterminated Global Block in File "+impName[i], 0, 0x042);
            return null;
          }
          for(x=globStart+1; x<globEnd; x++)
            sbImpGlob.Append(impContent[i][x]);
          for(x=globEnd+1; x<impContent[i].Length; x++)
            sbImpProc.Append(impContent[i][x]);
        }
      }

      string strImpGlob = sbImpGlob.ToString();
      string strImpProc = sbImpProc.ToString();

      //simple case
      if(strImpGlob.Length == 0)
        return CombineLines(lines) + strImpProc;

      //find our insertion point
      int insertionPoint = -1;
      Regex RootProc = new Regex(@"^(GLOBAL|SETUP|SORT|PROCESS|FINAL)");
      for(i=0; i<lines.Length; i++)
      {
        lines[i] = lines[i].TrimStart();
        if(lines[i].Length > 0)
          if(RootProc.Match(lines[i].ToUpper()).Success)
            insertionPoint = i;
        if(insertionPoint > -1)
          break;
      }
      if(insertionPoint == -1)
      {
        PwrError.SetFatal("Couldn't Find Insertion Point for Imported Globals", 0, 0x043);
        return null;
      }

      StringBuilder strFinal = new StringBuilder();
      for (i = 0; i < insertionPoint; i++)
        strFinal.Append(lines[i]);

      if(lines[insertionPoint].Substring(0,6).ToUpper() == "GLOBAL")
      {
        strFinal.Append(lines[insertionPoint]);
        strFinal.Append(strImpGlob);
        insertionPoint++;
      }
      else
      {
        strFinal.Append("GLOBAL//@PWRPLUSLINE@"+filename+"::0\n");
        strFinal.Append(strImpGlob);
        strFinal.Append("END//@PWRPLUSLINE@"   +filename+"::0\n");
      }

      for(i=insertionPoint; i<lines.Length; i++)
        strFinal.Append(lines[i]);
      strFinal.Append(strImpProc);

      return strFinal.ToString();
  	}
    //========================================================================
    public static string[] SplitAndComment(string src, string filename)
    {
      string[] ret = src.Replace("\r","").Split(new char[]{'\n'});
      for(int i=0; i<ret.Length; i++)
        ret[i] += "//@PWRPLUSLINE@"+filename+"::"+i.ToString()+'\n';
      return ret;
    }
    //========================================================================
    public static string CombineLines(string[] src)
    {
      StringBuilder sb = new StringBuilder();
      for(int i=0; i<src.Length; i++)
        sb.Append(src[i]);
      return sb.ToString();
    }
    //========================================================================
  }
}
