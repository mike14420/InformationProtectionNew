using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;
using IpModelData;

namespace InformationProtection.Controllers
{
    public class CellPhoneSyncingReqController : BaseController
    {

        //
        // GET: /CellPhoneSyncingReq/Details/5

        public ActionResult Details(String EmpID, string CellPhoneSyncDeviceId)
        {
            CellPhoneSyncMdl ourModel = new CellPhoneSyncMdl();
            CellPhoneSyncMdlData data = ourModel.GetDevice(CellPhoneSyncDeviceId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            ViewBag.ourData = data;
            ViewBag.RequestId = data.RequestId;
            return View(data);
        }

        //
        // GET: /CellPhoneSyncingReq/Create

        public ActionResult Create(String EmpID)
        {
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            if (String.IsNullOrEmpty(EmpID))
            {
                String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
                requestor = model.GetRequestorByLoginId(LoginUserName);
            }
            else
            {
                requestor = model.GetRequestor(EmpID);
            }
            CellPhoneSyncMdlData data = new CellPhoneSyncMdlData();
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /CellPhoneSyncingReq/Create

        [HttpPost]
        public ActionResult Create(String EmpID, CellPhoneSyncMdlData data, FormCollection col, String submitButton)
        {
            CellPhoneSyncMdl CellSyncModel = new CellPhoneSyncMdl();
            data.PersonOwnedType = col["RadBtnPersonOwnedType"];
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
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
                // Data saved ok
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData requestor = ipRequestorView.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);

        }

        public ActionResult Edit(String EmpID, String CellPhoneSyncDeviceId)
        {
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            CellPhoneSyncMdl cellPhoneSyncMdl = new CellPhoneSyncMdl();
            CellPhoneSyncMdlData data = cellPhoneSyncMdl.GetDevice(CellPhoneSyncDeviceId);
            if (!(data.RequestStatus == IpApprover.ApproveState.resubmit.ToString() || data.RequestStatus == IpApprover.ApproveState.saved.ToString()))
            {
                return RedirectToAction("Details", new { EmpID = EmpID, CellPhoneSyncDeviceId = CellPhoneSyncDeviceId });
            }
            IpRequestorViewData requestor;
            IpRequestorView ipRequestorView = new IpRequestorView();
            requestor = ipRequestorView.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(String EmpID, CellPhoneSyncMdlData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.PersonOwnedType = col["RadBtnPersonOwnedType"];

            CellPhoneSyncMdl cellPhoneSyncMdl = new CellPhoneSyncMdl();
            cellPhoneSyncMdl.ValidateRenownOwned(data, ModelState);
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
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }


        //public ActionResult ReSubmit(String EmpID, String CellPhoneSyncDeviceId)
        //{
        //    IpRequestorViewData requestor;
        //    IpRequestorView model = new IpRequestorView();
        //    if (String.IsNullOrEmpty(EmpID))
        //    {
        //        return RedirectToAction("Index", "UsersView", null);
        //    }
        //    CellPhoneSyncMdl cellPhoneSyncMdl = new CellPhoneSyncMdl();
        //    CellPhoneSyncMdlData data = null;
        //    data = cellPhoneSyncMdl.GetDevice(CellPhoneSyncDeviceId);
        //    if (data.RequestStatus != IpApprover.ApproveState.resubmit.ToString())
        //    {
        //        return RedirectToAction("Details", new { EmpID = EmpID, CellPhoneSyncDeviceId = CellPhoneSyncDeviceId });
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
        //public ActionResult ReSubmit(String EmpID, CellPhoneSyncMdlData data, FormCollection col, String submitButton)
        //{
        //    IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();
        //    data.PersonOwnedType = col["RadBtnPersonOwnedType"];

        //    CellPhoneSyncMdl cellPhoneSyncMdl = new CellPhoneSyncMdl();
        //    cellPhoneSyncMdl.ValidateRenownOwned(data, ModelState);
        //    if (ModelState.IsValid)
        //    {
        //        ipApprovalRequestView.ReSubmit(data);
        //        return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
        //    }
        //    IpRequestorView model = new IpRequestorView();
        //    IpRequestorViewData requestor = model.GetRequestor(EmpID);
        //    ViewData["EmpID"] = EmpID;
        //    ViewData["FullName"] = requestor.FullName;
        //    ViewBag.requestor = requestor;
        //    return View(data);
        //}

        public ActionResult Print(String EmpID, String CellPhoneSyncDeviceId)
        {
            CellPhoneSyncMdl cellPhoneSyncMdl = new CellPhoneSyncMdl();
            CellPhoneSyncMdlData data = cellPhoneSyncMdl.GetDevice(CellPhoneSyncDeviceId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }
    }
}
