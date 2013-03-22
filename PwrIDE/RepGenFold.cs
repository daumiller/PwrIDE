using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ICSharpCode.TextEditor.Document;

namespace PwrIDE
{
  public class RepGenFold : IFoldingStrategy
  {
    //========================================================================
    private static Regex SectStart  = new Regex(@"(^|\s+)(DEFINE|SETUP|SELECT|SORT|PRINT|TOTAL|PROCEDURE)(\s+|$)", RegexOptions.IgnoreCase);
    private static Regex BlockStart = new Regex(@"(^|\s+)(HEADERS|TRAILERS|DO)(\s+|$)", RegexOptions.IgnoreCase);
    private static Regex BlockStop  = new Regex(@"(^|\s+)(END)(\s+|$)", RegexOptions.IgnoreCase);
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
      Stack<int> startLin = new Stack<int>();
      Stack<int> startCol = new Stack<int>();
      int startSect = 0; bool inSect = false; string strSect = "";
      bool inComm = false; //TODO: right now we don't have nested comment support in ics.te; so we're pretending they don't exist here either
      int cPos, ePos; string eStr;

      Match match; int column;

      for(int i=0; i<document.TotalNumberOfLines; i++)
      {
        string line = document.GetText(document.GetLineSegment(i));
        
        //comment processing
        if(inComm)
        {
        	cPos = line.IndexOf(']');
        	if(cPos == -1) continue;
        	line   = line.Substring(cPos+1);
        	inComm = false;
        }
        else
        {
        	cPos = line.IndexOf('[');
        	if(cPos > -1)
        		if(!InQuote(line, cPos))
	        	{
	        		ePos = line.IndexOf(']');
	        		eStr = (ePos != -1) ? line.Substring(ePos+1) : "";
	        		inComm = (ePos == -1);
	        		line = line.Substring(0,cPos) + eStr;
	        		if(line.Trim().Length==0) continue;
	        	}
        }
        
        //match "END"s first, so we can have an "END ELSE DO" double block on a single line
        if((startLin.Count>0) && (line.Length>2))
        {
          match = BlockStop.Match(line);
          if(match.Success)
          {
            column = match.Groups[2].Index+2;
            if(!InQuote(line, column))
            {
            	if(inSect)
            		if(startLin.Count==startSect)
            		{
            			inSect = false;
            			if(!procedures.ContainsKey(strSect))
            				procedures.Add(strSect, startLin.Peek());
            		}
              list.Add(new FoldMarker(document, startLin.Pop(), startCol.Pop(), i, column));
            }
          }
        }
        if(!inSect)
        {
        	match = SectStart.Match(line);
        	if(match.Success)
	        {
	          column = match.Groups[2].Index;
	          if(!InQuote(line, column))
	          {
	            if(match.Groups[2].Value.ToUpper() == "PROCEDURE")
	              column = line.Length-1;
	            else
	              column += match.Groups[2].Length-1;
	
	            startLin.Push(i);
	            startCol.Push(column);
	            
	            inSect = true;
	        		startSect = startLin.Count;
	        		strSect = line.Substring(match.Groups[2].Index, column - match.Groups[2].Index + 1);
	        		if(match.Groups[2].Value.ToUpper() == "PROCEDURE")
	        			strSect = strSect.Replace(match.Groups[2].Value,"");
	          }
	        }
	      }
        else
        {
          match = BlockStart.Match(line);
          if(match.Success)
          {
            column = match.Groups[2].Index;
            if(match.Groups[2].Value.Length != 2) column+=match.Groups[2].Value.Length;
            if(!InQuote(line, column))
            {
              startLin.Push(i);
              startCol.Push(column);
            }
          }
        }
      }
      return list;
    }
    //========================================================================
  }
}
