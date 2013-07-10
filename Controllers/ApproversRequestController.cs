using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InformationProtection.Models;
using JPR;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Controllers
{
    public class ApproversRequestController : BaseController
    {
        //
        // GET: /ApproversView/

        public ActionResult Index()
        {
            String EmpId = null;
            String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
            if (!Membership.ValidateUser(LoginUserName, "MyPass"))
                ModelState.AddModelError("", "Incorrect username or password");
            string[] userRoles = System.Web.Security.Roles.GetRolesForUser(LoginUserName);
            if (!userRoles.Contains(IpModelData.Roles.RoleNameEnum.approver.ToString()))
            {
                return RedirectToAction("Index", "UsersView");
            }
            EmployeeView model = new EmployeeView();
            IpRequestorViewData thisEmployee = model.GetUserInfo(LoginUserName);
            EmpId = thisEmployee.EmpID;

            return RedirectToAction("Approvers", new { ApproverEmpID = EmpId });
        }

        public ActionResult Details(String Id, String ApproverEmpID)
        {

            IpApprovalRequestView Model = new IpApprovalRequestView();
            IpApprovalRequestViewData data = Model.GetApprovalRequest(Id);


            IpRequestorView rModel = new IpRequestorView();
            IpRequestorViewData requestor = rModel.GetRequestorByRequestorId(data.IpRequestorId);
            IpRequestorViewData approver = rModel.GetRequestor(ApproverEmpID);
            ViewBag.approver = approver;
            ViewBag.requestor = requestor;
            return View(data);
        }

        [HttpPost]
        public ActionResult ViewRequest(String ApprovalRequestId)
        {
            IpApprovalRequestView Model = new IpApprovalRequestView();
            IpApprovalRequestViewData data = Model.GetApprovalRequest(ApprovalRequestId);

            return View(data);
        }

        /// <summary>
        /// Display a list of approvers pending data
        /// for the approver with EMPID
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public ActionResult Approvers(String ApproverEmpID)
        {
            String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
            if (!Membership.ValidateUser(LoginUserName, "MyPass"))
                ModelState.AddModelError("", "Incorrect username or password");
            string[] userRoles = System.Web.Security.Roles.GetRolesForUser(LoginUserName);
            if (!userRoles.Contains(IpModelData.Roles.RoleNameEnum.approver.ToString()))
            {
                return RedirectToAction("Index", "UsersView");
            }

            ViewBag.ApproverEmpID = ApproverEmpID;
            IpApproverViewData thisEmp = new IpApproverViewData();

            EmployeeView ourModel = new EmployeeView();
            // GET APPROVERS INFORMATION FOR TITLE
            Employee employee = ourModel.DbGetEmployeeByEmpId(ApproverEmpID);
            if (employee != null)
            {
                thisEmp = new IpApproverViewData
                {
                    EmailAddress = employee.Email,
                    Title = employee.JobTitle,
                    Name = String.Format("{0} {1}", employee.FirstName, employee.LastName),
                    EmpID = employee.Emp_id.IntValue,
                    ApproverLevel = IpApproverViewData.ApproverLevelEnum.other
                };
                return View(thisEmp);
            }
            return RedirectToAction("Index", "Error", new { id = "Employee not found" });
        }

        [HttpPost]
        public ActionResult Approvers(String submitButton, String Comments, String OurApprovalList, String ApproverEmpID)
        {
            List<int> approvalList = GetApproveTheFollowing(OurApprovalList);
            if (approvalList != null && approvalList.Count > 0)
            {
                IpApprovalRequestView Model = new IpApprovalRequestView();
                Model.ApproveProcessing(ApproverEmpID, submitButton, approvalList, Comments);
            }
            return RedirectToAction("Approvers", new { ApproverEmpID = ApproverEmpID });
        }

        /// <summary>
        /// ourList += "Index=" + x + " RecordId=" + record.Id + ",";
        /// </summary>
        /// <param name="ourList"></param>
        internal List<int> GetApproveTheFollowing(string ourList)
        {
           
            String ourHdr = "Our Approvalls:";
            int iLength = "Index=".Length;
            int rLength = "RecordId=".Length;
            String[] elements = null;
            if (ourList == null || ourList.Length < ourHdr.Length)
            {
                return null;
            }
            String startStr = ourList.Substring(ourHdr.Length);
            elements = startStr.Split(',');
            List<int> toApprove = new List<int>();
            foreach (String item in elements)
            {
                if (item.Length >= iLength + rLength)
                {
                    string[] componets = item.Split(' ');
                    String Index = componets[0].Substring(iLength);
                    String RecordId = componets[1].Substring(rLength);
                    int toAdd = 0;
                    int.TryParse(RecordId, out toAdd);
                    if (toAdd > 0)
                    {
                        toApprove.Add(toAdd);
                    }
                }
            }
            return toApprove;
        }

    }
}
