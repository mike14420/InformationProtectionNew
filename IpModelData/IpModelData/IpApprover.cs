using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpModelData
{
    public class IpApprover
    {
        public int IpApproverId { get; set; }
        public String Name { get; set; }
        public String Title { get; set; }
        public String EmailAddress { get; set; }
        public String ApproverLevel { get; set; } // vphr, rhcfo, ipd, firstsup

        public const string VPHR = "vphr";
        public const int VPHRID = 4;

        public const string RHCFO = "rhcfo";
        public const int RHCFOID = 3;

        public const string IPD = "ipd";
        public const int IPDID = 2;

        public const string FIRSTSUP = "firstsup";
        public const int FIRSTSUPID = 1;

        public enum ApproveState { Pending, Approved, Rejected };

    }
}
