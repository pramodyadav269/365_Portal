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
            if (HttpContext.Current.Session["UserId"] != null)
            {
                if (HttpContext.Current.Session["IsFirstTimeLogin"] != null)
                {
                    // This is 1st time login & user has not changed password..
                    Response.Redirect("~/settings.aspx");
                }
                if (HttpContext.Current.Session["IsFirstPasswordChanged"] != null)
                {
                    // This is 1st time login & user has not changed password..
                    Response.Redirect("~/ChangePassword.aspx");
                }

                if (HttpContext.Current.Session["FirstName"] != null && HttpContext.Current.Session["LastName"] != null)
                {
                    lblUserName.Text = HttpContext.Current.Session["FirstName"].ToString() + " " + HttpContext.Current.Session["LastName"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
}