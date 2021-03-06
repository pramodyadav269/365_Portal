﻿using _365_Portal.Code;
using _365_Portal.Models;
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
        #region TOPICS
        /// <summary>
        /// Data Access Layer for Topic Creation and Modification
        /// </summary>
        public static DataSet CreateTopic(int Action, ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateTopic";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", Action);
                //if (!string.IsNullOrEmpty(content.UserIDs))
                //{
                //    cmd.Parameters.AddWithValue("p_UserID", content.UserIDs);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("p_UserID", DBNull.Value);
                //}
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                if (!string.IsNullOrEmpty(content.TopicTitle))
                {
                    cmd.Parameters.AddWithValue("p_Title", content.TopicTitle.Trim().ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_Title", DBNull.Value);
                }
                if (!string.IsNullOrEmpty(content.TopicDescription))
                {
                    cmd.Parameters.AddWithValue("p_Description", content.TopicDescription);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_Description", DBNull.Value);
                }
                //if (!string.IsNullOrEmpty(content.TopicOverview))
                //{
                //    cmd.Parameters.AddWithValue("p_TopicOverview", content.TopicOverview);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("p_TopicOverview", DBNull.Value);
                //}
                if (!string.IsNullOrEmpty(content.SrNo.ToString()))
                {
                    cmd.Parameters.AddWithValue("p_SrNo", content.SrNo);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_SrNo", DBNull.Value);
                }
                if (!string.IsNullOrEmpty(content.MinUnlockedModules))
                {
                    cmd.Parameters.AddWithValue("p_MinUnlockedModules", content.MinUnlockedModules);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_MinUnlockedModules", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("p_IsPublished", Convert.ToInt32(content.IsPublished));
                cmd.Parameters.AddWithValue("p_IsActive", Convert.ToInt32(content.IsActive));
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

                conn.Open(); //spDeleteTopic
                string stm = "spDeleteTopic";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_IsActive", Convert.ToInt32(content.IsActive));
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

                conn.Open(); //spGetTopics
                string stm = "spGetTopic";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
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
                cmd.Parameters.AddWithValue("p_IsActive", Convert.ToInt32(content.IsActive));
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
        #endregion

        #region Modules
        /// <summary>
        /// Data Access Layer for Module Creation and Modification
        /// </summary>
        public static DataSet CreateModule(int Action, ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateModule";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", Action);
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);

                //if (!string.IsNullOrEmpty(content.UserIDs))
                //{
                //    cmd.Parameters.AddWithValue("p_UserID", content.UserIDs);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("p_UserID", DBNull.Value);
                //}
                if (!string.IsNullOrEmpty(content.ModuleTitle))
                {
                    cmd.Parameters.AddWithValue("p_Title", content.ModuleTitle.Trim().ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_Title", DBNull.Value);
                }
                if (!string.IsNullOrEmpty(content.ModuleOverview))
                {
                    cmd.Parameters.AddWithValue("p_Overview", content.ModuleOverview);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_Overview", DBNull.Value);
                }
                if (!string.IsNullOrEmpty(content.ModuleDescription))
                {
                    cmd.Parameters.AddWithValue("p_Description", content.ModuleDescription);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_Description", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("p_SrNo", content.SrNo);
                cmd.Parameters.AddWithValue("p_IsPublished", Convert.ToInt32(content.IsPublished));
                cmd.Parameters.AddWithValue("p_IsActive", Convert.ToInt32(content.IsActive));
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
                string stm = "spDeleteModule";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_IsActive", Convert.ToInt32(content.IsActive));
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
                string stm = "spGetModule";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_IsActive", Convert.ToInt32(content.IsActive));
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
        #endregion

        #region Contents
        /// <summary>
        /// Data Access Layer for Content Creation and Modification
        /// </summary>
        public static DataSet CreateContent(int Action, ContentBO content)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateContent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", Action);
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_ContentID", content.ContentID);
                cmd.Parameters.AddWithValue("p_TypeID", content.ContentTypeID);
                cmd.Parameters.AddWithValue("p_DocType", content.DocType);
                cmd.Parameters.AddWithValue("p_ContentFileID", content.ContentFileID);
                cmd.Parameters.AddWithValue("p_Title", content.ContentTitle.Trim());
                cmd.Parameters.AddWithValue("p_FlashcardHighlights", content.FlashcardHighlights);
                cmd.Parameters.AddWithValue("p_Description", content.ContentDescription);
                cmd.Parameters.AddWithValue("p_Overview", content.ModuleOverview);//Overview column
                cmd.Parameters.AddWithValue("p_FlashcardTitle", content.FlashcardTitle);
                cmd.Parameters.AddWithValue("p_SkipFlashcards", Convert.ToInt32(content.SkipFlashcard));
                cmd.Parameters.AddWithValue("p_IsGift", Convert.ToInt32(content.IsGift));
                cmd.Parameters.AddWithValue("p_IsPublished", Convert.ToInt32(content.IsPublished));
                cmd.Parameters.AddWithValue("p_IsActive", Convert.ToInt32(content.IsActive));
                cmd.Parameters.AddWithValue("p_TotalScore", content.TotalScore);
                cmd.Parameters.AddWithValue("p_PassingPercent", content.PassingPercent);
                cmd.Parameters.AddWithValue("p_PassingScore", content.PassingScore);
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
                cmd.Parameters.AddWithValue("p_ContentID", content.ContentID);
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
                string stm = "spGetContents";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", content.CompID);
                cmd.Parameters.AddWithValue("p_TopicID", content.TopicID);
                cmd.Parameters.AddWithValue("p_ModuleID", content.ModuleID);
                cmd.Parameters.AddWithValue("p_ContentID", content.ContentID);
                cmd.Parameters.AddWithValue("p_TypeID", content.ContentTypeID);
                cmd.Parameters.AddWithValue("p_IsGift", content.IsGift);
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
        #endregion



        #region ReOrdering Table
        /// <summary>
        /// Data Access Layer for making changes in Tables data order
        /// </summary>
        public static DataSet ReorderContent(int compid,string userid,int type,string ids)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spReorderContent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("p_compId",compid);
                cmd.Parameters.AddWithValue("p_userId", userid);
                cmd.Parameters.AddWithValue("p_type", type);
                cmd.Parameters.AddWithValue("p_ids", ids);

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
        #endregion
    }
}