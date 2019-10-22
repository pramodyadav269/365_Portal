using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace _365_Portal.Code.DAL
{
    public class AchivementDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "ContentDAL", methodName);
        }


        public static DataSet CreateAchievement(Int32 CompId, Int32 AchievementId,  string AchivementTitle, string AchivementDescription, string LongDescription, string CreatedBy)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateAchievement";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompId);
                cmd.Parameters.AddWithValue("p_AchievementId", AchievementId);
                cmd.Parameters.AddWithValue("p_AchivementTitle", AchivementTitle);
                cmd.Parameters.AddWithValue("p_AchivementDescription", AchivementDescription);
                cmd.Parameters.AddWithValue("p_LongDescription", LongDescription);
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

        public static DataSet DeleteAchievement(Int32 CompId, Int32 AchievementId)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spDeleteAchievement";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompId);
                cmd.Parameters.AddWithValue("p_ContentId", AchievementId);
                //cmd.Parameters.AddWithValue("p_CreatedBy", CreatedBy);
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

        public static DataSet GetAchievements(Int32 CompId, Int32 AchievementId)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spGetAchievements";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompId", CompId);
                cmd.Parameters.AddWithValue("p_AchievementId", AchievementId);
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