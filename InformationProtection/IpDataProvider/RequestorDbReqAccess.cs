using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpModelData;
using System.Linq;

namespace IpDataProvider
{
    public class RequestorDbReqAccess
    {
        String ConnectionString { get; set; }
        public RequestorDbReqAccess(String ourConnectionString)
        {
            ConnectionString = ourConnectionString;
        }

        public int Create(IpRequestor data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertRequestor";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int returnValue = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@IpRequestorId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@EmpID", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = data.EmpID;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@Fname", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = data.Fname;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Lname", SqlDbType.VarChar);
                p4.Direction = ParameterDirection.Input;
                p4.Value = data.Lname;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@Mname", SqlDbType.VarChar);
                p5.Direction = ParameterDirection.Input;
                if (data.Mname == null || data.Mname.Length == 0)
                {
                    data.Mname = String.Empty;
                }
                p5.Value = data.Mname;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@Email", SqlDbType.VarChar);
                p6.Direction = ParameterDirection.Input;
                p6.Value = data.Email;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@JobTitle", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                p7.Value = data.JobTitle;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@DeptName", SqlDbType.VarChar);
                p8.Direction = ParameterDirection.Input;
                p8.Value = data.DeptName;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@DeptID", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                p9.Value = data.DeptID;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@PhoneNumber", SqlDbType.VarChar);
                p10.Direction = ParameterDirection.Input;
                p10.Value = data.PhoneNumber;
                cmd.Parameters.Add(p10);

                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    returnValue = (int)p1.Value;
                }

            }
            catch (Exception e)
            {
                String Message = e.Message;
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


        public int Update(IpRequestor data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateRequestor";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int returnValue = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@IpRequestorId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = data.IpRequestorId;
                cmd.Parameters.Add(p1);

                //SqlParameter p2 = new SqlParameter("@EmpID", SqlDbType.VarChar);
                //p2.Direction = ParameterDirection.Input;
                //p2.Value = data.EmpID;
                //cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@Fname", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = data.Fname;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Lname", SqlDbType.VarChar);
                p4.Direction = ParameterDirection.Input;
                p4.Value = data.Lname;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@Mname", SqlDbType.VarChar);
                p5.Direction = ParameterDirection.Input;
                if (data.Mname == null || data.Mname.Length == 0)
                {
                    data.Mname = String.Empty;
                }
                p5.Value = data.Mname;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@Email", SqlDbType.VarChar);
                p6.Direction = ParameterDirection.Input;
                p6.Value = data.Email;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@JobTitle", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                p7.Value = data.JobTitle;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@DeptName", SqlDbType.VarChar);
                p8.Direction = ParameterDirection.Input;
                p8.Value = data.DeptName;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@DeptID", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                p9.Value = data.DeptID;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@PhoneNumber", SqlDbType.VarChar);
                p10.Direction = ParameterDirection.Input;
                p10.Value = data.PhoneNumber;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@RetVal", SqlDbType.Int);
                p11.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p11);

                cmd.ExecuteScalar();
                if (p11 != null && p11.Value != null)
                {
                    returnValue = (int)p11.Value;
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


        public List<IpRequestor> GetRequestorsIncludeRoles()
        {
            List<IpRequestor> retData = new List<IpRequestor>();

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetAllRequestorsIncludingRoles";

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
                    retData = ReadDataWithRoleInfo(cmd);
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
        

        public List<IpRequestor> GetRequestors()
        {
            List<IpRequestor> retData = new List<IpRequestor>();

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRequestors";

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
                    retData = ReadDataWithRoleInfo(cmd);
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

        public List<IpRequestor> GetRequestorsInRole(String RoleName)
        {
            List<IpRequestor> retData = new List<IpRequestor>();

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRequestorsInRole";

            SqlParameter p1 = new SqlParameter("@RoleName", SqlDbType.VarChar);
            p1.Direction = ParameterDirection.Input;
            // should be requestor id
            p1.Value = RoleName;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@RetVal", SqlDbType.Int);
            p2.Direction = ParameterDirection.Output;
            // should be requestor id
            cmd.Parameters.Add(p2);

            try
            {
                con.Open();
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    retData = ReadDataWithRoleInfo(cmd);
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
        public IpRequestor GetRequestor(int RequestorID)
        {
            List<IpRequestor> retData = new List<IpRequestor>();

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetAllRequestorIncludeRoles";

            SqlParameter p1 = new SqlParameter("@RequestorId", SqlDbType.Int);
            p1.Direction = ParameterDirection.Input;
            // should be requestor id
            p1.Value = RequestorID;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@RetVal", SqlDbType.Int);
            p2.Direction = ParameterDirection.Output;
            // should be requestor id
            cmd.Parameters.Add(p2);

            try
            {
                con.Open();
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    retData = ReadDataWithRoleInfo(cmd);
                }
            }
            catch (Exception error)
            {
            }
            finally
            {
                con.Close();

            }
            if (retData.Count > 0)
            {
                return retData[0];
            }
            else
            {
                return null;
            }
        }

        public List<IpRequestor> GetRequestorByEmpID(String EmpID)
        {

           List<IpRequestor> retData = GetRequestorsIncludeRoles();
           List<IpRequestor> requestor = null;
           if (retData != null && retData.Count > 0)
           {
               requestor = (from item in retData
                                              where item.EmpID == EmpID
                                              select item).ToList();
           }
           return requestor;
        }

        public List<IpRequestor> GetRequestorsIncludingRoles(string RequestorId)
        {
            List<IpRequestor> retData = new List<IpRequestor>();

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRequestorRoles";

            SqlParameter p1 = new SqlParameter("@RequestorId", SqlDbType.Int);
            p1.Direction = ParameterDirection.Input;
            // should be requestor id
            p1.Value = RequestorId;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@RetVal", SqlDbType.Int);
            p2.Direction = ParameterDirection.Output;
            // should be requestor id
            cmd.Parameters.Add(p2);

            try
            {
                con.Open();
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    retData = ReadDataWithRoleInfo(cmd);
                }
            }
            catch (Exception error)
            {
            }
            finally
            {
                con.Close();

            }
            if (retData.Count > 0)
            {
                return retData;
            }
            else
            {
                return null;
            }
        }

        //private List<IpRequestor> ReadData(SqlCommand cmd)
        //{
        //    List<IpRequestor> Requestors = new List<IpRequestor>();
        //    SqlDataReader reader = null;
        //    try
        //    {
        //        reader = cmd.ExecuteReader();
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        return null;
        //    }
        //    catch (System.InvalidOperationException)
        //    {
        //        return null;
        //    }

        //    try
        //    {
        //        while (reader.Read())
        //        {
        //            IpRequestor requestor = new IpRequestor();
        //            requestor.IpRequestorId = (int)reader["IpRequestorId"];

        //            if (reader["EmpID"] != DBNull.Value)
        //            {
        //                requestor.EmpID = ((String)reader["EmpID"]).Trim().ToUpper();
        //            }
        //            else
        //            {
        //                requestor.EmpID = String.Empty;
        //            }

        //            if (reader["Fname"] != DBNull.Value)
        //            {
        //                requestor.Fname = (String)reader["Fname"];
        //            }
        //            else
        //            {
        //                requestor.Fname = String.Empty;
        //            }

        //            if (reader["Lname"] != DBNull.Value)
        //            {
        //                requestor.Lname = (String)reader["Lname"];
        //            }
        //            else
        //            {
        //                requestor.Lname = String.Empty;
        //            }

        //            if (reader["Mname"] != DBNull.Value)
        //            {
        //                requestor.Mname = (String)reader["Mname"];
        //            }
        //            else
        //            {
        //                requestor.Mname = String.Empty;
        //            }

        //            if (reader["Email"] != DBNull.Value)
        //            {
        //                requestor.Email = (String)reader["Email"];
        //            }
        //            else
        //            {
        //                requestor.Email = String.Empty;
        //            }

        //            if (reader["JobTitle"] != DBNull.Value)
        //            {
        //                requestor.JobTitle = (String)reader["JobTitle"];
        //            }
        //            else
        //            {
        //                requestor.JobTitle = String.Empty;
        //            }

        //            if (reader["DeptName"] != DBNull.Value)
        //            {
        //                requestor.DeptName = (String)reader["DeptName"];
        //            }
        //            else
        //            {
        //                requestor.DeptName = String.Empty;
        //            }

        //            if (reader["DeptID"] != DBNull.Value)
        //            {
        //                requestor.DeptID = (String)reader["DeptID"];
        //            }
        //            else
        //            {
        //                requestor.DeptID = String.Empty;
        //            }

        //            if (reader["PhoneNumber"] != DBNull.Value)
        //            {
        //                requestor.PhoneNumber = (String)reader["PhoneNumber"];
        //            }
        //            else
        //            {
        //                requestor.PhoneNumber = String.Empty;
        //            }

        //            if (reader["PhoneNumber"] != DBNull.Value)
        //            {
        //                requestor.PhoneNumber = (String)reader["PhoneNumber"];
        //            }
        //            else
        //            {
        //                requestor.PhoneNumber = String.Empty;
        //            }

        //            Requestors.Add(requestor);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        String message = ex.Message;
        //    }



        //    reader.Close();
        //    return Requestors;
        //}


        private List<IpRequestor> ReadDataWithRoleInfo(SqlCommand cmd)
        {
            List<IpRequestor> Requestors = new List<IpRequestor>();
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

            try
            {
                while (reader.Read())
                {
                    IpRequestor requestor = new IpRequestor();
                    requestor.IpRequestorId = (int)reader["IpRequestorId"];

                    if (reader["EmpID"] != DBNull.Value)
                    {
                        requestor.EmpID = ((String)reader["EmpID"]).Trim().ToUpper();
                    }
                    else
                    {
                        requestor.EmpID = String.Empty;
                    }

                    if (reader["Fname"] != DBNull.Value)
                    {
                        requestor.Fname = (String)reader["Fname"];
                    }
                    else
                    {
                        requestor.Fname = String.Empty;
                    }

                    if (reader["Lname"] != DBNull.Value)
                    {
                        requestor.Lname = (String)reader["Lname"];
                    }
                    else
                    {
                        requestor.Lname = String.Empty;
                    }

                    if (reader["Mname"] != DBNull.Value)
                    {
                        requestor.Mname = (String)reader["Mname"];
                    }
                    else
                    {
                        requestor.Mname = String.Empty;
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        requestor.Email = (String)reader["Email"];
                    }
                    else
                    {
                        requestor.Email = String.Empty;
                    }

                    if (reader["JobTitle"] != DBNull.Value)
                    {
                        requestor.JobTitle = (String)reader["JobTitle"];
                    }
                    else
                    {
                        requestor.JobTitle = String.Empty;
                    }

                    if (reader["DeptName"] != DBNull.Value)
                    {
                        requestor.DeptName = (String)reader["DeptName"];
                    }
                    else
                    {
                        requestor.DeptName = String.Empty;
                    }

                    if (reader["DeptID"] != DBNull.Value)
                    {
                        requestor.DeptID = (String)reader["DeptID"];
                    }
                    else
                    {
                        requestor.DeptID = String.Empty;
                    }

                    if (reader["PhoneNumber"] != DBNull.Value)
                    {
                        requestor.PhoneNumber = (String)reader["PhoneNumber"];
                    }
                    else
                    {
                        requestor.PhoneNumber = String.Empty;
                    }

                    if (reader["RoleName"] != DBNull.Value)
                    {
                        requestor.RoleName = (String)reader["RoleName"];
                    }
                    else
                    {
                        requestor.RoleName = String.Empty;
                    }

                    if (reader["RoleId"] != DBNull.Value)
                    {
                        requestor.RoleId = (string)reader["RoleId"].ToString();
                    }
                    else
                    {
                        requestor.RoleId = String.Empty;
                    }
                    Requestors.Add(requestor);
                }
            }
            catch (Exception ex)
            {
                String message = ex.Message;
            }



            reader.Close();
            return Requestors;
        }


    }
}
