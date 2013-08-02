using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IpModelData;
using System.Linq;
using JPR;
using WMC.Core.Employees.BusObj2008;
using System.Web.Configuration;
using IpDataProvider;

namespace InformationProtection.Models
{
    public class IpRequestorView
    {
        public List<IpRequestor> requestors = new List<IpRequestor>();


        public IpRequestorView()
        {

        }

        public int CreateRequestor(String EmpID, String role)
        {
            EmployeeView employeeView = new EmployeeView();
            Employee employee = employeeView.DbGetEmployeeByEmpId(EmpID);

            IpRequestorViewData thisEmp = null;
            int requestorId = 0;
            if (employee != null)
            {
                thisEmp = new IpRequestorViewData
                {
                    DeptID = employee.Department.Dept_id.IntValue.ToString(),
                    DeptName = employee.Department.Dept_name,
                    Email = employee.Email,
                    JobTitle = employee.JobTitle,
                    Lname = employee.LastName,
                    Mname = employee.MiddleName,
                    Fname = employee.FirstName,
                    PhoneNumber = String.Empty,
                    EmpID = employee.Emp_id.ToString()
                };
                requestorId = CreateRequestor(thisEmp, role);
            }
            return requestorId;
        }


        public int CreateRequestor(IpRequestorViewData newRequestor, String role)
        {
            IpRequestor data = Convert(newRequestor);
            int requestorId = 0;
            // CHECK To see if requestor allready exist
            IpRequestorViewData requestor1 = GetRequestor(newRequestor.EmpID);
            
            if (requestor1 == null)
            {
                String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
                RequestorDbReqAccess RequestorDbAcess = new RequestorDbReqAccess(connectionString);
                requestorId = RequestorDbAcess.Create(data);
            }
            else
            {
                requestorId = requestor1.IpRequestorId;
            }

            if (requestorId > 0)
            {
                List<IpRequestorViewData> requestors = GetRequestorsInRole(role);
                IpRequestorViewData result = (from item in requestors
                            where item.IpRequestorId == requestorId
                            select item).FirstOrDefault();
                if (result == null)
                {
                    UsersInRoleView modelUsers = new UsersInRoleView();
                    modelUsers.CreateUserInRole(requestorId, role);
                }
            }

            return requestorId;
        }

        public bool Update(IpRequestorViewData data)
        {
            int result = 0;
            IpRequestor newRequestor = Convert(data);
            IpRequestorViewData dataCheck = GetRequestor(data.EmpID);
            if (dataCheck != null)
            {

                String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
                RequestorDbReqAccess RequestorDbAcess = new RequestorDbReqAccess(connectionString);
                result = RequestorDbAcess.Update(newRequestor);
            }

            return result > 0;
        }

        public int UpdateRequestorRoll(string RollName, string RequestorRollButton, string RequestorId)
        {
            UsersInRoleView modelUsers = new UsersInRoleView();
            int requestorIdInt = 0;
            int.TryParse(RequestorId, out requestorIdInt);
            int result = 0;
            IpRequestorViewData requestor = GetRequestorByRequestorId(requestorIdInt);


            
            if (RequestorRollButton == "Add Roll")
            {
                bool addNewRoll = true;
                // CHECK TO see if he has already got the roll
                if (requestor.IsAdmin && RollName == Roles.RoleNameEnum.admin.ToString())
                {
                    addNewRoll = false;
                }
                if (requestor.IsUser && RollName == Roles.RoleNameEnum.user.ToString())
                {
                    addNewRoll = false;
                }
                if (requestor.IsApprover && RollName == Roles.RoleNameEnum.approver.ToString())
                {
                    addNewRoll = false;
                }
                if (addNewRoll)
                {
                    result = modelUsers.CreateUserInRole(requestorIdInt, RollName);
                }
            }
            else if (RequestorRollButton == "Remove Roll")
            {
                bool removeRoll = false;
                if (requestor.IsAdmin && RollName == Roles.RoleNameEnum.admin.ToString())
                {
                    removeRoll = true;
                }
                if (requestor.IsUser && RollName == Roles.RoleNameEnum.user.ToString())
                {
                    removeRoll = true;
                }
                if (requestor.IsApprover && RollName == Roles.RoleNameEnum.approver.ToString())
                {
                    removeRoll = true;
                }
                if (removeRoll)
                {
                    result = modelUsers.RemoveUserInRole(requestorIdInt, RollName);
                }
            }
            return result;
        }


