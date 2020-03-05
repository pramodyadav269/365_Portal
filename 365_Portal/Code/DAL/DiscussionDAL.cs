using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
namespace _365_Portal.Code.DAL
{
    public class DiscussionDAL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "DiscussionDAL", methodName);
        }

        public static DataSet SendMessage(int compID, string userId, int moduleId, string message, string ipaddress)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spSendMessage";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compID", compID);
                cmd.Parameters.AddWithValue("p_moduleid", moduleId);
                cmd.Parameters.AddWithValue("p_userid", userId);
                cmd.Parameters.AddWithValue("p_message", message);
                cmd.Parameters.AddWithValue("p_ipaddress", ipaddress);
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


        public static DataSet GetDiscussionChatByModule(int compID, int userId, int moduleId)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetDiscussionChat";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compid", compID);
                cmd.Parameters.AddWithValue("p_moduleid", moduleId);
                cmd.Parameters.AddWithValue("p_userid", userId);
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