using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class CellPhoneDevice
    {
        public int CellPhoneDeviceId { get; set; }
        public int RequestorId { get; set; }

        public String EmployeeSignature { get; set; }
        public String Model { get; set; }
        public String Make { get; set; }
        public String RenownOwnedType { get; set; }
        public String RenownOwnedCarrier { get; set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        public String RenownOwnedPhone { get; set; } 
        public String BusJustification { get; set; }

        public bool SecuredAck1 { get; set; }
        public bool SecuredAck2 { get; set; }
        public bool SecuredAck3 { get; set; }
        public bool SecuredAck4 { get; set; }
        public bool SecuredAck5 { get; set; }
        public bool SecuredAck6 { get; set; }

       
    }
}
