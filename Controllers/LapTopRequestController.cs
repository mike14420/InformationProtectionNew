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
            // only allow view of data for owner
            if (!IpApprovalRequestView.IsDataOwner(HttpContext.Request.LogonUserIdentity.Name, data.RequestorId))
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
                int retValue = ourModel.Create(data, EmpID, IpApprover.ApproveState.pending);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorViewData requestor;
            requestor = model.GetRequestor(EmpID);
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);

        }

        //
        // GET: /LapTopRequest/Edit/5

        public ActionResult Edit(String EmpID, String LapTopDeviceId)
        {

            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            LapTopView lapTopView = new LapTopView();
            LapTopViewData data = lapTopView.GetLapTopRequest(LapTopDeviceId);
            // only allow view of data for owner
            if (!IpApprovalRequestView.IsDataOwner(HttpContext.Request.LogonUserIdentity.Name, data.RequestorId))
            {
                return RedirectToAction("Index", "UsersView");
            }
            // edit is available for resubmit or saved form data
            if (!(data.RequestStatus == IpApprover.ApproveState.resubmit.ToString() || data.RequestStatus == IpApprover.ApproveState.saved.ToString()))
            {
                return RedirectToAction("Details", new { EmpID = EmpID, LapTopDeviceId = LapTopDeviceId });
            }
            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData requestor = ipRequestorView.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
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
                ourModel.Update(data, IpApprover.ApproveState.pending);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorViewData requestor;
            IpRequestorView ipRequestorView = new IpRequestorView();
            requestor = ipRequestorView.GetRequestor(EmpID);
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //public ActionResult ReSubmit(String EmpID, String LapTopDeviceId)
        //{
        //    IpRequestorViewData requestor;
        //    IpRequestorView model = new IpRequestorView();
        //    if (String.IsNullOrEmpty(EmpID))
        //    {
        //        return RedirectToAction("Index", "UsersView", null);
        //    }
        //    LapTopView lapTopView = new LapTopView();
        //    LapTopViewData data = null;
        //    data = lapTopView.GetLapTopRequest(LapTopDeviceId);
        //    if (data.RequestStatus != IpApprover.ApproveState.resubmit.ToString())
        //    {
        //        return RedirectToAction("Details", new { EmpID = EmpID, LapTopDeviceId = LapTopDeviceId });
        //    }
        //    requestor = model.GetRequestor(EmpID);
        //    ViewData["EmpID"] = EmpID;
        //    ViewData["FullName"] = requestor.FullName;

        //    ViewBag.requestor = requestor;
        //    return View(data);
        //}

        ////
        //// POST: /CdDvdRequest/Edit/5

        //[HttpPost]
        //public ActionResult ReSubmit(String EmpID, LapTopViewData data, FormCollection col, String submitButton)
        //{
        //    IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();
        //    data.BusJustType = col["RadBtnRenownOwnedType"];

        //    LapTopView lapTopView = new LapTopView();
        //    lapTopView.ValidateRenownOwned(data, ModelState);
        //    if (ModelState.IsValid)
        //    {
        //        ipApprovalRequestView.ReSubmit(data);
        //        return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
        //    }
        //    IpRequestorViewData requestor;
        //    IpRequestorView ipRequestorView = new IpRequestorView();
        //    requestor = ipRequestorView.GetRequestor(EmpID);
        //    data.RequestorId = requestor.IpRequestorId;
        //    ViewData["EmpID"] = requestor.EmpID; ;
        //    ViewData["FullName"] = requestor.FullName;
        //    ViewBag.requestor = requestor;
        //    return View(data);
        //}

        public ActionResult Print(String EmpID, String LapTopDeviceId)
        {
            LapTopView lapTopView = new LapTopView();
            LapTopViewData data = lapTopView.GetLapTopRequest(LapTopDeviceId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }
    }
}

        //public ActionResult Edit(String EmpID, String LapTopDeviceId)
        //{

        //    if (String.IsNullOrEmpty(EmpID))
        //    {
        //        return RedirectToAction("Index", "UsersView", null);
        //    }
        //    IpRequestorViewData requestor;
        //    IpRequestorView model = new IpRequestorView();

        //    LapTopView lapTopView = new LapTopView();
        //    LapTopViewData data = lapTopView.GetLapTopRequest(LapTopDeviceId);
        //    if (data.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
        //    {
        //        return RedirectToAction("ReSubmit", new { EmpID = EmpID, LapTopDeviceId = LapTopDeviceId });
        //    }
        //    if (data.RequestStatus == IpApprover.ApproveState.saved.ToString())
        //    {
        //        requestor = model.GetRequestor(EmpID);
        //        ViewData["EmpID"] = EmpID;
        //        ViewData["FullName"] = requestor.FullName;

        //        ViewBag.requestor = requestor;
        //        return View(data);
        //    }


        //    return RedirectToAction("Index", "UsersView", null);
        //}

        ////
        //// POST: /LapTopRequest/Edit/5

        //[HttpPost]
        //public ActionResult Edit(String EmpID, LapTopViewData data, FormCollection col, String submitButton)
        //{
        //    IpApprovalRequestView ourModel = new IpApprovalRequestView();
        //    data.BusJustType = col["RadBtnRenownOwnedType"];

        //    LapTopView lapTopView = new LapTopView();
        //    lapTopView.ValidateRenownOwned(data, ModelState);
        //    if (submitButton == "Save")
        //    {
        //        // save without checking model validataion
        //        data.SaveInitialize();
        //        ourModel.Update(data, IpApprover.ApproveState.saved);
        //        return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        ourModel.Update(data, IpApprover.ApproveState.pending);
        //        return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
        //    }
        //    IpRequestorViewData requestor;
        //    IpRequestorView ipRequestorView = new IpRequestorView();
        //    requestor = ipRequestorView.GetRequestor(EmpID);
        //    data.RequestorId = requestor.IpRequestorId;
        //    ViewData["EmpID"] = requestor.EmpID; ;
        //    ViewData["FullName"] = requestor.FullName;
        //    ViewBag.requestor = requestor;
        //    return View(data);
        //}