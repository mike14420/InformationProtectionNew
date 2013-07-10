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
        public IpRequestor Requestor { get; set; }

        public DateTime SubmitDate { get; set; }
        public DateTime GrantDate { get; set; }
        public DateTime ReturnDate { get; set; }

        // The approval Chain
        public int      FirstSupId { get; set; }
        public DateTime FirstSupApprovalDate { get; set; }
        public string   FirstSupApproval { get; set; }
        public string   FirstSupComment { get; set; }

        public int      VpHrApproverId { get; set; }
        public DateTime VpHrApprovalDate { get; set; }
        public string   VpHrApproval { get; set; }
        public string   VpHrComment { get; set; }

        public int      RhCfoApproverId { get; set; }
        public DateTime RhCfoApprovalDate { get; set; }
        public string   RhCfoApproval { get; set; }
        public string   RhCfoComment { get; set; }

        public int      IpdApproverId { get; set; }
        public DateTime IpdApprovalDate { get; set; }
        public string   IpdApproval { get; set; }
        public string   IpdComment { get; set; }

        public String IpRequestType { get; set; } // identifies one of the following
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
