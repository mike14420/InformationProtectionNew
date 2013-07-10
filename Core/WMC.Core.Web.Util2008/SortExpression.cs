using System;
using System.Collections.Generic;
using System.Text;

namespace WMC.Core.Web.Util2008
{
	/// <summary>
	/// Summary description for SortExpression.
	/// </summary>
	public class SortExpression
	{
		private string sortOrder;
		private string sortField;

		public string SortOrder{get{return sortOrder;}set{sortOrder=value;}}
		public string SortField{get{return sortField;}set{sortField=value;}}

		public string ToSqlSortExpression()
		{
			string expression = " ORDER BY " + sortField + " " + sortOrder;
			return expression;
		}

		public SortExpression()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
