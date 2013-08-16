using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.ModelBinding;
using IpModelData;
using IpDataProvider;
using System.Web.Configuration;
using System.Text;

namespace InformationProtection.Models
{
    public class CellPhoneSyncMdl
    {
        public int Create(CellPhoneSyncMdlData CelPhoneSyncRequest)
        {
            CellPhoneSyncDevice data = Convert(CelPhoneSyncRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneSyncReqDbAccess CellPhoneSyncDbAcess = new CellPhoneSyncReqDbAccess(connectionString);
            int CellPhoneSyncDeviceId = CellPhoneSyncDbAcess.Create(data);
            return CellPhoneSyncDeviceId;
        }

        public bool Update(CellPhoneSyncMdlData CelPhoneSyncRequest)
        {
            CellPhoneSyncDevice data = Convert(CelPhoneSyncRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneSyncReqDbAccess cellPhoneReqDb = new CellPhoneSyncReqDbAccess(connectionString);
            bool result = cellPhoneReqDb.Update(data);

            return result;
        }

        public List<CellPhoneSyncMdlData> GetDevices()
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneSyncReqDbAccess CellPhoneSyncReqDbAcess = new CellPhoneSyncReqDbAccess(connectionString);

            List<CellPhoneSyncDevice> data = CellPhoneSyncReqDbAcess.GetDevices();

            return Convert(data);
        }

        public CellPhoneSyncMdlData GetDevice(string CellPhoneSyncDeviceIdStr)
        {
            int intId = 0;
            int.TryParse(CellPhoneSyncDeviceIdStr, out intId);
            CellPhoneSyncDevice cDevice;

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneSyncReqDbAccess CellPhoneSyncReqDbAcess = new CellPhoneSyncReqDbAccess(connectionString);

            List<CellPhoneSyncDevice> data = CellPhoneSyncReqDbAcess.GetDevices();

            cDevice = (from C in data
                       where C.CellPhoneSyncDeviceId == intId
                       select C).FirstOrDefault();
            return IpApprovalRequestView.AddOtherProperties(Convert(cDevice));
        }


        public List<CellPhoneSyncMdlData> GetDeviceFor(String EmpId, String Controller)
        {
            // FIRST Get the requestor ID
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpId);
            int RequestorId = requestor.IpRequestorId;
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneSyncReqDbAccess CellPhoneSyncReqDbAcess = new CellPhoneSyncReqDbAccess(connectionString);

            List<CellPhoneSyncDevice> data = CellPhoneSyncReqDbAcess.GetDevicesFor(RequestorId);

            List<CellPhoneSyncMdlData> retData = Convert(data);

            foreach (CellPhoneSyncMdlData item in retData)
            {
                IpApprovalRequestView.AddOtherProperties(item);
                StringBuilder RequestDetailsLink = new StringBuilder();
                String EditLink = String.Empty;

                RequestDetailsLink.Append(String.Format("<a href=\"{0}/Details?EmpID={1}&CellPhoneSyncDeviceId={2}\">Details</a>", 
                    Controller, requestor.EmpID, item.CellPhoneSyncDeviceId));

                if (item.RequestStatus == IpApprover.ApproveState.saved.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&CellPhoneSyncDeviceId={2}\">Edit</a>",
                        Controller, requestor.EmpID, item.CellPhoneSyncDeviceId);
                }
                if (item.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edut?EmpID={1}&CellPhoneSyncDeviceId={2}\">ReSubmit</a>",
                        Controller, requestor.EmpID, item.CellPhoneSyncDeviceId);
                }
                if (EditLink.Length > 0)
                {
                    RequestDetailsLink.Append("<br />" + EditLink);
                }
                item.RequestDetailsLink = RequestDetailsLink.ToString();
            }
            return retData;
        }

        public static List<CellPhoneSyncMdlData> Convert(List<CellPhoneSyncDevice> ourData)
        {
            List<CellPhoneSyncMdlData> retData = new List<CellPhoneSyncMdlData>();
            if (ourData != null)
            {

                foreach (CellPhoneSyncDevice device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static List<CellPhoneSyncDevice> Convert(List<CellPhoneSyncMdlData> ourData)
        {
            List<CellPhoneSyncDevice> retData = new List<CellPhoneSyncDevice>();
            if (ourData != null)
            {
                foreach (CellPhoneSyncMdlData device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static CellPhoneSyncMdlData Convert(CellPhoneSyncDevice data)
        {
            CellPhoneSyncMdlData retData = null;
            if (data != null)
            {
                retData = new CellPhoneSyncMdlData
                {
                    CellPhoneSyncDeviceId = data.CellPhoneSyncDeviceId,
                    RequestorId = data.RequestorId,
                    EmployeeSignature = data.EmployeeSignature,
                    Model = data.Model,
                    Make = data.Make,
                    BusJustification = data.BusJustification,
                    PersonOwnedType = data.PersonOwnedType,
                    Carrier = data.Carrier,
                    SerialNumber = data.SerialNumber,
                    PhoneNumber = data.PhoneNumber,
                    MobileOS = data.MobileOS,

                    SecuredAck1 = data.SecuredAck1,
                    SecuredAck2 = data.SecuredAck2,
                    SecuredAck3 = data.SecuredAck3,
                    SecuredAck4 = data.SecuredAck4,
                    SecuredAck5 = data.SecuredAck5,
                    SecuredAck6 = data.SecuredAck6,

                    cb_sync1 = data.cb_sync1,
                    cb_sync2 = data.cb_sync2,
                    cb_sync3 = data.cb_sync3

                };
            }
            return retData;
        }

        public static CellPhoneSyncDevice Convert(CellPhoneSyncMdlData data)
        {
            CellPhoneSyncDevice retData = null;
            if (data != null)
            {
                retData = new CellPhoneSyncDevice
                {
                    CellPhoneSyncDeviceId = data.CellPhoneSyncDeviceId,
                    RequestorId = data.RequestorId,
                    EmployeeSignature = data.EmployeeSignature,
                    Model = data.Model,
                    Make = data.Make,
                    BusJustification = data.BusJustification,
                    PersonOwnedType = data.PersonOwnedType,
                    Carrier = data.Carrier,
                    SerialNumber = data.SerialNumber,
                    PhoneNumber = data.PhoneNumber,
                    MobileOS = data.MobileOS,

                    SecuredAck1 = data.SecuredAck1,
                    SecuredAck2 = data.SecuredAck2,
                    SecuredAck3 = data.SecuredAck3,
                    SecuredAck4 = data.SecuredAck4,
                    SecuredAck5 = data.SecuredAck5,
                    SecuredAck6 = data.SecuredAck6,

                    cb_sync1 = data.cb_sync1,
                    cb_sync2 = data.cb_sync2,
                    cb_sync3 = data.cb_sync3,

                };
            }
            return retData;
        }
        internal void ValidateRenownOwned(CellPhoneSyncMdlData data, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            if (String.IsNullOrEmpty(data.PersonOwnedType))
            {
                ModelState.AddModelError("RadBtnPersonOwnedType", "Must Select Personally-Owned Type");
            }
        }
    }
}