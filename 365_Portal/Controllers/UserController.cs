﻿using _365_Portal.Code.DAL;
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
using _365_Portal.Code.BO;
using static _365_Portal.Code.BO.UserProfile;
using System.IO;

namespace _365_Portal.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/User/LoginUser")]
        public IHttpActionResult LoginUser(JObject requestParams)
        {
            WebServiceLog objServiceLog = new WebServiceLog();
            objServiceLog.RequestTime = DateTime.Now;
            objServiceLog.ControllerName = this.GetType().Name;
            objServiceLog.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            objServiceLog.RequestString = JSONHelper.ConvertJsonToString(requestParams);

            LoginResponse objResponse = null;
            string data = string.Empty;
            try
            {
                string EmailId = Convert.ToString(requestParams["UserName"]).Trim();
                string UserPwd = Convert.ToString(requestParams["Password"]);

                if (string.IsNullOrEmpty(EmailId) || string.IsNullOrEmpty(UserPwd))
                {
                    data = Utility.API_Status("0", ConstantMessages.Login.InvalidUser);
                    objServiceLog.RequestType = ConstantMessages.WebServiceLog.Validation;
                }
                else
                {
                    LoginRequest objRequest = new LoginRequest();
                    objRequest.UserName = EmailId;
                    objRequest.Password = UserPwd;

                    objResponse = new LoginResponse();

                    UserBO objUser = new UserBO();
                    objUser = UserDAL.LoginUser(objRequest);

                    objResponse.ReturnCode = objUser.ReturnCode;
                    objResponse.ReturnMessage = objUser.ReturnMessage;
                    objResponse.Role = objUser.Role;
                    objResponse.EmailID = objUser.EmailID;
                    objResponse.FirstName = objUser.FirstName;
                    objResponse.LastName = objUser.LastName;
                    objResponse.IsFirstLogin = objUser.IsFirstLogin;
                    objResponse.ProfilePicFileID = objUser.ProfilePicFileID;

                    objResponse.IsFirstPasswordNotChanged = objUser.IsFirstPasswordNotChanged;

                    // Success
                    if (objResponse.ReturnCode == "1")
                    {
                        GetAccessToken(EmailId, UserPwd);

                        if (HttpContext.Current.Session["access_token"] != null)
                        {
                            objResponse.Token = HttpContext.Current.Session["access_token"].ToString();
                            data = JsonConvert.SerializeObject(objResponse, Formatting.Indented);
                            data = Utility.Successful(data);
                            objServiceLog.RequestType = ConstantMessages.WebServiceLog.Success;
                        }
                        else
                        {
                            data = Utility.API_Status("0", ConstantMessages.WebServiceLog.GenericErrorMsg);
                            objServiceLog.RequestType = ConstantMessages.WebServiceLog.Validation;
                        }
                    }
                    else
                    {
                        // Failed
                        data = Utility.API_Status("0", objResponse.ReturnMessage);
                        objServiceLog.RequestType = ConstantMessages.WebServiceLog.Validation;
                    }
                    objServiceLog.ResponseString = data;
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
            var data = string.Empty;
            WebServiceLog objServiceLog = new WebServiceLog();
            ResponseBase objResponse = null;
            //string Response = string.Empty;

            objServiceLog.RequestTime = DateTime.Now;
            objServiceLog.ControllerName = this.GetType().Name;
            objServiceLog.MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                LoginRequest objRequest = new LoginRequest();
                objResponse = new ResponseBase();
                if (identity != null)
                {
                    objRequest.UserID = identity.UserID;  //Here UserName Varaible is used as UserID
                    Utility.DestroyAllSession();

                    DataSet ds = UserDAL.UserLogout(identity.CompId, identity.UserID, Utility.GetClientIPaddress());
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ReturnCode"].ToString() == "1")
                    {
                        objResponse.ReturnCode = "1";
                        objResponse.ReturnMessage = "User logout succesfully.";
                        data = Utility.ConvertJsonToString(objResponse);
                        data = Utility.Successful(data);
                    }
                    else
                    {
                        objResponse.ReturnCode = "2";
                        objResponse.ReturnMessage = "Unable to logout.";

                        data = Utility.ConvertJsonToString(objResponse);
                        data = Utility.Failed(data);
                    }
                    //Response = JsonConvert.SerializeObject(objResponse, Formatting.Indented);
                    objServiceLog.RequestString = JSONHelper.ConvertJsonToString(objRequest);
                    objServiceLog.ResponseString = data;
                    objServiceLog.RequestType = ConstantMessages.WebServiceLog.Success;
                }
                else
                {
                    objServiceLog.ResponseString = data;
                    objServiceLog.RequestType = ConstantMessages.WebServiceLog.Validation;
                    data = Utility.AuthenticationError();
                }
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
            return new APIResult(Request, data);
        }

        //Get request api to autheticate user
        //[HttpGet]
        //[Route("api/User/GetForAuthenticate")]
        //public IHttpActionResult GetForAuthenticate()
        //{
        //    //var identity = (ClaimsIdentity)User.Identity;
        //    var identity = MyAuthorizationServerProvider.AuthenticateUser();
        //    if (identity != null)
        //    {
        //        var twest = identity.UserID;
        //    }
        //    return Ok("Hello " + identity.FirstName);
        //}

        /// <summary>
        /// Change password Api
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns>
        /// Result as True or false 
        /// </returns>
        [HttpPost]
        [Route("API/User/ChangePassword")]//With Old Password
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
                                            //HttpContext.Current.Session["IsFirstPasswordNotChanged"] = false;
                                            Utility.CreateFirstPasswordNotChangedSession(false);
                                            data = Utility.ConvertDataSetToJSONString(dt);
                                            data = Utility.Successful(data);

                                            EmailHelper.GetEmailContent(Convert.ToInt32(identity.UserID), identity.CompId, EmailHelper.Functionality.CHANGE_PASS, "", "");
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

        [HttpPost]
        [Route("API/User/GetMyProfile")]//JObject requestParams
        public IHttpActionResult GetMyProfile()
        {
            var data = string.Empty;
            UserProfileResponse objUserProfile = new UserProfileResponse();

            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                var UserDetails = UserDAL.GetUserDetailsByUserID(identity.UserID, "");
                if (UserDetails != null)
                {
                    objUserProfile.Role = UserDetails.Role;
                    objUserProfile.FirstName = UserDetails.FirstName;
                    objUserProfile.LastName = UserDetails.LastName;
                    objUserProfile.EmailID = UserDetails.EmailID;
                    objUserProfile.MobileNum = UserDetails.MobileNum;
                    objUserProfile.Position = UserDetails.Position;
                    objUserProfile.EmailNotification = UserDetails.EmailNotification;
                    objUserProfile.PushNotification = UserDetails.PushNotification;
                    objUserProfile.ThemeColor = UserDetails.ThemeColor;
                    objUserProfile.ThemeColor2 = UserDetails.ThemeColor2;
                    objUserProfile.ThemeColor3 = UserDetails.ThemeColor3;
                    objUserProfile.ThemeColor4 = UserDetails.ThemeColor4;
                    objUserProfile.GroupName = UserDetails.GroupName;
                    objUserProfile.OrganizationName = UserDetails.OrganizationName;
                    if (!string.IsNullOrEmpty(UserDetails.ProfilePicFileID))
                    {
                        //objUserProfile.ProfilePicFile = HttpContext.Current.Server.MapPath("~/Files/ProfilePic/") + UserDetails.ProfilePicFileID;
                        objUserProfile.ProfilePicFile = UserDetails.ProfilePicFileID;
                    }
                    if (!string.IsNullOrEmpty(UserDetails.CompanyProfilePicFileID))
                    {
                        //objUserProfile.CompanyProfilePicFile = HttpContext.Current.Server.MapPath("~/Files/CompLogo/") + UserDetails.CompanyProfilePicFileID;
                        objUserProfile.CompanyProfilePicFile = UserDetails.CompanyProfilePicFileID;
                    }

                    data = Utility.ConvertJsonToString(objUserProfile);
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

                try
                {
                    string userProfilePicBase64 = Convert.ToString(requestParams.SelectToken("UserProfileImageBase64"));

                    if (!string.IsNullOrEmpty(userProfilePicBase64))
                    {
                        var files = userProfilePicBase64.Split(new string[] { "," }, StringSplitOptions.None);
                        if (files.Count() == 1)
                            userProfilePicBase64 = files[0];
                        else
                            userProfilePicBase64 = files[1];
                        byte[] imageBytes = Convert.FromBase64String(userProfilePicBase64);

                        //To get image size
                        //using (var ms = new MemoryStream(imageBytes))
                        //{
                        //    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);                            
                        //    return new Tuple<int, int>(img.Width, img.Height); // or some other data container
                        //}
                        //End

                        string fileName = _userdetail.UserID + "_" + Guid.NewGuid() + "." + Utility.GetFileExtension(userProfilePicBase64);
                        string filePath = HttpContext.Current.Server.MapPath("~/Files/ProfilePic/" + fileName);
                        File.WriteAllBytes(filePath, imageBytes);
                        HttpContext.Current.Session["ProfilePicFile"] = fileName;

                        DataSet ds = UserBL.CreateFile(fileName, HttpContext.Current.Server.MapPath("~/Files/ProfilePic/"), false, "ProfilePic");
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            _userdetail.ProfilePicFileID = ds.Tables[0].Rows[0]["UniqueID"].ToString();
                        }
                    }
                    if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                    {
                        string companyLogoBase64 = Convert.ToString(requestParams.SelectToken("CompanyProfileImageBase64"));
                        if (!string.IsNullOrEmpty(companyLogoBase64))
                        {
                            var files = companyLogoBase64.Split(new string[] { "," }, StringSplitOptions.None);
                            if (files.Count() == 1)
                                companyLogoBase64 = files[0];
                            else
                                companyLogoBase64 = files[1];
                            byte[] imageBytes = Convert.FromBase64String(companyLogoBase64);
                            string fileName = _userdetail.UserID + "_" + Guid.NewGuid() + "." + Utility.GetFileExtension(companyLogoBase64);
                            string filePath = HttpContext.Current.Server.MapPath("~/Files/CompLogo/" + fileName);
                            File.WriteAllBytes(filePath, imageBytes);
                            HttpContext.Current.Session["CompanyProfilePicFile"] = fileName;
                            DataSet ds = UserBL.CreateFile(fileName, HttpContext.Current.Server.MapPath("~/Files/CompLogo/"), false, "");
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                _userdetail.CompanyProfilePicFileID = ds.Tables[0].Rows[0]["UniqueID"].ToString();
                            }
                        }
                        _userdetail.ThemeColor = (string)requestParams.SelectToken("CompanyThemeColor");
                        _userdetail.ThemeColor2 = (string)requestParams.SelectToken("CompanyThemeColor2");
                        _userdetail.ThemeColor3 = (string)requestParams.SelectToken("CompanyThemeColor3");
                        _userdetail.ThemeColor4 = (string)requestParams.SelectToken("CompanyThemeColor4");

                        HttpContext.Current.Session["ThemeColor"] = _userdetail.ThemeColor;
                        HttpContext.Current.Session["ThemeColor2"] = _userdetail.ThemeColor2;
                        HttpContext.Current.Session["ThemeColor3"] = _userdetail.ThemeColor3;
                        HttpContext.Current.Session["ThemeColor4"] = _userdetail.ThemeColor4;
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
                //ResponseBase.Ref1 = _userdetail.ProfilePicFile;
                data = Utility.ConvertJsonToString(ResponseBase);

                if (Convert.ToString(requestParams.SelectToken("EmailNotification")) != "")
                {
                    _userdetail.EmailNotification = (bool)requestParams.SelectToken("EmailNotification");
                    _userdetail.PushNotification = (bool)requestParams.SelectToken("PushNotification");

                    UserDAL.UpdateNotificationByUserID(_userdetail, "");
                }

                if (ResponseBase.ReturnCode == "1")
                {
                    //HttpContext.Current.Session["IsFirstLogin"] = false;
                    Utility.CreateFirstLoginSession(false);
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
        [Route("API/User/UpdateNotification")]
        public IHttpActionResult UpdateNotification(JObject requestParams)
        {
            var data = string.Empty;
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                UserBO _userdetail = new UserBO();
                _userdetail.UserID = identity.UserID;
                _userdetail.EmailNotification = (bool)requestParams.SelectToken("EmailNotification");
                _userdetail.PushNotification = (bool)requestParams.SelectToken("PushNotification");

                var ResponseBase = UserDAL.UpdateNotificationByUserID(_userdetail, "");
                if (ResponseBase.ReturnCode == "1")
                {
                    data = Utility.ConvertJsonToString(ResponseBase);
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
            int CompId = 0;
            int Action;
            string UserId = string.Empty;
            string Type = string.Empty;
            string IP_Address = string.Empty;
            /*Both values are taken as input whatever the logic needs to impemented*/
            string EmailId = requestParams["EmailId"].ToString();
            string MobileNum = requestParams["MobileNum"].ToString();
            Action = Convert.ToInt32(requestParams["Action"]);
            /*This fields are for the mobile Request*/
            string DeviceDetails = requestParams["DeviceDetails"].ToString();
            string DeviceType = requestParams["DeviceType"].ToString();
            Regex regex_Email = new Regex(@"^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            Regex regex_Mobile_Num = new Regex(@"^[0-9]{10}$");
            Match mtch_Email = regex_Email.Match(EmailId);
            Match mtch_Mobile = regex_Mobile_Num.Match(MobileNum);
            try
            {
                if (!string.IsNullOrEmpty(EmailId) || !string.IsNullOrEmpty(MobileNum))
                {
                    if (mtch_Email.Success || mtch_Mobile.Success)
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
                                if (EmailId.Trim().ToUpper() == identity.EmailID.Trim().ToUpper())
                                {
                                    try
                                    {
                                        CompId = identity.CompId;
                                        UserId = identity.UserID;
                                        IP_Address = Utility.GetClientIPaddress();
                                        if ((HttpContext.Current.Request.Browser.IsMobileDevice == true || HttpContext.Current.Request.Browser.IsMobileDevice == false) && string.IsNullOrEmpty(DeviceDetails) && string.IsNullOrEmpty(DeviceType))
                                        {
                                            DeviceDetails = Utility.GetDeviceDetails(ConstantMessages.DeviceInfo.InfoType.Trim().ToLower());
                                            DeviceType = Utility.GetDeviceDetails(ConstantMessages.DeviceInfo.DeviceTypeBrowser.Trim().ToLower());
                                        }
                                        if (Action == Convert.ToInt32(ConstantMessages.StatusCode.Failure))
                                        {
                                            Type = ConstantMessages.ForgotPassowrd.Type_0;
                                        }
                                        else
                                        {
                                            Type = ConstantMessages.ForgotPassowrd.Type_1;
                                        }
                                        var ds = UserBL.ResetPassword(Action, CompId, UserId, MobileNum, EmailId, Type, DeviceDetails, DeviceType, IP_Address);
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
                                        data = ConstantMessages.ForgotPassowrd.Error;
                                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                                    }
                                }
                                else
                                {
                                    data = ConstantMessages.ForgotPassowrd.InvalidEmailId;
                                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                                }
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
                        if (!string.IsNullOrEmpty(EmailId))
                        {
                            data = ConstantMessages.ForgotPassowrd.EmailMisMatch;
                        }
                        else
                        {
                            data = ConstantMessages.ForgotPassowrd.InvalidMobile;
                        }
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(EmailId))
                    {
                        data = ConstantMessages.ForgotPassowrd.EmailIdEmpty;
                    }
                    else
                    {
                        data = ConstantMessages.ForgotPassowrd.Mobile_NumEmpty;
                    }
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }
            return new APIResult(Request, data);
        }
        /// <summary>
        /// Password Changed API which will Change the PAssword
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/User/ResetPassword")]//Before Login
        public IHttpActionResult ResetPassword(JObject requestParams)
        {
            var data = string.Empty;
            try
            {

                string Password = requestParams["Password"].ToString();
                string Token = requestParams["Token"].ToString();
                /*This fields are for the mobile Request*/
                string DeviceDetails = requestParams["DeviceDetails"].ToString();
                string DeviceType = requestParams["DeviceType"].ToString();
                if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Token))
                {
                    var User_details = UserDAL.GetUserDetailsByToken(Token, string.Empty);

                    if (Token.ToUpper().ToString() == User_details.Token.ToUpper().ToString())
                    {
                        var _ds = CommonDAL.UserResetPassword((int)4, User_details.CompId, User_details.UserID, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, User_details.Token);
                        if (_ds.Tables.Count > 0)
                        {
                            DataTable _dt = _ds.Tables["Data"];
                            if (_dt.Rows[0]["ReturnCode"].ToString() == "1")
                            {
                                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");// Password Validation regex with atleast 1 lowercase,1 Uppercase,1 numeric,1 special charcter and 8 Charcters long  
                                Match m = regex.Match(Password);
                                if (m.Success)
                                {
                                    UserBO _details = new UserBO();
                                    _details.CompId = User_details.CompId;
                                    _details.UserID = User_details.UserID;
                                    _details.Token = Token;
                                    _details.NewPassword = Password;
                                    _details.IP_Address = Utility.GetClientIPaddress();
                                    if ((HttpContext.Current.Request.Browser.IsMobileDevice == true || HttpContext.Current.Request.Browser.IsMobileDevice == false) && string.IsNullOrEmpty(DeviceDetails) && string.IsNullOrEmpty(DeviceType) && string.IsNullOrEmpty(DeviceType))
                                    {
                                        _details.DeviceDetails = Utility.GetDeviceDetails(ConstantMessages.DeviceInfo.InfoType.Trim().ToLower());
                                        _details.DeviceType = Utility.GetDeviceDetails(ConstantMessages.DeviceInfo.DeviceTypeBrowser.Trim().ToLower());
                                    }
                                    var ds = CommonBL.ChangePassword(_details);
                                    if (ds.Tables.Count > 0)
                                    {
                                        DataTable dt = ds.Tables["Data"];
                                        if (dt.Rows[0]["ReturnCode"].ToString() == "1")
                                        {
                                            data = Utility.ConvertDataSetToJSONString(dt);
                                            data = Utility.Successful(data);

                                            EmailHelper.GetEmailContent(Convert.ToInt32(User_details.UserID), User_details.CompId, EmailHelper.Functionality.CHANGE_PASS, "", "");
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
                                else
                                {
                                    data = ConstantMessages.ChangePassowrd.Password_Validation;
                                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                                }
                            }
                            else
                            {

                                data = Utility.ConvertDataSetToJSONString(_dt);
                                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                            }
                        }
                        else
                        {
                            data = ConstantMessages.ChangePassowrd.Error;
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                    }
                    else
                    {
                        data = ConstantMessages.ForgotPassowrd.InvalidToken;
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }

                }
                else
                {
                    data = ConstantMessages.ForgotPassowrd.PasswordTokenEmpty;
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }

            return new APIResult(Request, data);
        }
        /// <summary>
        /// Token Verification 
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/User/TokenVerification")]
        public IHttpActionResult TokenVerification(JObject requestParams)
        {
            var data = string.Empty;
            try
            {
                string Token = requestParams["Token"].ToString();
                if (!string.IsNullOrEmpty(Token))
                {

                    var User_details = UserDAL.GetUserDetailsByToken(Token, string.Empty);
                    if (User_details != null)
                    {
                        var ds = CommonDAL.UserResetPassword((int)4, User_details.CompId, User_details.UserID, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, User_details.Token);
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
                                data = Utility.ConvertDataSetToJSONString(dt);
                                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                            }
                        }
                        else
                        {
                            data = "Dear customer this link has expired.";
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                    }
                    else
                    {
                        data = "Dear customer you have entered invalid URL";
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);

                    }
                }
                else
                {
                    data = ConstantMessages.ForgotPassowrd.InvalidToken;
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }

            return new APIResult(Request, data);
        }


        #region CRUD FOR GROUPS
        [HttpPost]
        [Route("API/User/CreateGroups")]
        public IHttpActionResult CreateGroups(JObject requestParams)
        {
            var data = string.Empty;
            string GroupName = string.Empty;
            int CompID;
            string CreatedBy = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {
                    if (!string.IsNullOrEmpty(requestParams["GroupName"].ToString()))
                    {
                        CompID = identity.CompId;
                        CreatedBy = identity.UserID;


                        if (!string.IsNullOrEmpty(requestParams["GroupName"].ToString()))
                        {
                            GroupName = requestParams["GroupName"].ToString();
                        }
                        else
                        {
                            GroupName = string.Empty;
                        }
                        var ds = UserBL.CreateGroup(CompID, GroupName, string.Empty, CreatedBy);
                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
                                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                                }

                            }
                            else
                            {
                                data = ConstantMessages.WebServiceLog.GenericErrorMsg;
                                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                            }
                        }
                        else
                        {
                            data = ConstantMessages.WebServiceLog.GenericErrorMsg;
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                    }
                    else
                    {
                        data = ConstantMessages.WebServiceLog.InValidValues;
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }

                }
                else
                {
                    data = Utility.AuthenticationError();
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }
            return new APIResult(Request, data);

        }

        [HttpPost]
        [Route("API/User/ModifyGroups")]
        public IHttpActionResult ModifyGroups(JObject requestParams)
        {
            var data = string.Empty;
            int GroupId = 0;
            string GroupName = string.Empty;
            int CompID;
            string CreatedBy = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (((Convert.ToInt32(requestParams["GroupID"]) != 0 && !string.IsNullOrEmpty(requestParams["GroupID"].ToString())) &&
                        !string.IsNullOrEmpty(requestParams["GroupName"].ToString())))
                    {
                        CompID = identity.CompId;
                        CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["GroupID"].ToString()))
                        {
                            GroupId = Convert.ToInt32(requestParams["GroupID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["GroupName"].ToString()))
                        {
                            GroupName = requestParams["GroupName"].ToString();
                        }
                        else
                        {
                            content.TopicTitle = null;
                        }
                        var ds = UserBL.ModifyGroup(CompID, GroupName, GroupId, string.Empty, CreatedBy);

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

                                data = dt.Rows[0]["ReturnMessage"].ToString();
                                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                            }

                        }
                        else
                        {
                            data = ConstantMessages.WebServiceLog.GenericErrorMsg;
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                    }
                    else
                    {
                        data = ConstantMessages.WebServiceLog.InValidValues;
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }

                }
                else
                {
                    data = Utility.AuthenticationError();
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }
            return new APIResult(Request, data);


        }

        [HttpPost]
        [Route("API/User/GetGroups")]
        public IHttpActionResult GetGroups(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {



                    var ds = UserBL.ViewGroup(identity.CompId);
                    DataTable dt = ds.Tables["Data"];
                    if (dt != null)
                    {
                        if (ds.Tables.Count > 0)
                        {


                            data = Utility.ConvertDataSetToJSONString(dt);
                            data = Utility.Successful(data);


                        }
                        else
                        {
                            data = dt.Rows[0]["ReturnMessage"].ToString();
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                    }
                    else
                    {
                        data = "Please Try Again";
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }
                }
                else
                {
                    data = Utility.AuthenticationError();
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }
            return new APIResult(Request, data);

        }

        [HttpPost]
        [Route("API/User/DeleteGroups")]
        public IHttpActionResult DeleteGroups(JObject requestParams)
        {
            var data = string.Empty;
            int GroupId = 0;
            string GroupName = string.Empty;
            int CompID;

            string CreatedBy = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if ((Convert.ToInt32(requestParams["GroupID"]) != 0 && !string.IsNullOrEmpty(requestParams["GroupID"].ToString())) && !string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                    {
                        bool IsActive = false;
                        CompID = identity.CompId;
                        CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["GroupID"].ToString()))
                        {
                            GroupId = Convert.ToInt32(requestParams["GroupID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            IsActive = (bool)requestParams["IsActive"];
                        }
                        var ds = UserBL.DeleteGroup(CompID, GroupId, IsActive, CreatedBy);
                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
                                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                                }

                            }
                            else
                            {
                                data = ConstantMessages.WebServiceLog.GenericErrorMsg;
                                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                            }
                        }
                        else
                        {
                            data = ConstantMessages.WebServiceLog.GenericErrorMsg;
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                    }
                    else
                    {
                        data = ConstantMessages.WebServiceLog.InValidValues;
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }

                }
                else
                {
                    data = Utility.AuthenticationError();
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }
            return new APIResult(Request, data);
        }
        #endregion


        [Route("API/User/GetUsers")]
        [HttpPost]
        public IHttpActionResult GetUsers(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;
                    if (objUser.Role == "superadmin")
                    {
                        objUser.CompId = Convert.ToInt32(requestParams["CompId"].ToString());
                    }

                    var ds = CommonBL.GetUsers(objUser);
                    if (ds.Tables.Count > 0)
                    {
                        data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                        data = Utility.Successful(data);
                    }
                    else
                    {
                        data = Utility.API_Status("2", "No user found");
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("API/User/BindRoleAndGroup")]
        [HttpPost]
        public IHttpActionResult BindRoleAndGroup()
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;

                    var ds = CommonBL.BindRoleAndGroup(objUser);

                    data = Utility.ConvertDataSetToJSONString(ds);
                    data = Utility.Successful(data);
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        public static bool IsValidEmail(string eMail)
        {
            string strRegex = @"^([0-9a-zA-Z]([\+\-_\.]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(eMail))
                return true;
            else
                return false;
        }


        [Route("API/User/CreateUser")]
        [HttpPost]
        public IHttpActionResult CreateUser(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    if (ValidateUserDetails(jsonResult, out Message, out objUser, "create"))
                    {
                        objUser.UserID = identity.UserID;
                        objUser.CompId = identity.CompId;
                        objUser.Role = identity.Role;

                        var ds = CommonBL.CreateUpdateUser(objUser, 1, 0);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ReturnCode"].ToString() == "1")
                        {
                            data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                            data = Utility.Successful(data);

                            EmailHelper.GetEmailContent(Convert.ToInt32(ds.Tables[0].Rows[0]["InsertedID"]), identity.CompId, EmailHelper.Functionality.CREATE_USER, "", "");//
                        }
                        else if (ds.Tables[0].Rows.Count > 0)
                        {
                            data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                            data = Utility.Failed(data);
                        }
                        else
                        {
                            data = Utility.API_Status("0", "No data found");
                        }
                    }
                    else
                    {
                        data = Utility.API_Status("2", Message);
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("API/User/UpdateUser")]
        [HttpPost]
        public IHttpActionResult UpdateUser(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    if (ValidateUserDetails(jsonResult, out Message, out objUser, "update"))
                    {
                        objUser.UserID = identity.UserID;
                        objUser.CompId = identity.CompId;
                        objUser.Role = identity.Role;

                        int ChildUserID = 0;
                        if (jsonResult.SelectToken("UserID") != null && jsonResult.SelectToken("UserID").ToString().Trim() != "")
                        {
                            ChildUserID = (int)jsonResult.SelectToken("UserID");

                            var ds = CommonBL.CreateUpdateUser(objUser, 2, ChildUserID);
                            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ReturnCode"].ToString() == "1")
                            {
                                data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                                data = Utility.Successful(data);


                                if (jsonResult.SelectToken("UpdateFlag") != null && jsonResult.SelectToken("UpdateFlag").ToString().Trim() == "1")
                                {
                                    EmailHelper.GetEmailContent(ChildUserID, identity.CompId, EmailHelper.Functionality.CHANGE_PASS_ADMIN, "", "");
                                }
                            }
                            else if (ds.Tables[0].Rows.Count > 0)
                            {
                                data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                                data = Utility.Failed(data);
                            }
                            else
                            {
                                data = Utility.API_Status("0", ConstantMessages.WebServiceLog.GenericErrorMsg);
                            }
                        }
                        else
                        {
                            data = Utility.API_Status("2", "Please provide UserID");
                        }
                    }
                    else
                    {
                        data = Utility.API_Status("2", Message);
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("API/User/DeleteUser")]
        [HttpPost]
        public IHttpActionResult DeleteUser(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                int ChildUserID = 0;
                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;
                    objUser.IsDeleted = false;

                    if (jsonResult.SelectToken("UserID") != null && jsonResult.SelectToken("UserID").ToString().Trim() != "")
                    {
                        ChildUserID = (int)jsonResult.SelectToken("UserID");

                        var ds = CommonBL.DeleteUser(objUser, ChildUserID);
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ReturnCode"].ToString() == "1")
                        {
                            data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                            data = Utility.Successful(data);
                        }
                        else if (ds.Tables[0].Rows.Count > 0)
                        {
                            data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                            data = Utility.Failed(data);
                        }
                        else
                        {
                            data = Utility.API_Status("0", ConstantMessages.WebServiceLog.GenericErrorMsg);
                        }
                    }
                    else
                    {
                        data = Utility.API_Status("2", "Please provide UserID");
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("API/User/GetUserDetailsForParent")]
        [HttpPost]
        public IHttpActionResult GetUserDetailsForParent(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                int ChildUserID = 0;
                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;
                    objUser.IsDeleted = false;

                    if (jsonResult.SelectToken("UserID") != null && jsonResult.SelectToken("UserID").ToString().Trim() != "")
                    {
                        ChildUserID = (int)jsonResult.SelectToken("UserID");

                        var ds = CommonBL.GetUserDetailsForParent(objUser, ChildUserID);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                            data = Utility.Successful(data);
                        }
                        else
                        {
                            data = Utility.API_Status("0", "No data found");
                        }
                    }
                    else
                    {
                        data = Utility.API_Status("2", "Please provide UserID");
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        private bool ValidateUserDetails(JObject jsonResult, out string Message, out UserBO objUserVal, string flag)
        {
            bool ValFlag = true;
            Message = string.Empty;
            objUserVal = new UserBO();

            if (jsonResult.SelectToken("RoleID") != null && jsonResult.SelectToken("RoleID").ToString().Trim() != "")
            {
                objUserVal.RoleID = (string)jsonResult.SelectToken("RoleID");
            }
            else
            {
                Message = "Please provide RoleID of user."; ValFlag = false; return ValFlag;
            }

            if (jsonResult.SelectToken("FirstName") != null && jsonResult.SelectToken("FirstName").ToString().Trim() != "")
            {
                objUserVal.FirstName = (string)jsonResult.SelectToken("FirstName");
            }
            else
            {
                Message = "Please provide First Name of user."; ValFlag = false; return ValFlag;
            }

            if (jsonResult.SelectToken("LastName") != null && jsonResult.SelectToken("LastName").ToString().Trim() != "")
            {
                objUserVal.LastName = (string)jsonResult.SelectToken("LastName");
            }
            else
            {
                Message = "Please provide LastName Name of user."; ValFlag = false; return ValFlag;
            }

            if (jsonResult.SelectToken("EmailID") != null && jsonResult.SelectToken("EmailID").ToString().Trim() != "")
            {
                objUserVal.EmailID = (string)jsonResult.SelectToken("EmailID");

                if (!IsValidEmail(objUserVal.EmailID))
                {
                    Message = "Please provide valid EmailID of user."; ValFlag = false; return ValFlag;
                }
            }
            else
            {
                Message = "Please provide EmailID of user."; ValFlag = false; return ValFlag;
            }

            if (flag == "create")
            {
                if (jsonResult.SelectToken("Password") != null && jsonResult.SelectToken("Password").ToString().Trim() != "")
                {
                    objUserVal.NewPassword = (string)jsonResult.SelectToken("Password");
                    objUserVal.UserKey = Guid.NewGuid().ToString();
                    objUserVal.PasswordSalt = Utility.GetSalt();
                    objUserVal.PasswordHash = Utility.GetHashedPassword(objUserVal.NewPassword, objUserVal.PasswordSalt);
                }
                else
                {
                    Message = "Please provide Password of user."; ValFlag = false; return ValFlag;
                }
            }
            else
            {
                if (jsonResult.SelectToken("UpdateFlag") != null && jsonResult.SelectToken("UpdateFlag").ToString().Trim() == "1")
                {
                    if (jsonResult.SelectToken("Password") != null && jsonResult.SelectToken("Password").ToString().Trim() != "")
                    {
                        objUserVal.NewPassword = (string)jsonResult.SelectToken("Password");
                        objUserVal.IsChangingPassword = "1";
                        objUserVal.UserKey = Guid.NewGuid().ToString();
                        objUserVal.PasswordSalt = Utility.GetSalt();
                        objUserVal.PasswordHash = Utility.GetHashedPassword(objUserVal.NewPassword, objUserVal.PasswordSalt);
                    }
                    else
                    {
                        Message = "Please provide Password of user."; ValFlag = false; return ValFlag;
                    }
                }
                else
                {
                    objUserVal.IsChangingPassword = "0";
                }
            }

            if (jsonResult.SelectToken("MobileNum") != null && jsonResult.SelectToken("MobileNum").ToString().Trim() != "")
            {
                objUserVal.MobileNum = (string)jsonResult.SelectToken("MobileNum");
            }

            if (jsonResult.SelectToken("Position") != null && jsonResult.SelectToken("Position").ToString().Trim() != "")
            {
                objUserVal.Position = (string)jsonResult.SelectToken("Position");
            }

            if (jsonResult.SelectToken("GroupId") != null && jsonResult.SelectToken("GroupId").ToString().Trim() != "")
            {
                objUserVal.GroupId = (string)jsonResult.SelectToken("GroupId");
            }
            if (jsonResult.SelectToken("CompName") != null && jsonResult.SelectToken("CompName").ToString().Trim() != "")
            {
                objUserVal.CompName = (string)jsonResult.SelectToken("CompName");
            }
            if (jsonResult.SelectToken("Gender") != null && jsonResult.SelectToken("Gender").ToString().Trim() != "")
            {
                objUserVal.Gender = (string)jsonResult.SelectToken("Gender");
            }
            //else
            //{
            //    Message = "Please select gender."; ValFlag = false; return ValFlag;
            //}
            if (jsonResult.SelectToken("DepartmentID") != null && jsonResult.SelectToken("DepartmentID").ToString().Trim() != "")
            {
                objUserVal.DepartmentID = (string)jsonResult.SelectToken("DepartmentID");
            }
            //else
            //{
            //    Message = "Please select Department."; ValFlag = false; return ValFlag;
            //}
            if (jsonResult.SelectToken("TeamID") != null && jsonResult.SelectToken("TeamID").ToString().Trim() != "")
            {
                objUserVal.TeamID = (string)jsonResult.SelectToken("TeamID");
            }
            //else
            //{
            //    Message = "Please select Team."; ValFlag = false; return ValFlag;
            //}
            if (jsonResult.SelectToken("ManagerID") != null && jsonResult.SelectToken("ManagerID").ToString().Trim() != "")
            {
                objUserVal.ManagerID = (string)jsonResult.SelectToken("ManagerID");
            }
            //else
            //{
            //    Message = "Please select Manager."; ValFlag = false; return ValFlag;
            //}
            if (jsonResult.SelectToken("DOJ") != null && jsonResult.SelectToken("DOJ").ToString().Trim() != "")
            {                
                try
                {
                    objUserVal.DOJ = DateTime.ParseExact(jsonResult.SelectToken("DOJ").ToString().Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    Message = "Please enter valid Date of Joining."; ValFlag = false; return ValFlag;
                }                
            }
            //else
            //{
            //    Message = "Please enter Date of Joining."; ValFlag = false; return ValFlag;
            //}

            return ValFlag;
        }

        [Route("API/User/GetUsersGroup")]
        [HttpPost]
        public IHttpActionResult GetUsersGroup(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;
                    objUser.IsDeleted = false;

                    var ds = CommonBL.GetUsersGroup(objUser, 4);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                        data = Utility.ConvertDataSetToJSONString(ds);
                        data = Utility.Successful(data);
                    }
                    else
                    {
                        data = Utility.API_Status("0", "No data found");
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("API/User/SearchUsersByGroups")]
        [HttpPost]
        public IHttpActionResult SearchUsersByGroups(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    if (jsonResult.SelectToken("GroupID") != null && jsonResult.SelectToken("GroupID").ToString().Trim() != "")
                    {
                        objUser.GroupId = jsonResult.SelectToken("GroupID").ToString();
                    }

                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;
                    objUser.IsDeleted = false;

                    var ds = CommonBL.GetUsersGroup(objUser, 5);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                        data = Utility.Successful(data);
                    }
                    else
                    {
                        data = Utility.API_Status("0", "No data found");
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }



        [Route("API/User/UpdateUserMapping")]
        [HttpPost]
        public IHttpActionResult UpdateUserMapping(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    if (jsonResult.SelectToken("UserID") != null && jsonResult.SelectToken("UserID").ToString().Trim() != "" &&
                        jsonResult.SelectToken("GroupID") != null && jsonResult.SelectToken("GroupID").ToString().Trim() != "")
                    {
                        objUser.UserID = jsonResult.SelectToken("UserID").ToString();
                        objUser.GroupId = jsonResult.SelectToken("GroupID").ToString();

                        objUser.CompId = identity.CompId;
                        objUser.Role = identity.Role;
                        objUser.IsDeleted = true;

                        var ds = CommonBL.GetUsersGroup(objUser, 1);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                            data = Utility.Successful(data);
                        }
                        else
                        {
                            data = Utility.API_Status("0", "No data found");
                        }
                    }
                    else
                    {
                        data = Utility.API_Status("2", "Please provide UserID and GroupID");
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("API/User/BindDropdown")]
        [HttpPost]
        public IHttpActionResult BindDropdown()
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin || identity.Role == ConstantMessages.Roles.subadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;

                    var ds = CommonBL.BindDropDown(objUser, "createuser");

                    data = Utility.ConvertDataSetToJSONString(ds);
                    data = Utility.Successful(data);
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

    }
}
