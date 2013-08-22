using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpModelData;

namespace IpDataProvider
{
    public class ApprovalRequestDbAccess
    {
        String ConnectionString { get; set; }

        public ApprovalRequestDbAccess(String DataConnectionStr)
        {
            ConnectionString = DataConnectionStr;
        }


        public int Create(IpApprovalRequest data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertApprovalRequest";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            int returnValue = 0;
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@IpApprovalRequestId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@IpRequestorId", SqlDbType.Int);
                p2.Direction = ParameterDirection.Input;
                p2.Value = data.IpRequestorId;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@SubmitDate", SqlDbType.DateTime);
                p3.Direction = ParameterDirection.Input;
                p3.Value = data.SubmitDate;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@GrantDate", SqlDbType.DateTime);
                p4.Direction = ParameterDirection.Input;
                p4.Value = data.GrantDate;
                cmd.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@ReturnDate", SqlDbType.DateTime);
                p5.Direction = ParameterDirection.Input;
                p5.Value = data.ReturnDate;
                cmd.Parameters.Add(p5);


                // FIRST SUP
                SqlParameter p6 = new SqlParameter("@FirstSupEmpId", SqlDbType.Int);
                p6.Direction = ParameterDirection.Input;
                p6.Value = data.FirstSupEmpId;
                cmd.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@FirstSupName", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                p7.Value = data.FirstSupName;
                cmd.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("@FirstSupApprovalDate", SqlDbType.DateTime);
                p8.Direction = ParameterDirection.Input;
                p8.Value = data.FirstSupApprovalDate;
                cmd.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("@FirstSupApproval", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                p9.Value = data.FirstSupApproval;
                cmd.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("@FirstSupComment", SqlDbType.VarChar);
                p10.Direction = ParameterDirection.Input;
                p10.Value = data.FirstSupComment;
                cmd.Parameters.Add(p10);

                SqlParameter p11 = new SqlParameter("@FirstSupEmail", SqlDbType.VarChar);
                p11.Direction = ParameterDirection.Input;
                p11.Value = data.FirstSupEmail;
                cmd.Parameters.Add(p11);


                // SECOND SUP
                SqlParameter p12 = new SqlParameter("@SecondSupEmpId", SqlDbType.Int);
                p12.Direction = ParameterDirection.Input;
                p12.Value = data.SecondSupEmpId;
                cmd.Parameters.Add(p12);

                SqlParameter p13 = new SqlParameter("@SecondSupName", SqlDbType.VarChar);
                p13.Direction = ParameterDirection.Input;
                p13.Value = data.SecondSupName;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@SecondSupApprovalDate", SqlDbType.DateTime);
                p14.Direction = ParameterDirection.Input;
                p14.Value = data.SecondSupApprovalDate;
                cmd.Parameters.Add(p14);

                SqlParameter p15 = new SqlParameter("@SecondSupApproval", SqlDbType.VarChar);
                p15.Direction = ParameterDirection.Input;
                p15.Value = data.SecondSupApproval;
                cmd.Parameters.Add(p15);

                SqlParameter p16 = new SqlParameter("@SecondSupComment", SqlDbType.VarChar);
                p16.Direction = ParameterDirection.Input;
                p16.Value = data.SecondSupComment;
                cmd.Parameters.Add(p16);

                SqlParameter p17 = new SqlParameter("@SecondSupEmail", SqlDbType.VarChar);
                p17.Direction = ParameterDirection.Input;
                p17.Value = data.SecondSupEmail;
                cmd.Parameters.Add(p17);

                // VPHR
                SqlParameter p18 = new SqlParameter("@VpHrApproverEmpId", SqlDbType.Int);
                p18.Direction = ParameterDirection.Input;
                p18.Value = data.VpHrApproverEmpId;
                cmd.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter("@VpHrName", SqlDbType.VarChar);
                p19.Direction = ParameterDirection.Input;
                p19.Value = data.VpHrName;
                cmd.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter("@VpHrApprovalDate", SqlDbType.DateTime);
                p20.Direction = ParameterDirection.Input;
                p20.Value = data.VpHrApprovalDate;
                cmd.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter("@VpHrApproval", SqlDbType.VarChar);
                p21.Direction = ParameterDirection.Input;
                p21.Value = data.VpHrApproval;
                cmd.Parameters.Add(p21);

                SqlParameter p22 = new SqlParameter("@VpHrComment", SqlDbType.VarChar);
                p22.Direction = ParameterDirection.Input;
                p22.Value = data.VpHrComment;
                cmd.Parameters.Add(p22);

                SqlParameter p23 = new SqlParameter("@VphrEmail", SqlDbType.VarChar);
                p23.Direction = ParameterDirection.Input;
                p23.Value = data.VphrEmail;
                cmd.Parameters.Add(p23);


                //RHCFO
                SqlParameter p24 = new SqlParameter("@RhCfoApproverEmpId", SqlDbType.Int);
                p24.Direction = ParameterDirection.Input;
                p24.Value = data.RhCfoApproverEmpId;
                cmd.Parameters.Add(p24);

                SqlParameter p25 = new SqlParameter("@RhCfoName", SqlDbType.VarChar);
                p25.Direction = ParameterDirection.Input;
                p25.Value = data.RhCfoName;
                cmd.Parameters.Add(p25);

                SqlParameter p26 = new SqlParameter("@RhCfoApprovalDate", SqlDbType.DateTime);
                p26.Direction = ParameterDirection.Input;
                p26.Value = data.RhCfoApprovalDate;
                cmd.Parameters.Add(p26);

                SqlParameter p27 = new SqlParameter("@RhCfoApproval", SqlDbType.VarChar);
                p27.Direction = ParameterDirection.Input;
                p27.Value = data.RhCfoApproval;
                cmd.Parameters.Add(p27);

                SqlParameter p28 = new SqlParameter("@RhCfoComment", SqlDbType.VarChar);
                p28.Direction = ParameterDirection.Input;
                p28.Value = data.RhCfoComment;
                cmd.Parameters.Add(p28);

                SqlParameter p29 = new SqlParameter("@RhCfoEmail", SqlDbType.VarChar);
                p29.Direction = ParameterDirection.Input;
                p29.Value = data.RhCfoEmail;
                cmd.Parameters.Add(p29);

                // IPD
                SqlParameter p30 = new SqlParameter("@IpdApproverEmpId", SqlDbType.Int);
                p30.Direction = ParameterDirection.Input;
                p30.Value = data.IpdApproverEmpId;
                cmd.Parameters.Add(p30);

                SqlParameter p31 = new SqlParameter("@IpdName", SqlDbType.VarChar);
                p31.Direction = ParameterDirection.Input;
                p31.Value = data.IpdName;
                cmd.Parameters.Add(p31);

                SqlParameter p32 = new SqlParameter("@IpdApprovalDate", SqlDbType.DateTime);
                p32.Direction = ParameterDirection.Input;
                p32.Value = data.IpdApprovalDate;
                cmd.Parameters.Add(p32);

                SqlParameter p33 = new SqlParameter("@IpdApproval", SqlDbType.VarChar);
                p33.Direction = ParameterDirection.Input;
                p33.Value = data.IpdApproval;
                cmd.Parameters.Add(p33);

                SqlParameter p34 = new SqlParameter("@IpdComment", SqlDbType.VarChar);
                p34.Direction = ParameterDirection.Input;
                p34.Value = data.IpdComment;
                cmd.Parameters.Add(p34);

                SqlParameter p35 = new SqlParameter("@IpdEmail", SqlDbType.VarChar);
                p35.Direction = ParameterDirection.Input;
                p35.Value = data.IpdEmail;
                cmd.Parameters.Add(p35);

                // CIO
                SqlParameter p36 = new SqlParameter("@CioApproverEmpId", SqlDbType.Int);
                p36.Direction = ParameterDirection.Input;
                p36.Value = data.CioApproverEmpId;
                cmd.Parameters.Add(p36);

                SqlParameter p37 = new SqlParameter("@CioName", SqlDbType.VarChar);
                p37.Direction = ParameterDirection.Input;
                p37.Value = data.CioName;
                cmd.Parameters.Add(p37);

                SqlParameter p38 = new SqlParameter("@CioApprovalDate", SqlDbType.DateTime);
                p38.Direction = ParameterDirection.Input;
                p38.Value = data.CioApprovalDate;
                cmd.Parameters.Add(p38);

                SqlParameter p39 = new SqlParameter("@CioApproval", SqlDbType.VarChar);
                p39.Direction = ParameterDirection.Input;
                p39.Value = data.CioApproval;
                cmd.Parameters.Add(p39);

                SqlParameter p40 = new SqlParameter("@CioComment", SqlDbType.VarChar);
                p40.Direction = ParameterDirection.Input;
                p40.Value = data.CioComment;
                cmd.Parameters.Add(p40);

                SqlParameter p41 = new SqlParameter("@CioEmail", SqlDbType.VarChar);
                p41.Direction = ParameterDirection.Input;
                p41.Value = data.CioEmail;
                cmd.Parameters.Add(p41);

                ///REQUEST TYPE
                SqlParameter p42 = new SqlParameter("@RequestType", SqlDbType.VarChar);
                p42.Direction = ParameterDirection.Input;
                p42.Value = data.RequestType;
                cmd.Parameters.Add(p42);

                SqlParameter p43 = new SqlParameter("@CellPhoneDeviceId", SqlDbType.Int);
                p43.Direction = ParameterDirection.Input;
                p43.Value = data.CellPhoneDeviceId;
                cmd.Parameters.Add(p43);

                SqlParameter p44 = new SqlParameter("@CdburnerDeviceID", SqlDbType.Int);
                p44.Direction = ParameterDirection.Input;
                p44.Value = data.CdburnerDeviceID;
                cmd.Parameters.Add(p44);

                SqlParameter p45 = new SqlParameter("@CellPhoneSyncDeviceID", SqlDbType.Int);
                p45.Direction = ParameterDirection.Input;
                p45.Value = data.CellPhoneSyncDeviceID;
                cmd.Parameters.Add(p45);

                SqlParameter p46 = new SqlParameter("@UsbDeviceID", SqlDbType.Int);
                p46.Direction = ParameterDirection.Input;
                p46.Value = data.UsbDeviceID;
                cmd.Parameters.Add(p46);

                SqlParameter p47 = new SqlParameter("@LapTopID", SqlDbType.Int);
                p47.Direction = ParameterDirection.Input;
                p47.Value = data.LapTopID;
                cmd.Parameters.Add(p47);

                SqlParameter p48 = new SqlParameter("@RemoteAccessID", SqlDbType.Int);
                p48.Direction = ParameterDirection.Input;
                p48.Value = data.RemoteAccessID;
                cmd.Parameters.Add(p48);

                SqlParameter p49 = new SqlParameter("@WirelessDeviceID", SqlDbType.Int);
                p49.Direction = ParameterDirection.Input;
                p49.Value = data.WirelessDeviceID;
                cmd.Parameters.Add(p49);

                SqlParameter p50 = new SqlParameter("@LogonUserIdentity", SqlDbType.VarChar);
                p50.Direction = ParameterDirection.Input;
                p50.Value = data.LogonUserIdentity;
                cmd.Parameters.Add(p50);

                SqlParameter p51 = new SqlParameter("@Archive", SqlDbType.Bit);
                p51.Direction = ParameterDirection.Input;
                p51.Value = data.Archive;
                cmd.Parameters.Add(p51);


                //// EXECUTE THE SP
                cmd.ExecuteScalar();
                if (p1 != null && p1.Value != null)
                {
                    returnValue = (int)p1.Value;
                }

            }
            catch (Exception e)
            {
                String Message = e.Message;
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





        public List<IpApprovalRequest> GetRequestFor(string RequestorId)
        {
            List<IpApprovalRequest> retData = new List<IpApprovalRequest>();
            RequestorDbReqAccess requestorDbAccess = new RequestorDbReqAccess(ConnectionString);

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRequestFor";

            SqlParameter p1 = new SqlParameter("@RequestorId", SqlDbType.Int);
            p1.Direction = ParameterDirection.Input;
            // should be requestor id
            p1.Value = RequestorId;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@retVal", SqlDbType.Int);
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
                    retData = ReadData(cmd);
                }
            }
            catch (Exception error)
            {
                String Message = error.Message;
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



        public List<IpApprovalRequest> GetAllRequest()
        {
            List<IpApprovalRequest> retData = new List<IpApprovalRequest>();
            RequestorDbReqAccess requestorDbAccess = new RequestorDbReqAccess(ConnectionString);

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetAllRequest";

            SqlParameter p1 = new SqlParameter("@retVal", SqlDbType.Int);
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
                String Message = error.Message;
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

        public int GetRequestorPendingCount(String EmpId)
        {
            int retData = 0;
            RequestorDbReqAccess requestorDbAccess = new RequestorDbReqAccess(ConnectionString);

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRequestorPendingCount";

            SqlParameter p1 = new SqlParameter("@retVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            // should be requestor id
            cmd.Parameters.Add(p1);

            int id = 0;
            int.TryParse(EmpId, out id);
            SqlParameter p2 = new SqlParameter("@EmpId", SqlDbType.Int);
            p2.Direction = ParameterDirection.Input;
            p2.Value = id;
            // should be requestor id
            cmd.Parameters.Add(p2);

            try
            {
                con.Open();
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    retData = ReadCountValue(cmd);
                    if (p2 != null && p2.Value != null)
                    {
                        retData = (int)p2.Value;
                    }
                }
            }
            catch (Exception error)
            {
                String Message = error.Message;
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

        public IpApprovalRequest GetApprovalRequest(String ApprovalRequestId)
        {
            List<IpApprovalRequest> retData = null;
            int RetVal;
            RequestorDbReqAccess requestorDbAccess = new RequestorDbReqAccess(ConnectionString);

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRequest";

            SqlParameter p1 = new SqlParameter("@retVal", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            // should be requestor id
            cmd.Parameters.Add(p1);

            int id = 0;
            int.TryParse(ApprovalRequestId, out id);
            SqlParameter p2 = new SqlParameter("@ApprovalRequestId", SqlDbType.Int);
            p2.Direction = ParameterDirection.Input;
            p2.Value = id;
            // should be requestor id
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
                String Message = error.Message;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }
            if (retData != null && retData.Count > 0)
            {
                return retData[0];
            }
            return null;

        }

        public bool ChangeState(int IpApprovalRequestId, String oldState, String newState)
        {
            int RetVal = 0;

            IpApprovalRequest request = GetApprovalRequest(IpApprovalRequestId.ToString());

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateApprovalRequestState";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@IpApprovalRequestId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = IpApprovalRequestId;
                cmd.Parameters.Add(p1);

                //----------------------
                SqlParameter p2 = new SqlParameter("@FirstSupUpdate", SqlDbType.Bit);
                p2.Direction = ParameterDirection.Input;
                p2.Value = 0;
                if (request.FirstSupApproval == oldState)
                {
                    p2.Value = 1;
                }
                cmd.Parameters.Add(p2);
                SqlParameter p3 = new SqlParameter("@FirstSupApproval", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = newState;
                cmd.Parameters.Add(p3);

                //------------------------------------
                SqlParameter p4 = new SqlParameter("@SecondSupUpdate", SqlDbType.Bit);
                p4.Direction = ParameterDirection.Input;
                p4.Value = 0;
                if (request.SecondSupApproval == oldState)
                {
                    p4.Value = 1;
                }
                cmd.Parameters.Add(p4);
                SqlParameter p5 = new SqlParameter("@SecondSupApproval", SqlDbType.VarChar);
                p5.Direction = ParameterDirection.Input;
                p5.Value = newState;
                cmd.Parameters.Add(p5);

                //-------------------------------
                SqlParameter p6 = new SqlParameter("@VpHrUpdate", SqlDbType.Bit);
                p6.Direction = ParameterDirection.Input;
                p6.Value = 0;
                if (request.VpHrApproval == oldState)
                {
                    p6.Value = 1;
                }
                cmd.Parameters.Add(p6);
                SqlParameter p7 = new SqlParameter("@VpHrApproval", SqlDbType.VarChar);
                p7.Direction = ParameterDirection.Input;
                p7.Value = newState;
                cmd.Parameters.Add(p7);
                //-------------------------------
                SqlParameter p8 = new SqlParameter("@RhCfoUpdate", SqlDbType.Bit);
                p8.Direction = ParameterDirection.Input;
                p8.Value = 0;
                if (request.RhCfoApproval == oldState)
                {
                    p8.Value = 1;
                }
                cmd.Parameters.Add(p8);
                SqlParameter p9 = new SqlParameter("@RhCfoApproval", SqlDbType.VarChar);
                p9.Direction = ParameterDirection.Input;
                p9.Value = newState;
                cmd.Parameters.Add(p9);
                //-------------------------------
                SqlParameter p10 = new SqlParameter("@IpdUpdate", SqlDbType.Bit);
                p10.Direction = ParameterDirection.Input;
                p10.Value = 0;
                if (request.IpdApproval == oldState)
                {
                    p10.Value = 1;
                }
                cmd.Parameters.Add(p10);
                SqlParameter p11 = new SqlParameter("@IpdApproval", SqlDbType.VarChar);
                p11.Direction = ParameterDirection.Input;
                p11.Value = newState;
                cmd.Parameters.Add(p11);
                //-------------------------------
                SqlParameter p12 = new SqlParameter("@CioUpdate", SqlDbType.Bit);
                p12.Direction = ParameterDirection.Input;
                p12.Value = 0;
                if (request.CioApproval == oldState)
                {
                    p12.Value = 1;
                }
                cmd.Parameters.Add(p12);
                SqlParameter p13 = new SqlParameter("@CioApproval", SqlDbType.VarChar);
                p13.Direction = ParameterDirection.Input;
                p13.Value = newState;
                cmd.Parameters.Add(p13);

                SqlParameter p14 = new SqlParameter("@RetVal", SqlDbType.Int);
                p14.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p14);


                cmd.ExecuteScalar();

                if (p14 != null && p14.Value != null)
                {
                    RetVal = (int)p14.Value;
                }

            }
            catch (Exception ex)
            {
            }
            return RetVal > 0;
        }

        public bool InitApprovalRequest(int DeviceId, String RequestType, String state)
        {
            int returnValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InitApprovalRequestState";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@DeviceId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = DeviceId;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@RequestType", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = RequestType;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@State", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = state;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@RetVal", SqlDbType.Int);
                p4.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p4);

                cmd.ExecuteScalar();

                if (p4 != null && p4.Value != null)
                {
                    returnValue = (int)p4.Value;
                }

            }
            catch (Exception ex)
            {
            }
            return returnValue > 0;
        }


        public IpApprovalRequest GetApprovalRequestByDeviceId(int DeviceId, String RequestType)
        {
            int RetVal = 0;

            List<IpApprovalRequest> retData = null;

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetApprovalRequestByDeviceId";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p1 = new SqlParameter("@DeviceId", SqlDbType.Int);
            p1.Direction = ParameterDirection.Input;
            p1.Value = DeviceId;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@RequestType", SqlDbType.VarChar);
            p2.Direction = ParameterDirection.Input;
            p2.Value = RequestType;
            cmd.Parameters.Add(p2);

            SqlParameter p3 = new SqlParameter("@RetVal", SqlDbType.Int);
            p3.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p3);

            try
            {
                con.Open();
                if (cmd != null)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    retData = ReadData(cmd);
                    if (p3 != null && p3.Value != null)
                    {
                        RetVal = (int)p3.Value;
                    }
                }
            }
            catch (Exception error)
            {
                String Message = error.Message;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }
            if (retData != null && retData.Count > 0)
            {
                return retData[0];
            }
            return null;
        }

