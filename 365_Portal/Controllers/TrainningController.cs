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
                    var ds = TrainningBL.GetContentDetails(compId, userId, topicId, moduleId, contentId);
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
                    string rating = requestParams["Rating"].ToString();
                    var ds = TrainningBL.SubmitAnswers(compId, userId, surveyId, requestParams);                
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
    }
}
 