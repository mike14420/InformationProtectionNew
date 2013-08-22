using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using JPR;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Models
{
    public class EmployeeView
    {

        public Employee DbGetUserInfo(String UserName)
        {

            SQLRFSDP dp = null;
            Employee thisEmp = null;
            dp = new SQLRFSDP();

            if (UserName != null && UserName.Substring(0, 12) != "WMCDOMAIN\\AL")
            {
                thisEmp = dp.GetEmployeeByLogon(UserName);
            }

            return thisEmp;

        }

        public Employee DbGetEmployeeByEmpId(String EmpID)
        {
            SQLRFSDP dp = null;
            Employee DbEmp = null;
            dp = new SQLRFSDP();
            int EmpIDint = 0;
            int.TryParse(EmpID, out EmpIDint);
            DbEmp = dp.GetEmployeeById(EmpIDint);
            return DbEmp;
        }


        public SelectList GetEmployees()
        {
            SQLRFSDP dp = new SQLRFSDP();
            List<Employee> allEmployees = dp.GetEmployeesAll();
            allEmployees = allEmployees.OrderBy(A => A.LastName).ToList();
            
            SelectList returnData = new SelectList(
                    (
                        from E in allEmployees
                        select new SelectListItem
                        {
                            Value = E.Emp_id.ToString(),
                            Text = String.Format("{0}, {1}",E.LastName, E.FirstName)
                        }
                    )
                    ,"Value", "Text");
            return returnData;
        }


        public SelectList GetSupervisorsEmployees()
        {
            String LogonUserIdentity = HttpContext.Current.Request.LogonUserIdentity.Name;
            Employee theSupervisor = DbGetUserInfo(LogonUserIdentity);

            // MIKE @@@ WORK  DATA FOR TESTING A SUPERVISOR as me
            //theSupervisor = DbGetEmployeeByEmpId("19541");


            SQLRFSDP dp = new SQLRFSDP();
            List<Employee> allEmployees = dp.GetEmployeesAll();
            List<Employee> supervisorsEmployees = new List<Employee>();

            supervisorsEmployees = (from item in allEmployees
                 where item.SupId == theSupervisor.Emp_id
                 select item).ToList();

            SelectList returnData = new SelectList(
                    (
                        from E in supervisorsEmployees
                        select new SelectListItem
                        {
                            Value = E.Emp_id.ToString(),
                            Text = String.Format("{0}, {1}", E.LastName, E.FirstName)
                        }
                    )
                    , "Value", "Text");
            return returnData;
        }


        public IpRequestorViewData GetUserInfo(String UserName)
        {
            IpRequestorViewData thisEmp = new IpRequestorViewData();

            SQLRFSDP dp = null;
            Employee DbEmp = null;
            dp = new SQLRFSDP();

            if (UserName != null && UserName.Substring(0, 12) != "WMCDOMAIN\\AL")
            {
                DbEmp = dp.GetEmployeeByLogon(UserName);
            }
            if (DbEmp != null)
            {

                thisEmp.DeptID = DbEmp.Department.Dept_id.IntValue.ToString();
                thisEmp.DeptName = DbEmp.Department.Dept_name;
                thisEmp.Email = DbEmp.Email;
                thisEmp.JobTitle = DbEmp.JobTitle;
                thisEmp.Lname = DbEmp.LastName;
                thisEmp.Mname = DbEmp.MiddleName;
                thisEmp.Fname = DbEmp.FirstName;
                thisEmp.EmpID = DbEmp.Emp_id.ToString();
                thisEmp.SupervisorEmpID = DbEmp.Supervisor.Sup_id.ToString();
            }
            return thisEmp;

        }
    }
}