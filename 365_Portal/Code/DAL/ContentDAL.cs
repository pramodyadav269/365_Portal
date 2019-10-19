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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@TopicTitle", content.TopicTitle);
                cmd.Parameters.AddWithValue("@TopicDescription", content.TopicDescription);
                cmd.Parameters.AddWithValue("@TopicOverview", content.TopicOverview);
                cmd.Parameters.AddWithValue("@IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("@SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("@MinUnlockedModules", content.MinUnlockedModules);
                //cmd.Parameters.AddWithValue("@MobileNo", userdetails.MobileNo);
                //cmd.Parameters.AddWithValue("@Position", userdetails.Position);
                //cmd.Parameters.AddWithValue("@GroupId", userdetails.GroupId);
                //cmd.Parameters.AddWithValue("@ThemeColor", userdetails.ThemeColor);
                //cmd.Parameters.AddWithValue("@Logo", userdetails.Logo);
                //cmd.Parameters.AddWithValue("@ProfilePic", userdetails.ProfilePic);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicName", content.TopicName);
                cmd.Parameters.AddWithValue("@IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@UserIds", content.UserIds);
                cmd.Parameters.AddWithValue("@TopicIds", content.TopicIds);
                cmd.Parameters.AddWithValue("@IsActive", content.IsActive);
                cmd.Parameters.AddWithValue("@SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@ModuleTitle", content.ModuleTitle);
                cmd.Parameters.AddWithValue("@ModuleDescription", content.ModuleDescription);
                cmd.Parameters.AddWithValue("@ModuleOverview", content.ModuleOverview);
                cmd.Parameters.AddWithValue("@IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("@SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@ModuleTitle", content.ModuleTitle);
                cmd.Parameters.AddWithValue("@IsPublished", content.IsPublished);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@ContentId", content.ContentId);
                cmd.Parameters.AddWithValue("@ContentTypeId", content.ContentTypeId);
                cmd.Parameters.AddWithValue("@DocType", content.DocType);
                cmd.Parameters.AddWithValue("@ContentTitle", content.ContentTitle);
                cmd.Parameters.AddWithValue("@ContentDescription", content.ContentDescription);
                cmd.Parameters.AddWithValue("@FilePath", content.FilePath);
                cmd.Parameters.AddWithValue("@IsGift", content.IsGift);
                cmd.Parameters.AddWithValue("@SkipFlashcard", content.SkipFlashcard);
                cmd.Parameters.AddWithValue("@SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@ContentId", content.ContentId);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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
                cmd.Parameters.AddWithValue("@CompId", content.CompId);
                cmd.Parameters.AddWithValue("@TopicId", content.TopicId);
                cmd.Parameters.AddWithValue("@ModuleId", content.ModuleId);
                cmd.Parameters.AddWithValue("@ContentType", content.ContentType);
                cmd.Parameters.AddWithValue("@ContentTitle", content.ContentTitle);
                cmd.Parameters.AddWithValue("@CreatedBy", content.CreatedBy);
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