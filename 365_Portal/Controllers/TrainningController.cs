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
                    var ds = TrainningBL.GetContentDetails(compId, userId, topicId, moduleId, contentId);

                    // Table 0: Content Information
                    // Table 1: Questions
                    // Table 2: Answer Options
                    // Table 3: Flashcards     

                    List<Question> questionList = new List<Question>();
                    questionList = (from DataRow dr in ds.Tables[1].Rows
                                    select new Question()
                                    {
                                        QuestionID = Convert.ToInt32(dr["QuestionID"]),
                                        IsMultiSelectQuestion = dr["IsMultiSelectQuestion"].ToString(),
                                        QType = dr["QType"].ToString(),
                                        Type = dr["Type"].ToString(),
                                        QuestionTypeID = dr["QuestionTypeID"].ToString(),
                                        IsMandatory = dr["IsMandatory"].ToString(),
                                        IsMultiLine = dr["IsMultiLine"].ToString(),
                                        MaxLength = dr["MaxLength"].ToString(),
                                        Title = dr["Title"].ToString(),
                                        SortOrder = dr["SortOrder"].ToString(),
                                        IsAnswered = dr["IsAnswered"].ToString(),
                                        ResponseID = dr["ResponseID"].ToString(),
                                        TotalScore = dr["TotalScore"].ToString(),
                                        ScoreEarned = dr["ScoreEarned"].ToString(),
                                        PercentageEarned = dr["PercentageEarned"].ToString(),
                                        IPAddress = dr["IPAddress"].ToString()
                                    }).ToList();

                    List<AnswerOption> ansOptionList = new List<AnswerOption>();
                    ansOptionList = (from DataRow dr in ds.Tables[2].Rows
                                     select new AnswerOption()
                                     {
                                         QuestionID = Convert.ToInt32(dr["QuestionID"].ToString()),
                                         IsAnswered = dr["IsAnswered"].ToString(),                                        
                                         AnswerID = dr["AnswerID"].ToString(),
                                         AnswerText = dr["AnswerText"].ToString(),
                                         SortOrder = dr["SortOrder"].ToString(),
                                         IsCorrect = dr["IsCorrect"].ToString(),
                                         CorrectScore = dr["CorrectScore"].ToString(),
                                         InCorrectScore = dr["InCorrectScore"].ToString(),
                                         Value_ID = dr["Value_ID"].ToString(),
                                         Value_Text = dr["Value_Text"].ToString(),
                                         Value_IsCorrect = dr["Value_IsCorrect"].ToString(),
                                         Value_CorrectScore = dr["Value_CorrectScore"].ToString(),
                                         Value_InCorrectScore = dr["Value_InCorrectScore"].ToString(),
                                         FilePath = dr["FilePath"].ToString()
                                     }).ToList();
                    foreach (var question in questionList)
                    {
                        List<AnswerOption> ansOptions = new List<AnswerOption>();
                        question.AnswerOptions = ansOptionList.Where(p => p.QuestionID == question.QuestionID).ToList();
                    }
                    var questionJson = JsonConvert.SerializeObject(questionList);
                    var dd = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    dd = dd.Substring(1, dd.Length - 2);                    
                    data = Utility.GetJSONData("1", "Successful", dd, questionJson, Utility.ConvertDataSetToJSONString(ds.Tables[3]));
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

        public class AnswerOption
        {
            public int QuestionID { get; set; }
            public string IsAnswered { get; set; }
            public string AnswerID { get; set; }
            public string AnswerText { get; set; }
            public string SortOrder { get; set; }
            public string IsCorrect { get; set; }
            public string CorrectScore { get; set; }
            public string InCorrectScore { get; set; }
            public string Value_ID { get; set; }
            public string Value_Text { get; set; }
            public string Value_IsCorrect { get; set; }
            public string Value_CorrectScore { get; set; }
            public string Value_InCorrectScore { get; set; }
            public string FilePath { get; set; }
        }

        public class Question
        {
            public int QuestionID { get; set; }
            public string IsMultiSelectQuestion { get; set; }
            public string QType { get; set; }
            public string Type { get; set; }
            public string QuestionTypeID { get; set; }
            public string IsMandatory { get; set; }
            public string IsMultiLine { get; set; }
            public string MaxLength { get; set; }
            public string Title { get; set; }
            public string SortOrder { get; set; }
            public string IsAnswered { get; set; }
            public string ResponseID { get; set; }
            public string TotalScore { get; set; }
            public string ScoreEarned { get; set; }
            public string PercentageEarned { get; set; }
            public string IPAddress { get; set; }
            public List<AnswerOption> AnswerOptions { get; set; }
        }
    }
}
