﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using IpDataProvider;
using IpModelData;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Models
{
    public class IpApprovalRequestView
    {
        public List<IpRequestor> requestors = new List<IpRequestor>();

        public int Create(IpApprovalRequestViewData ourRequest)
        {
            IpApprovalRequest data = Convert(ourRequest);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess ApprovalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);
            int IpApprovalRequestId = ApprovalReqReqDbAcess.Create(data);
            ourRequest.Id = IpApprovalRequestId;

            return IpApprovalRequestId;
        }

        public bool UpdateState(IpApprover.ApproveState state, int devId, IpApprovalRequest.RequestTypeEnum type)
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess ApprovalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);
            bool result = ApprovalReqReqDbAcess.InitApprovalRequest(devId, type.ToString(), state.ToString());

            return result;
        }

        IpApprovalRequestViewData CreateRequest(String EmpID, IpApprover.ApproveState state)
        {
            IpApprovalRequestViewData Request = new IpApprovalRequestViewData();
            Request.RequuestorsEmpId = EmpID;
            Request.LogonUserIdentity = HttpContext.Current.Request.LogonUserIdentity.Name;
            IpRequestorView Mdl = new IpRequestorView();
            IpRequestorViewData requestor = Mdl.GetRequestor(EmpID);
            Request.IpRequestorId = requestor.IpRequestorId;
            EmployeeView employeeView = new EmployeeView();
            Employee thisEmp = employeeView.DbGetEmployeeByEmpId(EmpID);
            Supervisor supervisor = thisEmp.Supervisor;


            Employee firstSupEmployee = employeeView.DbGetEmployeeByEmpId(supervisor.Sup_id.ToString());//me
            Employee secondSupEmployee = employeeView.DbGetEmployeeByEmpId(firstSupEmployee.Supervisor.Sup_id.ToString());

            // 1. Get the top 4 approvers ID 
            IpApproverView approversMdl = new IpApproverView();
            List<IpApproverViewData> approvers = approversMdl.GetAllApproversFromApprovers();
            var vphr = (from item in approvers
                        where item.ApproverLevel == IpApproverViewData.ApproverLevelEnum.vphr
                        select item).First();
            var ipd = (from item in approvers
                       where item.ApproverLevel == IpApproverViewData.ApproverLevelEnum.ipd
                       select item).First();
            var cio = (from item in approvers
                       where item.ApproverLevel == IpApproverViewData.ApproverLevelEnum.cio
                       select item).First();
            var rhcfo = (from item in approvers
                         where item.ApproverLevel == IpApproverViewData.ApproverLevelEnum.rhcfo
                         select item).First();

            if (firstSupEmployee != null)
            {
                Request.FirstSupApproval = state.ToString();
                Request.FirstSupApprovalDate = DateTime.Now;
                Request.FirstSupComment = String.Empty;
                Request.FirstSupEmpId = firstSupEmployee.Emp_id.IntValue;
                Request.FirstSupEmail = firstSupEmployee.Email;
                Request.FirstSupName = String.Format("{0} {1}", firstSupEmployee.FirstName, firstSupEmployee.LastName);
            }

            if (secondSupEmployee != null)
            {
                Request.SecondSupApproval = state.ToString();
                Request.SecondSupApprovalDate = DateTime.Now;
                Request.SecondSupComment = String.Empty;
                Request.SecondSupEmpId = secondSupEmployee.Emp_id.IntValue;
                Request.SecondSupEmail = secondSupEmployee.Email;
                Request.SecondSupName = String.Format("{0} {1}", secondSupEmployee.FirstName, secondSupEmployee.LastName);
            }

            Request.VpHrApproval = state.ToString();
            Request.VpHrApprovalDate = DateTime.Now;
            Request.VpHrComment = String.Empty;
            Request.VpHrApproverEmpId = vphr.EmpID;
            Request.VphrEmail = vphr.EmailAddress;
            Request.VpHrName = vphr.Name;


            Request.RhCfoApproval = state.ToString();
            Request.RhCfoApprovalDate = DateTime.Now;
            Request.RhCfoComment = String.Empty;
            Request.RhCfoApproverEmpId = rhcfo.EmpID;
            Request.RhCfoEmail = rhcfo.EmailAddress;
            Request.RhCfoName = rhcfo.Name;



            Request.IpdApproval = state.ToString();
            Request.IpdApprovalDate = DateTime.Now;
            Request.IpdComment = String.Empty;
            Request.IpdApproverEmpId = ipd.EmpID;
            Request.IpdEmail = ipd.EmailAddress;
            Request.IpdName = ipd.Name;

            Request.CioApproval = state.ToString();
            Request.CioApprovalDate = DateTime.Now;
            Request.CioComment = String.Empty;
            Request.CioEmpId = cio.EmpID;
            Request.CioEmail = cio.EmailAddress;
            Request.CioName = cio.Name;

            Request.Archive = false;

            Request.GrantDate = DateTime.Now;
            Request.SubmitDate = DateTime.Now;
            Request.ReturnDate = DateTime.Now;

            // Fill in the FK
            Request.CdburnerDeviceID = 0;
            Request.CellPhoneSyncDeviceID = 0;
            Request.UsbDeviceID = 0;
            Request.LapTopID = 0;
            Request.RemoteAccessID = 0;
            Request.WirelessDeviceID = 0;

            int firstId = Mdl.CreateRequestor(firstSupEmployee.Emp_id.ToString(), Roles.RoleNameEnum.approver.ToString());
            int secondId = Mdl.CreateRequestor(secondSupEmployee.Emp_id.ToString(), Roles.RoleNameEnum.approver.ToString());

            return Request;
        }
        //***********************************************
        // CD Burnner
        //************************************************
        public int Create(CdBurrnerViewData data, String EmpID, IpApprover.ApproveState state)
        {

            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData requestor = ipRequestorView.GetRequestor(EmpID);

            // add the forien key
            data.RequestorId = requestor.IpRequestorId;

            CdBurnerView cdBurnerView = new CdBurnerView();
            int cdBurrnerId = cdBurnerView.Create(data);

            IpApprovalRequestViewData Request = CreateRequest(EmpID, state);
            Request.CdburnerDeviceID = cdBurrnerId;
            Request.RequestType = IpApprovalRequest.RequestTypeEnum.cdburnner.ToString();
            /// Now Write to DB
            int retValue = Create(Request);
            //SubmitRequest(Request);

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(Request.Id.ToString());
            // tell user request has been submitted
            approversEmailNotification.SendNotificationRequestApproved(Request);
            return retValue;
        }
        public bool Update(CdBurrnerViewData data, IpApprover.ApproveState state)
        {
            bool retValue = false;
            CdBurnerView cdBurnerView = new CdBurnerView();
            bool result = cdBurnerView.Update(data);

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
            IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
            AddOtherProperties(ipApprovalRequestViewData);
            String oldState = ipApprovalRequestViewData.ApprovedStatus.ToString(); ;
            retValue = approvalRequestDbAccess.ChangeState(data.RequestId, oldState, IpApprover.ApproveState.pending.ToString());
            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
            // Tell user his submit was success
            approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
            return retValue;
        }

        public static CdBurrnerViewData AddOtherProperties(CdBurrnerViewData data)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest tmp = approvalRequestDbAccess.GetApprovalRequestByDeviceId(data.CdburnerDeviceId,
                IpApprovalRequest.RequestTypeEnum.cdburnner.ToString());
            IpApprovalRequestViewData request = IpApprovalRequestView.Convert(tmp);
            data.RequestStatus = request.ApprovedStatus;
            data.RequestId = request.Id;
            data.RequestorsName = request.RequestorsName;
            data.FirstSupEmpId = request.FirstSupEmpId;
            data.SecondSupEmpId = request.SecondSupEmpId;
            data.VpHrApproverEmpId = request.VpHrApproverEmpId;
            data.RhCfoApproverEmpId = request.RhCfoApproverEmpId;
            data.IpdApproverEmpId = request.IpdApproverEmpId;
            data.CioEmpId = request.CioEmpId;
            
            return data;
        }

        //***********************************************
        // CELL PHONE
        //************************************************
        public int Create(CellPhoneViewData data, String EmpID, IpApprover.ApproveState state)
        {
            IpRequestorView Mdl = new IpRequestorView();
            IpRequestorViewData requestor = Mdl.GetRequestor(EmpID);
            // add the forien key
            data.IpRequestorId = requestor.IpRequestorId;

            CellPhoneView cellPhoneView = new CellPhoneView();
            int cellPhoneDevId = cellPhoneView.Create(data);

            IpApprovalRequestViewData Request = CreateRequest(EmpID, state);
            Request.CellPhoneDeviceId = cellPhoneDevId;
            Request.RequestType = IpApprovalRequest.RequestTypeEnum.cellphone.ToString();
            /// Now Write to DB
            int retValue = Create(Request);
            //SubmitRequest(Request);

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(Request.Id.ToString());
            // tell user request has been submitted
            approversEmailNotification.SendNotificationRequestApproved(Request);
            return retValue;
        }

        public bool Update(CellPhoneViewData data, IpApprover.ApproveState state)
        {
            bool retValue = false;
            CellPhoneView cellPhoneView = new CellPhoneView();
            bool result = cellPhoneView.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            // GET A COPY OF THE REQUEST
            IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
            IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
            String oldState = ipApprovalRequestViewData.ApprovedStatus.ToString(); ;
            AddOtherProperties(ipApprovalRequestViewData);
            retValue = approvalRequestDbAccess.ChangeState(data.RequestId, oldState, IpApprover.ApproveState.pending.ToString());

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
            // Tell user his submit was success
            approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
            return retValue;
        }
        //// move state from resubmit to pending
        //public bool ReSubmit(CellPhoneViewData data)
        //{
        //    bool retValue = false;
        //    CellPhoneView cellPhoneView = new CellPhoneView();
        //    // Update the Device
        //    bool result = cellPhoneView.Update(data);

        //    /// Get Copy of Request for email notifications
        //    /// 
        //    String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
        //    ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
        //    // Update the Request
        //    retValue = approvalRequestDbAccess.ChangeState(data.RequestId, IpApprover.ApproveState.resubmit.ToString(), IpApprover.ApproveState.pending.ToString());

        //    // NOW Send notification to next approver
        //    ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
        //    approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
        //    /// TELL USER HIS REQUEST HAS BEEN Submitted
        //    IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
        //    IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
        //    AddOtherProperties(ipApprovalRequestViewData);
        //    approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
        //    return retValue;
        //}


        public static CellPhoneViewData AddOtherProperties(CellPhoneViewData data)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest tmp = approvalRequestDbAccess.GetApprovalRequestByDeviceId(data.CellPhoneReqId,
                IpApprovalRequest.RequestTypeEnum.cellphone.ToString());
            IpApprovalRequestViewData request = IpApprovalRequestView.Convert(tmp);
            data.RequestStatus = request.ApprovedStatus;
            data.RequestId = request.Id;
            data.RequestorsName = request.RequestorsName;
            data.FirstSupEmpId = request.FirstSupEmpId;
            data.SecondSupEmpId = request.SecondSupEmpId;
            data.VpHrApproverEmpId = request.VpHrApproverEmpId;
            data.RhCfoApproverEmpId = request.RhCfoApproverEmpId;
            data.IpdApproverEmpId = request.IpdApproverEmpId;
            data.CioEmpId = request.CioEmpId;
            return data;
        }
        //***********************************************
        // LAP TOP
        //************************************************
        public int Create(LapTopViewData data, String EmpID, IpApprover.ApproveState state)
        {
            LapTopView lMdl = new LapTopView();
            IpRequestorView Mdl = new IpRequestorView();
            IpRequestorViewData requestor = Mdl.GetRequestor(EmpID);
            data.RequestorId = requestor.IpRequestorId;
            int LaptopDevId = lMdl.Create(data, requestor.IpRequestorId);

            IpApprovalRequestViewData Request = CreateRequest(EmpID, state);
            Request.LapTopID = LaptopDevId;
            Request.RequestType = IpApprovalRequest.RequestTypeEnum.laptop.ToString();
            int retValue = Create(Request);

            //SubmitRequest(Request);

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(Request.Id.ToString());
            // tell user request has been submitted
            approversEmailNotification.SendNotificationRequestApproved(Request);
            return retValue;
        }

        public bool Update(LapTopViewData data, IpApprover.ApproveState state)
        {
            bool retValue = false;
            LapTopView lapTopView = new LapTopView();
            bool result = lapTopView.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            //retValue = approvalRequestDbAccess.InitApprovalRequest(data.LapTopDeviceId, IpApprovalRequest.RequestTypeEnum.laptop.ToString(), state.ToString());
              // GET A COPY OF THE REQUEST
            IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
            IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
            String oldState = ipApprovalRequestViewData.ApprovedStatus.ToString(); ;
            AddOtherProperties(ipApprovalRequestViewData);
            retValue = approvalRequestDbAccess.ChangeState(data.RequestId, oldState, IpApprover.ApproveState.pending.ToString());
     
            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
            // Tell user his submit was success
            approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
            return retValue;
        }
        public static LapTopViewData AddOtherProperties(LapTopViewData data)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest tmp = approvalRequestDbAccess.GetApprovalRequestByDeviceId(data.LapTopDeviceId,
                IpApprovalRequest.RequestTypeEnum.laptop.ToString());
            IpApprovalRequestViewData request = IpApprovalRequestView.Convert(tmp);
            data.RequestStatus = request.ApprovedStatus;
            data.RequestId = request.Id;
            data.RequestorsName = request.RequestorsName;
            data.FirstSupEmpId = request.FirstSupEmpId;
            data.SecondSupEmpId = request.SecondSupEmpId;
            data.VpHrApproverEmpId = request.VpHrApproverEmpId;
            data.RhCfoApproverEmpId = request.RhCfoApproverEmpId;
            data.IpdApproverEmpId = request.IpdApproverEmpId;
            data.CioEmpId = request.CioEmpId;
            return data;
        }
        //***********************************************
        // USB DEVICE
        //************************************************
        public int Create(UsbViewData data, String EmpID, IpApprover.ApproveState state)
        {
            UsbView UsbMdl = new UsbView();
            IpRequestorView Mdl = new IpRequestorView();
            IpRequestorViewData requestor = Mdl.GetRequestor(EmpID);
            data.RequestorId = requestor.IpRequestorId;
            int UsbDevId = UsbMdl.Create(data);

            IpApprovalRequestViewData Request = CreateRequest(EmpID, state);
            Request.UsbDeviceID = UsbDevId;
            Request.RequestType = IpApprovalRequest.RequestTypeEnum.usb.ToString();
            int retValue = Create(Request);

            //SubmitRequest(Request);


            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(Request.Id.ToString());
            // tell user request has been submitted
            approversEmailNotification.SendNotificationRequestApproved(Request);
            return retValue;

        }

        public bool Update(UsbViewData data, IpApprover.ApproveState state)
        {
            bool retValue = false;
            UsbView usbView = new UsbView();
            bool result = usbView.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            // GET A COPY OF THE REQUEST
            IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
            IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
            String oldState = ipApprovalRequestViewData.ApprovedStatus.ToString(); ;
            AddOtherProperties(ipApprovalRequestViewData);
            retValue = approvalRequestDbAccess.ChangeState(data.RequestId, oldState, IpApprover.ApproveState.pending.ToString());

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
            // Tell user his submit was success
            approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
            return retValue;
        }

        // move state from resubmit to pending
        //public bool ReSubmit(UsbViewData data)
        //{
        //    bool retValue = false;
        //    UsbView usbView = new UsbView();
        //    // Update the Device
        //    bool result = usbView.Update(data);

        //    /// Get Copy of Request for email notifications
        //    /// 

        //    String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
        //    ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
        //    // Update the Request
        //    retValue = approvalRequestDbAccess.ChangeState(data.RequestId, IpApprover.ApproveState.resubmit.ToString(), IpApprover.ApproveState.pending.ToString());

        //    IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
        //    IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
        //    AddOtherProperties(ipApprovalRequestViewData);
        //    // NOW Send notification to next approver
        //    ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
        //    approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
        //    // Tell user his submit was success
        //    approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
        //    return retValue;
        //}
        public static UsbViewData AddOtherProperties(UsbViewData data)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest tmp = approvalRequestDbAccess.GetApprovalRequestByDeviceId(data.UsbDeviceId,
                IpApprovalRequest.RequestTypeEnum.usb.ToString());
            IpApprovalRequestViewData request = IpApprovalRequestView.Convert(tmp);
            data.RequestStatus = request.ApprovedStatus;
            data.RequestId = request.Id;
            data.RequestorsName = request.RequestorsName;
            data.FirstSupEmpId = request.FirstSupEmpId;
            data.SecondSupEmpId = request.SecondSupEmpId;
            data.VpHrApproverEmpId = request.VpHrApproverEmpId;
            data.RhCfoApproverEmpId = request.RhCfoApproverEmpId;
            data.IpdApproverEmpId = request.IpdApproverEmpId;
            data.CioEmpId = request.CioEmpId;
            return data;
        }
        //***********************************************
        // CELL PHONE SYNC DEVICE
        //************************************************
        public int Create(CellPhoneSyncMdlData data, String EmpID, IpApprover.ApproveState state)
        {
            CellPhoneSyncMdl CellSyncMdl = new CellPhoneSyncMdl();
            IpRequestorView Mdl = new IpRequestorView();
            IpRequestorViewData requestor = Mdl.GetRequestor(EmpID);
            data.RequestorId = requestor.IpRequestorId;
            int CellSyncDevId = CellSyncMdl.Create(data);

            IpApprovalRequestViewData Request = CreateRequest(EmpID, state);
            Request.CellPhoneSyncDeviceID = CellSyncDevId;
            Request.RequestType = IpApprovalRequest.RequestTypeEnum.cellphonesync.ToString();
            int retValue = Create(Request);
            //SubmitRequest(Request);

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(Request.Id.ToString());
            // tell user request has been submitted
            approversEmailNotification.SendNotificationRequestApproved(Request);
            return retValue;
        }
        public bool Update(CellPhoneSyncMdlData data, IpApprover.ApproveState state)
        {
            bool retValue = false;
            CellPhoneSyncMdl cellPhoneSyncMdl = new CellPhoneSyncMdl();
            bool result = cellPhoneSyncMdl.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            // GET A COPY OF THE REQUEST
            IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
            IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
            String oldState = ipApprovalRequestViewData.ApprovedStatus.ToString(); ;
            AddOtherProperties(ipApprovalRequestViewData);
            retValue = approvalRequestDbAccess.ChangeState(data.RequestId, oldState, IpApprover.ApproveState.pending.ToString());

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
            // Tell user his submit was success
            approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
            return retValue;
        }
        public static CellPhoneSyncMdlData AddOtherProperties(CellPhoneSyncMdlData data)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest tmp = approvalRequestDbAccess.GetApprovalRequestByDeviceId(data.CellPhoneSyncDeviceId,
                IpApprovalRequest.RequestTypeEnum.cellphonesync.ToString());
            IpApprovalRequestViewData request = IpApprovalRequestView.Convert(tmp);
            data.RequestStatus = request.ApprovedStatus;
            data.RequestId = request.Id;
            data.RequestorsName = request.RequestorsName;
            data.FirstSupEmpId = request.FirstSupEmpId;
            data.SecondSupEmpId = request.SecondSupEmpId;
            data.VpHrApproverEmpId = request.VpHrApproverEmpId;
            data.RhCfoApproverEmpId = request.RhCfoApproverEmpId;
            data.IpdApproverEmpId = request.IpdApproverEmpId;
            data.CioEmpId = request.CioEmpId;
            return data;
        }
        //***********************************************
        // WIRELESS DEVICE
        //************************************************
        public int Create(WirelessMdlData data, String EmpID, IpApprover.ApproveState state)
        {
            WirelessMdl WirelessMdl = new WirelessMdl();
            IpRequestorView Mdl = new IpRequestorView();
            IpRequestorViewData requestor = Mdl.GetRequestor(EmpID);
            data.RequestorId = requestor.IpRequestorId;
            int WirelessDevId = WirelessMdl.Create(data);

            IpApprovalRequestViewData Request = CreateRequest(EmpID, state);
            Request.WirelessDeviceID = WirelessDevId;
            Request.RequestType = IpApprovalRequest.RequestTypeEnum.wireless.ToString();
            int retValue = Create(Request);

            //SubmitRequest(Request);

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(Request.Id.ToString());
            // tell user request has been submitted
            approversEmailNotification.SendNotificationRequestApproved(Request);
            return retValue;
        }
        public bool Update(WirelessMdlData data, IpApprover.ApproveState state)
        {
            bool retValue = false;
            WirelessMdl wirelessMdl = new WirelessMdl();
            bool result = wirelessMdl.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            // GET A COPY OF THE REQUEST
            IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
            IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
            String oldState = ipApprovalRequestViewData.ApprovedStatus.ToString(); ;
            AddOtherProperties(ipApprovalRequestViewData);
            retValue = approvalRequestDbAccess.ChangeState(data.RequestId, oldState, IpApprover.ApproveState.pending.ToString());

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
            // Tell user his submit was success
            approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
            return retValue;
        }
        // move state from resubmit to pending
        //public bool ReSubmit(WirelessMdlData data)
        //{
        //    bool retValue = false;
        //    WirelessMdl wirelessMdl = new WirelessMdl();
        //    // Update the Device
        //    bool result = wirelessMdl.Update(data);
        //    String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
        //    ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
        //    // Update the Request
        //    retValue = approvalRequestDbAccess.ChangeState(data.RequestId, IpApprover.ApproveState.resubmit.ToString(), IpApprover.ApproveState.pending.ToString());

        //    // NOW Send notification to next approver
        //    ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
        //    approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
        //    /// TELL USER HIS REQUEST HAS BEEN Submitted
        //    IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
        //    IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
        //    AddOtherProperties(ipApprovalRequestViewData);
        //    approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
        //    return retValue;
        //}
        public static WirelessMdlData AddOtherProperties(WirelessMdlData data)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest tmp = approvalRequestDbAccess.GetApprovalRequestByDeviceId(data.WirelessDeviceId,
                IpApprovalRequest.RequestTypeEnum.wireless.ToString());
            IpApprovalRequestViewData request = IpApprovalRequestView.Convert(tmp);
            data.RequestStatus = request.ApprovedStatus;
            data.RequestId = request.Id;
            data.RequestorsName = request.RequestorsName;
            data.FirstSupEmpId = request.FirstSupEmpId;
            data.SecondSupEmpId = request.SecondSupEmpId;
            data.VpHrApproverEmpId = request.VpHrApproverEmpId;
            data.RhCfoApproverEmpId = request.RhCfoApproverEmpId;
            data.IpdApproverEmpId = request.IpdApproverEmpId;
            data.CioEmpId = request.CioEmpId;
            return data;
        }


        //***********************************************
        // REMOTE DEVICE
        //************************************************
        internal int Create(RemoteAccessMdlData data, string EmpID, IpApprover.ApproveState state)
        {
            RemoteAccessMdl RemoteAccessMdl = new RemoteAccessMdl();
            IpRequestorView Mdl = new IpRequestorView();
            IpRequestorViewData requestor = Mdl.GetRequestor(EmpID);
            data.RequestorId = requestor.IpRequestorId;
            int RemoteAccessId = RemoteAccessMdl.Create(data);

            IpApprovalRequestViewData Request = CreateRequest(EmpID, state);
            Request.RemoteAccessID = RemoteAccessId;
            Request.RequestType = IpApprovalRequest.RequestTypeEnum.remoteaccess.ToString();
            int retValue = Create(Request);
            // Now Submit it
            //SubmitRequest(Request);

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(Request.Id.ToString());
            // tell user request has been submitted
            approversEmailNotification.SendNotificationRequestApproved(Request);
            return retValue;
        }

        public bool Update(RemoteAccessMdlData data, IpApprover.ApproveState state)
        {
            bool retValue = false;
            RemoteAccessMdl remoteAccessMdl = new RemoteAccessMdl();
            bool result = remoteAccessMdl.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            // GET A COPY OF THE REQUEST
            IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(data.RequestId.ToString());
            IpApprovalRequestViewData ipApprovalRequestViewData = Convert(request);
            String oldState = ipApprovalRequestViewData.ApprovedStatus.ToString(); ;
            AddOtherProperties(ipApprovalRequestViewData);
            retValue = approvalRequestDbAccess.ChangeState(data.RequestId, oldState, IpApprover.ApproveState.pending.ToString());

            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            approversEmailNotification.SubmitRequestToNextApprover(data.RequestId.ToString());
            // Tell user his submit was success
            approversEmailNotification.SendNotificationRequestApproved(ipApprovalRequestViewData);
            return retValue;
        }
        public static RemoteAccessMdlData AddOtherProperties(RemoteAccessMdlData data)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest tmp = approvalRequestDbAccess.GetApprovalRequestByDeviceId(data.RemoteAccessId,
                IpApprovalRequest.RequestTypeEnum.remoteaccess.ToString());
            IpApprovalRequestViewData request = IpApprovalRequestView.Convert(tmp);
            data.RequestStatus = request.ApprovedStatus;
            data.RequestId = request.Id;
            data.RequestorsName = request.RequestorsName;
            data.FirstSupEmpId = request.FirstSupEmpId;
            data.SecondSupEmpId = request.SecondSupEmpId;
            data.VpHrApproverEmpId = request.VpHrApproverEmpId;
            data.RhCfoApproverEmpId = request.RhCfoApproverEmpId;
            data.IpdApproverEmpId = request.IpdApproverEmpId;
            data.CioEmpId = request.CioEmpId;
            return data;
        }
        /// <summary>
        ///  FIND ALL REQUEST for approver with EMPID
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public List<IpApprovalRequestViewData> GetApproversDataPending(String ApproverEmpID)
        {
            int intApproverEmpID = 0;
            int.TryParse(ApproverEmpID, out intApproverEmpID);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess ApprovalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);
            List<IpApprovalRequest> data = ApprovalReqReqDbAcess.GetApproversData(ApproverEmpID);
            List<IpApprovalRequestViewData> approversData = Convert(data);
            List<IpApprovalRequestViewData> PendingList = new List<IpApprovalRequestViewData>();
            foreach (IpApprovalRequestViewData item in approversData)
            {
                if (item.IsPending(intApproverEmpID))
                {
                    AddOtherProperties(item);
                    PendingList.Add(item);
                }
            }
            foreach (IpApprovalRequestViewData item in PendingList)
            {
                item.RequestDetailsLink = String.Format("<a href=\"Details?Id={0}&ApproverEmpID={1}\">Details</a>",
                    item.Id, ApproverEmpID);

            }
            return PendingList.OrderBy(P => P.RequuestorsEmpId).ToList();
        }

        /// <summary>
        ///  FIND ALL REQUEST for approver with EMPID
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public List<IpApprovalRequestViewData> GetApproversDataAll(String ApproverEmpID)
        {
            int intApproverEmpID = 0;
            int.TryParse(ApproverEmpID, out intApproverEmpID);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess ApprovalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);
            List<IpApprovalRequest> data = ApprovalReqReqDbAcess.GetApproversData(ApproverEmpID);
            List<IpApprovalRequestViewData> approversData = Convert(data);

            foreach (IpApprovalRequestViewData item in approversData)
            {
                AddOtherProperties(item);
                item.RequestDetailsLink = String.Format("<a href=\"Details?Id={0}&ApproverEmpID={1}\">Details</a>",
                    item.Id, ApproverEmpID);

            }
            return approversData.OrderBy(P => P.RequuestorsEmpId).ToList();
        }

        /// <summary>
        /// STATE=IpApprover.ApproveState.not_submitted.ToString();
        /// </summary>
        /// <returns></returns>
        public List<IpApprovalRequestViewData> GetRequestByState(String state, String action, string jtsorting = null)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess ApprovalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);

            List<IpApprovalRequest> data = ApprovalReqReqDbAcess.GetRequestByState(state);
            IEnumerable<IpApprovalRequestViewData> request = Convert(data);

            IpRequestorView RequestorsView = new IpRequestorView();
            foreach (IpApprovalRequestViewData item in request)
            {
                IpRequestorViewData ourRequestor = RequestorsView.GetRequestorByRequestorId(item.IpRequestorId);
                if (ourRequestor != null)
                {
                    item.RequestorsName = String.Format("{0} {1}", ourRequestor.Fname, ourRequestor.Lname);
                }
                AddOtherProperties(item);
                String ReminderLnk = String.Format("<a href=\"{0}?Id={1}\">Reminder</a><br/><a href=\"Details?Id={1}\">Details</a>",
                        action, item.Id);
                String DetailsLnk = String.Format("<a href=\"Details?Id={0}\">Details</a>",
                    item.Id);

                item.RemindersLink = ReminderLnk;
                item.RequestDetailsLink = DetailsLnk;
                
            }

            if (string.IsNullOrEmpty(jtsorting) || jtsorting.Equals("SubmitDate ASC"))
            {
                request = request.OrderBy(p => p.SubmitDate);
            }
            else if (jtsorting.Equals("SubmitDate DESC"))
            {
                request = request.OrderByDescending(p => p.SubmitDate);
            }
            else if (jtsorting.Equals("RequestType ASC"))
            {
                request = request.OrderBy(p => p.RequestType);
            }
            else if (jtsorting.Equals("RequestType DESC"))
            {
                request = request.OrderByDescending(p => p.RequestType);
            }
            else if (jtsorting.Equals("RequestorsName ASC"))
            {
                request = request.OrderBy(p => p.RequestorsName);
            }
            else if (jtsorting.Equals("RequestorsName DESC"))
            {
                request = request.OrderByDescending(p => p.RequestorsName);
            }
            else if (jtsorting.Equals("FirstSupName ASC"))
            {
                request = request.OrderBy(p => p.FirstSupName);
            }
            else if (jtsorting.Equals("FirstSupName DESC"))
            {
                request = request.OrderByDescending(p => p.FirstSupName);
            }
            else if (jtsorting.Equals("SecondSupName ASC"))
            {
                request = request.OrderBy(p => p.SecondSupName);
            }
            else if (jtsorting.Equals("SecondSupName DESC"))
            {
                request = request.OrderByDescending(p => p.SecondSupName);
            }
            else if (jtsorting.Equals("VpHrName ASC"))
            {
                request = request.OrderBy(p => p.VpHrName);
            }
            else if (jtsorting.Equals("VpHrName DESC"))
            {
                request = request.OrderByDescending(p => p.VpHrName);
            }
            else if (jtsorting.Equals("RhCfoName ASC"))
            {
                request = request.OrderBy(p => p.RhCfoName);
            }
            else if (jtsorting.Equals("RhCfoName DESC"))
            {
                request = request.OrderByDescending(p => p.RhCfoName);
            }
            else if (jtsorting.Equals("IpdName ASC"))
            {
                request = request.OrderBy(p => p.IpdName);
            }
            else if (jtsorting.Equals("IpdName DESC"))
            {
                request = request.OrderByDescending(p => p.IpdName);
            }
            else if (jtsorting.Equals("CioName ASC"))
            {
                request = request.OrderBy(p => p.CioName);
            }
            else if (jtsorting.Equals("CioName DESC"))
            {
                request = request.OrderByDescending(p => p.CioName);
            }
            else if (jtsorting.Equals("CioApprovalDate ASC"))
            {
                request = request.OrderBy(p => p.CioApprovalDate);
            }
            else if (jtsorting.Equals("CioApprovalDate DESC"))
            {
                request = request.OrderByDescending(p => p.CioApprovalDate);
            }
            
            return request.ToList();
        }
        
        public List<IpApprovalRequestViewData> GetRequest(String EmpID)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);

            IpRequestorView requestorView = new IpRequestorView();
            IpRequestorViewData requestor = requestorView.GetRequestor(EmpID);

            List<IpApprovalRequest> data = approvalReqReqDbAcess.GetRequestFor(requestor.IpRequestorId.ToString());
            List<IpApprovalRequestViewData> retData = Convert(data);
            foreach (IpApprovalRequestViewData item in retData)
            {
                AddOtherProperties(item);
            }
            return retData;
        }

        public List<IpApprovalRequestViewData> GetRequestFor(String EmpId, String Controller)
        {
            List<IpApprovalRequestViewData> retData = new List<IpApprovalRequestViewData>();
            IpRequestorView Model = new IpRequestorView();

            List<IpApprovalRequest> requests = null;

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpRequestorView requestorView = new IpRequestorView();
            IpRequestorViewData requestor = requestorView.GetRequestor(EmpId);

            requests = approvalRequestDbAccess.GetRequestFor(requestor.IpRequestorId.ToString());

            retData = IpApprovalRequestView.Convert(requests);

            foreach (IpApprovalRequestViewData item in retData)
            {
                AddOtherProperties(item);
                BuildDetailsLink(EmpId, item);
            }
            return retData;
        }

        public void BuildDetailsLink(String EmpId, IpApprovalRequestViewData ipApprovalRequest)
        {

            // BUILD THE LINK FOR THE USER ACTION based on Controller (cdburner, cell, etc)
            // and action type (Details, Edit, ReSubmit)
            String requestType = ipApprovalRequest.RequestType;
            String devIdStr = String.Empty;
            int devIdInt = 0;
            IpApprovalRequest.RequestTypeEnum requestTypeEnum;
            Enum.TryParse(requestType, out requestTypeEnum);
            String devController = String.Empty;
            switch (requestTypeEnum)
            {
                case IpApprovalRequest.RequestTypeEnum.cdburnner:
                    devController = "CdDvdRequest";
                    devIdStr = "CdburnerDeviceID";
                    devIdInt = ipApprovalRequest.CdburnerDeviceID;
                    break;
                case IpApprovalRequest.RequestTypeEnum.cellphone:
                    devController = "CellPhoneRequest";
                    devIdStr = "CellPhoneReqId";
                    devIdInt = ipApprovalRequest.CellPhoneDeviceId;
                    break;
                case IpApprovalRequest.RequestTypeEnum.cellphonesync:
                    devController = "CellPhoneSyncingReq";
                    devIdStr = "CellPhoneSyncDeviceId";
                    devIdInt = ipApprovalRequest.CellPhoneSyncDeviceID;
                    break;
                case IpApprovalRequest.RequestTypeEnum.laptop:
                    devController = "LapTopRequest";
                    devIdStr = "LapTopDeviceId";
                    devIdInt = ipApprovalRequest.LapTopID;
                    break;
                case IpApprovalRequest.RequestTypeEnum.remoteaccess:
                    devController = "RemoteAccess";
                    devIdStr = "RemoteAccessID";
                    devIdInt = ipApprovalRequest.RemoteAccessID;
                    break;
                case IpApprovalRequest.RequestTypeEnum.usb:
                    devController = "UsbRequest";
                    devIdStr = "UsbDeviceID";
                    devIdInt = ipApprovalRequest.UsbDeviceID;
                    break;
                case IpApprovalRequest.RequestTypeEnum.wireless:
                    devController = "WirelessRequest";
                    devIdStr = "WirelessDeviceID";
                    devIdInt = ipApprovalRequest.WirelessDeviceID;
                    break;
            }
            IpApprover.ApproveState approveState;
            Enum.TryParse(ipApprovalRequest.ApprovedStatus, out approveState);
            switch (approveState)
            {
                case IpApprover.ApproveState.approved:
                case IpApprover.ApproveState.not_submitted:
                case IpApprover.ApproveState.pending:
                case IpApprover.ApproveState.rejected:
                    ipApprovalRequest.RequestDetailsLink = String.Format("<a href=\"{0}/Details?EmpID={1}&{2}={3}\">Details</a>",
                        devController, EmpId, devIdStr, devIdInt);
                    break;
                case IpApprover.ApproveState.resubmit:
                    ipApprovalRequest.RequestDetailsLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&{2}={3}\">ReSubmit</a>",
                        devController, EmpId, devIdStr, devIdInt);
                    break;
                case IpApprover.ApproveState.saved:
                    ipApprovalRequest.RequestDetailsLink = String.Format("<a href=\"{0}/Edit?EmpID={1}&{2}={3}\">Edit</a>",
                        devController, EmpId, devIdStr, devIdInt);
                    break;
            }
        }
        public IpApprovalRequestViewData GetApprovalRequest(String ApprovalRequestId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest request = null;
            request = approvalRequestDbAccess.GetApprovalRequest(ApprovalRequestId);
            IpApprovalRequestViewData retData = IpApprovalRequestView.Convert(request);
            AddOtherProperties(retData);
            return retData;
        }

        public void AddOtherProperties(IpApprovalRequestViewData ipApprovalRequestViewData)
        {
            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData requestor = ipRequestorView.GetRequestorByRequestorId(ipApprovalRequestViewData.IpRequestorId);
            ipApprovalRequestViewData.RequestorsName = requestor.FullName;
            ipApprovalRequestViewData.RequuestorsEmpId = requestor.EmpID;
        }

        public IpApprovalRequestViewData GetApprovalRequest(String deviceId, IpApprovalRequest.RequestTypeEnum DeviceType)
        {
            IpApprovalRequestViewData retData = new IpApprovalRequestViewData();
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest request = null;
            request = model.GetApprovalRequest(deviceId);

            retData = IpApprovalRequestView.Convert(request);
            return retData;
        }


        private void AddRequestItem(IpRequestorViewData item, Dictionary<string, IpRequestorViewData> DictionaryOfRequestors, string controller, string action)
        {
            item.RequestDetailsLink = String.Format("<a href=\"/{0}/{1}?EmpID={2}\">Details</a>", controller, action, item.EmpID);
            if (!DictionaryOfRequestors.ContainsKey(item.IpRequestorId.ToString()))
            {
                item.NumberOfPendingReq = 1;
                DictionaryOfRequestors.Add(item.IpRequestorId.ToString(), item);
            }
            else
            {
                IpRequestorViewData ourData = DictionaryOfRequestors[item.IpRequestorId.ToString()] as IpRequestorViewData;
                ourData.NumberOfPendingReq++;
                DictionaryOfRequestors[item.IpRequestorId.ToString()] = ourData;
            }
        }


        /// <summary>
        /// Get un submited request and send emails to next approvers
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        internal bool SubmitRequest(string EmpID)
        {
            bool retValue = false;

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            IpRequestorView requestorView = new IpRequestorView();
            IpRequestorViewData requestor = requestorView.GetRequestor(EmpID);

            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            List<IpApprovalRequest> data = approvalReqDb.GetRequestFor(requestor.IpRequestorId.ToString());

            List<IpApprovalRequestViewData> allRequest = new List<IpApprovalRequestViewData>();
            foreach (IpApprovalRequest item in data)
            {
                // if the firstSup is not submitted then all the other should also be not_submitted
                if (item.FirstSupApproval == IpApprover.ApproveState.not_submitted.ToString())
                {
                    IpApprovalRequestViewData x = IpApprovalRequestView.Convert(item);
                    allRequest.Add(x);
                }
            }


            try
            {
                // Modify State to Pending 
                foreach (IpApprovalRequestViewData item in allRequest)
                {
                    item.FirstSupApproval = IpApprover.ApproveState.pending.ToString();
                    item.SecondSupApproval = IpApprover.ApproveState.pending.ToString();
                    item.VpHrApproval = IpApprover.ApproveState.pending.ToString();
                    item.RhCfoApproval = IpApprover.ApproveState.pending.ToString();
                    item.IpdApproval = IpApprover.ApproveState.pending.ToString();
                    item.CioApproval = IpApprover.ApproveState.pending.ToString();
                    item.VpHrApproval = IpApprover.ApproveState.pending.ToString();
                    IpApprovalRequest result = Convert(item);
                    approvalReqDb.UpdateFirstSupRequest(result);
                    approvalReqDb.UpdateSecondSupRequest(result);
                    approvalReqDb.UpdateVphrRequest(result);
                    approvalReqDb.UpdateRhCfoRequest(result);
                    approvalReqDb.UpdateCioRequest(result);
                    approvalReqDb.UpdateIpdRequest(result);
                }
                // Send Email to next Pending
            }
            catch (Exception ex)
            {
            }


            return retValue;
        }

        internal void ApproveProcessing(string ApproversEmpID, string ApproveOrReject, List<int> approvalList, String Comment)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);


            int intApproversEmpID = 0;
            int.TryParse(ApproversEmpID, out intApproversEmpID);
            IpApprover.ApproveState statusUpdate = IpApprover.ApproveState.not_submitted;

            if (ApproveOrReject == "Approve")
            {
                statusUpdate = IpApprover.ApproveState.approved;
            }
            if (ApproveOrReject == "Reject")
            {
                statusUpdate = IpApprover.ApproveState.rejected;
            }
            if (ApproveOrReject == "Re-Submit with changes")
            {
                statusUpdate = IpApprover.ApproveState.resubmit;
            }

            if (intApproversEmpID > 0)
            {
                foreach (int index in approvalList)
                {
                    IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(index.ToString());
                    if (request != null)
                    {
                        if (request.FirstSupEmpId == intApproversEmpID)
                        {
                            request.FirstSupApproval = statusUpdate.ToString();
                            request.FirstSupApprovalDate = DateTime.Now.Date;
                            request.FirstSupComment = Comment;
                            approvalRequestDbAccess.UpdateFirstSupRequest(request);
                        }
                        if (request.SecondSupEmpId == intApproversEmpID)
                        {
                            request.SecondSupApproval = statusUpdate.ToString();
                            request.SecondSupApprovalDate = DateTime.Now.Date;
                            request.SecondSupComment = Comment;
                            approvalRequestDbAccess.UpdateSecondSupRequest(request);
                        }
                        if (request.VpHrApproverEmpId == intApproversEmpID)
                        {
                            request.VpHrApproval = statusUpdate.ToString();
                            request.VpHrApprovalDate = DateTime.Now.Date;
                            request.VpHrComment = Comment;
                            approvalRequestDbAccess.UpdateVphrRequest(request);
                        }
                        if (request.RhCfoApproverEmpId == intApproversEmpID)
                        {
                            request.RhCfoApproval = statusUpdate.ToString();
                            request.RhCfoApprovalDate = DateTime.Now.Date;
                            request.RhCfoComment = Comment;
                            approvalRequestDbAccess.UpdateRhCfoRequest(request);
                        }
                        if (request.CioApproverEmpId == intApproversEmpID)
                        {
                            request.CioApproval = statusUpdate.ToString();
                            request.CioApprovalDate = DateTime.Now.Date;
                            request.CioComment = Comment;
                            approvalRequestDbAccess.UpdateCioRequest(request);
                        }
                        if (request.IpdApproverEmpId == intApproversEmpID)
                        {
                            request.IpdApproval = statusUpdate.ToString();
                            request.IpdApprovalDate = DateTime.Now.Date;
                            request.IpdComment = Comment;
                            approvalRequestDbAccess.UpdateIpdRequest(request);
                        }
                    }
                    IpApprovalRequestViewData req = Convert(request);
                    AddOtherProperties(req);

                    ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
                    approversEmailNotification.SubmitRequestToNextApprover(req.Id.ToString());
                    approversEmailNotification.SendNotificationRequestApproved(req);
                }

            }
        }

        internal void SubmitRequest(IpApprovalRequestViewData request)
        {

            request.FirstSupApproval = IpApprover.ApproveState.pending.ToString();
            request.SecondSupApproval = IpApprover.ApproveState.pending.ToString();
            request.VpHrApproval = IpApprover.ApproveState.pending.ToString();
            request.RhCfoApproval = IpApprover.ApproveState.pending.ToString();
            request.IpdApproval = IpApprover.ApproveState.pending.ToString();
            request.CioApproval = IpApprover.ApproveState.pending.ToString();

            IpApprovalRequestView model = new IpApprovalRequestView();
            model.UpdateFirstSupRequest(request);
            model.UpdateSecondSupRequest(request);
            model.UpdateVphrRequest(request);
            model.UpdateRhCfoRequest(request);
            model.UpdateIpdRequest(request);
            model.UpdateCioRequest(request);
            return;
        }

        public bool UpdateFirstSupRequest(IpApprovalRequestViewData request)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(request);
            bool result = approvalReqDb.UpdateFirstSupRequest(data);

            // notify user if rejected or need to resubmit
            IpApprover.ApproveState approveState;
            Enum.TryParse(request.FirstSupApproval, out approveState);
            if (approveState == IpApprover.ApproveState.rejected
                || approveState == IpApprover.ApproveState.resubmit)
            {
                ApproversEmailNotification notifier = new ApproversEmailNotification();
                notifier.SendNotificationRequestApproved(request);
            }

            return result;
        }

        public bool UpdateSecondSupRequest(IpApprovalRequestViewData request)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(request);
            bool result = approvalReqDb.UpdateSecondSupRequest(data);
            return result;
        }

        internal bool UpdateVphrRequest(IpApprovalRequestViewData request)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(request);
            bool result = approvalReqDb.UpdateVphrRequest(data);
            return result;
        }

        public bool UpdateRhCfoRequest(IpApprovalRequestViewData request)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(request);
            bool result = approvalReqDb.UpdateRhCfoRequest(data);
            return result;
        }

        public bool UpdateIpdRequest(IpApprovalRequestViewData request)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(request);
            bool result = approvalReqDb.UpdateIpdRequest(data);
            return result;
        }

        public bool UpdateCioRequest(IpApprovalRequestViewData request)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(request);
            // notify user if rejected or need to resubmit
            IpApprover.ApproveState approveState;
            Enum.TryParse(request.FirstSupApproval, out approveState);
            if (approveState == IpApprover.ApproveState.rejected
                || approveState == IpApprover.ApproveState.resubmit)
            {
                ApproversEmailNotification notifier = new ApproversEmailNotification();
                notifier.SendNotificationRequestApproved(request);
            }
            bool result = approvalReqDb.UpdateCioRequest(data);
            return result;
        }



        internal int SubmitRequest(String EmpID, String OurApprovalList)
        {
            // Convert String of int to list of request ids
            List<int> approvalList = GetApproveTheFollowing(OurApprovalList);

            if (approvalList == null || approvalList.Count == 0)
            {
                return 0;
            }
            List<IpApprovalRequestViewData> notSubmited = new List<IpApprovalRequestViewData>();
            IpApprovalRequestView model = new IpApprovalRequestView();
            // look up the request items  and build a list of request the user selected
            foreach (int approvalItem in approvalList)
            {
                IpApprovalRequestViewData req = model.GetApprovalRequest(approvalItem.ToString());
                if (req != null)
                {
                    notSubmited.Add(req);
                }
            }

            // Update the record
            foreach (IpApprovalRequestViewData item in notSubmited)
            {
                item.FirstSupApproval = IpApprover.ApproveState.pending.ToString();
                item.SecondSupApproval = IpApprover.ApproveState.pending.ToString();
                item.VpHrApproval = IpApprover.ApproveState.pending.ToString();
                item.RhCfoApproval = IpApprover.ApproveState.pending.ToString();
                item.IpdApproval = IpApprover.ApproveState.pending.ToString();
                item.CioApproval = IpApprover.ApproveState.pending.ToString();
                model.UpdateFirstSupRequest(item);
                model.UpdateSecondSupRequest(item);
                model.UpdateVphrRequest(item);
                model.UpdateRhCfoRequest(item);
                model.UpdateIpdRequest(item);
                model.UpdateCioRequest(item);
            }

            // SEND Notifications to the approvers
            ApproversEmailNotification Notification = new ApproversEmailNotification();
            foreach (IpApprovalRequestViewData item in notSubmited)
            {
                Notification.SubmitRequestToNextApprover(item.IpRequestorId.ToString());
            }

            return notSubmited.Count;
        }


        /// <summary>
        /// ourList += "Index=" + x + " RecordId=" + record.Id + ",";
        /// </summary>
        /// <param name="ourList"></param>
        internal List<int> GetApproveTheFollowing(string ourList)
        {
            String ourHdr = "Our Approvalls:";
            int iLength = "Index=".Length;
            int rLength = "RecordId=".Length;
            List<int> toApprove = new List<int>();
            if (ourList.Length > ourHdr.Length + iLength + rLength)
            {
                String startStr = ourList.Substring(ourHdr.Length);
                String[] elements = startStr.Split(',');

                foreach (String item in elements)
                {
                    if (item.Length >= iLength + rLength)
                    {
                        string[] componets = item.Split(' ');
                        String Index = componets[0].Substring(iLength);
                        String RecordId = componets[1].Substring(rLength);
                        int toAdd = 0;
                        int.TryParse(RecordId, out toAdd);
                        if (toAdd > 0)
                        {
                            toApprove.Add(toAdd);
                        }
                    }
                }
            }
            return toApprove;
        }

        public static bool IsDataOwner(String LoginUserName, int dataOwnerRequestorsId)
        {
            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData logInRequestor = ipRequestorView.GetRequestorByLoginId(LoginUserName);
            IpRequestorViewData dataOwnerRequestor = ipRequestorView.GetRequestorByRequestorId(dataOwnerRequestorsId);
            if (dataOwnerRequestor.EmpID == logInRequestor.EmpID)
            {
                return true;
            }
            return false;
        }

        public static bool IsSupervisor(String LoginUserName, int dataOwnerRequestorsId)
        {
            IpRequestorView ipRequestorView = new IpRequestorView();
            IpRequestorViewData logInRequestor = ipRequestorView.GetRequestorByLoginId(LoginUserName);
            IpRequestorViewData dataOwnerRequestor = ipRequestorView.GetRequestorByRequestorId(dataOwnerRequestorsId);
            if (dataOwnerRequestor.EmpID == logInRequestor.EmpID)
            {
                return true;
            }
            return false;
        }

        public static List<IpApprovalRequestViewData> Convert(List<IpApprovalRequest> ourData)
        {

            List<IpApprovalRequestViewData> retData = new List<IpApprovalRequestViewData>();
            if (ourData != null)
            {
                foreach (IpApprovalRequest device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static List<IpApprovalRequest> Convert(List<IpApprovalRequestViewData> ourData)
        {
            List<IpApprovalRequest> retData = new List<IpApprovalRequest>();
            if (ourData != null)
            {
                foreach (IpApprovalRequestViewData device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static IpApprovalRequestViewData Convert(IpApprovalRequest data)
        {
            try
            {
                IpApprovalRequestViewData retData = new IpApprovalRequestViewData();
                retData.LogonUserIdentity = data.LogonUserIdentity;
                retData.FirstSupApproval = data.FirstSupApproval;
                retData.FirstSupApprovalDate = data.FirstSupApprovalDate;
                retData.FirstSupComment = data.FirstSupComment;
                retData.FirstSupEmpId = data.FirstSupEmpId;
                retData.FirstSupName = data.FirstSupName;
                retData.FirstSupEmail = data.FirstSupEmail;

                retData.SecondSupEmpId = data.SecondSupEmpId;
                retData.SecondSupName = data.SecondSupName;
                retData.SecondSupApprovalDate = data.SecondSupApprovalDate;
                retData.SecondSupApproval = data.SecondSupApproval;
                retData.SecondSupComment = data.SecondSupComment;
                retData.SecondSupEmail = data.SecondSupEmail;

                retData.VpHrApproval = data.VpHrApproval;
                retData.VpHrApprovalDate = data.VpHrApprovalDate;
                retData.VpHrApproverEmpId = data.VpHrApproverEmpId;
                retData.VpHrComment = data.VpHrComment;
                retData.VpHrName = data.VpHrName;
                retData.VphrEmail = data.VphrEmail;

                retData.RhCfoApproval = data.RhCfoApproval;
                retData.RhCfoApprovalDate = data.RhCfoApprovalDate;
                retData.RhCfoApproverEmpId = data.RhCfoApproverEmpId;
                retData.RhCfoComment = data.RhCfoComment;
                retData.RhCfoName = data.RhCfoName;
                retData.RhCfoEmail = data.RhCfoEmail;

                retData.IpdApproval = data.IpdApproval;
                retData.IpdApprovalDate = data.IpdApprovalDate;
                retData.IpdApproverEmpId = data.IpdApproverEmpId;
                retData.IpdComment = data.IpdComment;
                retData.IpdName = data.IpdName;
                retData.IpdEmail = data.IpdEmail;

                retData.CioApproval = data.CioApproval;
                retData.CioApprovalDate = data.CioApprovalDate;
                retData.CioEmpId = data.CioApproverEmpId;
                retData.CioComment = data.CioComment;
                retData.CioName = data.CioName;
                retData.CioEmail = data.CioEmail;

                retData.RequestType = data.RequestType;
                retData.GrantDate = data.GrantDate;
                retData.SubmitDate = data.SubmitDate;
                retData.ReturnDate = data.ReturnDate;

                retData.IpRequestorId = data.IpRequestorId;
                retData.Id = data.IpApprovalRequestId;

                retData.CellPhoneDeviceId = data.CellPhoneDeviceId;
                retData.CdburnerDeviceID = data.CdburnerDeviceID;
                retData.CellPhoneSyncDeviceID = data.CellPhoneSyncDeviceID;
                retData.UsbDeviceID = data.UsbDeviceID;
                retData.LapTopID = data.LapTopID;
                retData.RemoteAccessID = data.RemoteAccessID;
                retData.WirelessDeviceID = data.WirelessDeviceID;

                retData.Archive = data.Archive;
                return retData;
            }
            catch (Exception ex)
            {
                String Message = String.Empty;
                Message = ex.Message;
            }
            return null;
        }

        public static IpApprovalRequest Convert(IpApprovalRequestViewData data)
        {
            IpApprovalRequest retData = new IpApprovalRequest()
            {
                LogonUserIdentity = data.LogonUserIdentity,
                FirstSupApproval = data.FirstSupApproval,
                FirstSupApprovalDate = data.FirstSupApprovalDate,
                FirstSupComment = data.FirstSupComment,
                FirstSupEmpId = data.FirstSupEmpId,
                FirstSupName = data.FirstSupName,
                FirstSupEmail = data.FirstSupEmail,

                SecondSupEmpId = data.SecondSupEmpId,
                SecondSupName = data.SecondSupName,
                SecondSupApprovalDate = data.SecondSupApprovalDate,
                SecondSupApproval = data.SecondSupApproval,
                SecondSupComment = data.SecondSupComment,
                SecondSupEmail = data.SecondSupEmail,

                IpdApproval = data.IpdApproval,
                IpdApprovalDate = data.IpdApprovalDate,
                IpdApproverEmpId = data.IpdApproverEmpId,
                IpdComment = data.IpdComment,
                IpdName = data.IpdName,
                IpdEmail = data.IpdEmail,

                RhCfoApproval = data.RhCfoApproval,
                RhCfoApprovalDate = data.RhCfoApprovalDate,
                RhCfoApproverEmpId = data.RhCfoApproverEmpId,
                RhCfoComment = data.RhCfoComment,
                RhCfoName = data.RhCfoName,
                RhCfoEmail = data.RhCfoEmail,

                VpHrApproval = data.VpHrApproval,
                VpHrApprovalDate = data.VpHrApprovalDate,
                VpHrApproverEmpId = data.VpHrApproverEmpId,
                VpHrComment = data.VpHrComment,
                VpHrName = data.VpHrName,
                VphrEmail = data.VphrEmail,

                CioApproval = data.CioApproval,
                CioApprovalDate = data.CioApprovalDate,
                CioApproverEmpId = data.CioEmpId,
                CioComment = data.CioComment,
                CioName = data.CioName,
                CioEmail = data.CioEmail,

                RequestType = data.RequestType,
                GrantDate = data.GrantDate,
                SubmitDate = data.SubmitDate,
                ReturnDate = data.ReturnDate,

                IpRequestorId = data.IpRequestorId,

                CellPhoneDeviceId = data.CellPhoneDeviceId,
                CdburnerDeviceID = data.CdburnerDeviceID,
                CellPhoneSyncDeviceID = data.CellPhoneSyncDeviceID,
                UsbDeviceID = data.UsbDeviceID,
                LapTopID = data.LapTopID,
                RemoteAccessID = data.RemoteAccessID,
                WirelessDeviceID = data.WirelessDeviceID,

                IpApprovalRequestId = data.Id,

                Archive = data.Archive
            };

            return retData;
        }

    }
}
//public List<IpApprovalRequestViewData> GetAllResubmitRequest(string EmpID)
//{
//    List<IpApprovalRequestViewData> request = GetRequest(EmpID);

//    var reSubmitted = (from A in request
//                        where A.FirstSupApproval == IpApprover.ApproveState.resubmit.ToString()
//                        select A);

//    return reSubmitted.ToList();
//}