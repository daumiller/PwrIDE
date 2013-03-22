using System;
using System.Text;
using System.Collections.Generic;

namespace PwrPlus
{
	public static class PwrError
	{
    //========================================================================
		public struct ErrorDesc
		{
      public string Filename;
			public string Message;
			public int    Line;
			public int    Code;
      public ErrorDesc(int cod, int lin, string msg)
      {
        Filename = "";
        Code     = cod;
        Line     = lin;
        Message  = msg;
      }
		}
    //========================================================================
		public static ErrorDesc Fatal;
		public static List<ErrorDesc> Warnings;
    //========================================================================
		public static string GetDesc()
		{
			return "Line:  "+Fatal.Line.ToString()+"\nError: "+Fatal.Message+"\nCode:  "+Fatal.Code.ToString(); 
		}
    //========================================================================
		public static void SetFatal(string msg, int line, int code)
		{
			Fatal.Message = msg;
			Fatal.Line    = line+1;
			Fatal.Code    = code;
		}
    //========================================================================
		public static void SetWarning(string msg, int line, int code)
		{
			ErrorDesc warn = new ErrorDesc();
			warn.Message = msg;
			warn.Line    = line;
			warn.Code    = code;
			Warnings.Add(warn);
		}
    //========================================================================
		public static void Reset()
		{
			Fatal.Message="";
			Fatal.Line = Fatal.Code = -1;
			Warnings = new List<ErrorDesc>();
		}
    //========================================================================
	}
}
