using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _365_Portal.Admin
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["RoleName"] != null)
            {
                if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.superadmin)
                {

                }
                else if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.companyadmin)
                {
                    dvAdminTasks.Visible = true;
                }
                else if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.subadmin)
                {

                }
                else if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.enduser)
                {
                    Response.Redirect("~/t/default.aspx");
                }
                // Take UserName from Session.
                dvUserName.InnerText = "Welcome back, " + HttpContext.Current.Session["FirstName"] + "!!";
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
}