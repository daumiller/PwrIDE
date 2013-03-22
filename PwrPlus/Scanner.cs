using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PwrPlus
{
	public class Scanner
	{
		public string source;
		public List<string> tokVal;
		public List<int> tokLine;
		public Dictionary<string, List<string>> literals;
		public string seperators;
		//--------------------------------------------------------------------------------------------------------------------------
		public Scanner()
		{
			source   = null;
			tokVal   = new List<string>();
			tokLine  = new List<int>();
			literals = new Dictionary<string, List<string>>();
			literals.Add("CHAR" ,new List<string>());
			literals.Add("DATE" ,new List<string>());
			literals.Add("MONEY",new List<string>());
			literals.Add("RATE" ,new List<string>());
			literals.Add("FLOAT",new List<string>());
			literals.Add("INT"  ,new List<string>());
			seperators = @"(&&|\|\||\+\+|\-\-|\+=|\-=|\*=|\/=|!=|==|\<=|\>=|\<|\>|\+|\-|\*|\/|!|=|\(|\)|\{|\}|\[|\]|,|\?|;)";
		}
		//--------------------------------------------------------------------------------------------------------------------------
		public bool Scan(string src)
		{
			source = src + '\n';

			if(!FindInvalids()   ) return false;
			if(!ReplaceNewlines()) return false;
			if(!RemoveComments() ) return false;
			if(!PreLiterals()    ) return false;
			source = source.ToUpper();
			if(!CondenseWhitespace()) return false;
			if(!Tokenize()          ) return false;
			if(!PostLiterals()      ) return false;

			return true;
		}
		//--------------------------------------------------------------------------------------------------------------------------
		private bool FindInvalids()
		{
			int line=0, hit=-1; char invalid=' ';
			for(int i=0; i<source.Length; i++)
			{
				switch(source[i])
				{
					case '\n': line++;  break;
					case '¶': hit=line; break;
					case '§': hit=line; break;
					case '¼': hit=line; break;
					case '£': hit=line; break;
					case '‰': hit=line; break;
					case 'ƒ': hit=line; break;
					case '¤': hit=line; break;
					case '¥': hit=line; break;
				}
				if(hit>-1){ invalid=source[i]; break; }
			}
			if(hit>-1) { PwrError.SetFatal("Invalid Character '"+invalid+"'",hit,0x001); return false; }
			return true;
		}
		//--------------------------------------------------------------------------------------------------------------------------
		private bool ReplaceNewlines()
        {
            source = new Regex(@"\r\n").Replace(source,"¶"); //LF
            source = new Regex(@"\n"  ).Replace(source,"¶"); //CRLF
            return true;
        }
		//--------------------------------------------------------------------------------------------------------------------------
		private bool RemoveComments()
		{
			//multi-line comments /*...*/ (these are actually a real pain to regex, for lack of NOT(MultiChar))
			bool match=true;
			while(match)
			{
				match=false;
				int line=0, rows=0, depth=0, begin=-1, end=-1, beginLine=-1, endLine=-1; char prev=' ';
				for(int i=0; i<source.Length; i++)
				{
					switch(source[i])
					{
						case '¶':
                            line++;
                            if (begin > -1) rows++;
                            break;
						case '*':
							if(prev=='/')
							{
								depth++;
								if(begin==-1){begin=i; beginLine=line;}
							}
							break;
						case '/':
							if(prev=='*')
							{
								depth--;
								if(depth==0) end=i;
								endLine=i;
							}
							break;
					}
					prev=source[i];
					if(end>-1) break;
				}
				if(depth>0) { PwrError.SetFatal("Multiline Comment With No Ending" ,beginLine,0x002); return false; }
				if(depth<0) { PwrError.SetFatal("Multiline Comment With No Beginning",endLine,0x003); return false; }
				if(begin>-1)
				{
					match=true;
          string replacement = "";
          for(int i=0; i<=rows; i++)
          	replacement += '¶'; 
					source = source.Substring(0,begin-1) + replacement + source.Substring(end+1);
				}
			}

			//single line comments //...
			//source=new Regex(@"\/\/.*?[\r\n¶]").Replace(source,"¶"); //old way, broke inside string
      source = new Regex(@"\/\/@PWRPLUSLINE@[^¶]+¶").Replace(source, "¶"); //quick-rip preprocessor lines
			source = new Regex(@"¶\/\/[^¶]+").Replace(source,"¶"); //quick-rip comments that couldn't be in a string
			source = new Regex(@"^\/\/[^¶]*¶").Replace(source,"¶");   //quick-rip beginning comments
			int position = 0;
			Regex regSLC   = new Regex(@"([^¶]+)(\/\/.*?¶)");
			Match matchSLC = regSLC.Match(source, position);
			while(matchSLC.Success)
			{
				//see if we're in a string
				int count=0;
				for(int i=0; i<matchSLC.Groups[1].Value.Length; i++)
					if(matchSLC.Groups[1].Value[i]=='"')
						count++;
				if((count & 1)==0) //not inside a quoted string
					source = source.Replace(matchSLC.Groups[2].Value, "¶");
				else
					position = matchSLC.Groups[2].Index+1;
				
				matchSLC = regSLC.Match(source,position);
			}
			return true;
		}
		//--------------------------------------------------------------------------------------------------------------------------
		private bool PreLiterals()
		{
			//PreLiteralHelper(new Regex( "\"[^\"]*\""                                ), '§', literals["CHAR" ]);
			PreLiteralStrings('§', literals["CHAR" ]); //strings containing valid src (like: ",") were being a pain here...
			PreLiteralHelper(new Regex(@"'[0-9\-]{1,2}\/[0-9\-]{1,2}\/[0-9\-]{2,4}'"), '¼', literals["DATE" ]);
			PreLiteralHelper(new Regex(@"\$[0-9,]+\.[0-9]{2}"                       ), '£', literals["MONEY"]);
			PreLiteralHelper(new Regex(@"[0-9]+\.[0-9]+%"                           ), '‰', literals["RATE" ]);
			PreLiteralHelper(new Regex(@"[0-9]+\.[0-9]+"                            ), 'ƒ', literals["FLOAT"]);
			return true;
		}
		private void PreLiteralHelper(Regex reg, char chr, List<string> arr)
		{
			Match match = reg.Match(source);
			while(match.Success)
			{
				source = source.Replace(match.Value, chr+arr.Count.ToString());
				arr.Add(match.Value);
				match = reg.Match(source);
			}
		}
		private void PreLiteralStrings(char chr, List<string> arr)
		{
			int start=-1, end=-1, pos, idx;
			string repl, str;
			bool hit=true;

			while(hit)
			{
				hit=false;
				for(pos=0; pos<source.Length; pos++)
				{
					switch(source[pos])
					{
						case '¶':
							start=end=-1;
							break;
						case '"':
							if(start<0) start=pos;
							else
							{
								end=pos;
								str = source.Substring(start,end-start+1);
								idx=-1;
								for(int i=0; i<arr.Count; i++) if(arr[i]==str){idx=i; break;}
								if(idx==-1){ idx=arr.Count; arr.Add(str); }
								repl = chr+idx.ToString();
								source = source.Substring(0,start)+repl+source.Substring(end+1);
								start=end=-1;
								hit=true;
							}
							break;
					}
					if(hit) break;
				}
			}
		}
		//--------------------------------------------------------------------------------------------------------------------------
		private bool CondenseWhitespace()
		{
			MatchEvaluator grpOne = new MatchEvaluator(Group1);
			
			//condense whitespace
			source = new Regex(@"[\s]+").Replace(source, "¥");
			source = new Regex(@"[¥]+¶").Replace(source, "¶");
			source = new Regex(@"¶[¥]+").Replace(source, "¶");

			//condense seperator+whitespace to seperator-only
			source = new Regex(seperators+"¥").Replace(source, grpOne);
			source = new Regex("¥"+seperators).Replace(source, grpOne);

			return true;
		}
		private string Group1(Match m){ return m.Groups[1].Value; }
		//--------------------------------------------------------------------------------------------------------------------------
		private bool Tokenize()
		{
			string stack=source[0]+""; int line=0;
			Regex sep = new Regex('^'+seperators+'$');
			for(int i=1; i<source.Length; i++)
			{
				if(stack=="¶")
				{
					line++;
					stack=source[i]+"";
				}
				else if(stack=="¥")
					stack=source[i]+"";
				else if(source[i]=='¶')
				{
					if(stack.Length>0)
					{
						tokVal.Add(stack);
						tokLine.Add(line);
					}
					line++;
					stack="";
				}
				else if(source[i]=='¥')
				{
					if(stack.Length>0)
					{
						tokVal.Add(stack);
						tokLine.Add(line);
					}
					stack="";
				}
				else if(sep.IsMatch(stack+source[i]))
					stack+=source[i];
				else if(sep.IsMatch(source[i]+""))
				{
					tokVal.Add(stack);
					tokLine.Add(line);
					stack=source[i]+"";					
				}
				else if(sep.IsMatch(stack))
				{
					tokVal.Add(stack);
					tokLine.Add(line);
					stack=source[i]+"";
				}
				else
					stack+=source[i];
			}

			return true;
		}
		//--------------------------------------------------------------------------------------------------------------------------
		private bool PostLiterals()
		{
			Regex reg = new Regex("^[0-9]+$");
			for(int i=0; i<tokVal.Count; i++)
			{
				if(tokVal[i]=="BOOL") tokVal[i]="INT";
				else
				{
					if(tokVal[i]=="TRUE" ) tokVal[i]="1";
					if(tokVal[i]=="FALSE") tokVal[i]="0";
					Match match = reg.Match(tokVal[i]);
					if(match.Success)
					{
	          int index = literals["INT"].IndexOf(match.Value);
	          if(index > -1)
	              tokVal[i] = '¤'+index.ToString();
	          else
	          {
						  tokVal[i]='¤'+literals["INT"].Count.ToString();
						  literals["INT"].Add(match.Value);
	          }
					}
				}
			}
			return true;
		}
		//--------------------------------------------------------------------------------------------------------------------------
	}
}
