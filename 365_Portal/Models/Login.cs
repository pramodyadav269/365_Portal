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
            public string UserName { get; set; }
            public string Password { get; set; }
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
            public string Token { get; set; }
            public string IsFirstPasswordChanged { get; set; }
            
        }

        public static string GetAccessToken(string userName, string password)
        {
            string accessToken = string.Empty;
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
                        accessToken = token["access_token"];
                        HttpContext.Current.Session["access_token"] = accessToken;
                        HttpContext.Current.Session["expires_in"] = token["expires_in"];
                    }
                    else
                    {
                        // Invalid User-Id & Password
                        return "";
                    }
                    //resolve the access_token here for the later use
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return accessToken;
        }
    }


}