using _365_Portal.Code.BL;
using _365_Portal.Code;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace _365_Portal.Controllers
{
    public class TrainningController : ApiController
    {
        [Route("api/Trainning/GetTopicsByUser")]
        [HttpPost]
        public IHttpActionResult GetTopicsByUser(JObject jsonResult)
        {
            var data = "";
            try
            {
                int compId = 1;
                string userId = "4";
                var ds = TrainningBL.GetTopicsByUser(compId, userId);
                data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                APIResult.ThrowException(ex);
            }
            return new APIResult(Request, data);
        }

        [Route("api/Trainning/GetModulesByTopic")]
        [HttpPost]
        public IHttpActionResult GetModulesByTopic(JObject jsonResult)
        {
            var data = "";
            try
            {
                int compId = 1;
                string userId = "4";
                int topicId = 1;
                var ds = TrainningBL.GetModulesByTopic(compId, userId, topicId);
                data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                APIResult.ThrowException(ex);
            }
            return new APIResult(Request, data);
        }
    }
}
