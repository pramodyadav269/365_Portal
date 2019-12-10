using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace _365_Portal.Code.DAL
{
    public class TrainningDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "TrainningDAL", methodName);
        }

        public static DataSet GetTopics(int compID, string userId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "SELECT * FROM tblTopics";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetTopicsByUser(int compID, string userId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetTopicsByUser";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetModulesByTopic(int compID, string userId, int topicId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetModulesByTopic";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_topicId", topicId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetContentsByModule(int compID, string userId, int topicId, int ModuleID, bool isGift)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetContentsByModule";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_topicId", topicId);
                cmd.Parameters.AddWithValue("p_ModuleID", ModuleID);
                cmd.Parameters.AddWithValue("p_IsGift", isGift);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetContentDetails(int compID, string userId, int topicId, int ModuleID, int ContentID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetContentDetails";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_topicId", topicId);
                cmd.Parameters.AddWithValue("p_ModuleID", ModuleID);
                cmd.Parameters.AddWithValue("p_ContentID", ContentID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet UpdateContent(int compID, string userId, int topicId, int ModuleID, int ContentID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spUpdateContent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_topicId", topicId);
                cmd.Parameters.AddWithValue("p_ModuleID", ModuleID);
                cmd.Parameters.AddWithValue("p_ContentID", ContentID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet RateContent(int compID, string userId, int topicId, int ModuleID, int ContentID, string Rating, string CreatedBy)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spRateContent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_topicId", topicId);
                cmd.Parameters.AddWithValue("p_ModuleID", ModuleID);
                cmd.Parameters.AddWithValue("p_ContentID", ContentID);
                cmd.Parameters.AddWithValue("p_Rating", Rating);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet SubmitResponse(int compID, string userId, int surveyId, double totalScore, double scoreEarned, double percentageEarned, bool isPassed, string IPAddress)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spSubmitResponse";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_SurveyID", surveyId);
                cmd.Parameters.AddWithValue("p_TotalScore", totalScore);
                cmd.Parameters.AddWithValue("p_ScoreEarned", scoreEarned);
                cmd.Parameters.AddWithValue("p_IsPassed", isPassed);
                cmd.Parameters.AddWithValue("p_PercentageEarned", percentageEarned);
                cmd.Parameters.AddWithValue("p_IPAddress", IPAddress);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet SubmitAnswers(int compID, string userId, int surveyId, int responseId, int questionId, int answerId,
            int fileId, string valueText, bool isCorrect, double correctScore, double incorrectScore, string filePath)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spSubmitAnswers";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_SurveyID", surveyId);
                cmd.Parameters.AddWithValue("p_ResponseID", responseId);
                cmd.Parameters.AddWithValue("p_QuestionID", questionId);
                cmd.Parameters.AddWithValue("p_AnswerID", answerId);
                cmd.Parameters.AddWithValue("p_FileID", fileId);
                cmd.Parameters.AddWithValue("p_Value_Text", valueText);
                cmd.Parameters.AddWithValue("p_IsCorrect", isCorrect);
                cmd.Parameters.AddWithValue("p_CorrectScore", correctScore);
                cmd.Parameters.AddWithValue("p_InCorrectScore", incorrectScore);
                cmd.Parameters.AddWithValue("p_FilePath", filePath);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetNotifications(int compID, string userId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetNotifications";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet UpdateNotification(int compID, string userId, string type, string notificationIds, string token)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spUpdateNotification";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compid", compID);
                cmd.Parameters.AddWithValue("p_userid", userId);
                cmd.Parameters.AddWithValue("p_type", type);
                cmd.Parameters.AddWithValue("p_notficationIds", notificationIds);
                cmd.Parameters.AddWithValue("p_token", token);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet CreateNotification(int compID, string userId, string title, string message, string token)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spCreateNotification";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_title", title);
                cmd.Parameters.AddWithValue("p_message", message);
                cmd.Parameters.AddWithValue("p_token", token);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetAchievementGifts(int compID, string userId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetAchievementNGifts";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet ClearAnswers(int compID, string userId, string surveyId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spClearAnswers";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_SurveyID", surveyId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet IsUserOnline(int compID, string userId, int type, DateTime startDate, DateTime endDate, float totalTime)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spUserOnline";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_StartDate", startDate);
                cmd.Parameters.AddWithValue("p_EndDate", endDate);
                cmd.Parameters.AddWithValue("p_TotalTime", totalTime);
                cmd.Parameters.AddWithValue("p_Type", type);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet AssignTopicsByEntity(int compID, string userId, string topicIds, string groupIds, string userIds,string removeTopic)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spAssignTopicsByEntity";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compId", compID);
                cmd.Parameters.AddWithValue("p_userId", userId);
                cmd.Parameters.AddWithValue("p_TopicIds", topicIds);
                cmd.Parameters.AddWithValue("p_groupIds", groupIds);
                cmd.Parameters.AddWithValue("p_userIds", userIds);
                cmd.Parameters.AddWithValue("p_removetopic", removeTopic);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet GetTableDataByType(int compID, string type, string valueType, string valueId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetTableDataByType";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compId", compID);
                cmd.Parameters.AddWithValue("p_type", type);
                cmd.Parameters.AddWithValue("p_valueType", valueType);
                cmd.Parameters.AddWithValue("p_valueId", valueId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataSet QuestionCRUD(int action, int compId, string userId, int contentId, int questionId, string title, int questionTypeId, bool isBox)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);
            try
            {
                conn.Open();
                string stm = "spClearAnswers";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", action);
                cmd.Parameters.AddWithValue("p_CompID", compId);
                cmd.Parameters.AddWithValue("p_ContentID", contentId);
                cmd.Parameters.AddWithValue("p_QuestionID", questionId);
                cmd.Parameters.AddWithValue("p_QuestionTypeID", questionTypeId);
                cmd.Parameters.AddWithValue("p_IsMandatory", true);
                cmd.Parameters.AddWithValue("p_IsBox", isBox);
                cmd.Parameters.AddWithValue("p_IsMultiline", true);
                cmd.Parameters.AddWithValue("p_MaxLength", 150);
                cmd.Parameters.AddWithValue("p_Title", title);
                cmd.Parameters.AddWithValue("p_CreatedBy", userId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        //public static DataSet QuestionCRUD(int action, int compId, string userId, int contentId, int questionId, string title, int questionTypeId, bool isBox)
        //{
        //    DataSet ds = new DataSet();
        //    MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);
        //    try
        //    {
        //        conn.Open();
        //        string stm = "spClearAnswers";
        //        MySqlCommand cmd = new MySqlCommand(stm, conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("p_Action", action);
        //        cmd.Parameters.AddWithValue("p_CompID", compId);
        //        cmd.Parameters.AddWithValue("p_AnswerID", contentId);
        //        cmd.Parameters.AddWithValue("p_QuestionID", questionId);
        //        cmd.Parameters.AddWithValue("p_AnswerText", questionTypeId);
        //        cmd.Parameters.AddWithValue("p_IsCorrect", true);
        //        cmd.Parameters.AddWithValue("p_CorrectScore", isBox);
        //        cmd.Parameters.AddWithValue("p_InCorrectScore", true);
        //        cmd.Parameters.AddWithValue("p_SrNo", 1);
        //        cmd.Parameters.AddWithValue("p_SurveyId", 1);
        //        cmd.Parameters.AddWithValue("p_CreatedBy", userId);

        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        da.Fill(ds, "Data");
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return ds;
        //}
    }
}