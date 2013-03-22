using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ICSharpCode.TextEditor.Document;

namespace PwrIDE
{
  public class PwrPlusFold : IFoldingStrategy
  {
    //========================================================================
    private static Regex SectStart = new Regex(@"(^|\s+)(GLOBAL|SETUP|SELECT|ORDER|PROCESS|FINAL)(\s+|$)", RegexOptions.IgnoreCase);
    private static Regex ProcStart = new Regex(@"(^|\s+)(INT|BOOL|CHAR|DATE|MONEY|FLOAT|RATE|VOID)\s+([A-Z][A-Z0-9]*)\([^\)]*\)(\s+|$)", RegexOptions.IgnoreCase);
    private static Regex SectStop  = new Regex(@"(^|\s+)(END)(\s+|$)", RegexOptions.IgnoreCase);
    private static Regex ProcStop  = new Regex(@"(^|\s+)(RETURN)(\([^\)]+\))?(\s+|$)", RegexOptions.IgnoreCase);
    //========================================================================
    private static bool InQuote(string line, int pos)
    {
      bool ret=false;
      for(int i=0; i<pos; i++)
        if(line[i] == '"')
          ret = !ret;
      return ret;
    }
    //========================================================================
    public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
    {
    	Dictionary<string, int> procedures = (Dictionary<string, int>)parseInformation;
      List<FoldMarker> list = new List<FoldMarker>();
      Stack<int> linSect  = new Stack<int>();
      Stack<int> colSect  = new Stack<int>();
      Stack<int> linProc  = new Stack<int>();
      Stack<int> colProc  = new Stack<int>();
      Stack<int> linBlock = new Stack<int>();
      Stack<int> colBlock = new Stack<int>();
      bool inComm = false; //TODO: right now we don't have nested comment support in ics.te; so we're pretending they don't exist here either
      int cPos, ePos; string eStr, strSect="";

      Match match; int column;

      for(int i=0; i<document.TotalNumberOfLines; i++)
      {
        string line = document.GetText(document.GetLineSegment(i));
        
        //multi-line comments
        if(inComm)
        {
        	cPos = line.IndexOf("*/");
        	if(cPos == -1) continue;
        	line   = line.Substring(cPos+2);
        	inComm = false;
        }
        else
        {
        	cPos = line.IndexOf("/*");
        	if(cPos > -1)
        		if(!InQuote(line, cPos))
	        	{
	        		ePos = line.IndexOf("*/");
	        		eStr = (ePos != -1) ? line.Substring(ePos+2) : "";
	        		inComm = (ePos == -1);
	        		line = line.Substring(0,cPos) + eStr;
	        		if(line.Trim().Length==0) continue;
	        	}
        }
        //single-line comments
        cPos = line.IndexOf("//");
        if(cPos != -1)
        	if(!InQuote(line,cPos))
        	{
        		line = line.Substring(0,cPos);
        		if(line.Trim().Length==0) continue;
        	}
        //closing blocks
        if((linBlock.Count>0))
        {
          column = line.IndexOf('}');
          while((column > -1) && (linBlock.Count>0))
          {
            if(!InQuote(line, column))
              list.Add(new FoldMarker(document, linBlock.Pop(), colBlock.Pop(), i, column+1));
            column = line.IndexOf('}', column + 1);
          }
        }
        //closing procedures
        if((linProc.Count>0) && (line.Length>5))
        {
          match = ProcStop.Match(line);
          if(match.Success)
          {
            column = match.Groups[2].Index;
            if(!InQuote(line, column))
            {
            	if(!procedures.ContainsKey(strSect))
            		procedures.Add(strSect, linProc.Peek());
              column += match.Groups[2].Value.Length + match.Groups[3].Value.Length;
              list.Add(new FoldMarker(document, linProc.Pop(), colProc.Pop(), i, column));
            }
          }
        }
        //closing sections
        if((linSect.Count>0) && (line.Length>2))
        {
          match = SectStop.Match(line);
          if(match.Success)
          {
            column = match.Groups[2].Index+2;
            if(!InQuote(line, column))
            {
            	if(!procedures.ContainsKey(strSect))
            		procedures.Add(strSect, linSect.Peek());
              list.Add(new FoldMarker(document, linSect.Pop(), colSect.Pop(), i, column));
            }
          }
        }
        //opening sections
        if((linSect.Count==0) &&(linProc.Count==0))
        {
	        match = SectStart.Match(line);
	        if(match.Success)
	        {
	          column = match.Groups[2].Index;
	          if(!InQuote(line, column))
	          {
	            column += match.Groups[2].Length -1;
	            linSect.Push(i);
	            colSect.Push(column);
	            strSect = line.Substring(match.Groups[2].Index, match.Groups[2].Length).Trim();
	          }
	        }
	      }
	      //opening procedures
	      if((linProc.Count==0) && (linSect.Count==0))
	      {
	        match = ProcStart.Match(line);
	        if(match.Success)
	        {
	          column = match.Groups[2].Index;
	          if(!InQuote(line, column))
	          {
	            column += match.Value.TrimEnd().Length;
	            linProc.Push(i);
	            colProc.Push(column);
	            strSect = line.Substring(match.Groups[3].Index, match.Groups[3].Length).Trim();
	          }
	        }
	      }
        //opening blocks
        column = line.IndexOf('{');
        while(column > -1)
        {
          if(!InQuote(line, column))
          {
            linBlock.Push(i);
            colBlock.Push(column -1);
          }
          column = line.IndexOf('{', column+1);
        }
      }
      return list;
    }
    //========================================================================
  }
}
