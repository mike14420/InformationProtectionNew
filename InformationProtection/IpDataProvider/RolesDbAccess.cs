using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IpModelData;

namespace IpDataProvider
{
    public class RolesDbAccess
    {        
        String ConnectionString { get; set; }
        public RolesDbAccess(String ourConnectionString)
        {
            ConnectionString = ourConnectionString;
        }
        public List<Roles> GetRoles()
        {
            List<Roles> retData = new List<Roles>();

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRoles";

            SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            // should be requestor id
            cmd.Parameters.Add(p1);

            try
            {
                con.Open();
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    retData = ReadData(cmd);
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                con.Close();

            }
            return retData;
        }
        private List<Roles> ReadData(SqlCommand cmd)
        {
            List<Roles> ourRoles = new List<Roles>();
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return null;
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }

            while (reader.Read())
            {
                Roles role = new Roles();

                if (reader["ApplicationId"] != DBNull.Value)
                {
                    role.ApplicationId = ((String)reader["ApplicationId"]);
                }
                else
                {
                    role.ApplicationId = String.Empty;
                }

                if (reader["RoleId"] != DBNull.Value)
                {
                    role.RoleId = (String)reader["RoleId"];
                }
                else
                {
                    role.RoleId = String.Empty;
                }

                if (reader["RoleName"] != DBNull.Value)
                {
                    role.RoleName = (String)reader["RoleName"];
                }
                else
                {
                    role.RoleName = String.Empty;
                }

                if (reader["LoweredRoleName"] != DBNull.Value)
                {
                    role.LoweredRoleName = (String)reader["LoweredRoleName"];
                }
                else
                {
                    role.LoweredRoleName = String.Empty;
                }

                if (reader["Description"] != DBNull.Value)
                {
                    role.Description = (String)reader["Email"];
                }
                else
                {
                    role.Description = String.Empty;
                }

                ourRoles.Add(role);
            }
            reader.Close();
            return ourRoles;
        }
    }
}
