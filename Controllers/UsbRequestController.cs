using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;
using IpModelData;

namespace InformationProtection.Controllers
{
    public class UsbRequestController : Controller
    {
        //
        // GET: /UsbRequest/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /UsbRequest/Details/5

        public ActionResult Details(String EmpID, String UsbDeviceId)
        {
            UsbView ourModel = new UsbView();
            int key = 0;
            int.TryParse(UsbDeviceId, out key);
            UsbViewData data = ourModel.GetUsbRequest(key);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            ViewBag.ourData = data;
            return View(data);
        }


        public ActionResult Create(String EmpID)
        {
            IpRequestorViewData requestor;
            IpRequestorView requestorView = new IpRequestorView();
            EmployeeView model = new EmployeeView();
            /// EMPLOYEE DOES NOT EXIST IN Request6or DB yet
            if (String.IsNullOrEmpty(EmpID))
            {
                String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
                requestor = model.GetUserInfo(LoginUserName);
            }
            else
            {
                requestor = requestorView.GetRequestor(EmpID);
            }
            UsbViewData data = new UsbViewData();
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /LapTopRequest/Create

        [HttpPost]
        public ActionResult Create(String EmpID, UsbViewData data, FormCollection col, String submitButton)
        {
            UsbView UsbModel = new UsbView();
            data.RenownOwned = col["RadBtnRenownOwned"];
            UsbModel.ValidateRenownOwned(data, ModelState);
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            if (submitButton == "Save")
            {
                // save without checking model validataion
                data.SaveInitialize();
                ourModel.Create(data, EmpID, IpApprover.ApproveState.saved);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            if (ModelState.IsValid)
            {

                int retValue = ourModel.Create(data, EmpID, IpApprover.ApproveState.pending);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            requestor = model.GetRequestor(EmpID);
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);

        }

        //
        // GET: /UsbRequest/Edit/5

        public ActionResult Edit(String EmpID, String UsbDeviceId)
        {
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            UsbView usbView = new UsbView();
            UsbViewData data = null;
            int deviceId = 0;
            int.TryParse(UsbDeviceId, out deviceId);
            if (deviceId > 0)
            {
                data = usbView.GetUsbRequest(deviceId);
            }      
            if (data != null && (data.RequestStatus == IpApprover.ApproveState.saved.ToString()))
            {
                IpRequestorViewData requestor;
                IpRequestorView model = new IpRequestorView();
                requestor = model.GetRequestor(EmpID);
                ViewData["EmpID"] = EmpID;
                ViewData["FullName"] = requestor.FullName;
                ViewBag.requestor = requestor;
                return View(data);
            }
            return RedirectToAction("Index", "UsersView", null);
        }

        //
        // POST: /UsbRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(String EmpID, UsbViewData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.RenownOwned = col["RadBtnRenownOwned"];

            UsbView wModel = new UsbView();
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
                ourModel.Update(data, IpApprover.ApproveState.not_submitted);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorViewData requestor;
            IpRequestorView ipRequestorView = new IpRequestorView();
            requestor = ipRequestorView.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            return View(data);
        }

        public ActionResult ReSubmit(String EmpID, String UsbDeviceId)
        {

            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            UsbView usbView = new UsbView();
            UsbViewData data = null;
            int deviceId;
            int.TryParse(UsbDeviceId, out deviceId);
            if (deviceId > 0)
            {
                data = usbView.GetUsbRequest(deviceId);
            }  
            if (data.RequestStatus != IpApprover.ApproveState.resubmit.ToString())
            {
                return RedirectToAction("Details", new { EmpID = EmpID, UsbDeviceId = UsbDeviceId });
            }
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            requestor = model.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /CdDvdRequest/Edit/5

        [HttpPost]
        public ActionResult ReSubmit(String EmpID, UsbViewData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();
            data.RenownOwned = col["RadBtnRenownOwned"];

            UsbView usbView = new UsbView();
            usbView.ValidateRenownOwned(data, ModelState);
            if (ModelState.IsValid)
            {
                ipApprovalRequestView.ReSubmit(data);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            requestor = model.GetRequestor(EmpID);

            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        public ActionResult Print(String EmpID, String UsbDeviceId)
        {
            UsbView usbView = new UsbView();
            int deviceId = 0;
            int.TryParse(UsbDeviceId, out deviceId);
            UsbViewData data = usbView.GetUsbRequest(deviceId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }
    }
}
