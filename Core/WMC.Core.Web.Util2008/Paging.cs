using System;
using System.Collections.Generic;
using System.Text;
using WMC.Core.Util.NativeWrapper2008;

namespace WMC.Core.Web.Util2008
{
	/// <summary>
	/// Summary description for Paging.
	/// </summary>
	public class Paging
	{
		private string tableName;
		private string primaryKey;
		private string sortField;
		private IntObj pageSize;
		private IntObj pageIndex;
		private IntObj pageCount;
		private string queryFilter;
		private string sortOrder;

		public string TableName{get{return tableName;}set{tableName=value;}}
		public string PrimaryKey{get{return primaryKey;}set{primaryKey=value;}}
		public string SortField{get{return sortField;}set{sortField=value;}}
		public IntObj PageSize{get{return pageSize;}set{pageSize=value;}}
		public IntObj PageIndex{get{return pageIndex;}set{pageIndex=value;}}
		public IntObj PageCount{get{return pageCount;}set{pageCount=value;}}
		public string QueryFilter{get{return queryFilter;}set{queryFilter=value;}}
		public string SortOrder{get{return sortOrder;}set{sortOrder=value;}}

		public Paging()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
