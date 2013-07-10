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
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
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
                ourModel.Create(data, EmpID, IpApprover.ApproveState.not_submitted);
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
        // GET: /WirelessRequest/Edit/5

        public ActionResult Edit(String EmpID, String WirelessDeviceId)
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


            WirelessMdl wModel = new WirelessMdl();
            int deviceId = 0;
            int.TryParse(WirelessDeviceId, out deviceId);
            WirelessMdlData data = null;
            if (deviceId > 0)
            {
                data = wModel.GetWirelessRequest(deviceId);
            }
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
        // GET: /WirelessRequest/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WirelessRequest/Delete/5

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
