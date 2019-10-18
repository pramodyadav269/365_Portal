using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BO
{
    public class ContentBO
    {
        public static Int64 CompId { get; set; }
        public static Int64 ContentId { get; set; }

        public static Int64 ContentTypeId { get; set; }

        public static string ContentTitle { get; set; }
        public static string ContentDescription { get; set; }
        public static string FilePath { get; set; }
        public static string UserIds { get; set; }
        public static Int64 TopicId { get; set; }
        public static string TopicTitle { get; set; }
        public static string TopicName { get; set; }
        public static string TopicDescription { get; set; }
        public static string TopicOverview { get; set; }
        public static string ModuleTitle { get; set; }
        public static string ModuleDescription { get; set; }
        public static string ModuleOverview { get; set; }
        public static string DocType { get; set; }
        public static bool IsPublished { get; set; }
        public static bool IsGift { get; set; }
        public static bool SkipFlashcard { get; set; }
        public static bool IsActive { get; set; }
        public static Int64 SrNo { get; set; }
        public static string MinUnlockedModules { get; set; }
        public static string CreatedBy { get; set; }


    }
}