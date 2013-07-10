using System;


namespace WMC.Core.Util.NativeWrapper2008
{
	/// <summary>
	/// Summary description for DoubleObj.
	/// </summary>
	public class DoubleObj
	{

		private double val;

		public double DoubleValue{get{return val;}set{val=value;}}

		public static DoubleObj FromObj(object o) 
		{
			if (o == null || o == System.DBNull.Value) 
			{
				return null;
			}

			return new DoubleObj(System.Double.Parse(o.ToString()));
		}

		public static DoubleObj FromObjDontThrow(object o) 
		{
			try 
			{
				return FromObj(o);
			} 
			catch (Exception){}
			return null;

		}

		public static string GetDouble(string s)
		{
			double doubleVal;

			double.TryParse(s,System.Globalization.NumberStyles.Float,null,out doubleVal);

			if(doubleVal == 0)
			{
				return null;
			}
			return s;
		}


		public override string ToString() 
		{
			return "" + DoubleValue;
			
		}
		
		

		public DoubleObj(double doubleValue) {DoubleValue=doubleValue;}

		public DoubleObj()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
