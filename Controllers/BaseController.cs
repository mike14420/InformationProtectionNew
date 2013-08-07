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
        public JsonResult GetAppsData(string ApprsEmpID, int jtStartIndex = 0, int jtPageSize = 0, string jtsorting = null)
        {
            try
            {
                IpApprovalRequestView ourRespository = new IpApprovalRequestView();

                IEnumerable<IpApprovalRequestViewData> outData = ourRespository.GetApproversData(ApprsEmpID);

                if (string.IsNullOrEmpty(jtsorting) || jtsorting.Equals("RequestType ASC"))
                {
                    outData = outData.OrderBy(p => p.RequestType);
                }
                else if (jtsorting.Equals("RequestType DESC"))
                {
                    outData = outData.OrderByDescending(p => p.RequestType);
                }
                else if (jtsorting.Equals("RequestorsName ASC"))
                {
                    outData = outData.OrderBy(p => p.RequestorsName);
                }
                else if (jtsorting.Equals("RequestorsName DESC"))
                {
                    outData = outData.OrderByDescending(p => p.RequestorsName);
                }
                else if (jtsorting.Equals("RequuestorsEmpId ASC"))
                {
                    outData = outData.OrderBy(p => p.RequuestorsEmpId);
                }
                else if (jtsorting.Equals("RequuestorsEmpId DESC"))
                {
                    outData = outData.OrderByDescending(p => p.RequuestorsEmpId);
                }

                IEnumerable<IpApprovalRequestViewData> outData1 = outData.Skip(jtStartIndex).Take(jtPageSize);
                return Json(new { Result = "OK", Records = outData1, TotalRecordCount = outData.Count() });

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
                List<CellPhoneViewData> ourList = Model.GetDevicesFor(EmpId, "CellPhoneRequest");

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
                List<CdBurrnerViewData> ourList = Model.CdDvdRequestFor(EmpId, "CdDvdRequest");

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
                List<UsbViewData> ourList = Model.GetUsbRequestFor(EmpId, "UsbRequest");

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
                List<CellPhoneSyncMdlData> ourList = Model.GetDeviceFor(EmpId, "CellPhoneSyncingReq");

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
                List<WirelessMdlData> ourList = Model.GetWirelessFor(EmpId, "WirelessRequest");

                return Json(new { Result = "OK", Records = ourList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        //[HttpPost]
        //public JsonResult GetRequestsNotSubmitted(string EmpId)
        //{
        //    try
        //    {
        //        IpApprovalRequestView Model = new IpApprovalRequestView();
        //        List<IpApprovalRequestViewData> outData = null;

        //        outData = Model.GetRequestFor(EmpId, "IpApprovalRequest");

        //        outData = (from item in outData
        //                   where item.ApprovedStatus == IpApprover.ApproveState.not_submitted.ToString()
        //                   select item).ToList();

        //        return Json(new { Result = "OK", Records = outData });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}
        [HttpPost]
        public JsonResult GetRequestsAll(string EmpId)
        {
            try
            {
                IpApprovalRequestView Model = new IpApprovalRequestView();
                List<IpApprovalRequestViewData> outData = null;

                outData = Model.GetRequestFor(EmpId, "IpApprovalRequest");

                return Json(new { Result = "OK", Records = outData });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult GetByRequestsState(string state, int jtStartIndex = 0, int jtPageSize = 0, string jtsorting = null)
        {
            try
            {


                IpApprovalRequestView request = new IpApprovalRequestView();
                IEnumerable<IpApprovalRequestViewData> outData = null;
                outData = request.GetRequestByState(state, "SendReminder", jtsorting);
                List<IpApprovalRequestViewData> outData1 = outData.Skip(jtStartIndex).Take(jtPageSize).ToList();
                return Json(new { Result = "OK", Records = outData1, TotalRecordCount = outData.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetAllApprovers(int jtStartIndex = 0, int jtPageSize = 0, string jtsorting = null)
        {
            try
            {
                IpRequestorView requestorModel = new IpRequestorView();
                IEnumerable<IpRequestorViewData> approvers = requestorModel.GetAllApprovers("ApproversRequest", "Approvers");


                if (string.IsNullOrEmpty(jtsorting) || jtsorting.Equals("EmpID ASC"))
                {
                    approvers = approvers.OrderBy(p => p.EmpID);
                }
                else if (jtsorting.Equals("EmpID DESC"))
                {
                    approvers = approvers.OrderByDescending(p => p.FullName);
                }
                else if (jtsorting.Equals("FullName ASC"))
                {
                    approvers = approvers.OrderBy(p => p.FullName);
                }
                else if (jtsorting.Equals("FullName DESC"))
                {
                    approvers = approvers.OrderByDescending(p => p.FullName);
                }
                else if (jtsorting.Equals("Email ASC"))
                {
                    approvers = approvers.OrderBy(p => p.Email);
                }
                else if (jtsorting.Equals("Email DESC"))
                {
                    approvers = approvers.OrderByDescending(p => p.Email);
                }
                else if (jtsorting.Equals("JobTitle ASC"))
                {
                    approvers = approvers.OrderBy(p => p.JobTitle);
                }
                else if (jtsorting.Equals("JobTitle DESC"))
                {
                    approvers = approvers.OrderByDescending(p => p.JobTitle);
                }
                else if (jtsorting.Equals("DeptName ASC"))
                {
                    approvers = approvers.OrderBy(p => p.DeptName);
                }
                else if (jtsorting.Equals("DeptName DESC"))
                {
                    approvers = approvers.OrderByDescending(p => p.DeptName);
                }
                else if (jtsorting.Equals("NumberOfPendingReq ASC"))
                {
                    approvers = approvers.OrderBy(p => p.NumberOfPendingReq);
                }
                else if (jtsorting.Equals("NumberOfPendingReq DESC"))
                {
                    approvers = approvers.OrderByDescending(p => p.NumberOfPendingReq);
                }

                List<IpRequestorViewData> outData1 = approvers.Skip(jtStartIndex).Take(jtPageSize).ToList();
                return Json(new { Result = "OK", Records = approvers, TotalRecordCount = approvers.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetRequestors(int jtStartIndex = 0, int jtPageSize = 0, string jtsorting = null)
        {
            try
            {
                IpRequestorView Mdl = new IpRequestorView();
                IEnumerable<IpRequestorViewData> allRequestors = Mdl.GetRequestorsIncludeRoles("requestor");

                if (string.IsNullOrEmpty(jtsorting) || jtsorting.Equals("FullName ASC"))
                {
                    allRequestors = allRequestors.OrderBy(p => p.FullName);
                }
                else if (jtsorting.Equals("FullName DESC"))
                {
                    allRequestors = allRequestors.OrderByDescending(p => p.FullName);
                }
                else if (jtsorting.Equals("EmpID ASC"))
                {
                    allRequestors = allRequestors.OrderBy(p => p.EmpID);
                }
                else if (jtsorting.Equals("EmpID DESC"))
                {
                    allRequestors = allRequestors.OrderByDescending(p => p.EmpID);
                }
                else if (jtsorting.Equals("Email ASC"))
                {
                    allRequestors = allRequestors.OrderBy(p => p.Email);
                }
                else if (jtsorting.Equals("Email DESC"))
                {
                    allRequestors = allRequestors.OrderByDescending(p => p.Email);
                }

                List<IpRequestorViewData> outData1 = allRequestors.Skip(jtStartIndex).Take(jtPageSize).ToList();
                return Json(new { Result = "OK", Records = allRequestors, TotalRecordCount = allRequestors.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult GetRequestorsIncludeRoles(String controllerType, int jtStartIndex = 0, int jtPageSize = 0, string jtsorting = null)
        {
            try
            {
                IpRequestorView Mdl = new IpRequestorView();
                IEnumerable<IpRequestorViewData> allRequestors;
                allRequestors = Mdl.GetRequestorsIncludeRoles(controllerType);


                if (string.IsNullOrEmpty(jtsorting) || jtsorting.Equals("FullName ASC"))
                {
                    allRequestors = allRequestors.OrderBy(p => p.FullName);
                }
                else if (jtsorting.Equals("FullName DESC"))
                {
                    allRequestors = allRequestors.OrderByDescending(p => p.FullName);
                }
                else if (jtsorting.Equals("EmpID ASC"))
                {
                    allRequestors = allRequestors.OrderBy(p => p.EmpID);
                }
                else if (jtsorting.Equals("EmpID DESC"))
                {
                    allRequestors = allRequestors.OrderByDescending(p => p.EmpID);
                }
                else if (jtsorting.Equals("Email ASC"))
                {
                    allRequestors = allRequestors.OrderBy(p => p.Email);
                }
                else if (jtsorting.Equals("Email DESC"))
                {
                    allRequestors = allRequestors.OrderByDescending(p => p.Email);
                }
                else if (jtsorting.Equals("JobTitle ASC"))
                {
                    allRequestors = allRequestors.OrderBy(p => p.JobTitle);
                }
                else if (jtsorting.Equals("JobTitle DESC"))
                {
                    allRequestors = allRequestors.OrderByDescending(p => p.JobTitle);
                }
                List<IpRequestorViewData> outData = allRequestors.Skip(jtStartIndex).Take(jtPageSize).ToList();

                return Json(new { Result = "OK", Records = outData, TotalRecordCount = outData.Count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
