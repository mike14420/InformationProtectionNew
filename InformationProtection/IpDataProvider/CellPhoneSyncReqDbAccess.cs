using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IpModelData;

namespace IpDataProvider
{
    public class CellPhoneSyncReqDbAccess
    {
        String ConnectionString { get; set; }

        public CellPhoneSyncReqDbAccess(String DataConnectionStr)
        {
            ConnectionString = DataConnectionStr;
        }

        public int Create(CellPhoneSyncDevice data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertCellPhoneSyncReq";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int CellPhoneSyncDeviceId = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@CellPhoneSyncDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                p2.Value = data.RequestorId;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p3.Value = data.EmployeeSignature;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Model", SqlDbType.VarChar);
                p4.Value = data.Model;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@Make", SqlDbType.VarChar);
                p5.Value = data.Make;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p6.Value = data.BusJustification;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@PersonOwnedType", SqlDbType.VarChar);
                p7.Value = data.PersonOwnedType;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@Carrier", SqlDbType.VarChar);
                p8.Value = data.Carrier;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                p9.Value = data.SerialNumber;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@PhoneNumber", SqlDbType.VarChar);
                p10.Value = data.PhoneNumber;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@MobileOS", SqlDbType.VarChar);
                p11.Value = data.MobileOS;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@cb_sync1", SqlDbType.VarChar);
                p12.Value = data.cb_sync1;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@cb_sync2", SqlDbType.VarChar);
                p13.Value = data.cb_sync2;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@cb_sync3", SqlDbType.VarChar);
                p14.Value = data.cb_sync3;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p15.Value = data.SecuredAck1;
                cmd.Parameters.Add(p15);

                SqlParameter p16 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p16.Value = data.SecuredAck2;
                cmd.Parameters.Add(p16);

                SqlParameter p17 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p17.Value = data.SecuredAck3;
                cmd.Parameters.Add(p17);

                SqlParameter p18 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p18.Value = data.SecuredAck4;
                cmd.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p19.Value = data.SecuredAck3;
                cmd.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter("@SecuredAck6", SqlDbType.Bit);
                p20.Value = data.SecuredAck3;
                cmd.Parameters.Add(p20);

                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    CellPhoneSyncDeviceId = (int)p1.Value;
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

            return CellPhoneSyncDeviceId;
        }
        public bool Update(CellPhoneSyncDevice data)
        {
            int retData = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateCellPhoneSyncReq";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@CellPhoneSyncDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = data.CellPhoneSyncDeviceId;
                cmd.Parameters.Add(p1);

                //SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                //p2.Value = data.RequestorId;
                //cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p3.Value = data.EmployeeSignature;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Model", SqlDbType.VarChar);
                p4.Value = data.Model;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@Make", SqlDbType.VarChar);
                p5.Value = data.Make;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p6.Value = data.BusJustification;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@PersonOwnedType", SqlDbType.VarChar);
                p7.Value = data.PersonOwnedType;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@Carrier", SqlDbType.VarChar);
                p8.Value = data.Carrier;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                p9.Value = data.SerialNumber;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@PhoneNumber", SqlDbType.VarChar);
                p10.Value = data.PhoneNumber;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@MobileOS", SqlDbType.VarChar);
                p11.Value = data.MobileOS;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@cb_sync1", SqlDbType.VarChar);
                p12.Value = data.cb_sync1;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@cb_sync2", SqlDbType.VarChar);
                p13.Value = data.cb_sync2;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@cb_sync3", SqlDbType.VarChar);
                p14.Value = data.cb_sync3;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p15.Value = data.SecuredAck1;
                cmd.Parameters.Add(p15);

                SqlParameter p16 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p16.Value = data.SecuredAck2;
                cmd.Parameters.Add(p16);

                SqlParameter p17 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p17.Value = data.SecuredAck3;
                cmd.Parameters.Add(p17);

                SqlParameter p18 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p18.Value = data.SecuredAck4;
                cmd.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p19.Value = data.SecuredAck3;
                cmd.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter("@SecuredAck6", SqlDbType.Bit);
                p20.Value = data.SecuredAck3;
                cmd.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter("@RetVal", SqlDbType.Int);
                p21.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p21);

