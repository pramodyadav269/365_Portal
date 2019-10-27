using _365_Portal.Code.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using _365_Portal.Code.BO;

namespace _365_Portal.Code.BL
{
    public class TrainningBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "TrainningBL", methodName);
        }

        public static DataSet GetTopics(int CompID, string UserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetTopics(CompID, UserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetTopicsByUser(int CompID, string UserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetTopicsByUser(CompID, UserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetModulesByTopic(int CompID, string UserID, int TopicID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetModulesByTopic(CompID, UserID, TopicID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetContentsByModule(int CompID, string UserID, int TopicID, int ModuleID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetContentsByModule(CompID, UserID, TopicID, ModuleID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetContentDetails(int CompID, string UserID, int TopicID, int ModuleID, int ContentID, ref List<Question> questionList)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetContentDetails(CompID, UserID, TopicID, ModuleID, ContentID);

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
                                    IsMultiSelectQuestion = Convert.ToBoolean(dr["IsMultiSelectQuestion"].ToString()),
                                    QType = dr["QType"].ToString(),
                                    Type = dr["Type"].ToString(),
                                    QuestionTypeID = Convert.ToInt32(dr["QuestionTypeID"].ToString()),
                                    IsMandatory = Convert.ToBoolean(dr["IsMandatory"].ToString()),
                                    IsMultiLine = Convert.ToBoolean(dr["IsMultiLine"].ToString()),
                                    MaxLength = Convert.ToInt32(dr["MaxLength"].ToString()),
                                    Title = dr["Title"].ToString(),
                                    SortOrder = Convert.ToInt32(dr["SortOrder"].ToString()),
                                    IsAnswered = Convert.ToBoolean(dr["IsAnswered"].ToString()),
                                    ResponseID = dr["ResponseID"].ToString(),
                                    TotalScore = Convert.ToDouble(dr["TotalScore"].ToString()),
                                    ScoreEarned = Convert.ToDouble(dr["ScoreEarned"].ToString()),
                                    PercentageEarned = Convert.ToDouble(dr["PercentageEarned"].ToString()),
                                    IPAddress = dr["IPAddress"].ToString()
                                }).ToList();

                List<AnswerOption> ansOptionList = new List<AnswerOption>();
                ansOptionList = (from DataRow dr in ds.Tables[2].Rows
                                 select new AnswerOption()
                                 {
                                     QuestionID = Convert.ToInt32(dr["QuestionID"].ToString()),
                                     IsAnswered = Convert.ToBoolean(dr["IsAnswered"].ToString()),
                                     AnswerID =Convert.ToInt32( dr["AnswerID"].ToString()),
                                     AnswerText = dr["AnswerText"].ToString(),
                                     SortOrder = Convert.ToInt32(dr["SortOrder"].ToString()),
                                     IsCorrect = Convert.ToBoolean(dr["IsCorrect"].ToString()),
                                     CorrectScore = Convert.ToDouble(dr["CorrectScore"].ToString()),
                                     InCorrectScore = Convert.ToDouble(dr["InCorrectScore"].ToString()),
                                     Value_ID = Convert.ToInt32(dr["Value_ID"].ToString()),
                                     Value_Text = dr["Value_Text"].ToString(),
                                     Value_IsCorrect = Convert.ToBoolean(dr["Value_IsCorrect"].ToString()),
                                     Value_CorrectScore = Convert.ToDouble(dr["Value_CorrectScore"].ToString()),
                                     Value_InCorrectScore = Convert.ToDouble(dr["Value_InCorrectScore"].ToString()),
                                     FilePath = dr["FilePath"].ToString()
                                 }).ToList();
                foreach (var question in questionList)
                {
                    List<AnswerOption> ansOptions = new List<AnswerOption>();
                    question.AnswerOptions = ansOptionList.Where(p => p.QuestionID == question.QuestionID).ToList();
                }
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet UpdateContent(int CompID, string UserID, int TopicID, int ModuleID, int ContentID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.UpdateContent(CompID, UserID, TopicID, ModuleID, ContentID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet RateContent(int CompID, string UserID, int TopicID, int ModuleID, int ContentID, string Rating, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.RateContent(CompID, UserID, TopicID, ModuleID, ContentID, Rating, CreatedBy);
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

                double totalScore = Convert.ToDouble(dataSet.Tables[0].Rows[0]["TotalScore"].ToString());
                double scoreEarned = 0, percentageEarned = 0;
                List<AnswerOption> lstAnswers = new List<AnswerOption>();

                if (Convert.ToString(responseDetail["ContentType"]) != "SURVEY")
                {
                    // Flashcard & Final Quiz
                    for (int i = 0; i <= responseDetail["Questions"].Count(); i++)
                    {
                        bool isCorrect = false;
                        double correctScore = 0;
                        double inCorrectScore = 0;
                        var questionid = Convert.ToInt32(responseDetail["Questions"][i]["QuestionID"]);
                        var answerIds = Convert.ToString(responseDetail["Questions"][i]["AnswerIDs"]);
                        var value_text = Convert.ToString(responseDetail["Questions"][i]["Value_Text"]);

                        AnswerOption ansOption = new AnswerOption();
                        ansOption.QuestionID = questionid;
                        ansOption.Value_Text = value_text;

                        var answer = questionAnswerList.SingleOrDefault(p => p.QuestionID == questionid);
                        if (answer != null)
                        {
                            // Single Select
                            if (answer.QuestionTypeID == 2 && answer.QuestionTypeID == 3)
                            {
                                var correctAnswer = answer.AnswerOptions.SingleOrDefault(p => p.IsCorrect == true);
                                if (correctAnswer != null)
                                {
                                    if (correctAnswer.AnswerID.ToString() == answerIds)
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

                                        ansOption.AnswerID = answerOption.AnswerID;
                                        ansOption.IsCorrect = isCorrect;
                                        ansOption.CorrectScore = answerOption.CorrectScore;
                                        ansOption.InCorrectScore = answerOption.InCorrectScore;
                                        lstAnswers.Add(ansOption);
                                    }
                                }
                            }
                        }
                        scoreEarned += correctScore - inCorrectScore;
                    }
                }
                percentageEarned = (scoreEarned / totalScore) * 100;

                //Submit response in DB
                ds = TrainningDAL.SubmitResponse(compId, userId, surveyId, totalScore, scoreEarned, percentageEarned, Utility.GetClientIPaddress());

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
    }
}