using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IpModelData;

namespace IpDataProvider
{
    public class CellPhoneReqDbAccess
    {
        String ConnectionString { get; set; }

        public CellPhoneReqDbAccess(String DataConnectionStr)
        {
            ConnectionString = DataConnectionStr;
        }

        public int Create(CellPhoneDevice data)
        {
            int CellPhoneDeviceId = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertCellPhoneReq";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@CellPhoneDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                p2.Direction = ParameterDirection.Input;
                p2.Value = data.RequestorId;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = data.EmployeeSignature;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Model", SqlDbType.VarChar);
                p4.Direction = ParameterDirection.Input;
                p4.Value = data.Model;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@Make", SqlDbType.VarChar);
                p5.Direction = ParameterDirection.Input;
                p5.Value = data.Make;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@RenownOwnedType", SqlDbType.VarChar);
                p6.Direction = ParameterDirection.Input;
                p6.Value = data.RenownOwnedType;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@RenownOwnedCarrier", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                p7.Value = data.RenownOwnedCarrier;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@RenownOwnedPhone", SqlDbType.VarChar);
                p8.Direction = ParameterDirection.Input;
                p8.Value = data.RenownOwnedPhone;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                p9.Value = data.BusJustification;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p10.Direction = ParameterDirection.Input;
                p10.Value = data.SecuredAck1;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p11.Direction = ParameterDirection.Input;
                p11.Value = data.SecuredAck2;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p12.Direction = ParameterDirection.Input;
                p12.Value = data.SecuredAck3;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p13.Direction = ParameterDirection.Input;
                p13.Value = data.SecuredAck4;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p14.Direction = ParameterDirection.Input;
                p14.Value = data.SecuredAck5;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecuredAck6", SqlDbType.Bit);
                p15.Direction = ParameterDirection.Input;
                p15.Value = data.SecuredAck6;
                cmd.Parameters.Add(p15);

                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    CellPhoneDeviceId = (int)p1.Value;
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

            return CellPhoneDeviceId;
        }

        public bool Update(CellPhoneDevice data)
        {
            int retVal = 0;
            List<CellPhoneDevice> result = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateCellPhoneReq";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);

            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@CellPhoneDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = data.CellPhoneDeviceId;
                cmd.Parameters.Add(p1);

                //SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                //p2.Direction = ParameterDirection.Input;
                //p2.Value = data.RequestorId;
                //cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = data.EmployeeSignature;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Model", SqlDbType.VarChar);
                p4.Direction = ParameterDirection.Input;
                p4.Value = data.Model;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@Make", SqlDbType.VarChar);
                p5.Direction = ParameterDirection.Input;
                p5.Value = data.Make;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@RenownOwnedType", SqlDbType.VarChar);
                p6.Direction = ParameterDirection.Input;
                p6.Value = data.RenownOwnedType;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@RenownOwnedCarrier", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                p7.Value = data.RenownOwnedCarrier;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@RenownOwnedPhone", SqlDbType.VarChar);
                p8.Direction = ParameterDirection.Input;
                p8.Value = data.RenownOwnedPhone;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                p9.Value = data.BusJustification;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p10.Direction = ParameterDirection.Input;
                p10.Value = data.SecuredAck1;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p11.Direction = ParameterDirection.Input;
                p11.Value = data.SecuredAck2;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p12.Direction = ParameterDirection.Input;
                p12.Value = data.SecuredAck3;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p13.Direction = ParameterDirection.Input;
                p13.Value = data.SecuredAck4;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p14.Direction = ParameterDirection.Input;
                p14.Value = data.SecuredAck5;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecuredAck6", SqlDbType.Bit);
                p15.Direction = ParameterDirection.Input;
                p15.Value = data.SecuredAck6;
                cmd.Parameters.Add(p15);

