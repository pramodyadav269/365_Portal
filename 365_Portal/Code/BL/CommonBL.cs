using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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


        public static DataSet GetNotifications(Int64  CompID, Int64 UserId, string Token)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.GetNotifications(CompID,UserId,Token);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }


        public static DataSet GetProfileDetails(Int64 CompID, Int64 UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = CommonDAL.GetProfileDetails(CompID,UserId);
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

    }
}