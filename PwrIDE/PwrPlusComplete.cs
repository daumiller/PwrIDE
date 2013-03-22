using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace PwrIDE
{
  public class PwrPlusComplete : ICompletionDataProvider
  {
    //========================================================================
    public static ImageList ImgList;
    public static Dictionary<string, Dictionary<string, string>> Records;
    //========================================================================
    public static void LoadRecords(string contents)
    {
      Records = new Dictionary<string, Dictionary<string, string>>(StringComparer.InvariantCultureIgnoreCase);

      Regex regRecord = new Regex(@"^\*\*\*\|([^\|]+)\|([^\|]+)");
      Regex regField  = new Regex(@"^\t([^\|]+)\|([^\|]+)\|([^\|]+)\|([^\|]+)\|([^\|]+)");

      contents = contents.Replace("\r", "");
      string[] lines = contents.Split(new char[] { '\n' });
      
      int i=0;
      while(i < lines.Length)
      {
        Match match = regRecord.Match(lines[i]);
        if(match.Success)
        {
          string valu = match.Groups[1].Value;
          string desc = match.Groups[2].Value;
          Records.Add(valu.ToUpper(), LoadFields(regField, lines, ref i));
        }
        else
          i++;
      }
    }
    //------------------------------------------------------------------------
    private static Dictionary<string, string> LoadFields(Regex reg, string[] lines, ref int index)
    {
      Dictionary<string, string> ret = new Dictionary<string, string>();
      string valu, desc, field, type, max;
      
      index++;
      Match match = reg.Match(lines[index]);
      while (match.Success)
      {
        valu  = match.Groups[1].Value;
        desc  = match.Groups[2].Value;
        field = match.Groups[3].Value;
        type  = match.Groups[4].Value;
        max   = match.Groups[5].Value;
        field = "\nField: " + field;
        if(max == "null") max=""; else max="\nLen/Max: "+max;
        switch(type)
        {
          case  "0": type="\nType: char";      break;
          case  "1": type="\nType: char";      break;
          case  "2": type="\nType: rate";      break;
          case  "3": type="\nType: date";      break;
          case  "4": type="\nType: int";       break;
          case  "5": type="\nType: int";       break;
          case  "6": type="\nType: money";     break;
          case  "7": type="\nType: money";     break;
          case  "8": type="\nType: bool";      break;
          case  "9": type="\nType: undefined"; break;
          case "10": type="\nType: float";     break;
          default:  type="\nType: undefined";  break;
        }
        desc += field + type + max;
        ret.Add(valu, desc);
        
        index++;
        match = reg.Match(lines[index]);
      }
      return ret;
    }
    //========================================================================
    public ImageList ImageList
    {
      get { return ImgList; }
    }
    //------------------------------------------------------------------------
    public string PreSelection
    {
      get { return null; }
    }
    //------------------------------------------------------------------------
    public int DefaultIndex
    {
      get { return -1; }
    }
    //========================================================================
    public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
    {
      List<ICompletionData> icd = new List<ICompletionData>();
      int lineOffset  = textArea.Document.GetLineSegment(textArea.Caret.Line).Offset;
      int colonOffset = textArea.Caret.Offset;
      string line = textArea.Document.GetText(lineOffset, colonOffset-lineOffset).ToUpper();
      
      string[] words = line.Split(new char[]{' ','\t','=','+','-','*','/','(',')',',','{','}',';',':'});
      for(int i=0; i<words.Length; i++)
      {
        string combined = "";
        for(int a=i; a<words.Length; a++)
          combined += (a>i?" ":"") + words[a];
        if(Records.ContainsKey(combined))
        {
          foreach(KeyValuePair<string, string> field in Records[combined])
            icd.Add(new DefaultCompletionData(field.Key, field.Value, 0));
          break;
        }
      }

      return icd.ToArray();
    }
    //========================================================================
    public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
    {
      textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
      return data.InsertAction(textArea, key);
    }
    //========================================================================
    public CompletionDataProviderKeyResult ProcessKey(char key)
    {
      return CompletionDataProviderKeyResult.NormalKey;
    }
    //========================================================================
  }
}
