using System;
using System.Collections.Generic;
using System.Text;
using WMC.Core.DataProvider2008;
using WMC.Core.Util.NativeWrapper2008;
using WMC.Core.Employees.BusObj2008;
using System.Collections;
using System.Data;

namespace WMC.Core.Employees.DataProvider2008
{
	/// <summary>
	/// Summary description for EmployeeSQLDP.
	/// </summary>
	public class EmployeeSQLDP : SQLDataProvider
	{

		protected override string GetConnectionStringKey()
		{
			return "ConnectionString.EmployeeDB";
		}


		public Supervisor GetSupervisorById(IntObj id)
		{
			//  null in null out
			if(id == null){return null;}

			//  initialize some schtuff
			Supervisor thisSup = null;
			DataTable dt = null;

			try 
			{
				//  Set parameters
				ArrayList paramList = new ArrayList();
				paramList.Add(GetParameterInputInt("@id",id));
	
				//  Execute the stored procedure
				dt = ExecDataTable("supervisor_get_by_id", paramList);

				//  Pull out the process data
				if (dt != null && dt.Rows.Count > 0)
				{
					thisSup = new Supervisor();
					thisSup.Sup_id = GetInt(dt.Rows[0]["empID"]);
                    thisSup.Email = GetString(dt.Rows[0]["Email"]);
					thisSup.Name = GetString(dt.Rows[0]["SupervisorsName"]);
				}

				if(thisSup == null)
				{
					thisSup = new Supervisor(this.GetEmployeeByIdTermed(id));
					if(thisSup.Sup_id == IntObj.FromObj(00000) || thisSup.Name == "N/A")
					{
						thisSup = null;
					}
				}

			} 
			catch (Exception e) 
			{
				//  we could log this here if we want, but at this point lets just throw these up the chain
				throw e;	
			}

			//  return the collection (or if no records returned this will be null)
			return thisSup;
		}

		public ArrayList GetSupervisorAll()
		{
			//  initialize some schtuff
			ArrayList allSups = null;
			DataTable dt = null;

			try 
			{
				
				//  Execute the stored procedure
				dt = ExecDataTable("supervisor_get_all",null);

				//  Pull out the process data
				if (dt != null && dt.Rows.Count > 0) 
				{
					allSups = new ArrayList();
					for (int i=0; i < dt.Rows.Count;i++) 
					{
						Supervisor thisSup = new Supervisor();
						thisSup.Sup_id = GetInt(dt.Rows[i]["empID"]);
                        thisSup.Email = GetString(dt.Rows[0]["Email"]);
						thisSup.Name = GetString(dt.Rows[i]["SupervisorsName"]);
						allSups.Add(thisSup);						
					}
				}
			} 
			catch (Exception e) 
			{
				//  we could log this here if we want, but at this point lets just throw these up the chain
				throw e;	
			}

			//  return the collection (or if no records returned this will be null)
			return allSups;
		}


		public Employee GetEmployeeById(IntObj id)
		{
			//  null in null out
			if(id == null){return null;}

			//  initialize some schtuff
			Employee thisEmp = null;
			Supervisor thisSup = null;
			Department thisDept = null;
			DataTable dt = null;

			try 
			{
				//  Set parameters
				ArrayList paramList = new ArrayList();
				paramList.Add(GetParameterInputInt("@id",id));
	
				//  Execute the stored procedure
				dt = ExecDataTable("employee_get_by_id", paramList);

				//  Pull out the process data
				if (dt != null && dt.Rows.Count > 0)
				{
					thisEmp = new Employee();
					thisEmp.Emp_id = GetInt(dt.Rows[0]["EmpId"]);
                    thisEmp.TechType = GetInt(dt.Rows[0]["tt"]);
					thisEmp.Display_name = GetString(dt.Rows[0]["DisplayName"]);
					thisEmp.Name = GetString(dt.Rows[0]["Name"]);
                    thisEmp.FirstName = GetString(dt.Rows[0]["FirstName"]);
                    thisEmp.LastName = GetString(dt.Rows[0]["LastName"]);
                    thisEmp.MiddleName = GetString(dt.Rows[0]["MiddleName"]);
					thisEmp.Email = GetString(dt.Rows[0]["Email"]);
					thisEmp.Logon_name = GetString(dt.Rows[0]["LogonName"]);
					thisEmp.JobTitle = GetString(dt.Rows[0]["JobTitle"]);
                    thisEmp.NickName = GetString(dt.Rows[0]["NickName"]);

					thisSup = new Supervisor();
					thisSup = GetSupervisorById(IntObj.FromObj(GetInt(dt.Rows[0]["SupId"])));
					thisEmp.Supervisor = thisSup;

					thisDept = new Department();
					thisDept = GetDepartmentById(IntObj.FromObj(GetInt(dt.Rows[0]["DeptId"])));
					thisEmp.Department = thisDept;
					thisEmp.IsTerminated = false;
				}

				if(thisEmp == null)
				{
					thisEmp = this.GetEmployeeByIdTermed(id);
				}

			} 
			catch (Exception e) 
			{
				//  we could log this here if we want, but at this point lets just throw these up the chain
				throw e;	
			}

			//  return the collection (or if no records returned this will be null)
			return thisEmp;
		}

