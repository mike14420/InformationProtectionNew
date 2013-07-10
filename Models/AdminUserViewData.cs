using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationProtection.Models
{
    public class AdminUserViewData
    {
        public int AdminUsersID { get; set; }
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String JobTitle { get; set; }
        public String DeptID { get; set; }
        public String EmpID { get; set; }
    }
}