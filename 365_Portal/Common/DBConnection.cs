﻿using _365_Portal.Code;
using MySql.Data.MySqlClient;
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
        public static MySqlConnection getConnection(string ConnStr)
        {
            MySqlConnection conn;
            try
            {
                string constr = ConnectionManager.connectionString;
                conn = new MySqlConnection(constr);
            }
            catch
            {
                throw new Exception("SQL Connection String is invalid.");
            }
            return conn;
        }
        public static MySqlConnection getConnection()
        {
            MySqlConnection conn;
            try
            {
                string constr = ConnectionManager.connectionString;
                conn = new MySqlConnection(constr);
            }
            catch
            {
                throw new Exception("SQL Connection String is invalid.");
            }
            return conn;
        }

        public static DataSet GetDataSet(string procedureName, MySqlCommand command, string Ref1)
        {
            MySqlConnection connection = getConnection();
            DataSet ds = null;
            MySqlDataAdapter da = new MySqlDataAdapter();
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
            MySqlConnection connection = getConnection();
            connection.Open();
            DataSet ds = null;
            MySqlDataAdapter da = new MySqlDataAdapter();
            try
            {
                da = new MySqlDataAdapter(sqlQuery, connection);
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

        public static DataTable GetDataTable(string procedureName, MySqlCommand command,string Ref1)
        {
            MySqlConnection connection = getConnection();
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
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
            MySqlConnection connection = getConnection();
            DataTable dt;
            try
            {
                connection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(sSQl, connection);
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
        public static string InsertRecordWithIdentity(string procedureName, MySqlCommand command,string Ref1)
        {
            MySqlConnection connection = getConnection();
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
            MySqlConnection connection = getConnection();
            MySqlCommand command = new MySqlCommand();
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