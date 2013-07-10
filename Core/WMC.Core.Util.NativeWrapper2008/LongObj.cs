using System;

namespace WMC.Core.Util.NativeWrapper2008
{
	/// <summary>
	/// Summary description for LongObj.
	/// </summary>
	public class LongObj
	{

		private long val;

		public long LongValue{get{return val;}set{val=value;}}
		
		public void Add(int i) 
		{
			val += i;
		}

		public static LongObj FromObj(object o) 
		{
			if (o == null || o == System.DBNull.Value) 
			{
				return null;
			}
			try 
			{
				return new LongObj(Int32.Parse(o.ToString()));
			} 
			catch (Exception){}
			return null;
		}

		public int CompareTo(LongObj x) 
		{
			if (x == null) {return 1;}
			if (this.LongValue < x.LongValue) {return -1;}
			if (this.LongValue > x.LongValue) {return -1;}
			return 0;
			
		}

		public static LongObj FromObjDontThrow(object o) 
		{
			try 
			{
				return FromObj(o);
			} 
			catch (Exception){}
			return null;

		}



		public override string ToString() 
		{		
			return "" + val;
		}		

		public LongObj(long longValue) {LongValue=longValue;}
		public LongObj(object o) 
		{
			LongObj obj = LongObj.FromObj(o);
			if (obj == null) 
			{
				throw new Exception("Could not parse long value from object: " + o);
			}
			LongValue = obj.LongValue;

		}

		public LongObj()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
