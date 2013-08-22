using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http.ModelBinding;
using IpDataProvider;
using IpModelData;

namespace InformationProtection.Models
{
    public class RemoteAccessMdl
    {

        public RemoteAccessMdlData GetRemoteAccessRequest(string RemoteAccessId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RemoteAccessDbReqAccess RemoteAccessModel = new RemoteAccessDbReqAccess(connectionString);
            int dbKey = 0;
            int.TryParse(RemoteAccessId, out dbKey);
            RemoteAccess device = RemoteAccessModel.GetDevice(dbKey);

            RemoteAccessMdlData remoteAccessMdlData = IpApprovalRequestView.AddOtherProperties(Convert(device));
            if (remoteAccessMdlData.HasAccessRights())
            {
                return remoteAccessMdlData;
            }
            else
            {
                return null;
            }
        }
        public List<RemoteAccessMdlData> GetRemoteAccessFor(String EmpId, String Controller)
        {
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpId);
            int RequestorId = requestor.IpRequestorId;

            List<RemoteAccess> data;

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RemoteAccessDbReqAccess RemoteAccessModel = new RemoteAccessDbReqAccess(connectionString);
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);

            data = RemoteAccessModel.GetDevicesFor(RequestorId);

            List<RemoteAccessMdlData> retData = new List<RemoteAccessMdlData>();
            List<RemoteAccessMdlData> temp;
            temp = Convert(data);
            // onlly allow data viewed if has permission to view
            foreach (RemoteAccessMdlData item in temp)
            {
                IpApprovalRequestView.AddOtherProperties(item);
                if (item.HasAccessRights())
                {
                    retData.Add(item);
                }
            }

