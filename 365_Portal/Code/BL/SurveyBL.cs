using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace _365_Portal.Code.BL
{
    public class SurveyBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "UserBL", methodName);
        }


        public static DataSet CreateQuestion(Int32 CompId, Int32 ContentID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SurveyDAL.CreateQuestion(CompId,ContentID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet ModifyQuestion(Int32 CompId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SurveyDAL.CreateQuestion(CompId, 0);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet DeleteQuestion(Int32 CompId,Int32 ContentId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SurveyDAL.DeleteQuestion(CompId,ContentId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

    }
}