		public Employee GetEmployeeByLogon(String Logon)
		{
			//  null in null out
			if(Logon == null){return null;}

			//  initialize some schtuff
			Employee thisEmp = null;
			Supervisor thisSup = null;
			Department thisDept = null;
			DataTable dt = null;

			try 
			{
				//  Set parameters
				ArrayList paramList = new ArrayList();
				paramList.Add(GetParameterInputString("@logon",Logon));
	
				//  Execute the stored procedure
				dt = ExecDataTable("employee_get_by_logon", paramList);

				//  Pull out the process data
				if (dt != null && dt.Rows.Count > 0)
				{
					thisEmp = new Employee();
					thisEmp.Emp_id = GetInt(dt.Rows[0]["EmpId"]);
                    thisEmp.TechType = GetInt(dt.Rows[0]["tt"]);
					thisEmp.Display_name = GetString(dt.Rows[0]["DisplayName"]);
					thisEmp.Name = GetString(dt.Rows[0]["Name"]);
                    thisEmp.FirstName = GetString(dt.Rows[0]["FirstName"]);
                    thisEmp.LastName = GetString(dt.Rows[0]["LastName"]);
                    thisEmp.MiddleName = GetString(dt.Rows[0]["MiddleName"]);
					thisEmp.Email = GetString(dt.Rows[0]["Email"]);
					thisEmp.Logon_name = GetString(dt.Rows[0]["LogonName"]);
					thisEmp.JobTitle = GetString(dt.Rows[0]["JobTitle"]);
                    thisEmp.NickName = GetString(dt.Rows[0]["NickName"]);

					thisSup = new Supervisor();
					thisSup = GetSupervisorById(IntObj.FromObj(GetInt(dt.Rows[0]["SupID"])));
					thisEmp.Supervisor = thisSup;

					thisDept = new Department();
					thisDept = GetDepartmentById(IntObj.FromObj(GetInt(dt.Rows[0]["DeptId"])));
					thisEmp.Department = thisDept;
					thisEmp.IsTerminated = false;
				}
				else
				{
					return null;
				}

			} 
			catch (Exception e) 
			{
				//  we could log this here if we want, but at this point lets just throw these up the chain
				throw e;	
			}

			//  return the collection (or if no records returned this will be null)
			return thisEmp;
		}

        public List<Employee> GetEmployeesAll()
        {
            //  initialize some schtuff
            List<Employee> allEmps = null;
            Employee thisEmp = null;
            DataTable dt = null;

            try
            {

                //  Execute the stored procedure
                dt = ExecDataTable("sp_employee_get_all", null);

                //  Pull out the process data
                if (dt != null && dt.Rows.Count > 0)
                {
                    allEmps = new List<Employee>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        thisEmp = new Employee();
                        thisEmp.Emp_id = GetInt(dt.Rows[i]["EmpID"]);
                        thisEmp.TechType = GetInt(dt.Rows[i]["TechType"]);
                        thisEmp.FirstName = GetString(dt.Rows[i]["FirstName"]);
                        thisEmp.LastName = GetString(dt.Rows[i]["LastName"]);
                        thisEmp.MiddleName = GetString(dt.Rows[i]["MiddleName"]);
                        thisEmp.SupId = GetInt(dt.Rows[i]["supID"]);
                        thisEmp.JobTitle = GetString(dt.Rows[i]["JobTitle"]);
                        thisEmp.NickName = GetString(dt.Rows[i]["NickName"]);
                        allEmps.Add(thisEmp);
                    }
                }
            }
            catch (Exception e)
            {
                //  we could log this here if we want, but at this point lets just throw these up the chain
                throw e;
            }

