﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace _365_Portal.Code.DAL
{
    public class CommonDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "CommonDAL", methodName);
        }

        public static DataSet Login(UserBO u)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spLogin";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_UserName", u.UserName);
                cmd.Parameters.AddWithValue("p_ErrorCode", u.ErrorCode);
                cmd.Parameters.AddWithValue("p_ErrorMessage", u.PasswordSalt);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("p_CreatedBy", u.CreatedBy);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet Logout(UserBO u)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spLogout";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", u.CompId);
                cmd.Parameters.AddWithValue("p_UserId", u.UserId);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("p_CreatedBy", u.CreatedBy);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet ResetPassword(UserBO u)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spResetPassword";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", u.CompId);
                cmd.Parameters.AddWithValue("p_UserId", u.UserId);
                cmd.Parameters.AddWithValue("p_Token", u.Token);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("p_CreatedBy", u.CreatedBy);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public static DataSet UpdateActivity(UserBO u)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spUpdateActivity";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", u.CompId);
                cmd.Parameters.AddWithValue("p_UserId", u.UserId);
                cmd.Parameters.AddWithValue("p_Type", u.ActivityType);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Addess);
                //cmd.Parameters.AddWithValue("p_CreatedBy", u.CreatedBy);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet ChangePassword(UserBO u)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spChangePassword";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", u.CompId);
                cmd.Parameters.AddWithValue("p_UserId", u.UserId);
                //cmd.Parameters.AddWithValue("p_OldPassword", u.OldPassword);
                //cmd.Parameters.AddWithValue("p_NewPassword", u.NewPassword);
                cmd.Parameters.AddWithValue("p_NewPassword", u.NewPassword);
                cmd.Parameters.AddWithValue("p_PasswordSalt", u.PasswordSalt);
                cmd.Parameters.AddWithValue("p_Token", u.Token);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("p_CreatedBy", u.CreatedBy);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public static DataSet UpdateSettings(UserBO u)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spUpdateSettings";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", u.CompId);
                cmd.Parameters.AddWithValue("p_UserId", u.UserId);
                cmd.Parameters.AddWithValue("p_EmailNotification", u.EmailNotification);
                cmd.Parameters.AddWithValue("p_PushNotification", u.PushNotification);
                //cmd.Parameters.AddWithValue("p_Token", u.Token);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("p_CreatedBy", u.CreatedBy);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }



        public static DataSet GetNotifications(Int32 CompID, Int32 UserId, string Token)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetNotifications";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompID);
                cmd.Parameters.AddWithValue("p_UserId", UserId);
                cmd.Parameters.AddWithValue("p_Token", Token);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetProfileDetails(Int32 CompID, Int32 UserId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetProfileDetails";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompID);
                cmd.Parameters.AddWithValue("p_UserId", UserId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet SetProfileDetails(UserBO u)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spSetProfileDetails";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", u.CompId);
                cmd.Parameters.AddWithValue("p_UserId", u.UserId);
                cmd.Parameters.AddWithValue("p_FirstName", u.FirstName);
                cmd.Parameters.AddWithValue("p_LastName", u.LastName);
                cmd.Parameters.AddWithValue("p_ThemeColor", u.ThemeColor);
                cmd.Parameters.AddWithValue("p_Logo", u.Logo);
                cmd.Parameters.AddWithValue("p_ProfilePic", u.ProfilePic);
                cmd.Parameters.AddWithValue("p_EmailId", u.EmailId);
                cmd.Parameters.AddWithValue("p_MobileNo", u.MobileNo);
                cmd.Parameters.AddWithValue("p_Position", u.Position);
                cmd.Parameters.AddWithValue("p_GroupId", u.GroupId);
                cmd.Parameters.AddWithValue("p_CreatedBy", u.CreatedBy);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetLoginDetails(string UserName)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "GetLoginDetails";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_UserName", UserName);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }

            return ds;
        }
    }
}