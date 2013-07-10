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
        public String EmployeeSignature { get; set; }

        public IpRequestor Requestor { get; set; }
        public bool Archived { get; set; }
    }
}
