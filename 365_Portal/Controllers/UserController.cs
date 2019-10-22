using _365_Portal.Code.DAL;
using _365_Portal.Code.BL;
using _365_Portal.Common;
using _365_Portal.Models;
using _365_Portal.Code;
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
using Newtonsoft.Json.Linq;
using System.Web.Configuration;

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

                if (!string.IsNullOrEmpty(EmailId) || !string.IsNullOrEmpty(UserPwd))
                {
                    objResponse = new LoginResponse();
                    objResponse.ReturnCode = ConstantMessages.Login.InvalidUserCode;
                    objResponse.ReturnMessage = ConstantMessages.Login.InvalidUser;
                    objServiceLog.RequestType = ConstantMessages.WebServiceLog.Validation;
                }
                else
                {
                    LoginRequest objRequest = new LoginRequest();
                    objRequest.EmailId = EmailId;
                    objRequest.UserPwd = UserPwd;
                    objServiceLog.RequestString = JSONHelper.ConvertJsonToString(objRequest);

                    objResponse = new LoginResponse();
                    objResponse = UserDAL.LoginUser(objRequest);
                    objServiceLog.RequestType = ConstantMessages.WebServiceLog.Success;
                }

                Response = JsonConvert.SerializeObject(objResponse, Formatting.Indented);                
                objServiceLog.ResponseString = JSONHelper.ConvertJsonToString(objResponse);
                
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
                var twest = identity.UserID;
            }
            return Ok("Hello " + identity.FirstName);
        }
        
        /// <summary>
        /// Change password Api
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns>
        /// Result as True or false 
        /// </returns>
        [HttpPost]
        [Route("api/User/ChangePassword")]
        public IHttpActionResult ChangePassword(JObject jsonResult)
        {
            var data = "";
            UserBO _userdetail = new UserBO();
            string NewPassword = (string)jsonResult.SelectToken("new_password");
            string PasswordSalt = WebConfigurationManager.AppSettings["dbPasswordSalt"].ToString();
            string New_PasswordSalt = Utility.GetHashedPassword(NewPassword, PasswordSalt);
            try
            {
                _userdetail.PasswordHash = NewPassword;
                _userdetail.PasswordSalt = New_PasswordSalt;
                _userdetail.CompID = Convert.ToString(HttpContext.Current.Session["CompID"]);
                _userdetail.UserID = Convert.ToString(HttpContext.Current.Session["UserId"]);
                //_userdetail.Token = "";
                //_userdetail.DeviceDetails = "";
                //_userdetail.DeviceType = "";
                //_userdetail.IP_Addess = "";
                //_userdetail.CreatedBy = 0;
                var ds = CommonBL.ChangePassword(_userdetail); 
                data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                APIResult.ThrowException(ex);
            }
            return new APIResult(data, Request);
        }

        /// <summary>
        /// Gives the Login Details for User
        /// </summary>
        /// <param name="jsonResult">Username In Json result</param>
        /// <returns>
        /// Login Details
        /// </returns>
        [HttpPost] 
        [Route("api/User/GetLoginDetails")]
        public IHttpActionResult GetLoginDetails(JObject jsonResult)
        {
            var data = "";
            string UserName =(string)jsonResult.SelectToken("username");
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrWhiteSpace(UserName))
            {
                try
                {
                    //var ds =  CommonBL.GetLoginDetails(1);
                    var ds = CommonBL.GetLoginDetails(UserName);
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                }
                catch (Exception ex)
                {
                    APIResult.ThrowException(ex);
                }
            }
            else
            {
                data = "UserName is not entered. Please enter a UserName";
            }
            return new APIResult(data, Request);
        }

         

    }
}
