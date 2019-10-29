using _365_Portal.Code.DAL;
using _365_Portal.Models;
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

        public static DataSet DeleteUser(int CompId, string UserId, string UserKey, string CreatedBy)
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

        public static DataSet CreateGroup(int CompId, string GroupName, string Description, string CreatedBy)
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

        public static DataSet ModifyGroup(int CompId, string GroupName, string Description, string CreatedBy)
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
        public static DataSet SetUserPassword(int CompId, string UserId, string Password, string CreatedBy)
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


        /// <summary>
        /// Send OTP on provided mobile number/s
        /// </summary>
        /// <param name="mobileNumbers"></param>
        /// <param name="OTP"></param>
        /// <returns></returns>
        public static DataSet ResetPassword(int CompId,int UserId, long MobileNum, string EmailId, string Type,string DeviceDetails,string DeviceType,string IpAddress)
        {
            DataSet data = new DataSet();
            int OTP = Utility.GenerateOTP();
            //If otp type is for success message than skip otp code..            
                try
                {
                    data = null;

                }
                catch (Exception ex)
                {
                    data =null;
                }
            
            if (Type == ConstantMessages.ForgotPassowrd.Type_0)
            {
                // Send OTP on email-id.
                //SendEmail(FROM_EMAIL, emailId, GetMobileOTPVerificationMail(userName, OTP.ToString()), GetMobileOTPVerificationMailSubject());
            }
            else if (Type == ConstantMessages.ForgotPassowrd.Type_1)
            {
                // Send OTP on mobile phone..
                //string template = WebConfigurationManager.AppSettings["SMS_OTP_Template"].Replace("~OTP~", OTP.ToString());
                //SendSMS(new long[] { mobileNum }, template);
            }
            return data;
        }

        public static DataSet CreateFile(string FilePath, string FileDirectory, string Ref1)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = UserDAL.CreateFile(FilePath, FileDirectory, Ref1);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

    }
}