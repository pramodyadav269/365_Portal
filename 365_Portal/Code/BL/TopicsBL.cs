using _365_Portal.Code.DAL;
using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BL
{

    public class TopicsBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "TopicsBL", methodName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="TopicID"></param>
        /// <param name="CompId"></param>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="SrNo"></param>
        /// <param name="MinUnlockedModules"></param>
        /// <param name="IsPublished"></param>
        /// <param name="IsActive"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public static DataSet TopicsAllAction(int Action, int TopicID, int CompId, string Title, string Description, int SrNo, int MinUnlockedModules, bool IsPublished, string CreatedBy)
        {
            DataSet data = new DataSet();
            try
            {
                data = TopicsDAL.TopicsAllAction(Action, TopicID, CompId, Title, Description, SrNo, MinUnlockedModules, IsPublished,  CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return data;
        }
    }
}