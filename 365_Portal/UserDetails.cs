using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal
{
    public class UserDetails
    {
        public static int UserID = 0;

        public static bool LoginUser(UserBO obj)
        {
            bool IsValid = false;
            //string encription = string.Empty;
            //encription = Encription.Encrypt(obj.UserPwd);
            //DataTable dt = new DataTable();
            //SqlParameter[] param = new SqlParameter[2];
            ////param[0] = new SqlParameter("@TYPE", 0);
            //param[0] = new SqlParameter("@UserName", obj.UserName);
            //param[1] = new SqlParameter("@UserPwd", encription);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Parameters.AddRange(param);
            //dt = CDatabase.GetDataTable("pro_login", cmd);
            //if (dt.Rows.Count > 0)
            //{
            //    var passdb = dt.Rows[0]["UserPwd"].ToString();
            //    var dycrpt = Encription.Decrypt(passdb);
            //    if (obj.UserPwd == dycrpt)
            //    {
            //        UserID = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
            //        IsValid = true;
            //    }
            //}
            return IsValid;
        }

        public static UserBO GetUserDetails(int UserId)
        {
            UserBO objUser = new UserBO();
            objUser.UserId = 0;
            objUser.Name = "pramod";
            objUser.UserName = "";
            objUser.UserPwd = "";
            objUser.PwdNo = "";
            objUser.CreatedBy = 0;
            objUser.CreatedOn = "";
            objUser.ModifyBy = 0;
            objUser.ModifyOn = "";
            objUser.DeletedBy = 0;
            objUser.DeletedOn = "";
            objUser.IsDeleted = false;
            objUser.LastName = "Yadav";
            objUser.RoleId = 0;
            objUser.RoleName = "Developer";
            objUser.EmailId = "pramod@gmail.com";
            objUser.MobileNo = "9876543210";

            /*//Commented by pramod on 3 Nov 2018
            UserBO objUser = new UserBO();
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            //param[0] = new SqlParameter("@TYPE", 0);
            param[0] = new SqlParameter("@UserId", UserId);
            //param[1] = new SqlParameter("@UserPwd", obj.UserPwd);
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddRange(param);
            dt = CDatabase.GetDataTable("pro_userInfo", cmd);
            if (dt.Rows.Count > 0)
            {
                //objUser.UserId = 0;
                objUser.UserId = Convert.ToInt32(dt.Rows[0]["USERID"].ToString()); ;
                objUser.Name = dt.Rows[0]["FirstName"].ToString();
                objUser.UserName = "";
                objUser.UserPwd = "";
                objUser.PwdNo = "";
                objUser.CreatedBy = 0;
                objUser.CreatedOn = "";
                objUser.ModifyBy = 0;
                objUser.ModifyOn = "";
                objUser.DeletedBy = 0;
                objUser.DeletedOn = "";
                objUser.IsDeleted = false;
                objUser.LastName = dt.Rows[0]["LastName"].ToString();
                objUser.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"].ToString());
                objUser.RoleName = dt.Rows[0]["RoleName"].ToString();
                objUser.EmailId = dt.Rows[0]["EmailId"].ToString();
                objUser.MobileNo = dt.Rows[0]["MobileNo"].ToString();
            }
            *///end by pramod on 3 Nov 2018
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