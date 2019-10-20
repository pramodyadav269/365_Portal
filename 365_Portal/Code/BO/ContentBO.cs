using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal
{
    public class ContentBO
    {
        public  Int64 CompId { get; set; }
        public  Int64 ContentId { get; set; }

        public  Int64 ContentTypeId { get; set; }
        public string ContentType { get; set; }
        public  string ContentTitle { get; set; }
        public  string ContentDescription { get; set; }
        public  string FilePath { get; set; }
        public  string UserIds { get; set; }
        public  Int64 TopicId { get; set; }
        public string TopicIds { get; set; }
        public  string TopicTitle { get; set; }
        public  string TopicName { get; set; }
        public  string TopicDescription { get; set; }
        public  string TopicOverview { get; set; }
        public Int64 ModuleId { get; set; }
        public  string ModuleTitle { get; set; }
        public  string ModuleDescription { get; set; }
        public  string ModuleOverview { get; set; }
        public  string DocType { get; set; }
        public  bool IsPublished { get; set; }
        public  bool IsGift { get; set; }
        public  bool SkipFlashcard { get; set; }
        public  bool IsActive { get; set; }
        public  Int64 SrNo { get; set; }
        public  string MinUnlockedModules { get; set; }
        public  string CreatedBy { get; set; }


    }
}