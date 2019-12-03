using _365_Portal.Models;
using System;
using System.Web;

namespace Life
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["RoleName"]) == "superadmin")
            {
                Response.Redirect("~/t/dashboard.aspx");
            }
            else if (HttpContext.Current.Session["RoleName"].ToString() == ConstantMessages.Roles.companyadmin)
            {
                dvAdminTasks.Visible = true;
            }
           

            // Take UserName from Session.
            dvUserName.InnerText = "Hey " + HttpContext.Current.Session["FirstName"] + "!!";
        }
    }
}