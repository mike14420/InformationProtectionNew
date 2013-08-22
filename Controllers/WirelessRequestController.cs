using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;
using IpModelData;

namespace InformationProtection.Controllers
{
    public class WirelessRequestController : Controller
    {

        //
        // GET: /WirelessRequest/Details/5

        public ActionResult Details(String EmpID, String WirelessDeviceId)
        {
            WirelessMdl ourModel = new WirelessMdl();
            int id = 0;
            int.TryParse(WirelessDeviceId, out id);
            WirelessMdlData data = ourModel.GetWirelessRequest(id);
            // Only allow the data owner to view the form
            if (data == null)
            {
                return RedirectToAction("Index", "UsersView");
            }

            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            ViewBag.ourData = data;
            ViewBag.RequestId = data.RequestId;
            return View(data);
        }

        //
        // GET: /WirelessRequest/Create
        [HttpGet]
        public ActionResult Create(String EmpID)
        {
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            if (String.IsNullOrEmpty(EmpID))
            {
                String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
                requestor = model.GetRequestor(LoginUserName);
            }
            else
            {
                requestor = model.GetRequestor(EmpID);
            }
            WirelessMdlData data = new WirelessMdlData();
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /CdDvdRequest/Create
        [HttpPost]
        public ActionResult Create(String EmpID, WirelessMdlData data, FormCollection col, String submitButton)
        {

            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.RenownOwnedType = col["RadBtnRenownOwned"];

            WirelessMdl wModel = new WirelessMdl();
            wModel.ValidateRenownOwned(data, ModelState);
            if (submitButton == "Save")
            {
                // save without checking model validataion
                data.SaveInitialize();
                ourModel.Create(data, EmpID, IpApprover.ApproveState.saved);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            if (ModelState.IsValid)
            {
                ourModel.Create(data, EmpID, IpApprover.ApproveState.pending);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            // Data to be displayed
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            requestor = model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            data.RequestorId = requestor.IpRequestorId;
            return View(data);
        }

        //
        // GET: /WirelessRequest/Edit/5

        public ActionResult Edit(String EmpID, String WirelessDeviceId)
        {
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }

            WirelessMdl wirelessMdl = new WirelessMdl();
            int deviceId = 0;
            int.TryParse(WirelessDeviceId, out deviceId);
            
            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData requestor;
            requestor = ipRequestorView.GetRequestor(EmpID);
            if (deviceId <= 0)
            {
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            WirelessMdlData data = wirelessMdl.GetWirelessRequest(deviceId);
            // Only allow the data owner to view the form
            if (data == null)
            {
                return RedirectToAction("Index", "UsersView");
            }
            // only edit for resubmit or form that has been saved
            if (!(data.RequestStatus == IpApprover.ApproveState.resubmit.ToString() || data.RequestStatus == IpApprover.ApproveState.saved.ToString()))
            {
                return RedirectToAction("Details", new { EmpID = EmpID, WirelessDeviceId = WirelessDeviceId });
            }
            requestor = ipRequestorView.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /WirelessRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(String EmpID, WirelessMdlData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.RenownOwnedType = col["RadBtnRenownOwned"];

            WirelessMdl wModel = new WirelessMdl();
            wModel.ValidateRenownOwned(data, ModelState);
            if (submitButton == "Save")
            {
                // save without checking model validataion
                data.SaveInitialize();
                ourModel.Update(data, IpApprover.ApproveState.saved);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            if (ModelState.IsValid)
            {
                ourModel.Update(data, IpApprover.ApproveState.pending);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData requestor;
            requestor = ipRequestorView.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //public ActionResult ReSubmit(String EmpID, String WirelessDeviceId)
        //{

        //    if (String.IsNullOrEmpty(EmpID))
        //    {
        //        return RedirectToAction("Index", "UsersView", null);
        //    }
        //    WirelessMdl wirelessMdl = new WirelessMdl();
        //    WirelessMdlData data = null;
        //    int deviceId = 0;
        //    int.TryParse(WirelessDeviceId, out deviceId);
        //    if (deviceId <= 0)
        //    {
        //        return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
        //    }
        //    data = wirelessMdl.GetWirelessRequest(deviceId);
        //    if (data.RequestStatus != IpApprover.ApproveState.resubmit.ToString())
        //    {
        //        return RedirectToAction("Details", new { EmpID = EmpID, WirelessDeviceId = WirelessDeviceId });
        //    }
        //    IpRequestorViewData requestor;
        //    IpRequestorView model = new IpRequestorView();
        //    requestor = model.GetRequestor(EmpID);
        //    ViewData["EmpID"] = EmpID;
        //    ViewData["FullName"] = requestor.FullName;
        //    ViewBag.requestor = requestor;

        //    return View(data);
        //}

        //
        // POST: /CdDvdRequest/Edit/5

        //[HttpPost]
        //public ActionResult ReSubmit(String EmpID, WirelessMdlData data, FormCollection col, String submitButton)
        //{
        //    IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();
        //    data.RenownOwnedType = col["RadBtnRenownOwned"];

        //    WirelessMdl wirelessMdl = new WirelessMdl();
        //    wirelessMdl.ValidateRenownOwned(data, ModelState);
        //    if (ModelState.IsValid)
        //    {
        //        ipApprovalRequestView.ReSubmit(data);
        //        return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
        //    }
        //    IpRequestorViewData requestor;
        //    IpRequestorView model = new IpRequestorView();
        //    requestor = model.GetRequestor(EmpID);
        //    ViewData["EmpID"] = EmpID;
        //    ViewData["FullName"] = requestor.FullName;
        //    ViewBag.requestor = requestor;
        //    return View(data);
        //}

        public ActionResult Print(String EmpID, String WirelessDeviceId)
        {
            WirelessMdl wirelessMdl = new WirelessMdl();
            int deviceId = 0;
            int.TryParse(WirelessDeviceId, out deviceId);
            if (deviceId <= 0)
            {
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            WirelessMdlData data = wirelessMdl.GetWirelessRequest(deviceId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }
        public ActionResult ToUsersView()
        {
            return RedirectToAction("Index", "UsersView");
        }
    }
}
