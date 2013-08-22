using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IpRequestMvc2.Validators;

namespace InformationProtection.Models
{
    public class UsbViewData
    {
        public UsbViewData()
        {
            Initialize();
        }
        public int UsbDeviceId { get; set; }
        public int RequestorId { get; set; }
        [StringLength(160, MinimumLength = 3)]
        [Required]
        [Display(Name = "Employees Signature")]
        public String EmployeeSignature { get; set; }
        [StringLength(160, MinimumLength = 2)]
        [Required]
        public String Model { get; set; }
        [StringLength(160, MinimumLength = 8)]
        [Required]
        [Display(Name = "Serial Number")]
        public String SerialNumber { get; set; }
        [Display(Name = "New or Existing")]
        public String RenownOwned { get; set; } // New or Existing
        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Business Justification")]
        public String BusJustification { get; set; }


        [BooleanRequiredToBeTrue]
        public bool SecuredAck1 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecuredAck2 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecuredAck3 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecuredAck4 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecuredAck5 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecuredAck6 { get; set; }

        [BooleanRequiredToBeTrue]
        public bool SecurityAck1 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecurityAck2 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecurityAck3 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecurityAck4 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecurityAck5 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecurityAck6 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecurityAck7 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecurityAck8 { get; set; }
        [BooleanRequiredToBeTrue]
        public bool SecurityAck9 { get; set; }


        public bool SecuredAck
        {
            get
            {
                if (SecuredAck1 && SecuredAck2 && SecuredAck3 && SecuredAck4 && SecuredAck5 && SecuredAck6)
                    return true;
                else
                    return false;
            }
        }
        public bool SecurityAck
        {
            get
            {
                if (SecurityAck1 &&
                    SecurityAck2 &&
                    SecurityAck3 &&
                    SecurityAck4 &&
                    SecurityAck5 &&
                    SecurityAck6 &&
                    SecurityAck7 &&
                    SecurityAck8 &&
                    SecurityAck9
                    )
                    return true;
                else
                    return false;
            }
        }
        public bool Squestions
        {
            get
            {
                return SecuredAck && SecurityAck;
            }
        }

        public String RequestDetailsLink
        {
            get;
            set;
        }

        public string RequestEditLink { get; set; }

        internal void Initialize()
        {
            UsbDeviceId = 0;
            RequestorId = 0;

            Model = "";
            SerialNumber = "";
            BusJustification = "";
            RenownOwned = "";
            EmployeeSignature = "";

            SecuredAck1 = false;
            SecuredAck2 = false;
            SecuredAck3 = false;
            SecuredAck4 = false;
            SecuredAck5 = false;
            SecuredAck6 = false;

            SecurityAck1 = false;
            SecurityAck2 = false;
            SecurityAck3 = false;
            SecurityAck4 = false;
            SecurityAck5 = false;
            SecurityAck6 = false;
            SecurityAck7 = false;
            SecurityAck8 = false;
            SecurityAck9 = false;
                
        }
        internal void SaveInitialize()
        {
            if (Model == null)
                Model = "";
            if (SerialNumber == null)
                SerialNumber = "";
            if (BusJustification == null)
                BusJustification = "";
            if (RenownOwned == null)
                RenownOwned = "";
            if (EmployeeSignature == null)
                EmployeeSignature = "";
        }
        public bool HasAccessRights()
        {
            bool retValue = false;
            String logonUserIdentity = HttpContext.Current.Request.LogonUserIdentity.Name;
            IpRequestorView ipRequestorView = new IpRequestorView();

            // THE PERSON TRYING TO VIEW THE DATA
            IpRequestorViewData logInRequestor = ipRequestorView.GetRequestorByLoginId(logonUserIdentity);
            String logInEmpId = logInRequestor.EmpID;

            if (logInRequestor.IsAdmin)
            {
                retValue = true;
            }
            if (logInEmpId == RequestorId.ToString())
            {
                retValue = true;
            }
            if (logInEmpId == FirstSupEmpId.ToString())
            {
                retValue = true;
            }
            if (logInEmpId == SecondSupEmpId.ToString())
            {
                retValue = true;
            }
            if (logInEmpId == VpHrApproverEmpId.ToString())
            {
                retValue = true;
            }
            if (logInEmpId == RhCfoApproverEmpId.ToString())
            {
                retValue = true;
            }
            if (logInEmpId == IpdApproverEmpId.ToString())
            {
                retValue = true;
            }
            if (logInEmpId == CioEmpId.ToString())
            {
                retValue = true;
            }
            return retValue;
        }
        public string RequestStatus { get; set; }

        public int RequestId { get; set; }

        public string RequestorsName { get; set; }

        public string RequestReSubmitLink { get; set; }

        public int FirstSupEmpId { get; set; }

        public int SecondSupEmpId { get; set; }

        public int VpHrApproverEmpId { get; set; }

        public int RhCfoApproverEmpId { get; set; }

        public int IpdApproverEmpId { get; set; }

        public int CioEmpId { get; set; }
    }
}