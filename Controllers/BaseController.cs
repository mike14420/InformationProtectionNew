using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using InformationProtection.Models;
using IpModelData;

namespace InformationProtection.Controllers
{
    public abstract class BaseController : Controller
    {
        //[HandleError]
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    //dont interfere if the exception is already handled
        //    if (filterContext.ExceptionHandled)
        //        return;

        //    base.OnException(filterContext);
            
        //    if (filterContext == null)
        //        base.OnException(filterContext);

        //    ControllerContext Context = filterContext.Controller.ControllerContext;
        //    String message = filterContext.Exception.Message;
        //    var ex = filterContext.Exception ?? new Exception("No further information exists.");

        //    LogItException(ex, Context);
            
        //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
        //        new
        //        {
        //            action = "Index",
        //            controller = "Error",
        //            id = filterContext.Exception.Message,
        //            //exceptionAction = "Index",
        //            exceptionAction = (string)filterContext.RouteData.Values["action"],
        //            //exceptionController = "Error"
        //            exceptionController = (string)filterContext.RouteData.Values["controller"]
        //        }));
        //    filterContext.ExceptionHandled = true;
        //    Response.Redirect(String.Format("Error?id=", message));
        //}

        private void LogItException(Exception ex, ControllerContext context)
        {
            String ip = (String)Request.ServerVariables["REMOTE_ADDR"];
            String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
            String Message = ex.Message;
            String InnerMessage = (ex.InnerException != null) ? ex.InnerException.Message : String.Empty;
            String stackTrace = ex.StackTrace;
            string action = context.RouteData.Values["action"].ToString();
            string controller = context.RouteData.Values["controller"].ToString();

            var request = HttpRuntime.AppDomainAppVirtualPath;
            Error ourError = new Error
            {
                UserName = LoginUserName,
                TimeStamp = DateTime.Now,
                IpAddress = ip,
                Url = request,
                HelpLink = "",
                Source = InnerMessage,
                StackTrace = stackTrace,
                PhysLocatTargetSiteion = String.Format("{0} {1}", action, controller)
            };
            EventLog log = new EventLog();
            log.Source = "IP";
            log.WriteEntry(ex.Message);


        }

        [HttpPost]
        public JsonResult GetAppsData(string ApprsEmpID)
        {
            try
            {
                IpApprovalRequestView ourRespository = new IpApprovalRequestView();

                List<IpApprovalRequestViewData> ourList = ourRespository.GetApproversData(ApprsEmpID, "Details");
                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetApproversData()
        {
            try
            {
                IpRequestorView ourRespository = new IpRequestorView();
                List<IpRequestorViewData> ourList = ourRespository.GetAllApprovers(String.Empty, String.Empty);


                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetCellPhoneRequests(string EmpId)
        {
            try
            {
                CellPhoneView Model = new CellPhoneView();
                List<CellPhoneViewData> ourList = Model.GetDevicesFor(EmpId, "CellPhoneRequest", "Details", "Edit");

                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        
        [HttpPost]
        public JsonResult GetLapTopRequest(string EmpId)
        {
            try
            {
                LapTopView Model = new LapTopView();
                List<LapTopViewData> ourList = Model.GetLapTopRequestFor(EmpId, "LapTopRequest");

                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetCdDvdBurnerRequest(string EmpId)
        {
            try
            {
                CdBurnerView Model = new CdBurnerView();
                List<CdBurrnerViewData> ourList = Model.CdDvdRequestFor(EmpId, "CdDvdRequest", "Details", "Edit");

                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        
        [HttpPost]
        public JsonResult GetUsbRequest(string EmpId)
        {
            try
            {
                UsbView Model = new UsbView();
                List<UsbViewData> ourList = Model.GetUsbRequestFor(EmpId, "UsbRequest", "Details", "Edit");

                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetRemoteAccessRequest(string EmpId)
        {
            try
            {
                RemoteAccessMdl Model = new RemoteAccessMdl();
                List<RemoteAccessMdlData> ourList = Model.GetRemoteAccessFor(EmpId, "RemoteAccess");

                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetCellPhoneSyncRequest(string EmpId)
        {
            try
            {
                CellPhoneSyncMdl Model = new CellPhoneSyncMdl();
                List<CellPhoneSyncMdlData> ourList = Model.GetDeviceFor(EmpId, "CellPhoneSyncingReq", "Details", "Edit");

                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetWirelessRequest(string EmpId)
        {
            try
            {
                WirelessMdl Model = new WirelessMdl();
                List<WirelessMdlData> ourList = Model.GetWirelessFor(EmpId, "WirelessRequest", "Details", "Edit");

                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetRequests(string EmpId)
        {
            try
            {
                IpApprovalRequestView Model = new IpApprovalRequestView();
                List<IpApprovalRequestViewData> outData = null;

                outData = Model.GetRequestFor(EmpId, "IpApprovalRequest", "Details");

                return Json(new { Result = "OK", Records = outData });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetPendingRequests()
        {
            try
            {
                IpApprovalRequestView request = new IpApprovalRequestView();
                List<IpApprovalRequestViewData> outData = null;
                outData = request.GetRequestByState(IpApprover.ApproveState.pending.ToString(), "SendReminder");
                return Json(new { Result = "OK", Records = outData });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetAllApprovers()
        {
            try
            {
                IpRequestorView requestorModel = new IpRequestorView();
                List<IpRequestorViewData> approvers = requestorModel.GetAllApprovers("ApproversRequest", "Approvers");
                return Json(new { Result = "OK", Records = approvers });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetRequestors()
        {
            try
            {
                IpRequestorView Mdl = new IpRequestorView();
                List<IpRequestorViewData> allRequestors = Mdl.GetRequestorsIncludeRoles();
                // add the link for approvers details
                foreach (IpRequestorViewData item in allRequestors)
                {
                    item.RequestDetailsLink = String.Format("<a href=\"AdminView/Edit?EmpID={0}\">Edit</a>",
                        item.EmpID);
                }

                return Json(new { Result = "OK", Records = allRequestors });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public JsonResult GetRequestorsIncludeRoles()
        //{
        //    try
        //    {
        //        IpRequestorView Mdl = new IpRequestorView();
        //        List<IpRequestorViewData> allRequestors = Mdl.GetRequestorsIncludeRoles();
        //        // add the link for approvers details
        //        foreach (IpRequestorViewData item in allRequestors)
        //        {
        //            item.RequestDetailsLink = String.Format("<a href=\"Requestor/Edit?EmpID={0}\">Edit</a>",
        //                item.EmpID);
        //        }

        //        return Json(new { Result = "OK", Records = allRequestors });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        public JsonResult GetRequestorsIncludeRoles(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                IpRequestorView Mdl = new IpRequestorView();
                List<IpRequestorViewData> allRequestors = Mdl.GetRequestorsIncludeRoles();
                int Count = allRequestors.Count;
                // add the link for approvers details
                foreach (IpRequestorViewData item in allRequestors)
                {
                    item.RequestDetailsLink = String.Format("<a href=\"Requestor/Edit?EmpID={0}\">Edit</a>",
                        item.EmpID);
                }

                List<IpRequestorViewData> PageRequestor = allRequestors.Skip(jtStartIndex).Take(jtPageSize).ToList();

                return Json(new { Result = "OK", Records = PageRequestor, TotalRecordCount = Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
