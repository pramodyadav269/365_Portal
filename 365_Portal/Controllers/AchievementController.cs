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
using _365_Portal.Code.BO;
using static _365_Portal.Code.BO.UserProfile;
using System.IO;

namespace _365_Portal.Controllers
{
    public class AchievementController : ApiController
    {
        #region CRUD FOR Achievement   
        [HttpPost]
        [Route("API/Achievement/CreateAchievement")]
        public IHttpActionResult CreateAchievement(JObject requestParams)
        {
            var data = string.Empty;
            string AchievementTitle = string.Empty;
            string AchivementDescription = string.Empty;
            string LongDescription = string.Empty;
            int CompID;
            string CreatedBy = string.Empty;
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {
                    if (!string.IsNullOrEmpty(requestParams["AchievementTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["AchivementDescription"].ToString())
                 && !string.IsNullOrEmpty(requestParams["LongDescription"].ToString()))
                    {
                        CompID = identity.CompId;
                        CreatedBy = identity.UserID;


                        if (!string.IsNullOrEmpty(requestParams["AchievementTitle"].ToString()))
                        {
                            AchievementTitle = requestParams["AchievementTitle"].ToString();
                        }
                        else
                        {
                            AchievementTitle = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(requestParams["AchivementDescription"].ToString()))
                        {
                            AchivementDescription = requestParams["AchivementDescription"].ToString();
                        }
                        else
                        {
                            AchivementDescription = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(requestParams["LongDescription"].ToString()))
                        {
                            LongDescription = requestParams["LongDescription"].ToString();
                        }
                        else
                        {
                            LongDescription = string.Empty;
                        }
                        var ds = AchivementBL.CreateAchievement(CompID,0,AchievementTitle,AchivementDescription,LongDescription,CreatedBy);
                       
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
        [Route("API/Achievement/ModifyAchievement")]
        public IHttpActionResult ModifyAchievement(JObject requestParams)
        {
            var data = string.Empty;
            string AchievementTitle = string.Empty;
            string AchivementDescription = string.Empty;
            string LongDescription = string.Empty;
            int AchievementID;
            int CompID;
            string CreatedBy = string.Empty;

            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {
                    if (!string.IsNullOrEmpty(requestParams["AchievementTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["AchivementDescription"].ToString())
                                    && !string.IsNullOrEmpty(requestParams["LongDescription"].ToString()) && !string.IsNullOrEmpty(requestParams["AchievementID"].ToString()))
                    {
                        CompID = identity.CompId;
                        CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["AchievementID"].ToString()))
                        {
                            AchievementID = (int)requestParams["AchievementID"];
                        }
                        
                        if (!string.IsNullOrEmpty(requestParams["AchievementTitle"].ToString()))
                        {
                            AchievementTitle = requestParams["AchievementTitle"].ToString();
                        }
                        else
                        {
                            AchievementTitle = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(requestParams["AchivementDescription"].ToString()))
                        {
                            AchivementDescription = requestParams["AchivementDescription"].ToString();
                        }
                        else
                        {
                            AchivementDescription = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(requestParams["LongDescription"].ToString()))
                        {
                            LongDescription = requestParams["LongDescription"].ToString();
                        }
                        else
                        {
                            LongDescription = string.Empty;
                        }
                        var ds = AchivementBL.ModifyAchievement(CompID, 0, AchievementTitle, AchivementDescription, LongDescription, CreatedBy);
                        
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
        [Route("API/Achievement/ViewAchievement")]
        public IHttpActionResult ViewAchievement(JObject requestParams)
        {
            var data = string.Empty;
            int CompID;
            int AchievementID=0;
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {
                    CompID = identity.CompId;
                    if (!string.IsNullOrEmpty(requestParams["AchievementID"].ToString()))
                    {
                        AchievementID = (int)requestParams["AchievementID"];
                    }

                    var ds = AchivementBL.GetAchievements(CompID, AchievementID);
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables["Data"];

                        data = Utility.ConvertDataSetToJSONString(dt);
                        data = Utility.Successful(data);


                    }
                    else
                    {
                        data = ConstantMessages.WebServiceLog.GenericErrorMsg;
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
        [Route("API/Achievement/DeleteAchievement")]
        public IHttpActionResult DeleteAchievement(JObject requestParams)
        {
            var data = string.Empty;
            int AchievementID = 0;
            string CreatedBy = string.Empty;
            int CompID;


            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if ((Convert.ToInt32(requestParams["AchievementID"]) != 0 && !string.IsNullOrEmpty(requestParams["AchievementID"].ToString())))
                    {
                        CompID = identity.CompId;
                        CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["AchievementID"].ToString()))
                        {
                            AchievementID = Convert.ToInt32(requestParams["AchievementID"]);
                        }

                        var ds = AchivementBL.DeleteAchievement(CompID, AchievementID);

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
    }
}
