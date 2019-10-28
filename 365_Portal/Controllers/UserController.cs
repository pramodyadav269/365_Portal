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
        public IHttpActionResult LoginUser(JObject requestParams)
        {
            LoginResponse objResponse = null;
            string data = string.Empty;
            try
            {
                string EmailId = Convert.ToString(requestParams["UserName"]).Trim();
                string UserPwd = Convert.ToString(requestParams["Password"]);

                if (string.IsNullOrEmpty(EmailId) || string.IsNullOrEmpty(UserPwd))
                {
                    data = Utility.API_Status("0", ConstantMessages.Login.InvalidUser);
                }
                else
                {
                    LoginRequest objRequest = new LoginRequest();
                    objRequest.UserName = EmailId;
                    objRequest.Password = UserPwd;

                    objResponse = new LoginResponse();
                    objResponse = UserDAL.LoginUser(objRequest);

                    // Success
                    if (objResponse.ReturnCode == "1")
                    {
                        GetAccessToken(EmailId, UserPwd);

                        if (HttpContext.Current.Session["access_token"] != null)
                        {
                            objResponse.Token = HttpContext.Current.Session["access_token"].ToString();
                            data = JsonConvert.SerializeObject(objResponse, Formatting.Indented);
                            data = Utility.Successful(data);
                        }
                        else
                        {
                            data = Utility.API_Status("0", ConstantMessages.WebServiceLog.GenericErrorMsg);
                        }
                    }
                    else
                    {
                        // Failed
                        data = Utility.API_Status("0", objResponse.ReturnMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Error
                data = Utility.API_Status("0", "There might be some error. Please try again later.");
            }
            finally
            {

            }
            return new APIResult(Request, data);
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
                        var UserBO = UserDAL.GetAuthenticatedUserDetails(identity.UserID, "");
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
                                            HttpContext.Current.Session["IsFirstPasswordChanged"] = false;
                                            data = Utility.ConvertDataSetToJSONString(dt);
                                            data = Utility.Successful(data);
                                        }
                                        else
                                        {

                                            data = ConstantMessages.ChangePassowrd.Error;
                                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                                        }

                                    }
                                    else
                                    {
                                        data = ConstantMessages.ChangePassowrd.Error;
                                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    data = ConstantMessages.ChangePassowrd.Error;
                                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                                }
                            }
                            else
                            {

                                data = ConstantMessages.ChangePassowrd.Password_Validation;
                                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                            }
                        }
                        else
                        {
                            data = ConstantMessages.ChangePassowrd.PasswordMisMatch;
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                    }
                    else
                    {
                        data = ConstantMessages.ChangePassowrd.PasswordMacth;
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }
                }
                else
                {
                    data = ConstantMessages.ChangePassowrd.PasswordEmpty;
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }

            }
            else
            {
                data = Utility.AuthenticationError();
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }
            return new APIResult(Request, data);
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
            return new APIResult(Request, data);
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
            return new APIResult(Request, data);
        }

        [HttpPost]
        [Route("API/User/GetMyProfile")]
        public IHttpActionResult GetMyProfile()//JObject requestParams
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
                    _userdetail.ProfilePicFileID = UserDetails.ProfilePicFileID;
                    _userdetail.CompanyProfilePicFileID = UserDetails.CompanyProfilePicFileID;

                    if (!string.IsNullOrEmpty(_userdetail.ProfilePicFileID))
                    {
                        _userdetail.ProfilePicFile = Utility.GetBase64ImageByFileID(_userdetail.ProfilePicFileID, "~/Files/ProfilePic/");
                    }
                    if (!string.IsNullOrEmpty(_userdetail.CompanyProfilePicFileID))
                    {
                        _userdetail.CompanyProfilePicFile = Utility.GetBase64ImageByFileID(_userdetail.CompanyProfilePicFileID, "~/Files/CompLogo/");
                    }

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
            return new APIResult(Request, data);
        }
        [HttpPost]
        [Route("API/User/UpdateMyProfile")]
        public IHttpActionResult UpdateMyProfile(JObject requestParams)
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

                try
                {
                    if (requestParams.SelectToken("UserProfileImageBase64") != null && Convert.ToString(requestParams.SelectToken("UserProfileImageBase64")).Split(',').Length > 1)
                    {
                        _userdetail.ProfilePicFile = Convert.ToString(requestParams.SelectToken("UserProfileImageBase64")).Split(',')[1];

                        byte[] bytes = Convert.FromBase64String(_userdetail.ProfilePicFile);
                        System.Drawing.Image image;
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                        {
                            image = System.Drawing.Image.FromStream(ms);
                        }

                        string GUID = Guid.NewGuid().ToString();
                        string extension = "." + Convert.ToString(requestParams.SelectToken("UserProfileImageBase64")).Split(';')[0].Split('/')[1];
                        string FileName = _userdetail.UserID + "_" + GUID + extension;
                        string FullPath = HttpContext.Current.Server.MapPath("~/Files/ProfilePic/" + FileName);
                        image.Save(FullPath, System.Drawing.Imaging.ImageFormat.Png);
                        
                        DataSet ds = UserBL.CreateFile(FileName,HttpContext.Current.Server.MapPath("~/Files/ProfilePic/"), "");
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            _userdetail.ProfilePicFileID = ds.Tables[0].Rows[0]["UniqueID"].ToString();
                        }
                    }
                    if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                    {
                        if (requestParams.SelectToken("CompanyProfileImageBase64") != null && Convert.ToString(requestParams.SelectToken("CompanyProfileImageBase64")).Split(',').Length > 1)
                        {
                            _userdetail.CompanyProfilePicFile = Convert.ToString(requestParams.SelectToken("CompanyProfileImageBase64")).Split(',')[1];

                            byte[] bytes = Convert.FromBase64String(_userdetail.CompanyProfilePicFile);
                            System.Drawing.Image image;
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                            {
                                image = System.Drawing.Image.FromStream(ms);
                            }

                            string GUID = Guid.NewGuid().ToString();
                            string extension = "." + Convert.ToString(requestParams.SelectToken("CompanyProfileImageBase64")).Split(';')[0].Split('/')[1];
                            string FileName = _userdetail.UserID + "_" + GUID + extension;
                            string FullPath = HttpContext.Current.Server.MapPath("~/Files/CompLogo/" + FileName);
                            image.Save(FullPath, System.Drawing.Imaging.ImageFormat.Png);

                            DataSet ds = UserBL.CreateFile(FileName, HttpContext.Current.Server.MapPath("~/Files/CompLogo/"), "");
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                _userdetail.CompanyProfilePicFileID = ds.Tables[0].Rows[0]["UniqueID"].ToString();
                            }
                        }
                        _userdetail.ThemeColor = (string)requestParams.SelectToken("CompanyThemeColor");
                    }
                }
                catch (Exception ex)
                {
                    ResponseBase objResponse = new ResponseBase();
                    objResponse.ReturnCode = ConstantMessages.WebServiceLog.GenericErrorCode;
                    objResponse.ReturnMessage = ConstantMessages.WebServiceLog.GenericErrorMsg;
                    data = Utility.ConvertJsonToString(objResponse);
                    data = Utility.Failed(data);
                    return new APIResult(Request, data);
                }

                var ResponseBase = UserDAL.UpdateUserDetailsByUserID(_userdetail, "");
                ResponseBase.Ref1 = _userdetail.ProfilePicFile;
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
            return new APIResult(Request, data);
        }

        /// <summary>
        /// Forgot/Reset password Api
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns>
        /// Result as True or false 
        /// </returns>
        [HttpPost]
        [Route("API/User/ForgotPassword")]
        public IHttpActionResult ForgotPassword(JObject requestParams)
        {
            var data = string.Empty;
            //UserBO _userdetail = new UserBO();
            string EmailId = requestParams["EmailId"].ToString();
            Regex regex = new Regex(@"^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            Match mtch = regex.Match(EmailId);
            if (!string.IsNullOrEmpty(EmailId))
            {
                if (mtch.Success)
                {
                    var identity = UserDAL.GetUserDetailsByEmailID(EmailId, string.Empty);
                    if (identity != null)//User Entered EamilId(User) is present in system or not
                    {
                        if (identity.IsDeleted.ToString() == ConstantMessages.ForgotPassowrd.InActiveUserCode)//User is active or Inactive
                        {
                            data = ConstantMessages.ForgotPassowrd.InActiveUser;
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                        else
                        {
                            data = "";
                        }
                    }
                    else
                    {
                        data = ConstantMessages.ForgotPassowrd.InValidUser;
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }
                }
                else
                {
                    data = ConstantMessages.ForgotPassowrd.EmailMisMatch;
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            else
            {
                data = ConstantMessages.ForgotPassowrd.EmailIdEmpty;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }
            return new APIResult(Request, data);
        }
    }
}
