using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LancooDemo.Common
{
    public class SqlHelper : IDisposable
    {
        private static string CONN = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        private string connStr;
        private SqlConnection conn;
        private SqlTransaction tran;

        private bool disposed = false;

        /// <summary>
        /// Construction of SqlHelper.
        /// </summary>
        /// <param name="connStr">the connection string.</param>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public SqlHelper()
        {
            this.connStr = CONN;
        }

        public void Open(bool useTransaction = false)
        {
            if (disposed) return;

            conn = new SqlConnection(connStr);
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("开启SqlConnection失败，connStr=" + conn.ConnectionString, ex);
                }
                if (useTransaction && tran == null)
                    tran = conn.BeginTransaction();
            }
        }

        public DataTable QueryCommand(string cmdStr, CommandType cmdType, ICollection<IDataParameter> parameters = null)
        {
            IDataParameter param = null;
            return QueryCommand(cmdStr, cmdType, parameters, ref param);
        }

        /// <summary>
        /// Execute sql for query.
        /// </summary>
        /// <param name="cmdStr">Command string.</param>
        /// <param name="cmdType">CommandType( Sql procedure, text)</param>
        /// <param name="parameters">parameters needed.</param>
        /// <param name="returnValue">return parameter</param>
        /// <returns>the datatable result.</returns>
        public DataTable QueryCommand(string cmdStr, CommandType cmdType,
            ICollection<IDataParameter> parameters, ref IDataParameter returnValue)
        {
            if (disposed)
                return null;

            SqlCommand cmd = GetCommand(cmdStr, cmdType, parameters, returnValue);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                string msg = "Sql execute error：" + ex.Message + ", cmdStr:" + cmdStr + ", parameters：";
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        msg += param.ParameterName + "--" + param.Value + ",";
                    }
                }
                throw new Exception(msg, ex);
            }
            return dt;
        }

        public int RunCommand(string cmdStr, CommandType cmdType, ICollection<IDataParameter> parameters = null)
        {
            IDataParameter param = null;
            return RunCommand(cmdStr, cmdType, parameters, ref param);
        }


        /// <summary>
        /// Execute sql for insert, delete, update.
        /// <para>return rows count affected.</para>
        /// </summary>
        /// <param name="cmdStr">Command string.</param>
        /// <param name="cmdType">CommandType(SqlProcedure, Text)</param>
        /// <param name="parameters">parameters needed.</param>
        /// <param name="returnValue">return parameter.</param>
        /// <returns></returns>
        public int RunCommand(string cmdStr, CommandType cmdType,
            ICollection<IDataParameter> parameters, ref IDataParameter returnValue)
        {
            if (disposed)
                return -1;

            SqlCommand cmd = GetCommand(cmdStr, cmdType, parameters, returnValue);

            int result;
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = "Sql execute error：" + ex.Message + ", cmdStr:" + cmdStr + ", parameters：";
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        msg += param.ParameterName + "--" + param.Value + ",";
                    }
                }

                throw new Exception(msg, ex);
            }

            return result;
        }


        /// <summary>
        /// Generate an instance of sql command.
        /// </summary>
        /// <param name="cmdStr">command string.</param>
        /// <param name="cmdType"><see cref="CommandType"/></param>
        /// <returns></returns>
        public SqlCommand GetCommand(string cmdStr, CommandType cmdType)
        {
            if (disposed)
                return null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdStr;
            cmd.CommandType = cmdType;

            if (tran != null)
                cmd.Transaction = tran;

            return cmd;
        }



        private SqlCommand GetCommand(string cmdStr, CommandType cmdType,
            ICollection<IDataParameter> parameters, IDataParameter returnValue)
        {
            SqlCommand cmd = GetCommand(cmdStr, cmdType);

            if (parameters != null)
                SetParameter(cmd, parameters);

            if (returnValue != null)
                cmd.Parameters.Add(returnValue);

            return cmd;
        }


        /// <summary>
        /// Set parameters for sql command.
        /// </summary>
        /// <param name="cmd">the instance of sql command.</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        private SqlCommand SetParameter(SqlCommand cmd, ICollection<IDataParameter> parameters)
        {
            foreach (SqlParameter param in parameters)
            {
                if (param.Direction == ParameterDirection.Input && param.Value == null)
                    param.Value = DBNull.Value;
                cmd.Parameters.Add(param);
            }
            return cmd;
        }



        /// <summary>
        /// Begin transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        public void BeginTransaction()
        {
            if (disposed)
                return;

            if (conn == null || conn.State != ConnectionState.Open)
                throw new InvalidOperationException("There is no opened sql connection.");

            if (tran != null)
                throw new InvalidOperationException("There is no transaction opened.");

            tran = conn.BeginTransaction();
        }


        /// <summary>
        /// Submit current transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        public void CommitTransaction()
        {
            if (disposed)
                return;
            if (tran == null)
                return;
            if (conn == null || conn.State != ConnectionState.Open)
                throw new InvalidOperationException("There is no opened sql connection.");

            if (tran == null)
                throw new InvalidOperationException("There is no transaction opened.");

            tran.Commit();
            tran = null;
        }


        /// <summary>
        /// Rollback current transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        public void RollbackTransaction()
        {
            if (disposed)
                return;

            if (tran == null)
                return;

            if (conn == null || conn.State != ConnectionState.Open)
                throw new InvalidOperationException("There is no opened sql connection.");

            if (tran == null)
                throw new InvalidOperationException("There is no transaction opened.");

            tran.Rollback();
            tran = null;
        }


        /// <summary>
        /// Close current connection.
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        public void Close()
        {
            if (conn == null || conn.State != ConnectionState.Open)
                return;

            conn.Close();
            conn = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
                if (disposing)
                {
                    if (conn != null)
                        conn.Dispose();
                }
            }
        }
    }
}