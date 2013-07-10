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

                int retValue = ourModel.Create(data, EmpID, IpApprover.ApproveState.not_submitted);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorViewData thisEmp;
            IpRequestorView model = new IpRequestorView();
            thisEmp = model.GetRequestor(EmpID);

            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = thisEmp.FullName;
            return View(data);

        }

        //
        // GET: /UsbRequest/Edit/5

        public ActionResult Edit(String EmpID, String UsbDeviceId)
        {
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            requestor = model.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            UsbView wModel = new UsbView();
            int deviceId = 0;
            int.TryParse(UsbDeviceId, out deviceId);
            UsbViewData data = null;
            if (deviceId > 0)
            {
                data = wModel.GetUsbRequest(deviceId);
            }
            ViewBag.requestor = requestor;
            return View(data);
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
            IpRequestorViewData thisEmp;
            IpRequestorView model = new IpRequestorView();
            thisEmp = model.GetRequestor(EmpID);

            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = thisEmp.FullName;
            return View(data);
        }

        //
        // GET: /UsbRequest/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UsbRequest/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
