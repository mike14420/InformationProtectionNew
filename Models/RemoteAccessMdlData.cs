using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using InformationProtection.Validators;
using IpRequestMvc2.Validators;

namespace InformationProtection.Models
{
    public class RemoteAccessMdlData
    {
        public RemoteAccessMdlData()
        {
            Initialize();
        }

        public int RemoteAccessId { get; set; }
        public int RequestorId { get; set; }

        [Required]
        [Display(Name = "Employee Signature")]
        [StringLength(160, MinimumLength = 3)]
        public String EmployeeSignature { get; set; }


        public bool AccessToServer { get; set; }
        [Display(Name = "Host Name or IP Address")]
        [RequiredIf("AccessToServer", true, ErrorMessage = "Please Enter Server or Host Name")]
        public String IpAddressAndHostName { get; set; }


        public bool AccessToApp { get; set; }
        [RequiredIf("AccessToApp", true, ErrorMessage = "Please Enter Application Names")]
        [Display(Name = "Application Names")]
        [StringLength(160, MinimumLength = 3)]
        public String AppNames { get; set; }

        public bool AccessToMyWorkStation { get; set; }
        [RequiredIf("AccessToMyWorkStation", true, ErrorMessage = "Please Enter Computer Name")]
        [Display(Name = "My Workstation")]
        [StringLength(160, MinimumLength = 3)]
        public string ComputerName { get; set; }


        public bool AccessToLanShares { get; set; }
        public string LanShares { get; set; }

        public bool ConnectFromHome { get; set; }

        [RequiredIf("ConnectFromHome", true, ErrorMessage = "Please Enter Computer OS")]
        [Display(Name = "WorkStation OS")]
        [StringLength(160, MinimumLength = 3)]
        public String WorkStationOS { get; set; }
        public String RemoteConnectionType { get; set; }

        [Display(Name = "I understand I am responsible for:")]
        [BooleanRequiredToBeTrue]
        public bool SecuredAck1 { get; set; }

        [Display(Name = "Current patches")]
        [BooleanRequiredToBeTrue]
        public bool SecuredAck2 { get; set; }

        [Display(Name = "Current anti-virus")]
        [BooleanRequiredToBeTrue]
        public bool SecuredAck3 { get; set; }

        [BooleanRequiredToBeTrue]
        [Display(Name = "Personal firewall")]
        public bool SecuredAck4 { get; set; }

        [BooleanRequiredToBeTrue]     
        public bool SecuredAck5 { get; set; } // everything written is true

        [Required]
        [Display(Name = "Job Duties")]
        [StringLength(160, MinimumLength = 3)]
        public String JobDuties { get; set; }
        [Display(Name = " I am a non-exempt (hourly) Renown Health employee")]
        public bool NonExemptEmployee { get; set; }

        public bool SecuredAck
        {
            get
            {
                if (SecuredAck1 && SecuredAck2 && SecuredAck3 && SecuredAck4 && SecuredAck5)
                    return true;
                else
                    return false;
            }
        }

        public String AccessToServerStr
        {
            get
            {
                return AccessToServer ? "T" : "F";
            }
        }

        public String AccessToAppStr
        {
            get
            {
                return AccessToApp ? "T" : "F";
            }
        }
        public String AccessToMyWorkStationStr
        {
            get
            {
                return AccessToMyWorkStation ? "T" : "F";
            }
        }
        public String AccessToLanSharesStr
        {
            get
            {
                return AccessToLanShares ? "T" : "F";
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
            RemoteAccessId = 0;
            RequestorId = 0;

            NonExemptEmployee = false;
            AccessToApp = false;
            AccessToMyWorkStation = false;
            AccessToServer = false;
            AccessToLanShares = false;
            ConnectFromHome = false;
            SecuredAck1 = false;
            SecuredAck2 = false;
            SecuredAck3 = false;
            SecuredAck4 = false;
            SecuredAck5 = false;

            AppNames = "";
            EmployeeSignature = "";
            LanShares = "";
            IpAddressAndHostName = "";
            RemoteConnectionType = "";
            JobDuties = ""; 
            WorkStationOS = "";
            ComputerName = "";
        }

        internal void SaveInitialize()
        {
            if (AppNames == null)
                AppNames = "";
            if (EmployeeSignature == null)
                EmployeeSignature = "";
            if (LanShares == null)
                LanShares = "";
            if (IpAddressAndHostName == null)
                IpAddressAndHostName = "";
            if (RemoteConnectionType == null)
                RemoteConnectionType = "";
            if (JobDuties == null)
                JobDuties = "";
            if (WorkStationOS == null)
                WorkStationOS = "";
            if (ComputerName == null)
                ComputerName = "";
        }

        public string RequestStatus { get; set; }
    }
}
