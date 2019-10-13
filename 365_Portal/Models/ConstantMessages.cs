using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal.Models
{
    public class ConstantMessages
    {
        public class Login
        {
            public static string InvalidUserCode = "1";
            public static string InvalidUser = "Invalid EmailId or Password.";            
        }
        public class WebServiceLog
        {
            public static string Success = "Success";
            public static string Exception = "Error";
        }

        public class ConnectionString
        {
            public static string Portal_365 = "365_Portal";
        }
    }
}