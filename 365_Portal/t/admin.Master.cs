using _365_Portal.Code;
using _365_Portal.Code.BL;
using _365_Portal.Code.DAL;
using _365_Portal.Common;
using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static _365_Portal.Models.Login;

namespace _365_Portal.Admin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["UserId"] != null && HttpContext.Current.Session["CompId"] != null) 
                {
                    DataSet ds = TrainningBL.GetNotificationsCount(Convert.ToInt32(HttpContext.Current.Session["CompId"]), HttpContext.Current.Session["UserId"].ToString());
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblNotiCount.Text = ds.Tables[0].Rows[0]["NotificationCount"].ToString();
                    }
                    else
                    {
                        lblNotiCount.Text = "";
                    }
                }
            }


            if (HttpContext.Current.Session["UserId"] != null &&
                (
                Convert.ToString(Session["RoleName"]) == "superadmin"
                || Convert.ToString(Session["RoleName"]) == "companyadmin"
                || Convert.ToString(Session["RoleName"]) == "subadmin"
                 || Convert.ToString(Session["RoleName"]) == "enduser"
                ))
            {
                string CurrDirecotry = Server.MapPath("/").ToString();
                string FullPath = CurrDirecotry + System.IO.Path.GetFileName(Request.Url.AbsolutePath);

                if (HttpContext.Current.Session["IsFirstLogin"] != null && Convert.ToBoolean(HttpContext.Current.Session["IsFirstLogin"]) == true
                    && FullPath != CurrDirecotry + "Settings.aspx" && FullPath != CurrDirecotry + "ChangePassword.aspx")
                {
                    Response.Redirect("~/t/Settings.aspx", true);// This is 1st time login..
                }
                else if (HttpContext.Current.Session["IsFirstPasswordNotChanged"] != null && Convert.ToBoolean(HttpContext.Current.Session["IsFirstPasswordNotChanged"]) == true
                    && FullPath != CurrDirecotry + "ChangePassword.aspx" && FullPath != CurrDirecotry + "Settings.aspx")
                {
                    Response.Redirect("~/t/ChangePassword.aspx", true);// This is user has not changed password..
                }
                if (HttpContext.Current.Session["FirstName"] != null && HttpContext.Current.Session["LastName"] != null)
                {
                    lblUserName.Text = HttpContext.Current.Session["FirstName"].ToString() + " " + HttpContext.Current.Session["LastName"].ToString();
                }

                if (HttpContext.Current.Session["ProfilePicFile"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["ProfilePicFile"].ToString()))
                {
                    imgProfilePic.Src = "../Files/ProfilePic/" + HttpContext.Current.Session["ProfilePicFile"].ToString();
                }
                if (HttpContext.Current.Session["CompanyProfilePicFile"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["CompanyProfilePicFile"].ToString()))
                {
                    imgCompanyLogo.Src = "../Files/CompLogo/" + HttpContext.Current.Session["CompanyProfilePicFile"].ToString();
                }
                var fName = "Me";
                if (Session["FirstName"] != null)
                    fName = Convert.ToString(Session["FirstName"]);

                //aMe_Menu.InnerHtml = "<i class='fas fa-user'></i><span class='tooltiptext'>" + fName + "</span><span>" + fName + "</span>";

                //sideNav.Style.Add("background-color", "blue");

                if (HttpContext.Current.Session["RoleName"] != null)
                {
                    if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.superadmin)
                    {
                        dvDashboard.Visible = true;
                        dvUserDashboard.Visible = false;
                        dvGroups.Visible = true;
                        dvUsers.Visible = true;
                        dvTopics.Visible = true;
                        dvAssignTopics.Visible = true;

                        dvMenu_Directory.Visible = true;
                        dvSubMenu_Organizations.Visible = true;
                        dvMenu_Integrations.Visible = true;
                        dvSubMenu_ContentSettings.Visible = true;
                        dvSubMenu_Roles.Visible = true;
                        dvSubMenu_Customize.Visible = true;
                    }
                    else if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.companyadmin)
                    {
                        dvDashboard.Visible = true;
                        dvUserDashboard.Visible = false;
                        dvGroups.Visible = true;
                        dvUsers.Visible = true;
                        dvTopics.Visible = true;
                        dvAssignTopics.Visible = true;

                        dvSubMenu_AssignTopics.Visible = true;

                        dvMenu_Directory.Visible = true;
                        dvMenu_Integrations.Visible = true;
                        dvSubMenu_ContentSettings.Visible = true;
                        dvMenu_Account.Visible = true;
                        dvSubMenu_Customize.Visible = true;
                        dvSubMenu_AssignTopics.Visible = true;
                    }
                    else if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.subadmin)
                    {
                        dvDashboard.Visible = true;
                        dvUserDashboard.Visible = false;
                        dvGroups.Visible = false;
                        dvUsers.Visible = false;
                        dvTopics.Visible = true;
                        dvAssignTopics.Visible = false;
                        dvSubMenu_ContentSettings.Visible = true;
                        dvSubMenu_Customize.Visible = true;
                    }
                    else if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.enduser)
                    {
                        dvDashboard.Visible = false;
                        dvUserDashboard.Visible = true;
                        dvGroups.Visible = false;
                        dvUsers.Visible = false;
                        dvTopics.Visible = false;
                        dvAssignTopics.Visible = false;
                    }
                }

                // Change Theme Colors & Fonts..
                var theme1 = Convert.ToString(HttpContext.Current.Session["ThemeColor"]);
                var theme2 = Convert.ToString(HttpContext.Current.Session["ThemeColor2"]);
                var theme3 = Convert.ToString(HttpContext.Current.Session["ThemeColor3"]);
                var theme4 = Convert.ToString(HttpContext.Current.Session["ThemeColor4"]);
                if (!string.IsNullOrEmpty(theme1))
                {
                    //dvBody.Style.Add("font-family", "");
                }
                if (!string.IsNullOrEmpty(theme2))
                {
                    //dvBody.Style.Add("font-family", "");
                }
                if (!string.IsNullOrEmpty(theme3))
                {
                    // dvBody.Style.Add("font-family", "");
                }
                if (!string.IsNullOrEmpty(theme4))
                {
                    dvBody.Style.Add("font-family", theme4);
                }
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            WebServiceLog objServiceLog = new WebServiceLog();
            LoginRequest objRequest = new LoginRequest();
            ResponseBase objResponse = null;
            objServiceLog.RequestTime = DateTime.Now;
            objServiceLog.ControllerName = this.GetType().Name;
            objServiceLog.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                objResponse = new ResponseBase();
                if (HttpContext.Current.Session["UserId"] != null && HttpContext.Current.Session["CompId"] != null)
                {
                    objRequest.UserID = Convert.ToString(HttpContext.Current.Session["UserId"]);

                    DataSet ds = UserDAL.UserLogout(Convert.ToInt32(HttpContext.Current.Session["CompId"]), objRequest.UserID, Utility.GetClientIPaddress());
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ReturnCode"].ToString() == "1")
                    {
                        objResponse.ReturnCode = "1";
                        objResponse.ReturnMessage = "User logout succesfully.";
                    }
                    else
                    {
                        objResponse.ReturnCode = "2";
                        objResponse.ReturnMessage = "Unable to logout.";
                    }
                    objServiceLog.RequestString = JSONHelper.ConvertJsonToString(objRequest);
                    objServiceLog.ResponseString = JSONHelper.ConvertJsonToString(objResponse);
                    objServiceLog.RequestType = ConstantMessages.WebServiceLog.Success;

                    Utility.DestroyAllSession();
                    Response.Redirect("~/login.aspx", false);
                }
                else
                {
                    objServiceLog.RequestString = JSONHelper.ConvertJsonToString(objRequest);
                    objServiceLog.ResponseString = JSONHelper.ConvertJsonToString(objResponse);
                    objServiceLog.RequestType = ConstantMessages.WebServiceLog.Success;
                }
            }
            catch (Exception ex)
            {
                objServiceLog.ResponseString = "Exception " + ex.Message + " | " + ex.StackTrace;
                objServiceLog.RequestType = ConstantMessages.WebServiceLog.Exception;
            }
            finally
            {
                objServiceLog.ResponseTime = DateTime.Now;
                InsertRequestLog.SaveWebServiceLog(objServiceLog);
            }
        }
    }
}