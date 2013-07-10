using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IpModelData;

namespace IpDataProvider
{
    public class ApproversDbAcess
    {
        String patientConnectionStr;

        public ApproversDbAcess(String DataConnectionStr)
        {
            patientConnectionStr = DataConnectionStr;
        }


        public List<IpApprover> GetAllByRole(String role)
        {
            List<IpApprover> retData = new List<IpApprover>();

            SqlConnection con = new SqlConnection(patientConnectionStr);

            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetApprovers";

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
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }

            return retData;
        }

        private List<IpApprover> ReadData(SqlCommand cmd)
        {
            SqlParameter result;
            int retValue = 0;
            List<IpApprover> approvers = new List<IpApprover>();
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
                IpApprover approver = new IpApprover();
                approver.IpApproverId = (int)reader["IpApproverId"];
                if (reader["Name"] != DBNull.Value)
                {
                    approver.Name = ((String)reader["Name"]).Trim().ToUpper();
                }
                else
                {
                    approver.Name = String.Empty;
                }
                approver.EmpID = (int)reader["EmpID"];

                if (reader["Title"] != DBNull.Value)
                {
                    approver.Title = (String)reader["Title"];
                }
                else
                {
                    approver.Title = String.Empty;
                }

                if (reader["EmailAddress"] != DBNull.Value)
                {
                    approver.EmailAddress = (String)reader["EmailAddress"];
                }
                else
                {
                    approver.EmailAddress = String.Empty;
                }

                if (reader["ApproverLevel"] != DBNull.Value)
                {
                    approver.ApproverLevel = (String)reader["ApproverLevel"];
                }
                else
                {
                    approver.ApproverLevel = String.Empty;
                }
                approvers.Add(approver);
            }
            reader.Close();
            return approvers;
        }
    }
}
