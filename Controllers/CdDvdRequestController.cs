using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;
using IpModelData;

namespace InformationProtection.Controllers
{
    [HandleError]
    public class CdDvdRequestController : Controller
    {
        //
        // GET: /CdDvdRequest/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "UsersView");
        }

        //
        // GET: /CdDvdRequest/Details/5

        public ActionResult Details(String EmpID, String CdburnerDeviceId)
        {
            CdBurnerView ourModel = new CdBurnerView();
            CdBurrnerViewData data = ourModel.GetCdBurnerRequest(CdburnerDeviceId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            ViewBag.ourData = data;
            return View(data);
        }
        //
        // GET: /CdDvdRequest/Create
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
            CdBurrnerViewData data = new CdBurrnerViewData();
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /CdDvdRequest/Create
        [HttpPost]
        public ActionResult Create(String EmpID, CdBurrnerViewData data, FormCollection col, String submitButton)
        {
            data.BusJustType = col["RadBtnWriterType"];
            CdBurnerView cdModel = new CdBurnerView();
            cdModel.ValidateRenownOwned(data, ModelState);
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
                ourModel.Create(data, EmpID, IpApprover.ApproveState.pending);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            // show user the form with the error messages
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            requestor = model.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            data.RequestorId = requestor.IpRequestorId;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // GET: /CdDvdRequest/Edit/5

        public ActionResult Edit(String EmpID, String CdburnerDeviceId)
        {

            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            CdBurnerView cdBurnerView = new CdBurnerView();
            CdBurrnerViewData data = null;
            data = cdBurnerView.GetCdBurnerRequest(CdburnerDeviceId);

            if (data.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
            {
                return RedirectToAction("ReSubmit", new { EmpID=EmpID, CdburnerDeviceId = CdburnerDeviceId });
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
        // POST: /CdDvdRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(String EmpID, CdBurrnerViewData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.BusJustType = col["RadBtnWriterType"];

            CdBurnerView cdBurnerView = new CdBurnerView();
            cdBurnerView.ValidateRenownOwned(data, ModelState);
            if (submitButton == "Save")
            {
                // save without checking model validataion
                // and initialize string so they are not null
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
            IpRequestorView model = new IpRequestorView();
            requestor = model.GetRequestor(EmpID);

            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            data.RequestorId = requestor.IpRequestorId;
            return View(data);
        }

        public ActionResult ReSubmit(String EmpID, String CdburnerDeviceId)
        {

            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            CdBurnerView cdBurnerView = new CdBurnerView();
            CdBurrnerViewData data = null;
            data = cdBurnerView.GetCdBurnerRequest(CdburnerDeviceId);
            if (data.RequestStatus != IpApprover.ApproveState.resubmit.ToString())
            {
                return RedirectToAction("Details", new { EmpID = EmpID, CdburnerDeviceId = CdburnerDeviceId });
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
        public ActionResult ReSubmit(String EmpID, CdBurrnerViewData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();
            data.BusJustType = col["RadBtnWriterType"];

            CdBurnerView cdBurnerView = new CdBurnerView();
            cdBurnerView.ValidateRenownOwned(data, ModelState);
            if (ModelState.IsValid)
            {
                ipApprovalRequestView.ReSubmit(data);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }           
            IpRequestorView model = new IpRequestorView();
            IpRequestorViewData requestor = model.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        public ActionResult Print(String EmpID, String CdburnerDeviceId)
        {
            CdBurnerView ourModel = new CdBurnerView();
            CdBurrnerViewData data = ourModel.GetCdBurnerRequest(CdburnerDeviceId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }
    }
}
