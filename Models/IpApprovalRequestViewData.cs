﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IpModelData;

namespace InformationProtection.Models
{
    public class IpApprovalRequestViewData
    {
        public IpApprovalRequestViewData()
        {
            FirstSupApproval = IpApprover.ApproveState.not_submitted.ToString();
            SecondSupApproval = IpApprover.ApproveState.not_submitted.ToString();
            VpHrApproval = IpApprover.ApproveState.not_submitted.ToString();
            RhCfoApproval = IpApprover.ApproveState.not_submitted.ToString();
            IpdApproval = IpApprover.ApproveState.not_submitted.ToString();
            CioApproval = IpApprover.ApproveState.not_submitted.ToString();

            FirstSupComment = String.Empty;
            VpHrComment = String.Empty;
            RhCfoComment = String.Empty;
            SecondSupComment = String.Empty;
            IpdComment = String.Empty;
            CioComment = String.Empty;
        }

        public IpApprovalRequestViewData(int firstId, int vphId, int rhcfoId, int ipdId, int cioId)
        {
            FirstSupEmpId = firstId;
            VpHrApproverEmpId = vphId;
            RhCfoApproverEmpId = rhcfoId;
            IpdApproverEmpId = ipdId;
            CioEmpId = cioId;

        }
        public int Id { get; set; }
        [Display(Name = "Requestors Name")]
        public String RequestorsName { get; set; }


        private DateTime submitDate = DateTime.Now;
        [DataType(DataType.Date)]
        [Display(Name = "Submit Date")]
        public DateTime SubmitDate
        {
            get { return submitDate; }
            set { submitDate = value; }
        }
        private DateTime grantDate = DateTime.Now;
        [DataType(DataType.Date)]
        [Display(Name = "Grant Date")]
        public DateTime GrantDate
        {
            get { return grantDate; }
            set { grantDate = value; }
        }
        private DateTime returnDate = DateTime.Now;
        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }

        public int IpRequestorId { get; set; }
        //public IpRequestorViewData IpRequestorMdlData { get; set; }
        public String RequuestorsEmpId { get; set; }

        // 1. FIRST SUPERVISOR
        // The approval Chain (emp Id)
        public int FirstSupEmpId { get; set; }

        [Display(Name = "Name")]
        public String FirstSupName { get; set; }

         [Display(Name = "Approved Date")]
        public DateTime FirstSupApprovalDate { get; set; }

        private string firstSupApproval;
         [Display(Name = "Status")]
        public string FirstSupApproval 
        {
            get 
            {
                return firstSupApproval;
            }
            set
            {
                firstSupApproval = value.ToLower();
            }
        }
        [Display(Name = "Comment")]
        public string FirstSupComment { get; set; }
        [Display(Name = "Email")]
        public string FirstSupEmail { get; set; }

        // 2. Second Leader
        // assume for now that the 2nd sup is the sup of the 1st
        public int SecondSupEmpId { get; set; } // EmpID
        [Display(Name = "Name")]
        public String SecondSupName { get; set; }
        public DateTime SecondSupApprovalDate { get; set; }

        string secondSupApproval;
        public string SecondSupApproval
        {
            get
            {
                return secondSupApproval;
            }
            set
            {
                secondSupApproval = value.ToLower();
            }
        }
         [Display(Name = "Comment")]
        public string SecondSupComment { get; set; }
         [Display(Name = "Email")]
         public string SecondSupEmail { get; set; }

        // 3. VPHR
        public int VpHrApproverEmpId { get; set; }
        [Display(Name = "Name")]
        public String VpHrName { get; set; }
         [Display(Name = "Date")]
        public DateTime VpHrApprovalDate { get; set; }

        private string vpHrApproval;
         [Display(Name = "Status")]
        public string VpHrApproval
        {
            get
            {
                return vpHrApproval;
            }
            set
            {
                vpHrApproval = value.ToLower();
            }
        }
        [Display(Name = "Comment")]
        public string VpHrComment { get; set; }
        [Display(Name = "Email")]
        public string VphrEmail { get; set; }

        // 4. RHCFO
        public int RhCfoApproverEmpId { get; set; }
        [Display(Name = "Name")]
        public String RhCfoName { get; set; }
         [Display(Name = "Date")]
        public DateTime RhCfoApprovalDate { get; set; }
        private string rhCfoApproval;
        [Display(Name = "Status")]
        public string RhCfoApproval
        {
            get
            {
                return rhCfoApproval;
            }
            set
            {
                rhCfoApproval = value.ToLower();
            }
        }
         [Display(Name = "Comment")]
        public string RhCfoComment { get; set; }
         [Display(Name = "Email")]
         public string RhCfoEmail { get; set; }

