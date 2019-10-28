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
                    APIResult.ThrowException(ex);
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
                    data = Utility.GetModulesJSONFormat("1", "Successful", Utility.ConvertDataSetToJSONString(ds.Tables[0]), Utility.ConvertDataSetToJSONString(ds.Tables[1]));
                }
                catch (Exception ex)
                {
                    APIResult.ThrowException(ex);
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
                    var ds = TrainningBL.GetContentsByModule(compId, userId, topicId, moduleId);
                    data = Utility.GetModulesJSONFormat("1", "Successful", Utility.ConvertDataSetToJSONString(ds.Tables[0]), Utility.ConvertDataSetToJSONString(ds.Tables[1]));
                }
                catch (Exception ex)
                {
                    APIResult.ThrowException(ex);
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
                    APIResult.ThrowException(ex);
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

                    var questionJson = JsonConvert.SerializeObject(questionList);
                    var contents = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    contents = contents.Substring(2, contents.Length - 4);
                    data = Utility.GetJSONData("1", "Successful", contents, questionJson,
                        Utility.ConvertDataSetToJSONString(ds.Tables[3]), Utility.ConvertDataSetToJSONString(ds.Tables[4]));
                }
                catch (Exception ex)
                {
                    APIResult.ThrowException(ex);
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
                    APIResult.ThrowException(ex);
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
                    int surveyId = Convert.ToInt32(requestParams["SurveyID"].ToString());
                    int moduleId = Convert.ToInt32(requestParams["ModuleID"].ToString());
                    int contentId = Convert.ToInt32(requestParams["ContentID"].ToString());
                    var ds = TrainningBL.SubmitAnswers(compId, userId, surveyId, requestParams);
                    if (ds.Tables.Count > 0)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["ResponseID"].ToString()) > 0)
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
                    APIResult.ThrowException(ex);
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
