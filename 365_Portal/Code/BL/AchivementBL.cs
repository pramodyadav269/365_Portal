using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BL
{
    public class AchivementBL
    {

        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "AchivementBL", methodName);
        }

        public static DataSet CreateAchievement(Int64 CompId, Int64 AchievementId,  string AchivementTitle, string AchivementDescription, string LongDescription, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = AchivementDAL.CreateAchievement(CompId, AchievementId, AchivementTitle, AchivementDescription, LongDescription, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet DeleteAchievement(Int64 CompId, Int64 AchievementId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = AchivementDAL.DeleteAchievement(CompId, AchievementId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetAchievements(Int64 CompId, Int64 AchievementId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = AchivementDAL.GetAchievements(CompId, AchievementId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

    }
}