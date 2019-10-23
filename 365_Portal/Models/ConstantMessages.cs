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
            public static string InvalidUserCode = "2";
            public static string InvalidUser = "Invalid EmailId or Password.";            
        }
        public class WebServiceLog
        {
            public static string Success = "Success";
            public static string Exception = "Error";
            public static string Validation = "Validation";

            public static string GenericErrorMsg = "There seems to be technical problem, please try again.";
            public static string GenericErrorCode = "There seems to be technical problem, please try again.";
        }

        public class Roles
        {
            public static string superadmin = "superadmin";
            public static string companyadmin = "companyadmin";
            public static string subadmin = "subadmin";
            public static string enduser = "enduser";
        }

        public class ConnectionString
        {
            public static string Portal_365 = "365_Portal";
        }
    }
}