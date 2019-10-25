using _365_Portal.Code.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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

        public static DataSet GetContentDetails(int CompID, string UserID, int TopicID, int ModuleID, int ContentID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetContentDetails(CompID, UserID, TopicID, ModuleID, ContentID);
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
                int totalScore = 10; int scoreEarned = 20; int percentageEarned = 15;
                ds = TrainningDAL.SubmitResponse(compId, userId, surveyId, totalScore, scoreEarned, percentageEarned, Utility.GetClientIPaddress());

                if (ds.Tables.Count > 0)
                {
                    // Call SubmitAnswers
                    for (int i = 0; i <= responseDetail["Questions"].Count(); i++)
                    {
                        var responseId = Convert.ToInt32(ds.Tables[0].Rows[0]["ResponseID"]);
                        var questionid = Convert.ToInt32(responseDetail["Questions"][i]["QuestionID"]);
                        var answerIds = Convert.ToString(responseDetail["Questions"][i]["AnswerIDs"]);
                        var fileId = 0;
                        var value_text = Convert.ToString(responseDetail["Questions"][i]["Value_Text"]);
                        var isCorrect = true;
                        float correctScore = 1;
                        float inCorrectScore = 2;
                        TrainningDAL.SubmitAnswers(compId, userId, surveyId, responseId, questionid, answerIds, fileId, value_text, isCorrect, correctScore, inCorrectScore, "");
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