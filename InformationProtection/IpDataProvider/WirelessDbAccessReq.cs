using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IpModelData;

namespace IpDataProvider
{
    public class WirelessDbAccessReq
    {
        String ConnectionString { get; set; }

        public WirelessDbAccessReq(String DataConnectionStr)
        {
            ConnectionString = DataConnectionStr;
        }

        public int Create(WirelessDevice data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertWirelessReq";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int WirelessDeviceId = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@WirelessDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                p2.Value = data.RequestorId;
                p2.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p3.Value = data.EmployeeSignature;
                p3.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Model", SqlDbType.VarChar);
                p4.Value = data.Model;
                p4.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                p5.Value = data.SerialNumber;
                p5.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@Vendor", SqlDbType.VarChar);
                p6.Value = data.Vendor;
                p6.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@RenownOwnedType", SqlDbType.VarChar);
                p7.Value = data.RenownOwnedType;
                p7.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p8.Value = data.BusJustification;
                p8.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@PhysLocation", SqlDbType.VarChar);
                p9.Value = data.PhysLocation;
                p9.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p10.Value = data.SecuredAck1;
                p10.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p11.Value = data.SecuredAck2;
                p11.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p12.Value = data.SecuredAck3;
                p12.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p13.Value = data.SecuredAck4;
                p13.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p14.Value = data.SecuredAck5;
                p14.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecuredAck6", SqlDbType.Bit);
                p15.Value = data.SecuredAck6;
                p15.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p15);

                SqlParameter p16 = new SqlParameter("@SecurityAck1", SqlDbType.Bit);
                p16.Value = data.SecurityAck1;
                p16.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p16);

