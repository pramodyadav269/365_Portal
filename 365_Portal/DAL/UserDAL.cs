using _365_Portal.Common;
using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static _365_Portal.Models.Login;

namespace _365_Portal.DAL
{
    public class UserDAL
    {
        public static int UserID = 0;//commit

        public static LoginResponse LoginUser(LoginRequest objRequest)
        {
            LoginResponse objResponse = null;

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TYPE", 1);
            param[1] = new SqlParameter("@EmailId", objRequest.EmailId);
            param[2] = new SqlParameter("@UserPwd", objRequest.UserPwd);

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddRange(param);
            DataTable dt = DBConnection.GetDataTable("proc_LoginUser", cmd, "");

            if (dt.Rows.Count > 0)
            {
                objResponse = new LoginResponse();
                objResponse.ReturnCode = dt.Rows[0]["ReturnCode"].ToString();
                objResponse.ReturnMessage = dt.Rows[0]["ReturnMessage"].ToString();
                objResponse.ProfilePicPath = dt.Rows[0]["ProfilePicPath"].ToString();
                objResponse.FirstName = dt.Rows[0]["FirstName"].ToString();
                objResponse.LastName = dt.Rows[0]["LastName"].ToString();
                objResponse.EmailId = dt.Rows[0]["EmailId"].ToString();
                objResponse.MobileNo = dt.Rows[0]["MobileNo"].ToString();
                objResponse.Position = dt.Rows[0]["Position"].ToString();
            }
            else
            {
                objResponse = new LoginResponse();
                objResponse.ReturnCode = ConstantMessages.Login.InvalidUserCode;
                objResponse.ReturnMessage = ConstantMessages.Login.InvalidUser;
            }
            objRequest = null;
            return objResponse;
        }

        public static int UserLogout(LoginRequest objRequest)
        {
            string constr = ConfigurationSettings.AppSettings["conString"].ToString();
            int i = 0;
            SqlConnection conn = new SqlConnection(constr);
            using (SqlCommand cmd = new SqlCommand("proc_WebServiceLog", DBConnection.getConnection()))
            {
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ControllerName", objRequest.EmailId);                    
                    cmd.Parameters.AddWithValue("@Ref1", objRequest.Ref1);
                    cmd.Parameters.AddWithValue("@Ref2", objRequest.Ref2);
                    cmd.Parameters.AddWithValue("@Ref3", objRequest.Ref3);                    
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

        public static UserBO GetUserDetails(long UserId, string Ref1)
        {
            UserBO objUser = new UserBO();
            objUser.Id = 0;
            objUser.UserId = 0;
            objUser.ProfilePicPath = "";
            objUser.FirstName = "Pramod";
            objUser.LastName = "Yadav";
            objUser.EmailId = "PramodYadav269@gmail.com";
            objUser.UserPwd = "123456";
            objUser.MobileNo = "9930315629";
            objUser.Position = "EndUser";
            objUser.CreatedBy = 0;
            objUser.CreatedOn = "2019-10-12";
            objUser.ModifiedBy = 0;
            objUser.ModifiedOn = "2019-10-12";
            objUser.IsDeleted = false;
            objUser.DeletedBy = 0;
            objUser.DeletedOn = "2019-10-12";

            /*
            UserBO objUser = null;

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TYPE", 1);
            param[1] = new SqlParameter("@UserId", UserId);
            param[2] = new SqlParameter("@Ref1", Ref1);

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
                param[0] = new SqlParameter("@id", UserId);
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