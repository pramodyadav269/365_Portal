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

        public static DataSet CreateFlashcard(Int32 CompId, Int32 ContentId, Int32 FlashcardId, string FlashcardTitle, string FlashcardDescription, Int32 SrNo, string CreatedBy)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spCreateFlashcard";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_CompId", CompId);
                cmd.Parameters.AddWithValue("_ContentId", ContentId);
                cmd.Parameters.AddWithValue("_FlashcardId", FlashcardId);
                cmd.Parameters.AddWithValue("_FlashcardTitle", FlashcardTitle);
                cmd.Parameters.AddWithValue("_FlashcardDescription", FlashcardDescription);
                cmd.Parameters.AddWithValue("_SrNo", SrNo);
                cmd.Parameters.AddWithValue("_CreatedBy", CreatedBy);
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

        public static DataSet DeleteFlashcard(Int32 CompId, Int32 ContentId, string CreatedBy)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spDeleteFlashcard";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_CompId", CompId);
                cmd.Parameters.AddWithValue("_ContentId", ContentId);
                cmd.Parameters.AddWithValue("_CreatedBy", CreatedBy);
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

        public static DataSet GetFlashcards(Int32 CompId, Int32 ContentId)
        {

            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {

                conn.Open();
                string stm = "spGetFlashcards";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_ContentId", ContentId);
                cmd.Parameters.AddWithValue("_ContentId", ContentId);
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