                SqlParameter p17 = new SqlParameter("@SecurityAck2", SqlDbType.Bit);
                p17.Value = data.SecurityAck2;
                p17.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p17);

                SqlParameter p18 = new SqlParameter("@SecurityAck3", SqlDbType.Bit);
                p18.Value = data.SecurityAck3;
                p18.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter("@SecurityAck4", SqlDbType.Bit);
                p19.Value = data.SecurityAck4;
                p19.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter("@SecurityAck5", SqlDbType.Bit);
                p20.Value = data.SecurityAck5;
                p20.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter("@SecurityAck6", SqlDbType.Bit);
                p21.Value = data.SecurityAck6;
                p21.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p21);

                SqlParameter p22 = new SqlParameter("@SecurityAck7", SqlDbType.Bit);
                p22.Value = data.SecurityAck7;
                p22.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p22);

                SqlParameter p23 = new SqlParameter("@SecurityAck8", SqlDbType.Bit);
                p23.Value = data.SecurityAck8;
                p23.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p23);


                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    WirelessDeviceId = (int)p1.Value;
                }

            }
            catch (Exception ex)
            {
                String message = ex.Message;
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return WirelessDeviceId;
        }


        public bool Update(WirelessDevice data)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "UpdateWirelessDevice";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@WirelessDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = data.WirelessDeviceId;
                cmd.Parameters.Add(p1);

                SqlParameter p3 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p3.Value = data.EmployeeSignature;
                p3.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Model", SqlDbType.VarChar);
                p4.Value = data.Model;
                p4.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@SerialNumber", SqlDbType.VarChar);
                p5.Value = data.SerialNumber;
                p5.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@Vendor", SqlDbType.VarChar);
                p6.Value = data.Vendor;
                p6.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@RenownOwnedType", SqlDbType.VarChar);
                p7.Value = data.RenownOwnedType;
                p7.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p8.Value = data.BusJustification;
                p8.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@PhysLocation", SqlDbType.VarChar);
                p9.Value = data.PhysLocation;
                p9.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p10.Value = data.SecuredAck1;
                p10.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p11.Value = data.SecuredAck2;
                p11.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p12.Value = data.SecuredAck3;
                p12.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p13.Value = data.SecuredAck4;
                p13.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p14.Value = data.SecuredAck5;
                p14.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecuredAck6", SqlDbType.Bit);
                p15.Value = data.SecuredAck6;
                p15.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p15);

                SqlParameter p16 = new SqlParameter("@SecurityAck1", SqlDbType.Bit);
                p16.Value = data.SecurityAck1;
                p16.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p16);

                SqlParameter p17 = new SqlParameter("@SecurityAck2", SqlDbType.Bit);
                p17.Value = data.SecurityAck2;
                p17.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p17);

                SqlParameter p18 = new SqlParameter("@SecurityAck3", SqlDbType.Bit);
                p18.Value = data.SecurityAck3;
                p18.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter("@SecurityAck4", SqlDbType.Bit);
                p19.Value = data.SecurityAck4;
                p19.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter("@SecurityAck5", SqlDbType.Bit);
                p20.Value = data.SecurityAck5;
                p20.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter("@SecurityAck6", SqlDbType.Bit);
                p21.Value = data.SecurityAck6;
                p21.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p21);

                SqlParameter p22 = new SqlParameter("@SecurityAck7", SqlDbType.Bit);
                p22.Value = data.SecurityAck7;
                p22.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p22);

                SqlParameter p23 = new SqlParameter("@SecurityAck8", SqlDbType.Bit);
                p23.Value = data.SecurityAck8;
                p23.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p23);

                SqlParameter p24 = new SqlParameter("@Result", SqlDbType.Int);
                p24.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p24);

                cmd.ExecuteScalar();
                if (p24 != null && p24.Value != null)
                {
                    Result = (int)p24.Value;
                }

            }
            catch (Exception ex)
            {
                String message = ex.Message;
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }


            return Result > 0;
        }



        public WirelessDevice GetDevice(int WirelessDeviceId)
        {
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetWirelessDevice";
            SqlConnection con = new SqlConnection(ConnectionString);

            List<WirelessDevice> retData = null;
            SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@WirelessDeviceId", SqlDbType.Int);
            p2.Direction = ParameterDirection.Input;
            p2.Value = WirelessDeviceId;
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
            if (retData != null && retData.Count > 0)
            {
                return retData[0];
            }
            else
            {
                return null;
            }
        }

        public List<WirelessDevice> GetDevicesFor(int RequestorId)
        {
            List<WirelessDevice> retData = new List<WirelessDevice>();
            WirelessDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetWirelessDevicesFor";
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
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }
            return retData;
        }

        public List<WirelessDevice> GetDevices()
        {
            List<WirelessDevice> retData = null;
            UsbDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetWirelessDevices";
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
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }
            return retData;
        }

        private List<WirelessDevice> ReadData(SqlCommand cmd)
        {
            List<WirelessDevice> Devices = new List<WirelessDevice>();
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
                WirelessDevice Device = new WirelessDevice();


                Device.WirelessDeviceId = (int)reader["WirelessDeviceId"];
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

                if (reader["SerialNumber"] != DBNull.Value)
                {
                    Device.SerialNumber = (String)reader["SerialNumber"];
                }
                else
                {
                    Device.SerialNumber = String.Empty;
                }

                if (reader["Vendor"] != DBNull.Value)
                {
                    Device.Vendor = (String)reader["Vendor"];
                }
                else
                {
                    Device.Vendor = String.Empty;
                }

                if (reader["RenownOwnedType"] != DBNull.Value)
                {
                    Device.RenownOwnedType = (String)reader["RenownOwnedType"];
                }
                else
                {
                    Device.RenownOwnedType = String.Empty;
                }

                if (reader["BusJustification"] != DBNull.Value)
                {
                    Device.BusJustification = (String)reader["BusJustification"];
                }
                else
                {
                    Device.BusJustification = String.Empty;
                }

                if (reader["PhysLocation"] != DBNull.Value)
                {
                    Device.PhysLocation = (String)reader["PhysLocation"];
                }
                else
                {
                    Device.PhysLocation = String.Empty;
                }

                Device.SecuredAck1 = (bool)reader["SecuredAck1"];
                Device.SecuredAck2 = (bool)reader["SecuredAck2"];
                Device.SecuredAck3 = (bool)reader["SecuredAck3"];
                Device.SecuredAck4 = (bool)reader["SecuredAck4"];
                Device.SecuredAck5 = (bool)reader["SecuredAck5"];
                Device.SecuredAck6 = (bool)reader["SecuredAck6"];

                Device.SecurityAck1 = (bool)reader["SecurityAck1"];
                Device.SecurityAck2 = (bool)reader["SecurityAck2"];
                Device.SecurityAck3 = (bool)reader["SecurityAck3"];
                Device.SecurityAck4 = (bool)reader["SecurityAck4"];
                Device.SecurityAck5 = (bool)reader["SecurityAck5"];
                Device.SecurityAck6 = (bool)reader["SecurityAck6"];
                Device.SecurityAck7 = (bool)reader["SecurityAck7"];
                Device.SecurityAck8 = (bool)reader["SecurityAck8"];

                Devices.Add(Device);
            }
            reader.Close();
            return Devices;
        }
    }
}
