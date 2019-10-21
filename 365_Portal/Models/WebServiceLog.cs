using _365_Portal.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            MySqlParameter[] param = new MySqlParameter[18];
            param[0] = new MySqlParameter("p_TYPE", 1);
            param[1] = new MySqlParameter("p_ControllerName", objWebServiceLog.ControllerName);
            param[2] = new MySqlParameter("p_MethodName", objWebServiceLog.MethodName);
            param[3] = new MySqlParameter("p_RequestString", objWebServiceLog.RequestString);
            param[4] = new MySqlParameter("p_ResponseString", objWebServiceLog.ResponseString);
            param[5] = new MySqlParameter("p_RequestTime", objWebServiceLog.RequestTime);
            param[6] = new MySqlParameter("p_ResponseTime", objWebServiceLog.ResponseTime);
            param[7] = new MySqlParameter("p_RequestType", objWebServiceLog.RequestType);
            param[8] = new MySqlParameter("p_ClientIpAddress", objWebServiceLog.ClientIpAddress);
            param[9] = new MySqlParameter("p_IMEI", objWebServiceLog.IMEI);
            param[10] = new MySqlParameter("p_DeviceModelNo", objWebServiceLog.DeviceModelNo);
            param[11] = new MySqlParameter("p_DeviceType", objWebServiceLog.DeviceType);
            param[12] = new MySqlParameter("p_AppWebVersion", objWebServiceLog.AppWebVersion);
            param[13] = new MySqlParameter("p_Ref1", objWebServiceLog.Ref1);
            param[14] = new MySqlParameter("p_Ref2", objWebServiceLog.Ref2);
            param[15] = new MySqlParameter("p_Ref3", objWebServiceLog.Ref3);
            param[16] = new MySqlParameter("p_Ref4", objWebServiceLog.Ref4);
            param[17] = new MySqlParameter("p_Ref5", objWebServiceLog.Ref5);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddRange(param);
            DBConnection.InsertRecordWithIdentity("spWebServiceLog", cmd, "");                            
        }
    }
}