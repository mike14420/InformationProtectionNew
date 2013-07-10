using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InformationProtection.Models;

namespace InformationProtection.Controllers
{
    public class ApproversController : BaseController
    {
        //
        // GET: /Approvers/
        /// <summary>
        /// VIEW ALL APPROVERS
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
            if (!Membership.ValidateUser(LoginUserName, "MyPass"))
                ModelState.AddModelError("", "Incorrect username or password");
            string[] userRoles = Roles.GetRolesForUser(LoginUserName);
            if (!userRoles.Contains(IpModelData.Roles.RoleNameEnum.approver.ToString()))
            {
                return RedirectToAction("Index", "UsersView");
            }
            return View();
        }

        public ActionResult AddApprovers()
        {
            IpApproverView approverModel = new IpApproverView();
            List<IpApproverViewData> approvers = approverModel.GetAllApproversFromApprovers();
            return View(approvers);
        }

        public ActionResult CreateNewApprover(String EmpID)
        {
            IpRequestorView requestorsDB = new IpRequestorView();

            int requestorId = requestorsDB.CreateRequestor(EmpID, IpModelData.Roles.RoleNameEnum.approver.ToString());
            return RedirectToAction("AddApprovers");
        }
    }
}
