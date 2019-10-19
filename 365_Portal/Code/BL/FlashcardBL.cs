using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BL
{
    public class FlashcardBL
    {

        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "FlashcardBL", methodName);
        }

        public static DataSet CreateFlashcard(Int64 CompId, Int64 ContentId, Int64 FlashcardId,string FlashcardTitle,string FlashcardDescription,Int64 SrNo, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = FlashcardDAL.CreateFlashcard(CompId, ContentId, FlashcardId, FlashcardTitle, FlashcardDescription, SrNo, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ModifyFlashcard(Int64 CompId, Int64 ContentId, Int64 FlashcardId, string FlashcardTitle, string FlashcardDescription, Int64 SrNo, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = FlashcardDAL.CreateFlashcard(CompId, ContentId, FlashcardId, FlashcardTitle, FlashcardDescription, SrNo, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteFlashcard(Int64 CompId, Int64 ContentId,string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = FlashcardDAL.DeleteFlashcard(CompId, ContentId, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetFlashcards(Int64 CompId,Int64 ContentId)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = FlashcardDAL.GetFlashcards(CompId, ContentId);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

    }
}