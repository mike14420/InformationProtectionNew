using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IpModelData;

namespace IpDataProvider
{
    public class CdBurnerReqDbAccess
    {
        String ConnectionString { get; set; }

        public CdBurnerReqDbAccess(String DataConnectionStr)
        {
            ConnectionString = DataConnectionStr;
        }

        public int Create(CdBurnerDevice data)
        {   
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertCdBurnerReq";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int returnValue = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@CdburnerDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                p2.Value = data.RequestorId;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@BusJustType", SqlDbType.VarChar);
                p3.Value = data.BusJustType;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p4.Value = data.BusJustification;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@SecurityControls", SqlDbType.VarChar);
                p5.Value = data.SecurityControls;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@TypeOfData", SqlDbType.VarChar);
                p6.Value = data.TypeOfData;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                p7.Value = data.SerialNumber;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@Access2Computer", SqlDbType.VarChar);
                p8.Value = data.Access2Computer;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@Access2Media", SqlDbType.VarChar);
                p9.Value = data.Access2Media;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@ComputerLocation", SqlDbType.VarChar);
                p10.Value = data.ComputerLocation;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@MediaStorLocation", SqlDbType.VarChar);
                p11.Value = data.MediaStorLocation;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@IsMediaAttached", SqlDbType.Bit);
                p12.Value = data.IsMediaAttached;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p13.Value = data.EmployeeSignature;
                cmd.Parameters.Add(p13);

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


        public bool Update(CdBurnerDevice data)
        {   
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateCdBurnerDevice";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int returnValue = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@CdburnerDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = data.CdburnerDeviceId;
                cmd.Parameters.Add(p1);

                //SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                //p2.Value = data.RequestorId;
                //cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@BusJustType", SqlDbType.VarChar);
                p3.Value = data.BusJustType;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p4.Value = data.BusJustification;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@SecurityControls", SqlDbType.VarChar);
                p5.Value = data.SecurityControls;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@TypeOfData", SqlDbType.VarChar);
                p6.Value = data.TypeOfData;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                p7.Value = data.SerialNumber;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@Access2Computer", SqlDbType.VarChar);
                p8.Value = data.Access2Computer;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@Access2Media", SqlDbType.VarChar);
                p9.Value = data.Access2Media;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@ComputerLocation", SqlDbType.VarChar);
                p10.Value = data.ComputerLocation;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@MediaStorLocation", SqlDbType.VarChar);
                p11.Value = data.MediaStorLocation;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@IsMediaAttached", SqlDbType.Bit);
                p12.Value = data.IsMediaAttached;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p13.Value = data.EmployeeSignature;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@Result", SqlDbType.Int);
                p14.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p14);

                cmd.ExecuteScalar();
                if (p14 != null && p14.Value != null)
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

            return returnValue > 0;
        }


        public List<CdBurnerDevice> GetDevice()
        {
            List<CdBurnerDevice> retData = null;

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCdBurnerDevices";

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
        public List<CdBurnerDevice> GetDevicesFor(int RequestorId)
        {
            List<CdBurnerDevice> retData = new List<CdBurnerDevice>();
            CdBurnerDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCdBurnerDevicesFor";
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlParameter p1 = new SqlParameter("@retVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;

            SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
            p2.Direction = ParameterDirection.Input;
            p2.Value = RequestorId;
            cmd.Parameters.Add(p2);

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

        public List<CdBurnerDevice> GetDevices()
        {
            List<CdBurnerDevice> retData = new List<CdBurnerDevice>();
            CdBurnerDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCdBurnerDevices";
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlParameter p1 = new SqlParameter("@retVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;

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
        private List<CdBurnerDevice> ReadData(SqlCommand cmd)
        {
            List<CdBurnerDevice> CeBurnerDevices = new List<CdBurnerDevice>();
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
                CdBurnerDevice cdDevice = new CdBurnerDevice();
                cdDevice.CdburnerDeviceId = (int)reader["CdburnerDeviceId"];
                cdDevice.RequestorId = (int)reader["RequestorId"];
                if (reader["BusJustType"] != DBNull.Value)
                {
                    cdDevice.BusJustType = ((String)reader["BusJustType"]).Trim().ToUpper();
                }
                else
                {
                    cdDevice.BusJustType = String.Empty;
                }

                if (reader["BusJustification"] != DBNull.Value)
                {
                    cdDevice.BusJustification = (String)reader["BusJustification"];
                }
                else
                {
                    cdDevice.BusJustification = String.Empty;
                }

                if (reader["SecurityControls"] != DBNull.Value)
                {
                    cdDevice.SecurityControls = (String)reader["SecurityControls"];
                }
                else
                {
                    cdDevice.SecurityControls = String.Empty;
                }

                if (reader["TypeOfData"] != DBNull.Value)
                {
                    cdDevice.TypeOfData = (String)reader["TypeOfData"];
                }
                else
                {
                    cdDevice.TypeOfData = String.Empty;
                }

                if (reader["SerialNumber"] != DBNull.Value)
                {
                    cdDevice.SerialNumber = (String)reader["SerialNumber"];
                }
                else
                {
                    cdDevice.SerialNumber = String.Empty;
                }

                if (reader["Access2Computer"] != DBNull.Value)
                {
                    cdDevice.Access2Computer = (String)reader["Access2Computer"];
                }
                else
                {
                    cdDevice.Access2Computer = String.Empty;
                }

                if (reader["Access2Media"] != DBNull.Value)
                {
                    cdDevice.Access2Media = (String)reader["Access2Media"];
                }
                else
                {
                    cdDevice.Access2Media = String.Empty;
                }

                if (reader["ComputerLocation"] != DBNull.Value)
                {
                    cdDevice.ComputerLocation = (String)reader["ComputerLocation"];
                }
                else
                {
                    cdDevice.ComputerLocation = String.Empty;
                }

                if (reader["MediaStorLocation"] != DBNull.Value)
                {
                    cdDevice.MediaStorLocation = (String)reader["MediaStorLocation"];
                }
                else
                {
                    cdDevice.MediaStorLocation = String.Empty;
                }
                cdDevice.IsMediaAttached = (bool)reader["IsMediaAttached"];

                if (reader["EmployeeSignature"] != DBNull.Value)
                {
                    cdDevice.EmployeeSignature = (String)reader["EmployeeSignature"];
                }
                else
                {
                    cdDevice.EmployeeSignature = String.Empty;
                }
                CeBurnerDevices.Add(cdDevice);
            }
            reader.Close();
            return CeBurnerDevices;
        }
    }
}
