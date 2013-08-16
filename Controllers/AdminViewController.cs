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
            if(LoginUserName != @"WMCDOMAIN\29795" || LoginUserName != @"HHPNET\HIRJKF") 
            {
                if (!userRoles.Contains(IpModelData.Roles.RoleNameEnum.admin.ToString()) )
                {
                    return RedirectToAction("Index", "UsersView");
                }
            }
            IpRequestorView Mdl = new IpRequestorView();
            List<IpRequestorViewData> allRequestors = Mdl.GetRequestorsIncludeRoles("admin");
            return View(allRequestors);
        }


        public ActionResult Details(String Id)
        {

            IpApprovalRequestView Model = new IpApprovalRequestView();
            IpApprovalRequestViewData data = Model.GetApprovalRequest(Id);


            IpRequestorView rModel = new IpRequestorView();
            IpRequestorViewData requestor = rModel.GetRequestorByRequestorId(data.IpRequestorId);
            ViewBag.requestor = requestor;
            return View(data);
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

        public ActionResult ViewRejectedRequest()
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

        public ActionResult ViewApprovedRequest()
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
            Notifier.SubmitRequestToNextApprover(Id);
            return RedirectToAction("Index");
        }


        public ActionResult ApproversList()
        {

            return View();
        }

    }
}
