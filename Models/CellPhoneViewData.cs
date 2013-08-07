using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IpRequestMvc2.Validators;

namespace InformationProtection.Models
{
    public class CellPhoneViewData
    {
        public CellPhoneViewData()
        {
            Initialize();
        }
        public int CellPhoneReqId { get; set; }
        [Required]
        [StringLength(160, MinimumLength = 3)]
        public String EmployeeSignature { get; set; }
        
        [StringLength(160, MinimumLength = 2)]
        [Required]
        public String Model { get; set; }
        
        [StringLength(160, MinimumLength = 3)]
        [Required]
        public String Make { get; set; }
        
        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Business Justification")]
        public String BusJustification { get; set; }

        // new or used
        [Display(Name = "Renowned Owned Type")]
        public String RenownOwnedType { get; set; }
        
        [Required]
        [StringLength(160, MinimumLength = 3)]
        [Display(Name = "Renown Owned Carrier")]
        public String RenownOwnedCarrier { get; set; }
       
        [StringLength(20, MinimumLength = 4)]
        [Display(Name = "Phone Number")]
        [Required]
        public String RenownOwnedPhone { get; set; }
    
        public String radButLstROwned { get; set; }

        [BooleanRequiredToBeTrue]
        [Display(Name = "Secured Question 1")]
        public bool SecuredAck1 { get; set; }
        [Display(Name = "Secured Question 2")]
        [BooleanRequiredToBeTrue]
        public bool SecuredAck2 { get; set; }
        [BooleanRequiredToBeTrue]
        [Display(Name = "Secured Question 3")]
        public bool SecuredAck3 { get; set; }
        [BooleanRequiredToBeTrue]
        [Display(Name = "Secured Question 4")]
        public bool SecuredAck4 { get; set; }
        [BooleanRequiredToBeTrue]
        [Display(Name = "Secured Question 5")]
        public bool SecuredAck5 { get; set; }
        [BooleanRequiredToBeTrue]
        [Display(Name = "Secured Question 6")]
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

        public int IpRequestorId { get; set; }

        public String RequestDetailsLink
        {
            get;
            set;
        }

        public bool Returned { get; set; }
        public bool Archived { get; set; }



        public string RequestEditLink { get; set; }

        public void Initialize()
        {
            CellPhoneReqId = 0;
            IpRequestorId = 0;
            EmployeeSignature = "";
            Model = "";
            Make = "";
            BusJustification = "";
            RenownOwnedCarrier = "";
            RenownOwnedPhone = "";
            RenownOwnedType = "";
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
            if (Make == null)
                Make = "";
            if (BusJustification == null)
                BusJustification = "";
            if (RenownOwnedCarrier == null)
                RenownOwnedCarrier = "";
            if (RenownOwnedPhone == null)
               RenownOwnedPhone = "";
            if (RenownOwnedType == null)
                RenownOwnedType = "";

        }

        public string RequestStatus { get; set; }

        public string RequestorsName { get; set; }

        public int RequestId { get; set; }

        public string RequestReSubmitLink { get; set; }
    }

}