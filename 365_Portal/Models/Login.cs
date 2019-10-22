using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace _365_Portal.Models
{   
    public class Login 
    {
        public class LoginRequest //: RequestBase
        {
            public string EmailId { get; set; }
            public string UserPwd { get; set; }
            public string Ref1 { get; set; }
            public string Ref2 { get; set; }
            public string Ref3 { get; set; }
        }

        public class LoginResponse : ResponseBase
        {
            public string CompID { get; set; }
            public string UserID { get; set; }
            public string RoleID { get; set; }
            public string Role { get; set; }
            public string IsFirstLogin { get; set; }
            public string ProfilePicFileID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailID { get; set; }
            public string MobileNum { get; set; }
            public string Position { get; set; }
            public string PasswordHash { get; set; }
            public string PasswordSalt { get; set; }
        }

        public static bool GetAccessToken(string userName, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["TokenURL"]);
                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password)
                });
                    var result = client.PostAsync("/Token", content).Result;
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(resultContent))
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        dynamic token = serializer.Deserialize<object>(resultContent);
                        string accessToken = token["access_token"];
                        HttpContext.Current.Session["access_token"] = accessToken;
                        HttpContext.Current.Session["expires_in"] = token["expires_in"];
                        
                        return true;
                    }
                    else
                    {
                        // Invalid User-Id & Password
                        return false;
                    }
                    //resolve the access_token here for the later use
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}