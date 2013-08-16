using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InformationProtection.Models
{
    public class IpRequestorViewData
    {
        public int IpRequestorId { get; set; }
        [Display(Name = "Employee ID")]
        public String EmpID { get; set; }

        [Display(Name="First Name")]
        public string Fname { get; set; }


        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Display(Name = "Middle Initial")]
        public string Mname { get; set; }

        public string Email { get; set; }

        public string JobTitle { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string DeptName { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string DeptID { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string PhoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", Fname,Lname);
            }
            set
            {
                
            }
        }
        public string RequestDetailsLink
        {
            get;
            set;

        }
        [Display(Name="# Pending Request")]
        public int NumberOfPendingReq
        {
            get;
            set;
        }
        public List<String> RoleName{ get; set; }
        public List<String> RoleId { get; set; }
        [Display(Name = "Roles")]
        public String RequestorsRoles
        {
            get
            {
                StringBuilder reqRoles = new StringBuilder();
                foreach (String r in RoleName)
                {
                    reqRoles.Append(r + ",");
                }
                if (reqRoles.Length > 0)
                {
                    reqRoles.Length -= 1;
                }
                return reqRoles.ToString();
            }

            
        }
        public bool IsAdmin
        {
            get
            {
                if (RoleName != null && RoleName.Count > 0)
                {
                    return RoleName.Contains((IpModelData.Roles.RoleNameEnum.admin.ToString()));
                }
                return false;
            }
        }
        public bool IsApprover
        {
            get
            {
                if (RoleName != null && RoleName.Count > 0)
                {
                    return RoleName.Contains((IpModelData.Roles.RoleNameEnum.approver.ToString()));
                }
                return false;
            }
        }
        public bool IsUser
        {
            get
            {
                if (RoleName != null && RoleName.Count > 0)
                {
                    return RoleName.Contains((IpModelData.Roles.RoleNameEnum.user.ToString()));
                }
                return false;
            }
        }


        internal void UpdateRequestor(string submitButton, IpRequestorViewData requestor)
        {
            throw new NotImplementedException();
        }
    }
}