using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpModelData
{
    public class Error
    {
        public int ErrorId { get; set; }
        public int UserId { get; set; }
        public String UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public String IpAddress { get; set; }
        public String Url { get; set; }
        public String HelpLink { get; set; }
        public String Source { get; set; }
        public String Message { get; set; }
        public String StackTrace { get; set; }
        public String PhysLocatTargetSiteion { get; set; }
    }
}
