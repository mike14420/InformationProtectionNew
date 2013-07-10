using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class CdburnerDevice
    {
        public int CdburnerDeviceId { get; set; }
        public String EmployeeSignature { get; set; }
        public IpRequestor Requestor { get; set; }
        public bool Archived { get; set; }
    }
}
