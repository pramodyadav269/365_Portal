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
            param[1] = new MySqlParameter("p_EmailId", objRequest.EmailId);
            param[2] = new MySqlParameter("p_UserPwd", objRequest.UserPwd);
            
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
                    string HashPassword = Utility.GetHashedPassword(objRequest.UserPwd, dt.Rows[0]["PasswordSalt"].ToString());
                    if (HashPassword == dt.Rows[0]["PasswordHash"].ToString())
                    {                        
                        objResponse.CompID = dt.Rows[0]["CompID"].ToString();
                        objResponse.UserID = dt.Rows[0]["UserID"].ToString();
                        objResponse.RoleID = dt.Rows[0]["RoleID"].ToString();
                        objResponse.Role = dt.Rows[0]["RoleName"].ToString();
                        objResponse.EmailID = dt.Rows[0]["EmailID"].ToString();
                        objResponse.IsFirstLogin = dt.Rows[0]["IsFirstLogin"].ToString();
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
                    cmd.Parameters.AddWithValue("p_ControllerName", objRequest.EmailId);
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

        public static UserBO GetUserDetails(string UserId, string Ref1)
        {
            UserBO objUser = new UserBO();
            objUser.UserID = "0";
            objUser.RoleID = "4";
            objUser.Role = "EndUser";
            objUser.IsFirstLogin = false;
            objUser.ProfilePicFileID = "";
            objUser.FirstName = "Pramod";
            objUser.LastName = "Yadav";
            objUser.EmailID = "PramodYadav269@gmail.com";
            objUser.MobileNum = "9930315629";
            objUser.Position = "EndUser";
            objUser.PasswordHash = "";
            objUser.PasswordSalt = "2019-10-12";

            /*
            UserBO objUser = null;

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("TYPE", 1);
            param[1] = new SqlParameter("UserId", UserId);
            param[2] = new SqlParameter("Ref1", Ref1);

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddRange(param);
            DataTable dt = DBConnection.GetDataTable("proc_GetUserDetails", cmd,"");
            
            if (dt.Rows.Count > 0)
            {
                objUser = new UserBO();
                objUser.Id = Convert.ToInt64(dt.Rows[0]["ID"].ToString());
                objUser.UserId = Convert.ToInt64(dt.Rows[0]["USERID"].ToString());
                objUser.ProfilePicPath = dt.Rows[0]["ProfilePicPath"].ToString();
                objUser.FirstName = dt.Rows[0]["FirstName"].ToString();
                objUser.LastName = dt.Rows[0]["LastName"].ToString();
                objUser.EmailId = dt.Rows[0]["EmailId"].ToString();
                objUser.UserPwd = dt.Rows[0]["UserPwd"].ToString();                
                objUser.MobileNo = dt.Rows[0]["MobileNo"].ToString();
                objUser.Position = dt.Rows[0]["Position"].ToString();
                objUser.CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                objUser.CreatedOn = dt.Rows[0]["CreatedOn"].ToString();
                objUser.ModifiedBy = Convert.ToInt64(dt.Rows[0]["ModifiedBy"].ToString());
                objUser.ModifiedOn = dt.Rows[0]["ModifiedOn"].ToString();
                if (dt.Rows[0]["IsDeleted"].ToString() == "1")
                {
                    objUser.IsDeleted = true;
                }
                else
                {
                    objUser.IsDeleted = false;
                }
                objUser.DeletedBy = Convert.ToInt64(dt.Rows[0]["DeletedBy"].ToString());
                objUser.DeletedOn = dt.Rows[0]["DeletedOn"].ToString();
            }                       
            */
            return objUser;
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