﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;
using IpModelData;

namespace InformationProtection.Controllers
{
    public class RemoteAccessController : BaseController
    {

        //
        // GET: /RemoteAccess/Details/5

        public ActionResult Details(String EmpID, String RemoteAccessReqId)
        {
            RemoteAccessMdl ourModel = new RemoteAccessMdl();
            RemoteAccessMdlData data = ourModel.GetRemoteAccessRequest(RemoteAccessReqId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // GET: /RemoteAccess/Create

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
            RemoteAccessMdlData data = new RemoteAccessMdlData();
            data.RequestorId = requestor.IpRequestorId;
            ViewData["EmpID"] = requestor.EmpID; ;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /RemoteAccess/Create
        [HttpPost]
        public ActionResult Create(String EmpID, RemoteAccessMdlData data, FormCollection col, String submitButton)
        {
            data.RemoteConnectionType = col["RadBtnConnectionType"];
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            RemoteAccessMdl remoteModel = new RemoteAccessMdl();
            remoteModel.ValidateConnectionType(data, ModelState);
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
            // Data not valid so redisplay allowing errors to be corrected
            IpRequestorViewData thisEmp;
            IpRequestorView model = new IpRequestorView();
            thisEmp = model.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = thisEmp.FullName;
            return View(data);
        }

        public ActionResult Edit(String EmpID, String RemoteAccessId)
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
            RemoteAccessMdl remoteAccessMdl = new RemoteAccessMdl();
            RemoteAccessMdlData data = remoteAccessMdl.GetRemoteAccessRequest(RemoteAccessId);
            if (data == null)
            {
                return RedirectToAction("Index", "UsersView" , new {EmpID=EmpID});
            }
            ViewBag.requestor = requestor;
            return View(data);
        }

        //
        // POST: /RemoteAccess/Edit/5
        [HttpPost]
        public ActionResult Edit(String EmpID, RemoteAccessMdlData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ourModel = new IpApprovalRequestView();
            data.RemoteConnectionType = col["RadBtnConnectionType"];

            RemoteAccessMdl remoteAccessMdl = new RemoteAccessMdl();
            remoteAccessMdl.ValidateConnectionType(data, ModelState);
            if (submitButton == "Save")
            {
                // save without checking model validataion
                data.SaveInitialize();
                ourModel.Update(data, IpApprover.ApproveState.saved);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            if (ModelState.IsValid)
            {
                remoteAccessMdl.Update(data);
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
        // GET: /RemoteAccess/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RemoteAccess/Delete/5

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