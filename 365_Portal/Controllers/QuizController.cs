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

        [Route("api/Quiz/SaveSurvey")]
        [HttpPost]
        public IHttpActionResult SaveSurvey(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    var compId = identity.CompId;
                    var userId = identity.UserID;
                    var contentId = 0;
                    var type = Convert.ToInt32(Convert.ToString(requestParams["Type"]));
                    if (!string.IsNullOrEmpty(Convert.ToString(requestParams["ContentID"])))
                        contentId = Convert.ToInt32(requestParams["ContentID"]);
                    var title = Convert.ToString(requestParams["Title"]);
                    var description = Convert.ToString(requestParams["Description"]);
                    var overview = Convert.ToString(requestParams["Overview"]);
                    var isPublished = Convert.ToBoolean(Convert.ToString(requestParams["IsPublished"]));
                    var totalScore = 0.0;
                    var passingScore = 0.0;
                    var passingPercent = 0.0;
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
                        totalScore = Convert.ToDouble(Convert.ToString(requestParams["TotalScore"]));
                        passingScore = Convert.ToDouble(Convert.ToString(requestParams["PassingScore"]));
                        passingPercent = Convert.ToDouble(Convert.ToString(requestParams["PassingPercentage"]));
                    }

                    // CALL BL
                    var ds = QuizBL.SaveSurvey(compId, userId, contentId, title, description, overview, isPublished, totalScore, passingScore, passingPercent);
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
                    var questionId = 0; var isMandatory = true; var isMultiline = true; var maxLength = 150;
                    var contentId = 0; var title = ""; var qType = 0; var maxScore = 0.0;
                    var isBox = false;
                    var action = Convert.ToInt32(Convert.ToString(requestParams["Action"]));
                    var type = Convert.ToInt32(Convert.ToString(requestParams["Type"]));
                    if (!string.IsNullOrEmpty(Convert.ToString(requestParams["QuestionID"])))
                        questionId = Convert.ToInt32(requestParams["QuestionID"]);
                    if (action == 1 || action == 2)
                    {
                        contentId = Convert.ToInt32(requestParams["ContentID"]);
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
                    var ds = QuizBL.ManageQuestion(compId, userId, questionId, contentId, isMandatory, isMultiline, title, qType, isBox, type, action);
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
                    var type = Convert.ToInt32(Convert.ToString(requestParams["Type"]));
                    var contentId = Convert.ToInt32(requestParams["ContentID"]);
                    var questionId = Convert.ToInt32(requestParams["QuestionID"]);

                    if (action == 1 || action == 2)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(requestParams["AnswerID"])))
                            answerId = Convert.ToInt32(requestParams["AnswerID"]);
                        title = Convert.ToString(requestParams["Title"]);
                        isCorrect = false;
                        if (!string.IsNullOrEmpty(Convert.ToString(requestParams["IsCorrect"])))
                            isCorrect = Convert.ToBoolean(Convert.ToString(requestParams["IsCorrect"]));
                        if (type == 3)
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
                    var ds = QuizBL.ManageAnsOptions(compId, userId, type, contentId, questionId, answerId, title, isCorrect, score, action);
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