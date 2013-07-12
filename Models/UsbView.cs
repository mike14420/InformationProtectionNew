using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using IpDataProvider;
using IpModelData;

namespace InformationProtection.Models
{
    public class UsbView
    {
        public UsbView()
        {

        }

        public int Create(UsbViewData UsbRequest)
        {
            UsbDevice data = Convert(UsbRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            UsbDbAccessReq UsbDbAccess = new UsbDbAccessReq(connectionString);
            int UsbDeviceId = UsbDbAccess.Create(data);
            return UsbDeviceId;
        }


        internal bool Update(UsbViewData data)
        {
            UsbDevice ourData = Convert(data);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            UsbDbAccessReq wirelessDbAccess = new UsbDbAccessReq(connectionString);
            bool result = wirelessDbAccess.Update(ourData);
            return result;
        }

        public UsbViewData GetUsbRequest(int dbKey)
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            UsbDbAccessReq UsbDbAccess = new UsbDbAccessReq(connectionString);

            UsbDevice data = UsbDbAccess.GetDevice(dbKey);

            return IpApprovalRequestView.AddOtherProperties(Convert(data));
        }

        public List<UsbViewData> GetUsbRequestFor(String EmpId, String Controller, String Action, String EditAction)
        {
            // FIRST Get the requestor ID
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpId);
            int RequestorId = requestor.IpRequestorId;

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            UsbDbAccessReq UsbDbAccess = new UsbDbAccessReq(connectionString);

            List<UsbDevice> data = UsbDbAccess.GetDevicesFor(RequestorId);

            List<UsbViewData> retData = UsbView.Convert(data); ;
            foreach (UsbViewData item in retData)
            {
                IpApprovalRequestView.AddOtherProperties(item);
                item.RequestDetailsLink = String.Format("<a href=\"{0}/{1}?EmpID={2}&UsbDeviceId={3}\">Details</a>", 
                    Controller, Action, requestor.EmpID, item.UsbDeviceId);
                item.RequestEditLink = String.Format("<a href=\"{0}/{1}?EmpID={2}&WirelessDeviceId={3}\">Edit</a>",
                    Controller, EditAction, requestor.EmpID, item.UsbDeviceId);
            }
            return retData;
        }

        public List<UsbViewData> GetUsbRequests()
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            UsbDbAccessReq UsbDbAccess = new UsbDbAccessReq(connectionString);

            List<UsbDevice> data = UsbDbAccess.GetDevices();

            List<UsbViewData> retData = UsbView.Convert(data); ;
            return retData;
        }

        public static List<UsbViewData> Convert(List<UsbDevice> ourData)
        {
            List<UsbViewData> retData = new List<UsbViewData>();
            foreach (UsbDevice device in ourData)
            {
                retData.Add(Convert(device));
            }
            return retData;
        }

        public static List<UsbDevice> Convert(List<UsbViewData> ourData)
        {
            List<UsbDevice> retData = new List<UsbDevice>();
            foreach (UsbViewData device in ourData)
            {
                retData.Add(Convert(device));
            }
            return retData;
        }

        public static UsbViewData Convert(UsbDevice data)
        {
            UsbViewData retData = null;

            if (data != null)
            {
                retData = new UsbViewData
                {
                    UsbDeviceId = data.UsbDeviceId,
                    RequestorId = data.RequestorId,

                    Model = data.Model,
                    SerialNumber = data.SerialNumber,
                    BusJustification = data.BusJustification,
                    RenownOwned = data.RenownOwned,
                    EmployeeSignature = data.EmployeeSignature,

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
        public static UsbDevice Convert(UsbViewData data)
        {
            UsbDevice retData = new UsbDevice();
            if (data != null)
            {
                retData = new UsbDevice
                {
                    UsbDeviceId = data.UsbDeviceId,
                    RequestorId = data.RequestorId,

                    Model = data.Model,
                    SerialNumber = data.SerialNumber,
                    BusJustification = data.BusJustification,
                    RenownOwned = data.RenownOwned,
                    EmployeeSignature = data.EmployeeSignature,

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

        internal List<UsbViewData> GetUsbRequest(string RequestorId)
        {
            
            List<UsbViewData> retData;

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            UsbDbAccessReq UsbDbAccess = new UsbDbAccessReq(connectionString);
            int intRequestorId = 0;
            int.TryParse(RequestorId, out intRequestorId);

            List<UsbDevice> data = UsbDbAccess.GetDevicesFor(intRequestorId);

            retData = Convert(data);
            return retData;
        }

        internal void ValidateRenownOwned(UsbViewData data, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            if (String.IsNullOrEmpty(data.RenownOwned))
            {
                ModelState.AddModelError("RadBtnRenownOwned", "Must Select Renown Type");
            }
        }

    }
}