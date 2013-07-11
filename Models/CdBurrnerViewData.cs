using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IpRequestMvc2.Validators;

namespace InformationProtection.Models
{
    public class CdBurrnerViewData
    {
        public CdBurrnerViewData()
        {
            Initialize();
        }
        public int    CdburnerDeviceId { get; set; }
        public int    RequestorId { get; set; }

        [Display(Name = "Device Type: CD or CD/DVD Writer")]
        public String BusJustType { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name="Bussiness Justification")]
        public String BusJustification { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Security Controls")]
        public String SecurityControls { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Type of Data:")]
        public String TypeOfData { get; set; }

        [StringLength(20, MinimumLength = 8)]
        [Required]
        [Display(Name = "Serial Number")]
        public String SerialNumber { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Who Has Access To Computer")]
        public String Access2Computer { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Who Has Access To Media")]
        public String Access2Media { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Location of Computer")]
        public String ComputerLocation { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Location of Media Storage")]
        public String MediaStorLocation { get; set; }

        [BooleanRequiredToBeTrue]
        [Display(Name = "Is Media Attached")]
        public bool IsMediaAttached { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Employee Signature")]
        public String EmployeeSignature { get; set; }


        public string RequestDetailsLink
        {
            get;
            set;

        }

        public string RequestEditLink { get; set; }

        internal void Initialize()
        {
            CdburnerDeviceId = 0;
            RequestorId = 0;

            BusJustType = "";
            BusJustification = "";
            SecurityControls =  "";
            TypeOfData = "";
            SerialNumber = "";
            Access2Computer = "";
            Access2Media = "";
            ComputerLocation = "";
            MediaStorLocation = "";
            IsMediaAttached = false;
            EmployeeSignature = "";

        }

        internal void SaveInitialize()
        {
            if (BusJustType == null)
                BusJustType = "";
            if (BusJustification == null)
                BusJustification = "";
            if (SecurityControls == null)
                SecurityControls = "";
            if (TypeOfData == null)
                TypeOfData = "";
            if (SerialNumber == null)
                SerialNumber = "";
            if (Access2Computer == null)
                Access2Computer = "";
            if (Access2Media == null)
                Access2Media = "";
            if (ComputerLocation == null)
                ComputerLocation = "";
            if (MediaStorLocation == null)
                MediaStorLocation = "";
            if (EmployeeSignature == null)
                EmployeeSignature = "";

        }
    }
}