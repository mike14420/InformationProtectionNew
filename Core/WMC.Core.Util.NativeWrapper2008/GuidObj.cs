using System;

namespace WMC.Core.Util.NativeWrapper2008
{
	/// <summary>
	/// Summary description for GuidObj.
	/// </summary>
	public class GuidObj
	{

		private System.Guid val;

		public System.Guid Guid{get{return val;}set{val=value;}}

		public static GuidObj FromObj(object o) 
		{
			if (o == null || o == System.DBNull.Value) 
			{
				return null;
			}
			return new GuidObj(new System.Guid(o.ToString()));
		}

		public static GuidObj FromObjDontThrow(object o) 
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
			return "" + val.ToString();
		}

		public GuidObj(System.Guid g) {val=g;}
		public GuidObj(string g) 
		{
			if (g == null) 
			{
				throw new Exception("Tried to build an invalid guid.  value was '" + g + "'");
			}
			val = new Guid(g);
		}

		public GuidObj()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
