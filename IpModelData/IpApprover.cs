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
        public int    EmpID { get; set; }
        public String Title { get; set; }
        public String EmailAddress { get; set; }
        public String ApproverLevel { get; set; } // vphr, rhcfo, ipd, cio, firstsup, secondsup

        public const string FIRSTSUP = "firstsup";
        public const int FIRSTSUPID = 1;

        public const string SECONDSUP = "secondsup";
        public const int SECONDSUPID = 2;

        public const string VPHR = "vphr";
        public const int VPHRID = 19012; // Michelle sanchez bickley

        public const string RHCFO = "rhcfo";
        public const int RHCFOID = 16905;// Dawn Ahner

        public const string IPD = "ipd";
        public const int IPDID = 23433;// Stephanie Berrington

        public const string CIO = "cio";
        public const int CIOID =19355;  // Chuck Scully

        public enum ApproveState { not_submitted, pending, approved, rejected, saved };

    }
}
