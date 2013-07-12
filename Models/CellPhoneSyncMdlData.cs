using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using IpRequestMvc2.Validators;

namespace InformationProtection.Models
{
    public class CellPhoneSyncMdlData
    {
        public CellPhoneSyncMdlData()
        {
            Initialize();
        }
        public int CellPhoneSyncDeviceId { get; set; }
        public int RequestorId { get; set; }

        [Display(Name = "Employee Signature")]
        [StringLength(160, MinimumLength = 5)]
        [Required]
        public String EmployeeSignature { get; set; }

        [StringLength(160, MinimumLength = 2)]
        [Display(Name = "Model")]
        [Required]
        public String Model { get; set; }

        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Make")]
        [Required]
        public String Make { get; set; }

        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Business Justification")]
        [Required]
        public String BusJustification { get; set; }

        [Display(Name = "Personally-Owned")]
        public String PersonOwnedType { get; set; } // new or existing

        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Carrier")]
        [Required]
        public String Carrier { get; set; }

        [StringLength(160, MinimumLength = 8)]
        [Display(Name = "Serial Number")]
        [Required]
        public String SerialNumber { get; set; }

        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Phone Number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public String PhoneNumber { get; set; }

        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Mobile OS")]
        [Required]
        public String MobileOS { get; set; }

        [Display(Name = "Syncing to Outlook Calendar")]
        public bool cb_sync1 { get; set; }
        [Display(Name = "Syncing to Outlook Contacts")]
        public bool cb_sync2 { get; set; }
        [Display(Name = "Syncing to Outlook E-Mail")]
        public bool cb_sync3 { get; set; }


         [BooleanRequiredToBeTrue]
        public bool SecuredAck1 { get; set; }

         [BooleanRequiredToBeTrue]
        public bool SecuredAck2 { get; set; }

         [BooleanRequiredToBeTrue]
        public bool SecuredAck3 { get; set; }

         [BooleanRequiredToBeTrue]
         [Display(Name = "Logical security")]
        public bool SecuredAck4 { get; set; }

         [BooleanRequiredToBeTrue]
         [Display(Name = "Physical security")]
        public bool SecuredAck5 { get; set; }

         [BooleanRequiredToBeTrue]
         [Display(Name = "Immediate Notification: loss/theft")]
        public bool SecuredAck6 { get; set; }

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

        public String RequestDetailsLink
        {
            get;
            set;
        }

        public bool NonExemptEmployee { get; set; }

        public string RequestEditLink { get; set; }

        public void Initialize()
        {
            CellPhoneSyncDeviceId = 0;
            RequestorId = 0;

            EmployeeSignature = "";
            Model = "";
            Make = "";
            BusJustification = "";
            PersonOwnedType = "";
            Carrier = "";
            SerialNumber = "";
            PhoneNumber = "";
            MobileOS = "";

            SecuredAck1 = false;
            SecuredAck2 = false;
            SecuredAck3 = false;
            SecuredAck4 = false;
            SecuredAck5 = false;
            SecuredAck6 = false;

            cb_sync1 = false;
            cb_sync2 = false;
            cb_sync3 = false;
        }
        public void SaveInitialize()
        {
            if (EmployeeSignature == null)
                EmployeeSignature = "";
            if (Model == null)
                Model = "";
            if (Make == null)
                Make = "";
            if (BusJustification == null)
                BusJustification = "";
            if (PersonOwnedType == null)
                PersonOwnedType = "";
            if (Carrier == null)
                Carrier = "";
            if (SerialNumber == null)
                SerialNumber = "";
            if (PhoneNumber == null)
                PhoneNumber = "";
            if (MobileOS == null)
                MobileOS = "";
        }

        public string RequestStatus { get; set; }

        public int RequestId { get; set; }

        public string RequestorsName { get; set; }
    }
}
