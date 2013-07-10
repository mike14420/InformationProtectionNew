using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class IpRequestor
    {
        public int IpRequestorId { get; set; }

        public String EmpID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Mname { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string DeptName { get; set; }
        public string DeptID { get; set; }
        public string PhoneNumber { get; set; }

        // Request can be approved, pending or rejected
        public List<IpApprovalRequest> IpApprovalRequest { get; set; }
    }
}
