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
using System.Text.RegularExpressions;
using System.Data;

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
                    objRequest.UserName = EmailId;
                    objRequest.Password = UserPwd;
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
                objRequest.UserName = EmailId;

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
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");// Password Validation regex with atleast 1 lowercase,1 Uppercase,1 numeric,1 special charcter and 8 Charcters long  
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string NewPassword = requestParams["NewPassword"].ToString();
                string OldPassword = requestParams["OldPassword"].ToString();
                /*This fields are for the mobile Request*/
                string DeviceDetails = requestParams["DeviceDetails"].ToString();
                string DeviceType = requestParams["DeviceType"].ToString();
                /*Condition to check whether the entered old Password is correct or wrong*/
                _userdetail.OldPassword = OldPassword;
                _userdetail.NewPassword = NewPassword;//clear Text Password getting From User.                    
                _userdetail.CompId = identity.CompId;
                _userdetail.UserID = identity.UserID;

                if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(OldPassword))
                {
                    if (NewPassword.Trim().ToLower() != OldPassword.Trim().ToLower())
                    {
                        var UserBO = UserDAL.GetUserDetailsByUserID(identity.EmailID, "");
                        var OldPasswordSalt = UserBO.PasswordSalt;
                        var OldPasswordHash = Utility.GetHashedPassword(OldPassword, OldPasswordSalt);

                        if (OldPasswordHash == UserBO.PasswordHash)
                        {
                            Match match = regex.Match(_userdetail.NewPassword);
                            if (match.Success)
                            {
                                try
                                {
                                    if ((HttpContext.Current.Request.Browser.IsMobileDevice == true || HttpContext.Current.Request.Browser.IsMobileDevice == false) && string.IsNullOrEmpty(DeviceDetails) && string.IsNullOrEmpty(DeviceType) && string.IsNullOrEmpty(DeviceType))
                                    {
                                        _userdetail.DeviceDetails = Utility.GetDeviceDetails(ConstantMessages.DeviceInfo.InfoType.Trim().ToLower());
                                        _userdetail.DeviceType = Utility.GetDeviceDetails(ConstantMessages.DeviceInfo.DeviceTypeBrowser.Trim().ToLower());

                                    }
                                    else
                                    {
                                        _userdetail.DeviceDetails = DeviceDetails;
                                        _userdetail.DeviceType = DeviceType;
                                    }
                                    _userdetail.IP_Address = Utility.GetClientIPaddress();
                                    _userdetail.CreatedBy = Convert.ToInt32(identity.UserID);

                                    var ds = CommonBL.ChangePassword(_userdetail);
                                    if (ds.Tables.Count > 0)
                                    {
                                        DataTable dt = ds.Tables["Data"];
                                        if (dt.Rows[0]["ReturnCode"].ToString() == "1")
                                        {
                                            data = Utility.ConvertDataSetToJSONString(dt);
                                            data = Utility.Successful(data);
                                        }
                                        else
                                        {

                                            data = ConstantMessages.ChangePassowrd.Error;
                                            data = Utility.Failed(data);
                                        }

                                    }
                                    else
                                    {
                                        data = ConstantMessages.ChangePassowrd.Error;
                                        data = Utility.Failed(data);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    data = ConstantMessages.ChangePassowrd.Error;
                                    data = Utility.Failed(data);
                                }
                            }
                            else
                            {

                                data = ConstantMessages.ChangePassowrd.Password_Validation;
                                data = Utility.Failed(data);
                            }
                        }
                        else
                        {
                            data = ConstantMessages.ChangePassowrd.PasswordMisMatch;
                            data = Utility.Failed(data);
                        }
                    }
                    else
                    {
                        data = ConstantMessages.ChangePassowrd.PasswordMacth;
                        data = Utility.Failed(data);
                    }
                }
                else
                {
                    data = ConstantMessages.ChangePassowrd.PasswordEmpty;
                    data = Utility.Failed(data);
                }

            }
            else
            {
                data = Utility.AuthenticationError();
                data = Utility.Failed(data);
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

        [HttpPost]
        [Route("API/User/GetUserProfileDetails")]
        public IHttpActionResult GetUserProfileDetails()//JObject requestParams
        {
            var data = string.Empty;
            UserBO _userdetail = new UserBO();            
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                var UserDetails = UserDAL.GetUserDetailsByUserID(identity.UserID, "");
                if (UserDetails != null)
                {
                    _userdetail = new UserBO();
                    _userdetail.CompId = UserDetails.CompId;
                    _userdetail.Role = UserDetails.Role;
                    _userdetail.FirstName = UserDetails.FirstName;
                    _userdetail.LastName = UserDetails.LastName;
                    _userdetail.EmailID = UserDetails.EmailID;
                    _userdetail.MobileNum = UserDetails.MobileNum;
                    _userdetail.Position = UserDetails.Position;
                    _userdetail.EmailNotification = UserDetails.EmailNotification;
                    _userdetail.PushNotification = UserDetails.PushNotification;

                    _userdetail.ProfilePicFile = UserDetails.ProfilePicFile;// here will be base64 image format

                    data = Utility.ConvertJsonToString(_userdetail);
                    data = Utility.Successful(data);
                }
                else
                {
                    data = Utility.Failed(data);
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(data, Request);
        }
        [HttpPost]
        [Route("API/User/UpdateUserProfileDetails")]
        public IHttpActionResult UpdateUserProfileDetails(JObject requestParams)
        {
            var data = string.Empty;
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                UserBO _userdetail = new UserBO();
                _userdetail.UserID = identity.UserID;
                _userdetail.EmailID = (string)requestParams.SelectToken("EmailID");
                _userdetail.Position = (string)requestParams.SelectToken("Position");                
                _userdetail.EmailNotification = (bool)requestParams.SelectToken("EmailNotification");
                _userdetail.PushNotification = (bool)requestParams.SelectToken("PushNotification");

                _userdetail.ProfilePicFile = (string)requestParams.SelectToken("ProfilePicFileID");
                _userdetail.ProfilePicFileID = "";//This will be available after base64 conversion

                var ResponseBase = UserDAL.UpdateUserDetailsByUserID(_userdetail, "");
                data = Utility.ConvertJsonToString(ResponseBase);

                if (ResponseBase.ReturnCode == "1")
                {
                    HttpContext.Current.Session["IsFirstLogin"] = false;
                    data = Utility.Successful(data);
                }
                else
                {                    
                    data = Utility.Failed(data);    
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(data, Request);
        }
    }
}
