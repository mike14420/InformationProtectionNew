using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationProtection.Models
{
    public class IpApproverViewData
    {
        public int IpApproverId { get; set; }
        public String Name { get; set; }
        public String Title { get; set; }
        public String EmailAddress { get; set; }
        public int EmpID { get; set; }
        public enum ApproverLevelEnum { firstsup, secondsup, vphr, rhcfo, ipd, cio, other }
        public ApproverLevelEnum ApproverLevel { get; set; } 
    }
}