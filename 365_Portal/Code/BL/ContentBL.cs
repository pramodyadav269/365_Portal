﻿using _365_Portal.Code.DAL;
using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace _365_Portal.Code.BL
{
    public class ContentBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "ContentBL", methodName);
        }

        #region Topics all CRUD
        public static DataSet CreateTopic(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateTopic(Convert.ToInt32(ConstantMessages.Action.INSERT), content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ModifyTopic(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateTopic(Convert.ToInt32(ConstantMessages.Action.MODIFY), content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteTopic(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.DeleteTopic(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetTopics(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.GetTopics(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet AssignTopics(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.AssignTopics(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        #endregion
        #region Modules all CRUD
        public static DataSet CreateModule(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateModule(Convert.ToInt32(ConstantMessages.Action.INSERT), content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ModifyModule(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateModule(Convert.ToInt32(ConstantMessages.Action.MODIFY), content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteModule(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.DeleteModule(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetModules(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.GetModules(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        #endregion
        #region Content all CRUD
        public static DataSet CreateContent(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateContent((int)ConstantMessages.Action.INSERT, content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ModifyContent(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateContent((int)ConstantMessages.Action.MODIFY, content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteContent(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.DeleteContent(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetContentList(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.GetContentList(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        #endregion

        #region Reordering Table
        public static DataSet ReorderContent(int compid, string userid, int type, string ids)
        {

            DataSet ds = new DataSet();

            try
            {
                ds = ContentDAL.ReorderContent(compid, userid, type, ids);

            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        #endregion

    }


}