using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BL
{
    public class OrganizationBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "OrganizationBL", methodName);
        }
        public static DataSet GetAdminUsers(UserBO objUserBO)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = OrganizationDAL.GetAdminUsers(objUserBO);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet CreateUpdateAdminUser(UserBO objUserBO, int Action, int ChildUserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = OrganizationDAL.CreateUpdateAdminUser(objUserBO, Action, ChildUserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetCountry(int UserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = OrganizationDAL.GetCountry(UserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetRole(string Role,string RoleID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = OrganizationDAL.GetRole(Role, RoleID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetAdminUserDetails(UserBO objUserBO, int ChildUserID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = OrganizationDAL.GetAdminUserDetails(objUserBO, ChildUserID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
    }
}