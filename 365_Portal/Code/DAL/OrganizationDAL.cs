using _365_Portal.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.DAL
{
    public class OrganizationDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "OrganizationDAL", methodName);
        }

        public static DataSet GetAdminUsers(UserBO objUsers)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetUsers";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_UserID", objUsers.UserID);
                cmd.Parameters.AddWithValue("p_CompID", objUsers.CompId);
                cmd.Parameters.AddWithValue("p_Role", objUsers.Role);
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

        public static DataSet CreateUpdateAdminUser(UserBO objUsers, int Action, int ChildUserID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spCreateAdminUser";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", Action);                
                cmd.Parameters.AddWithValue("p_UserKey", objUsers.UserKey);
                cmd.Parameters.AddWithValue("p_CompName", objUsers.CompName);
                cmd.Parameters.AddWithValue("p_ChildUserID", ChildUserID);
                cmd.Parameters.AddWithValue("p_RoleID", objUsers.RoleID);
                cmd.Parameters.AddWithValue("p_FirstName", objUsers.FirstName);
                cmd.Parameters.AddWithValue("p_LastName", objUsers.LastName);
                cmd.Parameters.AddWithValue("p_EmailID", objUsers.EmailID);
                cmd.Parameters.AddWithValue("p_IsChangingPassword", objUsers.IsChangingPassword);
                cmd.Parameters.AddWithValue("p_PasswordHash", objUsers.PasswordHash);
                cmd.Parameters.AddWithValue("p_PasswordSalt", objUsers.PasswordSalt);
                cmd.Parameters.AddWithValue("p_MobileNum", objUsers.MobileNum);
                cmd.Parameters.AddWithValue("p_Position", objUsers.Position);

                cmd.Parameters.AddWithValue("p_ProFilePicFileID", objUsers.ProfilePicFileID);
                cmd.Parameters.AddWithValue("p_EmailNotification", objUsers.EmailNotification);
                cmd.Parameters.AddWithValue("p_PushNotification", objUsers.PushNotification);

                cmd.Parameters.AddWithValue("p_GroupId", objUsers.GroupId);
                cmd.Parameters.AddWithValue("p_CreatedBy", objUsers.UserID);

                cmd.Parameters.AddWithValue("p_CompanyProfilePicFileID", objUsers.CompanyProfilePicFileID);
                cmd.Parameters.AddWithValue("p_CompanyThemeColor", objUsers.ThemeColor);
                cmd.Parameters.AddWithValue("p_CompanyThemeColor2", objUsers.ThemeColor2);
                cmd.Parameters.AddWithValue("p_CompanyThemeColor3", objUsers.ThemeColor3);
                cmd.Parameters.AddWithValue("p_CustomFont", objUsers.ThemeColor4);
                cmd.Parameters.AddWithValue("p_BusinessName", objUsers.CompName);
                cmd.Parameters.AddWithValue("p_NoOfEmployees", objUsers.NoOfEmployees);
                cmd.Parameters.AddWithValue("p_Country", objUsers.Country);                  

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

        public static DataSet GetCountry(int UserID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetCountry";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public static DataSet GetRole(string Role,string RoleID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetRole";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Role", Role);
                cmd.Parameters.AddWithValue("p_RoleID", RoleID);
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

        public static DataSet GetAdminUserDetails(UserBO objUsers, int ChildUserID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetUserDetailsForParent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", objUsers.CompId);
                cmd.Parameters.AddWithValue("p_UserID", objUsers.UserID);
                cmd.Parameters.AddWithValue("p_Role", objUsers.Role);
                cmd.Parameters.AddWithValue("p_ChildUserID", ChildUserID);

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