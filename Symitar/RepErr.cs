using System;
using System.Text;

namespace Symitar
{
	public class RepErr
	{
    //========================================================================
		public SymFile fileSrc;
		public string  fileErr;
		public string  error;
		public int     line;
		public int     column;
    public int     installedSize;
		public bool    any;
    //========================================================================
		public RepErr(SymFile Source, string ErrFile, string ErrMsg, int Row, int Col)
		{
			fileSrc = Source;
			fileErr = ErrFile;
			error   = ErrMsg;
			line    = Row;
			column  = Col;
			any     = true;
		}
    //========================================================================
		public static RepErr None()
		{
			RepErr ret = new RepErr(null, "", "", 0, 0);
			ret.any = false;
			return ret;
		}
    //------------------------------------------------------------------------
    public static RepErr None(int size)
    {
      RepErr ret = new RepErr(null, "", "", 0, 0);
      ret.any = false;
      ret.installedSize = size;
      return ret;
    }
    //========================================================================
	}
}