        public List<SelectListItem> GetRolls()
        {

            List<SelectListItem> retData = new List<SelectListItem>();

            SelectListItem itemx = new SelectListItem
            {
                Value = IpModelData.Roles.RoleNameEnum.approver.ToString(),
                Text =  IpModelData.Roles.RoleNameEnum.approver.ToString()
            };
            retData.Add(itemx);
            SelectListItem itemy = new SelectListItem
            {
                Value = IpModelData.Roles.RoleNameEnum.admin.ToString(),
                Text = IpModelData.Roles.RoleNameEnum.admin.ToString()
            };
            retData.Add(itemy);
            SelectListItem itema = new SelectListItem
            {
                Value = IpModelData.Roles.RoleNameEnum.user.ToString(),
                Text = IpModelData.Roles.RoleNameEnum.user.ToString()
            };
            retData.Add(itema);


            return retData;      
        }




        public List<IpRequestorViewData> GetAllApprovers(String controller, String action)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RequestorDbReqAccess ipRequest = new RequestorDbReqAccess(connectionString);
            List<IpRequestor> data = ipRequest.GetRequestorsInRole(IpModelData.Roles.RoleNameEnum.approver.ToString());
            List<IpRequestorViewData> approvers = CombineRoles(data);

            ApprovalRequestDbAccess approvalRequestDb = new ApprovalRequestDbAccess(connectionString);
            List<IpApprovalRequest> temp = approvalRequestDb.GetRequestByState(IpApprover.ApproveState.pending.ToString());
            List<IpApprovalRequestViewData> allPending = IpApprovalRequestView.Convert(temp);

            foreach (IpApprovalRequestViewData item in allPending)
            {
                foreach (IpRequestorViewData approver in approvers)
                {
                    int empId = 0;
                    String tmpEmpId = approver.EmpID;
                    int.TryParse(tmpEmpId, out empId);
                    if (item.IsPending(empId))
                    {
                        approver.NumberOfPendingReq++;
                    }
                }

            }
            // add the link for approvers details
            foreach (IpRequestorViewData approver in approvers)
            {
                approver.RequestDetailsLink = String.Format("<a href=\"{0}/{1}?ApproverEmpID={2}\">Details</a>", 
                    controller, action, approver.EmpID);
            }
            return approvers;
        }


        public IpRequestorViewData GetRequestor(String EmpID)
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RequestorDbReqAccess RequestorDbAcess = new RequestorDbReqAccess(connectionString);
            List<IpRequestor> reqtor = RequestorDbAcess.GetRequestorByEmpID(EmpID);

            IpRequestorViewData retData = null;
            if (reqtor != null && reqtor.Count > 0)
            {
                retData = Convert(reqtor[0]);
                retData.RoleId = new List<string>();
                retData.RoleName = new List<string>();
                foreach (IpRequestor item in reqtor)
                {
                    retData.RoleName.Add(item.RoleName);
                    retData.RoleId.Add(item.RoleId);
                }
            }

