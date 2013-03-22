using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PwrPlus
{
  public class Parser
  {
    public struct ListItem
    {
        public string[] item; //child nodes
        public int      ponr; //point of no return
        public int      iErr; //error code
        public string   sErr; //error string
        public ListItem(string[] Items, int PoNR, int ErrCode, string ErrString)
        { item = Items;  ponr = PoNR; iErr = ErrCode; sErr = ErrString; }
    }

    public Symbol RootSym;
    public Scanner scan;
    private bool    errFlag=false;
    private int     tokPos =0;
    private string  tokVal { get{ return scan.tokVal[ tokPos]; }}
    private int     tokLin { get{ return scan.tokLine[tokPos]; }}
    
    public Parser(Scanner scanner)
    {
      scan=scanner;
      RootSym = new Symbol(-1, "Root");
      PopulateDefinitions();
    }
    public bool Parse()
    {
      return ParseOp("Program", RootSym);
    }
    
    //--------------------------------------------------------------------------------------------------------------------------
    //debugging system (sloooooow, but useful, functions)
    //--------------------------------------------------------------------------------------------------------------------------
    public bool          dbgMode=false;
    public string        dbgDump="";
    private List<string> dbgStack=new List<string>();
    private string dbgStackDump()
    {
      string _stack="";
      for(int i=0; i<dbgStack.Count; i++) _stack+=dbgStack[i]+" > ";
      return _stack + '(' + tokPos.ToString() + ',' + tokVal + ")\r\n";
    }
    private void dbgStackAdd(string funct)
    {
      dbgStack.Add(funct);
      dbgDump+=dbgStackDump();
    }
    private void dbgStackDel() { dbgStack.RemoveAt(dbgStack.Count-1); }
    private void dbgStackSingle(string f) { dbgStackAdd(f); dbgStackDel(); }
    public string DumpTree(Symbol node, string inherit)
    {
      string result = "";
      string dump = inherit +" > "+ node.typ;
      if (node.kid != null)
      {
        if (node.kid.Count > 0)
        {
          for (int i = 0; i < node.kid.Count; i++) result += DumpTree(node.kid[i], dump);
          return result;
        }
      }
      return dump + "\r\n";
    }
    //--------------------------------------------------------------------------------------------------------------------------
    //parsing definition functions
    //--------------------------------------------------------------------------------------------------------------------------
    private Dictionary<string, string>   opSyntax = new Dictionary<string, string>();
    private Dictionary<string, Regex>    opRegEx  = new Dictionary<string, Regex>();
    private Dictionary<string, string[]> opOr     = new Dictionary<string,string[]>();
    private Dictionary<string, ListItem> opList   = new Dictionary<string, ListItem>();
    private bool ParseOp(string name, Symbol node)
    {
      if (tokPos >= scan.tokVal.Count) return false;

      if (name == "Name") //single special case needed
      {
        if (dbgMode) dbgStackSingle(name);
        if (tokVal == "END")    return false;
        if (tokVal == "RETURN") return false;
        Match test = new Regex(@"^[a-zA-Z@&][a-zA-Z0-9]*(:[a-zA-Z0-9]+)?(\.[a-zA-Z0-9]+)*$").Match(tokVal);
        if (test.Success) { node.AddChild(new Symbol(tokPos, name)); tokPos++; return true; }
        return false;
      }
      else if (opSyntax.ContainsKey(name))
      {
        if (dbgMode) dbgStackSingle(name);
        if (tokVal == opSyntax[name]) { tokPos++; return true; }
        return false;
      }
      else if (opRegEx.ContainsKey(name))
      {
        if (dbgMode) dbgStackSingle(name);
        Match test = opRegEx[name].Match(tokVal);
        if (test.Success) { node.AddChild(new Symbol(tokPos, name)); tokPos++; return true; }
        return false;
      }
      else if (opOr.ContainsKey(name))
      {
        foreach (string p in opOr[name])
        {
          int _pos = tokPos;
          if (ParseOp(p, node)) return true;
          tokPos = _pos;
          if (errFlag) return false;
        }
        return false;
      }
      else if (opList.ContainsKey(name))
      {
        if (dbgMode) dbgStackAdd(name);
        int i, _pos = tokPos;
        bool completed = true;
        ListItem list = opList[name];
        Symbol symTmp = new Symbol(-1, name);
        for (i = 0; i < list.item.Length; i++)
        {
          string curr = list.item[i];
          char opt = curr[curr.Length - 1];
          if ((opt == '*') || (opt == '+') || (opt == '?'))
            curr = curr.Substring(0, curr.Length - 1);
          else
            opt = ' ';

          if (opt == ' ') //no special op, exactly one occurence
          {
            if (ParseOp(curr, symTmp) == false) { completed = false; break; }
          }
          else if (opt == '?') //single optional occurence
          {
            ParseOp(curr, symTmp);
          }
          else if (opt == '+') //at least once occurence
          {
            int count = 0;
            while (ParseOp(curr, symTmp)) { if (errFlag)break; count++; }
            if (count == 0) { completed = false; break; }
          }
          else if (opt == '*') //optional recurring definition
          {
            while (ParseOp(curr, symTmp)) if (errFlag) break;
          }
          if (errFlag) return false;
        }
        if (errFlag) return false;
        if ((!completed) && (i >= list.ponr))
        {
          errFlag = true;
          PwrError.SetFatal(list.sErr + " at \"" + tokVal + "\"", tokLin, list.iErr);
        }
        if (!completed)
        {
          if (dbgMode) dbgStackDel();
          tokPos = _pos;
          return false;
        }

        node.AddChild(symTmp);
        if (dbgMode) dbgStackDel();
        return true;
      }
      else
      {
        errFlag = true;
        PwrError.SetFatal("Internal Error: Unrecognized Parsing Definition \"" + name + "\"", tokLin, 0xFFF);
        return false;
      }
    }
    //--------------------------------------------------------------------------------------------------------------------------
    //parsing definitions
    //--------------------------------------------------------------------------------------------------------------------------
    private void PopulateDefinitions()
    {
      //Syntax definitions (matched, but not put in Symbol tree nodes)
      opSyntax.Add("ParenL", "("); opSyntax.Add("ParenR", ")"); opSyntax.Add("Semi"  , ";");
      opSyntax.Add("CrlBrL", "{"); opSyntax.Add("CrlBrR", "}"); opSyntax.Add("Comma" , ",");
      opSyntax.Add("BrackL", "["); opSyntax.Add("BrackR", "]"); opSyntax.Add("Equals", "=");
      string[] keywords = {"GLOBAL","SETUP","SELECT","SORT","PROCESS","FINAL","END","RETURN","CONST","IF","ELSEIF","ELSE","FOR","FOREACH","WHILE","IN","NOTIN","ANY","NOTANY", "HEADERS", "TRAILERS", "FMPERFORM", "TRANPERFORM", "SET"};
      foreach(string key in keywords) opSyntax.Add(key, key);
      //RegEx definitions
      opRegEx.Add("NONE"       , new Regex(@"^NONE$"));
      opRegEx.Add("PreUnaryOp" , new Regex(@"^(\-)$"));
      opRegEx.Add("PostUnaryOp", new Regex(@"^(\+\+|\-\-)$"));
      opRegEx.Add("AssignOp"   , new Regex(@"^(\+=|\-=|\*=|\/=|=)$"));
      opRegEx.Add("NonAssignOp", new Regex(@"^(&&|\|\||!=|\<=|\>=|\<|\>|\+|\-|\*|\/|==)$"));
      opRegEx.Add("RootProc"   , new Regex(@"^(MODE|TARGET|STACKSIZE|TITLE|DATAFILE)$"));
      opRegEx.Add("VarType"    , new Regex(@"^(INT|DATE|MONEY|RATE|FLOAT|CHAR)$"));
      opRegEx.Add("GlobType"   , new Regex(@"^(INT|DATE|MONEY|RATE|FLOAT|CHAR|CONST)$"));
      opRegEx.Add("ProcType"   , new Regex(@"^(INT|DATE|MONEY|RATE|FLOAT|CHAR|VOID)$"));
      opRegEx.Add("Literal"    , new Regex(@"^(§[0-9]+|¼[0-9]+|£[0-9]+|‰[0-9]+|ƒ[0-9]+|¤[0-9]+)$"));
      //OR definitions
      opOr.Add("ParamName"  , new string[] {"ParamArrRef", "Name"        });
      opOr.Add("BuiltIn"    , new string[] {"Built_In"   , "Built_NotIn" });
      opOr.Add("SubAble"    , new string[] {"Expression" , "ExprCompA"   });
      opOr.Add("StmntBody"  , new string[] {"StmntBlock" , "Statement"   });
      opOr.Add("AssignAble" , new string[] {"Assignment" , "ArrayRef"    , "Name"      });
      opOr.Add("Assignment" , new string[] {"AssignBlock", "ArrayAssign" , "PostUnary" });
      opOr.Add("NameLit"    , new string[] {"ArrayRef"   , "Name"        , "Literal"   });
      opOr.Add("TermAble"   , new string[] {"Definition" , "Assignment"  , "ProcCall"  });
      opOr.Add("SelectAble" , new string[] {"AnyBlock"   , "NotAnyBlock" , "Expression", "NONE"        });
      opOr.Add("Conditional", new string[] {"CndIf"      , "CndForRec"   , "CndFor"    , "CndForEach" , "CndWhile"   });
			opOr.Add("ExprCompA"  , new string[] {"PreUnary"   , "SubExpr"     , "BuiltIn"   , "ProcCall"   , "NameLit"    });
			opOr.Add("Statement"  , new string[] {"HeaderBlock", "TrailerBlock", "FmPerform" , "TranPerform", "Conditional", "Terminal"  });
      opOr.Add("Value"      , new string[] {"PreUnary"   , "PostUnary"   , "Expression", "BuiltIn"    , "ProcCall"   , "NameLit"   });
      opOr.Add("Section"    , new string[] {"SectGlobal" , "SectSetup"   , "SectSelect", "SectSort"   , "SectProcess", "SectFinal" });
      //List definitions
      opList.Add("ParamArrRef", new ListItem(new string[] {"Name"       , "BrackL"      , "BrackR"                                                                                                                     },    2, 0x03D, "Invalid Array Parameter Definition (don't include array size)"));
      opList.Add("ArrayRef"   , new ListItem(new string[] {"Name"       , "BrackL"      , "Value"     , "BrackR"                                                                                                       },    2, 0x03C, "Invalid Array Reference"));
      opList.Add("PreUnary"   , new ListItem(new string[] {"PreUnaryOp" , "Value"                                                                                                                                      },    1, 0x004, "Invalid Unary Operation"));
      opList.Add("PostUnary"  , new ListItem(new string[] {"Name"       , "PostUnaryOp"                                                                                                                                }, 1024, 0xFFF, "Uknown Error"));
      opList.Add("HeaderBlock", new ListItem(new string[] {"HEADERS"    , "StmntBlock"                                                                                                                                 },    1, 0x035, "Invalid Headers Block"));
			opList.Add("TrailerBlock",new ListItem(new string[] {"TRAILERS"   , "StmntBlock"                                                                                                                                 },    1, 0x036, "Invalid Trailers Block"));
			opList.Add("SetProc"    , new ListItem(new string[] {"SET"        , "ParenL"      , "NameLit"     , "Comma"     , "NameLit"    , "ParenR"    , "Semi"                                                            },    1, 0x037, "Invalid Set Call"));
			opList.Add("FmPerform"  , new ListItem(new string[] {"FMPERFORM"  , "ParenL"      , "ValueList"   , "ParenR"    , "CrlBrL"     , "SetProc*"  , "CrlBrR"                                                          },    1, 0x038, "Invalid FmPerform Block"));
			opList.Add("TranPerform", new ListItem(new string[] {"TRANPERFORM", "ParenL"      , "ValueList"   , "ParenR"    , "CrlBrL"     , "SetProc*"  , "CrlBrR"                                                          },    1, 0x03A, "Invalid TranPerform Block"));
			opList.Add("ValueB"     , new ListItem(new string[] {"Comma"      , "Value"                                                                                                                                      },    1, 0x005, "Invalid Value List"));
      opList.Add("ValueList"  , new ListItem(new string[] {"Value"      , "ValueB*"                                                                                                                                    }, 1024, 0xFFF, "Uknown Error"));
      opList.Add("Built_In"   , new ListItem(new string[] {"IN"         , "ParenL"      , "Value"       , "Semi"                     , "ValueList" , "ParenR"                                                          },    1, 0x006, "Invalid In() Call"));
      opList.Add("Built_NotIn", new ListItem(new string[] {"NOTIN"      , "ParenL"      , "Value"       , "Semi"                     , "ValueList" , "ParenR"                                                          },    1, 0x007, "Invalid NotIn() Call"));
      opList.Add("SubExpr"    , new ListItem(new string[] {"ParenL"     , "SubAble"     , "ParenR"                                                                                                                     },    1, 0x008, "Invlaid (SubExpression) Block"));
      opList.Add("ExprCompB"  , new ListItem(new string[] {"NonAssignOp", "ExprCompA"                                                                                                                                  },    1, 0x009, "Invalid Right-Hand Expression"));
      opList.Add("Expression" , new ListItem(new string[] {"ExprCompA"  , "ExprCompB+"                                                                                                                                 }, 1024, 0xFFF, "Uknown Error"));
      opList.Add("ProcCall"   , new ListItem(new string[] {"Name"       , "ParenL"      , "ValueList?"  , "ParenR"                                                                                                     },    2, 0x00A, "Invalid Procedure Call Format"));
			opList.Add("ArrayAssign", new ListItem(new string[] {"ArrayRef"   , "AssignOp"    , "Value"                                                                                                                      },    2, 0x00B, "Invalid Assignment"));
      opList.Add("AssignBlock", new ListItem(new string[] {"Name"       , "AssignOp"    , "Value"                                                                                                                      },    2, 0x00B, "Invalid Assignment"));
      opList.Add("RootCall"   , new ListItem(new string[] {"RootProc"   , "ParenL"      , "ValueList"   , "ParenR"                   , "Semi"                                                                          },    2, 0x00C, "Invalid Root Procedure Call Format"));
      opList.Add("DefBlockA"  , new ListItem(new string[] {"VarType"    , "AssignAble"                                                                                                                                 },    1, 0x00D, "Invalid Definition"));
      opList.Add("DefBlockB"  , new ListItem(new string[] {"Comma"      , "VarType?"    , "AssignAble"                                                                                                                 },    1, 0x00E, "Invalid Defintion"));
      opList.Add("Definition" , new ListItem(new string[] {"DefBlockA"  , "DefBlockB*"                                                                                                                                 }, 1024, 0xFFF, "Uknown Error"));
      opList.Add("GDefBlockA" , new ListItem(new string[] {"GlobType"   , "AssignAble"                                                                                                                                 },    1, 0x00F, "Invalid Global Defintion"));
      opList.Add("GDefBlockB" , new ListItem(new string[] {"Comma"      , "GlobType?"   , "AssignAble"                                                                                                                 },    1, 0x00F, "Invalid Global Defintion"));
      opList.Add("GlobalDef"  , new ListItem(new string[] {"GDefBlockA" , "GDefBlockB*" , "Semi"                                                                                                                       }, 1024, 0xFFF, "Uknown Error"));
      opList.Add("Terminal"   , new ListItem(new string[] {"TermAble"   , "Semi"                                                                                                                                       },    1, 0x010, "Invalid Terminal Statement (Missing Semicolon?)"));
      opList.Add("StmntBlock" , new ListItem(new string[] {"CrlBrL"     , "Statement+"  , "CrlBrR"                                                                                                                     },    1, 0x011, "Invalid {Statement Block} Format"));
      opList.Add("AnyBlock"   , new ListItem(new string[] {"ANY"        , "ParenL"      , "Name"        , "Semi"      , "Expression?", "ParenR"                                                                        },    2, 0x033, "Invalid Any Block"));
      opList.Add("NotAnyBlock", new ListItem(new string[] {"NOTANY"     , "ParenL"      , "Name"        , "Semi"      , "Expression?", "ParenR"                                                                        },    2, 0x033, "Invalid NotAny Block"));
      opList.Add("CndElse"    , new ListItem(new string[] {"ELSE"       , "StmntBody"                                                                                                                                  },    1, 0x012, "Invalid Else Clause"));
      opList.Add("CndElseIf"  , new ListItem(new string[] {"ELSEIF"     , "ParenL"      , "Expression"  , "ParenR"    , "StmntBody"                                                                                    },    1, 0x013, "Invalid ElseIf Clause"));
      opList.Add("CndIf"      , new ListItem(new string[] {"IF"         , "ParenL"      , "Expression"  , "ParenR"    , "StmntBody"  , "CndElseIf*", "CndElse?"                                                        },    1, 0x014, "Invalid If Block"));
      opList.Add("CndForRec"  , new ListItem(new string[] {"FOR"        , "ParenL"      , "Name"        , "NameLit"   , "NameLit?"   , "ParenR"    , "StmntBody"                                                                     },    4, 0x015, "Invalid For-Record Block"));
      opList.Add("CndFor"     , new ListItem(new string[] {"FOR"        , "ParenL"      , "Assignment?" , "Semi"      , "Expression" , "Semi"      , "Assignment?", "ParenR"     , "StmntBody"                         },    1, 0x015, "Invalid For Block"));
      opList.Add("CndForEach" , new ListItem(new string[] {"FOREACH"    , "ParenL"      , "Name"        , "Semi"      , "Expression?", "Semi"      , "Expression?", "ParenR"     , "StmntBody"                         },    1, 0x016, "Invlaid ForEach Block"));
      opList.Add("CndWhile"   , new ListItem(new string[] {"WHILE"      , "ParenL"      , "Expression"  , "ParenR"    , "StmntBody"                                                                                    },    1, 0x017, "Invalid While Block"));
      opList.Add("SectGlobal" , new ListItem(new string[] {"GLOBAL"     , "GlobalDef*"  , "END"                                                                                                                        },    1, 0x018, "Invalid Global Section"));
      opList.Add("SectSetup"  , new ListItem(new string[] {"SETUP"      , "Statement*"  , "END"                                                                                                                        },    1, 0x019, "Invalid Setup Section"));
      opList.Add("SectSelect" , new ListItem(new string[] {"SELECT"     , "SelectAble?" , "END"                                                                                                                        },    1, 0x01A, "Invalid Select Section"));
      opList.Add("SectSort"   , new ListItem(new string[] {"SORT"       , "Name+"       , "END"                                                                                                                        },    1, 0x01B, "Invalid Sort Section"));
      opList.Add("SectProcess", new ListItem(new string[] {"PROCESS"    , "Statement*"  , "END"                                                                                                                        },    1, 0x01C, "Invalid Process Section"));
      opList.Add("SectFinal"  , new ListItem(new string[] {"FINAL"      , "Statement*"  , "END"                                                                                                                        },    1, 0x01D, "Invalid Final Section"));
      opList.Add("ParamBlkB"  , new ListItem(new string[] {"Comma"      , "VarType"     , "ParamName"                                                                                                                  }, 1024, 0xFFF, "Uknown Error"));
      opList.Add("ParamBlkA"  , new ListItem(new string[] {"VarType"    , "ParamName"                                                                                                                                  },    1, 0x01E, "Invalid Parameter List"));
      opList.Add("ParamList"  , new ListItem(new string[] {"ParamBlkA"  , "ParamBlkB*"                                                                                                                                 }, 1024, 0xFFF, "Uknown Error"));
      opList.Add("ReturnBlk"  , new ListItem(new string[] {"ParenL"     , "Value?"      , "ParenR"                                                                                                                     },    1, 0x01F, "Invalid Return() Block"));
      opList.Add("Procedure"  , new ListItem(new string[] {"ProcType"   , "Name"        , "ParenL"      , "ParamList?", "ParenR"     , "Statement+", "RETURN"     , "ReturnBlk?"                                       },    1, 0x020, "Invalid Procedure Defintion"));
      opList.Add("Program"    , new ListItem(new string[] {"RootCall*"  , "Section*"    , "Procedure*"                                                                                                                 }, 1024, 0xFFF, "Uknown Error"));
    }
  }
}
