using _365_Portal.Code.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BL
{
    public class TopicBL
    {
        public static DataSet GetUserTopics()
        {
            return TopicDAL.GetUserTopics();
        }
    }
}