                cmd.ExecuteScalar();
                if (p21 != null && p21.Value != null)
                {
                    retData = (int)p21.Value;
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
            return retData > 0;

        }

        public CellPhoneSyncDevice GetDevice(int dbKey)
        {
            List<CellPhoneSyncDevice> retData = new List<CellPhoneSyncDevice>();
            CellPhoneSyncDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCellPhoneSyncDevice";
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@CellPhoneSyncDeviceId", SqlDbType.Int);
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
                }
            }
            catch (Exception error)
            {
            }
            finally
            {
                if (con.State == ConnectionState.Open)
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

        public List<CellPhoneSyncDevice> GetDevicesFor(int RequestorId)
        {
            List<CellPhoneSyncDevice> retData = new List<CellPhoneSyncDevice>();
            WirelessDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCellPhoneSyncDevicesFor";
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
                }
            }
            catch (Exception error)
            {
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return retData;
        }

        public List<CellPhoneSyncDevice> GetDevices()
        {

            List<CellPhoneSyncDevice> retData = new List<CellPhoneSyncDevice>();
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetCellPhoneSyncDevices";
            SqlConnection con = new SqlConnection(ConnectionString);

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
                }
            }
            catch (Exception error)
            {
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            return retData;
        }

        private List<CellPhoneSyncDevice> ReadData(SqlCommand cmd)
        {
            List<CellPhoneSyncDevice> Devices = new List<CellPhoneSyncDevice>();
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
                CellPhoneSyncDevice Device = new CellPhoneSyncDevice();

                Device.CellPhoneSyncDeviceId = (int)reader["CellPhoneSyncDeviceId"];
                Device.RequestorId = (int)reader["RequestorId"];
                if (reader["EmployeeSignature"] != DBNull.Value)
                {
                    Device.EmployeeSignature = ((String)reader["EmployeeSignature"]).Trim().ToUpper();
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

                if (reader["BusJustification"] != DBNull.Value)
                {
                    Device.BusJustification = (String)reader["BusJustification"];
                }
                else
                {
                    Device.BusJustification = String.Empty;
                }

                if (reader["PersonOwnedType"] != DBNull.Value)
                {
                    Device.PersonOwnedType = (String)reader["PersonOwnedType"];
                }
                else
                {
                    Device.PersonOwnedType = String.Empty;
                }

                if (reader["Carrier"] != DBNull.Value)
                {
                    Device.Carrier = (String)reader["Carrier"];
                }
                else
                {
                    Device.Carrier = String.Empty;
                }

                if (reader["SerialNumber"] != DBNull.Value)
                {
                    Device.SerialNumber = (String)reader["SerialNumber"];
                }
                else
                {
                    Device.SerialNumber = String.Empty;
                }

                if (reader["PhoneNumber"] != DBNull.Value)
                {
                    Device.PhoneNumber = (String)reader["PhoneNumber"];
                }
                else
                {
                    Device.PhoneNumber = String.Empty;
                }

                if (reader["MobileOS"] != DBNull.Value)
                {
                    Device.MobileOS = (String)reader["MobileOS"];
                }
                else
                {
                    Device.MobileOS = String.Empty;
                }

                if (reader["MobileOS"] != DBNull.Value)
                {
                    Device.MobileOS = (String)reader["MobileOS"];
                }
                else
                {
                    Device.MobileOS = String.Empty;
                }

                Device.cb_sync1 = (bool)reader["cb_sync1"];
                Device.cb_sync2 = (bool)reader["cb_sync2"];
                Device.cb_sync3 = (bool)reader["cb_sync3"];

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
