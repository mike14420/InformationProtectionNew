using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpModelData
{
    public class Roles
    {
        public String ApplicationId { get; set; }
        public String RoleId { get; set; }
        public String RoleName { get; set; }
        public String LoweredRoleName { get; set; }
        public String Description { get; set; }

        public enum RoleNameEnum { nonuser, user, approver, admin }
    }
}
