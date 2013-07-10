using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;

namespace InformationProtection.Controllers
{
    public class IpApprovalRequestController : Controller
    {

        public ActionResult Details(String EmpID, String ApprovalRequestId)
        {
            IpApprovalRequestView Model = new IpApprovalRequestView();
            IpApprovalRequestViewData data = Model.GetApprovalRequest(ApprovalRequestId);
            IpRequestorView rModel = new IpRequestorView();
            IpRequestorViewData requestor = rModel.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }

 
    }
}