            foreach (RemoteAccessMdlData item in retData)
            {
                StringBuilder RequestDetailsLink = new StringBuilder();
                String EditLink = String.Empty;

                RequestDetailsLink.Append(String.Format("<a href=\"{0}/Details?EmpID={1}&RemoteAccessId={2}\">Details</a>",
                    Controller, requestor.EmpID, item.RemoteAccessId));

                if (item.RequestStatus == IpApprover.ApproveState.saved.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&RemoteAccessId={2}\">Edit</a>",
                        Controller, requestor.EmpID, item.RemoteAccessId);
                }
                if (item.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&RemoteAccessId={2}\">ReSubmit</a>",
                        Controller, requestor.EmpID, item.RemoteAccessId);
                }
                if (EditLink.Length > 0)
                {
                    RequestDetailsLink.Append("<br />" + EditLink);
                }
                item.RequestDetailsLink = RequestDetailsLink.ToString();
            }
            return retData;
        }

        public int Create(RemoteAccessMdlData RemoteAccessRequest)
        {
            RemoteAccess data = Convert(RemoteAccessRequest);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RemoteAccessDbReqAccess RemoteAccessModel = new RemoteAccessDbReqAccess(connectionString);
            if (RemoteAccessRequest.AccessToApp == false)
            {
                RemoteAccessRequest.AppNames = String.Empty;
            }
            if (RemoteAccessRequest.AccessToLanShares == false)
            {
                RemoteAccessRequest.LanShares = String.Empty;
            }
            if (RemoteAccessRequest.AccessToMyWorkStation == false)
            {
                RemoteAccessRequest.WorkStationOS = String.Empty;
            }
            if (RemoteAccessRequest.AccessToServer == false)
            {
                RemoteAccessRequest.LanShares = String.Empty;
            }
            int dbKey = RemoteAccessModel.Create(data);
            return dbKey;
        }

        public bool Update(RemoteAccessMdlData RemoteAccessRequest)
        {
            RemoteAccess data = Convert(RemoteAccessRequest);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            RemoteAccessDbReqAccess RemoteAccessModel = new RemoteAccessDbReqAccess(connectionString);
            if (RemoteAccessRequest.AccessToApp == false)
            {
                RemoteAccessRequest.AppNames = String.Empty;
            }
            if (RemoteAccessRequest.AccessToLanShares == false)
            {
                RemoteAccessRequest.LanShares = String.Empty;
            }
            if (RemoteAccessRequest.AccessToMyWorkStation == false)
            {
                RemoteAccessRequest.WorkStationOS = String.Empty;
            }
            if (RemoteAccessRequest.AccessToServer == false)
            {
                RemoteAccessRequest.LanShares = String.Empty;
            }
            bool result = RemoteAccessModel.Update(data);
            return result;
        }



        public static List<RemoteAccessMdlData> Convert(List<RemoteAccess> ourData)
        {
            List<RemoteAccessMdlData> retData = new List<RemoteAccessMdlData>();
            if (ourData != null)
            {

                foreach (RemoteAccess device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static List<RemoteAccess> Convert(List<RemoteAccessMdlData> ourData)
        {
            List<RemoteAccess> retData = new List<RemoteAccess>();
            if (ourData != null)
            {
                foreach (RemoteAccessMdlData device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static RemoteAccessMdlData Convert(RemoteAccess data)
        {
            RemoteAccessMdlData retData = null;
            if (data != null)
            {
                retData = new RemoteAccessMdlData
                {
                    AccessToApp = data.AccessToApp,
                    RequestorId = data.RequestorId,
                    AppNames = data.AppNames,
                    EmployeeSignature = data.EmployeeSignature,
                    LanShares = data.LanShares,
                    AccessToMyWorkStation = data.AccessToMyWorkStation,
                    AccessToServer = data.AccessToServer,
                    AccessToLanShares = data.AccessToLanShares,
                    ConnectFromHome = data.ConnectFromHome,
                    IpAddressAndHostName = data.IpAddressAndHostName,
                    RemoteConnectionType = data.RemoteConnectionType,
                    JobDuties = data.JobDuties,

                    SecuredAck1 = data.SecuredAck1,
                    SecuredAck2 = data.SecuredAck2,
                    SecuredAck3 = data.SecuredAck3,
                    SecuredAck4 = data.SecuredAck4,
                    SecuredAck5 = data.SecuredAck5,
                    RemoteAccessId = data.RemoteAccessId,
                    WorkStationOS = data.WorkStationOS,
                    ComputerName = data.ComputerName,
                    NonExemptEmployee = data.NonExemptEmployee
                };
            }
            return retData;
        }

        public static RemoteAccess Convert(RemoteAccessMdlData data)
        {
            RemoteAccess retData = null;
            if (data != null)
            {
                 retData = new RemoteAccess
                 {
                     AccessToApp = data.AccessToApp,
                     RequestorId = data.RequestorId,
                     AppNames = data.AppNames,
                     EmployeeSignature = data.EmployeeSignature,
                     LanShares = data.LanShares,
                     AccessToMyWorkStation = data.AccessToMyWorkStation,
                     AccessToServer = data.AccessToServer,
                     AccessToLanShares = data.AccessToLanShares,
                     ConnectFromHome = data.ConnectFromHome,
                     IpAddressAndHostName = data.IpAddressAndHostName,
                     RemoteConnectionType = data.RemoteConnectionType,
                     JobDuties = data.JobDuties,

                     SecuredAck1 = data.SecuredAck1,
                     SecuredAck2 = data.SecuredAck2,
                     SecuredAck3 = data.SecuredAck3,
                     SecuredAck4 = data.SecuredAck4,
                     SecuredAck5 = data.SecuredAck5,

                     RemoteAccessId = data.RemoteAccessId,
                     WorkStationOS = data.WorkStationOS,
                     ComputerName = data.ComputerName,
                     NonExemptEmployee = data.NonExemptEmployee,
                 };
            }
            return retData;
        }

        internal void ValidateConnectionType(RemoteAccessMdlData data, System.Web.Mvc.ModelStateDictionary modelState)
        {
            if (String.IsNullOrEmpty(data.RemoteConnectionType))
            {
                modelState.AddModelError("RadBtnConnectionType", "Must Select Connection Type");
            }
        }


    }
}