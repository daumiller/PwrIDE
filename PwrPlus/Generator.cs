using System;
using System.Text;
using System.Collections.Generic;

namespace PwrPlus
{
  public class Generator
  {
  	//--------------------------------------------------------------------------------------------------------------------------
    //structures
    //--------------------------------------------------------------------------------------------------------------------------
    private struct Global
    {
      public string Name;
      public string Type;
      public string Init;
      public Global(string name, string type, string init){Name=name; Type=type; Init=init;}
    }
    private struct Variable
    {
      public string Name;
      public string Type;
      public Variable(string name, string type){Name=name; Type=type;}
    }
    private struct Procedure
    {
      public string Name;
      public string Type;
      public List<Variable> Param;
      public Procedure(string name, string type, List<Variable> param){Name=name; Type=type; Param=param;}
    }
    private struct Frame
    {
      public string pre;
      public string post;
      public Dictionary<string, List<string>> ctx;
      public Frame(string Pre, string Post, Dictionary<string, List<string>> Context){pre=Pre; post=Post; ctx=Context;}
    }
    //--------------------------------------------------------------------------------------------------------------------------
    //class globals
    //--------------------------------------------------------------------------------------------------------------------------
    private bool            errFlag       = false;
    private Symbol          root;
    private Scanner         scan;
    private List<Variable>  Consts        = new List<Variable>();
    private List<Global>    Globals       = new List<Global>();
    private List<Procedure> Procedures    = new List<Procedure>();
    private string[]        SymProcedures = { "HEADER", "COL", "PRINT", "NEWLINE", "NEWPAGE", "TERMINATE", "SUPPRESSNEWLINE", "SYSUSERNAME",
		                                          "OUTPUTOPEN", "OUTPUTSWITCH", "OUTPUTCLOSE",
																							"FILECLOSE", "FILEGETPOS", "FILELISTCLOSE", "FILELISTOPEN", "FILELISTREAD", "FILEOPEN", "FILEREAD", "FILEREADLINE", "FILESETPOS", "FILEWRITE", "FILEWRITELINE",
																							"FILECREATE", "FILEDELETE", "FILEARCHIVEADD", "FILEARCHIVEEXTRACT", "FILEDECRYPT", "FILEENCRYPT",
		                                          "DATE", "DATEOFFSET", "DATEVALUE", "DAY", "DAYOFWEEK", "FULLYEAR", "HOUR", "MINUTE", "MONTH", "YEAR",
		                                          "CHARACTERREAD", "CODEREAD", "DATEREAD", "MONEYREAD", "NUMBERREAD", "RATEREAD", "YESNOREAD",
																							"ENTERCHARACTER", "ENTERCODE", "ENTERDATE", "ENTERDELIMITER", "ENTERLINE", "ENTERMONEY", "ENTERNUMBER", "ENTERRATE", "ENTERYESNO",
																							"ABS", "EXP", "FLOAT", "FLOATVALUE", "FLOOR", "INT", "LOG", "MOD", "MONEY", "NUMBER", "PWR", "RATE", "VALUE",
																							"CAPITALIZE", "CHRVALUE", "CHARACTERSEARCH", "CTRLCHR", "FORMAT", "LENGTH", "LOWERCASE", "MD5HASH", "PASSWORDHASH", "REPEATCHR", "SEGMENT", "UPPERCASE",
																							"EMAILSTART", "EMAILLINE", "EMAILSEND", "INITSUBROUTINE", "EXECUTE",
																							"GETDATACHAR", "GETDATADATE", "GETDATAMONEY", "GETDATANUMBER", "GETDATARATE",
																							"GETFIELDNUMBER", "GETFIELDNAME", "GETFIELDMNEMONIC", "GETFIELDHELPFILE", "GETFIELDDATATYPE", "GETFIELDDATAMAX",
																							"WINDOWSSEND", "HTMLVIEWOPEN", "HTMLVIEWLINE", "HTMLVIEWDISPLAY", "POPUPMESSAGE", "YESNOPROMPT",
                                              "DIALOGSTART", "DIALOGINTROTEXT", "DIALOGNEWCOLUMN", "DIALOGDISPLAY", "DIALOGCLOSE", "DIALOGSTARTGROUPBOX", "DIALOGENDGROUPBOX",
                                              "DIALOGSTARTGROUPING", "DIALOGENDGROUPING", "DIALOGPROMPTCOMBOSTART", "DIALOGPROMPTCOMBOOPTION", "DIALOGPROMPTCOMBOEND",
                                              "DIALOGPROMPTLISTSTART", "DIALOGPROMPTLISTOPTION", "DIALOGPROMPTLISTEND", "DIALOGTEXTLISTSTART", "DIALOGTEXTLISTOPTION", "DIALOGPROMPTLISTEND",
                                              "DIALOGTEXTLISTSTART", "DIALOGTEXTLISTOPTION", "DIALOGTEXTLISTEND", "DIALOGPROMPTCHAR", "DIALOGPROMPTCODE", "DIALOGPROMPTDATE", "DIALOGPROMPTMONEY",
                                              "DIALOGPROMPTNUMBER", "DIALOGPROMPTPASSWORD", "DIALOGPROMPTRATE", "DIALOGPROMPTYESNO" };
    private string[]        SymVariables  = { "PREVSYSTEMDATE", "SYSACTUALDATE", "SYSACTUALTIME", "SYSCLIENTNUMBER", "SYSCONSOLENUM", "SYSMEMOMODE", "SYSSYMDIRECTORY", "SYSTEMDATE", "SYSUSERNAME", "SYSUSERNUMBER", "SYSWINDOWSLEVEL", "WHILELIMIT" };
    private string[]        SymRecords    = { "ACCESS", "ACCOUNT", "ACHADDENDA", "ACHADDINFO", "ACHEDIT", "ACHITEM", "ACTIVITY", "ADDENDAINFO", "AGGREEMENT", "ANALYSIS", "ANALYSISGROUP", "ANALYSISPLAN",
		                                          "APINV", "ATMDIALOG", "BENEFICIARYADV", "BENEFICIARYFIADV", "BENEFICIARYIINFO", "CARD", "CASHLETTER", "CASHORDER", "CASHORDERTYPE", "CDMDIALOG",
																							"CHECK", "CHECKORDER", "COLLATERAL", "COLLHOLD", "COMMENT", "CORPTRANSFER", "CPQUEUE", "CPWORKCARD", "CREDREP", "CTR", "CTRACCOUNT", "CTRPERSON",
																							"DEALER", "DISTRIBUTION", "DOCUMENT", "DRAWDOWNDEBITACCTADV", "EFT", "PAYROLL", "BILL", "ESCROW", "ESCROWANALYSIS", "EXCPADDENDA", "EXCPADDINFO", "EXCPITEM",
																							"EXTERNALACCOUNT", "FINANCE", "FITOFIINFO", "FMHISTORY", "GLACCOUNT", "GLENTRY", "GLHISTORY", "GLSUBACCOUNT", "GLTRAN", "HOLD", "HOUSEHOLD",
																							"INTERMEDFIADV", "INTERMEDFIINFO", "INVENTORY", "INVOICE", "IRA", "IRS", "LNSEGMENT", "LOAN", "LOANAPP", "LOOKUP", "MBRADDRESS", "MEMBERREC",
																							"NAME", "NONACCTNAME", "NOTE", "OFACDETAILS", "PARTICIPANT", "PARTICIPATION", "PARTICIPATIONLOAN", "PAYEE", "PERSON", "PLEDGE", "PORTFOLIO", "PREFERENCE",
																							"RECEIVEDITEM", "RECEIVERFIINFO", "RMITTANCE", "RESERVEPLAN", "RESERVEPLANLOAN", "RETURNITEM", "SCHEDULE", "SHARE", "SERVICEMESSAGE", "SITE", "TRACKING",
																							"USCAUDITINFO", "USER", "VENDOR", "WESTERNUNION", "WIRE", "WORKLISTEDIT", "WORKLISTFIELD" }; //not sure these are all stand-alone record types...
    private string Title = "", DataFile = "";
    private Dictionary<string, int>    StackSize     = new Dictionary<string,int>();
    private Dictionary<string, string> SectionOutput = new Dictionary<string, string>();
    private int inlineCounter = 0;

