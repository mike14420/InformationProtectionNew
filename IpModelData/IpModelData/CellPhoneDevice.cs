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

        public String EmployeeSignature { get; set; }
        public String Model { get; set; }
        public String Make { get; set; }
        public String BusJustification { get; set; }

        public String RenownOwnedType { get; set; }
        public String RenownOwnedCarrier { get; set; }
        
        public String RenownOwnedPhone { get; set; }        

        public bool SeccurityAck1 { get; set; }
        public bool SeccurityAck2 { get; set; }
        public bool SeccurityAck3 { get; set; }
        public bool SeccurityAck4 { get; set; }
        public bool SeccurityAck5 { get; set; }
        public bool SeccurityAck6 { get; set; }

        public int RequestorId { get; set; }
       

        public bool Returned { get; set; }
        public bool Archived { get; set; }

    }
}