            //  return the collection (or if no records returned this will be null)
            return allEmps;
        }


		public Employee GetEmployeeByIdTermed(IntObj id)
		{
			//  null in null out
			if(id == null){return null;}

			//  initialize some schtuff
			Employee thisEmp = null;
			DataTable dt = null;

			try 
			{
				//  Set parameters
				ArrayList paramList = new ArrayList();
				paramList.Add(GetParameterInputInt("@id",id));
	
				//  Execute the stored procedure
				dt = ExecDataTable("employee_get_by_id_termed", paramList);

				//  Pull out the process data
				if (dt != null && dt.Rows.Count > 0)
				{
					thisEmp = new Employee();
					thisEmp.Emp_id = GetInt(dt.Rows[0]["empID"]);
					thisEmp.Display_name = GetString(dt.Rows[0]["displayName"]);
					thisEmp.Name = GetString(dt.Rows[0]["name"]);
					thisEmp.IsTerminated = true;
				}

			} 
			catch (Exception e) 
			{
				//  we could log this here if we want, but at this point lets just throw these up the chain
				throw e;	
			}

			//  return the collection (or if no records returned this will be null)
			return thisEmp;
		}


		public Department GetDepartmentById(IntObj id)
		{
			//  null in null out
			if(id == null){return null;}

			//  initialize some schtuff
			Department thisDept = null;
			DataTable dt = null;

			try 
			{
				//  Set parameters
				ArrayList paramList = new ArrayList();
				paramList.Add(GetParameterInputInt("@id",id));
	
				//  Execute the stored procedure
				dt = ExecDataTable("department_get_by_id", paramList);

				//  Pull out the process data
				if (dt != null && dt.Rows.Count > 0)
				{
					thisDept = new Department();
					thisDept.Dept_id = GetInt(dt.Rows[0]["DeptId"]);
					thisDept.Dept_name = GetString(dt.Rows[0]["DeptDesc"]);
					thisDept.Corp_no = GetInt(dt.Rows[0]["CorpNo"]);
					thisDept.Cost_center = GetInt(dt.Rows[0]["CostCenterNo"]);
				}

			} 
			catch (Exception e) 
			{
				//  we could log this here if we want, but at this point lets just throw these up the chain
				throw e;	
			}

			//  return the collection (or if no records returned this will be null)
			return thisDept;
		}

		public ArrayList GetDepartmentAll()
		{
			//  initialize some schtuff
			ArrayList allDepts = null;
			Department thisDept = null;
			DataTable dt = null;

			try 
			{	
				//  Execute the stored procedure
				dt = ExecDataTable("department_get_all",null);

				//  Pull out the process data
				if (dt != null && dt.Rows.Count > 0) 
				{
					allDepts = new ArrayList();
					for (int i=0; i < dt.Rows.Count;i++) 
					{
						thisDept = new Department();
						thisDept.Dept_id = GetInt(dt.Rows[i]["DeptId"]);
						thisDept.Dept_name = GetString(dt.Rows[i]["DeptDesc"]);
						thisDept.Corp_no = GetInt(dt.Rows[i]["CorpNo"]);
						thisDept.Cost_center = GetInt(dt.Rows[i]["CostCenterNo"]);
						allDepts.Add(thisDept);						
					}

				}

			} 
			catch (Exception e) 
			{
				//  we could log this here if we want, but at this point lets just throw these up the chain
				throw e;	
			}

			//  return the collection (or if no records returned this will be null)
			return allDepts;
		}


		public EmployeeSQLDP()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}


