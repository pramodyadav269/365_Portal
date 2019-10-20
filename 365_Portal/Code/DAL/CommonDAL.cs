using MySql.Data.MySqlClient;
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
                cmd.Parameters.AddWithValue("@UserName", u.UserName);
                cmd.Parameters.AddWithValue("@Password", u.UserPwd);
                cmd.Parameters.AddWithValue("@DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("@DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("@IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("@CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", u.CompId);
                cmd.Parameters.AddWithValue("@UserId", u.UserId);
                cmd.Parameters.AddWithValue("@DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("@DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("@IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("@CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", u.CompId);
                cmd.Parameters.AddWithValue("@UserId", u.UserId);
                cmd.Parameters.AddWithValue("@Token", u.Token);
                cmd.Parameters.AddWithValue("@DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("@DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("@IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("@CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", u.CompId);
                cmd.Parameters.AddWithValue("@UserId", u.UserId);
                cmd.Parameters.AddWithValue("@Type", u.ActivityType);
                cmd.Parameters.AddWithValue("@DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("@DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("@IPAddress", u.IP_Addess);
                //cmd.Parameters.AddWithValue("@CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", u.CompId);
                cmd.Parameters.AddWithValue("@UserId", u.UserId);
                cmd.Parameters.AddWithValue("@OldPassword", u.OldPassword);
                cmd.Parameters.AddWithValue("@NewPassword", u.NewPassword);
                cmd.Parameters.AddWithValue("@Token", u.Token);
                cmd.Parameters.AddWithValue("@DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("@DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("@IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("@CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", u.CompId);
                cmd.Parameters.AddWithValue("@UserId", u.UserId);
                cmd.Parameters.AddWithValue("@EmailNotification", u.EmailNotification);
                cmd.Parameters.AddWithValue("@PushNotification", u.PushNotification);
                //cmd.Parameters.AddWithValue("@Token", u.Token);
                cmd.Parameters.AddWithValue("@DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("@DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("@IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("@CreatedBy", u.CreatedBy);
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



        public static DataSet GetNotifications(Int64 CompID, Int64 UserId, string Token)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetNotifications";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompId", CompID);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Token", Token);
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

        public static DataSet GetProfileDetails(Int64 CompID, Int64 UserId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetProfileDetails";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompId", CompID);
                cmd.Parameters.AddWithValue("@UserId", UserId);
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
                cmd.Parameters.AddWithValue("@CompId", u.CompId);
                cmd.Parameters.AddWithValue("@UserId", u.UserId);
                cmd.Parameters.AddWithValue("@FirstName", u.FirstName);
                cmd.Parameters.AddWithValue("@LastName", u.LastName);
                cmd.Parameters.AddWithValue("@ThemeColor", u.ThemeColor);
                cmd.Parameters.AddWithValue("@Logo", u.Logo);
                cmd.Parameters.AddWithValue("@ProfilePic", u.ProfilePic);
                cmd.Parameters.AddWithValue("@EmailId", u.EmailId);
                cmd.Parameters.AddWithValue("@MobileNo", u.MobileNo);
                cmd.Parameters.AddWithValue("@Position", u.Position);
                cmd.Parameters.AddWithValue("@GroupId", u.GroupId);
                cmd.Parameters.AddWithValue("@CreatedBy", u.CreatedBy);
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