using _365_Portal.Models;
using _365_Portal.Code.BL;
using _365_Portal.Code;
using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Data;

namespace _365_Portal.Controllers
{
    public class ContentController : ApiController
    {

        #region Topics All CRUD
        [HttpPost]
        [Route("API/Content/CreateTopic")]
        public IHttpActionResult CreateTopic(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) &&
                        !string.IsNullOrEmpty(requestParams["TopicTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["TopicOverview"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["TopicDescription"].ToString()) && !string.IsNullOrEmpty(requestParams["MinUnlockedModules"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["IsPublished"].ToString()) && !string.IsNullOrEmpty(requestParams["SrNo"].ToString())))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        content.TopicTitle = requestParams["TopicTitle"].ToString();
                        content.TopicDescription = requestParams["TopicDescription"].ToString();
                        content.TopicOverview = requestParams["TopicOverview"].ToString();
                        content.MinUnlockedModules = requestParams["MinUnlockedModules"].ToString();
                        content.IsPublished = (bool)requestParams["IsPublished"];
                        content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        var ds = ContentBL.CreateTopic(content);
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

        [HttpPost]
        [Route("API/Content/ModifyTopic")]
        public IHttpActionResult ModifyTopic(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) &&
                        !string.IsNullOrEmpty(requestParams["TopicTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["TopicOverview"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["TopicDescription"].ToString()) && !string.IsNullOrEmpty(requestParams["MinUnlockedModules"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["IsPublished"].ToString()) && !string.IsNullOrEmpty(requestParams["SrNo"].ToString())))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        content.TopicTitle = requestParams["TopicTitle"].ToString();
                        content.TopicDescription = requestParams["TopicDescription"].ToString();
                        content.TopicOverview = requestParams["TopicOverview"].ToString();
                        content.MinUnlockedModules = requestParams["MinUnlockedModules"].ToString();
                        content.IsPublished = (bool)requestParams["IsPublished"];
                        content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        var ds = ContentBL.ModifyTopic(content);
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

        [HttpPost]
        [Route("API/Content/DeleteTopic")]
        public IHttpActionResult DeleteTopic(JObject requestParams)
        {

            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        var ds = ContentBL.DeleteTopic(content);
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

        [HttpPost]
        [Route("API/Content/GetTopics")]
        public IHttpActionResult GetTopics(JObject requestParams)
        {

            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (!string.IsNullOrEmpty(requestParams["TopicName"].ToString()) && !string.IsNullOrEmpty(requestParams["IsPublished"].ToString()))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        content.TopicName = requestParams["TopicName"].ToString();
                        content.IsPublished = (bool)requestParams["IsPublished"];
                        var ds = ContentBL.GetTopics(content);
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

        [HttpPost]
        [Route("API/Content/AssignTopics")]
        public IHttpActionResult AssignTopics(JObject requestParams)
        {
            var data = string.Empty;
            return new APIResult(Request, data);

        }
        #endregion
        #region     Modules All CRUD
        [HttpPost]
        [Route("API/Content/CreateModule")]
        public IHttpActionResult CreateModule(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) &&
                        !string.IsNullOrEmpty(requestParams["ModuleTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["Overview"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["ModuleDescription"].ToString()) && !string.IsNullOrEmpty(requestParams["ModuleOverview"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["IsPublished"].ToString()) && !string.IsNullOrEmpty(requestParams["SrNo"].ToString())))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        content.ModuleTitle = requestParams["ModuleTitle"].ToString();
                        content.ModuleDescription = requestParams["ModuleDescription"].ToString();
                        content.ModuleOverview = requestParams["ModuleOverview"].ToString();
                        content.IsPublished = (bool)requestParams["IsPublished"];
                        content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        var ds = ContentBL.CreateModule(content);
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

        [HttpPost]
        [Route("API/Content/ModifyModule")]
        public IHttpActionResult ModifyModule(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) &&
                    !string.IsNullOrEmpty(requestParams["ModuleTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["Overview"].ToString()) &&
                    !string.IsNullOrEmpty(requestParams["ModuleDescription"].ToString()) && !string.IsNullOrEmpty(requestParams["ModuleOverview"].ToString()) &&
                    !string.IsNullOrEmpty(requestParams["IsPublished"].ToString()) && !string.IsNullOrEmpty(requestParams["SrNo"].ToString())))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        content.ModuleTitle = requestParams["ModuleTitle"].ToString();
                        content.ModuleDescription = requestParams["ModuleDescription"].ToString();
                        content.ModuleOverview = requestParams["ModuleOverview"].ToString();
                        content.IsPublished = (bool)requestParams["IsPublished"];
                        content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        var ds = ContentBL.ModifyModule(content);
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

        [HttpPost]
        [Route("API/Content/DeleteModule")]
        public IHttpActionResult DeleteModule(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if ((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) && (Convert.ToInt32(requestParams["ModuleID"]) != 0 && !string.IsNullOrEmpty(requestParams["ModuleID"].ToString())))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        content.ModuleID = Convert.ToInt32(requestParams["ModuleID"]);
                        var ds = ContentBL.DeleteModule(content);
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

        [HttpPost]
        [Route("API/Content/GetModules")]
        public IHttpActionResult GetModules(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if ((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) && (Convert.ToInt32(requestParams["ModuleID"]) != 0 && !string.IsNullOrEmpty(requestParams["ModuleID"].ToString()))
                        && !string.IsNullOrEmpty(requestParams["ModuleTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["IsPublished"].ToString()))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        content.ModuleID = Convert.ToInt32(requestParams["ModuleID"]);
                        content.ModuleTitle = requestParams["ModuleTitle"].ToString();
                        content.IsPublished = (bool)requestParams["IsPublished"];
                        var ds = ContentBL.GetModules(content);
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
        #endregion
        #region Content all CRUD
        [HttpPost]
        [Route("API/Content/CreateContent")]
        public IHttpActionResult CreateContent(JObject requestParams)
        {
            var data = string.Empty;
            return new APIResult(Request, data);

        }

        [HttpPost]
        [Route("API/Content/ModifyContent")]
        public IHttpActionResult ModifyContent(JObject requestParams)
        {
            var data = string.Empty;
            return new APIResult(Request, data);

        }

        [HttpPost]
        [Route("API/Content/DeleteContent")]
        public IHttpActionResult DeleteContent(JObject requestParams)
        {
            var data = string.Empty;
            return new APIResult(Request, data);

        }

        [HttpPost]
        [Route("API/Content/GetContentList")]
        public IHttpActionResult GetContentList(JObject requestParams)
        {
            var data = string.Empty;
            return new APIResult(Request, data);

        }
        #endregion
    }
}
