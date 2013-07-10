using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using WMC.Core.Util.NativeWrapper2008;
using System.Collections;

namespace WMC.Core.DataProvider2008
{
    /// <summary>
    /// Summary description for SQLDataProvider.
    /// </summary>
    public abstract class SQLDataProvider : CacheDataProvider
    {

        public static string ROWS_AFFECTED = "rows_affected";
        public string HASHKEY_RESULTSET = "results";

        private SqlConnection transactionConnection = null;
        private SqlTransaction transactionObj = null;

        //  Get a connection, based on the environment type
        public SqlConnection GetConnection()
        {

            //  if we are in a transaction, return that connection
            if (transactionConnection != null)
            {
                return transactionConnection;
            }



            //  get the connection string from the web.config file
            string connectionStringKey = GetConnectionStringKey();
            string connectionString = ConfigurationManager.AppSettings[connectionStringKey];
            
            //  get a SqlConnection
            SqlConnection c = new SqlConnection(connectionString);

            //  return it
            return c;

        }   

        public override void StartTransaction()
        {

            //  calling start when the transaction is already open throws an exception
            if (transactionConnection != null)
            {
                throw new Exception("Cannot open transaction; there is already one open.");
            }

            //  set the member connection to a live connection
            transactionConnection = GetConnection();
            transactionConnection.Open();
            transactionObj = transactionConnection.BeginTransaction();
        }

        public override void CommitTransaction()
        {

            //  calling commit without a start is bad
            if (transactionConnection == null)
            {
                throw new Exception("Cannot commit transaction; there is not one that has been started.");
            }

            //  commit the transaction, and release the member variables
            transactionObj.Commit();
            if (transactionConnection != null && transactionConnection.State == ConnectionState.Open)
            {
                transactionConnection.Close();
                transactionConnection = null;
                transactionObj = null;
            }
        }

        public override void RollbackTransaction()
        {

            //  calling commit without a start is bad
            if (transactionConnection == null)
            {
                throw new Exception("Cannot rollback transaction; there is not one that has been started.");
            }

            //  commit the transaction, and release the member variables
            transactionObj.Rollback();
            if (transactionConnection != null && transactionConnection.State == ConnectionState.Open)
            {
                try
                {
                    transactionConnection.Close();
                    transactionConnection = null;
                    transactionObj = null;
                }
                catch (Exception ex)
                {
                    //  do something?
                    throw ex;
                }
            }
        }




        protected virtual string GetConnectionStringKey()
        {

            //  the connection string key is of the format "<classname>.env"
            return "ConnectionString.default";

        }


        protected SqlParameter GetParameterInputChar(string paramName, CharObj paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.Char);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue.CharValue;
            }

