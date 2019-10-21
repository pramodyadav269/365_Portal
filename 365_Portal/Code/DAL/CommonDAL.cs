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
                cmd.Parameters.AddWithValue("_UserName", u.UserName);
                cmd.Parameters.AddWithValue("_ErrorCode", u.ErrorCode);
                cmd.Parameters.AddWithValue("_ErrorMessage", u.PasswordSalt);
                cmd.Parameters.AddWithValue("_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("_CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", u.CompId);
                cmd.Parameters.AddWithValue("_UserId", u.UserId);
                cmd.Parameters.AddWithValue("_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("_CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", u.CompId);
                cmd.Parameters.AddWithValue("_UserId", u.UserId);
                cmd.Parameters.AddWithValue("_Token", u.Token);
                cmd.Parameters.AddWithValue("_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("_CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", u.CompId);
                cmd.Parameters.AddWithValue("_UserId", u.UserId);
                cmd.Parameters.AddWithValue("_Type", u.ActivityType);
                cmd.Parameters.AddWithValue("_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("_IPAddress", u.IP_Addess);
                //cmd.Parameters.AddWithValue("_CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", u.CompId);
                cmd.Parameters.AddWithValue("_UserId", u.UserId);
                //cmd.Parameters.AddWithValue("_OldPassword", u.OldPassword);
                //cmd.Parameters.AddWithValue("_NewPassword", u.NewPassword);
                cmd.Parameters.AddWithValue("_NewPassword", u.NewPassword);
                cmd.Parameters.AddWithValue("_PasswordSalt", u.PasswordSalt);
                cmd.Parameters.AddWithValue("_Token", u.Token);
                cmd.Parameters.AddWithValue("_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("_CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", u.CompId);
                cmd.Parameters.AddWithValue("_UserId", u.UserId);
                cmd.Parameters.AddWithValue("_EmailNotification", u.EmailNotification);
                cmd.Parameters.AddWithValue("_PushNotification", u.PushNotification);
                //cmd.Parameters.AddWithValue("_Token", u.Token);
                cmd.Parameters.AddWithValue("_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("_IPAddress", u.IP_Addess);
                cmd.Parameters.AddWithValue("_CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", CompID);
                cmd.Parameters.AddWithValue("_UserId", UserId);
                cmd.Parameters.AddWithValue("_Token", Token);
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
                cmd.Parameters.AddWithValue("_CompId", CompID);
                cmd.Parameters.AddWithValue("_UserId", UserId);
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
                cmd.Parameters.AddWithValue("_CompId", u.CompId);
                cmd.Parameters.AddWithValue("_UserId", u.UserId);
                cmd.Parameters.AddWithValue("_FirstName", u.FirstName);
                cmd.Parameters.AddWithValue("_LastName", u.LastName);
                cmd.Parameters.AddWithValue("_ThemeColor", u.ThemeColor);
                cmd.Parameters.AddWithValue("_Logo", u.Logo);
                cmd.Parameters.AddWithValue("_ProfilePic", u.ProfilePic);
                cmd.Parameters.AddWithValue("_EmailId", u.EmailId);
                cmd.Parameters.AddWithValue("_MobileNo", u.MobileNo);
                cmd.Parameters.AddWithValue("_Position", u.Position);
                cmd.Parameters.AddWithValue("_GroupId", u.GroupId);
                cmd.Parameters.AddWithValue("_CreatedBy", u.CreatedBy);
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
                cmd.Parameters.AddWithValue("_UserName", UserName);
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