using _365_Portal.Code.BL;
using _365_Portal.Code.DAL;
using MasterPayReportingModule.App_Code;
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

            var passwordSalt = Utility.GetSalt();

            // Call BL to get Hashed Password & Salt by UserName
            var dbPasswordSalt = "SUqfy3IHGUG4YVM/aO7lXWIKz4FAP18spNMiFdiGKNQ=";
            var dbPasswordHashed = "hdc5ZLZ4sE75e3OqbgR+PqXtr1Y=";

            var passwordHashed = Utility.GetHashedPassword(txtUserPassword.Value, dbPasswordSalt);

            if (passwordHashed == dbPasswordHashed)
            {
                // Correct Password
            }
            else
            {
                // Incorrect Password
            }

            var ds = TrainningBL.GetTopics(1, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("~/default.aspx");
            }
        }
    }
}