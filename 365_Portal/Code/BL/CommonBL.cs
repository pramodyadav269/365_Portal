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
            string PasswordSalt = string.Empty;
            string HashedPassword = string.Empty;
            DataSet ds = new DataSet();



            PasswordSalt = Utility.GetSalt();
            HashedPassword = Utility.GetHashedPassword(userdetails.NewPassword, PasswordSalt);
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

        public static DataSet GetUsers(UserBO objUserBO)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.GetUsers(objUserBO);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet BindRoleAndGroup(UserBO objUserBO)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.BindRoleAndGroup(objUserBO);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet CreateUpdateUser(UserBO objUserBO, int Action ,int ChildUserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.CreateUpdateUser(objUserBO, Action ,ChildUserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteUser(UserBO objUserBO, int ChildUserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.DeleteUser(objUserBO, ChildUserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetUserDetailsForParent(UserBO objUserBO, int ChildUserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.GetUserDetailsForParent(objUserBO, ChildUserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetUsersGroup(UserBO objUserBO, int Action)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.GetUsersGroup(objUserBO, Action);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet BindDropDown(UserBO objUserBO,string Type)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.BindDropDown(objUserBO,Type);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

    }
}