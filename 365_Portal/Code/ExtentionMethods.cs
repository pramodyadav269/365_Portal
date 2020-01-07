using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal.Code
{
    public static class ExtentionMethods
    {
        public static bool IsNumeric(this string str, int num)
        {
            int number;
            if (int.TryParse(str, out number))
                return true;
            return false;
        }

        public static bool IsNumeric(this string str, int num, string aaa)
        {
            int number;
            if (int.TryParse(str, out number))
                return true;
            return false;
        }
    }
}