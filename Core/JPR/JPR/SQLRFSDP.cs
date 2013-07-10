using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMC.Core.DataProvider2008;
using WMC.Core.Employees.BusObj2008;
using WMC.Core.Employees.DataProvider2008;
using WMC.Core.Util.NativeWrapper2008;

namespace JPR
{
    public class SQLRFSDP : SQLDataProvider
    {

        #region Constructors
        public SQLRFSDP()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region EmployeeSQL Data Provider Wrappers

        EmployeeSQLDP emp_dp = null;

        public Department GetDepartmentById(IntObj id)
        {
            emp_dp = new EmployeeSQLDP();
            Department thisDept = new Department();
            thisDept = emp_dp.GetDepartmentById(id);
            if (thisDept != null)
            {
                return thisDept;
            }
            else
            {
                return null;
            }
        }

        public Employee GetEmployeeById(IntObj id)
        {
            emp_dp = new EmployeeSQLDP();
            Employee thisEmp = new Employee();
            thisEmp = emp_dp.GetEmployeeById(id);
            if (thisEmp != null)
            {
                return thisEmp;
            }
            else
            {
                return null;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            emp_dp = new EmployeeSQLDP();
            Employee thisEmp = new Employee();
            IntObj iObj = new IntObj(id);
            thisEmp = emp_dp.GetEmployeeById(iObj);
            if (thisEmp != null)
            {
                return thisEmp;
            }
            else
            {
                return null;
            }
        }

        public Supervisor GetSupervisorById(IntObj id)
        {
            emp_dp = new EmployeeSQLDP();
            Supervisor thisSup = new Supervisor();
            thisSup = emp_dp.GetSupervisorById(id);
            if (thisSup != null)
            {
                return thisSup;
            }
            else
            {
                return null;
            }
        }

        public ArrayList GetDepartmentAll()
        {
            emp_dp = new EmployeeSQLDP();
            ArrayList allDepts = new ArrayList();
            allDepts = emp_dp.GetDepartmentAll();
            if (allDepts != null)
            {
                return allDepts;
            }
            else
            {
                return null;
            }
        }

        public ArrayList GetSupervisorAll()
        {
            emp_dp = new EmployeeSQLDP();
            ArrayList allSups = new ArrayList();
            allSups = emp_dp.GetSupervisorAll();
            if (allSups != null)
            {
                return allSups;
            }
            else
            {
                return null;
            }
        }

        public Employee GetEmployeeByLogon(String Logon)
        {
            emp_dp = new EmployeeSQLDP();
            Employee thisEmp = new Employee();
            thisEmp = emp_dp.GetEmployeeByLogon(Logon);
            if (thisEmp != null)
            {
                return thisEmp;
            }
            else
            {
                return null;
            }
        }

        public List<Employee> GetEmployeesAll()
        {
            emp_dp = new EmployeeSQLDP();
            List<Employee> allEmps = new List<Employee>();
            allEmps = emp_dp.GetEmployeesAll();
            if (allEmps != null)
            {
                return allEmps;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Get Employee Methods

        public ArrayList GetAllLiaisons()
        {
            //  initialize some schtuff
            ArrayList lias = null;
            Employee thisEmp = null;
            DataTable dt = null;

            try
            {
                //  Execute the call
                dt = ExecDataTable("liaisons_get_all", null);

                //  Pull out the process data
                if (dt != null && dt.Rows.Count > 0)
                {
                    //  Create ArrayList to hold liaisons
                    lias = new ArrayList();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        thisEmp = new Employee();
                        thisEmp = GetEmployeeById(GetInt(dt.Rows[i]["EmpId"]));
                        lias.Add(thisEmp);
                    }
                }
            }
            catch (Exception e)
            {
                //  Log exceptions to event viewer etc
                throw e;
            }

            //  return notes object
            return lias;
        }


        public ArrayList GetSupervisorsByDeptID(String deptID)
        {
            //  initialize some schtuff
            ArrayList allSups = null;
            DataTable dt = null;

            try
            {

                ArrayList paramList = new ArrayList();
                paramList.Add(GetParameterInputString("@DeptID", deptID));

                //  Execute the stored procedure
                dt = ExecDataTable("supervisor_get_by_deptID", paramList);

                //  Pull out the process data
                if (dt != null && dt.Rows.Count > 0)
                {
                    allSups = new ArrayList();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Supervisor thisSup = new Supervisor();
                        thisSup.Sup_id = GetInt(dt.Rows[i]["empID"]);
                        thisSup.Email = GetString(dt.Rows[i]["Email"]);
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


        public ArrayList GetLeadersForDelegate(IntObj empID)
        {
            //  initialize some schtuff
            ArrayList allSups = null;
            DataTable dt = null;

            try
            {

                ArrayList paramList = new ArrayList();
                paramList.Add(GetParameterInputInt("@empID", empID));

                //  Execute the stored procedure
                dt = ExecDataTable("leaders_get_for_delegate", paramList);

                //  Pull out the process data
                if (dt != null && dt.Rows.Count > 0)
                {
                    allSups = new ArrayList();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Supervisor thisSup = new Supervisor();
                        thisSup.Sup_id = GetInt(dt.Rows[i]["supid"]);
                        thisSup.Email = GetString(dt.Rows[0]["sup_email"]);
                        thisSup.Name = GetString(dt.Rows[i]["sup_name"]);
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


        #endregion

        #region WebApplicationError Methods

        public WebApplicationError Save(WebApplicationError theError)
        {
            return Insert(theError);
        }

        private WebApplicationError Insert(WebApplicationError theError)
        {

            ArrayList returnList = new ArrayList();

            try
            {
                ArrayList paramList = new ArrayList();

                paramList.Add(GetParameterInputInt("@user_id", theError.UserId));
                paramList.Add(GetParameterInputString("@user_name", theError.UserName));
                paramList.Add(GetParameterInputDate("@time_stamp", theError.TimeStamp));
                paramList.Add(GetParameterInputString("@ip_address", theError.IpAddress));
                paramList.Add(GetParameterInputString("@url", theError.WebPage));
                paramList.Add(GetParameterInputString("@help_link", theError.HelpLink));
                paramList.Add(GetParameterInputString("@source", theError.Source));
                paramList.Add(GetParameterInputString("@message", theError.Message));
                paramList.Add(GetParameterInputString("@stack_trace", theError.StackTrace));
                paramList.Add(GetParameterInputString("@target_site", theError.TargetSite));
                paramList.Add(GetParameterOutputInt("@createdId", null));

                //  Execute the call
                Hashtable oParams = ExecuteNonQuery("dbo.error_insert", paramList);

                //  get the newly assigned id
                theError.Id = IntObj.FromObj(oParams["@createdId"]);

            }
            catch (Exception e)
            {
                throw e;
            }

            return theError;
        }

        #endregion

        #region IPEmp Save/Update Methods

        public IPEmp Save(IPEmp ipEmp)
        {
            if (ipEmp.UserID == null)
            {
                return Insert(ipEmp);
            }
            return Update(ipEmp);
        }


        private IPEmp Insert(IPEmp ipEmp)
        {
            try
            {
                this.StartTransaction();

                ArrayList paramList = new ArrayList();

                //  save into table jobreq data 
                //****Not used for now******
                paramList.Add(GetParameterInputString("@fname", ipEmp.FName));
                paramList.Add(GetParameterInputString("@lname", ipEmp.LName));
                paramList.Add(GetParameterInputString("@mname", ipEmp.MName));
                paramList.Add(GetParameterInputString("@job_title", ipEmp.JobTitle));
                paramList.Add(GetParameterInputString("@empID", ipEmp.EmpID));
                paramList.Add(GetParameterInputString("@deptID", ipEmp.DeptID));
                paramList.Add(GetParameterInputString("@dept_name", ipEmp.DeptName));
                paramList.Add(GetParameterInputString("@phone", ipEmp.EmpPhone));
                paramList.Add(GetParameterInputString("@email_address", ipEmp.EmpEmail));
                paramList.Add(GetParameterInputString("@user_name", ipEmp.UserName));

                paramList.Add(GetParameterOutputInt("@createdId", null));

                Hashtable oParams = ExecuteNonQuery("dbo.sp_IPEmpInsertCommand", paramList);
                ipEmp.UserID = IntObj.FromObj(oParams["@createdId"]);

                this.CommitTransaction();

                //  return the  item, whose Id should now be set				
                return ipEmp;

            }
            catch (Exception e)
            {
                this.RollbackTransaction();
                ipEmp.UserID = null;
                throw e;
            }
        }


        private IPEmp Update(IPEmp ipEmp)
        {
            try
            {
                this.StartTransaction();

                ArrayList paramList = new ArrayList();

                //  update table jobreq data 
                //****Currently used on completing saved requests and form edits***
                paramList.Add(GetParameterInputInt("@userID", ipEmp.UserID));
                paramList.Add(GetParameterInputString("@fname", ipEmp.FName));
                paramList.Add(GetParameterInputString("@lname", ipEmp.LName));
                paramList.Add(GetParameterInputString("@mname", ipEmp.MName));
                paramList.Add(GetParameterInputString("@job_title", ipEmp.JobTitle));
                paramList.Add(GetParameterInputString("@empID", ipEmp.EmpID));
                paramList.Add(GetParameterInputString("@deptID", ipEmp.DeptID));
                paramList.Add(GetParameterInputString("@dept_name", ipEmp.DeptName));
                paramList.Add(GetParameterInputString("@phone", ipEmp.EmpPhone));
                paramList.Add(GetParameterInputString("@email_address", ipEmp.EmpEmail));
                paramList.Add(GetParameterInputString("@user_name", ipEmp.UserName));

                ExecuteNonQuery("dbo.sp_IPEmpUpdateCommand", paramList);

                this.CommitTransaction();

                //  return the  item, whose Id should now be set				
                return ipEmp;

            }
            catch (Exception e)
            {
                this.RollbackTransaction();
                throw e;
            }
        }


        public IPEmp GetIPEmpByuserID(IntObj userID)
        {
            //  null in null out
            if (userID == null) { return null; }

            //  initialize some stuff
            IPEmp ipEmp = null;
            DataTable dt = null;

            try
            {
                ArrayList paramList = new ArrayList();

                paramList.Add(GetParameterInputInt("@userID", userID));

                //  Execute the stored procedure
                dt = ExecDataTable("dbo.sp_IPEmpSelectCommand", paramList);

                //  Pull out the  data
                if (dt != null && dt.Rows.Count > 0)
                {
                    ipEmp = new IPEmp();
                    ipEmp.UserID = GetInt(dt.Rows[0]["userID"]);
                    ipEmp.FName = GetString(dt.Rows[0]["fname"]);
                    ipEmp.LName = GetString(dt.Rows[0]["lname"]);
                    ipEmp.MName = GetString(dt.Rows[0]["mname"]);
                    ipEmp.JobTitle = GetString(dt.Rows[0]["job_title"]);
                    ipEmp.EmpID = GetString(dt.Rows[0]["empID"]);
                    ipEmp.DeptID = GetString(dt.Rows[0]["deptID"]);
                    ipEmp.DeptName = GetString(dt.Rows[0]["dept_name"]);
                    ipEmp.EmpPhone = GetString(dt.Rows[0]["phone"]);
                    ipEmp.EmpEmail = GetString(dt.Rows[0]["email_address"]);
                    ipEmp.SubDate = GetDate(dt.Rows[0]["sub_date"]);
                }
            }
            catch (Exception e)
            {
                //  we could log this here if we want, but at this point lets just throw these up the chain
                throw e;
            }

            //  return the collection (or if no records returned this will be null)
            return ipEmp;
        }


        //update IPEmp table with the non-exempt status
        public void UpdateIPEmpStatusByUserID(String userID, String empStatus)
        {
            if (userID == null) { return; }

            try
            {
                //  search params
                ArrayList paramList = new ArrayList();
                paramList.Add(GetParameterInputString("@userID", userID));
                paramList.Add(GetParameterInputString("@emp_status", empStatus));
                //  Execute the stored procedure
                ExecuteNonQuery("sp_UpdateIPEmpStatusByUserID", paramList);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region IPR Save/Update Methods

        public IPR Save(IPR ipr)
        {
            if (ipr.IpID == null)
            {
                return Insert(ipr);
            }
            return Update(ipr);
        }


        private IPR Insert(IPR ipr)
        {
            try
            {
                this.StartTransaction();

                ArrayList paramList = new ArrayList();

                //  save into table jobreq data 
                //****Not used for now******
                paramList.Add(GetParameterInputString("@formID", ipr.FormID));
                paramList.Add(GetParameterInputString("@userID", ipr.UserID));
                paramList.Add(GetParameterInputString("@empID", ipr.EmpID));
                paramList.Add(GetParameterInputString("@model", ipr.Model));
                paramList.Add(GetParameterInputString("@make", ipr.Make));
                paramList.Add(GetParameterInputString("@vendor", ipr.VendorNum));
                paramList.Add(GetParameterInputString("@size", ipr.Size));
                paramList.Add(GetParameterInputString("@device_type", ipr.DeviceType));
                paramList.Add(GetParameterInputString("@security_controls", ipr.SecurityControls));
                paramList.Add(GetParameterInputString("@os_version", ipr.OsVersion));
                paramList.Add(GetParameterInputString("@serial_number", ipr.SerialNumber));
                paramList.Add(GetParameterInputString("@bus_justification_type", ipr.BusJustType));
                paramList.Add(GetParameterInputString("@bus_justification", ipr.BusJustification));
                paramList.Add(GetParameterInputString("@secured_ack1", ipr.SecuredAck1));
                paramList.Add(GetParameterInputString("@secured_ack2", ipr.SecuredAck2));
                paramList.Add(GetParameterInputString("@secured_ack3", ipr.SecuredAck3));
                paramList.Add(GetParameterInputString("@secured_ack4", ipr.SecuredAck4));
                paramList.Add(GetParameterInputString("@secured_ack5", ipr.SecuredAck5));
                paramList.Add(GetParameterInputString("@secured_ack6", ipr.SecuredAck6));
                paramList.Add(GetParameterInputString("@security_ack1", ipr.SecurityAck1));
                paramList.Add(GetParameterInputString("@security_ack2", ipr.SecurityAck2));
                paramList.Add(GetParameterInputString("@security_ack3", ipr.SecurityAck3));
                paramList.Add(GetParameterInputString("@security_ack4", ipr.SecurityAck4));
                paramList.Add(GetParameterInputString("@security_ack5", ipr.SecurityAck5));
                paramList.Add(GetParameterInputString("@security_ack6", ipr.SecurityAck6));
                paramList.Add(GetParameterInputString("@security_ack7", ipr.SecurityAck7));
                paramList.Add(GetParameterInputString("@security_ack8", ipr.SecurityAck8));
                paramList.Add(GetParameterInputString("@security_ack9", ipr.SecurityAck9));
                paramList.Add(GetParameterInputString("@renown_owned_type", ipr.RenownOwnedType));
                paramList.Add(GetParameterInputString("@renown_owned_carrier", ipr.RenownOwnedCarrier));
                paramList.Add(GetParameterInputString("@renown_owned_phone", ipr.RenownOwnedPhone));
                paramList.Add(GetParameterInputString("@person_owned_type", ipr.PersonOwnedType));
                paramList.Add(GetParameterInputString("@person_owned_carrier", ipr.PersonOwnedCarrier));
                paramList.Add(GetParameterInputString("@person_owned_phone", ipr.PersonOwnedPhone));
                paramList.Add(GetParameterInputString("@person_owned_SN", ipr.PersonOwnedSN));
                paramList.Add(GetParameterInputString("@person_owned_OS", ipr.PersonOwnedOS));
                paramList.Add(GetParameterInputString("@sync_to_calendar", ipr.SyncToCalendar));
                paramList.Add(GetParameterInputString("@sync_to_contacts", ipr.SyncToContacts));
                paramList.Add(GetParameterInputString("@sync_to_email", ipr.SyncToEmail));
                paramList.Add(GetParameterInputString("@physical_location", ipr.PhysLocation));
                paramList.Add(GetParameterInputString("@users_access_computer", ipr.UserAccessComputer));
                paramList.Add(GetParameterInputString("@users_access_media", ipr.UserAccessMedia));
                paramList.Add(GetParameterInputString("@type_of_data", ipr.TypeOfData));
                paramList.Add(GetParameterInputString("@data_sample", ipr.DataSample));
                paramList.Add(GetParameterInputString("@data_storage", ipr.DataStorage));
                paramList.Add(GetParameterInputString("@chk_server", ipr.ChkServer));
                paramList.Add(GetParameterInputString("@chk_apps", ipr.ChkApps));
                paramList.Add(GetParameterInputString("@chk_LAN", ipr.ChkLAN));
                paramList.Add(GetParameterInputString("@chk_workstation", ipr.ChkWorkstation));
                paramList.Add(GetParameterInputString("@access_to_server", ipr.AccessToServer));
                paramList.Add(GetParameterInputString("@access_to_applications", ipr.AccessToApp));
                paramList.Add(GetParameterInputString("@access_to_LAN", ipr.AccessToLAN));
                paramList.Add(GetParameterInputString("@connection_type", ipr.ConnectionType));
                paramList.Add(GetParameterInputString("@access_from_home", ipr.AccessFromHome));
                paramList.Add(GetParameterInputString("@access_from_business", ipr.AccessFromBusiness));
                paramList.Add(GetParameterInputString("@employee_signature", ipr.EmployeeSignature));

                paramList.Add(GetParameterOutputInt("@createdId", null));

                Hashtable oParams = ExecuteNonQuery("dbo.sp_IPRInsertCommand", paramList);
                ipr.IpID = IntObj.FromObj(oParams["@createdId"]);

                this.CommitTransaction();

                //  return the  item, whose Id should now be set				
                return ipr;

            }
            catch (Exception e)
            {
                this.RollbackTransaction();
                ipr.IpID = null;
                throw e;
            }
        }


        private IPR Update(IPR ipr)
        {
            try
            {
                this.StartTransaction();

                ArrayList paramList = new ArrayList();

                //  update table jobreq data 
                //****Currently used on completing saved requests and form edits***
                paramList.Add(GetParameterInputInt("@ipID", ipr.IpID));
                paramList.Add(GetParameterInputString("@formID", ipr.FormID));
                paramList.Add(GetParameterInputString("@userID", ipr.UserID));
                paramList.Add(GetParameterInputString("@empID", ipr.EmpID));
                paramList.Add(GetParameterInputString("@model", ipr.Model));
                paramList.Add(GetParameterInputString("@make", ipr.Make));
                paramList.Add(GetParameterInputString("@vendor", ipr.VendorNum));
                paramList.Add(GetParameterInputString("@size", ipr.Size));
                paramList.Add(GetParameterInputString("@device_type", ipr.DeviceType));
                paramList.Add(GetParameterInputString("@security_controls", ipr.SecurityControls));
                paramList.Add(GetParameterInputString("@os_version", ipr.OsVersion));
                paramList.Add(GetParameterInputString("@serial_number", ipr.SerialNumber));
                paramList.Add(GetParameterInputString("@bus_justification_type", ipr.BusJustType));
                paramList.Add(GetParameterInputString("@bus_justification", ipr.BusJustification));
                paramList.Add(GetParameterInputString("@secured_ack1", ipr.SecuredAck1));
                paramList.Add(GetParameterInputString("@secured_ack2", ipr.SecuredAck2));
                paramList.Add(GetParameterInputString("@secured_ack3", ipr.SecuredAck3));
                paramList.Add(GetParameterInputString("@secured_ack4", ipr.SecuredAck4));
                paramList.Add(GetParameterInputString("@secured_ack5", ipr.SecuredAck5));
                paramList.Add(GetParameterInputString("@secured_ack6", ipr.SecuredAck6));
                paramList.Add(GetParameterInputString("@security_ack1", ipr.SecurityAck1));
                paramList.Add(GetParameterInputString("@security_ack2", ipr.SecurityAck2));
                paramList.Add(GetParameterInputString("@security_ack3", ipr.SecurityAck3));
                paramList.Add(GetParameterInputString("@security_ack4", ipr.SecurityAck4));
                paramList.Add(GetParameterInputString("@security_ack5", ipr.SecurityAck5));
                paramList.Add(GetParameterInputString("@security_ack6", ipr.SecurityAck6));
                paramList.Add(GetParameterInputString("@security_ack7", ipr.SecurityAck7));
                paramList.Add(GetParameterInputString("@security_ack8", ipr.SecurityAck8));
                paramList.Add(GetParameterInputString("@security_ack9", ipr.SecurityAck9));
                paramList.Add(GetParameterInputString("@renown_owned_type", ipr.RenownOwnedType));
                paramList.Add(GetParameterInputString("@renown_owned_carrier", ipr.RenownOwnedCarrier));
                paramList.Add(GetParameterInputString("@renown_owned_phone", ipr.RenownOwnedPhone));
                paramList.Add(GetParameterInputString("@person_owned_type", ipr.PersonOwnedType));
                paramList.Add(GetParameterInputString("@person_owned_carrier", ipr.PersonOwnedCarrier));
                paramList.Add(GetParameterInputString("@person_owned_phone", ipr.PersonOwnedPhone));
                paramList.Add(GetParameterInputString("@person_owned_SN", ipr.PersonOwnedSN));
                paramList.Add(GetParameterInputString("@person_owned_OS", ipr.PersonOwnedOS));
                paramList.Add(GetParameterInputString("@sync_to_calendar", ipr.SyncToCalendar));
                paramList.Add(GetParameterInputString("@sync_to_contacts", ipr.SyncToContacts));
                paramList.Add(GetParameterInputString("@sync_to_email", ipr.SyncToEmail));
                paramList.Add(GetParameterInputString("@physical_location", ipr.PhysLocation));
                paramList.Add(GetParameterInputString("@users_access_computer", ipr.UserAccessComputer));
                paramList.Add(GetParameterInputString("@users_access_media", ipr.UserAccessMedia));
                paramList.Add(GetParameterInputString("@type_of_data", ipr.TypeOfData));
                paramList.Add(GetParameterInputString("@data_sample", ipr.DataSample));
                paramList.Add(GetParameterInputString("@data_storage", ipr.DataStorage));
                paramList.Add(GetParameterInputString("@chk_server", ipr.ChkServer));
                paramList.Add(GetParameterInputString("@chk_apps", ipr.ChkApps));
                paramList.Add(GetParameterInputString("@chk_LAN", ipr.ChkLAN));
                paramList.Add(GetParameterInputString("@chk_workstation", ipr.ChkWorkstation));
                paramList.Add(GetParameterInputString("@access_to_server", ipr.AccessToServer));
                paramList.Add(GetParameterInputString("@access_to_applications", ipr.AccessToApp));
                paramList.Add(GetParameterInputString("@access_to_LAN", ipr.AccessToLAN));
                paramList.Add(GetParameterInputString("@connection_type", ipr.ConnectionType));
                paramList.Add(GetParameterInputString("@access_from_home", ipr.AccessFromHome));
                paramList.Add(GetParameterInputString("@access_from_business", ipr.AccessFromBusiness));
                paramList.Add(GetParameterInputString("@employee_signature", ipr.EmployeeSignature));

                ExecuteNonQuery("dbo.sp_IPRUpdateCommand", paramList);

                this.CommitTransaction();

                //  return the  item, whose Id should now be set				
                return ipr;

            }
            catch (Exception e)
            {
                this.RollbackTransaction();
                throw e;
            }
        }


        public IPR GetIPRByID(IntObj ipID)
        {
            //  null in null out
            if (ipID == null) { return null; }

            //  initialize some stuff
            IPR ipr = null;
            DataTable dt = null;

            try
            {
                ArrayList paramList = new ArrayList();

                paramList.Add(GetParameterInputInt("@ipID", ipID));

                //  Execute the stored procedure
                dt = ExecDataTable("dbo.sp_IPRSelectCommandByID", paramList);

                //  Pull out the  data
                if (dt != null && dt.Rows.Count > 0)
                {
                    ipr = new IPR();
                    ipr.IpID = GetInt(dt.Rows[0]["ipID"]);
                    ipr.FormID = GetString(dt.Rows[0]["formID"]);
                    ipr.UserID = GetString(dt.Rows[0]["userID"]);
                    ipr.EmpID = GetString(dt.Rows[0]["empID"]);
                    ipr.Model = GetString(dt.Rows[0]["model"]);
                    ipr.Make = GetString(dt.Rows[0]["make"]);
                    ipr.VendorNum = GetString(dt.Rows[0]["vendor"]);
                    ipr.Size = GetString(dt.Rows[0]["size"]);
                    ipr.DeviceType = GetString(dt.Rows[0]["device_type"]);
                    ipr.SecurityControls = GetString(dt.Rows[0]["security_controls"]);
                    ipr.OsVersion = GetString(dt.Rows[0]["os_version"]);
                    ipr.SerialNumber = GetString(dt.Rows[0]["serial_number"]);
                    ipr.BusJustType = GetString(dt.Rows[0]["bus_justification_type"]);
                    ipr.BusJustification = GetString(dt.Rows[0]["bus_justification"]);
                    ipr.SecuredAck1 = GetString(dt.Rows[0]["secured_ack1"]);
                    ipr.SecuredAck2 = GetString(dt.Rows[0]["secured_ack2"]);
                    ipr.SecuredAck3 = GetString(dt.Rows[0]["secured_ack3"]);
                    ipr.SecuredAck4 = GetString(dt.Rows[0]["secured_ack4"]);
                    ipr.SecuredAck5 = GetString(dt.Rows[0]["secured_ack5"]);
                    ipr.SecuredAck6 = GetString(dt.Rows[0]["secured_ack6"]);
                    ipr.SecurityAck1 = GetString(dt.Rows[0]["security_ack1"]);
                    ipr.SecurityAck2 = GetString(dt.Rows[0]["security_ack2"]);
                    ipr.SecurityAck3 = GetString(dt.Rows[0]["security_ack3"]);
                    ipr.SecurityAck4 = GetString(dt.Rows[0]["security_ack4"]);
                    ipr.SecurityAck5 = GetString(dt.Rows[0]["security_ack5"]);
                    ipr.SecurityAck6 = GetString(dt.Rows[0]["security_ack6"]);
                    ipr.SecurityAck7 = GetString(dt.Rows[0]["security_ack7"]);
                    ipr.SecurityAck8 = GetString(dt.Rows[0]["security_ack8"]);
                    ipr.SecurityAck9 = GetString(dt.Rows[0]["security_ack9"]);
                    ipr.RenownOwnedType = GetString(dt.Rows[0]["renown_owned_type"]);
                    ipr.RenownOwnedCarrier = GetString(dt.Rows[0]["renown_owned_carrier"]);
                    ipr.RenownOwnedPhone = GetString(dt.Rows[0]["renown_owned_phone"]);
                    ipr.PersonOwnedType = GetString(dt.Rows[0]["person_owned_type"]);
                    ipr.PersonOwnedCarrier = GetString(dt.Rows[0]["person_owned_carrier"]);
                    ipr.PersonOwnedPhone = GetString(dt.Rows[0]["person_owned_phone"]);
                    ipr.PersonOwnedSN = GetString(dt.Rows[0]["person_owned_SN"]);
                    ipr.PersonOwnedOS = GetString(dt.Rows[0]["person_owned_OS"]);
                    ipr.SyncToCalendar = GetString(dt.Rows[0]["sync_to_calendar"]);
                    ipr.SyncToContacts = GetString(dt.Rows[0]["sync_to_contacts"]);
                    ipr.SyncToEmail = GetString(dt.Rows[0]["sync_to_email"]);
                    ipr.PhysLocation = GetString(dt.Rows[0]["physical_location"]);
                    ipr.UserAccessComputer = GetString(dt.Rows[0]["users_access_computer"]);
                    ipr.UserAccessMedia = GetString(dt.Rows[0]["users_access_media"]);
                    ipr.TypeOfData = GetString(dt.Rows[0]["type_of_data"]);
                    ipr.DataSample = GetString(dt.Rows[0]["data_sample"]);
                    ipr.DataStorage = GetString(dt.Rows[0]["data_storage"]);
                    ipr.ChkServer = GetString(dt.Rows[0]["chk_server"]);
                    ipr.ChkApps = GetString(dt.Rows[0]["chk_apps"]);
                    ipr.ChkLAN = GetString(dt.Rows[0]["chk_LAN"]);
                    ipr.ChkWorkstation = GetString(dt.Rows[0]["chk_workstation"]);
                    ipr.AccessToServer = GetString(dt.Rows[0]["access_to_server"]);
                    ipr.AccessToApp = GetString(dt.Rows[0]["access_to_applications"]);
                    ipr.AccessToLAN = GetString(dt.Rows[0]["access_to_LAN"]);
                    ipr.ConnectionType = GetString(dt.Rows[0]["connection_type"]);
                    ipr.AccessFromHome = GetString(dt.Rows[0]["access_from_home"]);
                    ipr.AccessFromBusiness = GetString(dt.Rows[0]["access_from_business"]);
                    ipr.EmployeeSignature = GetString(dt.Rows[0]["employee_signature"]);
                    ipr.SubmitDate = GetDate(dt.Rows[0]["submit_date"]);
                    ipr.FsupName = GetString(dt.Rows[0]["fsupName"]); ;
                    ipr.FsupApprove = GetString(dt.Rows[0]["fsupApprove"]);
                    ipr.FsupApproveDate = GetDate(dt.Rows[0]["fsupApproveDate"]);
                    ipr.VphrName = GetString(dt.Rows[0]["vphrName"]);
                    ipr.VphrApprove = GetString(dt.Rows[0]["vphrApprove"]);
                    ipr.VphrApproveDate = GetDate(dt.Rows[0]["vphrApproveDate"]);
                    ipr.RhcfoName = GetString(dt.Rows[0]["rhcfoName"]);
                    ipr.RhcfoApprove = GetString(dt.Rows[0]["rhcfoApprove"]);
                    ipr.RhcfoApproveDate = GetDate(dt.Rows[0]["rhcfoApproveDate"]);
                    ipr.IpdName = GetString(dt.Rows[0]["ipdName"]);
                    ipr.IpdApprove = GetString(dt.Rows[0]["ipdApprove"]);
                    ipr.IpdApproveDate = GetDate(dt.Rows[0]["ipdApproveDate"]);
                    ipr.RhcioName = GetString(dt.Rows[0]["cioName"]);
                    ipr.RhcioApprove = GetString(dt.Rows[0]["cioApprove"]);
                    ipr.RhcioApproveDate = GetDate(dt.Rows[0]["cioApproveDate"]);
                    ipr.ApprovalStatus = GetString(dt.Rows[0]["approvalStatus"]);
                    ipr.Archive = GetString(dt.Rows[0]["archive"]);
                    ipr.Comments = GetString(dt.Rows[0]["comments"]);
                }
            }
            catch (Exception e)
            {
                //  we could log this here if we want, but at this point lets just throw these up the chain
                throw e;
            }

            //  return the collection (or if no records returned this will be null)
            return ipr;
        }


        public void UpdateNotifiedByUserID(String userID)
        {
            if (userID == null) { return; }

            try
            {
                //  search params
                ArrayList paramList = new ArrayList();
                paramList.Add(GetParameterInputString("@userID", userID));

                //  Execute the stored procedure
                ExecuteNonQuery("sp_IPRUpdateNotifiedByUserID", paramList);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

    }
}
