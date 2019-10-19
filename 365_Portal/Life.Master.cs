using System;

namespace _365_Portal
{
    public partial class Life : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {

            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
}