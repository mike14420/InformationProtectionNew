using System;

namespace WMC.Core.Util.NativeWrapper2008
{
	/// <summary>
	/// Summary description for BoolObj.
	/// </summary>
	public class BoolObj
	{

		private bool val;

		
		public bool BoolValue 
		{
			get{return val;}
			set{val=value;}
		}
        		
		public static BoolObj FromObj(object o) 
		{
			if (o == null || o == System.DBNull.Value) 
			{
				return null;
			}
      
			if(o.ToString().Trim() == "1") 
			{
				return new BoolObj(true);
			}      
			else 
			{
				return new BoolObj(false);
			}
		}
		
		public static string ToString(BoolObj val, string valueIfNull, string valueIfTrue, string valueIfFalse) 
		{
			if (val==null) {return valueIfNull;}
			if (val.BoolValue) {return valueIfTrue;}
			return valueIfFalse;
		}


		
		public BoolObj(bool boolValue) 
		{
			BoolValue = boolValue;
		} 
		
		public override string ToString() 
		{
			if (val) {return "true";}
			return "false";
		}

		public static bool operator true(BoolObj theObj) 
		{
			if (theObj == null || theObj.BoolValue == false) 
			{
				return false;
			}
			return true;
		}
		public static bool operator false(BoolObj theObj) 
		{
			if (theObj == null || theObj.BoolValue == false) 
			{
				return true;
			}
			return false;
		}

		public static bool operator !(BoolObj theObj) 
		{
			if (theObj == null || theObj.BoolValue == false) 
			{
				return true;
			}
			return false;
		}

		public BoolObj()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
