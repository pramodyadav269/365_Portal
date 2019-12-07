using _365_Portal.Code;
using _365_Portal.Code.BL;
using _365_Portal.Code.BO;
using _365_Portal.Code.DAL;
using _365_Portal.Models;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using static _365_Portal.Models.Login;

namespace _365_Portal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //HttpContext.Current.Session["UserId"] = null;
                //HttpContext.Current.Session["RoleName"] = null;
                Utility.DestroyAllSession();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (string.IsNullOrEmpty(txtUserEmail.Text.Trim()) || string.IsNullOrEmpty(txtUserPassword.Text.Trim()))
                {
                    //lblError.Text = ConstantMessages.Login.InvalidUser;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Swal.fire({text: '" + ConstantMessages.Login.InvalidUser + "',allowOutsideClick:false})", true);
                    return;
                }
                else
                {
                    LoginRequest objRequest = new LoginRequest();
                    objRequest.UserName = txtUserEmail.Text.Trim();
                    objRequest.Password = txtUserPassword.Text;

                    UserBO objResponse = new UserBO();
                    objResponse = UserDAL.LoginUser(objRequest);

                    if (objResponse.ReturnCode == "1")
                    {
                        //Login Log
                        LoginLogout _loginLogout = new LoginLogout();
                        _loginLogout.UserID = objResponse.UserID;
                        _loginLogout.CompID = objResponse.CompId.ToString();
                        _loginLogout.Type = "login";
                        _loginLogout.IP_Address = Utility.GetClientIPaddress();
                        UserDAL.InsertLoginLogoutHistory(_loginLogout, "");
                        //End Login Log

                        GetAccessToken(txtUserEmail.Text.Trim(), txtUserPassword.Text.Trim());

                        if (HttpContext.Current.Session["access_token"] == null)
                        {
                            //lblError.Text = ConstantMessages.WebServiceLog.GenericErrorMsg;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Swal.fire({text: '" + ConstantMessages.WebServiceLog.GenericErrorMsg + "',allowOutsideClick:false})", true);

                            return;
                        }
                        else
                        {
                            // Call Login Business Layer Function to record message
                            Utility.CreateUserSession(objResponse.UserID, objResponse.Role, objResponse.FirstName, objResponse.LastName, objResponse.CompId);

                            //For ProfilePic,CompanyProfilePic & Theme
                            var UserDetails = UserDAL.GetUserDetailsByUserID(objResponse.UserID, "");
                            if (UserDetails != null && !string.IsNullOrEmpty(UserDetails.ProfilePicFileID))
                            {
                                //HttpContext.Current.Session["ProfilePicFile"] = Utility.GetBase64ImageByFileID(UserDetails.ProfilePicFileID, "~/Files/ProfilePic/");
                                //HttpContext.Current.Session["CompanyProfilePicFile"] = Utility.GetBase64ImageByFileID(UserDetails.CompanyProfilePicFileID, "~/Files/CompLogo/");
                                //HttpContext.Current.Session["ThemeColor"] = UserDetails.ThemeColor;

                                HttpContext.Current.Session["ThemeColor"] = UserDetails.ThemeColor;
                                HttpContext.Current.Session["ThemeColor2"] = UserDetails.ThemeColor2;
                                HttpContext.Current.Session["ThemeColor3"] = UserDetails.ThemeColor3;
                                HttpContext.Current.Session["ThemeColor4"] = UserDetails.ThemeColor4;

                                Utility.CreateProfileAndThemeSession(UserDetails.ProfilePicFileID, UserDetails.CompanyProfilePicFileID, UserDetails.ThemeColor);
                            }
                            //End For ProfilePic,CompanyProfilePic & Theme

                            if (objResponse.IsFirstLogin == "1" || objResponse.IsFirstPasswordNotChanged == "1")
                            {
                                if (objResponse.IsFirstLogin == "1")
                                {
                                    Utility.CreateFirstLoginSession(true);
                                    //HttpContext.Current.Session["IsFirstLogin"] = true;                                    
                                }
                                if (objResponse.IsFirstPasswordNotChanged == "1")
                                {
                                    Utility.CreateFirstPasswordNotChangedSession(true);
                                    //HttpContext.Current.Session["IsFirstPasswordNotChanged"] = true;                                    
                                }
                                if (objResponse.IsFirstLogin == "1")
                                {
                                    Response.Redirect("~/t/Settings.aspx", false);
                                }
                                else if (objResponse.IsFirstPasswordNotChanged == "1")
                                {
                                    Response.Redirect("~/t/ChangePassword.aspx", false);
                                }
                            }
                            else
                            {
                                if (objResponse.Role.ToLower() == "enduser")
                                {
                                    Response.Redirect("~/t/default.aspx", false);
                                }
                                else if (objResponse.Role.ToLower() == "superadmin" || objResponse.Role.ToLower() == "companyadmin" || objResponse.Role.ToLower() == "subadmin")
                                {
                                    Response.Redirect("~/t/dashboard.aspx", false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Call Login Business Layer Function to record message
                        //lblError.Text = objResponse.ReturnMessage;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Swal.fire({text: '" + objResponse.ReturnMessage + "',allowOutsideClick:false})", true);

                    }
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = ConstantMessages.WebServiceLog.GenericErrorMsg;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Swal.fire({text: '" + ConstantMessages.WebServiceLog.GenericErrorMsg + "',allowOutsideClick:false})", true);

            }
        }
    }
}