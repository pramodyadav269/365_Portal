using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _365_Portal
{
    public partial class Life : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserId"] != null)//&& Convert.ToString(Session["RoleName"]) == "enduser"
            {
                string CurrDirecotry = Server.MapPath("/").ToString();
                string FullPath = CurrDirecotry + System.IO.Path.GetFileName(Request.Url.AbsolutePath);

                if (HttpContext.Current.Session["IsFirstLogin"] != null && Convert.ToBoolean(HttpContext.Current.Session["IsFirstLogin"]) == true
                    && FullPath != CurrDirecotry + "Settings.aspx" && FullPath != CurrDirecotry + "ChangePassword.aspx")
                {
                    Response.Redirect("~/Settings.aspx", true);// This is 1st time login..
                }
                else if (HttpContext.Current.Session["IsFirstPasswordNotChanged"] != null && Convert.ToBoolean(HttpContext.Current.Session["IsFirstPasswordNotChanged"]) == true
                    && FullPath != CurrDirecotry + "ChangePassword.aspx" && FullPath != CurrDirecotry + "Settings.aspx")
                {
                    Response.Redirect("~/ChangePassword.aspx", true);// This is user has not changed password..
                }
                if (HttpContext.Current.Session["FirstName"] != null && HttpContext.Current.Session["LastName"] != null)
                {
                    lblUserName.Text = HttpContext.Current.Session["FirstName"].ToString() + " " + HttpContext.Current.Session["LastName"].ToString();
                }

                if (HttpContext.Current.Session["ProfilePicFile"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["ProfilePicFile"].ToString()))
                {
                    imgProfilePic.Src = "/Files/ProfilePic/" + HttpContext.Current.Session["ProfilePicFile"].ToString();
                }
                if (HttpContext.Current.Session["CompanyProfilePicFile"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["CompanyProfilePicFile"].ToString()))
                {
                    imgCompanyLogo.Src = "/Files/CompLogo/" + HttpContext.Current.Session["CompanyProfilePicFile"].ToString();
                }

                if (HttpContext.Current.Session["RoleName"] != null)
                {
                    if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.superadmin
                        || HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.companyadmin
                    )
                    {
                        menuUsers.Visible = true;
                    }
                    else
                    {
                        menuUsers.Visible = false;
                    }
                }
                
            }
            else
            {
                Response.Redirect("~/login.aspx");

            }
        }
    }
}