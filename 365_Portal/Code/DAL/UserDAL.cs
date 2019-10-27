using _365_Portal.Common;
using _365_Portal.Models;
using _365_Portal.Code.DAL;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static _365_Portal.Models.Login;
using _365_Portal.Code;

namespace _365_Portal.Code.DAL
{
    public class UserDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "UserDAL", methodName);
        }
        public static int UserID = 0;//commit1

        public static LoginResponse LoginUser(LoginRequest objRequest)
        {
            LoginResponse objResponse = null;

            MySqlParameter[] param = new MySqlParameter[3];
            param[0] = new MySqlParameter("p_TYPE", 1);
            param[1] = new MySqlParameter("p_EmailId", objRequest.UserName);
            param[2] = new MySqlParameter("p_UserPwd", objRequest.Password);
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddRange(param);
            DataTable dt = DBConnection.GetDataTable("spLogin", cmd, "");
            
            if (dt.Rows.Count > 0)
            {
                objResponse = new LoginResponse();
                objResponse.ReturnCode = dt.Rows[0]["ReturnCode"].ToString();
                objResponse.ReturnMessage = dt.Rows[0]["ReturnMessage"].ToString();

                if (objResponse.ReturnCode == "1")
                {
                    string HashPassword = Utility.GetHashedPassword(objRequest.Password, dt.Rows[0]["PasswordSalt"].ToString());
                    if (HashPassword == dt.Rows[0]["PasswordHash"].ToString())
                    {                        
                        objResponse.CompID = dt.Rows[0]["CompID"].ToString();
                        objResponse.UserID = dt.Rows[0]["UserID"].ToString();
                        objResponse.RoleID = dt.Rows[0]["RoleID"].ToString();
                        objResponse.Role = dt.Rows[0]["RoleName"].ToString();
                        objResponse.EmailID = dt.Rows[0]["EmailID"].ToString();
                        objResponse.FirstName = dt.Rows[0]["FirstName"].ToString();
                        objResponse.LastName = dt.Rows[0]["LastName"].ToString();
                        objResponse.IsFirstLogin = dt.Rows[0]["IsFirstLogin"].ToString();
                        objResponse.IsFirstPasswordChanged = dt.Rows[0]["IsFirstPasswordChanged"].ToString();
                    }
                    else
                    {
                        objResponse = new LoginResponse();
                        objResponse.ReturnCode = ConstantMessages.Login.InvalidUserCode;
                        objResponse.ReturnMessage = ConstantMessages.Login.InvalidUser;
                    }                 
                }
                else
                {
                    objResponse = new LoginResponse();
                    objResponse.ReturnCode = ConstantMessages.Login.InvalidUserCode;
                    objResponse.ReturnMessage = ConstantMessages.Login.InvalidUser;
                }
            }
            else
            {
                objResponse = new LoginResponse();
                objResponse.ReturnCode = ConstantMessages.WebServiceLog.GenericErrorCode;
                objResponse.ReturnMessage = ConstantMessages.WebServiceLog.GenericErrorMsg;
            }
            objRequest = null;
            return objResponse;
        }
        public static int UserLogout(LoginRequest objRequest)
        {
            string constr = ConfigurationSettings.AppSettings["conString"].ToString();
            int i = 0;
            MySqlConnection conn = new MySqlConnection(constr);
            using (MySqlCommand cmd = new MySqlCommand("proc_WebServiceLog", DBConnection.getConnection()))
            {
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_ControllerName", objRequest.UserName);
                    cmd.Parameters.AddWithValue("p_Ref1", objRequest.Ref1);
                    cmd.Parameters.AddWithValue("p_Ref2", objRequest.Ref2);
                    cmd.Parameters.AddWithValue("p_Ref3", objRequest.Ref3);
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd != null)
                    {
                        if (cmd.Connection.State == ConnectionState.Open ||
                            cmd.Connection.State == ConnectionState.Executing ||
                            cmd.Connection.State == ConnectionState.Fetching)
                            cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }
                }
                return i;
            }
        }

        public static UserBO GetUserDetailsByEmailID(string EmailID, string Ref1)
        {
            UserBO objUser = null;

            MySqlParameter[] param = new MySqlParameter[2];
            param[0] = new MySqlParameter("p_EmailID", EmailID);
            param[1] = new MySqlParameter("p_Ref1", Ref1);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddRange(param);
            DataTable dt = DBConnection.GetDataTable("spGetUserDetailsByEmailID", cmd, "");

            if (dt.Rows.Count > 0)
            {
                objUser = new UserBO();
                objUser.CompId = Convert.ToInt32(dt.Rows[0]["CompID"]);
                objUser.UserID = dt.Rows[0]["UserID"].ToString();
                objUser.RoleID = dt.Rows[0]["RoleID"].ToString();
                objUser.Role = dt.Rows[0]["RoleName"].ToString();                                
                objUser.FirstName = dt.Rows[0]["FirstName"].ToString();
                objUser.LastName = dt.Rows[0]["LastName"].ToString();
                objUser.EmailID = dt.Rows[0]["EmailID"].ToString();
                objUser.MobileNum = dt.Rows[0]["MobileNum"].ToString();
                objUser.Position = dt.Rows[0]["Position"].ToString();
                objUser.ProfilePicFileID = dt.Rows[0]["ProfilePicFileID"].ToString();
                objUser.PasswordHash = dt.Rows[0]["PasswordHash"].ToString(); // newly filed Added by Rana for Change Password Logic
                objUser.PasswordSalt = dt.Rows[0]["PasswordSalt"].ToString();// newly filed Added by Rana for Change Password Logic
                objUser.IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsActive"]);//new field added by for Forgot Password in Field Isdeleted
            }            
            return objUser;
        }
        public static UserBO GetUserDetailsByUserID(string UserId, string Ref1)
        {
            UserBO objUser = null;

            MySqlParameter[] param = new MySqlParameter[2];
            param[0] = new MySqlParameter("p_UserID", UserId);
            param[1] = new MySqlParameter("p_Ref1", Ref1);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddRange(param);
            DataTable dt = DBConnection.GetDataTable("spGetUserDetailsByUserID", cmd, "");

            if (dt.Rows.Count > 0)
            {
                objUser = new UserBO();
                objUser.CompId = Convert.ToInt32(dt.Rows[0]["CompID"]);
                objUser.UserID = dt.Rows[0]["UserID"].ToString();
                objUser.RoleID = dt.Rows[0]["RoleID"].ToString();
                objUser.Role = dt.Rows[0]["RoleName"].ToString();
                objUser.FirstName = dt.Rows[0]["FirstName"].ToString();
                objUser.LastName = dt.Rows[0]["LastName"].ToString();
                objUser.EmailID = dt.Rows[0]["EmailID"].ToString();
                objUser.MobileNum = dt.Rows[0]["MobileNum"].ToString();
                objUser.Position = dt.Rows[0]["Position"].ToString();
                objUser.ProfilePicFileID = dt.Rows[0]["ProfilePicFileID"].ToString();
                objUser.PasswordHash = dt.Rows[0]["PasswordHash"].ToString(); // newly filed Added by Rana for Change Password Logic
                objUser.PasswordSalt = dt.Rows[0]["PasswordSalt"].ToString();// newly filed Added by Rana for Change Password Logic

                if (dt.Rows[0]["EmailNotification"].ToString() == "")
                {
                    objUser.EmailNotification = false;
                }
                else
                {
                    objUser.EmailNotification = Convert.ToBoolean(dt.Rows[0]["EmailNotification"]);
                }
                if (dt.Rows[0]["PushNotification"].ToString() == "")
                {
                    objUser.PushNotification = false;
                }
                else
                {
                    objUser.PushNotification = Convert.ToBoolean(dt.Rows[0]["PushNotification"]);
                }                    
            }
            return objUser;
        }

        public static ResponseBase UpdateUserDetailsByUserID(UserBO _userdetail, string Ref1)
        {
            ResponseBase objResponse = null;

            MySqlParameter[] param = new MySqlParameter[9];
            param[0] = new MySqlParameter("p_UserID", _userdetail.UserID);
            param[1] = new MySqlParameter("p_EmailID", _userdetail.EmailID);
            param[2] = new MySqlParameter("p_FirstName", _userdetail.FirstName);
            param[3] = new MySqlParameter("p_LastName", _userdetail.LastName);
            param[4] = new MySqlParameter("p_Position", _userdetail.Position);
            param[5] = new MySqlParameter("p_EmailNotification", _userdetail.EmailNotification);
            param[6] = new MySqlParameter("p_PushNotification", _userdetail.PushNotification);
            param[7] = new MySqlParameter("p_ProfilePicFileID", _userdetail.ProfilePicFileID);
            param[8] = new MySqlParameter("p_Ref1", Ref1);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddRange(param);
            DataTable dt = DBConnection.GetDataTable("spUpdateUserDetailsByUserID", cmd, "");

            if (dt.Rows.Count > 0)
            {
                objResponse = new ResponseBase();
                objResponse.ReturnCode = dt.Rows[0]["ReturnCode"].ToString();
                objResponse.ReturnMessage = dt.Rows[0]["ReturnMessage"].ToString();
            }
            else
            {
                objResponse = new ResponseBase();
                objResponse.ReturnCode = ConstantMessages.WebServiceLog.GenericErrorCode;
                objResponse.ReturnMessage = ConstantMessages.WebServiceLog.GenericErrorMsg;
            }
            return objResponse;
        }

        public static DataSet CreateUser(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spCreateUser";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_UserID", userdetails.UserID);
                cmd.Parameters.AddWithValue("p_CompID", userdetails.CompId);
                cmd.Parameters.AddWithValue("p_FirstName", userdetails.FirstName);
                cmd.Parameters.AddWithValue("p_LastName", userdetails.LastName);                
                cmd.Parameters.AddWithValue("p_RoleID", userdetails.RoleID);
                cmd.Parameters.AddWithValue("p_EmailID", userdetails.EmailID);
                cmd.Parameters.AddWithValue("p_MobileNum", userdetails.MobileNum);
                cmd.Parameters.AddWithValue("p_Position", userdetails.Position);                
                cmd.Parameters.AddWithValue("p_ProfilePicFileID", userdetails.ProfilePicFileID);
                cmd.Parameters.AddWithValue("p_CreatedBy", userdetails.UserID);
                cmd.Parameters.AddWithValue("p_PasswordSalt", userdetails.PasswordSalt);
                cmd.Parameters.AddWithValue("p_PasswordHash", userdetails.PasswordHash);
                cmd.Parameters.AddWithValue("p_EmailNotification", userdetails.EmailNotification);
                cmd.Parameters.AddWithValue("p_PushNotification", userdetails.PushNotification);

                //cmd.Parameters.AddWithValue("p_GroupId", userdetails.GroupId);
                //cmd.Parameters.AddWithValue("p_ThemeColor", userdetails.ThemeColor);
                //cmd.Parameters.AddWithValue("p_Logo", userdetails.Logo);

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
        public static DataSet DeleteUser(Int32 CompId, string UserId, string UserKey, string CreatedBy)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spDeleteUser";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompId);
                cmd.Parameters.AddWithValue("p_UserId", UserId);
                cmd.Parameters.AddWithValue("p_UserKey", UserKey);
                cmd.Parameters.AddWithValue("p_CreatedBy", CreatedBy);
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
        public static DataSet GetUsers( string UserId, string RoleId, string EmailId,string GroupId,string MobileNo, string Position, string CreatedBy)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetUsers";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("p_UserId", UserId);
                cmd.Parameters.AddWithValue("p_RoleId", RoleId);
                cmd.Parameters.AddWithValue("p_EmailId", EmailId);
                cmd.Parameters.AddWithValue("p_GroupId", GroupId);
                cmd.Parameters.AddWithValue("p_MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("p_Position", Position);
                cmd.Parameters.AddWithValue("p_CreatedBy", CreatedBy);
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
        public static DataSet CreateGroup(Int32 CompId, string GroupName, string Description, string CreatedBy)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spCreateGroup";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompId);
                cmd.Parameters.AddWithValue("p_GroupName", GroupName);
                cmd.Parameters.AddWithValue("p_Description", Description);
                cmd.Parameters.AddWithValue("p_CreatedBy", CreatedBy);
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
        public static DataSet DeleteGroup(Int32 CompId, Int32 GroupId, string CreatedBy)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spDeleteGroup";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompId);
                cmd.Parameters.AddWithValue("p_GroupId", GroupId);                
                cmd.Parameters.AddWithValue("p_CreatedBy", CreatedBy);
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
        public static DataSet SetUserPassword(Int32 CompId, string UserId, string Password, string CreatedBy)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spSetUserPassword";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompId);
                cmd.Parameters.AddWithValue("p_GroupName", UserId);
                cmd.Parameters.AddWithValue("p_Description", Password);
                cmd.Parameters.AddWithValue("p_CreatedBy", CreatedBy);
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

        public static DataSet CreateFile(string FilePath, string FileName,string Ref1)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);
            try
            {
                conn.Open();
                string stm = "spCreateFiles";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_FilePath", FilePath);
                cmd.Parameters.AddWithValue("p_FileName", FileName);
                cmd.Parameters.AddWithValue("p_Ref1", Ref1);               
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

        /*
        public static string GetUserMenu(int UserId)
        {
            ResponseModel res = new ResponseModel();
            string response = string.Empty;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("p_id", UserId);
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddRange(param);
                dt = CDatabase.GetDataTable("pro_UserMenuLogin", cmd);
                List<GetMenuModel> resm = new List<GetMenuModel>();
                if (dt.Rows.Count > 0)
                {
                    DataView view = new DataView(dt);
                    DataTable distinctValues = view.ToTable(true, "MenuId", "Menu");

                    foreach (DataRow dr in distinctValues.Rows)
                    {
                        GetMenuModel Menu = new GetMenuModel();
                        Menu.Menuid = dr["MenuId"].ToString();
                        Menu.MenuName = dr["Menu"].ToString();
                        List<Submenu> Sublist = new List<Submenu>();
                        var teenAgerStudent = from s in dt.AsEnumerable()
                                              where s.Field<int>("MENUID").ToString() == Menu.Menuid
                                              select s;
                        foreach (DataRow dr1 in teenAgerStudent)
                        {
                            Submenu submenu = new Submenu();
                            submenu.SubmenuId = dr1["SubMenuId"].ToString();
                            submenu.Link = dr1["Link"].ToString();
                            submenu.SubmenuName = dr1["SubMenu"].ToString();
                            Sublist.Add(submenu);
                            //resm.Add(Menu);
                        }
                        Menu.Submenu = Sublist;
                        resm.Add(Menu);
                    }


                    res.status = true;
                    res.Msg = "OK";

                }
                response = JsonConvert.SerializeObject(new { res, resm }, Formatting.Indented);
            }
            catch (Exception ex)
            {
                res.status = true;
                res.Msg = ex.Message;
                response = JsonConvert.SerializeObject(res, Formatting.Indented);
            }
            return response;
        }
        */
    }
}