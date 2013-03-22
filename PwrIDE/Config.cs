using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace PwrIDE
{
  static class Config
  {
    //========================================================================
    public delegate void UpdateHandler();
    //========================================================================
    private static Dictionary<string, string> data = new Dictionary<string, string>();
    private static List<UpdateHandler> RegisteredUpdateHandlers = new List<UpdateHandler>();
    public static List<SymServer> Servers = new List<SymServer>();
    public static List<Local>     Locals  = new List<Local>();
    //========================================================================
    public static void Init()
    {
    	//initialize to defaults
      data.Add("Default_Queue"   , "-1");
      data.Add("Window_Width"    , "512");
      data.Add("Window_Height"   , "512");
      data.Add("Window_Top"      , "0");
      data.Add("Window_Left"     , "0");
      data.Add("Window_Maximized", "False");

      data.Add("Explorer_State" , "DockRightAutoHide");
      data.Add("Explorer_Size"  , "320");
      data.Add("Errors_State"   , "DockBottomAutoHide");
      data.Add("Errors_Size"    , "192");

      data.Add("Report_Width" , "670");
      data.Add("Report_Height", "600");
      data.Add("Report_Split" , "82");

      data.Add("Editor_Letter_Font"     , "Courier New");
      data.Add("Editor_Letter_Size"     , "8.5");
      data.Add("Editor_Letter_Tabs"     , "2");
      data.Add("Editor_Letter_Spaces"   , "True");
      data.Add("Editor_Letter_Highlight", "False");

      data.Add("Editor_RepGen_Font"     , "Courier New");
      data.Add("Editor_RepGen_Size"     , "8.5");
      data.Add("Editor_RepGen_Tabs"     , "2");
      data.Add("Editor_RepGen_Spaces"   , "True");
      data.Add("Editor_RepGen_Highlight", "False");

      data.Add("Editor_PwrPls_Font"     , "Courier New");
      data.Add("Editor_PwrPls_Size"     , "8.5");
      data.Add("Editor_PwrPls_Tabs"     , "2");
      data.Add("Editor_PwrPls_Spaces"   , "True");
      data.Add("Editor_PwrPls_Highlight", "False");

      string[] SyntaxTypes = new string[] {"BuiltIns", "Comment", "Control", "DataType", "Date", "Digits", "Include", "Logic", "Punctuation", "Fields", "Section", "String", "SysVar"};
      foreach(string str in SyntaxTypes)
      {
        data.Add("Editor_PwrPls_" + str + "_Bold"  , "False");
        data.Add("Editor_PwrPls_" + str + "_Italic", "False");
        data.Add("Editor_PwrPls_" + str + "_Color" , "0,0,0");
      }
      foreach(string str in SyntaxTypes)
      {
        data.Add("Editor_RepGen_" + str + "_Bold"  , "False");
        data.Add("Editor_RepGen_" + str + "_Italic", "False");
        data.Add("Editor_RepGen_" + str + "_Color" , "0,0,0");
      }
    }
    //========================================================================
    public static void Load(string path)
    {
      try
      {
        XmlTextReader xml = new XmlTextReader(path);
        while (xml.Read())
        {
          xml.MoveToElement();
          if ((xml.Name != "CONFIG") && (xml.Name != "xml") && (xml.Name != "") && (xml.NodeType != XmlNodeType.EndElement))
          {
            if (xml.Name == "SERVER")
            {
              SymServer serv = new SymServer();
              for (int i=0; i<xml.AttributeCount; i++)
              {
                if(i==0) xml.MoveToFirstAttribute();
                else     xml.MoveToNextAttribute();
                switch (xml.Name)
                {
                  case "IP"  : serv.IP       = xml.Value;             break;
                  case "PORT": serv.Port     = int.Parse(xml.Value);  break;
                  case "NAME": serv.Alias    = xml.Value;             break;
                  case "USR" : serv.AixUsr   = xml.Value;             break;
                  case "PWD" : serv.AixPwd   = xml.Value;             break;
                  case "SAVE": serv.Remember = bool.Parse(xml.Value); break;
                  case "EXPA": serv.Expanded = bool.Parse(xml.Value); break;
                }
              }
              Servers.Add(serv);
            }
            else if (xml.Name == "SYM")
            {
              if (Servers.Count > 0)
              {
                string symDir="", symId=""; bool symSave=true;
                for(int i=0; i<xml.AttributeCount; i++)
                {
                  if(i==0) xml.MoveToFirstAttribute();
                  else     xml.MoveToNextAttribute();
                  switch(xml.Name)
                  {
                    case "DIR" : symDir  = xml.Value;             break;
                    case "ID"  : symId   = xml.Value;             break;
                    case "SAVE": symSave = bool.Parse(xml.Value); break;
                  }
                }
                Servers[Servers.Count-1].AddSym(symDir, symId, symSave);
              }
            }
            else if (xml.Name == "LOCAL")
            {
              string locName="", locPath=""; bool locExpa=false;
              for(int i=0; i<xml.AttributeCount; i++)
              {
                if (i == 0) xml.MoveToFirstAttribute();
                else xml.MoveToNextAttribute();
                switch(xml.Name)
                {
                  case "NAME": locName = xml.Value;             break;
                  case "PATH": locPath = xml.Value;             break;
                  case "EXPA": locExpa = bool.Parse(xml.Value); break;
                }
              }
              Locals.Add(new Local(locName, locPath, locExpa));
            }
            else
              data[xml.Name] = xml.ReadInnerXml();
          }
        }
        xml.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Loading Config File\n" + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    //========================================================================
    public static void Save(string path)
    {
      bool tryAgain = true;
      while (tryAgain)
      {
        try
        {
          StreamWriter xml = new StreamWriter(path, false, Encoding.ASCII);
          xml.WriteLine("<?xml version=\"1.0\" encoding=\"us-ascii\"?>");
          xml.WriteLine("<CONFIG>");
          foreach(KeyValuePair<string, string> datum in data)
            xml.WriteLine("\t<"+datum.Key+'>'+datum.Value+"</"+datum.Key+'>');
          for(int i=0; i<Servers.Count; i++)
          {
            string serverPwd = Servers[i].Remember ? Servers[i].AixPwd : "";
            xml.WriteLine("\t<SERVER");
            xml.WriteLine("\t\tIP=\""   + Servers[i].IP                  + '\"');
            xml.WriteLine("\t\tPORT=\"" + Servers[i].Port.ToString()     + '\"');
            xml.WriteLine("\t\tNAME=\"" + Servers[i].Alias               + '\"');
            xml.WriteLine("\t\tUSR=\""  + Servers[i].AixUsr              + '\"');
            xml.WriteLine("\t\tPWD=\""  + serverPwd                      + '\"');
            xml.WriteLine("\t\tSAVE=\"" + Servers[i].Remember.ToString() + '\"');
            xml.WriteLine("\t\tEXPA=\"" + Servers[i].Expanded.ToString() + "\">");
            for(int c=0; c<Servers[i].Syms.Count; c++)
            {
              string symId = Servers[i].Syms[c].Remember ? Servers[i].Syms[c].SymId : "";
              xml.WriteLine("\t\t<SYM");
              xml.WriteLine("\t\t\tDIR=\""  + Servers[i].Syms[c].SymDir              + '\"');
              xml.WriteLine("\t\t\tID=\""   + symId                                  + '\"');
              xml.WriteLine("\t\t\tSAVE=\"" + Servers[i].Syms[c].Remember.ToString() + "\">");
              xml.WriteLine("\t\t</SYM>");
            }
            xml.WriteLine("\t</SERVER>");
          }
          for(int i=0; i<Locals.Count; i++)
          {
            xml.WriteLine("\t<LOCAL");
            xml.WriteLine("\t\tNAME=\"" + Locals[i].Name                + '\"');
            xml.WriteLine("\t\tPATH=\"" + Locals[i].Path                + '\"');
            xml.WriteLine("\t\tEXPA=\"" + Locals[i].Expanded.ToString() + "\">");
            xml.WriteLine("\t</LOCAL>");
          }
          xml.WriteLine("</CONFIG>");
          xml.Flush();
          xml.Close();
          tryAgain = false;
        }
        catch (Exception ex)
        {
          if(MessageBox.Show("Error Saving Config File\n"+ex.Message,"PwrIDE", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
            tryAgain = false;
        }
      }
    }
    //========================================================================
    public static string GetString(string key)
    {
      return data[key];
    }
    //------------------------------------------------------------------------
    public static bool GetBool(string key)
    {
    	return (data[key]=="True");
    }
    //------------------------------------------------------------------------
    public static int GetInt(string key)
    {
    	return int.Parse(data[key]);
    }
    //------------------------------------------------------------------------
    public static float GetFloat(string key)
    {
    	return float.Parse(data[key]);
    }
    //------------------------------------------------------------------------
    public static System.Drawing.Color GetColor(string key)
    {
    	string[] rgb = data[key].Split(new char[] {','});
    	return System.Drawing.Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
    }
    //========================================================================
    public static void SetValue(string key, string value)
    {
      data[key] = value;
      NotifyUpdateHandlers();
    }
    //------------------------------------------------------------------------
    public static void SetValue(string key, string value, bool bulkUpdate)
    {
      data[key] = value;
    }
    //------------------------------------------------------------------------
    public static void SetValue(string key, string value, UpdateHandler ignoreMe)
    {
      data[key] = value;
      for(int i=0; i<RegisteredUpdateHandlers.Count; i++)
        if(RegisteredUpdateHandlers[i] != ignoreMe)
          RegisteredUpdateHandlers[i]();
    }
    //========================================================================
    public static SymServer GetServer(string name)
    {
    	for(int i=0; i<Servers.Count; i++)
    		if(Servers[i].Alias == name)
    			return Servers[i];
    	return null;
    }
    //------------------------------------------------------------------------
    public static SymServer GetServerIP(string ip)
    {
    	for(int i=0; i<Servers.Count; i++)
    		if(Servers[i].IP == ip)
    			return Servers[i];
    	return null;
    }
    //========================================================================
    public static SymInst GetSym(string serverName, string symDir)
    {
    	for(int i=0; i<Servers.Count; i++)
    		if(Servers[i].Alias == serverName)
    			return Servers[i].GetSym(symDir);
    	return null;
    }
    //------------------------------------------------------------------------
    public static SymInst GetSymIP(string serverIP, string symDir)
    {
    	for(int i=0; i<Servers.Count; i++)
    		if(Servers[i].IP == serverIP)
    			return Servers[i].GetSym(symDir);
    	return null;
    }
    //========================================================================
    public static Local GetLocal(string localName)
    {
      for(int i=0; i<Locals.Count; i++)
        if(Locals[i].Name == localName)
          return Locals[i];
      return null;
    }
    //========================================================================
    private static void NotifyUpdateHandlers()
    {
      for(int i = 0; i < RegisteredUpdateHandlers.Count; i++)
        RegisteredUpdateHandlers[i]();
    }
    //------------------------------------------------------------------------
    public static void RegisterUpdateHandler(UpdateHandler upd)
    {
      RegisteredUpdateHandlers.Add(upd);
    }
    //------------------------------------------------------------------------
    public static void RemoveUpdateHandler(UpdateHandler upd)
    {
      if(RegisteredUpdateHandlers.Contains(upd))
        RegisteredUpdateHandlers.Remove(upd);
    }
    //========================================================================
    public static void LoadSyntax()
    {
      //init intellisense
      RepGenComplete.ImgList  = Util.MainForm.AutoCompleteIcons;
      PwrPlusComplete.ImgList = Util.MainForm.AutoCompleteIcons;
      string contents;

      try
      {
        contents = File.ReadAllText(Application.StartupPath+"\\Data\\RepGen.db.txt");
        RepGenComplete.LoadRecords(contents);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Loading RepGen Field DB\nAuto-Completion Will Not Be Available.\nError: " + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        contents = File.ReadAllText(Application.StartupPath+"\\Data\\PwrPlus.db.txt");
        PwrPlusComplete.LoadRecords(contents);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Loading PowerPlus Field DB\nAuto-Completion Will Not Be Available.\nError: " + ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      //init syntax highlighting
      if(!Directory.Exists(Application.StartupPath + "\\Data"))
      {
        MessageBox.Show("Couldn't Find Data Directory\nSyntax Highlighting Will Not Be Available.", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      FileSyntaxModeProvider syntaxProvider = new FileSyntaxModeProvider(Application.StartupPath + "\\Data");
      HighlightingManager.Manager.AddSyntaxModeFileProvider(syntaxProvider);
    }
    //------------------------------------------------------------------------
    public static void UpdateSyntax()
    {
      string path = Application.StartupPath + "\\Data\\PwrPlus.xshd";

      if(UpdateSyntaxFile(Application.StartupPath+"\\Data\\PwrPlus.xshd", true))
      {
        if(UpdateSyntaxFile(Application.StartupPath+"\\Data\\RepGen.xshd", false))
        {
          HighlightingManager.Manager.ReloadSyntaxModes();
          NotifyUpdateHandlers();
        }
      }
    }
    //------------------------------------------------------------------------
    private static bool UpdateSyntaxFile(string path, bool isPower)
    {
      string content="";

      try
      {
        content = File.ReadAllText(path);
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Updating Syntax Definition to File.\nFile: "+path+"\nError: "+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      Match match; string repl;
      Regex digits = new Regex(@"<\s*Digits\s+name=[^>]+>");
      string strSpan = "<\\s*Span\\s+name\\s*=\\s*\"%1\"[^>]+>";
      string strKeys = "<\\s*KeyWords\\s+name\\s*=\\s*\"%1\"[^>]+>";
      string[] spans = isPower ? (new string[]{ "Include", "String", "Date", "CommentLine", "CommentMulti" }) : (new string[]{ "Include", "String", "Date", "Comment" });
      string[] saeol = isPower ? (new string[]{"true", "true", "true", "true", "false"}) : (new string[]{"true", "true", "true", "false"});
      string[] keywords = new string[]{ "Punctuation", "DataType", "Section", "Control", "Logic", "SysVar", "BuiltIns", "Fields" };

      string b, i, c;
      string prefix = isPower ? "Editor_PwrPls_" : "Editor_RepGen_";
      match = digits.Match(content);
      if(match.Success)
      {
        b = GetString(prefix + "Digits_Bold"  ).ToLower();
        i = GetString(prefix + "Digits_Italic").ToLower();
        c = System.Drawing.ColorTranslator.ToHtml(GetColor( prefix + "Digits_Color" ));
        repl = "<Digits name=\"Digits\" bold=\""+b+"\" italic=\""+i+"\" color=\""+c+"\"/>";
        content = content.Replace(match.Value, repl);
      }

      string lookFor;
      for(int x=0; x<spans.Length; x++)
      {
        string str = spans[x];
        lookFor = strSpan.Replace("%1", str);
        match = new Regex(lookFor).Match(content);
        if(match.Success)
        {
          string strB = str.Replace("Line", "").Replace("Multi", "");
          b = GetString(prefix + strB + "_Bold").ToLower();
          i = GetString(prefix + strB + "_Italic").ToLower();
          c = System.Drawing.ColorTranslator.ToHtml(GetColor(prefix + strB + "_Color"));
          repl = "<Span name=\""+str+"\" bold=\""+b+"\" italic=\""+i+"\" color=\""+c+"\" stopateol=\""+saeol[x]+"\">";
          content = content.Replace(match.Value, repl);
        }
      }

      foreach(string str in keywords)
      {
        lookFor = strKeys.Replace("%1", str);
        match = new Regex(lookFor).Match(content);
        if(match.Success)
        {
          b = GetString(prefix + str + "_Bold").ToLower();
          i = GetString(prefix + str + "_Italic").ToLower();
          c = System.Drawing.ColorTranslator.ToHtml(GetColor(prefix + str + "_Color"));
          repl = "<KeyWords name=\""+str+"\" bold=\""+b+"\" italic=\""+i+"\" color=\""+c+"\">";
          content = content.Replace(match.Value, repl);
        }
      }

      try
      {
        File.WriteAllText(path, content);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Updating Syntax Definition to File.\nFile: "+path+"\nError: "+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      return true;
    }
    //========================================================================
  }
}
