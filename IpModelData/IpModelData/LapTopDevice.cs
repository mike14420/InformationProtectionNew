﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class LapTopDevice
    {
        public int LapTopDeviceId { get; set; }
        public String EmployeeSignature { get; set; }

        public IpRequestor Requestor { get; set; }
        public bool Archived { get; set; }
    }
}
