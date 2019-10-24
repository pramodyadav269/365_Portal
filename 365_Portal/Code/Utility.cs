using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace _365_Portal.Code
{
    public class Utility
    {
        public static string ConvertDataSetToJSONString(DataTable dt)
        {
            var lst = dt.AsEnumerable()
        .Select(r => r.Table.Columns.Cast<DataColumn>()
                .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])
               ).ToDictionary(z => z.Key, z => z.Value != null ? Utility.GetSafeString(z.Value.ToString(), true) : null)
        ).ToList();
            //now serialize it
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Serialize(lst);
        }

        public static string GetHashedPassword(string value, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            byte[] src = Convert.FromBase64String(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        private static int saltLengthLimit = 32;
        public static string GetSalt()
        {
            return Convert.ToBase64String(GetSalt(saltLengthLimit));
        }

        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public static DateTime GetIST()
        {
            return DateTime.Now;
        }

        public static string GetSafeString(string str, bool safe)
        {
            if (safe)
            {
                return HttpUtility.HtmlEncode(str);
            }
            return str;
        }

        public static string GetJSONData(string statusCode, string statusDescription,string data)
        {
            return "{\"StatusCode\":\"" + statusCode + "\",\"StatusDescription\":\"" + statusDescription + "\",\"Data\":"+ data + "}";           
        }

        public static string API_Status(string statusCode,string statusDescription)
        {
            return "{\"StatusCode\":\""+ statusCode + "\",\"StatusDescription\":\""+ statusDescription + "\"}";
            //return "{'StatusCode':'" + statusCode + "','StatusDescription':'" + statusDescription + "'}";
        }

        public static string AuthenticationError()
        {
            return API_Status("0","Invalid Token");
        }

        public static string Successful(string data)
        {
            return GetJSONData("1", "Successful", data);
        }

        public static string Failed(string data)
        {
            return GetJSONData("0", "Failed", data);
        }


        public static string GetClientIPaddress()
        {
            string IPAddress = "";
            // This Condition Checks is the Request is from Proxy server. 
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
            {
                IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                IPAddress = HttpContext.Current.Request.UserHostAddress;
            }
            return IPAddress;
        }


    }
}