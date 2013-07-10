using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationProtection.Models
{
    public class EmployeeViewData
    {
        public EmployeeViewData() { }
        public EmployeeViewData(SupervisorViewData sup) { }

        public String DepartmentName { get; set; }
        public String DepartmentID { get; set; }
        public string Display_name { get; set; }
        public string Email { get; set; }
        public int Emp_id { get; set; }
        public string FirstName { get; set; }
        public bool IsTerminated { get; set; }
        public string JobTitle { get; set; }
        public string LastName { get; set; }
        public string Logon_name { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public SupervisorViewData Supervisor { get; set; }
        public int SupId { get; set; }
        public int TechType { get; set; }
        public String PhoneNumber { get; set; }
    }
    public class SupervisorViewData
    {
        public SupervisorViewData() { }
        public SupervisorViewData(EmployeeViewData thisEmp) { }

        public string Email { get; set; }
        public bool IsTerminated { get; set; }
        public string Name { get; set; }
        public int Sup_id { get; set; }
    }
    public class Department
    {
        public static int HH_CORP;
        public static int HH_CORP2;
        public static int HH_CORP3;

        public Department() { }
        public Department(int deptId) { }

        public int Corp_no { get; set; }
        public int Cost_center { get; set; }
        public int Dept_id { get; set; }
        public string Dept_name { get; set; }

        public bool IsHometownDept() { return true; }
    }
}