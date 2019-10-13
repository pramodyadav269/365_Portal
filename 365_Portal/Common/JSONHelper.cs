using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace _365_Portal.Common
{
    public class JSONHelper
    {
        public static string ConvertJsonToString<T>(T model)
        {
            string result = null;
            try
            {
                JavaScriptSerializer JSSerialzer = new JavaScriptSerializer();
                var Jsonresult = JSSerialzer.Serialize(model);
                result = JSSerialzer.ConvertToType<string>(Jsonresult);
            }
            catch (Exception ex)
            {
                result = "Some unhandled Error occured.";
            }
            return result.ToString();
        }
    }
}