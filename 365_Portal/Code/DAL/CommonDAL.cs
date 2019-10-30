using MySql.Data.MySqlClient;
using _365_Portal.Models;
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
                cmd.Parameters.AddWithValue("p_EmailID", u.EmailID);
                //cmd.Parameters.AddWithValue("p_ErrorCode", u.ErrorCode);
                cmd.Parameters.AddWithValue("p_ErrorMessage", u.PasswordSalt);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Address);
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
                cmd.Parameters.AddWithValue("p_UserID", u.UserID);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Address);
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
                cmd.Parameters.AddWithValue("p_UserID", u.UserID);
                cmd.Parameters.AddWithValue("p_Token", u.Token);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Address);
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
                cmd.Parameters.AddWithValue("p_UserID", u.UserID);
                cmd.Parameters.AddWithValue("p_Type", u.ActivityType);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Address);
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
                cmd.Parameters.AddWithValue("p_CompID", u.CompId);
                cmd.Parameters.AddWithValue("p_UserID", u.UserID);
                cmd.Parameters.AddWithValue("p_PasswordHash", u.NewPassword);
                cmd.Parameters.AddWithValue("p_PasswordSalt", u.PasswordSalt);
                cmd.Parameters.AddWithValue("p_Token", string.Empty);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Address);
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
                cmd.Parameters.AddWithValue("p_UserID", u.UserID);
                cmd.Parameters.AddWithValue("p_EmailNotification", u.EmailNotification);
                cmd.Parameters.AddWithValue("p_PushNotification", u.PushNotification);
                //cmd.Parameters.AddWithValue("p_Token", u.Token);
                cmd.Parameters.AddWithValue("p_DeviceType", u.DeviceType);
                cmd.Parameters.AddWithValue("p_DeviceDetails", u.DeviceDetails);
                cmd.Parameters.AddWithValue("p_IPAddress", u.IP_Address);
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



        public static DataSet GetNotifications(int CompID, int UserID, string Token)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetNotifications";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", CompID);
                cmd.Parameters.AddWithValue("p_UserID", UserID);
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

        public static DataSet GetProfileDetails(int CompID, int UserID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetProfileDetails";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", CompID);
                cmd.Parameters.AddWithValue("p_UserID", UserID);
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
                cmd.Parameters.AddWithValue("p_UserId", u.UserID);
                cmd.Parameters.AddWithValue("p_FirstName", u.FirstName);
                cmd.Parameters.AddWithValue("p_LastName", u.LastName);
                cmd.Parameters.AddWithValue("p_ThemeColor", u.ThemeColor);
                cmd.Parameters.AddWithValue("p_Logo", u.Logo);
                cmd.Parameters.AddWithValue("p_ProfilePic", u.ProfilePicFileID);
                cmd.Parameters.AddWithValue("p_EmailId", u.EmailID);
                cmd.Parameters.AddWithValue("p_MobileNo", u.MobileNum);
                cmd.Parameters.AddWithValue("p_Position", u.Position);
                cmd.Parameters.AddWithValue("p_GroupId", u.GroupId);
                cmd.Parameters.AddWithValue("p_CreatedBy", u.UserID);
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

        public static DataSet GetLoginDetails(string UserName)//This will be call while creating Token
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


        public static DataSet UserResetPassword(int CompId, string UserId, string MobileNum, string EmailId, string Type, string DeviceDetails, string DeviceType, string IpAddress, int OTP, string token_url)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spUserResetPassword";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompId);
                cmd.Parameters.AddWithValue("p_UserId", UserId);
                cmd.Parameters.AddWithValue("p_MobileNum", MobileNum);
                cmd.Parameters.AddWithValue("p_EmailId", EmailId);
                cmd.Parameters.AddWithValue("p_Type", Type);
                cmd.Parameters.AddWithValue("p_DeviceDetails", DeviceDetails);
                cmd.Parameters.AddWithValue("p_DeviceType", DeviceType);
                cmd.Parameters.AddWithValue("p_IpAddress", IpAddress);
                if (Type == ConstantMessages.ForgotPassowrd.Type_0)
                {
                    cmd.Parameters.AddWithValue("p_Token", OTP);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_Token", token_url);
                }


                DataTable dt = ds.Tables.Add("Data");
                dt.Clear();
                dt.Columns.Add("ReturnCode");
                dt.Columns.Add("ReturnMessage");
                DataRow _row = dt.NewRow();
                _row["ReturnCode"] = "1";
                _row["ReturnMessage"] = "Success";
                dt.Rows.Add(_row);

                //da.Fill(_ds, "Data");
                //MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //
                // DataTable dt = ds.Tables["Data"];
                /*
                if (dt.Rows[0]["ReturnCode"].ToString() == "1")
                {
                    if (Type == ConstantMessages.ForgotPassowrd.Type_1)
                    {
                        // Send OTP on email-id.
                        //SendEmail(FROM_EMAIL, emailId, GetMobileOTPVerificationMail(userName, OTP.ToString()), GetMobileOTPVerificationMailSubject());
                    }
                    else if (Type == ConstantMessages.ForgotPassowrd.Type_0)
                    {
                        // Send OTP on mobile phone..x`
                        //string template = WebConfigurationManager.AppSettings["SMS_OTP_Template"].Replace("~OTP~", OTP.ToString());
                        //SendSMS(new long[] { mobileNum }, template);
                    }
                }
                */
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