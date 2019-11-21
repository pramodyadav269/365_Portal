using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;              // FOR "DataTable".
using System.Data.OleDb;
using System.IO;
using OfficeOpenXml;
using System.Data.SqlClient;
using System.Net;
using BulkUploadNamespace;
using _365_Portal.Common;
using _365_Portal.Code.BL;

namespace _365_Portal.Admin
{
    public partial class BulkUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "BulkUpload_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvRecords.GridLines = GridLines.Both;
            gvRecords.HeaderStyle.Font.Bold = true;
            gvRecords.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        void ProcessTable1(DataTable dt)
        {
            //Topic_Title	Topic_Description	Topic_Published	
            //Module_Title	Module_Description	Module_Overview	Module_Published
            //Content_Title	Content_Description	Content_Overview	Doc_Type	File_Path	Is_Gift	Content_Published

            List<Topic> lstTopics = new List<Topic>();
            List<Module> lstModules = new List<Module>();
            List<_Content> lstContents = new List<_Content>();

            int totalCount = 0;
            int srNo = 1;
            int topicSrNo = 1;
            int moduleSrNo = 1;
            int contentSrNo = 1;
            Topic topic = null;
            Module module = null;
            int errorCount = 0;
            var status = "";
            var message = "";
            foreach (DataRow row in dt.Rows)
            {
                status = "Verified";
                message = "Verified";
                row["SrNo"] = srNo;
                try
                {
                    var TopicTitle = Convert.ToString(row["Topic_Title"]);
                    var moduleTitle = Convert.ToString(row["Module_Title"]);
                    var contentTitle = Convert.ToString(row["Content_Title"]);
                    if (!string.IsNullOrEmpty(TopicTitle))
                    {
                        // New Topic
                        if (!IsEmpty(row["Topic_Description"]) && !IsEmpty(row["Topic_Published"]))
                        {
                            topic = new Topic();
                            topic.ParentSrNo = srNo;
                            topic.Title = TopicTitle;
                            topic.Description = Convert.ToString(row["Topic_Description"]);
                            topic.IsPublished = GetBoolValue(Convert.ToString((row["Topic_Published"])));
                            topic.SrNo = topicSrNo;
                            lstTopics.Add(topic);
                           // ContentBL.CreateTopic();
                        }
                        else
                        {
                            // Mandatory Fields
                            status = "Failed";
                            message = "All fields are mandatory";
                        }
                        topicSrNo++;
                        moduleSrNo = 1;
                        contentSrNo = 1;
                    }
                    if (!string.IsNullOrEmpty(moduleTitle))
                    {
                        // New Module of Topic
                        if (!IsEmpty(row["Module_Title"]) && !IsEmpty(row["Module_Description"])
                            && !IsEmpty(row["Module_Overview"]) && !IsEmpty(row["Module_Published"])
                            && topic != null)
                        {
                            module = new Module();
                            module.ParentSrNo = srNo;
                            module.TopicTitle = topic.Title;
                            module.Title = Convert.ToString(row["Module_Title"]); ;
                            module.Description = Convert.ToString(row["Module_Description"]);
                            module.Overview = Convert.ToString(row["Module_Overview"]);
                            module.IsPublished = GetBoolValue(Convert.ToString((row["Module_Published"])));
                            module.SrNo = moduleSrNo;
                            lstModules.Add(module);

                        }
                        else
                        {
                            // Mandatory Fields
                            status = "Failed";
                            message = "All fields are mandatory";
                        }
                        moduleSrNo++;
                        contentSrNo = 1;
                    }
                    if (!string.IsNullOrEmpty(contentTitle))
                    {
                        // Add Content
                        if (!IsEmpty(row["Content_Title"]) && !IsEmpty(row["Content_Description"])
                            && !IsEmpty(row["Content_Overview"]) && !IsEmpty(row["Doc_Type"])
                            && !IsEmpty(row["File_Path"]) && !IsEmpty(row["Is_Gift"])
                             && !IsEmpty(row["Content_Published"]) && topic != null && module != null)
                        {
                            var content = new _Content();
                            content.ParentSrNo = srNo;
                            content.TopicTitle = topic.Title;
                            content.ModuleTitle = module.Title;
                            content.Title = Convert.ToString(row["Content_Title"]);
                            content.Description = Convert.ToString(row["Content_Description"]);
                            content.Overview = Convert.ToString(row["Content_Overview"]);
                            content.DocType = Convert.ToString(row["Doc_Type"]);
                            content.FilePath = Convert.ToString(row["File_Path"]);
                            content.IsGift = GetBoolValue(Convert.ToString(row["Is_Gift"]));
                            content.IsPublished = GetBoolValue(Convert.ToString(row["Content_Published"]));
                            content.SrNo = contentSrNo;
                            lstContents.Add(content);
                        }
                        else
                        {
                            // Mandatory Fields
                            status = "Failed";
                            message = "All fields are mandatory";
                        }
                        contentSrNo++;
                    }

                    row["Status"] = status;
                    row["Message"] = message;
                    if (status.Trim().ToUpper() == "FAILED")
                        errorCount++;
                }
                catch (Exception ex)
                {
                    row["Status"] = "Failed";
                    row["Message"] = ex.Message;
                    errorCount++;
                }
                srNo++;
                totalCount++;
            }

            if (errorCount == 0)
            {
                // If there are no errors. Insert in Database
                foreach (var _topic in lstTopics)
                {
                    try
                    {
                        // Add Topic
                        int topicId = (new Random()).Next();
                        if (topicId > 0)
                        {
                            foreach (var _module in lstModules)
                            {
                                try
                                {
                                    // Add Module
                                    int moduleId = (new Random()).Next();
                                    if (moduleId > 0)
                                    {
                                        foreach (var _content in lstContents)
                                        {
                                            try
                                            {
                                                // Add Content
                                                int contentId = (new Random()).Next();
                                                if (contentId > 0)
                                                {

                                                }
                                                else
                                                {
                                                    // Unable to add content
                                                    errorCount++;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                errorCount++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Unable to add module
                                        errorCount++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errorCount++;
                                    UpdateDataTable(dt, _module.ParentSrNo, ex.Message);
                                }
                            }
                        }
                        else
                        {
                            // Unable to add topic
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                    }
                }

                // Check here..
                if (errorCount == 0)
                {
                    lblConfirm.Text = "Data uploaded successfully";
                }
                else
                {
                    if (errorCount == totalCount)
                    {
                        lblConfirm.Text = "No data uploaded. Resolve errors & try again.";
                        gvRecords.Visible = true;
                        btnExport.Visible = true;
                    }
                    else
                    {
                        lblConfirm.Text = "Records marked with successful status have been uploaded. Resolve errors & try again.";
                    }
                }
            }
            else
            {
                lblConfirm.Text = "No data uploaded. Resolve errors & try again.";
                gvRecords.Visible = true;
                btnExport.Visible = true;
            }
        }

        void ProcessTable2(DataTable dt)
        {
            //Topic_Title
            //Module_Title
            //Flashcard_Title	Flashcard_Description	Flashcard_Overview	Is_Published	Is_Gift	Skip_Flashcards	Flashcard_Introduction_Title

            List<Flashcard> lstFlashcards = new List<Flashcard>();
            List<FlashcardIntro> lstFlashcardIntro = new List<FlashcardIntro>();
            List<FlashcardSlide> lstFlashcardSlides = new List<FlashcardSlide>();

            int totalCount = 0;
            int srNo = 1;
            int flashcardSrNo = 1;
            int introSrNo = 1;
            int slidSrNo = 1;

            var introIdentifier = "Flashcard_Introduction";
            var slideIdentifier = "Flashcard_Slide";
            Flashcard flashcard = null;
            bool flashcardIntro = false;
            bool flashcardSlide = false;
            int errorCount = 0;
            var status = "";
            var message = "";
            foreach (DataRow row in dt.Rows)
            {
                status = "Verified";
                message = "Verified";
                row["SrNo"] = srNo;
                try
                {
                    var TopicTitle = Convert.ToString(row["Topic_Title"]);
                    var moduleTitle = Convert.ToString(row["Module_Title"]);
                    var flashcardTitle = Convert.ToString(row["Flashcard_Title"]);
                    if (!string.IsNullOrEmpty(TopicTitle))
                    {
                        if (introIdentifier.Trim().ToUpper() == TopicTitle.Trim().ToUpper())
                        {
                            // Flashcard Intro Starts
                            flashcardIntro = true;
                            flashcardSlide = false;
                        }
                        else if (slideIdentifier.Trim().ToUpper() == TopicTitle.Trim().ToUpper())
                        {
                            // Flashcard Slides Starts
                            flashcardIntro = false;
                            flashcardSlide = true;
                        }
                        else
                        {
                            // Topic/Module Starts
                            if (!string.IsNullOrEmpty(moduleTitle) && !string.IsNullOrEmpty(TopicTitle)
                                && !IsEmpty(row["Flashcard_Title"]) && !IsEmpty(row["Flashcard_Description"])
                           && !IsEmpty(row["Flashcard_Overview"]) && !IsEmpty(row["Is_Published"])
                           && !IsEmpty(row["Is_Gift"]) && !IsEmpty(row["Skip_Flashcards"])
                            && !IsEmpty(row["Flashcard_Introduction_Title"]))
                            {
                                flashcard = new Flashcard();
                                flashcard.TopicTitle = moduleTitle;
                                flashcard.ModuleTitle = TopicTitle;
                                flashcard.SrNo = flashcardSrNo;
                                flashcard.Title = Convert.ToString(row["Flashcard_Title"]);
                                flashcard.Description = Convert.ToString(row["Flashcard_Description"]);
                                flashcard.Overview = Convert.ToString(row["Flashcard_Overview"]);
                                flashcard.IsPublished = GetBoolValue(Convert.ToString(row["Is_Published"]));
                                flashcard.IsGift = GetBoolValue(Convert.ToString(row["Is_Gift"]));
                                flashcard.SkipFlashcards = GetBoolValue(Convert.ToString(row["Skip_Flashcards"]));
                                flashcard.IntroTitle = Convert.ToString(row["Flashcard_Introduction_Title"]);
                                lstFlashcards.Add(flashcard);
                            }
                            else
                            {
                                // Mandatory Fields
                                status = "Failed";
                                message = "All fields are mandatory";
                            }
                            flashcardSrNo++;
                            introSrNo = 1;
                            slidSrNo = 1;
                            flashcardIntro = false;
                            flashcardSlide = false;
                        }
                    }
                    else
                    {
                        if (flashcardIntro)
                        {
                            if (flashcard != null)
                            {
                                FlashcardIntro intro = new FlashcardIntro();
                                intro.TopicTitle = flashcard.TopicTitle;
                                intro.ModuleTitle = flashcard.ModuleTitle;
                                intro.SrNo = introSrNo;
                                intro.BulletPoint = moduleTitle; // Module_Title considered as BulletPoint
                                lstFlashcardIntro.Add(intro);
                            }
                            else
                            {
                                // Mandatory Fields
                                status = "Failed";
                                message = "All fields are mandatory";
                            }
                            introSrNo++;
                        }

                        if (flashcardSlide)
                        {
                            if (!string.IsNullOrEmpty(moduleTitle) && !string.IsNullOrEmpty(flashcardTitle) && flashcard != null)
                            {
                                FlashcardSlide slide = new FlashcardSlide();
                                slide.TopicTitle = flashcard.TopicTitle;
                                slide.ModuleTitle = flashcard.ModuleTitle;
                                slide.SrNo = slidSrNo;
                                slide.Title = moduleTitle; // Module_Title considered as Title
                                slide.Description = flashcardTitle; // Module_Title considered as Description
                                lstFlashcardSlides.Add(slide);
                            }
                            else
                            {
                                // Mandatory Fields
                                status = "Failed";
                                message = "All fields are mandatory";
                            }

                            slidSrNo++;
                        }
                    }
                    row["Status"] = status;
                    row["Message"] = message;
                    if (status.Trim().ToUpper() == "FAILED")
                        errorCount++;
                }
                catch (Exception ex)
                {
                    row["Status"] = "Failed";
                    row["Message"] = ex.Message;
                    errorCount++;
                }
                srNo++;
                totalCount++;
            }

            if (errorCount == 0)
            {
                foreach (var _flashcard in lstFlashcards)
                {
                    try
                    {
                        var flashcardId = (new Random()).Next();
                        if (flashcardId > 0)
                        {
                            foreach (var _intro in lstFlashcardIntro)
                            {
                                try
                                {
                                    var introId = (new Random()).Next();
                                    if (introId > 0)
                                    { }
                                    else
                                    {
                                        // Unable to add intro
                                        errorCount++;
                                        UpdateDataTable(dt, _intro.ParentSrNo, "Unable to add intro");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errorCount++;
                                    UpdateDataTable(dt, _intro.ParentSrNo, ex.Message);
                                }
                            }

                            foreach (var _slide in lstFlashcardSlides)
                            {
                                try
                                {
                                    var slideId = (new Random()).Next();
                                    if (slideId > 0)
                                    { }
                                    else
                                    {
                                        // Unable to add slide
                                        errorCount++;
                                        UpdateDataTable(dt, _slide.ParentSrNo, "Unable to add slide");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errorCount++;
                                    UpdateDataTable(dt, _slide.ParentSrNo, ex.Message);
                                }
                            }
                        }
                        else
                        {
                            // Unable to add flashcard
                            errorCount++;
                            UpdateDataTable(dt, _flashcard.ParentSrNo, "Unable to add flashcard");
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        UpdateDataTable(dt, _flashcard.ParentSrNo, ex.Message);
                    }
                }
                // Check here..
                if (errorCount == 0)
                {
                    lblConfirm.Text = "Data uploaded successfully";
                }
                else
                {
                    if (errorCount == totalCount)
                    {
                        lblConfirm.Text = "No data uploaded. Resolve errors & try again.";
                        gvRecords.Visible = true;
                        btnExport.Visible = true;
                    }
                    else
                    {
                        lblConfirm.Text = "Records marked with successful status have been uploaded. Resolve errors & try again.";
                    }
                }
            }
            else
            {
                lblConfirm.Text = "No data uploaded. Resolve errors & try again.";
                gvRecords.Visible = true;
                btnExport.Visible = true;
            }
        }

        void ProcessTable3(DataTable dt)
        {
            //Topic_Title Module_Title Quiz_Type	
            //Title	Description	Overview	Is_Published	Total_Score	Passing_Percent	
            //Question_Title	Question_Type	IsBox	
            //Option_Title	Is_Correct	Score

            List<QuizBO> lstSpecialContent = new List<QuizBO>();
            List<Question> lstQuestion = new List<Question>();
            List<AnswerOption> lstAnswerOptions = new List<AnswerOption>();

            int totalCount = 0;
            int srNo = 1;
            int contentSrNo = 1;
            int questionSrNo = 1;
            int answerOptionSrNo = 1;

            QuizBO quiz = null;
            Question question = null;
            int errorCount = 0;
            var status = "";
            var message = "";
            foreach (DataRow row in dt.Rows)
            {
                status = "Verified";
                message = "Verified";
                row["SrNo"] = srNo;
                try
                {
                    var TopicTitle = Convert.ToString(row["Topic_Title"]);
                    var moduleTitle = Convert.ToString(row["Module_Title"]);
                    var questionTitle = Convert.ToString(row["Question_Title"]);
                    var optionTitle = Convert.ToString(row["Option_Title"]);
                    var quizType = Convert.ToString(row["Quiz_Type"]);

                    if (!string.IsNullOrEmpty(TopicTitle))
                    {
                        // New Content (Survey,Flashcard Quiz,Final Quiz)
                        if (!string.IsNullOrEmpty(TopicTitle) && !string.IsNullOrEmpty(moduleTitle) && !string.IsNullOrEmpty(quizType)
                                && !IsEmpty(row["Title"]) && !IsEmpty(row["Description"])
                           && !IsEmpty(row["Overview"]) && !IsEmpty(row["Is_Published"])
                            && !IsEmpty(row["Passing_Percent"]))
                        {
                            quiz = new QuizBO();
                            quiz.TopicTitle = TopicTitle;
                            quiz.ModuleTitle = moduleTitle;
                            quiz.QuizType = quizType;
                            quiz.Title = Convert.ToString(row["Title"]);
                            quiz.Description = Convert.ToString(row["Description"]);
                            quiz.Overview = Convert.ToString(row["Overview"]);
                            quiz.IsPublished = GetBoolValue(Convert.ToString(row["Is_Published"]));
                            quiz.PassingPercent = GetDoubleValue(Convert.ToString(row["Passing_Percent"]));
                            quiz.PassingScore = (quiz.TotalScore * quiz.PassingPercent) / 100;
                            quiz.SrNo = contentSrNo;
                            lstSpecialContent.Add(quiz);
                        }
                        else
                        {
                            // Mandatory Fields
                            status = "Failed";
                            message = "All fields are mandatory";
                        }

                        contentSrNo++;
                        questionSrNo = 1;
                        answerOptionSrNo = 1;
                    }
                    if (!string.IsNullOrEmpty(questionTitle))
                    {
                        // New Question
                        if (!IsEmpty(row["Question_Title"]) && !IsEmpty(row["Question_Type"])
                           && quiz != null)
                        {
                            if (ValidQuestionType(row["Question_Type"]))
                            {
                                question = new Question();
                                question.TopicTitle = quiz.TopicTitle;
                                question.ModuleTitle = quiz.ModuleTitle;
                                question.QuizTitle = quiz.Title;
                                question.Title = Convert.ToString(row["Question_Title"]);
                                question.QType = Convert.ToString(row["Question_Type"]);
                                question.IsBox = GetBoolValue(Convert.ToString(row["IsBox"]));
                                question.SrNo = questionSrNo;
                                lstQuestion.Add(question);
                            }
                            else
                            {
                                // Mandatory Fields
                                status = "Failed";
                                message = "Invalid Question Type";
                            }
                        }
                        else
                        {
                            // Mandatory Fields
                            status = "Failed";
                            message = "All fields are mandatory";
                        }
                        questionSrNo++;
                        answerOptionSrNo = 1;
                    }

                    if (!string.IsNullOrEmpty(optionTitle))
                    {
                        // New Answer Option
                        if (!IsEmpty(row["Option_Title"]) && quiz != null && ValidateScore(quiz.QuizType, row["Score"]))
                        {
                            AnswerOption ansOption = new AnswerOption();
                            ansOption.TopicTitle = quiz.TopicTitle;
                            ansOption.ModuleTitle = quiz.ModuleTitle;
                            ansOption.QuestionTitle = quiz.Title;
                            ansOption.Title = Convert.ToString(row["Option_Title"]);
                            ansOption.IsCorrect = GetBoolValue(Convert.ToString(row["Is_Correct"]));
                            ansOption.Score = GetDoubleValue(Convert.ToString(row["Score"]));
                            ansOption.SrNo = questionSrNo;

                            if (question.QType.Trim().ToUpper() == QuestionType.RadioButton || question.QType.Trim().ToUpper() == QuestionType.Dropdown)
                            {
                                // Check Multiple Answer cannot be correct
                                var cnt = lstAnswerOptions.Count(p => p.IsCorrect == true && p.QuestionTitle == question.Title);
                                if (cnt > 0 && ansOption.IsCorrect)
                                {
                                    status = "Failed";
                                    message = "Multiple options cannot be correct for this question type";
                                }
                            }

                            if (quiz.QuizType.Trim().ToUpper() == "FINALQUIZ")
                                if (ansOption.IsCorrect && ansOption.Score > 0)
                                {
                                    lstAnswerOptions.Add(ansOption);
                                }
                                else
                                {
                                    status = "Failed";
                                    message = "Score should be greater than 0 for correct answer";
                                }
                            else
                                lstAnswerOptions.Add(ansOption);
                        }
                        else
                        {
                            // Mandatory Fields
                            status = "Failed";
                            message = "All fields are mandatory";
                        }
                        answerOptionSrNo++;
                    }
                    else if (IsMandatoryAnsOptions(quiz.QuizType))
                    {
                        status = "Failed";
                        message = "Answer options are mandatory";
                    }

                    row["Status"] = status;
                    row["Message"] = message;
                    if (status.Trim().ToUpper() == "FAILED")
                        errorCount++;
                }
                catch (Exception ex)
                {
                    row["Status"] = "Failed";
                    row["Message"] = ex.Message;
                    errorCount++;
                }
                srNo++;
                totalCount++;
            }

            if (errorCount == 0)
            {
                foreach (var _content in lstSpecialContent)
                {
                    try
                    {
                        var contentId = (new Random()).Next();
                        if (contentId > 0)
                        {
                            // Calculate Total Score
                            var _totalScore = 0;
                            foreach (var _question in lstQuestion)
                            {
                                try
                                {
                                    var questionId = (new Random()).Next();
                                    if (questionId > 0)
                                    {
                                        foreach (var _answerOption in lstAnswerOptions)
                                        {
                                            try
                                            {
                                                var answerId = (new Random()).Next();
                                                if (answerId > 0)
                                                {

                                                }
                                                else
                                                {
                                                    // Unable to add
                                                    errorCount++;
                                                    UpdateDataTable(dt, _answerOption.ParentSrNo, "Unable to add");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                errorCount++;
                                                UpdateDataTable(dt, _answerOption.ParentSrNo, ex.Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Unable to add
                                        errorCount++;
                                        UpdateDataTable(dt, _question.ParentSrNo, "Unable to add");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errorCount++;
                                    UpdateDataTable(dt, _question.ParentSrNo, ex.Message);
                                }
                            }
                            _content.TotalScore = _totalScore;
                        }
                        else
                        {
                            // Unable to add
                            errorCount++;
                            UpdateDataTable(dt, _content.ParentSrNo, "Unable to add");
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        UpdateDataTable(dt, _content.ParentSrNo, ex.Message);
                    }
                }
                // Check here..
                if (errorCount == 0)
                {
                    lblConfirm.Text = "Data uploaded successfully";
                }
                else
                {
                    if (errorCount == totalCount)
                    {
                        lblConfirm.Text = "No data uploaded. Resolve errors & try again.";
                        gvRecords.Visible = true;
                        btnExport.Visible = true;
                    }
                    else
                    {
                        lblConfirm.Text = "Records marked with successful status have been uploaded. Resolve errors & try again.";
                    }
                }
            }
            else
            {
                lblConfirm.Text = "No data uploaded. Resolve errors & try again.";
                gvRecords.Visible = true;
                btnExport.Visible = true;
            }
        }

        bool IsEmpty(object obj)
        {
            if (string.IsNullOrEmpty(Convert.ToString(obj)))
                return true;
            else
                return false;
        }

        public void UpdateDataTable(DataTable dt, int srNo, string message)
        {
            var rows = dt.Select("SrNo=" + srNo);
            foreach (DataRow row in rows)
            {
                row["Status"] = "Failed";
                row["Message"] = message;
            }
            dt.AcceptChanges();
        }

        bool GetBoolValue(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Trim().ToUpper() == "YES")
                    return true;
            }
            return false;
        }

        bool ValidQuestionType(object qType)
        {
            string strQtype = "";
            strQtype = Convert.ToString(qType).Trim().ToUpper();
            if (QuestionType.MultipleChoice == strQtype || QuestionType.Dropdown == strQtype || QuestionType.RadioButton == strQtype || QuestionType.FileUpload == strQtype ||
                QuestionType.Scale == strQtype || QuestionType.TextBox == strQtype || QuestionType.Paragraphs == strQtype || QuestionType.DateTime == strQtype)
            {
                return true;
            }
            return false;
        }

        bool IsMandatoryAnsOptions(object qType)
        {
            string strQtype = "";
            strQtype = Convert.ToString(qType).Trim().ToUpper();
            if (QuestionType.MultipleChoice == strQtype || QuestionType.Dropdown == strQtype || QuestionType.RadioButton == strQtype)
            {
                return true;
            }
            return false;
        }

        bool ValidateScore(object qType, object obj)
        {
            string strQtype = "";
            strQtype = Convert.ToString(qType).Trim().ToUpper();
            if (QuestionType.MultipleChoice == strQtype || QuestionType.Dropdown == strQtype || QuestionType.RadioButton == strQtype)
            {
                if (string.IsNullOrEmpty(Convert.ToString(obj)))
                    return false;
                else
                    Convert.ToDouble(obj);
                return true;
            }
            return true;
        }

        double GetDoubleValue(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    return Convert.ToDouble(str.Trim());
                }
                return 0.0;
            }
            catch
            {
                return 0.0;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                if (Path.GetExtension(FileUpload.FileName).Equals(".xlsx"))
                {
                    var excel = new ExcelPackage(FileUpload.FileContent);
                    if (ddlType.SelectedValue == "1")
                    {
                        var dt = excel.ToDataTable1();
                        dt.Columns.Add("SrNo");
                        dt.Columns.Add("Status");
                        dt.Columns.Add("Message");
                        ProcessTable1(dt);
                        gvRecords.DataSource = dt;
                        gvRecords.DataBind();
                    }
                    else if (ddlType.SelectedValue == "2")
                    {
                        var dt = excel.ToDataTable2();
                        dt.Columns.Add("SrNo");
                        dt.Columns.Add("Status");
                        dt.Columns.Add("Message");
                        ProcessTable2(dt);
                        gvRecords.DataSource = dt;
                        gvRecords.DataBind();
                    }
                    else if (ddlType.SelectedValue == "3")
                    {
                        var dt = excel.ToDataTable3();
                        dt.Columns.Add("SrNo");
                        dt.Columns.Add("Status");
                        dt.Columns.Add("Message");
                        ProcessTable3(dt);
                        gvRecords.DataSource = dt;
                        gvRecords.DataBind();
                    }
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            var fileType = ddlType.SelectedValue;
            string filePath = "~/bulk_upload.xlsx";
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + filePath);
            byte[] data = req.DownloadData(Server.MapPath(filePath));
            response.BinaryWrite(data);
            response.End();
        }

        // Add Topic
        // Add Module
        // Add Content
        // Add Flashcard
        // Add Flashcard Intro
        // Add Flashcard Slide
        // Add Special Content
        // Add Question
        // Add Ans Option
    }
}