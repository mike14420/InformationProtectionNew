using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http.ModelBinding;
using IpDataProvider;
using IpModelData;

namespace InformationProtection.Models
{
    public class WirelessMdl
    {

        public int Create(WirelessMdlData WirelessRequest)
        {
            WirelessDevice data = Convert(WirelessRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            WirelessDbAccessReq wirelessDbAccess = new WirelessDbAccessReq(connectionString);
            int wirelessDeviceId = wirelessDbAccess.Create(data);
            return wirelessDeviceId;
        }

        public bool Update(WirelessMdlData WirelessRequest)
        {
            WirelessDevice data = Convert(WirelessRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            WirelessDbAccessReq wirelessDbAccess = new WirelessDbAccessReq(connectionString);
            bool result = wirelessDbAccess.Update(data);
            return result;
        }

        public WirelessMdlData GetWirelessRequest(int dbKey)
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            WirelessDbAccessReq wirelessDbAccess = new WirelessDbAccessReq(connectionString);
            WirelessDevice device = wirelessDbAccess.GetDevice(dbKey);
            return IpApprovalRequestView.AddOtherProperties(Convert(device));
        }

        public List<WirelessMdlData> GetWirelessFor(String EmpId, String Controller, String Action, String EditAction)
        {
            // FIRST Get the requestor ID
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpId);
            int RequestorId = requestor.IpRequestorId;
         
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            WirelessDbAccessReq wirelessDbAccess = new WirelessDbAccessReq(connectionString);
            List<WirelessDevice> data = wirelessDbAccess.GetDevicesFor(RequestorId);

            List<WirelessMdlData> retData = Convert(data);
            foreach (WirelessMdlData item in retData)
            {
                IpApprovalRequestView.AddOtherProperties(item);    
                item.RequestDetailsLink = String.Format("<a href=\"{0}/{1}?EmpID={2}&WirelessDeviceId={3}\">Details</a>",
                    Controller, Action, requestor.EmpID, item.WirelessDeviceId);
                item.RequestEditLink = String.Format("<a href=\"{0}/{1}?EmpID={2}&WirelessDeviceId={3}\">Edit</a>",
                    Controller, EditAction, requestor.EmpID, item.WirelessDeviceId);
            }
            return retData;
        }



        public List<WirelessMdlData> GetWireless()
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            WirelessDbAccessReq wirelessDbAccess = new WirelessDbAccessReq(connectionString);

            List<WirelessDevice> data = wirelessDbAccess.GetDevices();

            List<WirelessMdlData> retData = Convert(data); ;
            return retData;

        }
        public static List<WirelessMdlData> Convert(List<WirelessDevice> ourData)
        {
            List<WirelessMdlData> retData = new List<WirelessMdlData>();
            if (ourData != null)
            {

                foreach (WirelessDevice device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static List<WirelessDevice> Convert(List<WirelessMdlData> ourData)
        {
            List<WirelessDevice> retData = new List<WirelessDevice>();
            if (ourData != null)
            {
                foreach (WirelessMdlData device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static WirelessMdlData Convert(WirelessDevice data)
        {
            WirelessMdlData retData = null;
            if (data != null)
            {
                retData = new WirelessMdlData
                {
                    WirelessDeviceId = data.WirelessDeviceId,
                    RequestorId = data.RequestorId,

                    BusJustification = data.BusJustification,
                    RenownOwnedType = data.RenownOwnedType,
                    EmployeeSignature = data.EmployeeSignature,
                    Model = data.Model,
                    Vendor = data.Vendor,
                    PhysLocation = data.PhysLocation,
                    SerialNumber = data.SerialNumber,

                    SecurityAck1 = data.SecurityAck1,
                    SecurityAck2 = data.SecurityAck2,
                    SecurityAck3 = data.SecurityAck3,
                    SecurityAck4 = data.SecurityAck4,
                    SecurityAck5 = data.SecurityAck5,
                    SecurityAck6 = data.SecurityAck6,
                    SecurityAck7 = data.SecurityAck7,
                    SecurityAck8 = data.SecurityAck8,

                    SecuredAck1 = data.SecuredAck1,
                    SecuredAck2 = data.SecuredAck2,
                    SecuredAck3 = data.SecuredAck3,
                    SecuredAck4 = data.SecuredAck4,
                    SecuredAck5 = data.SecuredAck5,
                    SecuredAck6 = data.SecuredAck6,
                };
            }
            return retData;
        }

        public static WirelessDevice Convert(WirelessMdlData data)
        {
            WirelessDevice retData = null;
            if (data != null)
            {
                retData = new WirelessDevice
                {
                    WirelessDeviceId = data.WirelessDeviceId,
                    RequestorId = data.RequestorId,

                    BusJustification = data.BusJustification,
                    RenownOwnedType = data.RenownOwnedType,
                    EmployeeSignature = data.EmployeeSignature,
                    Model = data.Model,
                    Vendor = data.Vendor,
                    PhysLocation = data.PhysLocation,
                    SerialNumber = data.SerialNumber,


                    SecurityAck1 = data.SecurityAck1,
                    SecurityAck2 = data.SecurityAck2,
                    SecurityAck3 = data.SecurityAck3,
                    SecurityAck4 = data.SecurityAck4,
                    SecurityAck5 = data.SecurityAck5,
                    SecurityAck6 = data.SecurityAck6,
                    SecurityAck7 = data.SecurityAck7,
                    SecurityAck8 = data.SecurityAck8,

                    SecuredAck1 = data.SecuredAck1,
                    SecuredAck2 = data.SecuredAck2,
                    SecuredAck3 = data.SecuredAck3,
                    SecuredAck4 = data.SecuredAck4,
                    SecuredAck5 = data.SecuredAck5,
                    SecuredAck6 = data.SecuredAck6,
                };
            }
            return retData;
        }


        internal void ValidateRenownOwned(WirelessMdlData data, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            if (String.IsNullOrEmpty(data.RenownOwnedType))
            {
                ModelState.AddModelError("RenownOwnedType", "Must Select Renowned-Owned Type");
            }
        }


    }
}