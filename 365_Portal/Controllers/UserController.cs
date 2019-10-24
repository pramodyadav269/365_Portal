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
                string EmailId = httpRequest.Form["EmailID"];
                string UserPwd = httpRequest.Form["UserPwd"];

                if (string.IsNullOrEmpty(EmailId) || string.IsNullOrEmpty(UserPwd))
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

                    if (objResponse.ReturnCode == "1")
                    {
                        GetAccessToken(EmailId.Trim(), UserPwd.Trim());

                        if (HttpContext.Current.Session["access_token"] != null)
                        {
                            objResponse.Token = HttpContext.Current.Session["access_token"].ToString();
                            objServiceLog.RequestType = ConstantMessages.WebServiceLog.Success;
                        }
                        else
                        {
                            objResponse.ReturnMessage = ConstantMessages.WebServiceLog.GenericErrorMsg;
                            objResponse.ReturnCode = ConstantMessages.WebServiceLog.GenericErrorMsg;
                            objServiceLog.RequestType = ConstantMessages.WebServiceLog.Validation;
                        }
                    }
                    else
                    {
                        objResponse.ReturnMessage = objResponse.ReturnMessage;
                        objResponse.ReturnCode = objResponse.ReturnCode;
                        objServiceLog.RequestType = ConstantMessages.WebServiceLog.Validation;
                    }
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
        [Route("API/User/ChangePassword")]
        public IHttpActionResult ChangePassword(JObject requestParams)
        {
            var data = string.Empty;
            UserBO _userdetail = new UserBO();
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string NewPassword = requestParams["NewPassword"].ToString();
                string OldPassword = requestParams["OldPassword"].ToString();
                /*This fields are for the mobile Request*/
                string DeviceDetails = requestParams["DeviceDetails"].ToString();
                string DeviceType = requestParams["DeviceType"].ToString();
                string IPAddess = requestParams["IPAddess"].ToString();
                /*Condition to check whether the entered old Password is correct or wrong*/
                _userdetail.OldPassword = OldPassword;
                _userdetail.NewPassword = NewPassword;//clear Text Password getting From User.                    
                _userdetail.CompId = identity.CompId;
                _userdetail.UserID = identity.UserID;
                _userdetail.Token = identity.Token;
                if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(OldPassword))
                {
                    try
                    {
                        if ((HttpContext.Current.Request.Browser.IsMobileDevice == true || HttpContext.Current.Request.Browser.IsMobileDevice == false) && string.IsNullOrEmpty(DeviceDetails) && string.IsNullOrEmpty(DeviceType) && string.IsNullOrEmpty(DeviceType))
                        {
                            _userdetail.DeviceDetails = Utility.GetDeviceDetails(ConstantMessages.DeviceInfo.InfoType.Trim().ToLower());
                            _userdetail.DeviceType = Utility.GetDeviceDetails(ConstantMessages.DeviceInfo.DeviceTypeBrowser.Trim().ToLower());
                            _userdetail.IP_Address = Utility.GetClientIPaddress();
                        }
                        else
                        {
                            _userdetail.DeviceDetails = DeviceDetails;
                            _userdetail.DeviceType = DeviceType;
                            _userdetail.IP_Address = IPAddess;
                        }
                        _userdetail.CreatedBy = Convert.ToInt32(identity.UserID);

                        var ds = CommonBL.ChangePassword(_userdetail);
                        data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                        data = Utility.Successful(data);
                    }
                    catch (Exception ex)
                    {
                        data = ConstantMessages.ChangePassowrd.error_code;
                        data = Utility.Failed(data);
                    }
                }
                else
                {
                    data = Utility.Failed(data);
                }

            }
            else
            {
                data = Utility.AuthenticationError(); ;
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
            string UserName = (string)jsonResult.SelectToken("username");
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

        [Route("API/User/CreateUser")]
        [HttpPost]
        public IHttpActionResult CreateUser(JObject jsonResult)
        {
            var data = "";
            string UserName = (string)jsonResult.SelectToken("username");
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
