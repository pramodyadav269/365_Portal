using _365_Portal.Code;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace _365_Portal.Code.DAL
{
    public class ContentDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "ContentDAL", methodName);
        }
        /// <summary>
        /// Data Access Layer for Topic Creation and Modification
        /// </summary>
        public static DataSet CreateTopic(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateTopic";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_TopicTitle", content.TopicTitle);
                cmd.Parameters.AddWithValue("p_TopicDescription", content.TopicDescription);
                cmd.Parameters.AddWithValue("p_TopicOverview", content.TopicOverview);
                cmd.Parameters.AddWithValue("p_IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("p_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("p_MinUnlockedModules", content.MinUnlockedModules);
                //cmd.Parameters.AddWithValue("p_MobileNo", userdetails.MobileNo);
                //cmd.Parameters.AddWithValue("p_Position", userdetails.Position);
                //cmd.Parameters.AddWithValue("p_GroupId", userdetails.GroupId);
                //cmd.Parameters.AddWithValue("p_ThemeColor", userdetails.ThemeColor);
                //cmd.Parameters.AddWithValue("p_Logo", userdetails.Logo);
                //cmd.Parameters.AddWithValue("p_ProfilePic", userdetails.ProfilePic);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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
        /// <summary>
        /// Data Access Layer for Topic Deletion
        /// </summary>
        /// <param name="contentBO"></param>
        /// <returns></returns>
        public static DataSet DeleteTopic(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spDeleteTopic";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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

        /// <summary>
        /// Data Access Layer for Getting Topic 
        /// </summary>
        public static DataSet GetTopics(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spGetTopics";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicName", content.TopicName);
                cmd.Parameters.AddWithValue("p_IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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


        /// <summary>
        /// Data Access Layer for Assigning Topic 
        /// </summary>
        public static DataSet AssignTopics(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spAssignTopics";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_UserIDs", content.UserIDs);
                cmd.Parameters.AddWithValue("p_TopicIDs", content.TopicIDs);
                cmd.Parameters.AddWithValue("p_IsActive", content.IsActive);
                cmd.Parameters.AddWithValue("p_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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

        /// <summary>
        /// Data Access Layer for Module Creation and Modification
        /// </summary>
        public static DataSet CreateModule(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateModule";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_ModuleTitle", content.ModuleTitle);
                cmd.Parameters.AddWithValue("p_ModuleDescription", content.ModuleDescription);
                cmd.Parameters.AddWithValue("p_ModuleOverview", content.ModuleOverview);
                cmd.Parameters.AddWithValue("p_IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("p_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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

        /// <summary>
        /// Data Access Layer for Module Deletion
        /// </summary>
        /// <param name="contentBO"></param>
        /// <returns></returns>
        public static DataSet DeleteModule(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);
            try
            {
                conn.Open();
                string stm = "spDeleteTopic";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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

        /// <summary>
        /// Data Access Layer for Getting Module 
        /// </summary>
        public static DataSet GetModules(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spGetModules";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_ModuleTitle", content.ModuleTitle);
                cmd.Parameters.AddWithValue("p_IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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

        /// <summary>
        /// Data Access Layer for Content Creation and Modification
        /// </summary>
        public static DataSet CreateContent(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateContent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_ContentID", content.ContentID);
                cmd.Parameters.AddWithValue("p_ContentTypeID", content.ContentTypeID);
                cmd.Parameters.AddWithValue("p_DocType", content.DocType);
                cmd.Parameters.AddWithValue("p_ContentTitle", content.ContentTitle);
                cmd.Parameters.AddWithValue("p_ContentDescription", content.ContentDescription);
                cmd.Parameters.AddWithValue("p_FilePath", content.FilePath);
                cmd.Parameters.AddWithValue("p_IsGift", content.IsGift);
                cmd.Parameters.AddWithValue("p_SkipFlashcard", content.SkipFlashcard);
                cmd.Parameters.AddWithValue("p_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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

        /// <summary>
        /// Data Access Layer for Content Deletion
        /// </summary>
        /// <param name="contentBO"></param>

        public static DataSet DeleteContent(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);
            try
            {
                conn.Open();
                string stm = "spDeleteContent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_ContentID", content.ContentID);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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

        /// <summary>
        /// Data Access Layer for GetContentList
        /// </summary>
        /// <param name="contentBO"></param>

        public static DataSet GetContentList(ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);
            try
            {
                conn.Open();
                string stm = "spGetContentList";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_ContentType", content.ContentType);
                cmd.Parameters.AddWithValue("p_ContentTitle", content.ContentTitle);
                cmd.Parameters.AddWithValue("p_CreatedBy", content.CreatedBy);
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