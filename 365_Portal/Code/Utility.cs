using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using SendGrid;

namespace _365_Portal.Code
{
    public class Utility
    {
        public static string ConvertDataSetToJSONString(DataTable dt)
        {
            var lst = dt.AsEnumerable()
        .Select(r => r.Table.Columns.Cast<DataColumn>()
                .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])
               ).ToDictionary(z => z.Key, z => z.Value != null ? Utility.GetSafeString(z.Value.ToString(), true) : null)
        ).ToList();
            //now serialize it
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Serialize(lst);
        }

        public static string ConvertJsonToString<T>(T model)
        {
            string result = null;
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer JSSerialzer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var Jsonresult = JSSerialzer.Serialize(model);
                result = JSSerialzer.ConvertToType<string>(Jsonresult);
            }
            catch (Exception ex)
            {
                result = "I am some unhandled Error";
            }
            return result.ToString();
        }

        public static string GetHashedPassword(string value, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            byte[] src = Convert.FromBase64String(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        private static int saltLengthLimit = 32;
        public static string GetSalt()
        {
            return Convert.ToBase64String(GetSalt(saltLengthLimit));
        }

        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public static DateTime GetIST()
        {
            return DateTime.Now;
        }

        public static string GetSafeString(string str, bool safe)
        {
            if (safe)
            {
                return HttpUtility.HtmlEncode(str);
            }
            return str;
        }

        public static string GetJSONData(string statusCode, string statusDescription, string data)
        {
            if (string.IsNullOrEmpty(data))
                data = "[]";
            return "{\"StatusCode\":\"" + statusCode + "\",\"StatusDescription\":\"" + statusDescription + "\",\"Data\":" + data + "}";
        }

        public static string GetJSONData(string statusCode, string statusDescription, string contents, string questions, string flashcardIntro, string flashcards)
        {
            if (string.IsNullOrEmpty(contents))
                contents = "[]";
            if (string.IsNullOrEmpty(questions))
                questions = "[]";
            if (string.IsNullOrEmpty(flashcards))
                flashcards = "[]";
            return "{\"StatusCode\":\"" + statusCode + "\",\"StatusDescription\":\"" + statusDescription
                + "\"," + contents
                + ",\"Questions\":" + questions
                + ",\"FlachardsIntro\":" + flashcardIntro
                + ",\"Flachards\":" + flashcards + "}";
        }

        public static string GetModulesJSONFormat(string statusCode, string statusDescription,string data, string unlockedModules, string lockedModules)
        {
            if (string.IsNullOrEmpty(unlockedModules))
                unlockedModules = "[]";
            if (string.IsNullOrEmpty(lockedModules))
                lockedModules = "[]";
            return "{\"StatusCode\":\"" + statusCode + "\",\"StatusDescription\":\"" + statusDescription
                 + "\"," + data
                + ",\"UnlockedItems\":" + unlockedModules
                + ",\"LockedItems\":" + lockedModules + "}";
        }

        public static string API_Status(string statusCode, string statusDescription)
        {
            return "{\"StatusCode\":\"" + statusCode + "\",\"StatusDescription\":\"" + statusDescription + "\"}";
            //return "{'StatusCode':'" + statusCode + "','StatusDescription':'" + statusDescription + "'}";
        }

        public static string AuthenticationError()
        {
            return API_Status("0", "Invalid Token");
        }

        public static string Successful(string data)
        {
            return GetJSONData("1", "Successful", data);
        }

        public static string Failed(string data)
        {
            return GetJSONData("0", "Failed", data);
        }


        public static string GetClientIPaddress()
        {
            string IPAddress = "";
            // This Condition Checks is the Request is from Proxy server. 
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
            {
                IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                IPAddress = HttpContext.Current.Request.UserHostAddress;
            }
            return IPAddress;
        }

        public static string GetClientIPAddress_1()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetDeviceDetails(string Type)
        {
            var Device_Details = string.Empty;
            if (Type == ConstantMessages.DeviceInfo.InfoType.Trim().ToLower())
            {
                Device_Details = "Browser Name : " + HttpContext.Current.Request.Browser.Browser + ", Browser Version : " + HttpContext.Current.Request.Browser.Version;
            }
            else
            {
                if (HttpContext.Current.Request.Browser.IsMobileDevice == false)
                {
                    Device_Details = "Desktop Browser";
                }
                else
                {
                    Device_Details = "Mobile Browser";
                }
            }
            return Device_Details;
        }


        public static Int32 GenerateOTP()
        {
            Random random = new Random();
            int OTP = random.Next(1111, 9999);
            return OTP;
        }
        /// <summary>
        /// Email Function
        /// </summary>
        /// <param name="from">Sender Email address</param>
        /// <param name="to">Recipient Email address </param>
        /// <param name="body">Body of the Mail</param>
        /// <param name="subject"> Subject for the Mail</param>
        /// <returns>Returns True or False</returns>
        public static bool SendEmail(string from, string to, string body, string subject)

        {
            try
            {

                var message = new SendGridMessage { From = new MailAddress(@from) };
                message.AddTo(to);
                message.Subject = subject;
                message.Html = body;
                //message.IsBodyHtml = true;
                //var client = new SmtpClient();
                //client.Send(message);

                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential("azure_9a292e7a8ba7e144014a30bd24a38ded@azure.com", "0gu8HfsYQnWF3EK");

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);
                // Send the email, which returns an awaitable task.
                Thread.Sleep(2000);
                transportWeb.DeliverAsync(message);
                Thread.Sleep(2000);
                return true;
            }
            catch (Exception ex)
            {
                StackTrace trace = new StackTrace(ex, true);
                StackFrame stackFrame = trace.GetFrame(trace.FrameCount - 1);
                string fileName = stackFrame.GetFileName();
                string methodName = stackFrame.GetMethod().Name;
                int lineNo = stackFrame.GetFileLineNumber();
                return false;
            }
        }
    }
}