        // 5. Renown CIO (Information Protection Director)
        public int IpdApproverEmpId { get; set; }
        [Display(Name = "Name")]
        public String IpdName { get; set; }
         [Display(Name = "Date")]
        public DateTime IpdApprovalDate { get; set; }

        private string ipdApproval;
         [Display(Name = "Status")]
        public string IpdApproval
        {
            get
            {
                return ipdApproval;
            }
            set
            {
                ipdApproval = value.ToLower();
            }
        }
        [Display(Name = "Comment")]
        public string IpdComment { get; set; }
        [Display(Name = "Email")]
        public string IpdEmail { get; set; }

        // 5. Renown CIO (Information Protection Director) Chuck Skully
        public int CioEmpId { get; set; } // EmpID
         [Display(Name = "Name")]
        public String CioName { get; set; }
         [Display(Name = "Date")]
        public DateTime CioApprovalDate { get; set; }

        private string cioApproval;
        [Display(Name = "Status")]
        public string CioApproval
        {
            get
            {
                return cioApproval;
            }
            set
            {
                cioApproval = value.ToLower();
            }
        }
        [Display(Name = "Comment")]
        public string CioComment { get; set; }
        [Display(Name = "Email")]
        public string CioEmail { get; set; }


         //[Display(Name = "IP Request Type")]
        //public String IpRequestType { get; set; } // identifies one of the following
         [Display(Name = "Approval Status")]
        public string ApprovedStatus
        {
            get
            {
                if (FirstSupApproval == IpApprover.ApproveState.resubmit.ToString()
                    || SecondSupApproval == IpApprover.ApproveState.resubmit.ToString()
                    || VpHrApproval == IpApprover.ApproveState.resubmit.ToString()
                    || RhCfoApproval == IpApprover.ApproveState.resubmit.ToString()
                    || IpdApproval == IpApprover.ApproveState.resubmit.ToString()
                    || CioApproval == IpApprover.ApproveState.resubmit.ToString())
                {
                    return IpApprover.ApproveState.resubmit.ToString();
                }
                if (FirstSupApproval == IpApprover.ApproveState.saved.ToString()
                    && SecondSupApproval == IpApprover.ApproveState.saved.ToString()
                    && VpHrApproval == IpApprover.ApproveState.saved.ToString()
                    && RhCfoApproval == IpApprover.ApproveState.saved.ToString()
                    && IpdApproval == IpApprover.ApproveState.saved.ToString()
                    && CioApproval == IpApprover.ApproveState.saved.ToString())
                {
                    return IpApprover.ApproveState.saved.ToString();
                }
                if (FirstSupApproval == IpApprover.ApproveState.approved.ToString() 
                    && SecondSupApproval == IpApprover.ApproveState.approved.ToString()
                    && VpHrApproval == IpApprover.ApproveState.approved.ToString()
                    && RhCfoApproval == IpApprover.ApproveState.approved.ToString() 
                    && IpdApproval == IpApprover.ApproveState.approved.ToString()
                    && CioApproval == IpApprover.ApproveState.approved.ToString())
                {
                    return IpApprover.ApproveState.approved.ToString();
                }
                if (FirstSupApproval == IpApprover.ApproveState.rejected.ToString()
                    || SecondSupApproval == IpApprover.ApproveState.rejected.ToString() 
                    || VpHrApproval == IpApprover.ApproveState.rejected.ToString() 
                    || RhCfoApproval == IpApprover.ApproveState.rejected.ToString() 
                    || IpdApproval == IpApprover.ApproveState.rejected.ToString()
                    || CioApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    return IpApprover.ApproveState.rejected.ToString();
                }
                return IpApprover.ApproveState.pending.ToString();
            }
        }

        // Information about the person requestiong
        public bool Archive { get; set; }

