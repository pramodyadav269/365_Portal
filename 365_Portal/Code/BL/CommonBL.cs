using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace _365_Portal.Code.BL
{
    public class CommonBL
    {

        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "CommonBL", methodName);
        }

        public static DataSet Login(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.Login(userdetails);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet Logout(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.Logout(userdetails);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ResetPassword(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.ResetPassword(userdetails);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet UpdateActivity(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.UpdateActivity(userdetails);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }


        public static DataSet ChangePassword(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            Regex regex = new Regex(@"^(.{5,7}|[^0-9]*|[^A-Z])$");

            if (!string.IsNullOrEmpty(userdetails.NewPassword))
            {
                Match match = regex.Match(userdetails.NewPassword);
                if(1==1)
                {
                    string PasswordSalt = Utility.GetSalt();
                    string HashedPassword = Utility.GetHashedPassword(userdetails.NewPassword, PasswordSalt);
                    userdetails.PasswordSalt = PasswordSalt;
                    userdetails.NewPassword = HashedPassword; //NewPassword variable storing the Newly generated password's Hash.
                    try
                    {
                        ds = CommonDAL.ChangePassword(userdetails);
                    }
                    catch (Exception ex)
                    {
                        Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    }
                }
                else
                {
                    new ApplicationException("Password should contain at least 1 Alphabet, 1 Number and 1 Special Character.");
                }
            }

            else
            {
                new ApplicationException("Password Can't be empty");
            }
            return ds;
        }


        public static DataSet GetNotifications(int CompID, int UserID, string Token)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.GetNotifications(CompID, UserID, Token);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }


        public static DataSet GetProfileDetails(int CompID, int UserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.GetProfileDetails(CompID, UserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet SetProfileDetails(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.SetProfileDetails(userdetails);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetLoginDetails(string UserName)
        {
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrWhiteSpace(UserName))
            {
                try
                {
                    ds = CommonDAL.GetLoginDetails(UserName);
                }
                catch (Exception ex)
                {
                    Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                }

            }
            else
            {

            }
            return ds;
        }
    }
}