using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class CellPhoneSyncDevice
    {
        public int CellPhoneSyncDeviceId { get; set; }
        public int RequestorId { get; set; }

        public String EmployeeSignature { get; set; }
        public String Model { get; set; }
        public String Make { get; set; }                ///5
        public String BusJustification { get; set; }
        public String PersonOwnedType { get; set; } // new or existing

        public String Carrier { get; set; }
        public String SerialNumber { get; set; }
        public String PhoneNumber { get; set; }   //10
        public String MobileOS { get; set; }

        public bool cb_sync1 { get; set; }
        public bool cb_sync2 { get; set; }
        public bool cb_sync3 { get; set; }

        public bool SecuredAck1 { get; set; }     //15
        public bool SecuredAck2 { get; set; }
        public bool SecuredAck3 { get; set; }
        public bool SecuredAck4 { get; set; }
        public bool SecuredAck5 { get; set; }
        public bool SecuredAck6 { get; set; }       //20


    }
}