            return sp;

        }


        protected SqlParameter GetParameterInputInt(string paramName, BoolObj paramValue, int trueValue, int falseValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.Int);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                if (paramValue.BoolValue)
                {
                    sp.Value = trueValue;
                }
                else
                {
                    sp.Value = falseValue;
                }
            }

            return sp;

        }



        protected SqlParameter GetParameterInputBit(string paramName, BoolObj paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.Bit);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue.BoolValue;
            }

            return sp;

        }

        protected SqlParameter GetParameterInputShort(string paramName, ShortObj paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.SmallInt);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue.ShortValue;
            }

            return sp;

        }

        protected SqlParameter GetParameterInputInt(string paramName, int paramValue)
        {
            SqlParameter sp = new SqlParameter(paramName, SqlDbType.Int);
            sp.Direction = ParameterDirection.Input;
            sp.Value = paramValue;
            return sp;
        }

        protected SqlParameter GetParameterInputInt(string paramName, IntObj paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.Int);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue.IntValue;
            }

            return sp;

        }

        protected SqlParameter GetParameterInputDouble(string paramName, DoubleObj paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.Float);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue.DoubleValue;
            }

            return sp;

        }

        protected SqlParameter GetParameterInputLong(string paramName, LongObj paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.BigInt);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue.LongValue;
            }

            return sp;

        }

        protected SqlParameter GetParameterInputDate(string paramName, DateTimeObj paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.DateTime);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue.DateTimeValue;
            }

            return sp;

        }


        protected SqlParameter GetParameterInputGuid(string paramName, GuidObj paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.UniqueIdentifier);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue.Guid;
            }
            return sp;
        }


        protected SqlParameter GetParameterInputString(string paramName, string paramValue)
        {

            SqlParameter sp = new SqlParameter(paramName, SqlDbType.VarChar);
            sp.Direction = ParameterDirection.Input;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue;
            }
            return sp;
        }

        protected SqlParameter GetParameterOutputInt(string paramName, IntObj paramValue)
        {
            SqlParameter sp = new SqlParameter(paramName, SqlDbType.Int);
            sp.Direction = ParameterDirection.Output;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue;
            }
            return sp;
        }

        protected SqlParameter GetParameterReturnValueInt(string paramName)
        {
            SqlParameter sp = new SqlParameter(paramName, SqlDbType.Int);
            sp.Direction = ParameterDirection.ReturnValue;
            return sp;
        }


        protected SqlParameter GetParameterOutputString(string paramName, string paramValue)
        {
            SqlParameter sp = new SqlParameter(paramName, SqlDbType.VarChar, 255);
            sp.Direction = ParameterDirection.Output;
            if (paramValue == null)
            {
                sp.Value = DBNull.Value;
            }
            else
            {
                sp.Value = paramValue;
            }
            return sp;
        }


        protected BoolObj GetBool(object i)
        {

            //  try as boolean first
            try
            {
                return new BoolObj(System.Boolean.Parse(i.ToString()));
            }
            catch (Exception) { }

            IntObj asInt = GetInt(i);
            if (asInt == null)
            {
                return null;
            }
            if (asInt.IntValue == 0)
            {
                return new BoolObj(false);
            }
            return new BoolObj(true);
        }


        protected IntObj GetInt(object i)
        {
            if (i == null) { return null; }
            try
            {
                return new IntObj(Int32.Parse(i.ToString()));
            }
            catch (Exception)
            {

            }
            return null;
        }

        protected DoubleObj GetDouble(object i)
        {
            if (i == null) { return null; }
            try
            {
                return new DoubleObj(Double.Parse(i.ToString()));
            }
            catch (Exception)
            {

            }
            return null;
        }

        protected LongObj GetLong(object i)
        {
            if (i == null) { return null; }
            try
            {
                return new LongObj(Int64.Parse(i.ToString()));
            }
            catch (Exception)
            {

            }
            return null;
        }

        protected DateTimeObj GetDate(object i)
        {
            if (i == null || i == DBNull.Value) { return null; }
            try
            {
                return DateTimeObj.FromObj(i);
            }
            catch (Exception)
            {

            }
            return null;
        }

        protected CharObj GetChar(object i)
        {
            if (i == null || i == DBNull.Value) { return null; }

            return CharObj.FromObj(i);
        }

        protected string GetString(object i)
        {
            //  Changed calonso; if (i==null) {return "";}
            if (i == null || i == DBNull.Value) { return null; }

            return i.ToString();
        }

        protected string GetStringTrim(object i)
        {
            //  Changed calonso; if (i==null) {return "";}
            if (i == null || i == DBNull.Value) { return null; }

            return i.ToString().Trim();
        }

        protected Hashtable ExecuteNonQuery(string spName, ArrayList sqlParams)
        {

            //  this is going to hold the outparams and return values
            Hashtable returnHash = new Hashtable();
            SqlConnection conn = null;
            try
            {

                //  Get a connection
                conn = GetConnection();
                //  this may be already open if we are in a transaction
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                //  Build a SqlCommand
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                //Added to see if timeout issue is resolved - Ogechi 6/20/2008
                cmd.CommandTimeout = 400;

                if (InTransaction())
                {
                    cmd.Transaction = transactionObj;
                }

                //  Add the parameters
                if (sqlParams != null)
                {
                    for (int i = 0; i < sqlParams.Count; i++)
                    {
                        cmd.Parameters.Add(sqlParams[i]);
                    }
                }

                //  run the command
                int rowsAffected = cmd.ExecuteNonQuery();

                //  add the rowsreturned to the hash
                returnHash.Add(ROWS_AFFECTED, rowsAffected);

                //  Build the hashtable from each return param
                if (sqlParams != null)
                {
                    for (int i = 0; i < sqlParams.Count; i++)
                    {
                        SqlParameter param = (SqlParameter)sqlParams[i];
                        if (param.Direction == ParameterDirection.Output ||
                            param.Direction == ParameterDirection.InputOutput ||
                            param.Direction == ParameterDirection.ReturnValue)
                        {
                            returnHash.Add(param.ParameterName, param.Value);
                        }
                    }
                }


            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (!InTransaction())
                {
                    //  Now clean up
                    if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
                }

            }


            //  return the data
            return returnHash;
        }

        protected Hashtable ExecDataTableWithOutparamsAndResultset(string spName, ArrayList sqlParams)
        {

            Hashtable returnHash = new Hashtable();

            //  Init stuff
            SqlConnection conn = null;
            DataTable dt = null;

            try
            {
                //  Get a connection  (should this come from the inherited class?)
                conn = GetConnection();
                //  this may be already open if we are in a transaction
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                //  Build a SqlCommand
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //Added to see if timeout issue is resolved - Ogechi 6/20/2008
                cmd.CommandTimeout = 400;

                if (InTransaction())
                {
                    cmd.Transaction = transactionObj;
                }

                //  Add the parameters
                if (sqlParams != null)
                {
                    for (int i = 0; i < sqlParams.Count; i++)
                    {
                        cmd.Parameters.Add(sqlParams[i]);
                    }
                }

                //  Build a SqlDataAdapter
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                //  run the command
                //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dt = ds.Tables[0];

                //  Build the hashtable from each return param
                if (sqlParams != null)
                {
                    for (int i = 0; i < sqlParams.Count; i++)
                    {
                        SqlParameter param = (SqlParameter)sqlParams[i];
                        if (param.Direction == ParameterDirection.Output ||
                            param.Direction == ParameterDirection.InputOutput ||
                            param.Direction == ParameterDirection.ReturnValue)
                        {
                            returnHash.Add(param.ParameterName, param.Value);
                        }
                    }
                }

                returnHash.Add(HASHKEY_RESULTSET, dt);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                if (!InTransaction())
                {
                    if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
                }

            }

            //  return the table
            return returnHash;

        }

        protected DataTable ExecDataTable(string spName, ArrayList sqlParams)
        {
            //  Init stuff
            SqlConnection conn = null;
            DataTable dt = null;

            try
            {
                //  Get a connection  (should this come from the inherited class?)
                conn = GetConnection();
                //  this may be already open if we are in a transaction
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                //  Build a SqlCommand
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //Added to see if timeout issue is resolved - Ogechi 6/20/2008
                cmd.CommandTimeout = 400;

                if (InTransaction())
                {
                    cmd.Transaction = transactionObj;
                }

                //  Add the parameters
                if (sqlParams != null)
                {
                    for (int i = 0; i < sqlParams.Count; i++)
                    {
                        cmd.Parameters.Add(sqlParams[i]);
                    }
                }

                //  Build a SqlDataAdapter
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                //  run the command
                //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                if (!InTransaction())
                {
                    if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
                }

            }

            //  return the table
            return dt;

        }

        ~SQLDataProvider()
        {
            if (InTransaction())
            {
                System.Diagnostics.Debug.WriteLine("***  WARNING: SQLDataProvider transaction was not explicitly closed, so it is being rolled back in the object finalizer!");
                this.RollbackTransaction();
            }
        }
        protected bool InTransaction()
        {
            return transactionConnection != null;
        }

        public SQLDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
