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

        public static DataSet GetTopics(int CompID, string UserID)
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

        public static DataSet GetTopicsByUser(int CompID, string UserID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetTopicsByUser";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", CompID);
                cmd.Parameters.AddWithValue("p_UserID", UserID);
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

        public static DataSet GetModulesByTopic(int CompID, string UserID, int TopicID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetModulesByTopic";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", CompID);
                cmd.Parameters.AddWithValue("p_UserID", UserID);
                cmd.Parameters.AddWithValue("p_TopicID", TopicID);
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

        public static DataSet GetContentsByModule(int CompID, string UserID, int TopicID, int ModuleID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetContentsByModule";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", CompID);
                cmd.Parameters.AddWithValue("p_UserID", UserID);
                cmd.Parameters.AddWithValue("p_TopicI", TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", ModuleID);
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

        public static DataSet GetContentDetails(int CompID, string UserID, int TopicID, int ModuleID, int ContentID)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetContentDetails";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", CompID);
                cmd.Parameters.AddWithValue("p_UserID", UserID);
                cmd.Parameters.AddWithValue("p_TopicID", TopicID);
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

        public static DataSet RateContent(int CompID, string UserID, int TopicID, int ModuleID, int ContentID, string Rating, string CreatedBy)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spRateContent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", CompID);
                cmd.Parameters.AddWithValue("p_UserID", UserID);
                cmd.Parameters.AddWithValue("p_TopicID", TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", ModuleID);
                cmd.Parameters.AddWithValue("p_ContentID", ContentID);
                cmd.Parameters.AddWithValue("p_Rating", Rating);
                cmd.Parameters.AddWithValue("p_ContentId", CreatedBy);
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

        public static DataSet SubmitResponse(int compId, string userId, int surveyId, int totalScore, int scoreEarned, int percentageEarned, string IPAddress)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spSubmitResponse";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", compId);
                cmd.Parameters.AddWithValue("p_UserID", userId);
                cmd.Parameters.AddWithValue("p_SurveyID", surveyId);
                cmd.Parameters.AddWithValue("p_TotalScore", totalScore);
                cmd.Parameters.AddWithValue("p_ScoreEarned", scoreEarned);
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

        public static DataSet SubmitAnswers(int compId, int userId, int surveyId, int responseId, int questionId,string answerIds, 
            int fileId, string valueText,bool isCorrect,float correctScore, float incorrectScore, string filePath)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spSubmitAnswers";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", compId);
                cmd.Parameters.AddWithValue("p_UserID", userId);
                cmd.Parameters.AddWithValue("p_SurveyID", surveyId);
                cmd.Parameters.AddWithValue("p_ResponseID", responseId);
                cmd.Parameters.AddWithValue("p_QuestionID", questionId);
                cmd.Parameters.AddWithValue("p_AnswerIDs", answerIds);
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
    }
}