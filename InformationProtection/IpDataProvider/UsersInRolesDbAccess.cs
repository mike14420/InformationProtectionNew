using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IpModelData;

namespace IpDataProvider
{
    public class UsersInRolesDbAccess
    {
        String ConnectionString { get; set; }
        public UsersInRolesDbAccess(String ourConnectionString)
        {
            ConnectionString = ourConnectionString;
        }

        public int RemoveRollFromRequestor(int RequestorId, string RoleName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RemoveUserInRole";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int returnValue = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = RequestorId;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@RoleName", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = RoleName;
                cmd.Parameters.Add(p3);

                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    returnValue = (int)p1.Value;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return returnValue;
        }

        public int CreateUserInRole(int RequestorId, String RoleName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertUserInRole";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int returnValue = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = RequestorId;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@RoleName", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = RoleName;
                cmd.Parameters.Add(p3);

                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    returnValue = (int)p1.Value;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return returnValue;
        }

        public List<UsersInRoles> GetUsersRoles()
        {
            List<UsersInRoles> retData = new List<UsersInRoles>();

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetUsersRoles";

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
            }
            finally
            {
                con.Close();

            }
            return retData;
        }

        private List<UsersInRoles> ReadData(SqlCommand cmd)
        {
            List<UsersInRoles> usersInRoles = new List<UsersInRoles>();
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
                UsersInRoles usersInRole = new UsersInRoles();
                usersInRole.RequestorId = (int)reader["RequestorId"];

                if (reader["RoleId"] != DBNull.Value)
                {
                    usersInRole.RoleId = ((String)reader["RoleId"]);
                }
                else
                {
                    usersInRole.RoleId = String.Empty;
                }


                if (reader["RoleName"] != DBNull.Value)
                {
                    usersInRole.RoleName = (String)reader["RoleName"];
                }
                else
                {
                    usersInRole.RoleName = String.Empty;
                }

                if (reader["LoweredRoleName"] != DBNull.Value)
                {
                    usersInRole.LoweredRoleName = (String)reader["LoweredRoleName"];
                }
                else
                {
                    usersInRole.LoweredRoleName = String.Empty;
                }

                if (reader["Description"] != DBNull.Value)
                {
                    usersInRole.Description = (String)reader["Description"];
                }
                else
                {
                    usersInRole.Description = String.Empty;
                }

                usersInRoles.Add(usersInRole);
            }
            reader.Close();
            return usersInRoles;
        }


    }
}
