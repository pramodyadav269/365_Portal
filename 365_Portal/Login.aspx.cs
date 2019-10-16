using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _365_Portal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            Session["UserId"] = txtUserEmail.Value;
            Session["Name"] = txtUserEmail.Value;
            Session["Role"] = txtUserEmail.Value;

            Response.Redirect("~/default.aspx");
        }
    }
}