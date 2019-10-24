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

        public static DataSet CreateFlashcard(int CompID, int ContentID, int FlashcardID,string FlashcardTitle,string FlashcardDescription,int SrNo, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = FlashcardDAL.CreateFlashcard(CompID, ContentID, FlashcardID, FlashcardTitle, FlashcardDescription, SrNo, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ModifyFlashcard(int CompID, int ContentID, int FlashcardID, string FlashcardTitle, string FlashcardDescription, int SrNo, string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = FlashcardDAL.CreateFlashcard(CompID, ContentID, FlashcardID, FlashcardTitle, FlashcardDescription, SrNo, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteFlashcard(int CompID, int ContentID,string CreatedBy)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = FlashcardDAL.DeleteFlashcard(CompID, ContentID, CreatedBy);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetFlashcards(int CompID,int ContentID)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = FlashcardDAL.GetFlashcards(CompID, ContentID);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

    }
}