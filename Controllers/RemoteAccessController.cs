using System;
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

        public ActionResult Details(String EmpID, String RemoteAccessId)
        {
            RemoteAccessMdl ourModel = new RemoteAccessMdl();
            RemoteAccessMdlData data = ourModel.GetRemoteAccessRequest(RemoteAccessId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            ViewBag.ourData = data;
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
                ourModel.Create(data, EmpID, IpApprover.ApproveState.pending);
                return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
            }
            // Data not valid so redisplay allowing errors to be corrected
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            requestor = model.GetRequestor(EmpID);
            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            return View(data);
        }

        public ActionResult Edit(String EmpID, String RemoteAccessId)
        {
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            RemoteAccessMdl remoteAccessMdl = new RemoteAccessMdl();
            RemoteAccessMdlData data = remoteAccessMdl.GetRemoteAccessRequest(RemoteAccessId);

            if (data.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
            {
                return RedirectToAction("ReSubmit", new { EmpID = EmpID, RemoteAccessId = RemoteAccessId });
            }
            if (data.RequestStatus == IpApprover.ApproveState.saved.ToString())
            {
                requestor = model.GetRequestor(EmpID);
                ViewData["EmpID"] = EmpID;
                ViewData["FullName"] = requestor.FullName;

                ViewBag.requestor = requestor;
                return View(data);
            }
            return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
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
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            requestor = model.GetRequestor(EmpID);

            ViewData["EmpID"] = EmpID;
            ViewData["FullName"] = requestor.FullName;
            ViewBag.requestor = requestor;
            data.RequestorId = requestor.IpRequestorId;
            return View(data);
        }

        public ActionResult ReSubmit(String EmpID, String RemoteAccessId)
        {
            IpRequestorViewData requestor;
            IpRequestorView model = new IpRequestorView();
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index", "UsersView", null);
            }
            RemoteAccessMdl remoteAccessMdl = new RemoteAccessMdl();
            RemoteAccessMdlData data = remoteAccessMdl.GetRemoteAccessRequest(RemoteAccessId);
            if (data.RequestStatus != IpApprover.ApproveState.resubmit.ToString())
            {
                return RedirectToAction("Details", new { EmpID = EmpID, RemoteAccessId = RemoteAccessId });
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
        public ActionResult ReSubmit(String EmpID, RemoteAccessMdlData data, FormCollection col, String submitButton)
        {
            IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();
            data.RemoteConnectionType = col["RadBtnConnectionType"];

            RemoteAccessMdl remoteAccessMdl = new RemoteAccessMdl();
            remoteAccessMdl.ValidateConnectionType(data, ModelState);
            if (ModelState.IsValid)
            {
                ipApprovalRequestView.ReSubmit(data);
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

        public ActionResult Print(String EmpID, String RemoteAccessId)
        {
            RemoteAccessMdl remoteAccessMdl = new RemoteAccessMdl();
            RemoteAccessMdlData data = remoteAccessMdl.GetRemoteAccessRequest(RemoteAccessId);
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpID);
            ViewBag.requestor = requestor;
            return View(data);
        }

    }
}
