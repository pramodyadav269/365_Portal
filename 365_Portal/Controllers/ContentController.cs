using _365_Portal.Models;
using _365_Portal.Code.BL;
using _365_Portal.Code;
using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;

namespace _365_Portal.ControllersReOrderContent
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
                        //if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        //{
                        //    content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
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


                        var ds = ContentBL.CreateTopic(content);

                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
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

                    if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()) && !string.IsNullOrEmpty(requestParams["TopicTitle"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["TopicDescription"].ToString()) &&
                        !string.IsNullOrEmpty(requestParams["IsPublished"].ToString()) && !string.IsNullOrEmpty(requestParams["SrNo"].ToString()))
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
                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
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

                    if ((!string.IsNullOrEmpty(requestParams["TopicID"].ToString())) && !string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
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
                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
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
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables["Data"];
                        if (ds.Tables.Count > 0)
                        {
                            data = Utility.ConvertDataSetToJSONString(dt);
                            data = Utility.Successful(data);
                        }
                        else
                        {
                            data = dt.Rows[0]["ReturnMessage"].ToString();
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

                    if ((!string.IsNullOrEmpty(requestParams["TopicID"].ToString()) &&
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
                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
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

                    if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()) &&
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
                        if (ds != null)
                        {
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
                                    data = dt.Rows[0]["ReturnMessage"].ToString();
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

                    if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()) && !string.IsNullOrEmpty(requestParams["ModuleID"].ToString()) && !string.IsNullOrEmpty("IsActive"))
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
                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
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
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables["Data"];
                        if (ds.Tables.Count > 0)
                        {

                            data = Utility.ConvertDataSetToJSONString(dt);
                            data = Utility.Successful(data);
                        }
                        else
                        {
                            data = dt.Rows[0]["ReturnMessage"].ToString();
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
        #endregion
        #region Content all CRUD
        [HttpPost]
        [Route("API/Content/CreateContent")]
        public IHttpActionResult CreateContent(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["ContentID"].ToString())
                        && !string.IsNullOrEmpty(requestParams["ModuleID"].ToString())
                        && !string.IsNullOrEmpty(requestParams["TypeID"].ToString())
                        && !string.IsNullOrEmpty(requestParams["DocType"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["ContentFileID"].ToString())
                        && !string.IsNullOrEmpty(requestParams["Title"].ToString())
                        && !string.IsNullOrEmpty(requestParams["Description"].ToString())
                        && !string.IsNullOrEmpty(requestParams["IsURL"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["Overview"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["IsGift"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["IsPublished"].ToString())
                        )
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;

                        if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        {
                            content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["ContentID"].ToString()))
                        {
                            content.ContentID = Convert.ToInt32(requestParams["ContentID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["ModuleID"].ToString()))
                        {
                            content.ModuleID = Convert.ToInt32(requestParams["ModuleID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(requestParams["TypeID"].ToString()))
                        {
                            content.ContentTypeID = Convert.ToInt32(requestParams["TypeID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(requestParams["DocType"].ToString()))
                        {
                            content.DocType = requestParams["DocType"].ToString();
                        }
                        else
                        {
                            content.DocType = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["ContentFileID"].ToString()) && requestParams["IsURL"].ToString() == "0")
                        {
                            string ContentBase64 = Convert.ToString(requestParams.SelectToken("ContentFileID"));
                            if (!string.IsNullOrEmpty(ContentBase64))
                            {
                                var files = ContentBase64.Split(new string[] { "," }, StringSplitOptions.None);
                                if (files.Count() == 1)
                                    ContentBase64 = files[0];
                                else
                                    ContentBase64 = files[1];
                                byte[] imageBytes = Convert.FromBase64String(ContentBase64);
                                string fileName = identity.UserID + "_" + Guid.NewGuid() + "." + Utility.GetFileExtension(ContentBase64);
                                string filePath = HttpContext.Current.Server.MapPath("~/Files/Content/" + fileName);
                                File.WriteAllBytes(filePath, imageBytes);

                                DataSet _ds = UserBL.CreateFile(fileName, HttpContext.Current.Server.MapPath("~/Files/Content/"), false, "Content");
                                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                                {
                                    content.ContentFileID = _ds.Tables[0].Rows[0]["UniqueID"].ToString();
                                }
                            }
                        }
                        else
                        {
                            string fileName =requestParams["ContentFileID"].ToString();
                            DataSet _ds = UserBL.CreateFile(fileName, "", true, "Content");
                            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                            {
                                content.ContentFileID = _ds.Tables[0].Rows[0]["UniqueID"].ToString();
                            }
                        }
                        if (!string.IsNullOrEmpty(requestParams["Title"].ToString()))
                        {
                            content.ContentTitle = requestParams["Title"].ToString();
                        }
                        else
                        {
                            content.ContentTitle = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["FlashcardHighlights"].ToString()))
                        {
                            content.FlashcardHighlights = requestParams["FlashcardHighlights"].ToString();
                        }
                        else
                        {
                            content.FlashcardHighlights = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["Description"].ToString()))
                        {
                            content.ContentDescription = requestParams["Description"].ToString();
                        }
                        else
                        {
                            content.ContentDescription = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["Overview"].ToString()))
                        {
                            content.ModuleOverview = requestParams["Overview"].ToString();
                        }
                        else
                        {
                            content.ModuleOverview = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["FlashcardTitle"].ToString()))
                        {
                            content.FlashcardTitle = requestParams["FlashcardTitle"].ToString();
                        }
                        else
                        {
                            content.FlashcardTitle = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["SkipFlashcards"].ToString()))
                        {
                            content.SkipFlashcard = (bool)requestParams["SkipFlashcards"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsGift"].ToString()))
                        {
                            content.IsGift = (bool)requestParams["IsGift"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsPublished"].ToString()))
                        {
                            content.IsPublished = (bool)requestParams["IsPublished"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            content.IsActive = (bool)requestParams["IsActive"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["SrNo"].ToString()))
                        {
                            content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["TotalScore"].ToString()))
                        {
                            content.TotalScore = Convert.ToDouble(requestParams["TotalScore"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["PassingPercent"].ToString()))
                        {
                            content.PassingPercent = Convert.ToDouble(requestParams["PassingPercent"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["PassingScore"].ToString()))
                        {
                            content.PassingScore = Convert.ToDouble(requestParams["PassingScore"]);
                        }
                        var ds = ContentBL.CreateContent(content);
                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
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
        [Route("API/Content/ModifyContent")]
        public IHttpActionResult ModifyContent(JObject requestParams)
        {
            var data = string.Empty;
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()) && !string.IsNullOrEmpty(requestParams["ContentID"].ToString())
                        && !string.IsNullOrEmpty(requestParams["ModuleID"].ToString())
                        && !string.IsNullOrEmpty(requestParams["TypeID"].ToString())
                        && !string.IsNullOrEmpty(requestParams["DocType"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["ContentFileID"].ToString())
                        && !string.IsNullOrEmpty(requestParams["Title"].ToString())
                        && !string.IsNullOrEmpty(requestParams["Description"].ToString())
                        && !string.IsNullOrEmpty(requestParams["IsURL"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["Overview"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["IsGift"].ToString())
                        // && !string.IsNullOrEmpty(requestParams["IsPublished"].ToString())
                        )
                    {
                        content.CompID = identity.CompId;
                        content.CreatedBy = identity.UserID;

                        if (!string.IsNullOrEmpty(requestParams["TopicID"].ToString()))
                        {
                            content.TopicID = Convert.ToInt32(requestParams["TopicID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["ContentID"].ToString()))
                        {
                            content.ContentID = Convert.ToInt32(requestParams["ContentID"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["ModuleID"].ToString()))
                        {
                            content.ModuleID = Convert.ToInt32(requestParams["ModuleID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(requestParams["TypeID"].ToString()))
                        {
                            content.ContentTypeID = Convert.ToInt32(requestParams["TypeID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(requestParams["DocType"].ToString()))
                        {
                            content.DocType = requestParams["DocType"].ToString();
                        }
                        else
                        {
                            content.DocType = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["ContentFileID"].ToString()) && requestParams["IsURL"].ToString() == "0")
                        {
                            string ContentBase64 = Convert.ToString(requestParams.SelectToken("ContentFileID"));
                            if (!string.IsNullOrEmpty(ContentBase64))
                            {
                                var files = ContentBase64.Split(new string[] { "," }, StringSplitOptions.None);
                                if (files.Count() == 1)
                                    ContentBase64 = files[0];
                                else
                                    ContentBase64 = files[1];
                                byte[] imageBytes = Convert.FromBase64String(ContentBase64);
                                string fileName = identity.UserID + "_" + Guid.NewGuid() + "." + Utility.GetFileExtension(ContentBase64);
                                string filePath = HttpContext.Current.Server.MapPath("~/Files/Content/" + fileName);
                                File.WriteAllBytes(filePath, imageBytes);

                                DataSet _ds = UserBL.CreateFile(fileName, HttpContext.Current.Server.MapPath("~/Files/Content/"), false, "Content");
                                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                                {
                                    content.ContentFileID = _ds.Tables[0].Rows[0]["UniqueID"].ToString();
                                }
                            }
                        }
                        else
                        {
                            string fileName = requestParams["ContentFileID"].ToString();
                            DataSet _ds = UserBL.CreateFile(fileName, "", true, "Content");
                            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                            {
                                content.ContentFileID = _ds.Tables[0].Rows[0]["UniqueID"].ToString();
                            }
                        }
                        if (!string.IsNullOrEmpty(requestParams["Title"].ToString()))
                        {
                            content.ContentTitle = requestParams["Title"].ToString();
                        }
                        else
                        {
                            content.ContentTitle = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["FlashcardHighlights"].ToString()))
                        {
                            content.FlashcardHighlights = requestParams["FlashcardHighlights"].ToString();
                        }
                        else
                        {
                            content.FlashcardHighlights = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["Description"].ToString()))
                        {
                            content.ContentDescription = requestParams["Description"].ToString();
                        }
                        else
                        {
                            content.ContentDescription = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["Overview"].ToString()))
                        {
                            content.ModuleOverview = requestParams["Overview"].ToString();
                        }
                        else
                        {
                            content.ModuleOverview = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["FlashcardTitle"].ToString()))
                        {
                            content.FlashcardTitle = requestParams["FlashcardTitle"].ToString();
                        }
                        else
                        {
                            content.FlashcardTitle = null;
                        }
                        if (!string.IsNullOrEmpty(requestParams["SkipFlashcards"].ToString()))
                        {
                            content.SkipFlashcard = (bool)requestParams["SkipFlashcards"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsGift"].ToString()))
                        {
                            content.IsGift = (bool)requestParams["IsGift"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsPublished"].ToString()))
                        {
                            content.IsPublished = (bool)requestParams["IsPublished"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["IsActive"].ToString()))
                        {
                            content.IsActive = (bool)requestParams["IsActive"];
                        }
                        if (!string.IsNullOrEmpty(requestParams["SrNo"].ToString()))
                        {
                            content.SrNo = Convert.ToInt32(requestParams["SrNo"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["TotalScore"].ToString()))
                        {
                            content.TotalScore = Convert.ToDouble(requestParams["TotalScore"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["PassingPercent"].ToString()))
                        {
                            content.PassingPercent = Convert.ToDouble(requestParams["PassingPercent"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["PassingScore"].ToString()))
                        {
                            content.PassingScore = Convert.ToDouble(requestParams["PassingScore"]);
                        }
                        var ds = ContentBL.ModifyContent(content);
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

                                data = dt.Rows[0]["ReturnMessage"].ToString();
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
        [Route("API/Content/DeleteContent")]
        public IHttpActionResult DeleteContent(JObject requestParams)
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
                    if (!string.IsNullOrEmpty(requestParams["p_ContentID"].ToString()))
                    {
                        content.ContentID = Convert.ToInt32(requestParams["p_ContentID"]);
                    }
                    var ds = ContentBL.DeleteContent(content);
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables["Data"];
                        if (ds.Tables.Count > 0)
                        {

                            data = Utility.ConvertDataSetToJSONString(dt);
                            data = Utility.Successful(data);
                        }
                        else
                        {
                            data = dt.Rows[0]["ReturnMessage"].ToString();
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
        [Route("API/Content/GetContentList")]
        public IHttpActionResult GetContentList(JObject requestParams)
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
                    if (!string.IsNullOrEmpty(requestParams["ContentID"].ToString()))
                    {
                        content.ContentID = Convert.ToInt32(requestParams["ContentID"]);
                    }
                    if (!string.IsNullOrEmpty(requestParams["ModuleID"].ToString()))
                    {
                        content.ModuleID = Convert.ToInt32(requestParams["ModuleID"]);
                    }
                    if (!string.IsNullOrEmpty(requestParams["ContentTypeID"].ToString()))
                    {
                        content.ContentTypeID = Convert.ToInt32(requestParams["ContentTypeID"]);
                    }
                    if (!string.IsNullOrEmpty(requestParams["IsGift"].ToString()))
                    {
                        content.IsGift = (bool)requestParams["IsGift"];
                    }
                    var ds = ContentBL.GetContentList(content);
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables["Data"];
                        if (ds.Tables.Count > 0)
                        {

                            data = Utility.ConvertDataSetToJSONString(dt);
                            data = Utility.Successful(data);
                        }
                        else
                        {
                            data = dt.Rows[0]["ReturnMessage"].ToString();
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

        #region Table Reordering API
        [HttpPost]
        [Route("API/Content/ReOrderContent")]
        public IHttpActionResult ReOrderContent(JObject requestParams)
        {
            var data = string.Empty;
            var compid = 0;
            var userid = "";
            var Type = 0;
            var IDs = "";
            ContentBO content = new ContentBO();
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {

                    if (!string.IsNullOrEmpty(requestParams["Type"].ToString()) && !string.IsNullOrEmpty(requestParams["IDs"].ToString()))
                    {
                        compid = identity.CompId;
                        userid = identity.UserID;

                        if (!string.IsNullOrEmpty(requestParams["Type"].ToString()))
                        {
                            Type = Convert.ToInt32(requestParams["Type"]);
                        }
                        if (!string.IsNullOrEmpty(requestParams["IDs"].ToString()))
                        {
                            IDs = requestParams["IDs"].ToString();
                        }
                        var ds = ContentBL.ReorderContent(compid, userid, Type, IDs);
                        if (ds != null)
                        {
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

                                    data = dt.Rows[0]["ReturnMessage"].ToString();
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
    }
}
