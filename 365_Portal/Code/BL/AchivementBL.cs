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

        public static DataSet CreateAchievement(int CompID, int AchievementID,  string AchivementTitle, string AchivementDescription, string LongDescription, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = AchivementDAL.CreateAchievement(CompID, AchievementID, AchivementTitle, AchivementDescription, LongDescription, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet DeleteAchievement(int CompID, int AchievementID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = AchivementDAL.DeleteAchievement(CompID, AchievementID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetAchievements(int CompID, int AchievementID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = AchivementDAL.GetAchievements(CompID, AchievementID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

    }
}