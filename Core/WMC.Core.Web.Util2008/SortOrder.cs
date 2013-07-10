using System;
using System.Collections.Generic;
using System.Text;
using WMC.Core.Util.NativeWrapper2008;
//using WMC.Core.Util.NativeWrapper2008;

namespace WMC.Core.Web.Util2008
{
	/// <summary>
	/// Summary description for SortOrder.
	/// </summary>
	public class SortOrder
	{

		public static int SORT_ORDER_ASCENDING = 1;
		public static int SORT_ORDER_DESCENDING = 2;
		private static int SORT_ORDER_DEFAULT = SORT_ORDER_ASCENDING;

		private int theSortOrder;
		
		public int TheSortOrder
		{
			get{return theSortOrder;}
			set
			{
				if (value == SORT_ORDER_ASCENDING || value == SORT_ORDER_DESCENDING) 
				{
					theSortOrder=value;	
				} 
				else 
				{
					throw new Exception("Invalid theSortOrder specified.  Use SORT_ORDER_ASCENDING or SORT_ORDER_DESCENDING.");
				}
				
			}
		}

		public SortOrder()
		{
			this.TheSortOrder=SORT_ORDER_DEFAULT;
			
		}

		public SortOrder(string sortOrder) 
		{
			try 
			{
				this.TheSortOrder=Int32.Parse(sortOrder);
			} 
			catch (Exception) 
			{
				this.TheSortOrder = SORT_ORDER_DEFAULT;
			}
		}

		public SortOrder(int sortOrder) 
		{
			
			this.TheSortOrder = sortOrder;
		}

		public int OppositeOfSortOrder() 
		{
			if (TheSortOrder == SORT_ORDER_ASCENDING) 
			{
				return SORT_ORDER_DESCENDING;
			}
			return SORT_ORDER_ASCENDING;
		}

		public string ToSqlString() 
		{
			if (TheSortOrder == SORT_ORDER_ASCENDING) 
			{
				return "asc";
			}
			return "desc";
		}

		public bool IsAscending() 
		{
			return TheSortOrder == SORT_ORDER_ASCENDING;
		}

		public SortOrder(IntObj sortOrder) 
		{
			
			if (sortOrder == null) 
			{
				this.TheSortOrder = SORT_ORDER_DEFAULT;
			}
			this.TheSortOrder = sortOrder.IntValue;
		}
	}
}

