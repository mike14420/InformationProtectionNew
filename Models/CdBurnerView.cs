using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using IpModelData;
using System.Linq;
using IpDataProvider;
using System.Web.Configuration;
using System.Text;


namespace InformationProtection.Models
{
    public class CdBurnerView
    {
        public int Create(CdBurrnerViewData cdBurnerRequest)
        {
            CdBurnerDevice data = Convert(cdBurnerRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CdBurnerReqDbAccess CdBurnerReqDbAcess = new CdBurnerReqDbAccess(connectionString);
            int CdburnerDeviceId = CdBurnerReqDbAcess.Create(data);

            return CdburnerDeviceId;
        }


        public bool Update(CdBurrnerViewData cdBurnerRequest)
        {
            CdBurnerDevice data = Convert(cdBurnerRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CdBurnerReqDbAccess CdBurnerReqDbAcess = new CdBurnerReqDbAccess(connectionString);
            bool result = CdBurnerReqDbAcess.Update(data);

            return result;
        }

        public CdBurrnerViewData GetCdBurnerRequest(string CdburnerDeviceId)
        {
            int intId = 0;
            int.TryParse(CdburnerDeviceId, out intId);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CdBurnerReqDbAccess CdBurnerReqDbAcess = new CdBurnerReqDbAccess(connectionString);

            List<CdBurnerDevice> data = CdBurnerReqDbAcess.GetDevices();
            CdBurnerDevice cDevice = (from C in data
                           where C.CdburnerDeviceId == intId
                           select C).FirstOrDefault();  
            CdBurrnerViewData tmp = Convert(cDevice);
            IpApprovalRequestView.AddOtherProperties(tmp);
            return tmp;
        }

        public List<CdBurrnerViewData> CdDvdRequestFor(String EmpId, String Controller)
        {
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpId);
            int RequestorId = requestor.IpRequestorId;
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;          
            CdBurnerReqDbAccess CdBurnerReqDbAcess = new CdBurnerReqDbAccess(connectionString);

            List<CdBurnerDevice> data = CdBurnerReqDbAcess.GetDevices();

            List<CdBurnerDevice> RequestorsData = (from C in data
                                                   where C.RequestorId == RequestorId
                                                   select C).ToList();
            List<CdBurrnerViewData> retData;
            retData = Convert(RequestorsData);
            foreach (CdBurrnerViewData item in retData)
            {
                IpApprovalRequestView.AddOtherProperties(item);

                StringBuilder RequestDetailsLink = new StringBuilder();
                String EditLink = String.Empty;
                RequestDetailsLink.Append(String.Format("<a href=\"{0}/Details?EmpID={1}&CdburnerDeviceId={2}\">Details</a>",
                    Controller, requestor.EmpID, item.CdburnerDeviceId));

                item.RequestEditLink = String.Empty;
                if (item.RequestStatus == IpApprover.ApproveState.saved.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&CdburnerDeviceId={2}\">Edit</a>",
                        Controller, requestor.EmpID, item.CdburnerDeviceId);
                }
                if (item.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&CdburnerDeviceId={2}\">ReSubmit</a>",
                        Controller, requestor.EmpID, item.CdburnerDeviceId);
                }
                if (EditLink.Length > 0)
                {
                    RequestDetailsLink.Append("<br />" + EditLink);
                }
                item.RequestDetailsLink = RequestDetailsLink.ToString();
            }
            return retData;
        }

        public static List<CdBurrnerViewData> Convert(List<CdBurnerDevice> ourData)
        {
            List<CdBurrnerViewData> retData = new List<CdBurrnerViewData>();
            if (ourData != null)
            {

                foreach (CdBurnerDevice device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }
        public static List<CdBurnerDevice> Convert(List<CdBurrnerViewData> ourData)
        {
            List<CdBurnerDevice> retData = new List<CdBurnerDevice>();
            if (ourData != null)
            {

                foreach (CdBurrnerViewData device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }
        public static CdBurnerDevice Convert(CdBurrnerViewData data)
        {
            CdBurnerDevice retData = null;
            if (data != null)
            {
                retData = new CdBurnerDevice
                {
                    CdburnerDeviceId = data.CdburnerDeviceId,
                    RequestorId = data.RequestorId,
                    BusJustType = data.BusJustType,
                    BusJustification = data.BusJustification,
                    SecurityControls = data.SecurityControls,
                    TypeOfData = data.TypeOfData,
                    SerialNumber = data.SerialNumber,
                    Access2Computer = data.Access2Computer,
                    Access2Media = data.Access2Media,
                    ComputerLocation = data.ComputerLocation,
                    MediaStorLocation = data.MediaStorLocation,
                    IsMediaAttached = data.IsMediaAttached,

                    EmployeeSignature = data.EmployeeSignature
                };


            }
            return retData;
        }
        public static CdBurrnerViewData Convert(CdBurnerDevice data)
        {
            CdBurrnerViewData retData = null;
            if (data != null)
            {
                retData = new CdBurrnerViewData
                {
                    CdburnerDeviceId = data.CdburnerDeviceId,
                    RequestorId = data.RequestorId,
                    BusJustType = data.BusJustType,
                    BusJustification = data.BusJustification,
                    SecurityControls = data.SecurityControls,
                    TypeOfData = data.TypeOfData,
                    SerialNumber = data.SerialNumber,
                    Access2Computer = data.Access2Computer,
                    Access2Media = data.Access2Media,
                    ComputerLocation = data.ComputerLocation,
                    MediaStorLocation = data.MediaStorLocation,
                    IsMediaAttached = data.IsMediaAttached,

                    EmployeeSignature = data.EmployeeSignature
                };


            }
            return retData;
        }
        internal void ValidateRenownOwned(CdBurrnerViewData data, ModelStateDictionary modelState)
        {
            if (String.IsNullOrEmpty(data.BusJustType))
            {
                modelState.AddModelError("RadBtnRenownOwned", "Must Select CD/DVD Device Type");
            }
        }


    }
}