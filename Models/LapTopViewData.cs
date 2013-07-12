using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IpRequestMvc2.Validators;

namespace InformationProtection.Models
{
    public class LapTopViewData
    {
        public LapTopViewData()
        {
            Initialize();
        }
        public int LapTopDeviceId { get; set; }
        public int RequestorId { get; set; }

        [Required]
        [Display(Name = "Employee Signature")]
        [StringLength(160, MinimumLength = 3)]
        public String EmployeeSignature { get; set; }

        [StringLength(160, MinimumLength = 3)]
        [Required]
        public String Model { get; set; }

        [StringLength(160, MinimumLength = 8)]
        [Required]
        [Display(Name="Serial Number")]
        public String SerialNumber { get; set; }

         [Display(Name = "Business Type")]
        public String BusJustType { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Business Justification")]
        public String BusJustification { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        [Display(Name = "Physical Location")]
        public String PhysLocation { get; set; }

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
        public String RequestDetailsLink
        {
            get;
            set;
        }

        public string RequestEditLink { get; set; }

        public void Initialize()
        {
            LapTopDeviceId = 0;
            RequestorId = 0;

            EmployeeSignature = "";
            Model = "";
            SerialNumber = "";
            BusJustification = "";
            BusJustType = "";
            PhysLocation = "";

            SecurityAck1 = false;
            SecurityAck2 = false;
            SecurityAck3 = false;
            SecurityAck4 = false;
            SecurityAck5 = false;
            SecurityAck6 = false;
            SecurityAck7 = false;
            SecurityAck8 = false;
            SecurityAck9 = false;

            SecuredAck1 = false;
            SecuredAck2 = false;
            SecuredAck3 = false;
            SecuredAck4 = false;
            SecuredAck5 = false;
            SecuredAck6 = false;
        }
        public void SaveInitialize()
        {
            if (EmployeeSignature == null)  
                EmployeeSignature = "";
            if (Model == null)  
                Model = "";
            if (SerialNumber == null) 
                SerialNumber = "";
            if (BusJustification == null) 
                BusJustification = "";
            if (BusJustType == null) 
                BusJustType = "";
            if (PhysLocation == null) 
                PhysLocation = "";
        }

        public string RequestStatus { get; set; }
    }
}