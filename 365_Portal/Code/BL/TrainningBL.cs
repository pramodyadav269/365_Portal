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

        public static DataSet GetTopics(Int32 CompID, string userId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetTopics(CompID, userId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetTopicsByUser(Int32 CompID,string userId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetTopicsByUser(CompID,userId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetModulesByTopic(Int32 CompID, string userId, Int32 TopicId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetModulesByTopic(CompID, userId, TopicId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetContentsByModule(Int32 CompID, string userId, Int32 TopicId, Int32 ModuleId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetContentsByModule(CompID, userId, TopicId,ModuleId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetContentDetails(Int32 CompID, string userId, Int32 TopicId,Int32 ModuleId, Int32 ContentId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.GetContentDetails(CompID, userId, TopicId,ModuleId,ContentId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet RateContent(Int32 CompID, string userId, Int32 TopicId, Int32 ModuleId, Int32 ContentId,string Rating, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = TrainningDAL.RateContent(CompID, userId, TopicId,ModuleId,ContentId,Rating,CreatedBy);
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