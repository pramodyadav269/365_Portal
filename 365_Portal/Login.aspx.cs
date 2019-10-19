using _365_Portal.Code.DAL;
using System;

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

            var ds = TrainningDAL.GetUserTopics("1");

            Response.Redirect("~/default.aspx");
        }
    }
}