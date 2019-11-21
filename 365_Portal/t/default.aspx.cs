using System;
using System.Web;

namespace Life
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["RoleName"]) == "superadmin"
                || Convert.ToString(Session["RoleName"]) == "companyadmin"
                || Convert.ToString(Session["RoleName"]) == "subadmin"
                )
            {
                Response.Redirect("~/t/dashboard.aspx");
            }
            // Take UserName from Session.
            dvUserName.InnerText = "Hello, " + HttpContext.Current.Session["FirstName"] + "!";
        }
    }
}