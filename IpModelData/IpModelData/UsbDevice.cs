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

        public String Model { get; set; }
        public String Size { get; set; }
        public String SerialNumber { get; set; }
        public String BusJustType { get; set; }
        public String BusJustification { get; set; }
        public String PhysLocation { get; set; }

        public String Returned { get; set; }

        public int RequestorId { get; set; }
        public IpRequestor Requestor { get; set; }

        // Approved, Pending, Rejected, Returned, Archived
        public String      Status { get; set; }

        public String EmployeeSignature { get; set; }
        public bool Archived { get; set; }
    }
}
