using _365_Portal.Models;
using _365_Portal.Code.BL;
using _365_Portal.Code;
using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Data;

namespace _365_Portal.Controllers
{
    public class ModuleController : ApiController
    {
        /// <summary>
        /// All Actions for Module password Api
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns>
        /// Result as True or false 
        /// </returns>
        [HttpPost]
        [Route("API/Module/ModuleAllAction")]
        public IHttpActionResult ModuleAllAction(JObject requestParams)
        {
            var data = string.Empty;
            int Action;
            int TopicID;
            int ModuleID;
            int CompId;
            string SrNo= string.Empty;
            string Title = string.Empty;
            string Overview = string.Empty;
            string Description = string.Empty;
            string CreatedBy = string.Empty;
            string IsPublished = string.Empty;
            string SkipFlashcard = string.Empty;
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if ((Convert.ToInt32(requestParams["Action"]) != 4 && Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["Title"].ToString()) && !string.IsNullOrEmpty(requestParams["Overview"].ToString()) && !string.IsNullOrEmpty(requestParams["Description"].ToString())))
                    {
                        CompId = identity.CompId;
                        CreatedBy = identity.UserID;
                        Action = Convert.ToInt32(requestParams["Action"]);
                        TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        ModuleID = Convert.ToInt32(requestParams["ModuleID"]);
                        SrNo = requestParams["SrNo"].ToString();
                        Overview = requestParams["Overview"].ToString();
                        Title = requestParams["Title"].ToString();
                        Description = requestParams["Description"].ToString();
                        IsPublished = requestParams["IsPublished"].ToString();
                        var ds = ModulesBL.ModulesAllAction(Action, ModuleID, TopicID, CompId, Title, Overview, Description, SrNo, IsPublished, SkipFlashcard, CreatedBy);
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
                    else if(Convert.ToInt32(requestParams["Action"]) ==4)
                    {
                        CompId = identity.CompId;
                        CreatedBy = identity.UserID;
                        Action = Convert.ToInt32(requestParams["Action"]);
                        TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        ModuleID = Convert.ToInt32(requestParams["ModuleID"]);
                        IsPublished = requestParams["IsPublished"].ToString();
                        var ds = ModulesBL.ModulesAllAction(Action, ModuleID, TopicID, CompId, Title, Overview, Description, SrNo, IsPublished, SkipFlashcard, CreatedBy);
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables["Data1"];
                            if (dt.Rows[0]["ReturnCode"].ToString() == "1")
                            {
                                DataTable _dt = ds.Tables["Data"];
                                data = Utility.ConvertDataSetToJSONString(_dt);
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
