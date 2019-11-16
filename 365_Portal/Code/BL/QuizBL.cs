using _365_Portal.Code.DAL;
using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BL
{
    public class QuizBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "QuizBL", methodName);
        }



        public static DataSet SaveSurvey(int compId, string userId,int contentId, string title, string description,string overview,bool isPublished,double totalScore,double passingScore, double passingPercent)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = QuizDAL.SaveSurvey(compId, userId, contentId, title, description, overview, isPublished, totalScore, passingScore, passingPercent);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ManageQuestion(int  compId, string userId,int  questionId,int  contentId,bool isMandatory,bool isMultiline,string title,int qType,bool isBox,int type,int action)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = QuizDAL.ManageQuestion(compId, userId, questionId, contentId, isMandatory, isMultiline, title, qType, isBox, type, action);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet ManageAnsOptions(int compId, string userId, int type, int contentId,int  questionId,int answerId,string title,bool isCorrect,double score, int action)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = QuizDAL.ManageAnsOptions(compId, userId, type, contentId, questionId, answerId, title, isCorrect, score, action);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ManageFlashcardIntro(int compId, string userId,int  introId, string comments, int contentId, int action)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = QuizDAL.ManageFlashcardIntro(compId, userId, introId, comments, contentId, action);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;

        }


        public static DataSet ManageFlashcardSlides(int compId, string userId, int flashcardId, string title, int contentId, string description, int action)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = QuizDAL.ManageFlashcardSlides(compId, userId, flashcardId, title, contentId, description, action);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;

        }

    }
}