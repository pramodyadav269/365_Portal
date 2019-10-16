using MySql.Data.MySqlClient;
using System.Data;

namespace _365_Portal.Code.DAL
{
    public class TrainningDAL
    {
        public static DataSet GetUserTopics()
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);
          
            try
            {
                conn.Open();
                string stm = "SELECT * FROM city";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(stm, conn);
                da.Fill(ds, "Data");
                return ds;
            }
            catch (MySqlException ex)
            {
                
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
    }
}