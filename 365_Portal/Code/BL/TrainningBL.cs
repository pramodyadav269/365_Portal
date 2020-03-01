using _365_Portal.Code.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using _365_Portal.Code.BO;
using System.IO;
using _365_Portal.Models;

namespace _365_Portal.Code.BL
{
    public class TrainningBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "TrainningBL", methodName);
        }

        public static DataSet GetTopics(int compId, string userId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetTopics(compId, userId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetTopicsByUser(int compId, string userId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetTopicsByUser(compId, userId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetModulesByTopic(int compId, string userId, int topicId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetModulesByTopic(compId, userId, topicId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetContentsByModule(int compId, string userId, int topicId, int moduleId, bool isGift)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetContentsByModule(compId, userId, topicId, moduleId, isGift);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetContentDetails(int compId, string userId, int topicId, int moduleId, int ContentID, ref List<Question> questionList)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetContentDetails(compId, userId, topicId, moduleId, ContentID);

                // Table 0: Content Information
                // Table 1: Questions
                // Table 2: Answer Options
                // Table 3: Flashcards Intro 
                // Table 3: Flashcards     

                questionList = new List<Question>();
                questionList = (from DataRow dr in ds.Tables[1].Rows
                                select new Question()
                                {
                                    QuestionID = Convert.ToInt32(dr["QuestionID"]),
                                    IsMultiSelectQuestion = !string.IsNullOrEmpty(dr["IsMultiSelectQuestion"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsMultiSelectQuestion"].ToString())) : true,
                                    QType = dr["QType"].ToString(),
                                    Type = dr["Type"].ToString(),
                                    QuestionTypeID = Convert.ToInt32(dr["QuestionTypeID"].ToString()),
                                    IsBox = !string.IsNullOrEmpty(dr["IsBox"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsBox"].ToString())) : true,
                                    IsMandatory = !string.IsNullOrEmpty(dr["IsMandatory"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsMandatory"].ToString())) : true,
                                    IsMultiLine = !string.IsNullOrEmpty(dr["IsMultiLine"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsMultiLine"].ToString())) : false,
                                    MaxLength = !string.IsNullOrEmpty(dr["MaxLength"].ToString()) ? Convert.ToInt32(dr["MaxLength"].ToString()) : 0,
                                    Title = dr["Title"].ToString(),
                                    SortOrder = Convert.ToInt32(dr["SortOrder"].ToString()),
                                    IsAnswered = !string.IsNullOrEmpty(dr["IsAnswered"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsAnswered"].ToString())) : false,
                                    ResponseID = dr["ResponseID"].ToString(),
                                    TotalScore = !string.IsNullOrEmpty(dr["TotalScore"].ToString()) ? Convert.ToDouble(dr["TotalScore"].ToString()) : 0,
                                    ScoreEarned = !string.IsNullOrEmpty(dr["ScoreEarned"].ToString()) ? Convert.ToDouble(dr["ScoreEarned"].ToString()) : 0,
                                    PercentageEarned = !string.IsNullOrEmpty(dr["PercentageEarned"].ToString()) ? Convert.ToDouble(dr["PercentageEarned"].ToString()) : 0,
                                    IPAddress = dr["IPAddress"].ToString(),
                                    Value_Text = dr["Value_Text"].ToString()
                                }).ToList();

                List<AnswerOption> ansOptionList = new List<AnswerOption>();
                ansOptionList = (from DataRow dr in ds.Tables[2].Rows
                                 select new AnswerOption()
                                 {
                                     QuestionID = !string.IsNullOrEmpty(dr["QuestionID"].ToString()) ? Convert.ToInt32(dr["QuestionID"].ToString()) : 0,
                                     IsAnswered = !string.IsNullOrEmpty(dr["IsAnswered"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsAnswered"].ToString())) : false,
                                     AnswerID = !string.IsNullOrEmpty(dr["AnswerID"].ToString()) ? Convert.ToInt32(dr["AnswerID"].ToString()) : 0,
                                     AnswerText = dr["AnswerText"].ToString(),
                                     SortOrder = !string.IsNullOrEmpty(dr["SortOrder"].ToString()) ? Convert.ToInt32(dr["SortOrder"].ToString()) : 0,
                                     IsCorrect = !string.IsNullOrEmpty(dr["IsCorrect"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsCorrect"].ToString())) : false,
                                     IsSelected = !string.IsNullOrEmpty(dr["IsSelected"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsSelected"].ToString())) : false,
                                     CorrectScore = !string.IsNullOrEmpty(dr["CorrectScore"].ToString()) ? Convert.ToDouble(dr["CorrectScore"].ToString()) : 0,
                                     InCorrectScore = !string.IsNullOrEmpty(dr["InCorrectScore"].ToString()) ? Convert.ToDouble(dr["InCorrectScore"].ToString()) : 0,
                                     Value_ID = !string.IsNullOrEmpty(dr["Value_ID"].ToString()) ? Convert.ToInt32(dr["Value_ID"].ToString()) : 0,
                                     Value_Text = dr["Value_Text"].ToString(),
                                     Value_IsCorrect = !string.IsNullOrEmpty(dr["Value_IsCorrect"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["Value_IsCorrect"].ToString())) : false,
                                     Value_CorrectScore = !string.IsNullOrEmpty(dr["Value_CorrectScore"].ToString()) ? Convert.ToDouble(dr["Value_CorrectScore"].ToString()) : 0,
                                     Value_InCorrectScore = !string.IsNullOrEmpty(dr["Value_InCorrectScore"].ToString()) ? Convert.ToDouble(dr["Value_InCorrectScore"].ToString()) : 0,
                                     FilePath = dr["FilePath"].ToString()

                                 }).ToList();
                foreach (var question in questionList)
                {
                    List<AnswerOption> ansOptions = new List<AnswerOption>();
                    question.AnswerOptions = ansOptionList.Where(p => p.QuestionID == question.QuestionID).ToList();
                    if (question.QuestionTypeID == 2 || question.QuestionTypeID == 3)
                        question.TotalScore = question.AnswerOptions.Max(p => p.CorrectScore);
                    else if (question.QuestionTypeID == 1)
                        question.TotalScore = question.AnswerOptions.Where(p => p.IsCorrect == true).Sum(p => p.CorrectScore);
                    question.ScoreEarned = question.AnswerOptions.Sum(p => p.Value_CorrectScore);
                    if (question.AnswerOptions.Count(p => p.Value_IsCorrect == true) > 0)
                        question.IsCorrect = true;
                    else
                        question.IsCorrect = false;
                }
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet UpdateContent(int compId, string userId, int topicId, int moduleId, int ContentID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.UpdateContent(compId, userId, topicId, moduleId, ContentID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet RateContent(int compId, string userId, int topicId, int moduleId, int ContentID, string Rating, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.RateContent(compId, userId, topicId, moduleId, ContentID, Rating, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet SubmitAnswers(int compId, string userId, int surveyId, JObject responseDetail)
        {
            DataSet ds = new DataSet();
            try
            {
                List<Question> questionAnswerList = new List<Question>();
                var dataSet = GetContentDetails(compId, userId, Convert.ToInt32(responseDetail["TopicID"]),
                     Convert.ToInt32(responseDetail["ModuleID"]), Convert.ToInt32(responseDetail["ContentID"]), ref questionAnswerList);

                double totalScore = 0;
                double passingPercentage = 0;
                if (Convert.ToString(responseDetail["ContentType"]) != "SURVEY")
                {
                    totalScore = Convert.ToDouble(dataSet.Tables[0].Rows[0]["TotalScore"].ToString());
                }

                if (Convert.ToString(responseDetail["ContentType"]) == "FINALQUIZ")
                {
                    passingPercentage = Convert.ToDouble(dataSet.Tables[0].Rows[0]["PassingPercent"].ToString());
                }

                double scoreEarned = 0, percentageEarned = 0;
                List<AnswerOption> lstAnswers = new List<AnswerOption>();


                // Flashcard & Final Quiz
                for (int i = 0; i < responseDetail["Questions"].Count(); i++)
                {
                    bool isCorrect = false;
                    double correctScore = 0;
                    double inCorrectScore = 0;
                    var questionid = Convert.ToInt32(responseDetail["Questions"][i]["QuestionID"]);
                    var answerIds = Convert.ToString(responseDetail["Questions"][i]["AnswerIDs"]);
                    var value_text = Convert.ToString(responseDetail["Questions"][i]["Value_Text"]);

                    var answer = questionAnswerList.SingleOrDefault(p => p.QuestionID == questionid);
                    if (answer != null)
                    {
                        // Single Select
                        if (answer.QuestionTypeID == 2 || answer.QuestionTypeID == 3)
                        {
                            var correctAnswer = answer.AnswerOptions.SingleOrDefault(p => p.AnswerID == Convert.ToInt32(answerIds));
                            if (correctAnswer != null)
                            {
                                if (correctAnswer.IsCorrect)
                                {
                                    // Correct Answer
                                    correctScore = correctAnswer.CorrectScore;
                                    isCorrect = true;
                                }
                                else
                                {
                                    // Incorrect Answer
                                    inCorrectScore = correctAnswer.InCorrectScore;
                                    isCorrect = false;
                                }
                                AnswerOption ansOption = new AnswerOption();
                                ansOption.QuestionID = questionid;
                                ansOption.Value_Text = value_text;
                                ansOption.AnswerID = correctAnswer.AnswerID;
                                ansOption.IsCorrect = isCorrect;
                                ansOption.CorrectScore = correctAnswer.CorrectScore;
                                ansOption.InCorrectScore = correctAnswer.InCorrectScore;
                                lstAnswers.Add(ansOption);
                            }
                        }
                        // Multiple Select
                        else if (answer.QuestionTypeID == 1)
                        {
                            var selectedAnswerIds = answerIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            var allAnswers = answer.AnswerOptions;
                            foreach (var answerOption in allAnswers)
                            {
                                if (selectedAnswerIds.Contains(answerOption.AnswerID.ToString()))
                                {
                                    if (answerOption.IsCorrect)
                                    {
                                        // Correct Answer
                                        correctScore += answerOption.CorrectScore;
                                        isCorrect = true;
                                    }
                                    else
                                    {
                                        // Incorrect Answer
                                        inCorrectScore += answerOption.InCorrectScore;
                                        isCorrect = false;
                                    }
                                    AnswerOption ansOption = new AnswerOption();
                                    ansOption.QuestionID = questionid;
                                    ansOption.Value_Text = value_text;
                                    ansOption.AnswerID = answerOption.AnswerID;
                                    ansOption.IsCorrect = isCorrect;
                                    ansOption.CorrectScore = answerOption.CorrectScore;
                                    ansOption.InCorrectScore = answerOption.InCorrectScore;
                                    lstAnswers.Add(ansOption);
                                }
                            }
                        }
                        else
                        {
                            AnswerOption ansOption = new AnswerOption();
                            if (answer.QuestionTypeID == 4) //File Upload
                            {
                                var userProfilePicBase64 = "";
                                var base64Image = Convert.ToString(responseDetail["Questions"][i]["Base64"]);
                                if (!string.IsNullOrEmpty(base64Image))
                                {
                                    var files = base64Image.Split(new string[] { "," }, StringSplitOptions.None);
                                    if (files.Count() == 1)
                                        userProfilePicBase64 = files[0];
                                    else
                                        userProfilePicBase64 = files[1];
                                    byte[] imageBytes = Convert.FromBase64String(userProfilePicBase64);
                                    string fileName = Guid.NewGuid() + "." + Utility.GetFileExtension(userProfilePicBase64);
                                    string filePath = HttpContext.Current.Server.MapPath("~/Files/Survey/" + fileName);
                                    File.WriteAllBytes(filePath, imageBytes);

                                    DataSet dsFile = UserBL.CreateFile("~/Files/Survey/" + fileName, filePath, false, "ProfilePic");
                                    if (dsFile.Tables.Count > 0 && dsFile.Tables[0].Rows.Count > 0)
                                    {
                                        ansOption.FileID = Convert.ToInt32(dsFile.Tables[0].Rows[0]["UniqueID"].ToString());
                                    }
                                }
                            }
                            ansOption.QuestionID = questionid;
                            ansOption.Value_Text = value_text;
                            lstAnswers.Add(ansOption);
                        }
                    }
                    scoreEarned += correctScore - inCorrectScore;
                }

                if (totalScore == 0)
                    percentageEarned = 0;
                else
                    percentageEarned = (scoreEarned / totalScore) * 100;

                var isPassed = false;
                if (percentageEarned >= passingPercentage)
                    isPassed = true;

                //Submit response in DB
                ds = TrainningDAL.SubmitResponse(compId, userId, surveyId, totalScore, scoreEarned, percentageEarned, isPassed, Utility.GetClientIPaddress());

                if (ds.Tables.Count > 0)
                {
                    // Call SubmitAnswers
                    var fileId = 0;
                    var responseId = Convert.ToInt32(ds.Tables[0].Rows[0]["ResponseID"]);
                    foreach (var answer in lstAnswers)
                    {
                        TrainningDAL.SubmitAnswers(compId, userId, surveyId, responseId, answer.QuestionID, answer.AnswerID, fileId,
                            answer.Value_Text, answer.IsCorrect, answer.CorrectScore, answer.InCorrectScore, "");
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetNotifications(int compId, string userId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetNotifications(compId, userId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetMsgNotifications(int compId, string userId, int action)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetMsgNotifications(compId, userId, action);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet CreateNotification(int compId, string userId, string title, string message, string token)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.CreateNotification(compId, userId, title, message, token);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet CheckNotificationAccess(int compId, string userId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.CheckNotificationAccess(compId, userId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet UpdateNotification(int compId, string userId, string type, string notificationIds, string token)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.UpdateNotification(compId, userId, type, notificationIds, token);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetAchievementGifts(int compId, string userId, ref List<Achievement> achievementList)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetAchievementGifts(compId, userId);

                // Table 0: Achievements
                // Table 1: Achievement Requirements
                // Table 2: Gifts  

                achievementList = new List<Achievement>();
                achievementList = (from DataRow dr in ds.Tables[0].Rows
                                   select new Achievement()
                                   {
                                       AchievementID = Convert.ToInt32(dr["AchievementID"]),
                                       AchievedPercentage = Convert.ToDouble(Convert.ToString(dr["AchievedPercentage"])),
                                       UserTitle = dr["UserTitle"].ToString(),
                                       UserMessage = dr["UserMessage"].ToString(),
                                       Title = dr["Title"].ToString(),
                                       Description = dr["Description"].ToString(),
                                   }).ToList();

                List<Requirement> requirementList = new List<Requirement>();
                requirementList = (from DataRow dr in ds.Tables[1].Rows
                                   select new Requirement()
                                   {
                                       AchievementID = Convert.ToInt32(dr["AchievementID"]),
                                       SortOrder = Convert.ToInt32(dr["SortOrder"]),
                                       Description = dr["Description"].ToString()

                                   }).ToList();
                foreach (var achievement in achievementList)
                {
                    achievement.Requirements = requirementList.Where(p => p.AchievementID == achievement.AchievementID).ToList();
                }
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet ClearAnswers(int compId, string userId, string surveyId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.ClearAnswers(compId, userId, surveyId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet IsUserOnline(int compId, string userId, int type, DateTime startDate, DateTime endDate, float totalTime)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.IsUserOnline(compId, userId, type, startDate, endDate, totalTime);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet AssignTopicsByEntity(int compID, string userId, string topicIds, string groupIds, string userIds, string removeTopic)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.AssignTopicsByEntity(compID, userId, topicIds, groupIds, userIds, removeTopic);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetUserAssignedTopic(int compID, string userId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetUserAssignedTopic(compID, userId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetTableDataByType(int compID, string type, string valueType, string valueId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetTableDataByType(compID, type, valueType, valueId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static void SendNotification(int compId, string userId, string type, string token, string subject)
        {
            var msg = "";
            var title = "";
            if (type == ConstantMessages.NotificationType.gift)
            {
                msg = "You just unlocked the " + subject + " gift";
                title = "Gift";
            }
            else if (type == ConstantMessages.NotificationType.module)
            {
                msg = "You just completed the " + subject + " module";
                title = "Module";
            }
            else if (type == ConstantMessages.NotificationType.topic)
            {
                msg = "You just completed the " + subject + " topic";
                title = "Topic";
            }

            DataSet ds = CheckNotificationAccess(compId, userId);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PushNotification"].ToString() == "1")
                {
                    CreateNotification(compId, userId, title, msg, token);
                }
                if (ds.Tables[0].Rows[0]["EmailNotification"].ToString() == "1")
                {
                    EmailHelper.GetEmailContent(Convert.ToInt32(userId), compId, type, "", "");
                }
            }
        }

        public static DataSet QuestionCRUD(int action, int compId, string userId, int contentId, int questionId, string title, int questionTypeId, bool isBox)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.QuestionCRUD(action, compId, userId, contentId, questionId, title, questionTypeId, isBox);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet ChangeTopicProperty(int compID, int userId, int topicId, int type, bool flag)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.ChangeTopicProperty(compID, userId, topicId, type, flag);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
    }
}