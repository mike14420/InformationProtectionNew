using System;

namespace WMC.Core.Util.NativeWrapper2008
{
	/// <summary>
	/// Summary description for ShortObj.
	/// </summary>
	public class ShortObj
	{

		//  the native that we are wrapping
		private short val;

		//  Constructor that takes a native type
		public ShortObj(short shortValue) {val = shortValue;}  

		//  Getters / Setters
		public short ShortValue {get{return val;}set{val=value;}}

		//  Static method to try and parse the whatever into a short and return the wrapper object
		public static ShortObj FromObj(object o) 
		{

			//  if we are passed in null, then return null
			if (o == null || o == System.DBNull.Value) 
			{
				return null;
			}
    
			//  empty strings are treated as null
			if(o.ToString().Trim().Length == 0) 
			{
				return null;
			}

			//  this ought to work in this case, otherwise a NumberFormatException will be thrown
			return new ShortObj(short.Parse(o.ToString()));
		}	

		public ShortObj()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