    public Generator(Parser parser)
    {
      try{root = parser.RootSym.kid[0];} catch(Exception){root=null;}
      scan = parser.scan;

      StackSize.Add("INT", 0);
      StackSize.Add("DAT", 0);
      StackSize.Add("MNY", 0);
      StackSize.Add("RAT", 0);
      StackSize.Add("FLO", 0);
      StackSize.Add("CHR", 0);

      SectionOutput.Add("ROOT"      , "");
      SectionOutput.Add("DEFINE"    , "");
      SectionOutput.Add("SETUP"     , "");
      SectionOutput.Add("SELECT"    , "");
      SectionOutput.Add("SORT"      , "");
      SectionOutput.Add("PROCESS"   , "");
      SectionOutput.Add("FINAL"     , "");
      SectionOutput.Add("PROCEDURES", "");
    }
    public bool Generate()
    {
      //try
      //{
        if(GenRoot())
          if(GenDefine())
            if(GenProcedures())
              if(GenSetup())
                if(GenProcess())
                  if(GenFinal())
                    if(GenSelect())
                      if(GenSort())
                        return true;
      //}
      //catch(Exception){ return false; }
      return false;
    }
    public string Output
    {
      get
      {
      	//do some post processing here
        StringBuilder sbBody = new StringBuilder();
        sbBody.Append(SectionOutput["SETUP"     ]);
        sbBody.Append(SectionOutput["SELECT"    ]);
        sbBody.Append(SectionOutput["SORT"      ]);
        sbBody.Append(SectionOutput["PROCESS"   ]);
        sbBody.Append(SectionOutput["FINAL"     ]);
        sbBody.Append(SectionOutput["PROCEDURES"]);
        string strBody = sbBody.ToString();

        string strProcs = "";
        if(strBody.Contains("SYMPROCINT0")) strProcs += "SYMPROCINT0 = NUMBER\n";
        if(strBody.Contains("SYMPROCINT1")) strProcs += "SYMPROCINT1 = NUMBER\n";
        if(strBody.Contains("SYMPROCCHR0")) strProcs += "SYMPROCCHR0 = CHARACTER\n";
        if(strBody.Contains("SYMPROCCHR1")) strProcs += "SYMPROCCHR1 = CHARACTER\n";
        if(strBody.Contains("SYMPROCCHR2")) strProcs += "SYMPROCCHR2 = CHARACTER\n";
        string strDefine = SectionOutput["DEFINE"] + strProcs;
        if(strDefine.Length > 0)
          SectionOutput["DEFINE"] = "DEFINE\n"+strDefine+"END\n\n";

        StringBuilder strOut = new StringBuilder();
        strOut.Append(SectionOutput["ROOT"  ]);
        strOut.Append(SectionOutput["DEFINE"]);
        strOut.Append(strBody);
        strOut.Append(StdLib);
        return strOut.ToString();
      }
    }
    private string StdLib
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        foreach(KeyValuePair<string, int> p in StackSize)
        {
          if(p.Value > 0)
          {
          	if(p.Key.Substring(0,3) != "ARR")
          	{
	            sb.Append("PROCEDURE PUSH" + p.Key + "\n");
	            sb.Append("IF(SP" + p.Key + "=" + p.Value + ") THEN DO\n");
	            sb.Append("COL=01 \"=====================================\" NEWLINE\n");
	            sb.Append("COL=01 \"STACK OVERFLOW (" + p.Key + ")                 \" NEWLINE\n");
	            sb.Append("COL=01 \"=====================================\" NEWLINE\n");
	            sb.Append("TERMINATE\n");
	            sb.Append("END\n");
	            sb.Append("ST" + p.Key + "(SP" + p.Key + ")=IM" + p.Key + '\n');
	            sb.Append("SP" + p.Key + "=SP" + p.Key + "+1\n");
	            sb.Append("END\n");
	            sb.Append("PROCEDURE DEC" + p.Key + "\n");
	            sb.Append("SP" + p.Key + "=SP" + p.Key + "-1\n");
	            sb.Append("IF(SP" + p.Key + "<0) THEN SP" + p.Key + "=0\n");
	            sb.Append("END\n");
	            sb.Append("PROCEDURE SUB" + p.Key + '\n');
	            sb.Append("SP" + p.Key + "=SP" + p.Key + "-STACKSUB\n");
	            sb.Append("IF(SP" + p.Key + "<0) THEN SP" + p.Key + "=0\n");
	            sb.Append("END\n\n");
          	}
          	else
          	{
          		sb.Append("PROCEDURE PUSH"+p.Key+"\n");
          		sb.Append("SP"+p.Key+" = SP"+p.Key+" + IMINT\n");
          		sb.Append("IF(SP"+p.Key+" >= "+p.Value+" ) THEN DO\n");
          		sb.Append("COL=01 \"=====================================\" NEWLINE\n");
	            sb.Append("COL=01 \"STACK OVERFLOW (" + p.Key + ")              \" NEWLINE\n");
	            sb.Append("COL=01 \"=====================================\" NEWLINE\n");
          		sb.Append("END\n");
          		sb.Append("IMINT = SP"+p.Key+" - IMINT\n");
          		sb.Append("END\n\n");
          	}
          }
        }
        return sb.ToString();
      }
    }
    //--------------------------------------------------------------------------------------------------------------------------
    //context functions
    //--------------------------------------------------------------------------------------------------------------------------
    private Dictionary<string, List<string>> ContextCreate()
    {
      Dictionary<string, List<string>> retVal = new Dictionary<string, List<string>>();
      retVal.Add("INT", new List<string>());
      retVal.Add("DAT", new List<string>());
      retVal.Add("MNY", new List<string>());
      retVal.Add("RAT", new List<string>());
      retVal.Add("FLO", new List<string>());
      retVal.Add("CHR", new List<string>());
      return retVal;
    }
    private void ContextPush(Dictionary<string, List<string>> ctx, string type, string name)
    {
    		if(ctx.ContainsKey(type)==false)
        	ctx.Add(type, new List<string>());
        ctx[type].Add(name);
    }
    private Dictionary<string, List<string>> ContextCopy(Dictionary<string, List<string>> ctx)
    {
      Dictionary<string, List<string>> retVal = ContextCreate();
      foreach (KeyValuePair<string, List<string>> curr in ctx)
      {
      	if(retVal.ContainsKey(curr.Key) == false)
      		retVal.Add(curr.Key, new List<string>());
        foreach (string item in curr.Value)
          retVal[curr.Key].Add(item);
      }
      return retVal;
    }
    private string ContextKill(Dictionary<string, List<string>> ctx)
    {
      bool first = true;
      StringBuilder retSB = new StringBuilder();
      foreach (KeyValuePair<string, List<string>> curr in ctx) //first clear arrays
      {
      	if((curr.Key.Substring(0,3)=="ARR") && (curr.Value.Count>0))
      	{
      		int idx=-1; //find first non-passed array entry (to reset pointer to)
      		for(int c=0; c<curr.Value.Count; c++)
      			if(curr.Value[c].Substring(0,10)=="PWRPLUSARR")
      				{ idx=c; break; }
      		if(idx>-1)
      		{
      			if(!first) retSB.Append('\n'); else first=false;
      			retSB.Append("SP"+curr.Key+" = "+NameVar(curr.Value[idx].Replace("PWRPLUSARR",""),ctx));
      		}
      	}
      }
      foreach (KeyValuePair<string, List<string>> curr in ctx)
      {
        if((curr.Value.Count>0) && (curr.Key.Substring(0,3)!="REF") && (curr.Key.Substring(0,3)!="ARR"))
        {
          if(!first) retSB.Append('\n'); else first=false;
          if(curr.Value.Count == 1)
          {
          	retSB.Append("CALL DEC");
          	retSB.Append(curr.Key);
          }
          else
          {
          	retSB.Append("STACKSUB="+curr.Value.Count.ToString());
          	retSB.Append("\nCALL SUB"+curr.Key);
        	}
        }
      }
      return retSB.ToString();
    }
    private string ContextKillDiff(Dictionary<string, List<string>> parent, Dictionary<string, List<string>> child)
    {
      bool first = true;
      StringBuilder retSB = new StringBuilder();
      foreach (KeyValuePair<string, List<string>> curr in child) //first deal with arrays
      {
      	if((curr.Key.Substring(0,3)=="ARR") && (curr.Value.Count>0))
      	{
      		int diff = 0;
      		if(parent.ContainsKey(curr.Key)==false)
      			diff=1;
      		else
      			if(parent[curr.Key].Count < curr.Value.Count)
      				diff=1;
      		
      		if(diff>0)
      		{
	      		int idx=-1; //find first non-passed array entry (to reset pointer to)
	      		for(int c=0; c<curr.Value.Count; c++)
	      			if(curr.Value[c].Substring(0,10)=="PWRPLUSARR")
	      				{ idx=c; break; }
	      		if(idx>-1)
	      		{
	      			if(!first) retSB.Append('\n'); else first=false;
	      			retSB.Append("SP"+curr.Key+" = "+NameVar(curr.Value[idx].Replace("PWRPLUSARR",""),child));
	      		}
      		}
      	}
      }
      
      foreach (KeyValuePair<string, List<string>> curr in parent)
      {
        List<string> currCh = child[curr.Key];
        if((currCh.Count > curr.Value.Count) && (curr.Key.Substring(0,3)!="REF") && (curr.Key.Substring(0,3)!="ARR"))
        {
        	int thisDiff = currCh.Count - curr.Value.Count;
          if(!first) retSB.Append('\n'); else first=false;
          if(thisDiff == 1)
          {
          	retSB.Append("CALL DEC");
          	retSB.Append(curr.Key);
          }
          else
          {
          	retSB.Append("STACKSUB="+thisDiff.ToString());
          	retSB.Append("\nCALL SUB"+curr.Key);
        	}
        }
      }
      return retSB.ToString();
    }
    bool VerifyArgCount(List<Symbol> args, int expected, Symbol nameNode)
    {
      if(args.Count!=expected)
      {
        PwrError.SetFatal(NodeValue(nameNode)+" Takes "+expected.ToString()+" Parameters", NodeLine(nameNode), 0x032); 
        errFlag=true;
        return false;
      }
      return true;
    }
    //--------------------------------------------------------------------------------------------------------------------------
    //helper utilities
    //--------------------------------------------------------------------------------------------------------------------------
    private int    NodeLine(Symbol node) {if(node.idx==-1)return -1; return scan.tokLine[node.idx];}
    private string NodeValue(Symbol node){if(node.idx==-1)return ""; return scan.tokVal[node.idx] ;}
    private string SymType(string pwrType)
    {
      switch (pwrType)
      {
        case "INT"    : return "NUMBER";
        case "DAT"    : return "DATE";
        case "MNY"    : return "MONEY";
        case "RAT"    : return "RATE";
        case "FLO"    : return "FLOAT";
        case "CHR"    : return "CHARACTER";
        case "ARRINT" : return "NUMBER";
        case "ARRDAT" : return "DATE";
        case "ARRMNY" : return "MONEY";
        case "ARRRAT" : return "RATE";
        case "ARRFLO" : return "FLOAT";
        case "ARRCHR" : return "CHARACTER";
        default  : return pwrType;
      }
    }
    private string SymType(Symbol node){ return SymType(NodeValue(node)); }
    private string DataType(string inp)
    {
      switch (inp[0])
      {
        case '¤': return "INT";
        case '¼': return "DAT";
        case '£': return "MNY";
        case '‰': return "RAT";
        case 'ƒ': return "FLO";
        case '§': return "CHR";
      }
      switch(inp)
      {
        case "INT"  : return "INT";
        case "DATE" : return "DAT";
        case "MONEY": return "MNY";
        case "RATE" : return "RAT";
        case "FLOAT": return "FLO";
        case "CHAR" : return "CHR";
      }
      return inp;
    }
    private string DataType(Symbol node){ return DataType(NodeValue(node)); }
    private string Literal(string inp)
    {
      switch(inp[0])
      {
        case '¤': return scan.literals["INT"  ][int.Parse(inp.Substring(1))];
        case '¼': return scan.literals["DATE" ][int.Parse(inp.Substring(1))];
        case '£': return scan.literals["MONEY"][int.Parse(inp.Substring(1))];
        case '‰': return scan.literals["RATE" ][int.Parse(inp.Substring(1))];
        case 'ƒ': return scan.literals["FLOAT"][int.Parse(inp.Substring(1))];
        case '§': return scan.literals["CHAR" ][int.Parse(inp.Substring(1))];
        default: return inp;
      }
    }
    private string Literal(Symbol node) { return Literal(NodeValue(node)); }
    private string NameLit(Symbol node) { if(node.typ=="Name") return NodeValue(node); return Literal(NodeValue(node));}
    private string NameVar(string inp, Dictionary<string, List<string>> ctx)
    {
    	if(errFlag) return "";
      int i;
			//system variable
			if(inp[0]=='@') return inp;
      //a const value
      for(i=0; i<Consts.Count; i++) if(Consts[i].Name == inp) return Consts[i].Name;
      //a record - w/ subrecord or field
      if(inp.Contains(":") || inp.Contains(".")) return inp.Replace(':', ' ').Replace('.', ':');
      //a record - top level
      foreach(string tlc in SymRecords) if(tlc == inp) return inp;
      //a local variable (hides globals)
      foreach(KeyValuePair<string, List<string>> type in ctx)
      {
        for(int c=0; c<type.Value.Count; c++)
        {
          if(type.Value[c]==inp)
          {
            if(type.Key.Substring(0,3) == "REF") //reference varialbe
            {
            	string refType=type.Key.Substring(3);
            	string refName="PWRPLUSREF"+inp;
            	for(int d=0; d<ctx["INT"].Count; d++)
            		if(ctx["INT"][d]==refName)
            			return "ST"+refType+"(STINT(SPINT-"+(ctx["INT"].Count-d)+"))";
            }
            else //stack-immediate variables (this could also be an array reference)
            	return "ST"+type.Key+"(SP"+type.Key+"-"+(type.Value.Count-c)+")";
          }
        }
      }
      //a global variable
      foreach(Global curr in Globals) if(curr.Name == inp) return inp;
      //a Symitar variable
      foreach(string sys in SymVariables) if(sys == inp) return inp;
      //not resolved
      PwrError.SetFatal("Unknown Variable \"" + inp + "\"", -1, 0x021);
      errFlag = true;
      return "";
    }
    private string NameVar(Symbol node, Dictionary<string, List<string>> ctx)
    {
      if(errFlag) return "";
      int i; string inp=NodeValue(node);
			//system variable
			if(inp[0]=='@') return inp;
      //a const value
      for(i=0; i<Consts.Count; i++) if(Consts[i].Name == inp) return Consts[i].Name;
      //a record - w/ subrecord or field
      if(inp.Contains(":") || inp.Contains(".")) return inp.Replace(':', ' ').Replace('.', ':');
      //a record - top level
      foreach(string tlc in SymRecords) if(tlc == inp) return inp;
      //a local variable (hides globals)
      foreach(KeyValuePair<string, List<string>> type in ctx)
      {
        for(int c=0; c<type.Value.Count; c++)
        {
          if(type.Value[c]==inp)
          {
            if(type.Key.Substring(0,3) == "REF") //reference varialbe
            {
            	string refType=type.Key.Substring(3);
            	string refName="PWRPLUSREF"+inp;
            	for(int d=0; d<ctx["INT"].Count; d++)
            		if(ctx["INT"][d]==refName)
            			return "ST"+refType+"(STINT(SPINT-"+(ctx["INT"].Count-d)+"))";
            }
            else //stack-immediate variables
            	return "ST"+type.Key+"(SP"+type.Key+"-"+(type.Value.Count-c)+")";
          }
        }
      }
      //a global variable
      foreach(Global curr in Globals) if(curr.Name == inp) return inp;
      //a Symitar variable
      foreach(string sys in SymVariables) if(sys == inp) return inp;
      //not resolved
      PwrError.SetFatal("Unknown Variable \"" + inp + "\"", NodeLine(node), 0x021);
      errFlag = true;
      return "";
    }
		private string FmNameVar(Symbol node, Dictionary<string, List<string>> ctx)
		{
			if(errFlag) return "";
			string retval = NameVar(node,ctx);
			if(!errFlag) return retval;

      PwrError.SetFatal("Unknown Error", -1, 0xFFF);
			errFlag = false;
			return NameLit(node);
		}
    private List<Variable> ParamList(Symbol node)
    {
      List<Variable> p = new List<Variable>();
      for(int i=0; i<node.kid.Count; i++)
      {
      	if(node.kid[i].kid[1].typ=="ParamArrRef")
      		p.Add(new Variable('['+NodeValue(node.kid[i].kid[1].kid[0]), DataType(node.kid[i].kid[0])));
      	else
        	p.Add(new Variable(NodeValue(node.kid[i].kid[1]), DataType(node.kid[i].kid[0])));
      }
      return p;
    }
    private List<Symbol> ValueList(Symbol node)
    {
      List<Symbol> v = new List<Symbol>();
      if(node.typ!="ValueList") return v;
      v.Add(node.kid[0]);
      for(int i=1; i<node.kid.Count; i++)
        v.Add(node.kid[i].kid[0]);
      return v;
    }
    private void ReplaceFrameNode(Symbol node, Dictionary<string, List<string>> ctx, string type)
    {
      string tmpName = "PWRPLUSINLINE"+inlineCounter.ToString("D9");
      ContextPush(ctx,type,tmpName);
      scan.tokVal[node.kid[0].idx]=tmpName;
      node.typ="Name";
      node.idx=node.kid[0].idx;
      node.kid = new List<Symbol>();
      inlineCounter++;
    }
    //--------------------------------------------------------------------------------------------------------------------------
    //Generation
    //--------------------------------------------------------------------------------------------------------------------------
    private bool GenRoot()
    {
      if (errFlag) return false;
      string Mode="",Target=""; int i=0;
      while((i<root.kid.Count) && (root.kid[i].typ=="RootCall"))
      {
        string proc = NodeValue(root.kid[i].kid[0]);
        List<Symbol> args = ValueList(root.kid[i].kid[1]);
        switch(proc)
        {
          case "MODE":
            if(args.Count<1){PwrError.SetFatal("Mode Takes At Least One Parameter", NodeLine(root.kid[i].kid[0]), 0x026); errFlag=true; return false;}
            for(int c=0; c<args.Count; c++)
            {
              if(args[c].typ!="Name"){PwrError.SetFatal("Mode Parameter Must Be An UnQuoted Mode Name", NodeLine(root.kid[i].kid[1]), 0x027); errFlag=true; return false;}
              Mode+=NodeValue(args[c])+'\n';
            }
            break;
          case "TARGET":
            if(args.Count!=1)      {PwrError.SetFatal("Target takes exactly ONE parameter", NodeLine(root.kid[i].kid[1]), 0x028);                 errFlag=true; return false;}
            if(args[0].typ!="Name"){PwrError.SetFatal("Target's parameter should be an Unquoted Recrod Name", NodeLine(root.kid[i].kid[1]), 0x029); errFlag=true; return false;}
            Target=Literal(args[0]);
            break;
          case "STACKSIZE":
            if(args.Count!=2)         {PwrError.SetFatal("StackSize takes exactly TWO parameters", NodeLine(root.kid[i].kid[1]), 0x02A);                          errFlag=true; return false;}
            if(args[0].typ!="Name")   {PwrError.SetFatal("StackSize's first parameter should be an Unquoted DataType Name", NodeLine(root.kid[i].kid[1]), 0x02B); errFlag=true; return false;}
            if(args[1].typ!="Literal"){PwrError.SetFatal("StackSize's second parameter should be a Literal Number Value", NodeLine(root.kid[i].kid[1]), 0x02C);   errFlag=true; return false;}
            StackSize[DataType(args[0])]=int.Parse(Literal(args[1]));
            break;
          case "TITLE":
            if(args.Count!=1)         {PwrError.SetFatal("Title takes exactly ONE parameter", NodeLine(root.kid[i].kid[1]), 0x02D);                      errFlag=true; return false;}
            if(args[0].typ!="Literal"){PwrError.SetFatal("Title's parameter should be a Quoted Character Literal", NodeLine(root.kid[i].kid[1]), 0x02E); errFlag=true; return false;}
            Title=Literal(args[0]);
            break;
					case "DATAFILE":
						if(args.Count!=3)         {PwrError.SetFatal("DataFile takes exactly THREE parameters",                                           NodeLine(root.kid[i].kid[1]), 0x034); errFlag=true; return false;}
						if(args[0].typ!="Literal"){PwrError.SetFatal("DataFile's first parameter should be a quoted string value (\"ASCII\"/\"EBCDIC\")", NodeLine(root.kid[i].kid[1]), 0x034); errFlag=true; return false;}
						if(args[1].typ!="Literal"){PwrError.SetFatal("DataFile's second parameter should be a number literal",                            NodeLine(root.kid[i].kid[1]), 0x034); errFlag=true; return false;}
						if(args[2].typ!="Literal"){PwrError.SetFatal("DataFile's third parameter should be a number literal",                             NodeLine(root.kid[i].kid[1]), 0x034); errFlag=true; return false;}
						DataFile = " DATAFILE "+Literal(args[0]).Replace("\"","")+" RECORDSIZE="+Literal(args[1])+" BLOCKSIZE="+Literal(args[2]);
						break;
        }
        i++;
      }

      //RepGen Source
      if(Mode.Length  >0) SectionOutput["ROOT"]=Mode.Replace("SUBROUTINEDEMAND","SUBROUTINE DEMAND");
      if(Target.Length>0) SectionOutput["ROOT"]+="TARGET="+Target+'\n';
      SectionOutput["ROOT"] += '\n';
      return true;
    }
    private bool GenDefine()
    {
      if (errFlag) return false;
      int i=0;
      while((i<root.kid.Count)&&(root.kid[i].typ!="SectGlobal")) i++;
      if(i<root.kid.Count)
      {
	      if(root.kid[i].typ=="SectGlobal")
	      {
	        Symbol glob=root.kid[i];
	        for(i=0; i<glob.kid.Count; i++) //glob.kid[i].typ=="GlobDef"
	        {
	          string tipe="";
	          for(int c=0; c<glob.kid[i].kid.Count; c++) //glob.kid[i].idx[c].typ=="GDefBlockA/B"
	          {
	            List<Symbol> blk=glob.kid[i].kid[c].kid; int off=0; string name="",init="";
	            if(blk[0].typ=="GlobType"){tipe=DataType(blk[0]); off=1;}
	            if(blk[off].typ=="Name"){name=NodeValue(blk[off]);}
	            if(blk[off].typ=="AssignBlock")
	            {
	              name=NodeValue(blk[off].kid[0]);
	              if(NodeValue(blk[off].kid[1])!="="){PwrError.SetFatal("Definition Assignments Cannot Use '"+NodeValue(blk[off].kid[1])+"'", NodeLine(blk[off].kid[1]), 0x02F); errFlag=true; return false;}
	              if(blk[off].kid[2].typ!="Literal") {PwrError.SetFatal("Global Definition Initializations Must Use a Literal Value",     NodeLine(blk[off].kid[1]), 0x030);     errFlag=true; return false;}
	              init=NameLit(blk[off].kid[2]);
	            }
	            if(tipe=="CONST") if(init==""){PwrError.SetFatal("Const Definitions Must Be Initialized to a Literal Value", NodeLine(blk[0]), 0x031); errFlag=true; return false;}
	            if(tipe=="CONST") Consts.Add(new Variable(name, init));
	            else              Globals.Add(new Global(name, tipe, init));
	          }
	        }
	      }
    	}
      //Root RepGen Source
      string txt="";
      
      //stack stuff
      bool anyStack = false;
      foreach(KeyValuePair<string, int> curr in StackSize)
      {
        if(curr.Value>0)
        {
        	string st=SymType(curr.Key);
        	if(curr.Key.Substring(0,3)!="ARR") txt +="IM"+curr.Key+" = "+st+"\n";
          txt +="SP"+curr.Key+" = NUMBER\nST"+curr.Key+" = "+st+" ARRAY("+curr.Value.ToString()+")\n";
          anyStack = true;
        }
      }
      if(anyStack)
      	txt += "STACKSUB = NUMBER\n";

      //globals
      foreach(Global curr in Globals)
        txt += curr.Name+" = "+SymType(curr.Type)+'\n';
      if(txt.Length>0)
        SectionOutput["DEFINE"] = txt;
      return true;
    }
    private bool GenProcedures()
    {
      if (errFlag) return false;
      string ret = ""; int c;
      for(int i=0; i<root.kid.Count; i++) //first parse all procedure definitions (so we can call them from procedure bodies)
        if(root.kid[i].typ=="Procedure")
        {
          string tipe=DataType(root.kid[i].kid[0]), name=NodeValue(root.kid[i].kid[1]);
          List<Variable> parms=new List<Variable>();
          if(root.kid[i].kid[2].typ=="ParamList")
          {
            parms=ParamList(root.kid[i].kid[2]);
            //make some modifications for any Reference/Array-Reference parameters
            for(int r=0; r<parms.Count; r++)
            {
	            if(parms[r].Name[0]=='&') parms[r] = new Variable(parms[r].Name.Substring(1), "REF"+parms[r].Type);
            	if(parms[r].Name[0]=='[') parms[r] = new Variable(parms[r].Name.Substring(1), "ARR"+parms[r].Type);
            }
          }
          Procedures.Add(new Procedure(name,tipe,parms));
        }
      for(int i=0; i<root.kid.Count; i++) //now work on procedure bodies
        if(root.kid[i].typ=="Procedure")
        {
          string name=NodeValue(root.kid[i].kid[1]); int exp1st=2;
          Dictionary<string, List<string>> ctx=ContextCreate();
          if(root.kid[i].kid[2].typ=="ParamList")
          {
            //build context from parameters
            Procedure proc = new Procedure(); //doing this to make csc happy, though it _will_ be found, we already have found it
            for(c=0; c<Procedures.Count; c++) if(name==Procedures[c].Name){proc=Procedures[c]; break;}
            for(c=0; c<proc.Param.Count; c++)
            {
            	if(proc.Param[c].Type.Substring(0,3)=="ARR")
            	{
            		ContextPush(ctx,"INT",proc.Param[c].Name);
            		ContextPush(ctx,proc.Param[c].Type,"PWRPLUSAR2"+proc.Param[c].Name); //this PWRPLUSAR_2_ is intentional to ward off ContextKill
            	}
            	else
            	{
	            	ContextPush(ctx,proc.Param[c].Type,proc.Param[c].Name);
	            	if(proc.Param[c].Type.Substring(0,3)=="REF") ContextPush(ctx,"INT","PWRPLUSREF"+proc.Param[c].Name);
	            }
            }
            exp1st=3;
          }
          
          ret+="PROCEDURE "+name+'\n';
          for(c=exp1st; (c<root.kid[i].kid.Count)&&(root.kid[i].kid[c].typ!="ReturnBlk"); c++){string r=EvalNode(root.kid[i].kid[c], ctx); if(r=="")return false; ret+=r+'\n';}
          if((c<root.kid[i].kid.Count)&&(root.kid[i].kid[c].typ=="ReturnBlk")) //returning parameters
          {
            string tipe="";
            for(int a=0;a<Procedures.Count;a++)if(Procedures[a].Name==name)tipe=Procedures[a].Type;
            Frame blk=PreBlock(root.kid[i].kid[c].kid[0],ctx);   if(errFlag)return false;
            ret +=blk.pre+"IM"+tipe+"="+EvalNode(root.kid[i].kid[c].kid[0],blk.ctx)+blk.post+'\n';
          }
          string ctxKill = ContextKill(ctx);
          if(ctxKill.Length>0) ctxKill+='\n';
          ret+=ctxKill;
          ret+="END\n\n";
        }
      SectionOutput["PROCEDURES"]=ret;
      return true;
    }
    private bool GenSetup()
    {
      if (errFlag) return false;
      string ret="";
      for(int i=0; i<root.kid.Count; i++)
      {
        if(root.kid[i].typ=="SectSetup")
        {
           ret = "SETUP\n"; int c; Dictionary<string, List<string>> ctx=this.ContextCreate();
          for(c=0; c<this.Globals.Count; c++) if(this.Globals[c].Init!="") ret+=this.Globals[c].Name+'='+this.Globals[c].Init+'\n';
          for(c=0; c<root.kid[i].kid.Count; c++){string w=EvalNode(root.kid[i].kid[c],ctx); if(errFlag)return false; ret+=w+'\n';}
          string ctxKill = ContextKill(ctx);
          if(ctxKill.Length>0) ctxKill+='\n';
          ret+=ctxKill;
          ret+="END\n\n";
          SectionOutput["SETUP"]=ret;
        }
      }
      return true;
    }
    private bool GenProcess()
    {
      if (errFlag) return false;
      string ret = "";
      for(int i=0; i<root.kid.Count; i++)
      {
        if (root.kid[i].typ == "SectProcess")
        {
          ret = "PRINT TITLE="+Title+DataFile+'\n'; int c;
          Dictionary<string, List<string>> ctx = this.ContextCreate();
          for (c = 0; c < root.kid[i].kid.Count; c++){ string w=EvalNode(root.kid[i].kid[c], ctx); if(errFlag) return false; ret+=w+'\n'; }
          string ctxKill = ContextKill(ctx); if(ctxKill.Length>0) ctxKill+='\n';
          ret += ctxKill;
          ret += "END\n\n";
          SectionOutput["PROCESS"] = ret;
        }
      }
      return true;
    }
    private bool GenFinal()
    {
      if (errFlag) return false;
      string ret = "";
      for(int i=0; i<root.kid.Count; i++)
      {
        if (root.kid[i].typ == "SectFinal")
        {
          ret = "TOTAL\n"; int c;
          Dictionary<string, List<string>> ctx = this.ContextCreate();
          for (c = 0; c < root.kid[i].kid.Count; c++){ string w=EvalNode(root.kid[i].kid[c], ctx); if(errFlag) return false; ret+=w+'\n'; }
          string ctxKill = ContextKill(ctx); if(ctxKill.Length>0) ctxKill+='\n';
          ret += ctxKill;
          ret += "END\n\n";
          SectionOutput["FINAL"] = ret;
        }
      }
      return true;
    }
    private bool GenSelect()
    {
      if(errFlag) return false;
      for(int i=0; i<root.kid.Count; i++)
      if (root.kid[i].typ == "SectSelect")
      {
        Dictionary<string, List<string>> ctx = this.ContextCreate();
        if(root.kid[i].kid.Count>0)
        {
          string w = EvalNode(root.kid[i].kid[0], ctx);
          if(errFlag) return false;
          SectionOutput["SELECT"] = "SELECT\n"+w+"\nEND\n\n";
        }
      }
      return true;
    }
    private bool GenSort()
    {
      if(errFlag) return false;
      for(int i=0; i<root.kid.Count; i++)
        if (root.kid[i].typ == "SectSort")
        {
          string w = EvalNode(root.kid[i].kid[0], ContextCreate());
          if(errFlag) return false;
          SectionOutput["SORT"] = "SORT\n" + w + "\nEND\n\n";
        }
      return true;
    }
    //--------------------------------------------------------------------------------------------------------------------------
    //Node Processing Generation
    //--------------------------------------------------------------------------------------------------------------------------
    private string EvalNode(Symbol node, Dictionary<string, List<string>> ctx)
    {
      if(errFlag) return "";
      
      int i, idx;
      bool match=false;
      string pre, post, op, val, checkee, name;
      string[] checker;
      StringBuilder ret;
      Frame blk;
      List<Symbol> passing;
      Dictionary<string, List<string>> context;

      switch (node.typ)
      {
        //virtual nodes
        case "Terminal": return EvalNode(node.kid[0], ctx);
        case "ValueB"  : return EvalNode(node.kid[0], ctx);

        //value nodes
        case "NONE"       : return "NONE";
        case "Name"       : return NameVar(node,ctx);
        case "Literal"    : return Literal(node);
        case "AssignOp"   : return NodeValue(node);
        case "NonAssignOp":
          val = NodeValue(node);
          switch(val)
          {
            case "&&": return " AND ";
            case "||": return " OR ";
            case "!=": return " <> ";
            case "==": return " = ";
            default  : return ' '+val+' ';
          }

        //function nodes
        case "ArrayRef":
        	name = NodeValue(node.kid[0]); val=""; //array name
        	foreach(KeyValuePair<string, List<string>> curr in ctx) //lookup array type
        		if(curr.Value.Contains("PWRPLUSARR"+name) || curr.Value.Contains("PWRPLUSAR2"+name)) { val = curr.Key.Substring(3); break; }
        	op = NameVar(node.kid[0],ctx);   //get array base
        	blk = PreBlock(node.kid[1],ctx); //get array offset
        	return blk.pre+"STARR"+val+"( "+op+" + "+EvalNode(node.kid[1],blk.ctx)+" )";
        	
      	case "ArrayAssign":
      		string which="", where="";
      		blk   = PreBlock(node.kid[2] ,    ctx); if(errFlag)return "";
          which = EvalNode(node.kid[0] ,blk.ctx); if(errFlag)return "";
          op    = EvalNode(node.kid[1] ,blk.ctx); if(errFlag)return "";
          val   = EvalNode(node.kid[2] ,blk.ctx); if(errFlag)return "";
          where = (op=="=") ? which+" = " : which+" = "+which+' '+op[0]+' ';
          return blk.pre+where+val+blk.post;
        
				case "SetProc":
					return "SET "+FmNameVar(node.kid[0],ctx)+" TO "+EvalNode(node.kid[1],ctx);
					
				case "FmPerform":
					passing = ValueList(node.kid[0]);
					if(FmNameVar(passing[0],ctx)=="CREATE") idx=5; else idx=4;
					if(passing.Count<idx){PwrError.SetFatal("This FmPerform Takes at least "+idx.ToString()+" Parameters", NodeLine(node.kid[0]), 0x039); errFlag=true; return "";}
					ret = new StringBuilder();
					if(idx==5) ret.Append("SYMPROCINT0= "+EvalNode(passing[passing.Count-2],ctx)+'\n');
					ret.Append("SYMPROCCHR0= "+EvalNode(passing[passing.Count-1],ctx)+'\n');
					ret.Append("FMPERFORM ");
					for(i=0; i<(passing.Count-(idx-1)); i++) ret.Append(FmNameVar(passing[i],ctx)+' ');
					ret.Append("( "+EvalNode(passing[passing.Count-(idx-1)],ctx));
					ret.Append(", "+EvalNode(passing[passing.Count-(idx-2)],ctx));
					if(idx==5) ret.Append(", SYMPROCINT0, SYMPROCCHR0 ) DO\n"); else ret.Append(", SYMPROCCHR0 ) DO\n");
					for(i=1; i<node.kid.Count; i++) ret.Append(EvalNode(node.kid[i],ctx)+'\n');
					ret.Append("END\n");
					if(idx==5) ret.Append(EvalNode(passing[passing.Count-2],ctx)+" =SYMPROCINT0\n");
					ret.Append(EvalNode(passing[passing.Count-1],ctx)+" =SYMPROCCHR0");
					return ret.ToString();
					
				case "TranPerform":
					passing = ValueList(node.kid[0]);
					if(passing.Count<7){PwrError.SetFatal("TranPerform Takes Exactly 7 Parameters", NodeLine(node.kid[0]), 0x03B); errFlag=true; return "";}
					ret = new StringBuilder();
					ret.Append("SYMPROCINT0= "+EvalNode(passing[2],ctx)+'\n');
					ret.Append("SYMPROCINT1= "+EvalNode(passing[3],ctx)+'\n');
					ret.Append("SYMPROCCHR0= "+EvalNode(passing[4],ctx)+'\n');
					ret.Append("SYMPROCCHR1= "+EvalNode(passing[5],ctx)+'\n');
					ret.Append("SYMPROCCHR2= "+EvalNode(passing[6],ctx)+'\n');
					ret.Append("TRANPERFORM ");
					ret.Append(FmNameVar(passing[0],ctx)+' ');
					ret.Append("( "+EvalNode(passing[1],ctx));
					ret.Append(", SYMPROCINT0, SYMPROCINT1, SYMPROCCHR0, SYMPROCCHR1, SYMPROCCHR2 ) DO\n");
					for(i=1; i<node.kid.Count; i++) ret.Append(EvalNode(node.kid[i],ctx)+'\n');
					ret.Append("END\n");
					ret.Append(EvalNode(passing[2],ctx)+" =SYMPROCINT0\n");
					ret.Append(EvalNode(passing[3],ctx)+" =SYMPROCINT1\n");
					ret.Append(EvalNode(passing[4],ctx)+" =SYMPROCCHR0\n");
					ret.Append(EvalNode(passing[5],ctx)+" =SYMPROCCHR1\n");
					ret.Append(EvalNode(passing[6],ctx)+" =SYMPROCCHR2");
					return ret.ToString();
				
				case "HeaderBlock":
					return "HEADERS\n"+EvalNode(node.kid[0], ctx)+"\nEND";

				case "TrailerBlock":
					return "TRAILERS\n"+EvalNode(node.kid[0], ctx)+"\nEND";

        case "SubExpr":
          string expr = EvalNode(node.kid[0], ctx);
          return '('+expr+')';

        case "StmntBlock": //gets it's own stack frame
          context = ContextCopy(ctx);
          string block = "";
          for(i=0; (i<node.kid.Count) && (!errFlag); i++){if(i>0) block+='\n'; block+=EvalNode(node.kid[i],context);}
          string noCtx = ContextKillDiff(ctx, context);
          if(noCtx.Length>0) block+='\n'+noCtx;
          return block;

        case "PreUnary":
          op  = NodeValue(node.kid[0]);
          blk = PreBlock(node.kid[1], ctx);
          if(op[0]== '-') return blk.pre+"-("+EvalNode(node.kid[1], blk.ctx)+')'+blk.post;
          return "";
          
        case "PostUnary":
          blk = PreBlock(node.kid[0], ctx);
          val = EvalNode(node.kid[0], blk.ctx);
          op  = NodeValue(node.kid[1]);
          if(op=="++") return blk.pre+val+" = "+val+" + 1"+blk.post;
          if(op=="--") return blk.pre+val+" = "+val+" - 1"+blk.post;
          return "";
          
        case "AnyBlock":
          val = NameVar(node.kid[0],ctx);
          if(errFlag) return "";
          if(node.kid.Count==1) return "ANY "+val;
          return "ANY "+val+" WITH ( "+EvalNode(node.kid[1],ctx)+" )";
          
        case "NotAnyBlock":
          val = NameVar(node.kid[0],ctx);
          if(errFlag) return "";
          if(node.kid.Count==1) return "NOT ANY "+val;
          return "NOT ANY "+val+" WITH "+EvalNode(node.kid[1],ctx);

        case "Built_In":
          blk = PreBlock(node.kid[0], ctx);
          pre = blk.pre; post = blk.post; context = blk.ctx;
          for(i=0; i<node.kid[1].kid.Count; i++)
          {
            blk = PreBlock(node.kid[1].kid[i], context);
            pre+=blk.pre; post=blk.post+post; context=blk.ctx;
          }
          checkee = EvalNode(node.kid[0], context);
          checker = new string[node.kid[1].kid.Count];
          for(i=0; i<node.kid[1].kid.Count; i++)
            checker[i] = EvalNode(node.kid[1].kid[i], context);
          ret = new StringBuilder();
          ret.Append(pre);
          ret.Append('(');
          for(i=0; i<checker.Length; i++)
          {
            ret.Append('(');
            ret.Append(checkee);
            ret.Append('=');
            ret.Append(checker[i]);
            ret.Append(')');
            if (i<(checker.Length-1)) ret.Append(" OR ");
          }
          ret.Append(')');
          ret.Append(post);
          return ret.ToString();

        case "Built_NotIn":
          blk = PreBlock(node.kid[0], ctx);
          pre = blk.pre; post = blk.post; context = blk.ctx;
          for(i=0; i<node.kid[1].kid.Count; i++)
          {
            blk = PreBlock(node.kid[1].kid[i], context);
            pre+=blk.pre; post=blk.post+post; context=blk.ctx;
          }
          checkee = EvalNode(node.kid[0], context);
          checker = new string[node.kid[1].kid.Count];
          for(i=0; i<node.kid[1].kid.Count; i++)
            checker[i] = EvalNode(node.kid[1].kid[i], context);
          ret = new StringBuilder();
          ret.Append(pre);
          ret.Append('(');
          for(i=0; i<checker.Length; i++)
          {
            ret.Append('(');
            ret.Append(checkee);
            ret.Append("<>");
            ret.Append(checker[i]);
            ret.Append(')');
            if(i<(checker.Length-1)) ret.Append(" AND ");
          }
          ret.Append(')');
          ret.Append(post);
          return ret.ToString();

        case "Definition":
          int nOff;
          string tipe="", s=""; name="";
          for(i=0; i<node.kid.Count; i++)
          {
            nOff = 0;
            if(node.kid[i].kid[0].typ=="VarType"){nOff=1; tipe=DataType(node.kid[i].kid[0]);}
            if(node.kid[i].kid[nOff].typ == "Name")
            {
            	name=NodeValue(node.kid[i].kid[nOff]);
            	s+="CALL PUSH"+tipe+'\n';
            	ContextPush(ctx,tipe,name);
            }
            else if(node.kid[i].kid[nOff].typ == "ArrayRef")
            {
            	name=NodeValue(node.kid[i].kid[nOff].kid[0]);
            	blk =PreBlock(node.kid[i].kid[nOff].kid[1],ctx);
            	s+= blk.pre+"IMINT = "+EvalNode(node.kid[i].kid[nOff].kid[1],blk.ctx)+"\nCALL PUSHARRINT\nCALL PUSHINT\n";
            	ContextPush(ctx,"INT",name);
            	ContextPush(ctx,"ARR"+tipe,"PWRPLUSARR"+name);
            }
	          else
            {
            	name=NodeValue(node.kid[i].kid[nOff].kid[0]);
            	s+="CALL PUSH"+tipe+'\n';
            	ContextPush(ctx,tipe,name);
            }
          }
          for(i=0; i<node.kid.Count; i++)
          {
            nOff=0; if(node.kid[i].kid[0].typ=="VarType") nOff=1;
            if(node.kid[i].kid[nOff].typ=="AssignBlock") s+=EvalNode(node.kid[i].kid[nOff],ctx)+'\n';
          }
          if(s[s.Length-1]=='\n') return s.Substring(0,s.Length-1);
          return s;

        case "AssignBlock":
          string who="", how="";
          blk = PreBlock(node.kid[2] ,    ctx); if(errFlag)return "";
          who = NameVar( node.kid[0] ,blk.ctx); if(errFlag)return "";
          op  = EvalNode(node.kid[1] ,blk.ctx); if(errFlag)return "";
          val = EvalNode(node.kid[2] ,blk.ctx); if(errFlag)return "";
          how = (op=="=") ? who+" = " : who+" = "+who+' '+op[0]+' ';
          return blk.pre+how+val+blk.post;

        case "ProcCall":
          name = NodeValue(node.kid[0]); match=false;
          for(i=0; i<SymProcedures.Length; i++) if(SymProcedures[i]==name){match=true; break;}
          if (match) //this is a SymProcedure
          {
            if(node.kid.Count==1) return name;
            string passStr = "";
            passing = ValueList(node.kid[1]);
            Frame prePass = PreBlock(node.kid[1], ctx);
            switch(name)  //special case Symitar's oddities
            {
            	//special call formatting
              case "COL":
                if(VerifyArgCount(passing,2,node.kid[0])==false) return "";
                return prePass.pre+"COL="+EvalNode(passing[0],prePass.ctx)+' '+EvalNode(passing[1],prePass.ctx)+prePass.post;
              case "PRINT":
              	if(VerifyArgCount(passing,1,node.kid[0])==false) return "";
              	return prePass.pre+"PRINT "+EvalNode(passing[0],prePass.ctx)+prePass.post;
							case "HEADER":
								if(VerifyArgCount(passing,1,node.kid[0])==false) return "";
								return prePass.pre+"HEADER= "+EvalNode(passing[0],prePass.ctx)+prePass.post;
              //special variable passing
              case "INITSUBROUTINE":
              	if(VerifyArgCount(passing,1,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[0],prePass.ctx)+'\n';
                passStr+="INITSUBROUTINE(SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[0],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "EMAILSEND":
              	if(VerifyArgCount(passing,1,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[0],prePass.ctx)+'\n';
                passStr+="EMAILSEND(SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[0],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "EXECUTE":
                if(VerifyArgCount(passing,2,node.kid[0])==false) return "";
                passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[1],prePass.ctx)+'\n';
                passStr+="EXECUTE("+EvalNode(passing[0],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[1],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "EMAILLINE":
              	if(VerifyArgCount(passing,2,node.kid[0])==false) return "";
                passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[1],prePass.ctx)+'\n';
                passStr+="EMAILLINE("+EvalNode(passing[0],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[1],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "EMAILSTART":
              	if(VerifyArgCount(passing,4,node.kid[0])==false) return "";
                passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[3],prePass.ctx)+'\n';
                passStr+="EMAILSTART("+EvalNode(passing[0],prePass.ctx)+','+EvalNode(passing[1],prePass.ctx)+','+EvalNode(passing[2],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[3],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILELISTCLOSE":
              	if(VerifyArgCount(passing,1,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[0],prePass.ctx)+'\n';
                passStr+="FILELISTCLOSE(SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[0],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILECLOSE":
                if(VerifyArgCount(passing,2,node.kid[0])==false) return "";
                passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[1],prePass.ctx)+'\n';
                passStr+="FILECLOSE("+EvalNode(passing[0],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[1],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILELISTREAD":
              	if(VerifyArgCount(passing,2,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[1],prePass.ctx)+'\n';
                passStr+="FILELISTREAD("+EvalNode(passing[0],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[1],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILECREATE":
              	if(VerifyArgCount(passing,3,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[2],prePass.ctx)+'\n';
                passStr+="FILECREATE("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILEDELETE":
              	if(VerifyArgCount(passing,3,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[2],prePass.ctx)+'\n';
                passStr+="FILEDELETE("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILELISTOPEN":
              	if(VerifyArgCount(passing,3,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[2],prePass.ctx)+'\n';
                passStr+="FILELISTOPEN("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILEWRITELINE":
              	if(VerifyArgCount(passing,3,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[2],prePass.ctx)+'\n';
                passStr+="FILEWRITELINE("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILESETPOS":
              	if(VerifyArgCount(passing,3,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[2],prePass.ctx)+'\n';
                passStr+="FILESETPOS("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
                return passStr;
              case "FILEGETPOS":
              	if(VerifyArgCount(passing,3,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[2],prePass.ctx)+'\n';
                passStr+="SYMPROCINT0="+EvalNode(passing[1],prePass.ctx)+'\n';
                passStr+="FILEGETPOS("+EvalNode(passing[0],prePass.ctx)+", SYMPROCINT0, SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR0\n";
                passStr+=EvalNode(passing[1],prePass.ctx)+"=SYMPROCINT0";
                passStr+=prePass.post;
                return passStr;
              case "FILEREADLINE":
              	if(VerifyArgCount(passing,3,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[2],prePass.ctx)+'\n';
                passStr+="SYMPROCCHR1="+EvalNode(passing[1],prePass.ctx)+'\n';
                passStr+="FILEREADLINE("+EvalNode(passing[0],prePass.ctx)+", SYMPROCCHR1, SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR0\n";
                passStr+=EvalNode(passing[1],prePass.ctx)+"=SYMPROCCHR1";
                passStr+=prePass.post;
                return passStr;
              case "FILEREAD":
              	if(VerifyArgCount(passing,4,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[3],prePass.ctx)+'\n';
                passStr+="SYMPROCCHR1="+EvalNode(passing[2],prePass.ctx)+'\n';
                passStr+="FILEREAD("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", SYMPROCCHR1, SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[3],prePass.ctx)+"=SYMPROCCHR0\n";
                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR1";
                passStr+=prePass.post;
                return passStr;
              case "FILEOPEN":
              	if(VerifyArgCount(passing,5,node.kid[0])==false) return "";
              	passStr=prePass.pre;
                passStr+="SYMPROCCHR0="+EvalNode(passing[4],prePass.ctx)+'\n';
                passStr+="SYMPROCINT0="+EvalNode(passing[3],prePass.ctx)+'\n';
                passStr+="FILEOPEN("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", "+EvalNode(passing[2],prePass.ctx)+", SYMPROCINT0, SYMPROCCHR0)\n";
                passStr+=EvalNode(passing[4],prePass.ctx)+"=SYMPROCCHR0\n";
                passStr+=EvalNode(passing[3],prePass.ctx)+"=SYMPROCINT0";
                passStr+=prePass.post;
                return passStr;
              case "FILEWRITE":
                if((passing.Count!=3) && (passing.Count!=4))
                {
                  PwrError.SetFatal("FILEWRITE Takes 3 or 4 Parameters", NodeLine(node.kid[0]), 0x032);
                	errFlag=true;
                	return "";
                }
                if(passing.Count==3)
                {
                	passStr=prePass.pre;
	                passStr+="SYMPROCCHR0="+EvalNode(passing[2],prePass.ctx)+'\n';
	                passStr+="FILEWRITE("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", SYMPROCCHR0)\n";
	                passStr+=EvalNode(passing[2],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
	                return passStr;
                }
                else
                {
                	passStr=prePass.pre;
	                passStr+="SYMPROCCHR0="+EvalNode(passing[3],prePass.ctx)+'\n';
	                passStr+="FILEWRITE("+EvalNode(passing[0],prePass.ctx)+", "+EvalNode(passing[1],prePass.ctx)+", "+EvalNode(passing[2],prePass.ctx)+", SYMPROCCHR0)\n";
	                passStr+=EvalNode(passing[3],prePass.ctx)+"=SYMPROCCHR0"+prePass.post;
	                return passStr;
                }
              //regular SymProcedure
              default:
                for(i=0; i<passing.Count; i++)
                {
                  if(i>0)passStr+=", ";
                  passStr+=EvalNode(passing[i],prePass.ctx);
                }
                return prePass.pre+name+'('+passStr+')'+prePass.post;
            }
          }
          match=false; idx=-1;
          for(i=0; i<Procedures.Count; i++) if(Procedures[i].Name==name){match=true; idx=i;break;}
          if(!match){PwrError.SetFatal("Unknown Procedure \""+name+'\"', NodeLine(node.kid[0]), 0x24); errFlag=true; return "";}

          if(node.kid.Count>1) passing=ValueList(node.kid[1]); else passing = new List<Symbol>();
          if(Procedures[idx].Param.Count!=passing.Count){PwrError.SetFatal("Wrong Number of Parameters to Procedure \""+name+"\" (should be "+Procedures[idx].Param.Count.ToString()+" )",NodeLine(node.kid[0]),0x025); errFlag=true; return "";}

          if(passing.Count==0) return "CALL "+name; //no-param call

          pre=""; post=""; context=ctx;
          for(i=0; i<passing.Count; i++)
          {
            blk=PreBlock(passing[i],context);
            pre+=blk.pre; post=blk.post+post; context=blk.ctx;
          }
          string stack = ""; int refPasses=0; Dictionary<string, List<string>> passOnly=ContextCopy(context);
          for(i=0; i<passing.Count; i++)
          {
            string x=Procedures[idx].Param[i].Type;
            if(x.Substring(0,3)=="REF")
            {
            	x = EvalNode(passing[i],passOnly);
            	x = x.Substring(6,x.Length-7);
            	stack+="IMINT = "+x+"\nCALL PUSHINT\n";
            	ContextPush(passOnly,"INT","PWRPLUSPROCCALL");
            	refPasses++;
            }
            else if(x.Substring(0,3)=="ARR")
            {
            	stack+="IMINT = "+NameVar(passing[i],passOnly)+"\nCALL PUSHINT\n";
            	ContextPush(passOnly,"INT","PWRPLUSPROCCALL");
            }
            else
            {
            	stack+="IM"+x+" = "+EvalNode(passing[i],passOnly)+"\nCALL PUSH"+x+'\n';
            	ContextPush(passOnly,x,"PWRPLUSPROCCALL");
            }
          }
          return pre+stack+"CALL "+name+post;

        case "Expression":
          string body=""; context=ctx;
          blk = PreBlock(node.kid[0],ctx);
          body = EvalNode(node.kid[0],blk.ctx);
          for(i=1; i<node.kid.Count; i++)
            body += EvalNode(node.kid[i].kid[0], blk.ctx) + EvalNode(node.kid[i].kid[1], blk.ctx);
          return blk.pre+body+blk.post;

        case "CndIf":
          body=""; pre=""; post=""; context=ctx;
          blk = PreBlock(node.kid[0], ctx); if (errFlag) return "";
          pre=blk.pre; post=blk.post; context=blk.ctx;
          for(i=2; i<node.kid.Count; i++)
          {
            if(node.kid[i].typ=="CndElseIf")
            {
              blk = PreBlock(node.kid[i].kid[0],context); if(errFlag) return "";
              pre+=blk.pre; post=blk.post+post; context=blk.ctx;
            }
          }
          body="IF("+EvalNode(node.kid[0],context)+") THEN DO\n"+EvalNode(node.kid[1],context)+"\nEND";
          for(i=2; i<node.kid.Count; i++)
          {
            if(node.kid[i].typ=="CndElseIf") body+="\nELSE IF("+EvalNode(node.kid[i].kid[0],context)+") THEN DO\n"+EvalNode(node.kid[i].kid[1],context)+"\nEND";
            else body+="\nELSE DO\n"+EvalNode(node.kid[i].kid[0],context)+"\nEND";
          }
          return pre+body+post;

        case "CndForRec":
        	if(node.kid.Count==3)
        	{
          	blk = PreBlock(node.kid[1],ctx);
          	body = EvalNode(node.kid[2],ctx);
          	if(errFlag)return "";
          	return blk.pre+"FOR "+EvalNode(node.kid[0],ctx)+' '+EvalNode(node.kid[1],blk.ctx)+" DO\n"+body+"\nEND"+blk.post;
          }
          else
          {
          	blk = PreBlock(node.kid[2],ctx);
          	body = EvalNode(node.kid[3],ctx);
          	if(errFlag)return "";
          	return blk.pre+"FOR "+EvalNode(node.kid[0],ctx)+" WITH "+EvalNode(node.kid[1],ctx)+' '+EvalNode(node.kid[2],blk.ctx)+" DO\n"+body+"\nEND"+blk.post;
          }

        case "CndFor":	
          Frame frmPre   = new Frame("","",ctx);
          Frame frmCheck = new Frame("","",ctx);
          Frame frmPost  = new Frame("","",ctx);
          string strPre="", strCheck="", strPost=""; body="";
          if(node.kid.Count==2) //for(; check; ) body;
          {
          	frmCheck = PreBlock(node.kid[0],ctx);
          	strCheck = EvalNode(node.kid[0],frmCheck.ctx);
          	    body = EvalNode(node.kid[1],ctx);
          }
          if(node.kid.Count==3)
          {
          	if(node.kid[0].typ=="Expression") //for(; check; post) body;
          	{
          	  frmCheck = PreBlock(node.kid[0],ctx);
          	  strCheck = EvalNode(node.kid[0],frmCheck.ctx);
          	  frmPost  = PreBlock(node.kid[1],ctx);
          	  strPost  = EvalNode(node.kid[1],frmPost.ctx);
          	     body  = EvalNode(node.kid[2],ctx);
          	}
          	else                              //for(pre; check; ) body;
          	{
          	  frmPre   = PreBlock(node.kid[0],ctx);
          	  strPre   = EvalNode(node.kid[0],frmPre.ctx);
          	  frmCheck = PreBlock(node.kid[1],ctx);
          	  strCheck = EvalNode(node.kid[1],frmCheck.ctx);
          	      body = EvalNode(node.kid[2],ctx);
          	}
          }
          if(node.kid.Count==4)               //for(pre; check; post) body;
          {
          	frmPre   = PreBlock(node.kid[0],ctx);
						strPre   = EvalNode(node.kid[0],frmPre.ctx);
          	frmCheck = PreBlock(node.kid[1],ctx);
						strCheck = EvalNode(node.kid[1],frmCheck.ctx);
          	frmPost  = PreBlock(node.kid[2],ctx);
						strPost  = EvalNode(node.kid[2],frmPost.ctx);
							 body  = EvalNode(node.kid[3],ctx);
          }
        	ret = new StringBuilder();
        	ret.Append(frmPre.pre+strPre+frmPre.post);
					if(strPre.Length>0) ret.Append('\n');
        	ret.Append(frmCheck.pre+"WHILE( "+strCheck+" ) DO\n"+frmCheck.post);
        	ret.Append(body);
					if(strPost.Length>0) ret.Append('\n');
        	ret.Append(frmPost.pre+strPost+frmPost.post);
        	ret.Append('\n'+frmCheck.pre+"END");
        	ret.Append(frmCheck.post);
					if(frmCheck.post.Length>0) ret.Append('\n');
        	return ret.ToString();

        case "CndForEach":
          Frame blkUntil=new Frame("","",ctx), blkWith=new Frame("","",ctx);
          string untilBody="", withBody="", record=""; ret=new StringBuilder();
          record = EvalNode(node.kid[0], ctx) + ' '; if(errFlag)return "";
          body=EvalNode(node.kid[node.kid.Count-1],ctx); if(errFlag) return "";
          if(node.kid.Count>2)
          {
            blkWith = PreBlock(node.kid[1],ctx);             if(errFlag)return "";
            withBody = "\nWITH "+EvalNode(node.kid[1],blkWith.ctx)+'\n'; if(errFlag)return "";
          }
          if(node.kid.Count>3)
          {
            blkUntil = PreBlock(node.kid[2],ctx);            if(errFlag)return "";
            untilBody = "\nUNTIL "+EvalNode(node.kid[2],blkUntil.ctx); if(errFlag)return "";
          }
          ret.Append(blkWith.pre);
          ret.Append(blkUntil.pre);
          ret.Append("FOR EACH ");
          ret.Append(record);
          ret.Append(withBody);
          ret.Append("DO\n");
          ret.Append(blkUntil.post);
          ret.Append(blkWith.post);
          ret.Append(body);
          ret.Append(blkWith.pre);
          ret.Append(blkUntil.pre);
          ret.Append("\nEND");
          ret.Append(untilBody);
          ret.Append(blkUntil.post);
          ret.Append(blkWith.post);
          return ret.ToString();

        case "CndWhile":
          body="";
          blk = PreBlock(node.kid[0],ctx); if(errFlag) return "";
          return blk.pre+"WHILE("+EvalNode(node.kid[0],blk.ctx)+") DO"+blk.post+'\n'+EvalNode(node.kid[1],blk.ctx)+'\n'+blk.pre+"END"+blk.post;

        default:
          PwrError.SetFatal("Internal Error: Unknown Symbol \"" + node.typ + "\"", NodeLine(node), 0xFFF);
          errFlag = true;
          return "";
      }
    }
    private Frame PreBlock(Symbol node, Dictionary<string, List<string>> ctx)
    {
      Frame pb =new Frame(); int i;
      pb.pre = pb.post = ""; pb.ctx = ctx;
      if (errFlag) return pb;

			pb.ctx = ContextCopy(ctx);
      for(i=(node.kid.Count-1); i>-1; i--)
      {
        Frame blk = PreBlock(node.kid[i], pb.ctx); if(errFlag) return pb;
        if(blk.pre.Length >0){if(pb.pre.Length >0) if(pb.pre[pb.pre.Length-1]!='\n') pb.pre+="\n"; pb.pre +=blk.pre;}
        if(blk.post.Length>0){if(pb.post.Length>0) if(pb.post[0]            != '\n' )pb.post="\n"+pb.post; pb.post=blk.post+pb.post;}
        pb.ctx = blk.ctx;
      }
      if(node.typ=="ProcCall")
      {
        int idx=0;
        string name=NodeValue(node.kid[0]);
        foreach(string spro in SymProcedures) if(name==spro){idx=1; break;}
        if(idx==1) return pb; //SymProcedures need no special stack frame

        idx=-1;
        for(i=0; i<Procedures.Count; i++) if(Procedures[i].Name==name){idx=i; break;}
        if(idx==-1)
        {
          PwrError.SetFatal("Unknown Procedure \""+name+"\"", NodeLine(node), 0x022);
          errFlag=true;
          return pb;
        }
        if(Procedures[i].Type=="VOID") //don't want these being used as a Value-type
        {
          //PwrError.SetFatal("Void Procedures Cannot Be Nested In a Statement (\""+name+"\")",NodeLine(node.kid[0]),0x023);
          //errFlag=true;
          return pb;
        }
        if(pb.post.Length>0) if(pb.post[0]              != '\n') pb.post='\n'+pb.post;
        if(pb.pre.Length >0) if(pb.pre[pb.pre.Length-1] != '\n') pb.pre+='\n';
        pb.pre += EvalNode(node, pb.ctx);
        pb.pre += "\nCALL PUSH"+Procedures[idx].Type+'\n';
        ReplaceFrameNode(node,pb.ctx,Procedures[idx].Type);
        pb.post="\nCALL DEC"+Procedures[idx].Type+pb.post;
      }
      return pb;
    }
  }
}
