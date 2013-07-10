using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IpModelData;

namespace IpDataProvider
{
    public class UsbDbAccessReq
    {
        String ConnectionString { get; set; }

        public UsbDbAccessReq(String DataConnectionStr)
        {
            ConnectionString = DataConnectionStr;
        }

        public int Create(UsbDevice data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertUsbReq";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int UsbDeviceId = 0;

            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@UsbDeviceId", SqlDbType.Int);
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

                SqlParameter p6 = new SqlParameter("@RenownOwned", SqlDbType.VarChar);
                p6.Value = data.RenownOwned;
                p6.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p7.Value = data.BusJustification;
                p7.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@SecuredAck1", SqlDbType.Bit); 
                p8.Value = data.SecuredAck1;
                p8.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p9.Value = data.SecuredAck2;
                p9.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p10.Value = data.SecuredAck3;
                p10.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p11.Value = data.SecuredAck4;
                p11.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p12.Value = data.SecuredAck5;
                p12.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@SecuredAck6", SqlDbType.Bit);
                p13.Value = data.SecuredAck6;
                p13.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@SecurityAck1", SqlDbType.Bit);
                p14.Value = data.SecurityAck1;
                p14.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecurityAck2", SqlDbType.Bit);
                p15.Value = data.SecurityAck2;
                p15.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p15);

                SqlParameter p16 = new SqlParameter("@SecurityAck3", SqlDbType.Bit);
                p16.Value = data.SecurityAck3;
                p16.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p16);

                SqlParameter p17 = new SqlParameter("@SecurityAck4", SqlDbType.Bit);
                p17.Value = data.SecurityAck4;
                p17.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p17);

                SqlParameter p18 = new SqlParameter("@SecurityAck5", SqlDbType.Bit);
                p18.Value = data.SecurityAck5;
                p18.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter("@SecurityAck6", SqlDbType.Bit);
                p19.Value = data.SecurityAck6;
                p19.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter("@SecurityAck7", SqlDbType.Bit);
                p20.Value = data.SecurityAck7;
                p20.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter("@SecurityAck8", SqlDbType.Bit);
                p21.Value = data.SecurityAck8;
                p21.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p21);

                SqlParameter p22 = new SqlParameter("@SecurityAck9", SqlDbType.Bit);
                p22.Value = data.SecurityAck9;
                p22.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p22);

                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    UsbDeviceId = (int)p1.Value;
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

            return UsbDeviceId;
        }


        public bool Update(UsbDevice data)
        {
            int Result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateUsbReq";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);

            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@UsbDeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = data.UsbDeviceId;
                cmd.Parameters.Add(p1);

                //SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                //p2.Value = data.RequestorId;
                //p2.Direction = ParameterDirection.Input;
                //cmd.Parameters.Add(p2);

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

                SqlParameter p6 = new SqlParameter("@RenownOwned", SqlDbType.VarChar);
                p6.Value = data.RenownOwned;
                p6.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@BusJustification", SqlDbType.VarChar);
                p7.Value = data.BusJustification;
                p7.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p8.Value = data.SecuredAck1;
                p8.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p9.Value = data.SecuredAck2;
                p9.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p10.Value = data.SecuredAck3;
                p10.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p11.Value = data.SecuredAck4;
                p11.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p11);

                SqlParameter p12 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p12.Value = data.SecuredAck5;
                p12.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@SecuredAck6", SqlDbType.Bit);
                p13.Value = data.SecuredAck6;
                p13.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@SecurityAck1", SqlDbType.Bit);
                p14.Value = data.SecurityAck1;
                p14.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecurityAck2", SqlDbType.Bit);
                p15.Value = data.SecurityAck2;
                p15.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p15);

                SqlParameter p16 = new SqlParameter("@SecurityAck3", SqlDbType.Bit);
                p16.Value = data.SecurityAck3;
                p16.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p16);

                SqlParameter p17 = new SqlParameter("@SecurityAck4", SqlDbType.Bit);
                p17.Value = data.SecurityAck4;
                p17.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p17);

                SqlParameter p18 = new SqlParameter("@SecurityAck5", SqlDbType.Bit);
                p18.Value = data.SecurityAck5;
                p18.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter("@SecurityAck6", SqlDbType.Bit);
                p19.Value = data.SecurityAck6;
                p19.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter("@SecurityAck7", SqlDbType.Bit);
                p20.Value = data.SecurityAck7;
                p20.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter("@SecurityAck8", SqlDbType.Bit);
                p21.Value = data.SecurityAck8;
                p21.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p21);

                SqlParameter p22 = new SqlParameter("@SecurityAck9", SqlDbType.Bit);
                p22.Value = data.SecurityAck9;
                p22.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p22);

                SqlParameter p23 = new SqlParameter("@Result", SqlDbType.Int);
                p23.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p23);

                cmd.ExecuteScalar();
                if (p23 != null && p23.Value != null)
                {
                    Result = (int)p23.Value;
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

            return Result > 0;
        }


        public UsbDevice GetDevice(int UsbDeviceId)
        {
            List<UsbDevice> retData = new List<UsbDevice>();
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetUsbDevice";
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@UsbDeviceId", SqlDbType.Int);
            p2.Direction = ParameterDirection.Input;
            p2.Value = UsbDeviceId;
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
        public List<UsbDevice> GetDevicesFor(int RequestorId)
        {
            List<UsbDevice> retData = new List<UsbDevice>();
            UsbDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetUsbDevicesFor";
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
                con.Close();

            }
            return retData;
        }

        public List<UsbDevice> GetDevices()
        {
            List<UsbDevice> retData = new List<UsbDevice>();
            UsbDevice d;
            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetUsbDevices";
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
                con.Close();

            }
            return retData;
        }

        private List<UsbDevice> ReadData(SqlCommand cmd)
        {
            List<UsbDevice> Devices = new List<UsbDevice>();
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
                UsbDevice Device = new UsbDevice();


                Device.UsbDeviceId = (int)reader["UsbDeviceId"];
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


                if (reader["RenownOwned"] != DBNull.Value)
                {
                    Device.RenownOwned = (String)reader["RenownOwned"];
                }
                else
                {
                    Device.RenownOwned = String.Empty;
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

                Device.SecurityAck1 = (bool)reader["SecurityAck1"];
                Device.SecurityAck2 = (bool)reader["SecurityAck2"];
                Device.SecurityAck3 = (bool)reader["SecurityAck3"];
                Device.SecurityAck4 = (bool)reader["SecurityAck4"];
                Device.SecurityAck5 = (bool)reader["SecurityAck5"];
                Device.SecurityAck6 = (bool)reader["SecurityAck6"];
                Device.SecurityAck7 = (bool)reader["SecurityAck7"];
                Device.SecurityAck8 = (bool)reader["SecurityAck8"];
                Device.SecurityAck9 = (bool)reader["SecurityAck9"];
                Devices.Add(Device);
            }
            reader.Close();
            return Devices;
        }
    }
}
