using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;
using IpModelData;

namespace InformationProtection.Controllers
{
    public class CellPhoneRequestController : BaseController
    {


        public ActionResult Details(String EmpID, String CellPhoneReqId)
        {
            CellPhoneView ourModel = new CellPhoneView();
            CellPhoneViewData data = ourModel.GetDevice(CellPhoneReqId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // GET: /CellPhoneReq/Create
        [HttpGet]
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
            CellPhoneViewData data = new CellPhoneViewData();
            data.IpRequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /CellPhoneReq/Create

        [HttpPost]
        public ActionResult Create(String EmpID, CellPhoneViewData data, FormCollection col, String submitButton)
        {
            data.RenownOwnedType = col["RadBtnRenownOwned"];
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            CellPhoneView cellModel = new CellPhoneView();
            cellModel.ValidateRenownOwned(data, ModelState);
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
                return RedirectToAction("Index", "UsersView", new  { EmpID=EmpID });
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
        // GET: /CellPhoneReq/Create
        [HttpGet]
        public ActionResult Edit(String EmpID, String CellPhoneReqId)
        {
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }

            CellPhoneView cellPhoneView = new CellPhoneView();
            CellPhoneViewData data = null;
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();

            data = cellPhoneView.GetDevice(CellPhoneReqId);
            if (data.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
            {
                return RedirectToAction("ReSubmit", new  {EmpID= EmpID, CellPhoneReqId=CellPhoneReqId });
            }
            if (data.RequestStatus == IpApprover.ApproveState.saved.ToString())
            {
                requestor = model.GetRequestor(EmpID);
                ViewData["EmpID"] = EmpID;
                ViewData["FullName"] = requestor.FullName;

                ViewBag.requestor = requestor;
                return View(data);
            }
            return RedirectToAction("Index", "UsersView", null);
        }

        //
        // POST: /CellPhoneReq/Create

        [HttpPost]
        public ActionResult Edit(String EmpID, CellPhoneViewData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.RenownOwnedType = col["RadBtnRenownOwned"];

            CellPhoneView cellPhoneView = new CellPhoneView();
            cellPhoneView.ValidateRenownOwned(data, ModelState);
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
            
            IpRequestorView model = new IpRequestorView();
            IpRequestorViewData requestor = model.GetRequestor(EmpID);
            ViewData["requestor"] = requestor;
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            return View(data);
        }

        public ActionResult ReSubmit(String EmpID, String CellPhoneReqId)
        {
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            CellPhoneView cellPhoneView = new CellPhoneView();
            CellPhoneViewData data = null;
            data = cellPhoneView.GetDevice(CellPhoneReqId);
            if (data.RequestStatus != IpApprover.ApproveState.resubmit.ToString())
            {
                return RedirectToAction("Details", new { EmpID = EmpID, CellPhoneReqId = CellPhoneReqId });
            }
            requestor = model.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;

            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /CdDvdRequest/Edit/5

        [HttpPost]
        public ActionResult ReSubmit(String EmpID, CellPhoneViewData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();
            data.RenownOwnedType = col["RadBtnRenownOwned"];

            CellPhoneView cellPhoneView = new CellPhoneView();
            cellPhoneView.ValidateRenownOwned(data, ModelState);
            if (ModelState.IsValid)
            {
                ipApprovalRequestView.ReSubmit(data);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            
            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData requestor = ipRequestorView.GetRequestor(EmpID);

            ViewBag.requestor = requestor;
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            return View(data);
        }

        public ActionResult Print(String EmpID, String CellPhoneReqId)
        {
            CellPhoneView ourModel = new CellPhoneView();
            CellPhoneViewData data = ourModel.GetDevice(CellPhoneReqId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }
    }
}
