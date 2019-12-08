using _365_Portal.Code;
using _365_Portal.Code.BL;
using _365_Portal.Code.DAL;
using _365_Portal.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using static _365_Portal.Code.BO.UserProfile;
//using System.Web.Mvc;

namespace _365_Portal.Controllers
{
    public class OrganizationController : ApiController
    {
        [Route("API/Organization/GetAdminUsers")]
        [HttpPost]
        public IHttpActionResult GetAdminUsers()
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

                    var ds = OrganizationBL.GetAdminUsers(objUser);
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

        [Route("API/Organization/GetCountry")]
        [HttpPost]
        public IHttpActionResult GetCountry(JObject requestParams)
        {
            var data = "";
            var ds = OrganizationBL.GetCountry(0);
            if (ds.Tables.Count > 0)
            {
                data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                data = Utility.Successful(data);
            }
            else
            {
                data = Utility.API_Status("2", "No data found");
            }

            return new APIResult(Request, data);
        }

        [Route("API/Organization/BindRole")]
        [HttpPost]
        public IHttpActionResult BindRole(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                var ds = OrganizationBL.GetRole(identity.Role, identity.RoleID);
                if (ds.Tables.Count > 0)
                {
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    data = Utility.Successful(data);
                }
                else
                {
                    data = Utility.API_Status("2", "No data found");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("API/Organization/CreateAdminUser")]
        [HttpPost]
        public IHttpActionResult CreateAdminUser(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.superadmin)//identity.Role == ConstantMessages.Roles.companyadmin || 
                {
                    if (ValidateUserDetails(identity.CompId, jsonResult, out Message, out objUser, "create", Convert.ToInt32(identity.UserID)))
                    {
                        objUser.UserID = identity.UserID;
                        objUser.CompId = identity.CompId;
                        objUser.Role = identity.Role;

                        var ds = OrganizationBL.CreateUpdateAdminUser(objUser, 1, 0);
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

        private bool ValidateUserDetails(int compid, JObject jsonResult, out string Message, out UserBO objUserVal, string flag, int ChildUserID)
        {
            bool ValFlag = true;
            Message = string.Empty;
            objUserVal = new UserBO();


            if (jsonResult.SelectToken("BusinessName") != null && jsonResult.SelectToken("BusinessName").ToString().Trim() != "")
            {
                objUserVal.CompName = (string)jsonResult.SelectToken("BusinessName");
            }
            else
            {
                Message = "Please provide Company Name."; ValFlag = false; return ValFlag;
            }
            if (jsonResult.SelectToken("EmployeeCount") != null && jsonResult.SelectToken("EmployeeCount").ToString().Trim() != "")
            {
                objUserVal.NoOfEmployees = (string)jsonResult.SelectToken("EmployeeCount");
            }
            else
            {
                Message = "Please provide Employee Count."; ValFlag = false; return ValFlag;
            }
            if (jsonResult.SelectToken("Country") != null && jsonResult.SelectToken("Country").ToString().Trim() != "")
            {
                objUserVal.Country = (string)jsonResult.SelectToken("Country");
            }
            else
            {
                Message = "Please provide Country."; ValFlag = false; return ValFlag;
            }
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
            //if (jsonResult.SelectToken("CompName") != null && jsonResult.SelectToken("CompName").ToString().Trim() != "")//Commented by pramod on 7 DEC 19 at 12:06 AM
            //{
            //    objUserVal.CompName = (string)jsonResult.SelectToken("CompName");
            //}

            if (jsonResult.SelectToken("EmployeeCount") != null && jsonResult.SelectToken("EmployeeCount").ToString().Trim() != "")
            {
                objUserVal.NoOfEmployees = (string)jsonResult.SelectToken("EmployeeCount");
            }
            else
            {
                Message = "Please provide No Of Employee."; ValFlag = false; return ValFlag;
            }
            if (jsonResult.SelectToken("Country") != null && jsonResult.SelectToken("Country").ToString().Trim() != "")
            {
                objUserVal.Country = (string)jsonResult.SelectToken("Country");
            }
            else
            {
                Message = "Please provide Country."; ValFlag = false; return ValFlag;
            }

            string userProfilePicBase64 = Convert.ToString(jsonResult.SelectToken("UserProfileImageBase64"));

            if (!string.IsNullOrEmpty(userProfilePicBase64))
            {
                var files = userProfilePicBase64.Split(new string[] { "," }, StringSplitOptions.None);
                if (files.Count() == 1)
                    userProfilePicBase64 = files[0];
                else
                    userProfilePicBase64 = files[1];

                byte[] imageBytes = Convert.FromBase64String(userProfilePicBase64);
                string fileName = ChildUserID + "_" + Guid.NewGuid() + "." + Utility.GetFileExtension(userProfilePicBase64);
                string filePath = HttpContext.Current.Server.MapPath("~/Files/ProfilePic/" + fileName);
                File.WriteAllBytes(filePath, imageBytes);
                if (HttpContext.Current.Session["CompId"] != null)
                {
                    if (Convert.ToInt32(HttpContext.Current.Session["CompId"]) == compid)
                    {
                        HttpContext.Current.Session["ProfilePicFile"] = fileName;
                    }
                }

                DataSet dsProfilePic = UserBL.CreateFile(fileName, HttpContext.Current.Server.MapPath("~/Files/ProfilePic/"), false, "ProfilePic");
                if (dsProfilePic.Tables.Count > 0 && dsProfilePic.Tables[0].Rows.Count > 0)
                {
                    objUserVal.ProfilePicFileID = dsProfilePic.Tables[0].Rows[0]["UniqueID"].ToString();
                }
            }

            objUserVal.EmailNotification = false;
            objUserVal.PushNotification = false;

            string companyLogoBase64 = Convert.ToString(jsonResult.SelectToken("CompanyProfileImageBase64"));
            if (!string.IsNullOrEmpty(companyLogoBase64))
            {
                var files = companyLogoBase64.Split(new string[] { "," }, StringSplitOptions.None);
                if (files.Count() == 1)
                    companyLogoBase64 = files[0];
                else
                    companyLogoBase64 = files[1];

                byte[] imageBytes = Convert.FromBase64String(companyLogoBase64);
                string fileName = ChildUserID + "_" + Guid.NewGuid() + "." + Utility.GetFileExtension(companyLogoBase64);
                string filePath = HttpContext.Current.Server.MapPath("~/Files/CompLogo/" + fileName);
                File.WriteAllBytes(filePath, imageBytes);

                if (HttpContext.Current.Session["CompId"] != null)
                {
                    if (Convert.ToInt32(HttpContext.Current.Session["CompId"]) == compid)
                    {
                        HttpContext.Current.Session["CompanyProfilePicFile"] = fileName;
                    }
                }
                DataSet dsCompanyLogo = UserBL.CreateFile(fileName, HttpContext.Current.Server.MapPath("~/Files/CompLogo/"), false, "");
                if (dsCompanyLogo.Tables.Count > 0 && dsCompanyLogo.Tables[0].Rows.Count > 0)
                {
                    objUserVal.CompanyProfilePicFileID = dsCompanyLogo.Tables[0].Rows[0]["UniqueID"].ToString();
                }
            }

            objUserVal.ThemeColor = (string)jsonResult.SelectToken("CompanyThemeColor");
            objUserVal.ThemeColor2 = (string)jsonResult.SelectToken("CompanyThemeColor2");
            objUserVal.ThemeColor3 = (string)jsonResult.SelectToken("CompanyThemeColor3");
            objUserVal.ThemeColor4 = (string)jsonResult.SelectToken("CompanyCustomFont");

            if (HttpContext.Current.Session["CompId"] != null)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["CompId"]) == compid)
                {
                    HttpContext.Current.Session["ThemeColor"] = objUserVal.ThemeColor;
                    HttpContext.Current.Session["ThemeColor2"] = objUserVal.ThemeColor2;
                    HttpContext.Current.Session["ThemeColor3"] = objUserVal.ThemeColor3;
                    HttpContext.Current.Session["ThemeColor4"] = objUserVal.ThemeColor4;
                }
            }

            return ValFlag;
        }

