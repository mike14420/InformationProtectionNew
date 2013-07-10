using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using InformationProtection.Models;
using IpDataProvider;
using IpModelData;
using JPR;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Controllers
{
    public class UsersViewController : BaseController
    {

        public ActionResult Index(String EmpID)
        {
            String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;

            EmployeeView model = new EmployeeView();
            IpRequestorView requestorView = new IpRequestorView();

            IpRequestorViewData thisEmployee = null;
            IpRequestorViewData thisRequestor = null;
            if (String.IsNullOrEmpty(EmpID))
            {

                ///LoginUserName = "wmcdomain\\29783"; for testing
                thisEmployee = model.GetUserInfo(LoginUserName);
                if (thisEmployee == null || String.IsNullOrEmpty(thisEmployee.EmpID))
                {
                    return RedirectToAction("Create", "Requestor");
                }

                thisRequestor = requestorView.GetRequestor(thisEmployee.EmpID);
            }
            else
            {
                thisRequestor = requestorView.GetRequestor(EmpID);
            }
            if (thisRequestor == null)
            {
                return RedirectToAction("Create", "Requestor");
            }
            ViewBag.EmpId = thisRequestor.EmpID;
            ViewBag.IpRequestorId = thisRequestor.IpRequestorId;
            return View(thisRequestor);
        }

        [HttpPost]
        public ActionResult SubmitRequest(String EmpID, String OurApprovalList)
        {
            IpApprovalRequestView approvalrequestView = new IpApprovalRequestView();
            int numSubmitted = approvalrequestView.SubmitRequest(EmpID, OurApprovalList);
            return RedirectToAction("Submited", new { EmpID = EmpID, NumberSubmited = numSubmitted });
        }


        public ActionResult TabsExample()
        {

            return View();
        }
        public ActionResult Submited(String EmpID, String NumberSubmited)
        {
            EmployeeView model = new EmployeeView();
            IpRequestorViewData thisEmp;
            if (String.IsNullOrEmpty(EmpID))
            {
                String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
                thisEmp = model.GetUserInfo(LoginUserName);
            }
            else
            {
                IpRequestorView requestorView = new IpRequestorView();
                thisEmp = requestorView.GetRequestor(EmpID);
            }
            ViewBag.NumberSubmitted = NumberSubmited;
            return View(thisEmp);
        }
        
    }
}
