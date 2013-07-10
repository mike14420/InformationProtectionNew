using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class WirelessDevice
    {
        public int WirelessDeviceId { get; set; }
        public int RequestorId { get; set; }

        public String EmployeeSignature { get; set; }
        public String Model { get; set; }
        public String SerialNumber { get; set; }    //--5


        public String Vendor { get; set; }
        public String RenownOwnedType { get; set; }// new or existing
        public String BusJustification { get; set; }
        public String PhysLocation { get; set; }

        public bool SecuredAck1 { get; set; } //--10
        public bool SecuredAck2 { get; set; }
        public bool SecuredAck3 { get; set; }
        public bool SecuredAck4 { get; set; }
        public bool SecuredAck5 { get; set; }
        public bool SecuredAck6 { get; set; } //15

        public bool SecurityAck1 { get; set; }
        public bool SecurityAck2 { get; set; }
        public bool SecurityAck3 { get; set; }
        public bool SecurityAck4 { get; set; }
        public bool SecurityAck5 { get; set; }   //--20
        public bool SecurityAck6 { get; set; }
        public bool SecurityAck7 { get; set; }
        public bool SecurityAck8 { get; set; }

    }
}
