using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class IpApprovalRequest
    {
        public int IpApprovalRequestId { get; set; }

        public int IpRequestorId { get; set; }

        public DateTime SubmitDate { get; set; }
        public DateTime GrantDate { get; set; }
        public DateTime ReturnDate { get; set; }

        // The approval Chain
        // 1. First Leader
        public int      FirstSupEmpId { get; set; } // EmpID
        public String   FirstSupName { get; set; }
        public DateTime FirstSupApprovalDate { get; set; }
        public string   FirstSupApproval { get; set; }
        public string   FirstSupComment { get; set; }
        public string   FirstSupEmail { get; set; }

        // 2. Second Leader
        // assume for now that the 2nd sup is the sup of the 1st
        public int      SecondSupEmpId { get; set; } // EmpID
        public String   SecondSupName { get; set; }
        public DateTime SecondSupApprovalDate { get; set; }
        public string   SecondSupApproval { get; set; }
        public string   SecondSupComment { get; set; }
        public string   SecondSupEmail { get; set; }
        // 3. VPHR - Michelle Sanchez-Bickley
        public int      VpHrApproverEmpId { get; set; } // EmpID
        public String   VpHrName { get; set; }
        public DateTime VpHrApprovalDate { get; set; }
        public string   VpHrApproval { get; set; }
        public string   VpHrComment { get; set; }
        public string   VphrEmail { get; set; }
        // 4. RHCFO - Dawn Abner
        public int      RhCfoApproverEmpId { get; set; } // EmpID
        public String   RhCfoName { get; set; }
        public DateTime RhCfoApprovalDate { get; set; }
        public string   RhCfoApproval { get; set; }
        public string   RhCfoComment { get; set; }
        public string   RhCfoEmail { get; set; }
        // 5. Renown CIO (Information Protection Director) Stephanie
        public int      IpdApproverEmpId { get; set; } // EmpID
        public String   IpdName { get; set; }
        public DateTime IpdApprovalDate { get; set; }
        public string   IpdApproval { get; set; }
        public string   IpdComment { get; set; }
        public string   IpdEmail { get; set; }
        // 5. Renown CIO (Information Protection Director) Chuck Skully
        public int      CioApproverEmpId { get; set; } // EmpID
        public String   CioName { get; set; }
        public DateTime CioApprovalDate { get; set; }
        public string   CioApproval { get; set; }
        public string   CioComment { get; set; }
        public string   CioEmail { get; set; }
      
        public enum RequestTypeEnum { cdburrner, cellphonesync, cellphone, laptop, remoteaccess, usb, wireless }
        public String RequestType { get; set; } // identifies one of the following

        public int CellPhoneDeviceId { get; set; }
        public int CdburnerDeviceID { get; set; }
        public int CellPhoneSyncDeviceID { get; set; }
        public int UsbDeviceID { get; set; }
        public int LapTopID { get; set; }
        public int RemoteAccessID { get; set; }
        public int WirelessDeviceID { get; set; }

        public bool Archive { get; set; }


    }
}
