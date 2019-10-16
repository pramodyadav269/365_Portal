using _365_Portal.Code.BL;
using MasterPayReportingModule.App_Code;
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
    public class TopicController : ApiController
    {
        [Route("api/Topic/GetUserTopics")]
        [HttpPost]
        public IHttpActionResult GetUserTopics()
        {
            var request = HttpContext.Current.Request;

            var lstData = "";
            try
            {
                var pageSize = Convert.ToInt32(request.Form["PageSize"]);
                var pageNumber = Convert.ToInt32(request.Form["PageNumber"]);
                var ds = TopicBL.GetUserTopics();
                lstData = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                APIResult.ThrowException(ex);
            }
            return new APIResult(lstData, Request);
        }
    }
}
