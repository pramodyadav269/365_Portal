using _365_Portal.Code;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Models
{
    public class EmailHelper
    {
        public class Functionality
        {
            public static string CREATE_USER = "CREATE_USER";
            public static string CREATE_ORG = "CREATE_ORG";
            public static string FORGOT_PASS = "FORGOT_PASS";
            public static string CHANGE_PASS = "CHANGE_PASS";
        }

        public class EmailResponse 
        {
            public string CompID { get; set; }
            public string Functionality { get; set; }
            public string FromMail { get; set; }
            public string FromCCMail { get; set; }
            public string FromBCCMail { get; set; }
            public string EmailContent { get; set; }
            public string SMSUserID { get; set; }
            public string SMSPassword { get; set; }
            public string SMSText { get; set; }
        }

        public static bool GetEmailContent(int CompID,string Functionality,string Ref1,string Ref2)
        {
            bool flag = true;
            try
            {
                DataSet ds = GetEmailContentFromDB(CompID, Functionality, Ref1, Ref2);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EmailResponse obj = new EmailResponse();
                    obj.CompID = ds.Tables[0].Rows[0]["CompID"].ToString();
                    obj.Functionality = ds.Tables[0].Rows[0]["Functionality"].ToString();
                    obj.FromMail = ds.Tables[0].Rows[0]["FromMail"].ToString();
                    obj.FromCCMail = ds.Tables[0].Rows[0]["FromCCMail"].ToString();
                    obj.FromBCCMail = ds.Tables[0].Rows[0]["FromBCCMail"].ToString();
                    obj.EmailContent = ds.Tables[0].Rows[0]["EmailContent"].ToString();
                    obj.SMSUserID = ds.Tables[0].Rows[0]["SMSUserID"].ToString();
                    obj.SMSPassword = ds.Tables[0].Rows[0]["SMSPassword"].ToString();
                    obj.SMSText = ds.Tables[0].Rows[0]["SMSText"].ToString();

                    //code goes here to send mail
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;       
        }
        public static DataSet GetEmailContentFromDB(int CompID, string Functionality, string Ref1, string Ref2)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(ConnectionManager.connectionString);

            try
            {
                conn.Open();
                string stm = "spGetEmailContent";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CompID", CompID);
                cmd.Parameters.AddWithValue("p_Functionality", Functionality);
                cmd.Parameters.AddWithValue("p_Ref1", Ref1);
                cmd.Parameters.AddWithValue("p_Ref2", Ref2);
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

        private static void Log(Exception ex, string name)
        {
            throw new NotImplementedException();
        }
    }
}