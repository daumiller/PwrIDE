using System;
using System.Collections.Generic;
using System.Text;

namespace PwrPlus
{
    public class Symbol
    {
        public int          idx;
        public string       typ;
        public List<Symbol> kid;
        public Symbol(int ndex, string type){ idx=ndex; typ=type; kid=new List<Symbol>(); }
        public void AddChild(Symbol node){if(kid==null) kid=new List<Symbol>(); kid.Add(node); }
    }
}
