using _365_Portal.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _365_Portal.Models
{    
    public class WebServiceLog
    {
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string RequestString { get; set; }
        public string ResponseString { get; set; }
        public DateTime RequestTime { get; set; }        
        public DateTime ResponseTime { get; set; }
        public string RequestType { get; set; }
        public string ClientIpAddress { get; set; }
        public string IMEI { get; set; }
        public string DeviceModelNo { get; set; }
        public string DeviceType { get; set; }
        public string AppWebVersion { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
    }

    public class InsertRequestLog
    {
        public static void SaveWebServiceLog(WebServiceLog objWebServiceLog)
        {
            string constr = ConfigurationSettings.AppSettings["conString"].ToString();
            SqlConnection conn = new SqlConnection(constr);
            using (SqlCommand cmd = new SqlCommand("proc_WebServiceLog", DBConnection.getConnection()))
            {
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_ControllerName", objWebServiceLog.ControllerName);
                    cmd.Parameters.AddWithValue("@_MethodName", objWebServiceLog.MethodName);
                    cmd.Parameters.AddWithValue("@_RequestString", objWebServiceLog.RequestString);
                    cmd.Parameters.AddWithValue("@_ResponseString", objWebServiceLog.ResponseString);
                    cmd.Parameters.AddWithValue("@_RequestTime", objWebServiceLog.RequestTime);
                    cmd.Parameters.AddWithValue("@_ResponseTime", objWebServiceLog.ResponseTime);
                    cmd.Parameters.AddWithValue("@_RequestType", objWebServiceLog.RequestType);
                    cmd.Parameters.AddWithValue("@_ClientIpAddress", objWebServiceLog.ClientIpAddress);
                    cmd.Parameters.AddWithValue("@_IMEI", objWebServiceLog.IMEI);
                    cmd.Parameters.AddWithValue("@_DeviceModelNo", objWebServiceLog.DeviceModelNo);
                    cmd.Parameters.AddWithValue("@_DeviceType", objWebServiceLog.DeviceType);
                    cmd.Parameters.AddWithValue("@_AppWebVersion", objWebServiceLog.AppWebVersion);
                    cmd.Parameters.AddWithValue("@_Ref1", objWebServiceLog.Ref1);
                    cmd.Parameters.AddWithValue("@_Ref2", objWebServiceLog.Ref2);
                    cmd.Parameters.AddWithValue("@_Ref3", objWebServiceLog.Ref3);
                    cmd.Parameters.AddWithValue("@_Ref4", objWebServiceLog.Ref4);
                    cmd.Parameters.AddWithValue("@_Ref5", objWebServiceLog.Ref5);
                    cmd.ExecuteNonQuery();                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd != null)
                    {
                        if (cmd.Connection.State == ConnectionState.Open ||
                            cmd.Connection.State == ConnectionState.Executing ||
                            cmd.Connection.State == ConnectionState.Fetching)
                            cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }
                }
            }
        }
    }
}