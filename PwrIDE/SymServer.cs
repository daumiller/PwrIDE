using System;
using System.Text;
using System.Collections.Generic;
using Symitar;

namespace PwrIDE
{
	public class SymServer
	{
    //========================================================================
		public string IP;
		public int    Port;
		public string AixUsr;
		public string AixPwd;
		public string Alias;
    public bool   Remember;
    public bool   Expanded = false;
    public List<SymInst> Syms;
    //========================================================================
    public SymServer()
    {
      IP = Alias = "";
      Port = 0;
      AixUsr = AixPwd = "";
      Remember = true;
      Syms = new List<SymInst>();
    }
		public SymServer(string IP, int Port, string Name, string AixUsr, string AixPwd, bool Remember)
		{
			this.IP = IP;
			this.Port = Port;
			this.Alias = Name;
			this.AixUsr = AixUsr;
			this.AixPwd = AixPwd;
			this.Syms = new List<SymInst>();
      this.Remember = Remember;
		}
    //========================================================================
		public void AddSym(string dir, string id, bool Remember)
		{
			Syms.Add(new SymInst(this, dir, id, Remember));
		}
		public void DelSym(string dir)
		{
			SymInst curr = GetSym(dir);
			if(curr != null)
				Syms.Remove(curr);
		}
    //========================================================================
		public SymInst GetSym(string dir)
		{
			foreach(SymInst sym in Syms)
				if(sym.SymDir == dir)
					return sym;
			return null;
		}
    //========================================================================
		public void Disconnect()
		{
			foreach(SymInst sym in Syms)
				sym.Disconnect();
		}
    //========================================================================
	}
}
