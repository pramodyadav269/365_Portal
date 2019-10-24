using _365_Portal.Code.DAL;
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

        public static DataSet GetTopicsByUser(int CompID,string UserID)
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
                ds = TrainningDAL.GetContentsByModule(CompID, UserID, TopicID,ModuleID);
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
                ds = TrainningDAL.GetContentDetails(CompID, UserID, TopicID,ModuleID,ContentID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet RateContent(int CompID, string UserID, int TopicID, int ModuleID, int ContentID,string Rating, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.RateContent(CompID, UserID, TopicID, ModuleID, ContentID, Rating,CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }


        public static DataSet spSubmitAnswers()
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.spSubmitAnswers();
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
    }
}