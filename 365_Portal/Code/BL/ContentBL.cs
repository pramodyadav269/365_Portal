using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace _365_Portal.Code.BL
{
    public class ContentBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "ContentBL", methodName);
        }
    }
}