using System;

namespace WMC.Core.Util.NativeWrapper2008
{
	/// <summary>
	/// Summary description for IntObj.
	/// </summary>
	public class IntObj
	{

		private int val;

		public int IntValue{get{return val;}set{val=value;}}
		
		public void Add(int i) 
		{
			val += i;
		}
		
		public static IntObj FromObj(object o) 
		{
			if (o == null || o == System.DBNull.Value) 
			{
				return null;
			}
			return new IntObj(Int32.Parse(o.ToString()));
		}

		public static IntObj FromObjDontThrow(object o) 
		{
			try 
			{
				return FromObj(o);
			} 
			catch (Exception){}
			return null;

		}


		public static bool operator !=(IntObj a, int b) 
		{
			return !(a==b);
		}
		public static bool operator ==(IntObj a, int b) 
		{
			try 
			{
				return a.IntValue == b;
			} 
			catch (Exception) {}
			return false;
		}

		public static bool operator !=(IntObj a, IntObj b) 
		{
			return !(a==b);
		}

		public static bool operator ==(IntObj a, IntObj b) 
		{
			
			try 
			{
				return a.IntValue == b.IntValue;
			} 
			catch (Exception)
			{
				//  one or the other was probably null.  In the case where they are both null, this needs to return true
				//  but we cant use ==...
				bool aIsNull = false;
				bool bIsNull = false;
				try {int x = a.IntValue;} 
				catch (Exception) {aIsNull=true;}
				try {int x = b.IntValue;} 
				catch (Exception) {bIsNull=true;}
				if (aIsNull && bIsNull) 
				{
					return true;
				}
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			return this==(IntObj)obj;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}


		
		



		public override string ToString() 
		{
			return "" + val;
		}

		public IntObj(int intValue) {IntValue=intValue;}

		public IntObj()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