                SqlParameter p16 = new SqlParameter("@RetVal", SqlDbType.Int);
                p16.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p16);

                cmd.ExecuteScalar();
                if (p16 != null && p16.Value != null)
                {
                    retVal = (int)p16.Value;
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


            return retVal > 0;
        }

        public CellPhoneDevice GetDevice(int dbKey)
        {
            int RetValue = 0;
            List<CellPhoneDevice> retData = new List<CellPhoneDevice>();
            WirelessDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCellPhoneDevice";
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@CellPhoneDeviceId", SqlDbType.Int);
            p2.Direction = ParameterDirection.Input;
            p2.Value = dbKey;
            cmd.Parameters.Add(p2);

            try
            {
                con.Open();
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    retData = ReadData(cmd);
                    if (p1 != null && p1.Value != null)
                    {
                        RetValue = (int)p1.Value;
                    }
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
            if (retData.Count > 0)
            {
                return retData[0];
            }
            else
            {
                return null;
            }
        }

        public List<CellPhoneDevice> GetDevicesFor(int RequestorId)
        {
            int RetVal = 0;
            List<CellPhoneDevice> retData = new List<CellPhoneDevice>();
            WirelessDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCellPhoneDevicesFor";
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);

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
                    if (p1 != null && p1.Value != null)
                    {
                        RetVal = (int)p1.Value;
                    }
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

        public List<CellPhoneDevice> GetDevices()
        {
            int RetVal = 0;
            List<CellPhoneDevice> retData = null;

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCellPhoneDevices";

            SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);

            try
            {
                con.Open();
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    retData = ReadData(cmd);
                    if (p1 != null && p1.Value != null)
                    {
                        RetVal = (int)p1.Value;
                    }
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



        private List<CellPhoneDevice> ReadData(SqlCommand cmd)
        {
            List<CellPhoneDevice> Devices = new List<CellPhoneDevice>();
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
                CellPhoneDevice Device = new CellPhoneDevice();


                Device.CellPhoneDeviceId = (int)reader["CellPhoneDeviceId"];
                Device.RequestorId = (int)reader["RequestorId"];

                if (reader["EmployeeSignature"] != DBNull.Value)
                {
                    Device.EmployeeSignature = (String)reader["EmployeeSignature"];
                }
                else
                {
                    Device.EmployeeSignature = String.Empty;
                }

                if (reader["Model"] != DBNull.Value)
                {
                    Device.Model = (String)reader["Model"];
                }
                else
                {
                    Device.Model = String.Empty;
                }

                if (reader["Make"] != DBNull.Value)
                {
                    Device.Make = (String)reader["Make"];
                }
                else
                {
                    Device.Make = String.Empty;
                }

                if (reader["RenownOwnedType"] != DBNull.Value)
                {
                    Device.RenownOwnedType = (String)reader["RenownOwnedType"];
                }
                else
                {
                    Device.RenownOwnedType = String.Empty;
                }

                if (reader["RenownOwnedCarrier"] != DBNull.Value)
                {
                    Device.RenownOwnedCarrier = (String)reader["RenownOwnedCarrier"];
                }
                else
                {
                    Device.RenownOwnedCarrier = String.Empty;
                }

                if (reader["RenownOwnedPhone"] != DBNull.Value)
                {
                    Device.RenownOwnedPhone = (String)reader["RenownOwnedPhone"];
                }
                else
                {
                    Device.RenownOwnedPhone = String.Empty;

                }

                if (reader["BusJustification"] != DBNull.Value)
                {
                    Device.BusJustification = (String)reader["BusJustification"];
                }
                else
                {
                    Device.BusJustification = String.Empty;
                }

                Device.SecuredAck1 = (bool)reader["SecuredAck1"];
                Device.SecuredAck2 = (bool)reader["SecuredAck2"];
                Device.SecuredAck3 = (bool)reader["SecuredAck3"];
                Device.SecuredAck4 = (bool)reader["SecuredAck4"];
                Device.SecuredAck5 = (bool)reader["SecuredAck5"];
                Device.SecuredAck6 = (bool)reader["SecuredAck6"];

                Devices.Add(Device);
            }
            reader.Close();
            return Devices;
        }
    }
}
