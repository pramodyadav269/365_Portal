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
                cmd.Parameters.AddWithValue("_CompId", content.CompId);
                cmd.Parameters.AddWithValue("_TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("_TopicTitle", content.TopicTitle);
                cmd.Parameters.AddWithValue("_TopicDescription", content.TopicDescription);
                cmd.Parameters.AddWithValue("_TopicOverview", content.TopicOverview);
                cmd.Parameters.AddWithValue("_IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("_MinUnlockedModules", content.MinUnlockedModules);
                //cmd.Parameters.AddWithValue("@_MobileNo", userdetails.MobileNo);
                //cmd.Parameters.AddWithValue("@_Position", userdetails.Position);
                //cmd.Parameters.AddWithValue("@_GroupId", userdetails.GroupId);
                //cmd.Parameters.AddWithValue("@_ThemeColor", userdetails.ThemeColor);
                //cmd.Parameters.AddWithValue("@_Logo", userdetails.Logo);
                //cmd.Parameters.AddWithValue("@_ProfilePic", userdetails.ProfilePic);
                cmd.Parameters.AddWithValue("_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", content.CompId);
                cmd.Parameters.AddWithValue("_TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", content.CompId);
                cmd.Parameters.AddWithValue("_TopicName", content.TopicName);
                cmd.Parameters.AddWithValue("_IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@_CompId", content.CompId);
                cmd.Parameters.AddWithValue("@_UserIds", content.UserIds);
                cmd.Parameters.AddWithValue("@_TopicIds", content.TopicIds);
                cmd.Parameters.AddWithValue("@_IsActive", content.IsActive);
                cmd.Parameters.AddWithValue("@_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("@_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("_CompId", content.CompId);
                cmd.Parameters.AddWithValue("_TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("_ModuleTitle", content.ModuleTitle);
                cmd.Parameters.AddWithValue("_ModuleDescription", content.ModuleDescription);
                cmd.Parameters.AddWithValue("_ModuleOverview", content.ModuleOverview);
                cmd.Parameters.AddWithValue("_IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@_CompId", content.CompId);
                cmd.Parameters.AddWithValue("@_TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@_ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@_CompId", content.CompId);
                cmd.Parameters.AddWithValue("@_TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@_ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@_ModuleTitle", content.ModuleTitle);
                cmd.Parameters.AddWithValue("@_IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("@_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@_CompId", content.CompId);
                cmd.Parameters.AddWithValue("@_TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@_ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@_ContentId", content.ContentId);
                cmd.Parameters.AddWithValue("@_ContentTypeId", content.ContentTypeId);
                cmd.Parameters.AddWithValue("@_DocType", content.DocType);
                cmd.Parameters.AddWithValue("@_ContentTitle", content.ContentTitle);
                cmd.Parameters.AddWithValue("@_ContentDescription", content.ContentDescription);
                cmd.Parameters.AddWithValue("@_FilePath", content.FilePath);
                cmd.Parameters.AddWithValue("@_IsGift", content.IsGift);
                cmd.Parameters.AddWithValue("@_SkipFlashcard", content.SkipFlashcard);
                cmd.Parameters.AddWithValue("@_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("@_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@_CompId", content.CompId);
                cmd.Parameters.AddWithValue("@_TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@_ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@_ContentId", content.ContentId);
                cmd.Parameters.AddWithValue("@_CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@_CompId", content.CompId);
                cmd.Parameters.AddWithValue("@_TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@_ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@_ContentType", content.ContentType);
                cmd.Parameters.AddWithValue("@_ContentTitle", content.ContentTitle);
                cmd.Parameters.AddWithValue("@_CreatedBy", content.CreatedBy);
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