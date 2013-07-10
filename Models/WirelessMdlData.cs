﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using IpRequestMvc2.Validators;

namespace InformationProtection.Models
{
    public class WirelessMdlData
    {
        public int WirelessDeviceId { get; set; }
        public int RequestorId { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Employee Signature")]
        public String EmployeeSignature { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 8)]
        [Display(Name = "Serial Number")]
        public String SerialNumber { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Model")]
        public String Model { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Vendor")]
        public String Vendor { get; set; }

        public string RenownOwnedType { get; set; }// new or existing

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Business Justification")]
        public String BusJustification { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Location of the Wireless Device")]
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
                if (SecurityAck1 && SecurityAck2 && SecurityAck3 && SecurityAck4 && SecurityAck5 && SecurityAck6 && SecurityAck7 && SecurityAck8)
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
                    WirelessDeviceId = 0;
                    RequestorId = 0;

                    BusJustification = "";
                    RenownOwnedType = "";
                    EmployeeSignature = "";
                    Model = "";
                    Vendor = "";
                    PhysLocation = "";
                    SerialNumber = "";

                    SecurityAck1 = false;
                    SecurityAck2 = false;
                    SecurityAck3 = false;
                    SecurityAck4 = false;
                    SecurityAck5 = false;
                    SecurityAck6 = false;
                    SecurityAck7 = false;
                    SecurityAck8 = false;

                    SecuredAck1 = false;
                    SecuredAck2 = false;
                    SecuredAck3 = false;
                    SecuredAck4 = false;
                    SecuredAck5 = false;
                    SecuredAck6 = false;
        }

        public void SaveInitialize()
        {
            if (BusJustification == null)
                BusJustification = "";
            if (RenownOwnedType == null)
                RenownOwnedType = "";
            if (EmployeeSignature == null)
                EmployeeSignature = "";
            if (Model == null)
                Model = "";
            if (Vendor == null)
                Vendor = "";
            if (PhysLocation == null)
                PhysLocation = "";
            if (SerialNumber == null)
                SerialNumber = "";
        }

    }
}