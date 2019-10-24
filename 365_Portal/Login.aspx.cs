using _365_Portal.Code;
using _365_Portal.Code.BL;
using _365_Portal.Code.DAL;
using _365_Portal.Models;
using System;
using System.Configuration;
using System.Web;
using static _365_Portal.Models.Login;

namespace _365_Portal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {               
                HttpContext.Current.Session["UserId"] = null;
                HttpContext.Current.Session["RoleName"] = null;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";

                if (string.IsNullOrEmpty(txtUserEmail.Text.Trim()) || string.IsNullOrEmpty(txtUserPassword.Text.Trim()))
                {
                    lblError.Text = ConstantMessages.Login.InvalidUser;
                    return;
                }
                else
                {
                    LoginRequest objRequest = new LoginRequest();
                    objRequest.EmailId = txtUserEmail.Text.Trim();
                    objRequest.UserPwd = txtUserPassword.Text;

                    LoginResponse objResponse = new LoginResponse();
                    objResponse = UserDAL.LoginUser(objRequest);

                    if (objResponse.ReturnCode == "1")
                    {
                        GetAccessToken(txtUserEmail.Text.Trim(), txtUserPassword.Text.Trim());

                        if (HttpContext.Current.Session["access_token"] == null)
                        {
                            lblError.Text = ConstantMessages.WebServiceLog.GenericErrorMsg;
                            return;
                        }
                        else
                        {
                            // Call Login Business Layer Function to record message
                            HttpContext.Current.Session["UserId"] = objResponse.UserID;
                            HttpContext.Current.Session["RoleName"] = objResponse.Role;

                            if (objResponse.IsFirstLogin == "1")
                            {
                                HttpContext.Current.Session["IsFirstTimeLogin"] = true;
                                Response.Redirect("ChangePassword.aspx",false);
                            }
                            else
                            {
                                if (objResponse.Role.ToLower() == "enduser")
                                {
                                    Response.Redirect("~/Topics.aspx",false);
                                }
                                else if (objResponse.Role.ToLower() == "superadmin" || objResponse.Role.ToLower() == "companyadmin" || objResponse.Role.ToLower() == "subadmin")
                                {
                                    Response.Redirect("~/admin/dashboard.aspx",false);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Call Login Business Layer Function to record message
                        lblError.Text = objResponse.ReturnMessage;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}