using _365_Portal.Code.BO;
using _365_Portal.Code.DAL;
using _365_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace _365_Portal.Code.BL
{
    public class ContentBL
    {
        public static void Log(Exception ex, string methodName)
        {
            Logger.Log(ex, "ContentBL", methodName);
        }

        #region Topics all CRUD
        public static DataSet CreateTopic(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateTopic(Convert.ToInt32(ConstantMessages.Action.INSERT), content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ModifyTopic(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateTopic(Convert.ToInt32(ConstantMessages.Action.MODIFY), content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteTopic(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.DeleteTopic(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetTopics(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.GetTopics(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet AssignTopics(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.AssignTopics(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        #endregion
        #region Modules all CRUD
        public static DataSet CreateModule(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateModule(Convert.ToInt32(ConstantMessages.Action.INSERT), content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ModifyModule(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateModule(Convert.ToInt32(ConstantMessages.Action.MODIFY), content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteModule(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.DeleteModule(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetModules(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.GetModules(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        #endregion
        #region Content all CRUD
        public static DataSet CreateContent(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateContent((int)ConstantMessages.Action.INSERT, content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet ModifyContent(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.CreateContent((int)ConstantMessages.Action.MODIFY, content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet DeleteContent(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.DeleteContent(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        public static DataSet GetContentList(ContentBO content)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.GetContentList(content);
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        public static DataSet GetContentList_Formatted(ContentBO content, ref List<ContentBO> lstContent)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = ContentDAL.GetContentList(content);
                lstContent = new List<ContentBO>();
                lstContent = (from DataRow dr in ds.Tables[0].Rows
                              select new ContentBO()
                              {
                                  ContentID = Convert.ToInt32(Convert.ToString(dr["ContentID"])),
                                  Title = Convert.ToString(dr["Title"]),
                                  SrNo = Convert.ToInt32(Convert.ToString(dr["SrNo"])),
                                  Description = Convert.ToString(dr["Description"]),
                                  IntroTitle = Convert.ToString(dr["FlashcardTitle"]),
                                  QuizCount = Convert.ToInt32(Convert.ToString(dr["QuizCount"])),
                                  IsPublished = !string.IsNullOrEmpty(dr["IsPublished"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsPublished"].ToString())) : true,
                                  IsGift = !string.IsNullOrEmpty(dr["IsGift"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsGift"].ToString())) : true,
                                  IsSkipFlashcards = !string.IsNullOrEmpty(dr["SkipFlashcards"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["SkipFlashcards"].ToString())) : true,
                                  TotalScore = !string.IsNullOrEmpty(dr["TotalScore"].ToString()) ? Convert.ToDouble(dr["TotalScore"].ToString()) : 0,
                                  PassingScore = !string.IsNullOrEmpty(dr["PassingScore"].ToString()) ? Convert.ToDouble(dr["PassingScore"].ToString()) : 0,
                                  PassingPercent = !string.IsNullOrEmpty(dr["PassingPercent"].ToString()) ? Convert.ToDouble(dr["PassingPercent"].ToString()) : 0,
                                  ContentType = Convert.ToString(dr["Type"])
                              }).ToList();

                List<FlashcarIntro> lstFlashcarIntro = new List<FlashcarIntro>();
                lstFlashcarIntro = (from DataRow dr in ds.Tables[1].Rows
                                    select new FlashcarIntro()
                                    {
                                        ContentID = Convert.ToInt32(Convert.ToString(dr["ContentID"])),
                                        SrNo = Convert.ToInt32(Convert.ToString(dr["SrNo"])),
                                        ID = Convert.ToInt32(Convert.ToString(dr["ID"])),
                                        Comments = Convert.ToString(dr["Comments"])
                                    }).ToList();

                List<FlashcardSlides> lstFlashcardSlides = new List<FlashcardSlides>();
                lstFlashcardSlides = (from DataRow dr in ds.Tables[2].Rows
                                      select new FlashcardSlides()
                                      {
                                          ContentID = Convert.ToInt32(Convert.ToString(dr["ContentID"])),
                                          SrNo = Convert.ToInt32(Convert.ToString(dr["SrNo"])),
                                          FlashcardID = Convert.ToInt32(Convert.ToString(dr["FlashcardID"])),
                                          Title = Convert.ToString(dr["Title"]),
                                          Description = Convert.ToString(dr["Description"])
                                      }).ToList();

                var questionList = new List<Question>();
                questionList = (from DataRow dr in ds.Tables[3].Rows
                                select new Question()
                                {
                                    ContentID = Convert.ToInt32(Convert.ToString(dr["ContentID"])),
                                    QuestionID = Convert.ToInt32(dr["QuestionID"]),
                                    Title = dr["Title"].ToString(),
                                    SortOrder = Convert.ToInt32(dr["SrNo"].ToString()),
                                    QType = dr["QuestionType"].ToString(),
                                    QuestionTypeID = Convert.ToInt32(dr["QuestionTypeID"].ToString()),
                                    IsBox = !string.IsNullOrEmpty(dr["IsBox"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsBox"].ToString())) : true
                                }).ToList();

                List<AnswerOption> ansOptionList = new List<AnswerOption>();
                ansOptionList = (from DataRow dr in ds.Tables[4].Rows
                                 select new AnswerOption()
                                 {
                                     QuestionID = !string.IsNullOrEmpty(dr["QuestionID"].ToString()) ? Convert.ToInt32(dr["QuestionID"].ToString()) : 0,
                                     SortOrder = !string.IsNullOrEmpty(dr["SrNo"].ToString()) ? Convert.ToInt32(dr["SrNo"].ToString()) : 0,
                                     AnswerID = !string.IsNullOrEmpty(dr["AnswerID"].ToString()) ? Convert.ToInt32(dr["AnswerID"].ToString()) : 0,
                                     AnswerText = dr["AnswerText"].ToString(),
                                     IsCorrect = !string.IsNullOrEmpty(dr["IsCorrect"].ToString()) ? Convert.ToBoolean(Convert.ToInt32(dr["IsCorrect"].ToString())) : false,
                                     CorrectScore = !string.IsNullOrEmpty(dr["CorrectScore"].ToString()) ? Convert.ToDouble(dr["CorrectScore"].ToString()) : 0,
                                 }).ToList();

                lstContent = lstContent.OrderBy(p => p.SrNo).ToList();
                foreach (var _content in lstContent)
                {
                    var questions = questionList.Where(p => p.ContentID == _content.ContentID).OrderBy(p => p.SortOrder).ToList();
                    _content.Questions = questions;
                    _content.FlashcardIntro = lstFlashcarIntro.Where(p => p.ContentID == _content.ContentID).OrderBy(p => p.SrNo).ToList();
                    _content.FlashcardSlides = lstFlashcardSlides.Where(p => p.ContentID == _content.ContentID).OrderBy(p => p.SrNo).ToList();
                    foreach (var question in questions)
                    {
                        List<AnswerOption> ansOptions = new List<AnswerOption>();
                        question.AnswerOptions = ansOptionList.Where(p => p.QuestionID == question.QuestionID).OrderBy(p => p.SortOrder).ToList();
                        if (question.AnswerOptions.Count() > 0)
                            if (question.QuestionTypeID == 2 || question.QuestionTypeID == 3)
                                question.TotalScore = question.AnswerOptions.Max(p => p.CorrectScore);
                            else if (question.QuestionTypeID == 1)
                                question.TotalScore = question.AnswerOptions.Where(p => p.IsCorrect == true).Sum(p => p.CorrectScore);
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }

        #endregion

        #region Reordering Table
        public static DataSet ReorderContent(int compid, string userid, int type, string ids)
        {

            DataSet ds = new DataSet();

            try
            {
                ds = ContentDAL.ReorderContent(compid, userid, type, ids);

            }
            catch (Exception ex)
            {
                Log(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ds;
        }
        #endregion

    }


}