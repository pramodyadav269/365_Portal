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
        [Route("api/Trainning/GetUserTopics")]
        [HttpPost]
        public IHttpActionResult GetUserTopics(JObject jsonResult)
        {
            var data = "";
            try
            {
                var ds = TrainningBL.GetTopicsByUser(1, "4");
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
