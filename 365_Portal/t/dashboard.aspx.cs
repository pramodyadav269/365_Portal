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
            if (Convert.ToString(Session["RoleName"]) == "enduser")
            {
                Response.Redirect("~/t/default.aspx");
            }
        }
    }
}