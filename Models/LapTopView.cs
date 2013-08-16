using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using IpModelData;
using System.Linq;
using System.Web.Configuration;
using IpDataProvider;
using System.Text;

namespace InformationProtection.Models
{
    public class LapTopView
    {
        public LapTopViewData GetLapTopRequest(string LapTopDeviceId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            LapTopReqDbReqAccess LapTopModel = new LapTopReqDbReqAccess(connectionString);
            int intLapTopDeviceId = 0;
            int.TryParse(LapTopDeviceId, out intLapTopDeviceId);

            LapTopDevice data = LapTopModel.GetDevice(intLapTopDeviceId);
            return IpApprovalRequestView.AddOtherProperties(LapTopView.Convert(data));
        }

        public int Create(LapTopViewData LapTopRequest, int IpRequestorId)
        {

            LapTopDevice data = Convert(LapTopRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            LapTopReqDbReqAccess LapTopModel = new LapTopReqDbReqAccess(connectionString);
            LapTopRequest.RequestorId = IpRequestorId;
            int LapTopDeviceId = LapTopModel.Create(data);
            return LapTopDeviceId;
        }

        public bool Update(LapTopViewData data)
        {

            LapTopDevice ourData = Convert(data);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            LapTopReqDbReqAccess LapTopModel = new LapTopReqDbReqAccess(connectionString);

            bool retVal = LapTopModel.Update(ourData);
            return retVal;
        }


        public List<LapTopViewData> GetLapTopRequestFor(String EmpId, String Controller)
        {
            // FIRST Get the requestor ID
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpId);
            int RequestorId = requestor.IpRequestorId;
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            LapTopReqDbReqAccess LapTopModel = new LapTopReqDbReqAccess(connectionString);

            List<LapTopDevice> devices = LapTopModel.GetDevicesFor(RequestorId);
            List<LapTopViewData> retData = Convert(devices);

            foreach (LapTopViewData item in retData)
            {
                IpApprovalRequestView.AddOtherProperties(item);
                StringBuilder RequestDetailsLink = new StringBuilder();
                String EditLink = String.Empty;

                RequestDetailsLink.Append(String.Format("<a href=\"{0}/Details?EmpID={1}&LapTopDeviceId={2}\">Details</a>",
                    Controller, requestor.EmpID, item.LapTopDeviceId));

                if (item.RequestStatus == IpApprover.ApproveState.saved.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&LapTopDeviceId={2}\">Edit</a>",
                        Controller, requestor.EmpID, item.LapTopDeviceId);
                }
                if (item.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&LapTopDeviceId={2}\">Resubmit</a>",
                        Controller, requestor.EmpID, item.LapTopDeviceId);
                }
                if (EditLink.Length > 0)
                {
                    RequestDetailsLink.Append("<br />" + EditLink);
                }
                item.RequestDetailsLink = RequestDetailsLink.ToString();
            }
            return retData;
        }

        public static List<LapTopViewData> Convert(List<LapTopDevice> ourData)
        {
            List<LapTopViewData> retData = new List<LapTopViewData>();
            if (ourData != null)
            {

                foreach (LapTopDevice device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static List<LapTopDevice> Convert(List<LapTopViewData> ourData)
        {
            List<LapTopDevice> retData = new List<LapTopDevice>();
            if (ourData != null)
            {
                foreach (LapTopViewData device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static LapTopViewData Convert(LapTopDevice data)
        {
            LapTopViewData retData = new LapTopViewData();
            if (data != null)
            {
                retData = new LapTopViewData
                {
                    LapTopDeviceId = data.LapTopDeviceId,
                    RequestorId = data.RequestorId,

                    EmployeeSignature = data.EmployeeSignature,
                    Model = data.Model,
                    SerialNumber = data.SerialNumber,
                    BusJustification = data.BusJustification,
                    BusJustType = data.BusJustType,
                    PhysLocation = data.PhysLocation,

                    SecuredAck1 = data.SecuredAck1,
                    SecuredAck2 = data.SecuredAck2,
                    SecuredAck3 = data.SecuredAck3,
                    SecuredAck4 = data.SecuredAck4,
                    SecuredAck5 = data.SecuredAck5,
                    SecuredAck6 = data.SecuredAck6,

                    SecurityAck1 = data.SecurityAck1,
                    SecurityAck2 = data.SecurityAck2,
                    SecurityAck3 = data.SecurityAck3,
                    SecurityAck4 = data.SecurityAck4,
                    SecurityAck5 = data.SecurityAck5,
                    SecurityAck6 = data.SecurityAck6,
                    SecurityAck7 = data.SecurityAck7,
                    SecurityAck8 = data.SecurityAck8,
                    SecurityAck9 = data.SecurityAck9
                };
            }
            return retData;
        }

        public static LapTopDevice Convert(LapTopViewData data)
        {
            LapTopDevice retData = new LapTopDevice();
            if (data != null)
            {
                retData = new LapTopDevice
                {
                    LapTopDeviceId = data.LapTopDeviceId,
                    RequestorId = data.RequestorId,

                    EmployeeSignature = data.EmployeeSignature,
                    Model = data.Model,
                    SerialNumber = data.SerialNumber,
                    BusJustification = data.BusJustification,
                    BusJustType = data.BusJustType,
                    PhysLocation = data.PhysLocation,

                    SecuredAck1 = data.SecuredAck1,
                    SecuredAck2 = data.SecuredAck2,
                    SecuredAck3 = data.SecuredAck3,
                    SecuredAck4 = data.SecuredAck4,
                    SecuredAck5 = data.SecuredAck5,
                    SecuredAck6 = data.SecuredAck6,

                    SecurityAck1 = data.SecurityAck1,
                    SecurityAck2 = data.SecurityAck2,
                    SecurityAck3 = data.SecurityAck3,
                    SecurityAck4 = data.SecurityAck4,
                    SecurityAck5 = data.SecurityAck5,
                    SecurityAck6 = data.SecurityAck6,
                    SecurityAck7 = data.SecurityAck7,
                    SecurityAck8 = data.SecurityAck8,
                    SecurityAck9 = data.SecurityAck9
                };
            }
            return retData;
        }


        internal void ValidateRenownOwned(LapTopViewData data, ModelStateDictionary ModelState)
        {
            if (String.IsNullOrEmpty(data.BusJustType))
            {
                ModelState.AddModelError("RadBtnRenownOwnedType", "Must Select Renown Type");
            }
        }
    }
}
