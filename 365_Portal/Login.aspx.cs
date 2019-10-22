﻿using _365_Portal.Code;
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
                    bool flag = GetAccessToken(txtUserEmail.Text.Trim(), txtUserPassword.Text.Trim());

                    if (!flag)
                    {
                        lblError.Text = ConstantMessages.WebServiceLog.GenericErrorMsg;
                        return;
                    }

                    // Call Login Business Layer Function to record message

                    HttpContext.Current.Session["UserId"] = objResponse.UserID;
                    HttpContext.Current.Session["RoleName"] = objResponse.RoleID;
                  
                    if (objResponse.IsFirstLogin == "1")
                    {
                        HttpContext.Current.Session["IsFirstTimeLogin"] = true;
                        Response.Redirect("~/ChangePassword.aspx");
                    }
                    else
                    {                       
                        if (objResponse.RoleID.ToLower() == "enduser")
                        {
                            Response.Redirect("~/Topics.aspx");
                        }
                        else if (objResponse.RoleID.ToLower() == "superadmin" || objResponse.RoleID.ToLower() == "companyadmin" || objResponse.RoleID.ToLower() == "subadmin")
                        {
                            Response.Redirect("~/admin/dashboard.aspx");
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
    }
}