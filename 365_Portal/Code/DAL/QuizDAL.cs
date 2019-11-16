using _365_Portal.Code;
using _365_Portal.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace _365_Portal.Code.DAL
{
    public class QuizDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "QuizDAL", methodName);
        }

        public static DataSet SaveSurvey(int compId, string userId, int contentId, string title, string description, string overview, bool isPublished, double totalScore, double passingScore, double passingPercent)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string sp = "";
                MySqlCommand cmd = new MySqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", compId);
                cmd.Parameters.AddWithValue("p_ContentID", contentId);
                cmd.Parameters.AddWithValue("p_Title", title);
                cmd.Parameters.AddWithValue("p_Description", description);
                cmd.Parameters.AddWithValue("p_Overview", overview);
                cmd.Parameters.AddWithValue("p_IsPublished", isPublished);
                cmd.Parameters.AddWithValue("p_TotalScore", totalScore);
                cmd.Parameters.AddWithValue("p_PassingScore", passingScore);
                cmd.Parameters.AddWithValue("p_passingPercent", passingPercent);
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
        public static DataSet ManageQuestion(int compId, string userId, int questionId, int contentId, bool isMandatory, bool isMultiline, string title, int qType, bool isBox, int type, int action)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string sp = "";
                MySqlCommand cmd = new MySqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", action);
                cmd.Parameters.AddWithValue("p_CompID", compId);
                cmd.Parameters.AddWithValue("p_ContentID", contentId);
                cmd.Parameters.AddWithValue("p_Title", title);
                cmd.Parameters.AddWithValue("p_QuestionId", questionId);
                cmd.Parameters.AddWithValue("p_IsMandatory", isMandatory);
                cmd.Parameters.AddWithValue("p_IsMultiline", isMultiline);
                cmd.Parameters.AddWithValue("p_QType", qType);
                cmd.Parameters.AddWithValue("p_IsBox", isBox);
                cmd.Parameters.AddWithValue("p_Type", type);
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

        public static DataSet ManageAnsOptions(int compId, string userId, int type, int contentId, int questionId, int answerId, string title, bool isCorrect, double score, int action)
        {


            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string sp = "";
                MySqlCommand cmd = new MySqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", action);
                cmd.Parameters.AddWithValue("p_CompID", compId);
                cmd.Parameters.AddWithValue("p_Type", type);
                cmd.Parameters.AddWithValue("p_ContentID", contentId);
                cmd.Parameters.AddWithValue("p_QuestionId", questionId);
                cmd.Parameters.AddWithValue("p_AnswerId", answerId);
                cmd.Parameters.AddWithValue("p_Title", title);
                cmd.Parameters.AddWithValue("p_IsCorrect", isCorrect);
                cmd.Parameters.AddWithValue("p_Score", score);
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
        public static DataSet ManageFlashcardIntro(int compId, string userId, int introId, string comments, int contentId, int action)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string sp = "";
                MySqlCommand cmd = new MySqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", action);
                cmd.Parameters.AddWithValue("p_CompID", compId);
                cmd.Parameters.AddWithValue("p_IntroID", introId);
                cmd.Parameters.AddWithValue("p_ContentID", contentId);
                cmd.Parameters.AddWithValue("p_Comments", comments);
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


        public static DataSet ManageFlashcardSlides(int compId, string userId, int flashcardId, string title, int contentId, string description, int action)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string sp = "";
                MySqlCommand cmd = new MySqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", action);
                cmd.Parameters.AddWithValue("p_CompID", compId);
                cmd.Parameters.AddWithValue("p_FlashCardID", flashcardId);
                cmd.Parameters.AddWithValue("p_Title", title);
                cmd.Parameters.AddWithValue("p_ContentID", contentId);
                cmd.Parameters.AddWithValue("p_Description", description);
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
    }
}