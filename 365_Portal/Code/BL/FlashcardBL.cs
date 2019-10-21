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

        public static DataSet CreateFlashcard(Int32 CompId, Int32 ContentId, Int32 FlashcardId,string FlashcardTitle,string FlashcardDescription,Int32 SrNo, string CreatedBy)
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
        public static DataSet ModifyFlashcard(Int32 CompId, Int32 ContentId, Int32 FlashcardId, string FlashcardTitle, string FlashcardDescription, Int32 SrNo, string CreatedBy)
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
        public static DataSet DeleteFlashcard(Int32 CompId, Int32 ContentId,string CreatedBy)
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
        public static DataSet GetFlashcards(Int32 CompId,Int32 ContentId)
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