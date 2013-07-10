using System;


namespace WMC.Core.Util.NativeWrapper2008
{
	/// <summary>
	/// Summary description for DateTimeObj.
	/// </summary>
	public class DateTimeObj
	{

		private DateTime val;

		public DateTime DateTimeValue{get{return val;}set{val=value;}}

		public static DateTimeObj FromObj(object o) 
		{
			if (o == null || o == System.DBNull.Value) 
			{
				return null;
			}
			if(o.ToString().Trim().Length == 0) 
			{
				return null;
			}
			return new DateTimeObj(DateTime.Parse(o.ToString()));
		}


		public bool IsInFuture() 
		{
			
			TimeSpan ts = val.Subtract(DateTime.Now);
			return ts.TotalSeconds > 0;
		}

		public static DateTimeObj FromObjDontThrow(object o) 
		{
			try 
			{
				return DateTimeObj.FromObj(o);
			} 
			catch (Exception){}
			return null;
		}

		public DateTimeObj()
		{
			val = DateTime.Now;
		}

		public DateTimeObj(DateTime DateTimeValue) {val = DateTimeValue;}
		
		public string DateString() 
		{
			return val.ToString("MM/dd/yyyy");
		}
		
		public bool IsSameDay(DateTimeObj otherDate) 
		{
			if (otherDate == null) {return false;}
			return (
				otherDate.DateTimeValue.Year == val.Year &&
				otherDate.DateTimeValue.Month == val.Month &&
				otherDate.DateTimeValue.Day == val.Day);
					
		}
		
		public override string ToString() 
		{
			
			
			return val.ToString("MM/dd/yyyy HH:mm:ss");
			
		}

	}
}
