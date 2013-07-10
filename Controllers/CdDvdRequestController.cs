﻿using System;
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
            return View();
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

            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.BusJustType = col["RadBtnWriterType"];

            CdBurnerView cdModel = new CdBurnerView();
            cdModel.ValidateRenownOwned(data, ModelState);
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


            // show user the form with the error messages
            IpRequestorViewData thisEmp;
            IpRequestorView model = new IpRequestorView();
            thisEmp = model.GetRequestor(EmpID);

            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = thisEmp.FullName;
            return View(data);
        }

        //
        // GET: /CdDvdRequest/Edit/5

        public ActionResult Edit(String EmpID, String CdburnerDeviceId)
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
            CdBurnerView cdBurnerView = new CdBurnerView();
            CdBurrnerViewData data = null;
            data = cdBurnerView.GetCdBurnerRequest(CdburnerDeviceId);
            ViewBag.requestor = requestor;
            return View(data);
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
            IpRequestorViewData thisEmp;
            IpRequestorView model = new IpRequestorView();
            thisEmp = model.GetRequestor(EmpID);

            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = thisEmp.FullName;
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
