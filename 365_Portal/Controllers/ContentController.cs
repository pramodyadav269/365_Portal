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
                    if ((
                        !string.IsNullOrEmpty(requestParams["TopicTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["TopicDescription"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["IsPublished"].ToString())))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        {
                            content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["TopicTitle"].ToString()))
                        {
                            content.TopicTitle = requestParams["TopicTitle"].ToString();
                        }
                        else
                        {
                            content.TopicTitle = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["TopicDescription"].ToString()))
                        {
                            content.TopicDescription = requestParams["TopicDescription"].ToString();
                        }
                        else
                        {
                            content.TopicDescription = null;
                        }

                        if (!string.IsNullOrEmpty(requestParams["IsPublished"].ToString()))
                        {
                            content.IsPublished = (bool)requestParams["IsPublished"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            content.IsPublished = (bool)requestParams["IsActive"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["MinUnlockedModules"].ToString()))
                        {
                            content.MinUnlockedModules = requestParams["MinUnlockedModules"].ToString();
                        }
                        else
                        {
                            content.MinUnlockedModules = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["SrNo"].ToString()))
                        {
                            content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        }


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
                        !string.IsNullOrEmpty(requestParams["TopicTitle"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["TopicDescription"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["IsPublished"].ToString()) && !string.IsNullOrEmpty(requestParams["SrNo"].ToString())))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        {
                            content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        }
                        //if (!string.IsNullOrEmpty(requestParams["UserID"].ToString()))
                        //{
                        //    content.UserIDs = requestParams["UserID"].ToString();
                        //}
                        if (!string.IsNullOrEmpty(requestParams["TopicTitle"].ToString()))
                        {
                            content.TopicTitle = requestParams["TopicTitle"].ToString();
                        }
                        else
                        {
                            content.TopicTitle = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["TopicDescription"].ToString()))
                        {
                            content.TopicDescription = requestParams["TopicDescription"].ToString();
                        }
                        else
                        {
                            content.TopicDescription = null;
                        }
                        //content.TopicOverview = requestParams["TopicOverview"].ToString();
                        if (!string.IsNullOrEmpty(requestParams["IsPublished"].ToString()))
                        {
                            content.IsPublished = (bool)requestParams["IsPublished"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            content.IsActive = (bool)requestParams["IsActive"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["MinUnlockedModules"].ToString()))
                        {
                            content.MinUnlockedModules = requestParams["MinUnlockedModules"].ToString();
                        }
                        else
                        {
                            content.MinUnlockedModules = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["SrNo"].ToString()))
                        {
                            content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        }
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

                    if ((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) && !string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        {
                            content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            content.IsActive = (bool)requestParams["IsActive"];
                        }
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
                    content.CompID = identity.CompId;
                    var ds = ContentBL.GetTopics(content);
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables["Data"];

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
                        !string.IsNullOrEmpty(requestParams["ModuleTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["ModuleDescription"].ToString())
                        && !string.IsNullOrEmpty(requestParams["ModuleOverview"].ToString()) && !string.IsNullOrEmpty(requestParams["IsPublished"].ToString())))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;

                        if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        {
                            content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        }

                        //if (!string.IsNullOrEmpty(requestParams["ModuleID"].ToString()))
                        //{
                        //    content.ModuleID = Convert.ToInt32(requestParams["ModuleID"]);
                        //}
                        if (!string.IsNullOrEmpty(requestParams["ModuleTitle"].ToString()))
                        {
                            content.ModuleTitle = requestParams["ModuleTitle"].ToString();
                        }
                        else
                        {
                            content.ModuleDescription = null;
                        }

                        if (!string.IsNullOrEmpty(requestParams["ModuleDescription"].ToString()))
                        {
                            content.ModuleDescription = requestParams["ModuleDescription"].ToString();
                        }
                        else
                        {
                            content.ModuleDescription = null;
                        }

                        if (!string.IsNullOrEmpty(requestParams["ModuleOverview"].ToString()))
                        {
                            content.ModuleOverview = requestParams["ModuleOverview"].ToString();
                        }
                        else
                        {
                            content.ModuleOverview = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsPublished"].ToString()))
                        {
                            content.IsPublished = (bool)requestParams["IsPublished"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["SrNo"].ToString()))
                        {
                            content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            content.IsActive = (bool)requestParams["IsActive"];
                        }
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

                                data = Utility.ConvertDataSetToJSONString(dt);
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

                    if ((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) &&
                    !string.IsNullOrEmpty(requestParams["ModuleTitle"].ToString()) && !string.IsNullOrEmpty(requestParams["ModuleOverview"].ToString()) &&
                    !string.IsNullOrEmpty(requestParams["ModuleDescription"].ToString()) && !string.IsNullOrEmpty(requestParams["IsPublished"].ToString())
                    )
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        {
                            content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        }

                        if (!string.IsNullOrEmpty(requestParams["ModuleID"].ToString()))
                        {
                            content.ModuleID = Convert.ToInt32(requestParams["ModuleID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["ModuleTitle"].ToString()))
                        {
                            content.ModuleTitle = requestParams["ModuleTitle"].ToString();
                        }
                        else
                        {
                            content.ModuleDescription = null;
                        }

                        if (!string.IsNullOrEmpty(requestParams["ModuleDescription"].ToString()))
                        {
                            content.ModuleDescription = requestParams["ModuleDescription"].ToString();
                        }
                        else
                        {
                            content.ModuleDescription = null;
                        }

                        if (!string.IsNullOrEmpty(requestParams["ModuleOverview"].ToString()))
                        {
                            content.ModuleOverview = requestParams["ModuleOverview"].ToString();
                        }
                        else
                        {
                            content.ModuleOverview = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsPublished"].ToString()))
                        {
                            content.IsPublished = (bool)requestParams["IsPublished"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["SrNo"].ToString()))
                        {
                            content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            content.IsActive = (bool)requestParams["IsActive"];
                        }
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
                                data = Utility.ConvertDataSetToJSONString(dt);
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

                    if ((Convert.ToInt32(requestParams["TopicID"]) != 0 && !string.IsNullOrEmpty(requestParams["TopicID"].ToString())) &&
                        (Convert.ToInt32(requestParams["ModuleID"]) != 0 && !string.IsNullOrEmpty(requestParams["ModuleID"].ToString()) &&
                        !string.IsNullOrEmpty("IsActive")
                        ))
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;
                        if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        {
                            content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["ModuleID"].ToString()))
                        {
                            content.ModuleID = Convert.ToInt32(requestParams["ModuleID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            content.IsActive = (bool)requestParams["IsActive"];
                        }
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
                    content.CompID = identity.CompId;
                    content.CreatedBy = identity.UserID;
                    if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                    {
                        content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                    }
                    if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                    {
                        content.IsActive = (bool)requestParams["IsActive"];
                    }
                    var ds = ContentBL.GetModules(content);
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables["Data"];
                        data = Utility.ConvertDataSetToJSONString(dt);
                        data = Utility.Successful(data);
                    }
                    else
                    {
                        data = ConstantMessages.WebServiceLog.GenericErrorMsg;
                        data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    }
                    // }
                    //else
                    //{
                    //    data = ConstantMessages.WebServiceLog.InValidValues;
                    //    data = Utility.API_Status(Convert.ToInt32(ConstantMessages.StatusCode.Failure).ToString(), data);
                    //}

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
