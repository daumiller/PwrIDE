using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PwrPlus
{
    public class Beautifier
    {
        private string indenter = "  ";
        private int    maxWidth = 132;
        private bool   errFlag  = false;

        public string Beautify(string source)
        {
            return LineWrap(Indent(source));
        }

        private string IndentStr(int width)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<width; i++) sb.Append(indenter);
            return sb.ToString();
        }

        private string LineWrap(string[] arr)
        {
            List<string> result = new List<string>();
            for(int i=0; i<arr.Length; i++)
            {
                if(arr[i].Length > maxWidth)
                {
                    List<string> tmp = WrapStr(arr[i], i+1);
                    foreach(string curr in tmp) result.Add(curr);
                    if(errFlag) return "";
                }
                else
                    result.Add(arr[i]);
            }
            
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<result.Count; i++){sb.Append(result[i]); sb.Append('\n');}
            return sb.ToString();
        }
        private List<string> WrapStr(string str, int line)
        {
            List<string> ret = new List<string>(); ret.Add(str);
            string indention = new Regex(@"^\s*").Match(str).Value;
            while (ret[ret.Count - 1].Length > maxWidth)
            {
                string curr = ret[ret.Count - 1];
                int space = -1;
                for(int i=(maxWidth-1); i>-1; i--)if(curr.Substring(i,1)==" "){space=i; break;}
                if (space == -1) { PwrError.SetFatal("Unable to Splite Line to " + maxWidth.ToString() + " Characters", line, 0x07F); errFlag=true; return ret;}
                ret.Add(indention+curr.Substring(space + 1));
                ret[ret.Count-2] = curr.Substring(0,space);
            }
            return ret;
        }

        private string[] Indent(string src)
        {
            string[] lines = src.Split(new char[] { '\n' });
            Regex increase = new Regex(@"(^DEFINE$|^SETUP$|^SELECT$|^SORT$|^PRINT\s|^TOTAL$|^PROCEDURE\s|^DO$|\sDO$|^\s*HEADERS$|^\s*TRAILERS$)");
            Regex decrease = new Regex(@"^END$");
            int level=0; string white="";
            for(int i=0; i<lines.Length; i++)
            {
                if(decrease.IsMatch(lines[i])){level--; white=IndentStr(level); }
                lines[i]=white+lines[i];
                if(increase.IsMatch(lines[i])){level++; white=IndentStr(level); }
            }
            return lines;
        }
    }
}
