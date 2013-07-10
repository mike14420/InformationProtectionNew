using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IpModelData;

namespace IpDataProvider
{
    public class ErrorDbAccess
    {
        String ConnectionString { get; set; }
        public ErrorDbAccess(String DataConnectionStr)
        {
            ConnectionString = DataConnectionStr;
        }

        public int Create(Error data)
        {
            int retVal = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertError";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@ErrorId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@IpAddress", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = data.IpAddress;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@UserId", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = data.UserId;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@UserName", SqlDbType.VarChar);
                p4.Direction = ParameterDirection.Input;
                p4.Value = data.UserName;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@TimeStamp", SqlDbType.DateTime);
                p5.Direction = ParameterDirection.Input;
                p5.Value = DateTime.Now.Date; ;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@Url", SqlDbType.VarChar);
                p6.Direction = ParameterDirection.Input;
                p6.Value = data.Url;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@HelpLink", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                p7.Value = data.HelpLink;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@Source", SqlDbType.VarChar);
                p8.Direction = ParameterDirection.Input;
                p8.Value = data.Source;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@Message", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                p9.Value = data.Message;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@StackTrace", SqlDbType.VarChar);
                p10.Direction = ParameterDirection.Input;
                p10.Value = data.StackTrace;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@PhysLocatTargetSiteion", SqlDbType.VarChar);
                p11.Direction = ParameterDirection.Input;
                p11.Value = data.PhysLocatTargetSiteion;
                cmd.Parameters.Add(p11);

                //// EXECUTE THE SP
                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    retVal = (int)p1.Value;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retVal;
        }
    }
}
