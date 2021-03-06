﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using IpDataProvider;
using IpModelData;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Models
{
    public class CellPhoneView
    {
        public List<IpRequestor> requestors = new List<IpRequestor>();

        public int Create(CellPhoneViewData cellPhoneRequest)
        {
            CellPhoneDevice data = Convert(cellPhoneRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneReqDbAccess CellPhoneReqDbAcess = new CellPhoneReqDbAccess(connectionString);
            int CellPhoneDeviceId = CellPhoneReqDbAcess.Create(data);

            return CellPhoneDeviceId;
        }



        public CellPhoneViewData GetDevice(string cellPhoneDevId)
        {
            int intId = 0;
            int.TryParse(cellPhoneDevId, out intId);
            CellPhoneDevice cellPhoneDevice;
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneReqDbAccess CellReqDbAcess = new CellPhoneReqDbAccess(connectionString);
            cellPhoneDevice = CellReqDbAcess.GetDevice(intId);
            CellPhoneViewData data = Convert(cellPhoneDevice);
            CellPhoneViewData cellPhoneViewData = IpApprovalRequestView.AddOtherProperties(data);
            if (cellPhoneViewData.HasAccessRights())
            {
                return cellPhoneViewData;
            }
            else
            {
                return null;
            }
        }

        public List<CellPhoneViewData> GetDevicesFor(String EmpId, String Controller)
        {
            // FIRST Get the requestor ID
            IpRequestorView Model = new IpRequestorView();
            IpRequestorViewData requestor = Model.GetRequestor(EmpId);
            int RequestorId = requestor.IpRequestorId;
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneReqDbAccess CellPhoneReqDbAcess = new CellPhoneReqDbAccess(connectionString);
            List<CellPhoneDevice> data = CellPhoneReqDbAcess.GetDevicesFor(RequestorId);
            List<CellPhoneViewData> retData = new List<CellPhoneViewData>();

            List<CellPhoneViewData> temp = new List<CellPhoneViewData>();
            foreach (CellPhoneViewData item in temp)
            {
                IpApprovalRequestView.AddOtherProperties(item);
                if (item.HasAccessRights())
                {
                    retData.Add(item);
                }
            }

            foreach (CellPhoneViewData item in retData)
            {
                StringBuilder RequestDetailsLink = new StringBuilder();
                String EditLink = String.Empty;

                RequestDetailsLink.Append(String.Format("<a href=\"{0}/Details?EmpID={1}&CellPhoneReqId={2}\">Details</a>",
                    Controller, requestor.EmpID, item.CellPhoneReqId));
                if (item.RequestStatus == IpApprover.ApproveState.saved.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&CellPhoneReqId={2}\">Edit</a>",
                        Controller, requestor.EmpID, item.CellPhoneReqId);
                }
                if (item.RequestStatus == IpApprover.ApproveState.resubmit.ToString())
                {
                    EditLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&CellPhoneReqId={2}\">ReSubmit</a>",
                        Controller, requestor.EmpID, item.CellPhoneReqId);
                }
                if (EditLink.Length > 0)
                {
                    RequestDetailsLink.Append("<br />" + EditLink);
                }
                item.RequestDetailsLink = RequestDetailsLink.ToString();
            }
            return retData;
        }

        public bool Update(CellPhoneViewData cellPhoneRequest)
        {
            CellPhoneDevice data = Convert(cellPhoneRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            CellPhoneReqDbAccess cellPhoneReqDb = new CellPhoneReqDbAccess(connectionString);
            bool result = cellPhoneReqDb.Update(data);

            return result;
        }

        public static List<CellPhoneViewData> Convert(List<CellPhoneDevice> ourData)
        {
            List<CellPhoneViewData> retData = new List<CellPhoneViewData>();
            if (ourData != null)
            {

                foreach (CellPhoneDevice device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static List<CellPhoneDevice> Convert(List<CellPhoneViewData> ourData)
        {
            List<CellPhoneDevice> retData = new List<CellPhoneDevice>();
            if (ourData != null)
            {
                foreach (CellPhoneViewData device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static CellPhoneViewData Convert(CellPhoneDevice data)
        {
            CellPhoneViewData retData = new CellPhoneViewData();
            if (data != null)
            {
                retData.CellPhoneReqId = data.CellPhoneDeviceId;

                retData.EmployeeSignature = data.EmployeeSignature;
                retData.Model = data.Model;
                retData.Make = data.Make;
                retData.BusJustification = data.BusJustification;

                retData.RenownOwnedCarrier = data.RenownOwnedCarrier;
                retData.RenownOwnedPhone = data.RenownOwnedPhone;
                retData.RenownOwnedType = data.RenownOwnedType;

                retData.SecuredAck1 = data.SecuredAck1;
                retData.SecuredAck2 = data.SecuredAck2;
                retData.SecuredAck3 = data.SecuredAck3;
                retData.SecuredAck4 = data.SecuredAck4;
                retData.SecuredAck5 = data.SecuredAck5;
                retData.SecuredAck6 = data.SecuredAck6;

                retData.IpRequestorId = data.RequestorId;
              
            }
            return retData;
        }

        public static CellPhoneDevice Convert(CellPhoneViewData data)
        {
            CellPhoneDevice retData = new CellPhoneDevice();
            if (data != null)
            {
                retData.CellPhoneDeviceId = data.CellPhoneReqId;

                retData.EmployeeSignature = data.EmployeeSignature;
                retData.Model = data.Model;
                retData.Make = data.Make;
                retData.BusJustification = data.BusJustification;

                retData.RenownOwnedCarrier = data.RenownOwnedCarrier;
                retData.RenownOwnedPhone = data.RenownOwnedPhone;
                retData.RenownOwnedType = data.RenownOwnedType;

                retData.SecuredAck1 = data.SecuredAck1;
                retData.SecuredAck2 = data.SecuredAck2;
                retData.SecuredAck3 = data.SecuredAck3;
                retData.SecuredAck4 = data.SecuredAck4;
                retData.SecuredAck5 = data.SecuredAck5;
                retData.SecuredAck6 = data.SecuredAck6;
   
                retData.RequestorId = data.IpRequestorId;

            }
            return retData;
        }

        internal void ValidateRenownOwned(CellPhoneViewData data, ModelStateDictionary modelState)
        {
            if (String.IsNullOrEmpty(data.RenownOwnedType))
            {
                modelState.AddModelError("RadBtnRenownOwned", "Must Select Renown Type");
            }
        }

    }
}