        // Request is for one of the following
        public String RequestType { get; set; } // identifies one of the following
        public int CdburnerDeviceID { get; set; }
        public int CellPhoneSyncDeviceID { get; set; }
        public int CellPhoneDeviceId { get; set; }
        public int LapTopID { get; set; }
        public int RemoteAccessID { get; set; }
        public int UsbDeviceID { get; set; }
        public int WirelessDeviceID { get; set; }
        public int DeviceId
        {
            get
            {
                if (RequestType == IpModelData.IpApprovalRequest.RequestTypeEnum.cdburnner.ToString())
                {
                    return CdburnerDeviceID;
                }
                if (RequestType == IpModelData.IpApprovalRequest.RequestTypeEnum.cellphonesync.ToString())
                {
                    return CellPhoneSyncDeviceID;
                }
                if (RequestType == IpModelData.IpApprovalRequest.RequestTypeEnum.cellphone.ToString())
                {
                    return CellPhoneDeviceId;
                }
                if (RequestType == IpModelData.IpApprovalRequest.RequestTypeEnum.laptop.ToString())
                {
                    return LapTopID;
                }
                if (RequestType == IpModelData.IpApprovalRequest.RequestTypeEnum.remoteaccess.ToString())
                {
                    return RemoteAccessID;
                }
                if (RequestType == IpModelData.IpApprovalRequest.RequestTypeEnum.usb.ToString())
                {
                    return UsbDeviceID;
                }
                if (RequestType == IpModelData.IpApprovalRequest.RequestTypeEnum.wireless.ToString())
                {
                    return WirelessDeviceID;
                }
                return 0;
   
            }
        }
        public String RequestDetailsLink { get; set; }
        public String RemindersLink { get; set; }

        public string FirstSupStatus 
        {
            get
            {
                String result = "p";
                if (FirstSupApproval == IpApprover.ApproveState.resubmit.ToString())
                {
                    result = "fb";
                }
                else
                {
                    result = FirstSupApproval.Substring(0, 1);
                }
                return result;
            }
        }
        public string SecondSupStatus 
        { 
            get 
            {
                String result = "p";
                if (SecondSupApproval == IpApprover.ApproveState.resubmit.ToString())
                {
                    result = "fb";
                }
                else
                {
                    result = SecondSupApproval.Substring(0, 1);
                }
                return result;
            } 
        }
        public string VpHrStatus 
        { 
            get 
            {
                String result = "p";
                if (VpHrApproval == IpApprover.ApproveState.resubmit.ToString())
                {
                    result = "fb";
                }
                else
                {
                    result = VpHrApproval.Substring(0, 1);
                }
                return result;
            } 
        }
        public string RhCfoStatus 
        { 
            get 
            {
                String result = "p";
                if (RhCfoApproval == IpApprover.ApproveState.resubmit.ToString())
                {
                    result = "fb";
                }
                else
                {
                    result = RhCfoApproval.Substring(0, 1);
                }
                return result;
            } 
        }
        public string IpdStatus 
        { 
            get 
            {
                String result = "p";
                if (ipdApproval == IpApprover.ApproveState.resubmit.ToString())
                {
                    result = "fb";
                }
                else
                {
                    result = ipdApproval.Substring(0, 1);
                }
                return result;
            } 
        }
        public string CioStatus 
        { 
            get 
            {
                String result = "p";
                if (CioApproval == IpApprover.ApproveState.resubmit.ToString())
                {
                    result = "fb";
                }
                else
                {
                    result = CioApproval.Substring(0, 1);
                }
                return result; 
            } 
        }

        public bool IsFirstSupApprovalNextPending()
        {
            return FirstSupApproval == IpApprover.ApproveState.pending.ToString();
        }

        public bool IsSecondSupApprovalNextPending()
        {
            return FirstSupApproval == IpApprover.ApproveState.approved.ToString() &&
                SecondSupApproval == IpApprover.ApproveState.pending.ToString();
        }

        public bool IsVpHrApprovalNextPending()
        {
            return FirstSupApproval == IpApprover.ApproveState.approved.ToString()
                && SecondSupApproval == IpApprover.ApproveState.approved.ToString()
                &&  VpHrApproval == IpApprover.ApproveState.pending.ToString();
        }

        public bool IsRhCfoApprovalNextPending()
        {
            return FirstSupApproval == IpApprover.ApproveState.approved.ToString()
                && SecondSupApproval == IpApprover.ApproveState.approved.ToString()
                &&  VpHrApproval == IpApprover.ApproveState.approved.ToString()
                && rhCfoApproval == IpApprover.ApproveState.pending.ToString();
        }


        public bool IsIpdApprovalNextPending()
        {
            return FirstSupApproval == IpApprover.ApproveState.approved.ToString()
                && SecondSupApproval == IpApprover.ApproveState.approved.ToString()
                && VpHrApproval == IpApprover.ApproveState.approved.ToString()
                && rhCfoApproval == IpApprover.ApproveState.approved.ToString()
                && ipdApproval == IpApprover.ApproveState.pending.ToString();
        }

