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


        public static DataSet CreateQuestion(int CompID, int ContentID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SurveyDAL.CreateQuestion(CompID,ContentID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet ModifyQuestion(int CompID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SurveyDAL.CreateQuestion(CompID, 0);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet DeleteQuestion(int CompID,int ContentID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SurveyDAL.DeleteQuestion(CompID,ContentID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

    }
}