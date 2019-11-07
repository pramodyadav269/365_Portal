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
using System.IO;

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

        public static string GetBase64ImageByFileID(string FileID, string Directory)
        {
            string Base64String = string.Empty;
            try
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Directory) + FileID))
                {
                    using (System.IO.MemoryStream m = new System.IO.MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        Base64String = Convert.ToBase64String(imageBytes);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Base64String;
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
                + ",\"FlashcardsIntro\":" + flashcardIntro
                + ",\"Flashcards\":" + flashcards + "}";
        }

        public static string GetAchievementGiftsJSONFormat(string statusCode, string statusDescription, string achievements, string gifts)
        {
            if (string.IsNullOrEmpty(achievements))
                achievements = "[]";
            if (string.IsNullOrEmpty(gifts))
                gifts = "[]";
            return "{\"StatusCode\":\"" + statusCode + "\",\"StatusDescription\":\"" + statusDescription
                 + "\""
                + ",\"Achievements\":" + achievements
                + ",\"Gifts\":" + gifts + "}";
        }

        public static string GetModulesJSONFormat(string statusCode, string statusDescription, string data, string unlockedModules, string lockedModules)
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

        public static string Exception(Exception ex)
        {
            return API_Status("0", "There might be some error " + ex.Message);
        }

        public static string Successful(string data)
        {
            return GetJSONData("1", "Successful", data);
        }

        public static string ContentUpdated(string statusCode, string statusDescription, string isGift, string data)
        {
            if (string.IsNullOrEmpty(data))
                data = "[]";
            return "{\"StatusCode\":\"" + statusCode + "\",\"StatusDescription\":\"" + statusDescription + "\",\"IsGift\":\"" + isGift + "\",\"Data\":" + data + "}";
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


        public static int GenerateOTP(int length)
        {
            string numbers = "1234567890";
            string characters = numbers;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }

            return Convert.ToInt32(otp);
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
                var credentials = new NetworkCredential("", "");

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


        /// <summary>
        /// Send Sms Function
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="msg"></param>
        /// <returns>True of False</returns>
        public static string SendSMS(string mobileNo, string msg)
        {
            string api_Response = string.Empty;
            string sender_Code = string.Empty;
            try
            {
                /*
                                string url = baseURL + string.Format("sms.php?uid={0}&pin={1}&senderid={2}&route={3}&mobile={4}&message={5}&pushid={6}"
                              , champRechargeLoginId, champRechargePassword, champRechargeSenderid, champRechargeroute, mobileNo, msg, champRechargepushid);

                                var request = (HttpWebRequest)WebRequest.Create(url);
                                var response = (HttpWebResponse)request.GetResponse();
                                using (var streamReader = new StreamReader(response.GetResponseStream()))
                                {
                                    var result = streamReader.ReadToEnd();

                                    var respDocument = new XmlDocument();
                                    respDocument.LoadXml(result);

                                    var status = respDocument.GetElementsByTagName("status").Item(0).InnerText;
                                    var errorCode = respDocument.GetElementsByTagName("error_code").Item(0).InnerText;
                                    var message = respDocument.GetElementsByTagName("message").Item(0).InnerText;
                                    var senderId = respDocument.GetElementsByTagName("senderid").Item(0).InnerText;

                                    //    //check errorcode return by api
                                    if (!string.IsNullOrEmpty(errorCode))
                                    {
                                        api_Response = errorCode;
                                        if (api_Response == Constants.StatusMessage.Sender.SenderRegistrationSubmissionCode)
                                        {
                                            sender_Code = senderId;
                                        }
                                    }
                                    else
                                    {
                                        api_Response = Constants.StatusMessage.Common.Exception;
                                    }
                                }
                                */
            }
            catch (Exception ex)
            {
                api_Response = null;
            }
            return null;

        }

        public static string urlNewShorter(string Url)
        {
            try
            {

                if (!Url.ToLower().StartsWith("http") && !Url.ToLower().StartsWith("ftp"))
                {
                    Url = "http://" + Url;
                }
                var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + Url);
                var res = request.GetResponse();
                string text;
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    text = reader.ReadToEnd();
                }
                return text;
            }
            catch (Exception ex)
            {
                //Add Exception Code
                return Url;
            }
        }

        public static void DestroyAllSession()
        {
            System.Web.HttpContext.Current.Session.Clear();
            //System.Web.HttpContext.Current.Session.Abandon(); //Commented because this is does not allowing to create sessnio
        }

        public static void CreateUserSession(string UserID,string Role,string FirstName,string LastName)
        {
            System.Web.HttpContext.Current.Session["UserId"] = UserID;
            System.Web.HttpContext.Current.Session["RoleName"] = Role;
            System.Web.HttpContext.Current.Session["FirstName"] = FirstName;
            System.Web.HttpContext.Current.Session["LastName"] = LastName;
        }
        public static void CreateProfileAndThemeSession(string ProfilePicFileID, string CompanyProfilePicFileID, string ThemeColor)
        {
            System.Web.HttpContext.Current.Session["ProfilePicFile"] = ProfilePicFileID;
            System.Web.HttpContext.Current.Session["CompanyProfilePicFile"] = CompanyProfilePicFileID;
            System.Web.HttpContext.Current.Session["ThemeColor"] = ThemeColor;
        }
        public static void CreateFirstLoginSession(bool flag)
        {
            System.Web.HttpContext.Current.Session["IsFirstLogin"] = flag;
        }
        public static void CreateFirstPasswordNotChangedSession(bool flag)
        {
            System.Web.HttpContext.Current.Session["IsFirstPasswordNotChanged"] = flag;
        }
    }
}