        public bool IsCioApprovalNextPending()
        {
            return FirstSupApproval == IpApprover.ApproveState.approved.ToString()
                && SecondSupApproval == IpApprover.ApproveState.approved.ToString()
                && VpHrApproval == IpApprover.ApproveState.approved.ToString()
                && RhCfoApproval == IpApprover.ApproveState.approved.ToString()
                && IpdApproval == IpApprover.ApproveState.approved.ToString()
                && CioApproval == IpApprover.ApproveState.pending.ToString();
        }

        internal bool IsPending(int EmpID)
        {
            if (FirstSupEmpId == EmpID && IsFirstSupApprovalNextPending())
            {
                return true;
            }
            if (SecondSupEmpId == EmpID && IsSecondSupApprovalNextPending())
            {
                return true;
            }
            if (VpHrApproverEmpId == EmpID && IsVpHrApprovalNextPending())
            {
                return true;
            }
            if (RhCfoApproverEmpId == EmpID && IsRhCfoApprovalNextPending())
            {
                return true;
            }
            if (IpdApproverEmpId == EmpID && IsIpdApprovalNextPending())
            {
                return true;
            }
            if (CioEmpId == EmpID && IsCioApprovalNextPending())
            {
                return true;
            }
            return false;
        }
        public String Identifier
        {
            get
            {
                String retValue = String.Empty;
                retValue = String.Format("({0},{1})", Id, DeviceId);
                return retValue;
            }
        }
        public String PendingApproverLevel
        {
            get
            {
                String retValue = String.Empty;
                if (IsFirstSupApprovalNextPending())
                {
                    retValue = IpApprover.FIRSTSUP;
                }
                if (IsSecondSupApprovalNextPending())
                {
                    retValue = IpApprover.SECONDSUP;
                }
                if (IsVpHrApprovalNextPending())
                {
                    retValue = IpApprover.VPHR;
                }
                if (IsRhCfoApprovalNextPending())
                {
                    retValue = IpApprover.RHCFO;
                }
                if (IsIpdApprovalNextPending())
                {
                    retValue = IpApprover.IPD;
                }
                if (IsCioApprovalNextPending())
                {
                    retValue = IpApprover.CIO;
                }
                return retValue;
            }
        }
        public String PendingApproverName
        {
            get
            {
                String retValue = String.Empty;
                if (IsFirstSupApprovalNextPending())
                {
                    retValue = FirstSupName;
                }
                if (IsSecondSupApprovalNextPending())
                {
                    retValue = SecondSupName;
                }
                if (IsVpHrApprovalNextPending())
                {
                    retValue = VpHrName;
                }
                if (IsRhCfoApprovalNextPending())
                {
                    retValue = RhCfoName;
                }
                if (IsIpdApprovalNextPending())
                {
                    retValue = IpdName;
                }
                if (IsCioApprovalNextPending())
                {
                    retValue = CioName;
                }
                return retValue;
            }
        }
        public String RejectedApproverName
        {
            get
            {
                String retValue = String.Empty;
                if (FirstSupApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = FirstSupName;
                }
                if (SecondSupApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = SecondSupName;
                }
                if (VpHrApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = VpHrName;
                }
                if (RhCfoApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = RhCfoName;
                }
                if (IpdApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = IpdName;
                }
                if ( CioApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = CioName;
                }
                return retValue;
            }
        }
        public DateTime RejectedDate
        {
            get
            {
                DateTime retValue = DateTime.MinValue;
                if (FirstSupApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = FirstSupApprovalDate;
                }
                if (SecondSupApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = SecondSupApprovalDate;
                }
                if (VpHrApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = VpHrApprovalDate;
                }
                if (RhCfoApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = RhCfoApprovalDate;
                }
                if (IpdApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = IpdApprovalDate;
                }
                if ( CioApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = CioApprovalDate;
                }
                return retValue;
            }

        }
        public String RejectedApproverLevel
        {
            get
            {
                String retValue = String.Empty;
                if (FirstSupApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = IpApprover.FIRSTSUP;
                }
                if (SecondSupApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = IpApprover.SECONDSUP;
                }
                if (VpHrApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = IpApprover.VPHR;
                }
                if (RhCfoApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = IpApprover.RHCFO;
                }
                if (IpdApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = IpApprover.IPD;
                }
                if (CioApproval == IpApprover.ApproveState.rejected.ToString())
                {
                    retValue = IpApprover.CIO;
                }
                return retValue;
            }
        }

        public string LogonUserIdentity { get; set; }
    }
}


                //RejectedApproverLevel: {
                //    title: 'Rejected On'
                //},
                //RejectedApproverName: {
                //    title: 'Rejector Name',
                //    sorting: false
                //},
                //RejectedDate: {
                //    title: 'Rejected Date',
                //    sorting: false
                //}