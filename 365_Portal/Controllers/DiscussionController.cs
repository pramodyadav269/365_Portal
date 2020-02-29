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
using System.Reflection;
using Newtonsoft.Json;
using _365_Portal.Code.BO;
using _365_Portal.Models;

namespace _365_Portal.Controllers
{
    public class DiscussionController : ApiController
    {
        [Route("api/Discussion/SendMessage")]
        [HttpPost]
        public IHttpActionResult SendMessage(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    int moduleId = Convert.ToInt32(requestParams["ModuleID"].ToString());
                    var msg = Convert.ToString(requestParams["ModuleID"]);
                    var ds = DiscussionBL.SendMessage(compId, userId, moduleId, msg, Utility.GetClientIPaddress());
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    data = Utility.GetJSONData("1", "Successful", data);
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("api/Discussion/GetDiscussionChat")]
        [HttpPost]
        public IHttpActionResult GetDiscussionChat(JObject requestParams)
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                try
                {
                    int compId = identity.CompId;
                    string userId = identity.UserID;
                    int moduleId = Convert.ToInt32(requestParams["ModuleID"].ToString());
                    var ds = DiscussionBL.GetDiscussionChatByModule(compId, Convert.ToInt32(userId), moduleId);
                    data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                    data = Utility.GetJSONData("1", "Successful", data);
                }
                catch (Exception ex)
                {
                    data = Utility.Exception(ex); ;
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }
    }
}