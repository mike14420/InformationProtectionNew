using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class CdBurnerDevice
    {
        public int CdburnerDeviceId { get; set; }
        public int RequestorId { get; set; }

        public String BusJustType { get; set; }
        public enum DeeviceTypeEnum { CdBurnerOnly, CdDvdBurner }

        public String BusJustification { get; set; }
        public String SecurityControls { get; set; }
        public String TypeOfData { get; set; }
        public String SerialNumber { get; set; }
        public String Access2Computer { get; set; }
        public String Access2Media { get; set; }
        public String ComputerLocation { get; set; }
        public String MediaStorLocation { get; set; }
        public bool IsMediaAttached { get; set; }

        public String EmployeeSignature { get; set; }


    }
}
