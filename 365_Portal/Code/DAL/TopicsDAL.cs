using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.DAL
{
    public class TopicsDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "TopicsDAL", methodName);
        }
        public static DataSet TopicsAllAction(int Action, int TopicID, int CompId, string Title, string Description, int SrNo, int MinUnlockedModules, bool IsPublished, string CreatedBy)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "sp_Modules_All_Actionable";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Action", Action);
                cmd.Parameters.AddWithValue("p_TopicID", TopicID);
                cmd.Parameters.AddWithValue("p_CompID", CompId);
                cmd.Parameters.AddWithValue("p_Title", Title);
                cmd.Parameters.AddWithValue("p_Description", Description);
                cmd.Parameters.AddWithValue("p_SrNo", SrNo);
                cmd.Parameters.AddWithValue("p_MinUnlockedModules", MinUnlockedModules);
                cmd.Parameters.AddWithValue("p_IsPublished", IsPublished);
                cmd.Parameters.AddWithValue("p_CreatedBy", CreatedBy);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                conn.Close();
            }

            return ds;
        }


    }
}