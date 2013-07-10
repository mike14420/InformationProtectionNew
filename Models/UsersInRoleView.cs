using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using IpDataProvider;

namespace InformationProtection.Models
{
    public class UsersInRoleView
    {
        public int CreateUserInRole(int requestorId, string role)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            UsersInRolesDbAccess userInRolesModel = new UsersInRolesDbAccess(connectionString);
            
            int retValue = userInRolesModel.CreateUserInRole(requestorId, role);
            return retValue;
        }

        public int RemoveUserInRole(int requestorId, string role)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            UsersInRolesDbAccess userInRolesModel = new UsersInRolesDbAccess(connectionString);

            int retValue = userInRolesModel.RemoveRollFromRequestor(requestorId, role);
            return retValue;
        }
    }
}