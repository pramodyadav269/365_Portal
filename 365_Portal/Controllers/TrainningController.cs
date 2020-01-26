using _365_Portal.Code.BL;
using _365_Portal.Code;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Reflection;
using Newtonsoft.Json;
using _365_Portal.Code.BO;
using _365_Portal.Models;

namespace _365_Portal.Controllers
{
    public class TrainningController : ApiController
    {
        [Route("api/Trainning/GetTopicsByUser")]
        [HttpPost]
        public IHttpActionResult GetTopicsByUser(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    var ds = TrainningBL.GetTopicsByUser(compId, userId);
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    data = Utility.Successful(data);
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/GetModulesByTopic")]
        [HttpPost]
        public IHttpActionResult GetModulesByTopic(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    int topicId = Convert.ToInt32(requestParams["TopicID"].ToString());
                    var ds = TrainningBL.GetModulesByTopic(compId, userId, topicId);
                    var sourceInfo = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    sourceInfo = sourceInfo.Substring(2, sourceInfo.Length - 4);
                    data = Utility.GetModulesJSONFormat("1", "Successful", sourceInfo, Utility.ConvertDataSetToJSONString(ds.Tables[1]), Utility.ConvertDataSetToJSONString(ds.Tables[2]));
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/GetContentsByModule")]
        [HttpPost]
        public IHttpActionResult GetContentsByModule(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    int topicId = Convert.ToInt32(requestParams["TopicID"].ToString());
                    int moduleId = Convert.ToInt32(requestParams["ModuleID"].ToString());
                    var isGift = false;
                    if (!string.IsNullOrEmpty(Convert.ToString(requestParams["IsGift"])))
                        isGift = Convert.ToBoolean(Convert.ToInt32(requestParams["IsGift"].ToString()));
                    var ds = TrainningBL.GetContentsByModule(compId, userId, topicId, moduleId, isGift);
                    var sourceInfo = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    sourceInfo = sourceInfo.Substring(2, sourceInfo.Length - 4);
                    data = Utility.GetModulesJSONFormat("1", "Successful", sourceInfo, Utility.ConvertDataSetToJSONString(ds.Tables[1]), Utility.ConvertDataSetToJSONString(ds.Tables[2]));
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/UpdateContent")]
        [HttpPost]
        public IHttpActionResult UpdateContent(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    int topicId = Convert.ToInt32(requestParams["TopicID"].ToString());
                    int moduleId = Convert.ToInt32(requestParams["ModuleID"].ToString());
                    int contentId = Convert.ToInt32(requestParams["ContentID"].ToString());
                    var ds = TrainningBL.UpdateContent(compId, userId, topicId, moduleId, contentId);
                    if (ds.Tables.Count > 0)
                    {
                        var tableIndex = 0;
                        string isGift = "0";
                        if (ds.Tables[0].Columns.Contains("IsGift"))
                        {
                            // Successful -  Unlocked a gift
                            isGift = "1";
                            data = Utility.ContentUpdated("1", "Success", isGift, Utility.ConvertDataSetToJSONString(ds.Tables[0]));
                            tableIndex = 1;
                            TrainningBL.SendNotification(compId, userId, ConstantMessages.NotificationType.gift, "", ds.Tables[0].Rows[0]["Title"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["StatusCode"].ToString() == "1")
                        {
                            // Successful - Without Gift
                            data = Utility.ContentUpdated("1", "Success", isGift, "");
                        }
                        else
                        {
                            // Error. Check Logs
                            data = Utility.API_Status("1", "There might be some error. Please try again later");
                        }

                        if (ds.Tables[tableIndex].Columns.Contains("StatusCode"))
                        {
                            var moduleCompleted = false;
                            if (Convert.ToBoolean(Convert.ToInt32(Convert.ToString(ds.Tables[tableIndex].Rows[0]["IsModuleCompleted"]))))
                            {
                                // Module Completed
                                // ds.Tables[tableIndex + 1];
                                moduleCompleted = true;
                                TrainningBL.SendNotification(compId, userId, ConstantMessages.NotificationType.module, "", ds.Tables[tableIndex + 1].Rows[0]["Title"].ToString());
                            }
                            if (Convert.ToBoolean(Convert.ToInt32(Convert.ToString(ds.Tables[tableIndex].Rows[0]["IsTopicCompleted"]))))
                            {
                                // Topic Completed
                                //ds.Tables[tableIndex + 1 + (moduleCompleted ? 1 : 0)];
                                TrainningBL.SendNotification(compId, userId, ConstantMessages.NotificationType.topic, "", ds.Tables[tableIndex + 1 + (moduleCompleted ? 1 : 0)].Rows[0]["Title"].ToString());
                            }
                        }
                    }
                    else
                    {
                        // Unknown Error
                        data = Utility.API_Status("0", "No Records Found.");
                    }
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/GetContentDetails")]
        [HttpPost]
        public IHttpActionResult GetContentDetails(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    int topicId = Convert.ToInt32(requestParams["TopicID"].ToString());
                    int moduleId = Convert.ToInt32(requestParams["ModuleID"].ToString());
                    int contentId = Convert.ToInt32(requestParams["ContentID"].ToString());

                    List<Question> questionList = new List<Question>();
                    var ds = TrainningBL.GetContentDetails(compId, userId, topicId, moduleId, contentId, ref questionList);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var questionJson = JsonConvert.SerializeObject(questionList);
                        var contents = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                        if (contents != "[]")
                            contents = contents.Substring(2, contents.Length - 4);
                        data = Utility.GetJSONData("1", "Successful", contents, questionJson,
                            Utility.ConvertDataSetToJSONString(ds.Tables[3]), Utility.ConvertDataSetToJSONString(ds.Tables[4]));
                    }
                    else
                    {
                        // Unknown Error
                        data = Utility.API_Status("0", "No Records Found.");
                    }
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex);
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/RateContent")]
        [HttpPost]
        public IHttpActionResult RateContent(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    int topicId = Convert.ToInt32(requestParams["TopicID"].ToString());
                    int moduleId = Convert.ToInt32(requestParams["ModuleID"].ToString());
                    int contentId = Convert.ToInt32(requestParams["ContentID"].ToString());
                    string rating = requestParams["Rating"].ToString();
                    var ds = TrainningBL.RateContent(compId, userId, topicId, moduleId, contentId, rating, userId);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["StatusCode"].ToString() == "1")
                        {
                            // Successful
                            data = Utility.Successful("");
                        }
                        else
                        {
                            // Error. Check Logs
                            data = Utility.API_Status("1", "There might be some error. Please try again later");
                        }
                    }
                    else
                    {
                        // Unknown Error
                        data = Utility.API_Status("1", "Unknown Error");
                    }
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/SubmitAnswers")]
        [HttpPost]
        public IHttpActionResult SubmitAnswers(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    int topicId = Convert.ToInt32(requestParams["TopicID"].ToString());
                    int surveyId = Convert.ToInt32(requestParams["SurveyID"].ToString());
                    int moduleId = Convert.ToInt32(requestParams["ModuleID"].ToString());
                    int contentId = Convert.ToInt32(requestParams["ContentID"].ToString());
                    var ds = TrainningBL.SubmitAnswers(compId, userId, surveyId, requestParams);
                    if (ds.Tables.Count > 0)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["ResponseID"].ToString()) > 0)
                        {
                            List<Question> questionList = new List<Question>();
                            var dsContent = TrainningBL.GetContentDetails(compId, userId, topicId, moduleId, contentId, ref questionList);
                            var questionJson = JsonConvert.SerializeObject(questionList);
                            var contents = Utility.ConvertDataSetToJSONString(dsContent.Tables[0]);
                            contents = contents.Substring(2, contents.Length - 4);
                            data = Utility.GetJSONData("1", "Successful", contents, questionJson,
                                Utility.ConvertDataSetToJSONString(dsContent.Tables[3]), Utility.ConvertDataSetToJSONString(dsContent.Tables[4]));
                        }
                        else
                        {
                            // Error. Check Logs
                            data = Utility.API_Status("1", "There might be some error. Please try again later");
                        }
                    }
                    else
                    {
                        // Unknown Error
                        data = Utility.API_Status("1", "Unknown Error");
                    }
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/GetNotifications")]
        [HttpPost]
        public IHttpActionResult GetNotifications(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    var ds = TrainningBL.GetNotifications(compId, userId);
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    data = Utility.Successful(data);
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/UpdateNotification")]
        [HttpPost]
        public IHttpActionResult UpdateNotification(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    string type = Convert.ToString(requestParams["Type"]);
                    string notificationIds = Convert.ToString(requestParams["NotificationIDs"]);
                    string token = Convert.ToString(requestParams["Token"]);
                    var ds = TrainningBL.UpdateNotification(compId, userId, type, notificationIds, token);
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    data = Utility.Successful(data);
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/GetAchievementNGifts")]
        [HttpPost]
        public IHttpActionResult GetAchievementNGifts(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    List<Achievement> achievementList = new List<Achievement>();
                    var ds = TrainningBL.GetAchievementGifts(compId, userId, ref achievementList);
                    data = Utility.GetAchievementGiftsJSONFormat("1", "Success",
                        JsonConvert.SerializeObject(achievementList),
                        Utility.ConvertDataSetToJSONString(ds.Tables[2]));
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/IsUserOnline")]
        [HttpPost]
        public IHttpActionResult IsUserOnline(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    DateTime startDate = DateTime.Now;
                    DateTime endDate = DateTime.Now;
                    int totalTime = 0;
                    int type = Convert.ToInt32(requestParams["Type"].ToString());
                    if (type == 2)
                    {
                        startDate = Convert.ToDateTime(requestParams["StartDate"].ToString());
                        endDate = Convert.ToDateTime(requestParams["EndDate"].ToString());
                        totalTime = Convert.ToInt32(requestParams["TotalTime"].ToString());
                    }
                    var ds = TrainningBL.IsUserOnline(compId, userId, type, startDate, endDate, totalTime);
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    data = Utility.Successful(data);
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/RetakeTest")]
        [HttpPost]
        public IHttpActionResult RetakeTest(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    string surveyId = requestParams["SurveyID"].ToString();
                    var ds = TrainningBL.ClearAnswers(compId, userId, surveyId);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["StatusCode"].ToString() == "1")
                        {
                            // Successful
                            data = Utility.Successful("");
                        }
                        else
                        {
                            // Error. Check Logs
                            data = Utility.API_Status("1", "There might be some error. Please try again later");
                        }
                    }
                    else
                    {
                        // Unknown Error
                        data = Utility.API_Status("0", "No Records Found.");
                    }
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/AssignTopicsByEntity")]
        [HttpPost]
        public IHttpActionResult AssignTopicsByEntity(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string[] mailUserIDs = new string[] { };
                    string userId = identity.UserID;
                    var topicIds = Convert.ToString(requestParams["TopicIds"].ToString());
                    var groupIds = Convert.ToString(requestParams["GroupIds"].ToString());
                    var userIds = Convert.ToString(requestParams["UserIds"].ToString());
                    var removeTopic = Convert.ToString(requestParams["RemoveTopics"].ToString());
                    
                    if (!string.IsNullOrEmpty(userIds))
                    {
                        var dsTopics = TrainningBL.GetUserAssignedTopic(compId, userIds);

                        string[] arrTopics = topicIds.Split(',');
                        string[] arrUsers  = userIds.Split(',');

                        mailUserIDs = new string[arrUsers.Length];

                        if (dsTopics.Tables.Count > 0 && dsTopics.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < arrUsers.Length; j++)
                            {
                                for (int i = 0; i < arrTopics.Length; i++)
                                {
                                    if (!dsTopics.Tables[0].Select().ToList().Exists(row => row["Topics"].ToString() == arrTopics[i] && row["UserID"].ToString() == arrUsers[j]))
                                    {
                                        mailUserIDs[j] = arrUsers[j];
                                    }
                                }
                            }                            
                        }
                    }                    

                    var ds = TrainningBL.AssignTopicsByEntity(compId, userId, topicIds, groupIds, userIds, removeTopic);
                    if (ds.Tables.Count > 0)
                    {
                        // Successful
                        data = Utility.Successful("");
                        if (mailUserIDs.Length > 0)
                        {
                            foreach (string UserID in mailUserIDs)
                            {
                                EmailHelper.GetEmailContent(Convert.ToInt32(UserID), identity.CompId, EmailHelper.Functionality.ADD_TOPIC, "", "");
                            }                            
                        }
                    }
                    else
                    {
                        // Unknown Error
                        data = Utility.API_Status("1", "Unknown Error");
                    }
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/GetTableData")]
        [HttpPost]
        public IHttpActionResult GetTableData(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    var type = Convert.ToString(requestParams["Type"].ToString());
                    var valueType = Convert.ToString(requestParams["ValueType"]);
                    var valueId = Convert.ToString(requestParams["ValueID"]);
                    var ds = TrainningBL.GetTableDataByType(compId, type, valueType, valueId);
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    data = Utility.Successful(data);
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }


        [Route("API/Trainning/GetMsgNotifications")]
        [HttpPost]
        public IHttpActionResult GetMsgNotifications()
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                string Message = string.Empty;

                DataSet ds = TrainningBL.GetMsgNotifications(identity.CompId, identity.UserID.ToString(), 5);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
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
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }
    }
}