            return retData;
        }


        public IpRequestorViewData GetRequestorByRequestorId(int RequestorId)
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RequestorDbReqAccess RequestorDbAcess = new RequestorDbReqAccess(connectionString);
            List<IpRequestor> reqtor = RequestorDbAcess.GetRequestorsIncludingRoles(RequestorId.ToString());
            IpRequestorViewData retData = new IpRequestorViewData();

            if (reqtor != null && reqtor.Count > 0)
            {
                retData = Convert(reqtor[0]);
                retData.RoleId = new List<string>();
                retData.RoleName = new List<string>();
                foreach (IpRequestor item in reqtor)
                {
                    retData.RoleName.Add(item.RoleName);
                    retData.RoleId.Add(item.RoleId);
                }
            }

            return retData;
        }


        public IpRequestorViewData GetRequestorIncludeRoles(String LoginId)
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RequestorDbReqAccess RequestorDbAcess = new RequestorDbReqAccess(connectionString);

            EmployeeView employeeView = new EmployeeView();
            IpRequestorViewData employeeInfo = employeeView.GetUserInfo(LoginId);
            IpRequestorViewData requestor = GetRequestor(employeeInfo.EmpID);
            if (requestor == null)
            {
                return null;
            }
            List<IpRequestor> reqtor = RequestorDbAcess.GetRequestorsIncludingRoles(requestor.IpRequestorId.ToString());

            IpRequestorViewData retData = null;
            if (reqtor != null && reqtor.Count > 0)
            {
                retData = Convert(reqtor[0]);
                retData.RoleId = new List<string>();
                retData.RoleName = new List<string>();
                foreach (IpRequestor item in reqtor)
                {
                    retData.RoleName.Add(item.RoleName);
                    retData.RoleId.Add(item.RoleId);
                }
            }
            return retData;
        }


        public List<IpRequestorViewData> GetRequestorsIncludeRoles(String controllerType)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RequestorDbReqAccess RequestorDbAcess = new RequestorDbReqAccess(connectionString);
            List<IpRequestor> ourDbData = RequestorDbAcess.GetRequestorsIncludeRoles();
            List<IpRequestorViewData> outData = CombineRoles(ourDbData);

            foreach (IpRequestorViewData item in outData)
            {
                if (controllerType == "admin")
                {
                    item.RequestDetailsLink = String.Format("<a href=\"AdminView/Edit?EmpID={0}\">Edit</a>",
                        item.EmpID);
                }
                else
                {
                    item.RequestDetailsLink = String.Format("<a href=\"UsersView?EmpID={0}\">UsersView</a>",
                        item.EmpID);
                }

            }

            return outData;
        }

        public List<IpRequestorViewData> GetRequestorsInRole(String RoleName)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RequestorDbReqAccess RequestorDbAcess = new RequestorDbReqAccess(connectionString);
            List<IpRequestor> reqtor = RequestorDbAcess.GetRequestorsInRole(RoleName);
            List<IpRequestorViewData> outData = CombineRoles(reqtor);

            return Convert(reqtor);
        }


        public List<IpRequestorViewData> CombineRoles(List<IpRequestor> requestors)
        {
            List<IpRequestorViewData> outData = new List<IpRequestorViewData>();
            foreach (IpRequestor item in requestors)
            {
                int index = -1;
                index = outData.FindIndex(C => C.IpRequestorId == item.IpRequestorId);
                
                if (index >= 0)
                {  // a requestor already exist
                    IpRequestorViewData existingItem = outData[index];
                    outData.RemoveAt(index);
                    if (item.RoleName != null && existingItem.RoleName.Contains(item.RoleName) == false)
                    {
                        if (existingItem.RoleName == null)
                        {
                            existingItem.RoleName = new List<string>();
                            existingItem.RoleId = new List<string>();
                        }
                        existingItem.RoleName.Add(item.RoleName);
                        existingItem.RoleId.Add(item.RoleId);
                    }
                    outData.Add(existingItem);
                }
                else
                {  // new item
                    IpRequestorViewData convertedItem = Convert(item);
                    if (item.RoleName != null)
                    {
                        if (convertedItem.RoleName == null)
                        {
                            convertedItem.RoleName = new List<string>();
                            convertedItem.RoleId = new List<string>();
                        }
                        convertedItem.RoleName.Add(item.RoleName);
                        convertedItem.RoleId.Add(item.RoleId);
                    }
                    outData.Add(convertedItem);
                }

            }
            return outData;
        }

        public IpRequestorViewData GetRequestorByLoginId(String UserName)
        {
            EmployeeView employeeView = new EmployeeView();
            IpRequestorViewData userInfo = employeeView.GetUserInfo(UserName);
            IpRequestorViewData requestor = GetRequestor(userInfo.EmpID);
            return requestor;
        }


        public List<IpRequestorViewData> GetAllRequestors()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RequestorDbReqAccess RequestorDbAcess = new RequestorDbReqAccess(connectionString);
            List<IpRequestor> requestors = RequestorDbAcess.GetRequestors();

            List<IpRequestorViewData> retData = Convert(requestors);
            return retData;
        }


        public static List<IpRequestorViewData> Convert(List<IpRequestor> ourData)
        {

            List<IpRequestorViewData> retData = new List<IpRequestorViewData>();
            foreach (IpRequestor device in ourData)
            {
                retData.Add(Convert(device));
            }
            return retData;
        }

        public static List<IpRequestor> Convert(List<IpRequestorViewData> ourData)
        {
            List<IpRequestor> retData = new List<IpRequestor>();
            foreach (IpRequestorViewData device in ourData)
            {
                retData.Add(Convert(device));
            }
            return retData;
        }

        public static IpRequestorViewData Convert(IpRequestor data)
        {
            IpRequestorViewData retData = null;
            if (data != null)
            {
                retData = new IpRequestorViewData
                {
                    DeptID = data.DeptID,
                    DeptName = data.DeptName,
                    Email = data.Email,
                    EmpID = data.EmpID,
                    Fname = data.Fname,
                    IpRequestorId = data.IpRequestorId,
                    JobTitle = data.JobTitle,
                    Lname = data.Lname,
                    Mname = data.Mname,
                    PhoneNumber = data.PhoneNumber,
                };
            }

            return retData;
        }
        public static IpRequestor Convert(IpRequestorViewData data)
        {
            IpRequestor retData = null;
            if (data != null)
            {
                retData = new IpRequestor
                {
                    DeptID = data.DeptID,
                    DeptName = data.DeptName,
                    Email = data.Email,
                    EmpID = data.EmpID,
                    Fname = data.Fname,
                    IpRequestorId = data.IpRequestorId,
                    JobTitle = data.JobTitle,
                    Lname = data.Lname,
                    Mname = data.Mname,
                    PhoneNumber = data.PhoneNumber,
                };
            }

            return retData;
        }



    }
}