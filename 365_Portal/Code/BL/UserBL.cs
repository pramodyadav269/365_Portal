using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BL
{
    public class UserBL
    {

        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "UserBL", methodName);
        }

        public static DataSet CreateUser(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = UserDAL.CreateUser(userdetails);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet ModifyUser(UserBO userdetails)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = UserDAL.CreateUser(userdetails);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet DeleteUser(Int64 CompId, string UserId, string UserKey, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = UserDAL.DeleteUser(CompId,UserId,UserKey,CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetUsers(string UserId, string RoleId, string EmailId, string GroupId, string MobileNo, string Position, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = UserDAL.GetUsers( UserId,RoleId,EmailId,GroupId,MobileNo,Position, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet CreateGroup(Int64 CompId, string GroupName, string Description, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = UserDAL.CreateGroup(CompId,GroupName,Description,CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet ModifyGroup(Int64 CompId, string GroupName, string Description, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = UserDAL.CreateGroup(CompId, GroupName, Description, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet SetUserPassword(Int64 CompId, string UserId, string Password, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = UserDAL.SetUserPassword(CompId, UserId, Password, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
    }
}