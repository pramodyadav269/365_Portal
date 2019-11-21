using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _365_Portal
{
    public partial class Settings : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["FirstName"] != null && HttpContext.Current.Session["LastName"] != null)
            {

            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
}