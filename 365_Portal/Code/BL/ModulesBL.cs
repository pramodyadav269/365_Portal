using _365_Portal.Code.DAL;
using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BL
{
    public class ModulesBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "ModulesBL", methodName);
        }

        public static DataSet ModulesAllAction(int Action, int ModuleID, int TopicID, int CompId, string Title, string Overview, string Description, int SrNo, bool IsPublished, string CreatedBy)
        {
            DataSet data = new DataSet();
            try
            {

                data = ModuleDAL.ModulessAllAction(Action, ModuleID, TopicID, CompId, Title, Overview, Description, SrNo, IsPublished, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return data;
        }
    }
}