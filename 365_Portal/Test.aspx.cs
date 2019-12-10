using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace _365_Portal
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Run();
            //Execute().RunSynchronously();
        }
        static async Task Execute()
        {
            //var dd = Environment.GetEnvironmentVariables();
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient("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");
            var from = new EmailAddress("yogtambe.it@gmail.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("yogtambe.it@gmail.com", "Example User");
            var plainTextContent = " Async and easy to do anywhere, even with C# " + DateTime.Now;
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        static void Run()
        {
            //var dd = Environment.GetEnvironmentVariables();
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient("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");
            var from = new EmailAddress("yogtambe.it@gmail.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("yogtambe.it@gmail.com", "Example User");
            var plainTextContent = "Run and easy to do anywhere, even with C# " + DateTime.Now;
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}