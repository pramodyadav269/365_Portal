using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _365_Portal.Common
{
    public class DBConnection
    {
        public static SqlConnection getConnection(string ConnStr)
        {
            SqlConnection conn;
            try
            {
                string constr = ConfigurationSettings.AppSettings["conString"].ToString();
                conn = new SqlConnection(constr);
            }
            catch
            {
                throw new Exception("SQL Connection String is invalid.");
            }
            return conn;
        }
        public static SqlConnection getConnection()
        {
            SqlConnection conn;
            try
            {
                string constr = ConfigurationSettings.AppSettings["conString"].ToString();
                conn = new SqlConnection(constr);
            }
            catch
            {
                throw new Exception("SQL Connection String is invalid.");
            }
            return conn;
        }

        public static DataSet GetDataSet(string procedureName, SqlCommand command, string Ref1)
        {
            SqlConnection connection = getConnection();
            DataSet ds = null;
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                command.CommandText = procedureName;
                command.Connection = connection;
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = command;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
                command.Connection.Close();
                command.Connection.Dispose();
                command.Dispose();
            }
            return ds;
        }

        public static DataSet GetDataSet(string sqlQuery, string Ref1)
        {
            SqlConnection connection = getConnection();
            connection.Open();
            DataSet ds = null;
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da = new SqlDataAdapter(sqlQuery, connection);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return ds;
        }

        public static DataTable GetDataTable(string procedureName, SqlCommand command,string Ref1)
        {
            SqlConnection connection = getConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = null;
            try
            {
                command.CommandTimeout = 1000;
                command.CommandText = procedureName;
                command.Connection = connection;
                connection.Open();
                //Mark As Stored Procedure
                command.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = command;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
                command.Dispose();
                if (connection.State == ConnectionState.Open) connection.Close();
            }            
            return dt;
        }

        public static DataTable GetDataTable(string sSQl,string Ref1)
        {
            SqlConnection connection = getConnection();
            DataTable dt;
            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(sSQl, connection);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
                connection.Dispose();
            }
            return dt;
        }

        //it gets count return by a stored procedure
        public static string InsertRecordWithIdentity(string procedureName, SqlCommand command,string Ref1)
        {
            SqlConnection connection = getConnection();
            try
            {
                command.CommandText = procedureName;
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                return Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
                command.Dispose();
                connection.Dispose();
            }
        }

        //it gets count return by a stored procedure
        public static string ExecuteScalarString(string sql,string Ref1)
        {
            SqlConnection connection = getConnection();
            SqlCommand command = new SqlCommand();
            string retValue = "";
            try
            {
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Connection = connection;
                connection.Open();
                retValue = Convert.ToString(command.ExecuteScalar());
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
                command.Dispose();
                connection.Dispose();
            }
            return retValue;
        }

    }
}