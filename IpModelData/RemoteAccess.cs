using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class RemoteAccess
    {
        public int RemoteAccessId { get; set; }
        public int RequestorId { get; set; }
        public String EmployeeSignature { get; set; }
        public String IpAddressAndHostName { get; set; }
        public String AppNames { get; set; }                        /// 5
        public String LanShares { get; set; }
        public String ComputerName { get; set; }
        public String RemoteConnectionType { get; set; }
        public enum RemoteConnectionTypeEnum { Cable, DSL, DialUp }
        public String JobDuties { get; set; }                       /// --- 10
        public String WorkStationOS { get; set; }

        public bool AccessToServer { get; set; }
        public bool AccessToApp { get; set; }
        public bool AccessToLanShares { get; set; }
        public bool AccessToMyWorkStation { get; set; }                 /// 15 
        public bool ConnectFromHome { get; set; }
        public bool SecuredAck1 { get; set; } // I am responsible for
        public bool SecuredAck2 { get; set; } // current patches
        public bool SecuredAck3 { get; set; } // current anti-virus
        public bool SecuredAck4 { get; set; } // Personal firewall    -- 20
        public bool SecuredAck5 { get; set; } // Final Verify     
        public bool NonExemptEmployee { get; set; }

    }
}
