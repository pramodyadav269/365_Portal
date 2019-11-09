using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _365_Portal
{
    public partial class Profile : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            MasterPageFile = Convert.ToString(Session["MasterPage"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["FirstName"] != null && HttpContext.Current.Session["LastName"] != null)
            {
                lblUserName.InnerText = HttpContext.Current.Session["FirstName"].ToString() + " " + HttpContext.Current.Session["LastName"].ToString();
            }
        }
    }
}