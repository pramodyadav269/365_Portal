using _365_Portal.Models;
using _365_Portal.Code.BL;
using _365_Portal.Code;
using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Data;

namespace _365_Portal.Controllers
{
    public class TopicsController : ApiController
    {
        /// <summary>
        /// All Actions for Module password Api
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns>
        /// Result as True or false 
        /// </returns>
        [HttpPost]
        [Route("API/Topics/TopicsAllAction")]
        public IHttpActionResult TopicsAllAction(JObject requestParams)
        {
            var data = string.Empty;
            int Action;
            int TopicID;
            int CompId;
            int SrNo;
            int MinUnlockedModules;
            string Title = string.Empty;
            string Description = string.Empty;
            string CreatedBy = string.Empty;
            bool IsPublished;


            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {
                    CompId = identity.CompId;
                    CreatedBy = identity.UserID;
                    Action = Convert.ToInt32(requestParams["Action"]);
                    TopicID = Convert.ToInt32(requestParams["TopicID"]);
                    SrNo = Convert.ToInt32(requestParams["SrNo"]);
                    MinUnlockedModules = Convert.ToInt32(requestParams["MinUnlockedModules"]);
                    IsPublished = Convert.ToBoolean(requestParams["IsPublished"]);
                    if ((Action != 0 && TopicID != 0 && SrNo != 0 && MinUnlockedModules != 0))
                    {
                        var ds = TopicsBL.TopicsAllAction(Action, TopicID, CompId, Title, Description, SrNo, MinUnlockedModules, IsPublished, CreatedBy);
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables["Data"];
                            if (dt.Rows[0]["ReturnCode"].ToString() == "1")
                            {
                                data = Utility.ConvertDataSetToJSONString(dt);
                                data = Utility.Successful(data);
                            }
                            else
                            {

                                data = ConstantMessages.WebServiceLog.GenericErrorMsg;
                                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                            }

                        }
                        else
                        {
                            data = ConstantMessages.WebServiceLog.GenericErrorMsg;
                            data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                        }
                    }
                    else
                    {
                        data = ConstantMessages.WebServiceLog.InValidValues;
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }
                }
                else
                {
                    data = Utility.AuthenticationError();
                    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                }
            }
            catch (Exception ex)
            {
                data = ex.Message;
                data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
            }
            return new APIResult(Request, data);
        }
    }
}
