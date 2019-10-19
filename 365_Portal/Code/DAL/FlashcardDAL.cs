using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace _365_Portal.Code.DAL
{
    public class FlashcardDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "FlashcardDAL", methodName);
        }

        public static DataSet CreateFlashcard(Int64 CompId, Int64 ContentId, Int64 FlashcardId, string FlashcardTitle, string FlashcardDescription, Int64 SrNo, string CreatedBy)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateFlashcard";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompId", CompId);
                cmd.Parameters.AddWithValue("@ContentId", ContentId);
                cmd.Parameters.AddWithValue("@FlashcardId", FlashcardId);
                cmd.Parameters.AddWithValue("@FlashcardTitle", FlashcardTitle);
                cmd.Parameters.AddWithValue("@FlashcardDescription", FlashcardDescription);
                cmd.Parameters.AddWithValue("@SrNo", SrNo);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
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

        public static DataSet DeleteFlashcard(Int64 CompId, Int64 ContentId, string CreatedBy)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spDeleteFlashcard";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompId", CompId);
                cmd.Parameters.AddWithValue("@ContentId", ContentId);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
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

        public static DataSet GetFlashcards(Int64 CompId, Int64 ContentId)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spGetFlashcards";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContentId", ContentId);
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