using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IpModelData;

namespace IpDataProvider
{
    public class RemoteAccessDbReqAccess
    {
        String ConnectionString { get; set; }

        public RemoteAccessDbReqAccess(String DataConnectionStr)
        {
            ConnectionString = DataConnectionStr;
        }

        public int Create(RemoteAccess data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertRemoteAccessReq";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int RemoteAccessId = 0;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@RemoteAccessId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;

                SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                p2.Direction = ParameterDirection.Input;
                p2.Value = data.RequestorId;
                SqlParameter p3 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = data.EmployeeSignature;
                SqlParameter p4 = new SqlParameter("@IpAddressAndHostName", SqlDbType.VarChar);
                p4.Direction = ParameterDirection.Input;
                if (data.IpAddressAndHostName != null)
                {
                    p4.Value = data.IpAddressAndHostName;
                }
                else
                {
                    p4.Value = String.Empty;
                }

                SqlParameter p5 = new SqlParameter("@AppNames", SqlDbType.VarChar);
                p5.Direction = ParameterDirection.Input;
                p5.Value = data.AppNames;
                if (data.AppNames != null)
                {
                    p5.Value = data.AppNames;
                }
                else
                {
                    p5.Value = String.Empty;
                }

                SqlParameter p6 = new SqlParameter("@LanShares", SqlDbType.VarChar);
                p6.Direction = ParameterDirection.Input;
                if (data.LanShares != null)
                {
                    p6.Value = data.LanShares;
                }
                else
                {
                    p6.Value = String.Empty;
                }


                SqlParameter p7 = new SqlParameter("@ComputerName", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                if (data.ComputerName != null)
                {
                    p7.Value = data.ComputerName;
                }
                else
                {
                    p7.Value = String.Empty;
                }

                SqlParameter p8 = new SqlParameter("@RemoteConnectionType", SqlDbType.VarChar);
                p8.Direction = ParameterDirection.Input;
                if (data.RemoteConnectionType != null)
                {
                    p8.Value = data.RemoteConnectionType;
                }
                else
                {
                    p8.Value = String.Empty;
                }

                SqlParameter p9 = new SqlParameter("@JobDuties", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                if (data.JobDuties != null)
                {
                    p9.Value = data.JobDuties;
                }
                else
                {
                    p9.Value = String.Empty;
                }

                SqlParameter p10 = new SqlParameter("@WorkStationOS", SqlDbType.VarChar);
                p10.Direction = ParameterDirection.Input;
                if (data.WorkStationOS != null)
                {
                    p10.Value = data.WorkStationOS;
                }
                else
                {
                    p10.Value = String.Empty;
                }

                SqlParameter p11 = new SqlParameter("@AccessToServer", SqlDbType.Bit);
                p11.Direction = ParameterDirection.Input;
                p11.Value = data.AccessToServer;

                SqlParameter p12 = new SqlParameter("@AccessToApp", SqlDbType.Bit);
                p12.Direction = ParameterDirection.Input;
                p12.Value = data.AccessToApp;


                SqlParameter p13 = new SqlParameter("@AccessToLanShares", SqlDbType.Bit);
                p13.Direction = ParameterDirection.Input;
                p13.Value = data.AccessToLanShares;
                SqlParameter p14 = new SqlParameter("@AccessToMyWorkStation", SqlDbType.Bit);
                p14.Direction = ParameterDirection.Input;
                p14.Value = data.AccessToMyWorkStation;
                SqlParameter p15 = new SqlParameter("@ConnectFromHome", SqlDbType.Bit);
                p15.Direction = ParameterDirection.Input;
                p15.Value = data.ConnectFromHome;
                SqlParameter p16 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p16.Direction = ParameterDirection.Input;
                p16.Value = data.SecuredAck1;
                SqlParameter p17 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p17.Direction = ParameterDirection.Input;
                p17.Value = data.SecuredAck2;
                SqlParameter p18 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p18.Direction = ParameterDirection.Input;
                p18.Value = data.SecuredAck3;
                SqlParameter p19 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p19.Direction = ParameterDirection.Input;
                p19.Value = data.SecuredAck4;
                SqlParameter p20 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p20.Direction = ParameterDirection.Input;
                p20.Value = data.SecuredAck5;
                SqlParameter p21 = new SqlParameter("@NonExemptEmployee", SqlDbType.Bit);
                p21.Direction = ParameterDirection.Input;
                p21.Value = data.NonExemptEmployee;

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);
                cmd.Parameters.Add(p12);
                cmd.Parameters.Add(p13);
                cmd.Parameters.Add(p14);
                cmd.Parameters.Add(p15);
                cmd.Parameters.Add(p16);
                cmd.Parameters.Add(p17);
                cmd.Parameters.Add(p18);
                cmd.Parameters.Add(p19);
                cmd.Parameters.Add(p20);
                cmd.Parameters.Add(p21);

                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    RemoteAccessId = (int)p1.Value;
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

            return RemoteAccessId;
        }

        public bool Update(RemoteAccess data)
        {
            int RetVal = 0;

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd  = new SqlCommand();
            cmd.CommandText = "UpdateRemoteAccessReq";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.Connection = con;
                SqlParameter p1 = new SqlParameter("@RemoteAccessId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = data.RemoteAccessId;

                //SqlParameter p2 = new SqlParameter("@RequestorId", SqlDbType.Int);
                //p2.Direction = ParameterDirection.Input;
                //p2.Value = data.RequestorId;

                SqlParameter p3 = new SqlParameter("@EmployeeSignature", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = data.EmployeeSignature;
                SqlParameter p4 = new SqlParameter("@IpAddressAndHostName", SqlDbType.VarChar);
                p4.Direction = ParameterDirection.Input;
                if (data.IpAddressAndHostName != null)
                {
                    p4.Value = data.IpAddressAndHostName;
                }
                else
                {
                    p4.Value = String.Empty;
                }

                SqlParameter p5 = new SqlParameter("@AppNames", SqlDbType.VarChar);
                p5.Direction = ParameterDirection.Input;
                p5.Value = data.AppNames;
                if (data.AppNames != null)
                {
                    p5.Value = data.AppNames;
                }
                else
                {
                    p5.Value = String.Empty;
                }

                SqlParameter p6 = new SqlParameter("@LanShares", SqlDbType.VarChar);
                p6.Direction = ParameterDirection.Input;
                if (data.LanShares != null)
                {
                    p6.Value = data.LanShares;
                }
                else
                {
                    p6.Value = String.Empty;
                }


                SqlParameter p7 = new SqlParameter("@ComputerName", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                if (data.ComputerName != null)
                {
                    p7.Value = data.ComputerName;
                }
                else
                {
                    p7.Value = String.Empty;
                }

                SqlParameter p8 = new SqlParameter("@RemoteConnectionType", SqlDbType.VarChar);
                p8.Direction = ParameterDirection.Input;
                if (data.RemoteConnectionType != null)
                {
                    p8.Value = data.RemoteConnectionType;
                }
                else
                {
                    p8.Value = String.Empty;
                }

                SqlParameter p9 = new SqlParameter("@JobDuties", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                if (data.JobDuties != null)
                {
                    p9.Value = data.JobDuties;
                }
                else
                {
                    p9.Value = String.Empty;
                }

                SqlParameter p10 = new SqlParameter("@WorkStationOS", SqlDbType.VarChar);
                p10.Direction = ParameterDirection.Input;
                if (data.WorkStationOS != null)
                {
                    p10.Value = data.WorkStationOS;
                }
                else
                {
                    p10.Value = String.Empty;
                }

                SqlParameter p11 = new SqlParameter("@AccessToServer", SqlDbType.Bit);
                p11.Direction = ParameterDirection.Input;
                p11.Value = data.AccessToServer;

                SqlParameter p12 = new SqlParameter("@AccessToApp", SqlDbType.Bit);
                p12.Direction = ParameterDirection.Input;
                p12.Value = data.AccessToApp;


                SqlParameter p13 = new SqlParameter("@AccessToLanShares", SqlDbType.Bit);
                p13.Direction = ParameterDirection.Input;
                p13.Value = data.AccessToLanShares;
                SqlParameter p14 = new SqlParameter("@AccessToMyWorkStation", SqlDbType.Bit);
                p14.Direction = ParameterDirection.Input;
                p14.Value = data.AccessToMyWorkStation;
                SqlParameter p15 = new SqlParameter("@ConnectFromHome", SqlDbType.Bit);
                p15.Direction = ParameterDirection.Input;
                p15.Value = data.ConnectFromHome;
                SqlParameter p16 = new SqlParameter("@SecuredAck1", SqlDbType.Bit);
                p16.Direction = ParameterDirection.Input;
                p16.Value = data.SecuredAck1;
                SqlParameter p17 = new SqlParameter("@SecuredAck2", SqlDbType.Bit);
                p17.Direction = ParameterDirection.Input;
                p17.Value = data.SecuredAck2;
                SqlParameter p18 = new SqlParameter("@SecuredAck3", SqlDbType.Bit);
                p18.Direction = ParameterDirection.Input;
                p18.Value = data.SecuredAck3;
                SqlParameter p19 = new SqlParameter("@SecuredAck4", SqlDbType.Bit);
                p19.Direction = ParameterDirection.Input;
                p19.Value = data.SecuredAck4;
                SqlParameter p20 = new SqlParameter("@SecuredAck5", SqlDbType.Bit);
                p20.Direction = ParameterDirection.Input;
                p20.Value = data.SecuredAck5;
                SqlParameter p21 = new SqlParameter("@NonExemptEmployee", SqlDbType.Bit);
                p21.Direction = ParameterDirection.Input;
                p21.Value = data.NonExemptEmployee;

                cmd.Parameters.Add(p1);
               
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);
                cmd.Parameters.Add(p12);
                cmd.Parameters.Add(p13);
                cmd.Parameters.Add(p14);
                cmd.Parameters.Add(p15);
                cmd.Parameters.Add(p16);
                cmd.Parameters.Add(p17);
                cmd.Parameters.Add(p18);
                cmd.Parameters.Add(p19);
                cmd.Parameters.Add(p20);
                cmd.Parameters.Add(p21);


                SqlParameter p22 = new SqlParameter("@RetVal", SqlDbType.Int);
                p22.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p22);

                cmd.ExecuteScalar();
                if (p22 != null && p22.Value != null)
                {
                    RetVal = (int)p22.Value;
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


            return (RetVal > 0);

        }

        public RemoteAccess GetDevice(int RemoteAccessId)
        {
            RemoteAccess retData = null;
            List<RemoteAccess> result = null;

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = null;

            cmd = new SqlCommand();
            cmd.CommandText = "GetRemoteAccessDevice";
            SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@RemoteAccessId", SqlDbType.Int);
            p2.Direction = ParameterDirection.Input;
            p2.Value = RemoteAccessId;
            cmd.Parameters.Add(p2);

            try
            {
                con.Open();
                cmd.Connection = con;
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    result = ReadData(cmd);
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
            if (result != null && result.Count > 0)
            {
                retData = result[0];
            }
            return retData;
        }

        public List<RemoteAccess> GetDevicesFor(int RequestorId)
        {
            int RetVal;
            List<RemoteAccess> Devices = null;

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRemoteAccessDevicesFor";

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
                    Devices = ReadData(cmd);
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
            return Devices;
        }

        public List<RemoteAccess> GetDevices()
        {
            List<RemoteAccess> RetData = null;
            int RetVal = 0;

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRemoteAccessDevices";

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
                    RetData = ReadData(cmd);
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

            return RetData;
        }

        private List<RemoteAccess> ReadData(SqlCommand cmd)
        {
            List<RemoteAccess> RemoteAccessDevices = new List<RemoteAccess>();
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
                RemoteAccess Device = new RemoteAccess();

                Device.RemoteAccessId = (int)reader["RemoteAccessId"];
                Device.RequestorId = (int)reader["RequestorId"];

                if (reader["EmployeeSignature"] != DBNull.Value)
                {
                    Device.EmployeeSignature = (String)reader["EmployeeSignature"];
                }
                else
                {
                    Device.EmployeeSignature = String.Empty;
                }
                Device.AccessToServer = (bool)reader["AccessToServer"];
                Device.AccessToApp = (bool)reader["AccessToApp"];
                Device.AccessToLanShares = (bool)reader["AccessToLanShares"];
                Device.AccessToMyWorkStation = (bool)reader["AccessToMyWorkStation"];
                Device.ConnectFromHome = (bool)reader["ConnectFromHome"];
                Device.NonExemptEmployee = (bool)reader["NonExemptEmployee"];

                if (reader["IpAddressAndHostName"] != DBNull.Value)
                {
                    Device.IpAddressAndHostName = (String)reader["IpAddressAndHostName"];
                }
                else
                {
                    Device.IpAddressAndHostName = String.Empty;
                }

                if (reader["AppNames"] != DBNull.Value)
                {
                    Device.AppNames = (String)reader["AppNames"];
                }
                else
                {
                    Device.AppNames = String.Empty;
                }

                if (reader["LanShares"] != DBNull.Value)
                {
                    Device.LanShares = (String)reader["LanShares"];
                }
                else
                {
                    Device.LanShares = String.Empty;
                }

                if (reader["ComputerName"] != DBNull.Value)
                {
                    Device.ComputerName = (String)reader["ComputerName"];
                }
                else
                {
                    Device.ComputerName = String.Empty;
                }

                if (reader["RemoteConnectionType"] != DBNull.Value)
                {
                    Device.RemoteConnectionType = (String)reader["RemoteConnectionType"];
                }
                else
                {
                    Device.RemoteConnectionType = String.Empty;

                }

                if (reader["JobDuties"] != DBNull.Value)
                {
                    Device.JobDuties = (String)reader["JobDuties"];
                }
                else
                {
                    Device.JobDuties = String.Empty;
                }

                if (reader["WorkStationOS"] != DBNull.Value)
                {
                    Device.WorkStationOS = (String)reader["WorkStationOS"];
                }
                else
                {
                    Device.WorkStationOS = String.Empty;
                }

                Device.SecuredAck1 = (bool)reader["SecuredAck1"];
                Device.SecuredAck2 = (bool)reader["SecuredAck2"];
                Device.SecuredAck3 = (bool)reader["SecuredAck3"];
                Device.SecuredAck4 = (bool)reader["SecuredAck4"];
                Device.SecuredAck5 = (bool)reader["SecuredAck5"];

                RemoteAccessDevices.Add(Device);
            }
            reader.Close();
            return RemoteAccessDevices;
        }
    }
}
