using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace _365_Portal.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/User/LoginUser")]
        public IHttpActionResult LoginUser(UserBO obj)
        {
            string UserName = "";
            string Password = "";
            string StatusCode = "0";
            string Description = "";
            string Response = "";
            UserName = obj.UserName;
            Password = obj.UserPwd;

            bool IsValid = UserDetails.LoginUser(obj);

            ResponseModel res = new ResponseModel();
            if (IsValid)
            {
                if (GetAccessToken(UserName, Password))
                {
                    res.status = true;
                    string token = HttpContext.Current.Session["access_token"].ToString();
                    var userdetail = UserDetails.GetUserDetails(UserDetails.UserID);
                    var resm = new { token = token, userid = userdetail.UserId, name = userdetail.Name };
                    //HttpContext.Current.Session["refresh_token"].ToString();
                    string response = JsonConvert.SerializeObject(new { resm, res }, Formatting.Indented);
                    Response = response;
                }
            }
            else
            {
                res.Msg = "Please enter valid user credentials";
                res.status = false;
                Response = JsonConvert.SerializeObject(new { res }, Formatting.Indented); ;

            }
            return Ok(Response);
        }

        [HttpGet]
        [Route("api/User/GetForAuthenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            //var identity = (ClaimsIdentity)User.Identity;
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                var twest = identity.Id;
            }

            return Ok("Hello " + identity.Name);
        }

        public bool GetAccessToken(string userName, string password)
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
                    var result = client.PostAsync("Token", content).Result;
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(resultContent))
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        dynamic token = serializer.Deserialize<object>(resultContent);
                        string accessToken = token["access_token"];
                        HttpContext.Current.Session["access_token"] = accessToken;
                        //Added by pramod on 3 Nov 2018
                        HttpContext.Current.Session["expires_in"] = token["expires_in"];
                        HttpContext.Current.Session["refresh_token"] = token["refresh_token"];
                        //End by pramod on 3 Nov 2018
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

        public class ResponseModel
        {
            public bool status { get; set; }
            public string Msg { get; set; }
        }
    }
}
