using System;

namespace WMC.Core.Util.NativeWrapper2008
{
	/// <summary>
	/// Summary description for CharObj.
	/// </summary>
	public class CharObj
	{

		private char val;

		public char CharValue{get{return val;}set{val=value;}}

		public static CharObj FromObj(object o) 
		{
			if (o == null || o == System.DBNull.Value) 
			{
				return null;
			}
			
			string asString = o.ToString();
			char[] ca = asString.ToCharArray(0,1);
			return new CharObj(ca[0]);
		}

		public static CharObj FromObjDontThrow(object o) 
		{
			try 
			{
				return CharObj.FromObj(o);
			} 
			catch (Exception) {}
			return null;
		}

		public override string ToString() 
		{
			return "" + val;
		}

		public CharObj(char charValue) {CharValue=charValue;}

		public CharObj()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
