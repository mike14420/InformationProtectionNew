using System;
using System.Collections.Generic;
using System.Linq;
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


            return IpApprovalRequestId;
        }

        public bool UpdateState(IpApprover.ApproveState state, int devId, IpApprovalRequest.RequestTypeEnum type)
        {

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess ApprovalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);
            bool result = ApprovalReqReqDbAcess.InitApprovalRequestState(devId, type.ToString(), state.ToString());

            return result;
        }

        IpApprovalRequestViewData CreateRequest(String EmpID, IpApprover.ApproveState state)
        {
            IpApprovalRequestViewData Request = new IpApprovalRequestViewData();

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

        public int Create(CdBurrnerViewData data, String EmpID, IpApprover.ApproveState state)
        {

            IpRequestorView Mdl = new IpRequestorView();
            IpRequestorViewData requestor = Mdl.GetRequestor(EmpID);
            // add the forien key
            data.RequestorId = requestor.IpRequestorId;

            CdBurnerView cMdl = new CdBurnerView();
            int cellPhoneDevId = cMdl.Create(data);

            IpApprovalRequestViewData Request = CreateRequest(EmpID, state);
            Request.CellPhoneDeviceId = cellPhoneDevId;
            Request.RequestType = IpApprovalRequest.RequestTypeEnum.cellphone.ToString();
            /// Now Write to DB
            int retValue = Create(Request);
            return retValue;
        }
        public bool Update(CdBurrnerViewData data, IpApprover.ApproveState state)
        {
            CdBurnerView CdMdl = new CdBurnerView();
            // Update the Device
            bool result = CdMdl.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;           
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            // Update the Request
            bool retValue = model.InitApprovalRequestState(data.CdburnerDeviceId, IpApprovalRequest.RequestTypeEnum.cdburrner.ToString(), state.ToString());
            return retValue;
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
            return retValue;
        }
        public bool Update(CellPhoneViewData data, IpApprover.ApproveState state)
        {
            CellPhoneView cellPhoneView = new CellPhoneView();
            bool result = cellPhoneView.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;    
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            bool retValue = model.InitApprovalRequestState(data.CellPhoneReqId, IpApprovalRequest.RequestTypeEnum.cellphone.ToString(), state.ToString());
            return retValue;
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
            return Create(Request);
        }

        public bool Update(LapTopViewData data, IpApprover.ApproveState state)
        {
            LapTopView lapTopView = new LapTopView();
            bool result = lapTopView.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            bool retValue = model.InitApprovalRequestState(data.LapTopDeviceId, IpApprovalRequest.RequestTypeEnum.laptop.ToString(), state.ToString());
            return retValue;
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
            return Create(Request);
        }
        public bool Update(UsbViewData data, IpApprover.ApproveState state)
        {
            UsbView lapTopView = new UsbView();
            bool result = lapTopView.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            bool retValue = model.InitApprovalRequestState(data.UsbDeviceId, IpApprovalRequest.RequestTypeEnum.usb.ToString(), state.ToString());
            return retValue;
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
            return Create(Request);
        }
        public bool Update(CellPhoneSyncMdlData data, IpApprover.ApproveState state)
        {
            CellPhoneSyncMdl cellPhoneSyncMdl = new CellPhoneSyncMdl();
            bool result = cellPhoneSyncMdl.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            bool retValue = model.InitApprovalRequestState(data.CellPhoneSyncDeviceId, IpApprovalRequest.RequestTypeEnum.cellphonesync.ToString(), state.ToString());
            return retValue;
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
            return Create(Request);
        }
        public bool Update(WirelessMdlData data, IpApprover.ApproveState state)
        {
            WirelessMdl wirelessMdl = new WirelessMdl();
            bool result = wirelessMdl.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            bool retValue = model.InitApprovalRequestState(data.WirelessDeviceId, IpApprovalRequest.RequestTypeEnum.wireless.ToString(), state.ToString());
            return retValue;
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
            return Create(Request);
        }

        public bool Update(RemoteAccessMdlData data, IpApprover.ApproveState state)
        {
            RemoteAccessMdl remoteAccessMdl = new RemoteAccessMdl();
            bool result = remoteAccessMdl.Update(data);
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            bool retValue = model.InitApprovalRequestState(data.RemoteAccessId, IpApprovalRequest.RequestTypeEnum.remoteaccess.ToString(), state.ToString());
            return retValue;
        }

        /// <summary>
        ///  FIND ALL REQUEST for approver with EMPID
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public List<IpApprovalRequestViewData> GetApproversData(String ApproverEmpID, String action)
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
                    IpRequestorView requestorModel = new IpRequestorView();
                    IpRequestorViewData requestor = requestorModel.GetRequestorByRequestorId(item.IpRequestorId);
                    item.RequuestorsEmpId = requestor.EmpID;
                    item.RequestorsName = requestor.FullName;
                    PendingList.Add(item);
                }
            }
            foreach (IpApprovalRequestViewData item in PendingList)
            {
                item.RequestDetailsLink = String.Format("<a href=\"{0}?Id={1}&ApproverEmpID={2}\">Details</a>",
                    action, item.Id, ApproverEmpID);
            }
            return PendingList.OrderBy(P => P.RequuestorsEmpId).ToList();
        }

        /// <summary>
        /// STATE=IpApprover.ApproveState.not_submitted.ToString();
        /// </summary>
        /// <returns></returns>
        public List<IpApprovalRequestViewData> GetRequestByState(String state, String action)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess ApprovalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);

            List<IpApprovalRequest> data = ApprovalReqReqDbAcess.GetRequestByState(state);
            List<IpApprovalRequestViewData> request = Convert(data);

            IpRequestorView RequestorsView = new IpRequestorView();
            foreach (IpApprovalRequestViewData item in request)
            {
                IpRequestorViewData ourRequestor = RequestorsView.GetRequestorByRequestorId(item.IpRequestorId);
                if (ourRequestor != null)
                {
                    item.RequestorsName = String.Format("{0} {1}", ourRequestor.Fname, ourRequestor.Lname);
                }
                item.RequestDetailsLink = String.Format("<a href=\"{0}?Id={1}\">Reminder</a>",
                    action, item.Id);
            }
            return request.OrderBy(P => P.RequuestorsEmpId).ToList();
        }

        public List<IpApprovalRequestViewData> GetRequest(String EmpID)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess ApprovalReqReqDbAcess = new ApprovalRequestDbAccess(connectionString);

            IpRequestorView requestorView = new IpRequestorView();
            IpRequestorViewData requestor = requestorView.GetRequestor(EmpID);

            List<IpApprovalRequest> data = ApprovalReqReqDbAcess.GetRequestFor(requestor.IpRequestorId.ToString());
            List<IpApprovalRequestViewData> retData = Convert(data);

            return retData;
        }

        public List<IpApprovalRequestViewData> GetRequestFor(String EmpId, String Controller, String Action)
        {
            List<IpApprovalRequestViewData> retData = new List<IpApprovalRequestViewData>();
            IpRequestorView Model = new IpRequestorView();

            List<IpApprovalRequest> requests = null;

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            IpRequestorView requestorView = new IpRequestorView();
            IpRequestorViewData requestor = requestorView.GetRequestor(EmpId);

            requests = model.GetRequestFor(requestor.IpRequestorId.ToString());

            retData = IpApprovalRequestView.Convert(requests);

            foreach (IpApprovalRequestViewData item in retData)
            {
                item.RequestDetailsLink = String.Format("<a href=\"{0}/{1}?EmpID={2}&ApprovalRequestId={3}\">Details</a>",
                    Controller, Action, EmpId, item.Id);
            }
            return retData;
        }



        public IpApprovalRequestViewData GetApprovalRequest(String ApprovalRequestId)
        {
            IpApprovalRequestViewData retData = new IpApprovalRequestViewData();
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess model = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest request = null;
            request = model.GetApprovalRequest(ApprovalRequestId);

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

        internal void ApproveProcessing(string EmpID, string ApproveOrReject, List<int> approvalList, String Comment)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();

            int intEmpID = 0;
            int.TryParse(EmpID, out intEmpID);
            string statusUpdate = String.Empty;

            if (ApproveOrReject == "Approve")
            {
                statusUpdate = IpApprover.ApproveState.approved.ToString();
            }
            if (ApproveOrReject == "Reject")
            {
                statusUpdate = IpApprover.ApproveState.rejected.ToString();
            }

            if (!String.IsNullOrEmpty(statusUpdate) && intEmpID > 0)
            {
                foreach (int index in approvalList)
                {
                    IpApprovalRequest request = approvalReqDb.GetApprovalRequest(index.ToString());
                    if (request != null)
                    {
                        if (request.FirstSupEmpId == intEmpID)
                        {
                            request.FirstSupApproval = statusUpdate;
                            request.FirstSupApprovalDate = DateTime.Now.Date;
                            request.FirstSupComment = Comment;
                            approvalReqDb.UpdateFirstSupRequest(request);
                        }
                        if (request.SecondSupEmpId == intEmpID)
                        {
                            request.SecondSupApproval = statusUpdate;
                            request.SecondSupApprovalDate = DateTime.Now.Date;
                            request.SecondSupComment = Comment;
                            approvalReqDb.UpdateSecondSupRequest(request);
                        }
                        if (request.VpHrApproverEmpId == intEmpID)
                        {
                            request.VpHrApproval = statusUpdate;
                            request.VpHrApprovalDate = DateTime.Now.Date;
                            request.VpHrComment = Comment;
                            approvalReqDb.UpdateVphrRequest(request);
                        }
                        if (request.RhCfoApproverEmpId == intEmpID)
                        {
                            request.RhCfoApproval = statusUpdate;
                            request.RhCfoApprovalDate = DateTime.Now.Date;
                            request.RhCfoComment = Comment;
                            approvalReqDb.UpdateRhCfoRequest(request);
                        }
                        if (request.CioApproverEmpId == intEmpID)
                        {
                            request.CioApproval = statusUpdate;
                            request.CioApprovalDate = DateTime.Now.Date;
                            request.CioComment = Comment;
                            approvalReqDb.UpdateCioRequest(request);
                        }
                        if (request.IpdApproverEmpId == intEmpID)
                        {
                            request.IpdApproval = statusUpdate;
                            request.IpdApprovalDate = DateTime.Now.Date;
                            request.IpdComment = Comment;
                            approvalReqDb.UpdateIpdRequest(request);
                        }
                    }
                    IpApprovalRequestViewData req = Convert(request);
                    approversEmailNotification.SubmitRequestToNextApprover(EmpID, req);
                }

            }
        }

        public List<IpApprovalRequestViewData> GetAllNotSubmittedRequest(string EmpID)
        {
            List<IpApprovalRequestViewData> request = GetRequest(EmpID);

            var notSubmitted = (from A in request
                                where A.FirstSupApproval == IpApprover.ApproveState.not_submitted.ToString()
                                select A);

            return notSubmitted.ToList();
        }

        public bool UpdateFirstSupRequest(IpApprovalRequestViewData item)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(item);
            bool result = approvalReqDb.UpdateFirstSupRequest(data);
            return result;
        }

        public bool UpdateSecondSupRequest(IpApprovalRequestViewData item)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(item);
            bool result = approvalReqDb.UpdateSecondSupRequest(data);
            return result;
        }

        internal bool UpdateVphrRequest(IpApprovalRequestViewData item)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(item);
            bool result = approvalReqDb.UpdateVphrRequest(data);
            return result;
        }

        public bool UpdateRhCfoRequest(IpApprovalRequestViewData item)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(item);
            bool result = approvalReqDb.UpdateRhCfoRequest(data);
            return result;
        }

        public bool UpdateIpdRequest(IpApprovalRequestViewData item)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(item);
            bool result = approvalReqDb.UpdateIpdRequest(data);
            return result;
        }

        public bool UpdateCioRequest(IpApprovalRequestViewData item)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalReqDb = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest data = Convert(item);
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
                Notification.SubmitRequestToNextApprover(EmpID, item);
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
    }
}