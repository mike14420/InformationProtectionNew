using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InformationProtection.Models;


namespace InformationProtection.Controllers
{
    [HandleError]
    public class AdminViewController : BaseController
    {
       /// <summary>
       /// Desplay the list of all Approvers
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
            if (!Membership.ValidateUser(LoginUserName, "MyPass"))
                ModelState.AddModelError("", "Incorrect username or password");
            string[] userRoles = Roles.GetRolesForUser(LoginUserName);
            if (!userRoles.Contains(IpModelData.Roles.RoleNameEnum.admin.ToString()))
            {
                return RedirectToAction("Index", "UsersView");
            }
            IpRequestorView Mdl = new IpRequestorView();
            List<IpRequestorViewData> allRequestors = Mdl.GetRequestorsIncludeRoles();
            return View(allRequestors);
        }

        public ActionResult Edit(String EmpID)
        {
            IpRequestorViewData requestor = new IpRequestorViewData();
            IpRequestorView Model = new IpRequestorView();
            List<SelectListItem> rollsList = Model.GetRolls();
            ViewBag.AllRolls = rollsList;
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index");
            }
            else
            {
                requestor = Model.GetRequestor(EmpID);
            }
            if (requestor == null)
            {
                return RedirectToAction("Index");
            }
            return View(requestor);
        }


        [HttpPost]
        public ActionResult Edit(FormCollection col, String RequestorRollButton, String EmpId, String RequestorId)
        {
            String rollName = col["AllRolls"];
            IpRequestorView requestorView = new IpRequestorView();
            requestorView.UpdateRequestorRoll(rollName, RequestorRollButton, RequestorId);

            return RedirectToAction("Index");
        }

        public ActionResult ViewPendingRequest()
        {
            String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
            if (!Membership.ValidateUser(LoginUserName, "MyPass"))
                ModelState.AddModelError("", "Incorrect username or password");
            string[] userRoles = System.Web.Security.Roles.GetRolesForUser(LoginUserName);
            if (!userRoles.Contains(IpModelData.Roles.RoleNameEnum.admin.ToString()))
            {
                return RedirectToAction("Index", "UsersView");
            }
            return View();
        }

        public ActionResult SendReminder(String Id)
        {
            ApproversEmailNotification Notifier = new ApproversEmailNotification();

            IpApprovalRequestView approvalRequest = new IpApprovalRequestView();
            IpApprovalRequestViewData request = approvalRequest.GetApprovalRequest(Id);
            IpRequestorView requestorView = new IpRequestorView();
            IpRequestorViewData requestor = requestorView.GetRequestorByRequestorId(request.IpRequestorId);
            Notifier.SubmitRequestToNextApprover(requestor.EmpID, request);
            return RedirectToAction("Index");
        }


        public ActionResult ApproversList()
        {

            return View();
        }

    }
}
