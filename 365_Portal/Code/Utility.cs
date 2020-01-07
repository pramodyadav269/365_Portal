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
using System.Configuration;

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

        public static string ConvertDataSetToJSONString(DataSet ds)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
        }

        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);
            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
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

        public static void CreateUserSession(string UserID, string Role, string FirstName, string LastName, int CompID)
        {
            System.Web.HttpContext.Current.Session["UserId"] = UserID;
            System.Web.HttpContext.Current.Session["CompId"] = CompID;
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



        public static bool CheckFileValidation(HttpPostedFile file)
        {
            bool isValid = false;
            string fileName = file.FileName.ToLower();
            string contentType = file.ContentType;
            string extension = System.IO.Path.GetExtension(fileName);
            string MiMEType = GetMIMEType(extension);
            string[] validFileTypes = { "pdf", "mp4", "avi", "flv", "wmv", "mov", "3gp", "webm", "wav" };
            int allowedfilesize = Convert.ToInt32(ConfigurationManager.AppSettings["FileUploadSize"]);
            string fileExtension = System.IO.Path.GetExtension(fileName);
            if (fileExtension == "")
            {
                return isValid = false;
            }

            if (contentType.ToLower().Trim() == MiMEType.ToLower().Trim())
            {
                isValid = true;
            }
            else
            {
                return isValid = false;
            }
            if (Convert.ToInt32(file.ContentLength) <= allowedfilesize)
            {
                isValid = true;
            }
            else
            {
                return isValid = false;
            }
            for (int j = 0; j < validFileTypes.Length; j++)
            {
                if (fileExtension.Trim().ToUpper() == "." + validFileTypes[j].Trim().ToUpper())
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        public static string GetMIMEType(string extension)
        {
            if (extension.Length > 0 &&
              Utility.MIMETypesDictionary.ContainsKey(extension.Remove(0, 1)))
            {
                return Utility.MIMETypesDictionary[extension.Remove(0, 1)];
            }
            return "unknown/unknown";
        }

        public static string GetYoutubeVideoLink(string link)
        {
            if (link.Trim().ToUpper().Contains("WWW.YOUTUBE.COM"))
            {
                if (!link.Trim().ToUpper().Contains("WWW.YOUTUBE.COM/EMBED"))
                {
                    Uri uri = new Uri(link);
                    string videoId = HttpUtility.ParseQueryString(uri.Query).Get("v");
                    return "https://www.youtube.com/embed/" + videoId + "?enablejsapi=1";
                }
            }
            return link;
        }

        public static readonly Dictionary<string, string> MIMETypesDictionary = new Dictionary<string, string>
     {
                            {"ai", "application/postscript"},
                            {"aif", "audio/x-aiff"},
                            {"aifc", "audio/x-aiff"},
                            {"aiff", "audio/x-aiff"},
                            {"asc", "text/plain"},
                            {"atom", "application/atom+xml"},
                            {"au", "audio/basic"},
                            {"avi", "video/x-msvideo"},
                            {"bcpio", "application/x-bcpio"},
                            {"bin", "application/octet-stream"},
                            {"bmp", "image/bmp"},
                            {"cdf", "application/x-netcdf"},
                            {"cgm", "image/cgm"},
                            {"class", "application/octet-stream"},
                            {"cpio", "application/x-cpio"},
                            {"cpt", "application/mac-compactpro"},
                            {"csh", "application/x-csh"},
                            {"css", "text/css"},
                            {"dcr", "application/x-director"},
                            {"dif", "video/x-dv"},
                            {"dir", "application/x-director"},
                            {"djv", "image/vnd.djvu"},
                            {"djvu", "image/vnd.djvu"},
                            {"dll", "application/octet-stream"},
                            {"dmg", "application/octet-stream"},
                            {"dms", "application/octet-stream"},
                            {"doc", "application/msword"},
                            {"docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                            {"dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
                            {"docm","application/vnd.ms-word.document.macroEnabled.12"},
                            {"dotm","application/vnd.ms-word.template.macroEnabled.12"},
                            {"dtd", "application/xml-dtd"},
                            {"dv", "video/x-dv"},
                            {"dvi", "application/x-dvi"},
                            {"dxr", "application/x-director"},
                            {"eps", "application/postscript"},
                            {"etx", "text/x-setext"},
                            {"exe", "application/octet-stream"},
                            {"ez", "application/andrew-inset"},
                            {"gif", "image/gif"},
                            {"gram", "application/srgs"},
                            {"grxml", "application/srgs+xml"},
                            {"gtar", "application/x-gtar"},
                            {"hdf", "application/x-hdf"},
                            {"hqx", "application/mac-binhex40"},
                            {"htm", "text/html"},
                            {"html", "text/html"},
                            {"ice", "x-conference/x-cooltalk"},
                            {"ico", "image/x-icon"},
                            {"ics", "text/calendar"},
                            {"ief", "image/ief"},
                            {"ifb", "text/calendar"},
                            {"iges", "model/iges"},
                            {"igs", "model/iges"},
                            {"jnlp", "application/x-java-jnlp-file"},
                            {"jp2", "image/jp2"},
                            {"jpe", "image/jpeg"},
                            {"jpeg", "image/jpeg"},
                            {"jpg", "image/jpeg"},
                            {"js", "application/x-javascript"},
                            {"kar", "audio/midi"},
                            {"latex", "application/x-latex"},
                            {"lha", "application/octet-stream"},
                            {"lzh", "application/octet-stream"},
                            {"m3u", "audio/x-mpegurl"},
                            {"m4a", "audio/mp4a-latm"},
                            {"m4b", "audio/mp4a-latm"},
                            {"m4p", "audio/mp4a-latm"},
                            {"m4u", "video/vnd.mpegurl"},
                            {"m4v", "video/x-m4v"},
                            {"mac", "image/x-macpaint"},
                            {"man", "application/x-troff-man"},
                            {"mathml", "application/mathml+xml"},
                            {"me", "application/x-troff-me"},
                            {"mesh", "model/mesh"},
                            {"mid", "audio/midi"},
                            {"midi", "audio/midi"},
                            {"mif", "application/vnd.mif"},
                            {"mov", "video/quicktime"},
                            {"movie", "video/x-sgi-movie"},
                            {"mp2", "audio/mpeg"},
                            {"mp3", "audio/mpeg"},
                            {"mp4", "video/mp4"},
                            {"mpe", "video/mpeg"},
                            {"mpeg", "video/mpeg"},
                            {"mpg", "video/mpeg"},
                            {"mpga", "audio/mpeg"},
                            {"ms", "application/x-troff-ms"},
                            {"msh", "model/mesh"},
                            {"mxu", "video/vnd.mpegurl"},
                            {"nc", "application/x-netcdf"},
                            {"oda", "application/oda"},
                            {"ogg", "application/ogg"},
                            {"pbm", "image/x-portable-bitmap"},
                            {"pct", "image/pict"},
                            {"pdb", "chemical/x-pdb"},
                            {"pdf", "application/pdf"},
                            {"pgm", "image/x-portable-graymap"},
                            {"pgn", "application/x-chess-pgn"},
                            {"pic", "image/pict"},
                            {"pict", "image/pict"},
                            {"png", "image/png"},
                            {"pnm", "image/x-portable-anymap"},
                            {"pnt", "image/x-macpaint"},
                            {"pntg", "image/x-macpaint"},
                            {"ppm", "image/x-portable-pixmap"},
                            {"ppt", "application/vnd.ms-powerpoint"},
                            {"pptx","application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                            {"potx","application/vnd.openxmlformats-officedocument.presentationml.template"},
                            {"ppsx","application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
                            {"ppam","application/vnd.ms-powerpoint.addin.macroEnabled.12"},
                            {"pptm","application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
                            {"potm","application/vnd.ms-powerpoint.template.macroEnabled.12"},
                            {"ppsm","application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
                            {"ps", "application/postscript"},
                            {"qt", "video/quicktime"},
                            {"qti", "image/x-quicktime"},
                            {"qtif", "image/x-quicktime"},
                            {"ra", "audio/x-pn-realaudio"},
                            {"ram", "audio/x-pn-realaudio"},
                            {"ras", "image/x-cmu-raster"},
                            {"rdf", "application/rdf+xml"},
                            {"rgb", "image/x-rgb"},
                            {"rm", "application/vnd.rn-realmedia"},
                            {"roff", "application/x-troff"},
                            {"rtf", "text/rtf"},
                            {"rtx", "text/richtext"},
                            {"sgm", "text/sgml"},
                            {"sgml", "text/sgml"},
                            {"sh", "application/x-sh"},
                            {"shar", "application/x-shar"},
                            {"silo", "model/mesh"},
                            {"sit", "application/x-stuffit"},
                            {"skd", "application/x-koan"},
                            {"skm", "application/x-koan"},
                            {"skp", "application/x-koan"},
                            {"skt", "application/x-koan"},
                            {"smi", "application/smil"},
                            {"smil", "application/smil"},
                            {"snd", "audio/basic"},
                            {"so", "application/octet-stream"},
                            {"spl", "application/x-futuresplash"},
                            {"src", "application/x-wais-source"},
                            {"sv4cpio", "application/x-sv4cpio"},
                            {"sv4crc", "application/x-sv4crc"},
                            {"svg", "image/svg+xml"},
                            {"swf", "application/x-shockwave-flash"},
                            {"t", "application/x-troff"},
                            {"tar", "application/x-tar"},
                            {"tcl", "application/x-tcl"},
                            {"tex", "application/x-tex"},
                            {"texi", "application/x-texinfo"},
                            {"texinfo", "application/x-texinfo"},
                            {"tif", "image/tiff"},
                            {"tiff", "image/tiff"},
                            {"tr", "application/x-troff"},
                            {"tsv", "text/tab-separated-values"},
                            {"txt", "text/plain"},
                            {"ustar", "application/x-ustar"},
                            {"vcd", "application/x-cdlink"},
                            {"vrml", "model/vrml"},
                            {"vxml", "application/voicexml+xml"},
                            {"wav", "audio/x-wav"},
                            {"wbmp", "image/vnd.wap.wbmp"},
                            {"wbmxl", "application/vnd.wap.wbxml"},
                            {"wml", "text/vnd.wap.wml"},
                            {"wmlc", "application/vnd.wap.wmlc"},
                            {"wmls", "text/vnd.wap.wmlscript"},
                            {"wmlsc", "application/vnd.wap.wmlscriptc"},
                            {"wrl", "model/vrml"},
                            {"xbm", "image/x-xbitmap"},
                            {"xht", "application/xhtml+xml"},
                            {"xhtml", "application/xhtml+xml"},
                            {"xls", "application/vnd.ms-excel"},
                            {"xml", "application/xml"},
                            {"xpm", "image/x-xpixmap"},
                            {"xsl", "application/xml"},
                            {"xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                            {"xltx","application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
                            {"xlsm","application/vnd.ms-excel.sheet.macroEnabled.12"},
                            {"xltm","application/vnd.ms-excel.template.macroEnabled.12"},
                            {"xlam","application/vnd.ms-excel.addin.macroEnabled.12"},
                            {"xlsb","application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
                            {"xslt", "application/xslt+xml"},
                            {"xul", "application/vnd.mozilla.xul+xml"},
                            {"xwd", "image/x-xwindowdump"},
                            {"xyz", "chemical/x-xyz"},
                            {"zip", "application/zip"}
    };
    }
}