        [Route("API/Organization/GetAdminUserDetailsForParent")]
        [HttpPost]
        public IHttpActionResult GetAdminUserDetailsForParent(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                int ChildUserID = 0;
                if (identity.Role == ConstantMessages.Roles.superadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;

                    if (jsonResult.SelectToken("UserID") != null && jsonResult.SelectToken("UserID").ToString().Trim() != "")
                    {
                        ChildUserID = (int)jsonResult.SelectToken("UserID");

                        var ds = OrganizationBL.GetAdminUserDetails(objUser, ChildUserID);
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

        [Route("API/Organization/GetAdminUserDetails")]
        [HttpPost]
        public IHttpActionResult GetAdminUserDetails(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                int ChildUserID = 0;
                if (identity.Role == ConstantMessages.Roles.companyadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;

                    ChildUserID = Convert.ToInt32(identity.UserID);
                    var ds = OrganizationBL.GetAdminUserDetails(objUser, ChildUserID);
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


        [Route("API/Organization/UpdateAdminUserForParent")]
        [HttpPost]
        public IHttpActionResult UpdateAdminUserForParent(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    int ChildUserID = 0;
                    if (jsonResult.SelectToken("UserID") != null && jsonResult.SelectToken("UserID").ToString().Trim() != "")
                    {
                        ChildUserID = (int)jsonResult.SelectToken("UserID");

                        if (ValidateUserDetails(identity.CompId, jsonResult, out Message, out objUser, "update", ChildUserID))
                        {
                            objUser.UserID = identity.UserID;
                            objUser.CompId = identity.CompId;
                            objUser.Role = identity.Role;

                            var ds = OrganizationBL.CreateUpdateAdminUser(objUser, 2, ChildUserID);
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
                            data = Utility.API_Status("2", Message);
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

        [Route("API/Organization/UpdateAdminUser")]
        [HttpPost]
        public IHttpActionResult UpdateAdminUser(JObject jsonResult)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    int ChildUserID = 0;
                    ChildUserID = Convert.ToInt32(identity.UserID);

                    if (ValidateUserDetails(identity.CompId, jsonResult, out Message, out objUser, "update", ChildUserID))
                    {
                        objUser.UserID = identity.UserID;
                        objUser.CompId = identity.CompId;
                        objUser.Role = identity.Role;

                        var ds = OrganizationBL.CreateUpdateAdminUser(objUser, 2, ChildUserID);
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
    }
}