using _365_Portal.Code.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using _365_Portal.Code.BO;
using System.IO;
using _365_Portal.Models;


namespace _365_Portal.Code.BL
{
    public class DiscussionBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "DiscussionBL", methodName);
        }

        public static DataSet SendMessage(int compID, string userId, int moduleId, string message, string ipaddress)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = DiscussionDAL.SendMessage(compID, userId, moduleId, message, ipaddress);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetDiscussionChatByModule(int compID, int userId, int moduleId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = DiscussionDAL.GetDiscussionChatByModule(compID, userId, moduleId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
    }
}