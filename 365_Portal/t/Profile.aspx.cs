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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["FirstName"] != null && HttpContext.Current.Session["LastName"] != null)
            {
                lblUserName.InnerText = HttpContext.Current.Session["FirstName"].ToString() + " " + HttpContext.Current.Session["LastName"].ToString();

                if (HttpContext.Current.Session["ProfilePicFile"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["ProfilePicFile"].ToString()))
                {
                    imgProfilePic.Src = "../Files/ProfilePic/" + HttpContext.Current.Session["ProfilePicFile"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
}