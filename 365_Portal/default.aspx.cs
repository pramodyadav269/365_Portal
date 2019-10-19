using System;

namespace Life
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Take UserName from Session.
            dvUserName.InnerText = "Hello, Daniel!";
        }
    }
}