        public bool UpdateFirstSupRequest(IpApprovalRequest item)
        {
            int returnValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateReqFirstSupStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@IpApprovalRequestId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = item.IpApprovalRequestId;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@FirstSupApproval", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = item.FirstSupApproval;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@FirstSupComment", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = item.FirstSupComment;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@RETVALUE", SqlDbType.Int);
                p4.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p4);

                cmd.ExecuteScalar();

                if (p4 != null && p4.Value != null)
                {
                    returnValue = (int)p4.Value;
                }

            }
            catch (Exception ex)
            {
            }
            return returnValue == 0 ? false : true;
        }

        public bool UpdateSecondSupRequest(IpApprovalRequest item)
        {
            int returnValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateReqSecondSupStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@IpApprovalRequestId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = item.IpApprovalRequestId;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@SecondSupApproval", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = item.SecondSupApproval;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@SecondSupComment", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = item.SecondSupComment;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@RETVALUE", SqlDbType.Int);
                p4.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p4);

                cmd.ExecuteScalar();

                if (p4 != null && p4.Value != null)
                {
                    returnValue = (int)p4.Value;
                }
            }
            catch (Exception ex)
            {
            }
            return returnValue == 0 ? false : true;
        }

        public bool UpdateVphrRequest(IpApprovalRequest item)
        {
            int returnValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateReqVphrStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@IpApprovalRequestId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = item.IpApprovalRequestId;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@VphrApproval", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = item.VpHrApproval;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@VphrComment", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = item.VpHrComment;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@RETVALUE", SqlDbType.Int);
                p4.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p4);

                cmd.ExecuteScalar();

                if (p4 != null && p4.Value != null)
                {
                    returnValue = (int)p4.Value;
                }
            }
            catch (Exception ex)
            {
            }
            return returnValue == 0 ? false : true;
        }

        public bool UpdateRhCfoRequest(IpApprovalRequest item)
        {
            int returnValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateReqRhCfoStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@IpApprovalRequestId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = item.IpApprovalRequestId;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@RhCfoApproval", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = item.RhCfoApproval;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@RhCfoComment", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = item.RhCfoComment;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@RETVALUE", SqlDbType.Int);
                p4.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p4);

                cmd.ExecuteScalar();

                if (p4 != null && p4.Value != null)
                {
                    returnValue = (int)p4.Value;
                }
            }
            catch (Exception ex)
            {
            }
            return returnValue == 0 ? false : true;
        }
        public bool UpdateIpdRequest(IpApprovalRequest item)
        {
            int returnValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateReqIpdStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@IpApprovalRequestId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = item.IpApprovalRequestId;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@IpdApproval", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = item.IpdApproval;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@IpdComment", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = item.IpdComment;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@RETVALUE", SqlDbType.Int);
                p4.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p4);

                cmd.ExecuteScalar();

                if (p4 != null && p4.Value != null)
                {
                    returnValue = (int)p4.Value;
                }
            }
            catch (Exception ex)
            {
            }
            return returnValue == 0 ? false : true;
        }

        public bool UpdateCioRequest(IpApprovalRequest item)
        {
            int returnValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UpdateReqCioStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                cmd.Connection = con;

                SqlParameter p1 = new SqlParameter("@IpApprovalRequestId", SqlDbType.Int);
                p1.Direction = ParameterDirection.Input;
                p1.Value = item.IpApprovalRequestId;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@CioApproval", SqlDbType.VarChar);
                p2.Direction = ParameterDirection.Input;
                p2.Value = item.CioApproval;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@CioComment", SqlDbType.VarChar);
                p3.Direction = ParameterDirection.Input;
                p3.Value = item.CioComment;
                cmd.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@RETVALUE", SqlDbType.Int);
                p4.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p4);

                cmd.ExecuteScalar();

                if (p4 != null && p4.Value != null)
                {
                    returnValue = (int)p4.Value;
                }
            }
            catch (Exception ex)
            {
            }
            return returnValue == 0 ? false : true;
        }



        public List<IpApprovalRequest> GetRequestByState(String state)
        {
            List<IpApprovalRequest> retData = new List<IpApprovalRequest>();
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRequestBasedOnState";

            SqlParameter p1 = new SqlParameter("@State", SqlDbType.VarChar);
            p1.Direction = ParameterDirection.Input;
            // should be requestor id
            p1.Value = state;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@retVal", SqlDbType.Int);
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

        public List<IpApprovalRequest> GetApproversData(string EmpID)
        {
            List<IpApprovalRequest> retData = new List<IpApprovalRequest>();
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetApproversData";

            SqlParameter p1 = new SqlParameter("@EmpID", SqlDbType.Int);
            p1.Direction = ParameterDirection.Input;
            // should be requestor id
            p1.Value = EmpID;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@retVal", SqlDbType.Int);
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


        private List<IpApprovalRequest> ReadData(SqlCommand cmd)
        {
            List<IpApprovalRequest> requests = new List<IpApprovalRequest>();
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

            DateTime TmpDate;
            while (reader.Read())
            {
                IpApprovalRequest request = new IpApprovalRequest();
                request.IpApprovalRequestId = (int)reader["IpApprovalRequestId"];
                request.IpRequestorId = (int)reader["IpRequestorId"];

                if (reader["SubmitDate"] != DBNull.Value)
                {
                     TmpDate = ((DateTime)reader["SubmitDate"]);
                     request.SubmitDate = TmpDate.Date;
                }
                else
                {
                    request.SubmitDate = DateTime.Now.Date;
                }

                if (reader["GrantDate"] != DBNull.Value)
                {
                    TmpDate = ((DateTime)reader["GrantDate"]);
                    request.GrantDate = TmpDate.Date;
                }
                else
                {
                    request.GrantDate = DateTime.Now.Date;
                }

                if (reader["ReturnDate"] != DBNull.Value)
                {
                    TmpDate = ((DateTime)reader["ReturnDate"]);
                    request.ReturnDate = TmpDate.Date;
                }
                else
                {
                    request.ReturnDate = DateTime.Now.Date;
                }
                /// FIRST SUPERVISOR
                request.FirstSupEmpId = (int)reader["FirstSupEmpId"];
                if (reader["FirstSupApproval"] != DBNull.Value)
                {
                    request.FirstSupApproval = (String)reader["FirstSupApproval"];
                }
                else
                {
                    request.FirstSupApproval = String.Empty;
                }

                if (reader["FirstSupApprovalDate"] != DBNull.Value)
                {
                    TmpDate = (DateTime)reader["FirstSupApprovalDate"];
                    request.FirstSupApprovalDate = TmpDate.Date;
                }
                else
                {
                    request.FirstSupApprovalDate = DateTime.Now.Date;
                }
                if (reader["FirstSupEmail"] != DBNull.Value)
                {
                    request.FirstSupEmail = (String)reader["FirstSupEmail"];
                }
                else
                {
                    request.FirstSupEmail = String.Empty;
                }
                if (reader["FirstSupName"] != DBNull.Value)
                {
                    request.FirstSupName = (String)reader["FirstSupName"];
                }
                else
                {
                    request.FirstSupName = String.Empty;
                }
                if (reader["FirstSupComment"] != DBNull.Value)
                {
                    request.FirstSupComment = (String)reader["FirstSupComment"];
                }
                else
                {
                    request.FirstSupComment = String.Empty;
                }


                /// SECOND SUPERVISOR
                request.SecondSupEmpId = (int)reader["SecondSupEmpId"];
                if (reader["SecondSupApproval"] != DBNull.Value)
                {
                    request.SecondSupApproval = (String)reader["SecondSupApproval"];
                }
                else
                {
                    request.SecondSupApproval = String.Empty;
                }

                if (reader["SecondSupApprovalDate"] != DBNull.Value)
                {
                    TmpDate = (DateTime)reader["SecondSupApprovalDate"];
                    request.SecondSupApprovalDate = TmpDate.Date;
                }
                else
                {
                    request.SecondSupApprovalDate = DateTime.Now.Date;
                }
                if (reader["SecondSupEmail"] != DBNull.Value)
                {
                    request.SecondSupEmail = (String)reader["SecondSupEmail"];
                }
                else
                {
                    request.SecondSupEmail = String.Empty;
                }
                if (reader["SecondSupName"] != DBNull.Value)
                {
                    request.SecondSupName = (String)reader["SecondSupName"];
                }
                else
                {
                    request.SecondSupName = String.Empty;
                }
                if (reader["SecondSupComment"] != DBNull.Value)
                {
                    request.SecondSupComment = (String)reader["SecondSupComment"];
                }
                else
                {
                    request.SecondSupComment = String.Empty;
                }

                /// VPHR
                request.VpHrApproverEmpId = (int)reader["VpHrApproverEmpId"];
                if (reader["VpHrApproval"] != DBNull.Value)
                {
                    request.VpHrApproval = (String)reader["VpHrApproval"];
                }
                else
                {
                    request.VpHrApproval = String.Empty;
                }
                if (reader["VpHrApprovalDate"] != DBNull.Value)
                {
                    TmpDate = (DateTime)reader["VpHrApprovalDate"];
                    request.VpHrApprovalDate = TmpDate.Date;
                }
                else
                {
                    request.VpHrApprovalDate = DateTime.Now.Date;
                }
                if (reader["VphrEmail"] != DBNull.Value)
                {
                    request.VphrEmail = (String)reader["VphrEmail"];
                }
                else
                {
                    request.VphrEmail = String.Empty;
                }
                if (reader["VpHrName"] != DBNull.Value)
                {
                    request.VpHrName = (String)reader["VpHrName"];
                }
                else
                {
                    request.VpHrName = String.Empty;
                }
                if (reader["VpHrComment"] != DBNull.Value)
                {
                    request.VpHrComment = (String)reader["VpHrComment"];
                }
                else
                {
                    request.VpHrComment = String.Empty;
                }

                /// RHCFO
                request.RhCfoApproverEmpId = (int)reader["RhCfoApproverEmpId"];
                if (reader["RhCfoApproval"] != DBNull.Value)
                {
                    request.RhCfoApproval = (String)reader["RhCfoApproval"];
                }
                else
                {
                    request.RhCfoApproval = String.Empty;
                }
                if (reader["RhCfoApprovalDate"] != DBNull.Value)
                {
                    TmpDate = (DateTime)reader["RhCfoApprovalDate"];
                    request.RhCfoApprovalDate = TmpDate.Date;
                }
                else
                {
                    request.RhCfoApprovalDate = DateTime.Now.Date;
                }
                if (reader["RhCfoEmail"] != DBNull.Value)
                {
                    request.RhCfoEmail = (String)reader["RhCfoEmail"];
                }
                else
                {
                    request.RhCfoEmail = String.Empty;
                }
                if (reader["RhCfoName"] != DBNull.Value)
                {
                    request.RhCfoName = (String)reader["RhCfoName"];
                }
                else
                {
                    request.RhCfoName = String.Empty;
                }
                if (reader["RhCfoComment"] != DBNull.Value)
                {
                    request.RhCfoComment = (String)reader["RhCfoComment"];
                }
                else
                {
                    request.RhCfoComment = String.Empty;
                }

                /// CIO
                request.CioApproverEmpId = (int)reader["CioApproverEmpId"];
                if (reader["CioApproval"] != DBNull.Value)
                {
                    request.CioApproval = (String)reader["CioApproval"];
                }
                else
                {
                    request.CioApproval = String.Empty;
                }
                if (reader["CioApprovalDate"] != DBNull.Value)
                {
                    TmpDate = (DateTime)reader["CioApprovalDate"];
                    request.CioApprovalDate = TmpDate.Date;
                }
                else
                {
                    request.CioApprovalDate = DateTime.Now.Date;
                }
                if (reader["CioEmail"] != DBNull.Value)
                {
                    request.CioEmail = (String)reader["CioEmail"];
                }
                else
                {
                    request.CioEmail = String.Empty;
                }
                if (reader["CioName"] != DBNull.Value)
                {
                    request.CioName = (String)reader["CioName"];
                }
                else
                {
                    request.CioName = String.Empty;
                }
                if (reader["CioComment"] != DBNull.Value)
                {
                    request.CioComment = (String)reader["CioComment"];
                }
                else
                {
                    request.CioComment = String.Empty;
                }

                /// IPD
                request.IpdApproverEmpId = (int)reader["IpdApproverEmpId"];
                if (reader["IpdApproval"] != DBNull.Value)
                {
                    request.IpdApproval = (String)reader["IpdApproval"];
                }
                else
                {
                    request.IpdApproval = String.Empty;
                }
                if (reader["IpdApprovalDate"] != DBNull.Value)
                {
                    TmpDate = (DateTime)reader["IpdApprovalDate"];
                    request.IpdApprovalDate = TmpDate.Date;
                }
                else
                {
                    request.IpdApprovalDate = DateTime.Now.Date;
                }
                if (reader["IpdEmail"] != DBNull.Value)
                {
                    request.IpdEmail = (String)reader["IpdEmail"];
                }
                else
                {
                    request.IpdEmail = String.Empty;
                }
                if (reader["IpdName"] != DBNull.Value)
                {
                    request.IpdName = (String)reader["IpdName"];
                }
                else
                {
                    request.IpdName = String.Empty;
                }
                if (reader["RequestType"] != DBNull.Value)
                {
                    request.RequestType = (String)reader["RequestType"];
                }
                else
                {
                    request.RequestType = String.Empty;
                }
                if (reader["IpdComment"] != DBNull.Value)
                {
                    request.IpdComment = (String)reader["IpdComment"];
                }
                else
                {
                    request.IpdComment = String.Empty;
                }
                if (reader["LogonUserIdentity"] != DBNull.Value)
                {
                    request.LogonUserIdentity = (String)reader["LogonUserIdentity"];
                }
                else
                {
                    request.LogonUserIdentity = String.Empty;
                }
                request.Archive = (bool)reader["Archive"];

                request.CellPhoneDeviceId = (int)reader["CellPhoneDeviceId"];
                request.CdburnerDeviceID = (int)reader["CdburnerDeviceID"];
                request.CellPhoneSyncDeviceID = (int)reader["CellPhoneSyncDeviceID"];
                request.UsbDeviceID = (int)reader["UsbDeviceID"];
                request.LapTopID = (int)reader["LapTopID"];
                request.RemoteAccessID = (int)reader["RemoteAccessID"];
                request.WirelessDeviceID = (int)reader["WirelessDeviceID"];
                requests.Add(request);
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return requests;
        }


        private int ReadCountValue(SqlCommand cmd)
        {
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return 0;
            }
            catch (System.InvalidOperationException)
            {
                return 0;
            }

            int NumOfRequest = 0; ;
            while (reader.Read())
            {
                NumOfRequest = (int)reader["NumOfRequest"];
            }
            return NumOfRequest;
        }


    }
}
