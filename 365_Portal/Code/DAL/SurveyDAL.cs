using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.DAL
{
    public class SurveyDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "SurveyDAL", methodName);
        }

        public static DataSet CreateQuestion(Int64 CompID, Int64 ContentId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spCreateQuestion";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompID", CompID);
                cmd.Parameters.AddWithValue("@UserID", ContentId);
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
        public static DataSet DeleteQuestion(Int64 CompID, Int64 ContentId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spDeleteQuestion";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompID", CompID);
                cmd.Parameters.AddWithValue("@ContentId", ContentId);
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