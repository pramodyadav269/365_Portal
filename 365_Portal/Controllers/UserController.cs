using _365_Portal.Code.DAL;
using _365_Portal.Common;
using _365_Portal.Models;
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
using static _365_Portal.Models.Login;

namespace _365_Portal.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/User/LoginUser")]
        public IHttpActionResult LoginUser()
        {
            WebServiceLog objServiceLog = new WebServiceLog();
            LoginResponse objResponse = null;
            string Response = string.Empty;

            objServiceLog.RequestTime = DateTime.Now;
            objServiceLog.ControllerName = this.GetType().Name;
            objServiceLog.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                var httpRequest = HttpContext.Current.Request;
                string EmailId = httpRequest.Form["EmailId"];
                string UserPwd = httpRequest.Form["UserPwd"];

                LoginRequest objRequest = new LoginRequest();
                objRequest.EmailId = EmailId;
                objRequest.UserPwd = UserPwd;

                objResponse = new LoginResponse();
                objResponse = UserDAL.LoginUser(objRequest);
                Response = JsonConvert.SerializeObject(objResponse, Formatting.Indented);
                objServiceLog.RequestString = JSONHelper.ConvertJsonToString(objRequest);
                objServiceLog.ResponseString = JSONHelper.ConvertJsonToString(objResponse);
                objServiceLog.RequestType = ConstantMessages.WebServiceLog.Success;
            }
            catch (Exception ex)
            {
                objServiceLog.ResponseString = "Exception " + ex.Message + " | " + ex.StackTrace;
                objServiceLog.RequestType = ConstantMessages.WebServiceLog.Exception;
            }
            finally
            {
                objServiceLog.ResponseTime = DateTime.Now;
                InsertRequestLog.SaveWebServiceLog(objServiceLog);
            }
            return Ok(Response);
        }

        [HttpPost]
        [Route("api/User/UserLogout")]
        public IHttpActionResult UserLogout()
        {
            WebServiceLog objServiceLog = new WebServiceLog();
            LoginResponse objResponse = null;
            string Response = string.Empty;

            objServiceLog.RequestTime = DateTime.Now;
            objServiceLog.ControllerName = this.GetType().Name;
            objServiceLog.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                var httpRequest = HttpContext.Current.Request;
                string EmailId = httpRequest.Form["EmailId"];

                LoginRequest objRequest = new LoginRequest();
                objRequest.EmailId = EmailId;

                objResponse = new LoginResponse();
                int i = UserDAL.UserLogout(objRequest);
                if (i > 0)
                {
                    objResponse.ReturnCode = "0";
                    objResponse.ReturnMessage = "User logout succesfully.";
                }
                else
                {
                    objResponse.ReturnCode = "1";
                    objResponse.ReturnMessage = "Unable to logout.";
                }
                Response = JsonConvert.SerializeObject(objResponse, Formatting.Indented);
                objServiceLog.RequestString = JSONHelper.ConvertJsonToString(objRequest);
                objServiceLog.ResponseString = JSONHelper.ConvertJsonToString(objResponse);
                objServiceLog.RequestType = ConstantMessages.WebServiceLog.Success;
            }
            catch (Exception ex)
            {
                objServiceLog.ResponseString = "Exception " + ex.Message + " | " + ex.StackTrace;
                objServiceLog.RequestType = ConstantMessages.WebServiceLog.Exception;
            }
            finally
            {
                objServiceLog.ResponseTime = DateTime.Now;
                InsertRequestLog.SaveWebServiceLog(objServiceLog);
            }
            return Ok(Response);
        }

        //Get request api to autheticate user
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
            return Ok("Hello " + identity.FirstName);
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

        [HttpPost]
        [Route("api/User/ChangePassword")]
        public IHttpActionResult ChangePassword()
        {
            return null;
        }
    }
}
