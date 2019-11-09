using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkUploadNamespace
{
    public class Topic
    {
        public int TopicID { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }

    public class QuestionType
    {
        public static string MultipleChoice = "MULTIPLECHOICE";
        public static string Dropdown = "DROPDOWN";
        public static string RadioButton = "RADIO";
        public static string FileUpload = "FILEUPLOAD";
        public static string Scale = "SCALE";
        public static string TextBox = "TEXTBOX";
        public static string Paragraphs = "PARAGRAPHS";
        public static string DateTime = "DATETIME";
    }

    public class Module
    {
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public int ModuleID { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public bool IsPublished { get; set; }
    }

    public class _Content
    {
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public int ModuleID { get; set; }
        public string ModuleTitle { get; set; }
        public int ContentID { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public string DocType { get; set; }
        public string FilePath { get; set; }
        public bool IsGift { get; set; }
        public bool IsPublished { get; set; }
    }

    public class Flashcard
    {
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public int ModuleID { get; set; }
        public string ModuleTitle { get; set; }
        public int ContentID { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public string IntroTitle { get; set; }
        public bool SkipFlashcards { get; set; }
        public bool IsGift { get; set; }
        public bool IsPublished { get; set; }
    }

    public class FlashcardIntro
    {
        public int ContentID { get; set; }
        public string TopicTitle { get; set; }
        public string ModuleTitle { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public string BulletPoint { get; set; }
    }

    public class FlashcardSlide
    {
        public int ContentID { get; set; }
        public string TopicTitle { get; set; }
        public string ModuleTitle { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class QuizBO
    {
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public int ModuleID { get; set; }
        public string ModuleTitle { get; set; }
        public int ContentID { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public string QuizType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public bool IsPublished { get; set; }
        public double TotalScore { get; set; }
        public double PassingPercent { get; set; }
        public double PassingScore { get; set; }
    }

    public class Question
    {
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public int ModuleID { get; set; }
        public string ModuleTitle { get; set; }
        public int ContentID { get; set; }
        public string QuizTitle { get; set; }
        public int QuizID { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public string Title { get; set; }
        public string QType { get; set; }
        public bool IsBox { get; set; }
    }

    public class AnswerOption
    {
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public int ModuleID { get; set; }
        public string ModuleTitle { get; set; }
        public int QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string Title { get; set; }
        public int ParentSrNo { get; set; }
        public int SrNo { get; set; }
        public bool IsCorrect { get; set; }
        public double Score { get; set; }
    }
}
