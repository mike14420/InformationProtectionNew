using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class UsbDevice
    {
        public int UsbDeviceId { get; set; }   
        public int RequestorId { get; set; }

        public String EmployeeSignature { get; set; }
        public String Model { get; set; }
        public String SerialNumber { get; set; }   //-- 5
        public String RenownOwned { get; set; } // New or Existing
        public String BusJustification { get; set; }

        public bool SecuredAck1 { get; set; }    // 8
        public bool SecuredAck2 { get; set; }
        public bool SecuredAck3 { get; set; }    //10
        public bool SecuredAck4 { get; set; }
        public bool SecuredAck5 { get; set; }
        public bool SecuredAck6 { get; set; }

        public bool SecurityAck1 { get; set; }
        public bool SecurityAck2 { get; set; }   //15
        public bool SecurityAck3 { get; set; }
        public bool SecurityAck4 { get; set; }
        public bool SecurityAck5 { get; set; }
        public bool SecurityAck6 { get; set; }
        public bool SecurityAck7 { get; set; }  //20
        public bool SecurityAck8 { get; set; }
        public bool SecurityAck9 { get; set; }

    }
}
