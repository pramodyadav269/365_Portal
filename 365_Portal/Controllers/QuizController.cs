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
    public class QuizController : ApiController
    {
        [Route("api/Quiz/GetContentList")]
        [HttpPost]
        public IHttpActionResult GetContentList(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    var topicId = Convert.ToInt32(Convert.ToString(requestParams["TopicID"]));
                    var moduleId = Convert.ToInt32(Convert.ToString(requestParams["ModuleID"]));
                    var contentId = Convert.ToInt32(Convert.ToString(requestParams["ContentID"]));
                    var contentTypeId = Convert.ToInt32(Convert.ToString(requestParams["ContentTypeID"]));
                    var isGift = Convert.ToBoolean(Convert.ToInt32(requestParams["IsGift"]));

                    ContentBO _content = new ContentBO();
                    _content.CompID = identity.CompId;
                    _content.TopicID = topicId;
                    _content.ModuleID = moduleId;
                    _content.ContentID = contentId;
                    _content.ContentTypeID = contentTypeId;
                    _content.IsGift = isGift;
                    var lstContent = new List<ContentBO>();
                    var ds = ContentBL.GetContentList_Formatted(_content, ref lstContent);
                    var contentJson = JsonConvert.SerializeObject(lstContent);
                    data = Utility.Successful(contentJson);
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

        [Route("api/Quiz/SaveContent")]
        [HttpPost]
        public IHttpActionResult SaveContent(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    ContentBO content = new ContentBO();
                    content.CompID = identity.CompId;
                    content.CreatedBy = identity.UserID;
                    content.TopicID = Convert.ToInt32(Convert.ToString(requestParams["TopicID"]));
                    content.ModuleID = Convert.ToInt32(Convert.ToString(requestParams["ModuleID"]));
                    content.ContentID = 0;
                    if (!string.IsNullOrEmpty(Convert.ToString(requestParams["ContentID"])))
                        content.ContentID = Convert.ToInt32(requestParams["ContentID"]);
                    content.ContentTypeID = Convert.ToInt32(Convert.ToString(requestParams["ContentTypeID"]));
                    content.ContentTitle = Convert.ToString(requestParams["Title"]);
                    content.ContentDescription = Convert.ToString(requestParams["Description"]);
                    content.IsPublished = Convert.ToBoolean(Convert.ToString(requestParams["IsPublished"]));
                    content.SkipFlashcard = Convert.ToBoolean(Convert.ToString(requestParams["SkipFlashcard"]));
                    content.IsActive = true;
                    content.IsGift = Convert.ToBoolean(Convert.ToString(requestParams["IsGift"]));
                    var totalScore = 0.0;
                    var passingScore = 0.0;
                    var passingPercent = 0.0;
                    var docType = "";
                    if (content.ContentTypeID == 2)
                    {
                        // Survey
                        docType = "SURVEY";
                    }
                    else if (content.ContentTypeID == 3)
                    {
                        // Flashcards
                        docType = "FLASHCARD";
                    }
                    else if (content.ContentTypeID == 5)
                    {
                        // Final Quiz
                        docType = "FINALQUIZ";
                        totalScore = Convert.ToDouble(Convert.ToString(requestParams["TotalScore"]));
                        passingScore = Convert.ToDouble(Convert.ToString(requestParams["PassingScore"]));
                        passingPercent = Convert.ToDouble(Convert.ToString(requestParams["PassingPercentage"]));
                    }
                    content.TotalScore = totalScore;
                    content.PassingScore = passingScore;
                    content.PassingPercent = passingPercent;
                    content.DocType = docType;
                    content.ContentFileID = null;
                    content.FlashcardHighlights = null;

                    var ds = new DataSet();
                    if (content.ContentID > 0)
                        ds = ContentBL.ModifyContent(content);
                    else
                        ds = ContentBL.CreateContent(content);
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

        [Route("api/Quiz/ManageQuestion")]
        [HttpPost]
        public IHttpActionResult ManageQuestion(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    var compId = identity.CompId;
                    var userId = identity.UserID;
                    var questionId = 0; var isMandatory = true; var isMultiline = true;
                    var contentId = 0; var title = ""; var qType = 0; var maxScore = 0.0;
                    var isBox = false;
                    var contentTypeId = Convert.ToInt32(Convert.ToString(requestParams["ContentTypeID"]));
                    var action = Convert.ToInt32(Convert.ToString(requestParams["Action"]));
                    var type = Convert.ToInt32(Convert.ToString(requestParams["Type"]));
                    if (!string.IsNullOrEmpty(Convert.ToString(requestParams["QuestionID"])))
                        questionId = Convert.ToInt32(requestParams["QuestionID"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(requestParams["ContentID"])))
                        contentId = Convert.ToInt32(requestParams["ContentID"]);
                    if (action == 1 || action == 2)
                    {
                        title = Convert.ToString(requestParams["Title"]);
                        qType = Convert.ToInt32(Convert.ToString(requestParams["QType"])); // Numeric
                        if (!string.IsNullOrEmpty(Convert.ToString(requestParams["IsBox"])))
                            isBox = Convert.ToBoolean(Convert.ToString(requestParams["IsBox"]));

                        if (type == 1)
                        {
                            // Survey
                        }
                        else if (type == 2)
                        {
                            // Flashcards
                        }
                        else if (type == 3)
                        {
                            // Final Quiz
                            maxScore = Convert.ToDouble(Convert.ToString(requestParams["MaxScore"]));
                        }
                    }
                    else if (action == 3)
                    {

                    }
                    else if (action == 4)
                    {

                    }

                    // CALL BL
                    var ds = QuizBL.ManageQuestion(compId, userId, questionId, contentId, isMandatory, isMultiline, title, qType, isBox, action);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["QuestionID"].ToString()) > 0 && (action == 1 || action == 2))
                    {
                        questionId = Convert.ToInt32(ds.Tables[0].Rows[0]["QuestionID"].ToString());
                        for (int i = 0; i < requestParams["AnswerOptions"].Count(); i++)
                        {
                            bool isCorrect = false;
                            double score = 0;
                            var answerid = 0;
                            if (!string.IsNullOrEmpty(Convert.ToString(requestParams["AnswerOptions"][i]["CorrectScore"])))
                                score = Convert.ToInt32(requestParams["AnswerOptions"][i]["CorrectScore"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(requestParams["AnswerOptions"][i]["AnswerID"])))
                                answerid = Convert.ToInt32(requestParams["AnswerOptions"][i]["AnswerID"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(requestParams["AnswerOptions"][i]["IsCorrect"])))
                                isCorrect = Convert.ToBoolean(Convert.ToString(requestParams["AnswerOptions"][i]["IsCorrect"]));
                            var ds1 = QuizBL.ManageAnsOptions(compId, userId, contentId, questionId, answerid, Convert.ToString(requestParams["AnswerOptions"][i]["AnswerText"]),
                                isCorrect, score, answerid > 0 ? 2 : 1);
                        }
                    }

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

        [Route("api/Quiz/ManageAnsOptions")]
        [HttpPost]
        public IHttpActionResult ManageAnsOptions(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    var compId = identity.CompId;
                    var userId = identity.UserID;
                    var answerId = 0; var title = ""; var isCorrect = false; var score = 0.0;

                    var action = Convert.ToInt32(Convert.ToString(requestParams["Action"]));
                    var contentTypeId = Convert.ToInt32(Convert.ToString(requestParams["ContentTypeID"]));
                    var contentId = Convert.ToInt32(requestParams["ContentID"]);
                    var questionId = Convert.ToInt32(requestParams["QuestionID"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(requestParams["AnswerID"])))
                        answerId = Convert.ToInt32(requestParams["AnswerID"]);
                    if (action == 1 || action == 2)
                    {
                        title = Convert.ToString(requestParams["Title"]);
                        isCorrect = false;
                        if (!string.IsNullOrEmpty(Convert.ToString(requestParams["IsCorrect"])))
                            isCorrect = Convert.ToBoolean(Convert.ToString(requestParams["IsCorrect"]));
                        if (contentTypeId == 5)
                        {
                            // Final Quiz
                            score = Convert.ToDouble(Convert.ToString(requestParams["Score"])); // Numeric
                        }
                    }
                    else if (action == 3)
                    {

                    }
                    else if (action == 4)
                    {

                    }

                    // CALL BL
                    var ds = QuizBL.ManageAnsOptions(compId, userId, contentId, questionId, answerId, title, isCorrect, score, action);
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

        [Route("api/Quiz/ManageFlashcardIntro")]
        [HttpPost]
        public IHttpActionResult ManageFlashcardIntro(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    var compId = identity.CompId;
                    var userId = identity.UserID;
                    var introId = 0; var comments = "";
                    var contentId = Convert.ToInt32(Convert.ToString(requestParams["ContentID"]));
                    var action = Convert.ToInt32(Convert.ToString(requestParams["Action"]));
                    if (action == 1 || action == 2)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(requestParams["IntroID"])))
                            introId = Convert.ToInt32(requestParams["IntroID"]);
                        comments = Convert.ToString(requestParams["Comments"]);
                    }
                    else if (action == 3)
                    {

                    }
                    else if (action == 4)
                    {

                    }

                    // CALL BL
                    var ds = QuizBL.ManageFlashcardIntro(compId, userId, introId, comments, contentId, action);
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

        [Route("api/Quiz/ManageFlashcardSlides")]
        [HttpPost]
        public IHttpActionResult ManageFlashcardSlides(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    var compId = identity.CompId;
                    var userId = identity.UserID;
                    var flashcardId = 0; var title = ""; var description = "";
                    var contentId = Convert.ToInt32(Convert.ToString(requestParams["ContentID"]));
                    var action = Convert.ToInt32(Convert.ToString(requestParams["Action"]));
                    if (action == 1 || action == 2)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(requestParams["FlashcardID"])))
                            flashcardId = Convert.ToInt32(requestParams["FlashcardID"]);
                        title = Convert.ToString(requestParams["Title"]);
                        description = Convert.ToString(requestParams["Description"]);
                    }
                    else if (action == 3)
                    {

                    }
                    else if (action == 4)
                    {

                    }

                    // CALL BL

                    var ds = QuizBL.ManageFlashcardSlides(compId, userId, flashcardId, title, contentId, description, action);
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
    }

}