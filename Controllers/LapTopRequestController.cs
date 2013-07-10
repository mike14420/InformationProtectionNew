using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;
using IpModelData;

namespace InformationProtection.Controllers
{
    public class LapTopRequestController : BaseController
    {
        //
        // GET: /LapTopRequest/

        public ActionResult Details(String EmpID, String LapTopDeviceId)
        {
            LapTopView ourModel = new LapTopView();
            LapTopViewData data = ourModel.GetLapTopRequest(LapTopDeviceId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // GET: /LapTopRequest/Create

        public ActionResult Create(String EmpID)
        {
            IpRequestorViewData requestor;
            IpRequestorView requestorView = new IpRequestorView();
            if (String.IsNullOrEmpty(EmpID))
            {
                String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
                requestor = requestorView.GetRequestorByLoginId(LoginUserName);
            }
            else
            {

                requestor = requestorView.GetRequestor(EmpID);
            }
            LapTopViewData data = new LapTopViewData();
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /LapTopRequest/Create

        [HttpPost]
        public ActionResult Create(String EmpID, LapTopViewData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            IpRequestorView model = new IpRequestorView();
            LapTopView lapModel = new LapTopView();
            data.BusJustType = col["RadBtnRenownOwnedType"];
            lapModel.ValidateRenownOwned(data, ModelState);
            if (submitButton == "Save")
            {
                // save without checking model validataion
                data.SaveInitialize();
                int retValue = ourModel.Create(data, EmpID, IpApprover.ApproveState.saved);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            if (ModelState.IsValid)
            {
                int retValue = ourModel.Create(data, EmpID, IpApprover.ApproveState.not_submitted);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorViewData thisEmp;

            thisEmp = model.GetRequestor(EmpID);

            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = thisEmp.FullName;
            return View(data);

        }

        //
        // GET: /LapTopRequest/Edit/5

        public ActionResult Edit(String EmpID, String LapTopDeviceId)
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
            LapTopView lapTopView = new LapTopView();
            LapTopViewData data = lapTopView.GetLapTopRequest(LapTopDeviceId);
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /LapTopRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(String EmpID, LapTopViewData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.BusJustType = col["RadBtnRenownOwnedType"];

            LapTopView lapTopView = new LapTopView();
            lapTopView.ValidateRenownOwned(data, ModelState);
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
        // GET: /LapTopRequest/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LapTopRequest/Delete/5

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
