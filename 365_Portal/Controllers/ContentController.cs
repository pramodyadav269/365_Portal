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
using System.Text.RegularExpressions;

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
        public IHttpActionResult CreateContent()
        {

            var httpRequest = HttpContext.Current.Request;
            var data = string.Empty;
            ContentBO content = new ContentBO();
            bool isValidrequest = true;

            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {
                    if (httpRequest != null)
                    {

                        if (!string.IsNullOrEmpty(httpRequest.Form["TopicID"].ToString())
                            //&& !string.IsNullOrEmpty(requestParams["ContentID"].ToString())
                            && !string.IsNullOrEmpty(httpRequest.Form["ModuleID"].ToString())
                            && !string.IsNullOrEmpty(httpRequest.Form["TypeID"].ToString())
                            && !string.IsNullOrEmpty(httpRequest.Form["DocType"].ToString())
                            //&& !string.IsNullOrEmpty(requestParams["ContentFileID"].ToString())
                            && !string.IsNullOrEmpty(httpRequest.Form["Title"].ToString())
                            && !string.IsNullOrEmpty(httpRequest.Form["Description"].ToString())
                            && !string.IsNullOrEmpty(httpRequest.Form["IsURL"].ToString())
                            //&& !string.IsNullOrEmpty(requestParams["Overview"].ToString())
                            //&& !string.IsNullOrEmpty(requestParams["IsGift"].ToString())
                            //&& !string.IsNullOrEmpty(requestParams["IsPublished"].ToString())
                            )
                        {
                            content.CompID = identity.CompId;
                            content.CreatedBy = identity.UserID;

                            if (!string.IsNullOrEmpty(httpRequest.Form["TopicID"].ToString()))
                            {
                                content.TopicID = Convert.ToInt32(httpRequest.Form["TopicID"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["ContentID"].ToString()))
                            {
                                content.ContentID = Convert.ToInt32(httpRequest.Form["ContentID"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["ModuleID"].ToString()))
                            {
                                content.ModuleID = Convert.ToInt32(httpRequest.Form["ModuleID"].ToString());
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["TypeID"].ToString()))
                            {
                                content.ContentTypeID = Convert.ToInt32(httpRequest.Form["TypeID"].ToString());
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["DocType"].ToString()))
                            {
                                content.DocType = httpRequest.Form["DocType"].ToString();
                            }
                            else
                            {
                                content.DocType = null;
                            }

                            if (System.Web.HttpContext.Current.Request.Files.Count > 0 && httpRequest.Form["IsURL"].ToString() == "0")
                            {
                                var File = HttpContext.Current.Request.Files["ContentFileID"];

                                if (File != null)
                                {
                                    string saveAsPath = string.Empty, folderName = string.Empty;
                                    var chkFileVal = Utility.CheckFileValidation(File);

                                    if (chkFileVal)
                                    {
                                        string fileName = identity.UserID + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(File.FileName);
                                        string filePath = HttpContext.Current.Server.MapPath("~/Files/Content/" + fileName);
                                        File.SaveAs(filePath); //Saving the file
                                        DataSet _ds = UserBL.CreateFile("/Files/Content/" + fileName, HttpContext.Current.Server.MapPath("~/Files/Content/"), false, "Content");
                                        if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                                        {
                                            content.ContentFileID = _ds.Tables[0].Rows[0]["UniqueID"].ToString();
                                        }
                                        else //IF Count is empty
                                        {
                                            isValidrequest = false;
                                            data = "File not uploaded. Please try again later";
                                        }
                                    }
                                    else //Invalid File Type
                                    {
                                        isValidrequest = false;
                                        data = "Invalid File Type!. Please upload valid file.";
                                    }
                                }
                                /*string ContentBase64 = Convert.ToString(requestParams.SelectToken("ContentFileID"));
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
                                }*/
                            } // URL is entered in the form
                            else if (httpRequest.Form["IsURL"].ToString() == "1")
                            {
                                if (!string.IsNullOrEmpty(httpRequest.Form["ContentFileID"].ToString()))
                                {
                                    string fileName = httpRequest.Form["ContentFileID"].ToString();
                                    Regex regex_Url = new Regex(@"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)");
                                    Match mtch_Url = regex_Url.Match(fileName);
                                    if (mtch_Url.Success)
                                    {
                                        DataSet _ds = UserBL.CreateFile(fileName, "", true, "Content");
                                        if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                                        {
                                            content.ContentFileID = _ds.Tables[0].Rows[0]["UniqueID"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        isValidrequest = false;
                                        data = "Entered Url is not in correct format";
                                    }
                                }
                                else
                                {
                                    isValidrequest = false;
                                    data = "No link provided";
                                }
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["Title"].ToString()))
                            {
                                content.ContentTitle = httpRequest.Form["Title"].ToString();
                            }
                            else
                            {
                                content.ContentTitle = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["FlashcardHighlights"].ToString()))
                            {
                                content.FlashcardHighlights = httpRequest.Form["FlashcardHighlights"].ToString();
                            }
                            else
                            {
                                content.FlashcardHighlights = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["Description"].ToString()))
                            {
                                content.ContentDescription = httpRequest.Form["Description"].ToString();
                            }
                            else
                            {
                                content.ContentDescription = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["Overview"].ToString()))
                            {
                                content.ModuleOverview = httpRequest.Form["Overview"].ToString();
                            }
                            else
                            {
                                content.ModuleOverview = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["FlashcardTitle"].ToString()))
                            {
                                content.FlashcardTitle = httpRequest.Form["FlashcardTitle"].ToString();
                            }
                            else
                            {
                                content.FlashcardTitle = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["SkipFlashcards"].ToString()))
                            {
                                content.SkipFlashcard = Convert.ToBoolean(httpRequest.Form["SkipFlashcards"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["IsGift"].ToString()))
                            {
                                content.IsGift = Convert.ToBoolean(httpRequest.Form["IsGift"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["IsPublished"].ToString()))
                            {
                                content.IsPublished = Convert.ToBoolean(httpRequest.Form["IsPublished"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["IsActive"].ToString()))
                            {
                                content.IsActive = Convert.ToBoolean(httpRequest.Form["IsActive"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["SrNo"].ToString()))
                            {
                                content.SrNo = Convert.ToInt32(httpRequest.Form["SrNo"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["TotalScore"].ToString()))
                            {
                                content.TotalScore = Convert.ToDouble(httpRequest.Form["TotalScore"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["PassingPercent"].ToString()))
                            {
                                content.PassingPercent = Convert.ToDouble(httpRequest.Form["PassingPercent"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["PassingScore"].ToString()))
                            {
                                content.PassingScore = Convert.ToDouble(httpRequest.Form["PassingScore"]);
                            }
                            if (isValidrequest == true)
                            {
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
                else
                {
                    data = "Invalid Request";
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
        public IHttpActionResult ModifyContent()
        {
            var httpRequest = HttpContext.Current.Request;
            var data = string.Empty;
            ContentBO content = new ContentBO();
            bool isValidrequest = true;
            try
            {
                var identity = MyAuthorizationServerProvider.AuthenticateUser();
                if (identity != null)
                {
                    if (httpRequest != null)
                    {

                        if (!string.IsNullOrEmpty(httpRequest.Form["TopicID"].ToString()) && !string.IsNullOrEmpty(httpRequest.Form["ContentID"].ToString())
                        && !string.IsNullOrEmpty(httpRequest.Form["ModuleID"].ToString())
                        && !string.IsNullOrEmpty(httpRequest.Form["TypeID"].ToString())
                        && !string.IsNullOrEmpty(httpRequest.Form["DocType"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["ContentFileID"].ToString())
                        && !string.IsNullOrEmpty(httpRequest.Form["Title"].ToString())
                        && !string.IsNullOrEmpty(httpRequest.Form["Description"].ToString())
                        && !string.IsNullOrEmpty(httpRequest.Form["IsURL"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["Overview"].ToString())
                        //&& !string.IsNullOrEmpty(requestParams["IsGift"].ToString())
                        // && !string.IsNullOrEmpty(requestParams["IsPublished"].ToString())
                        )
                        {
                            content.CompID = identity.CompId;
                            content.CreatedBy = identity.UserID;
                            if (!string.IsNullOrEmpty(httpRequest.Form["TopicID"].ToString()))
                            {
                                content.TopicID = Convert.ToInt32(httpRequest.Form["TopicID"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["ContentID"].ToString()))
                            {
                                content.ContentID = Convert.ToInt32(httpRequest.Form["ContentID"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["ModuleID"].ToString()))
                            {
                                content.ModuleID = Convert.ToInt32(httpRequest.Form["ModuleID"].ToString());
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["TypeID"].ToString()))
                            {
                                content.ContentTypeID = Convert.ToInt32(httpRequest.Form["TypeID"].ToString());
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["DocType"].ToString()))
                            {
                                content.DocType = httpRequest.Form["DocType"].ToString();
                            }
                            else
                            {
                                content.DocType = null;
                            }

                            if (System.Web.HttpContext.Current.Request.Files.Count > 0 && httpRequest.Form["IsURL"].ToString() == "0")
                            {
                                var File = HttpContext.Current.Request.Files["ContentFileID"];

                                if (File != null)
                                {
                                    string saveAsPath = string.Empty, folderName = string.Empty;
                                    var chkFileVal = Utility.CheckFileValidation(File);

                                    if (chkFileVal)
                                    {
                                        string fileName = identity.UserID + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(File.FileName);
                                        string filePath = HttpContext.Current.Server.MapPath("~/Files/Content/" + fileName);
                                        File.SaveAs(filePath); //Saving the file
                                        DataSet _ds = UserBL.CreateFile(fileName, HttpContext.Current.Server.MapPath("~/Files/Content/"), false, "Content");
                                        if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                                        {
                                            content.ContentFileID = _ds.Tables[0].Rows[0]["UniqueID"].ToString();
                                        }
                                        else //IF Count is empty
                                        {
                                            isValidrequest = false;
                                            data = "File not uploaded. Please try again later";
                                        }
                                    }
                                    else //Invalid File Type
                                    {
                                        isValidrequest = false;
                                        data = "Invalid File Type!. Please upload valid file.";
                                    }
                                }
                            } // URL is entered in the form
                            else if (httpRequest.Form["IsURL"].ToString() == "1")
                            {
                                if (!string.IsNullOrEmpty(httpRequest.Form["ContentFileID"].ToString()))
                                {
                                    string fileName = httpRequest.Form["ContentFileID"].ToString();
                                    Regex regex_Url = new Regex(@"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)");
                                    Match mtch_Url = regex_Url.Match(fileName);
                                    if (mtch_Url.Success)
                                    {
                                        DataSet _ds = UserBL.CreateFile(fileName, "", true, "Content");
                                        if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                                        {
                                            content.ContentFileID = _ds.Tables[0].Rows[0]["UniqueID"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        isValidrequest = false;
                                        data = "Entered link is not in correct format.";
                                    }
                                }
                                else
                                {
                                    isValidrequest = false;
                                    data = "No link provided";
                                }
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["Title"].ToString()))
                            {
                                content.ContentTitle = httpRequest.Form["Title"].ToString();
                            }
                            else
                            {
                                content.ContentTitle = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["FlashcardHighlights"].ToString()))
                            {
                                content.FlashcardHighlights = httpRequest.Form["FlashcardHighlights"].ToString();
                            }
                            else
                            {
                                content.FlashcardHighlights = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["Description"].ToString()))
                            {
                                content.ContentDescription = httpRequest.Form["Description"].ToString();
                            }
                            else
                            {
                                content.ContentDescription = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["Overview"].ToString()))
                            {
                                content.ModuleOverview = httpRequest.Form["Overview"].ToString();
                            }
                            else
                            {
                                content.ModuleOverview = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["FlashcardTitle"].ToString()))
                            {
                                content.FlashcardTitle = httpRequest.Form["FlashcardTitle"].ToString();
                            }
                            else
                            {
                                content.FlashcardTitle = null;
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["SkipFlashcards"].ToString()))
                            {
                                content.SkipFlashcard = Convert.ToBoolean(httpRequest.Form["SkipFlashcards"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["IsGift"].ToString()))
                            {
                                content.IsGift = Convert.ToBoolean(httpRequest.Form["IsGift"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["IsPublished"].ToString()))
                            {
                                content.IsPublished = Convert.ToBoolean(httpRequest.Form["IsPublished"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["IsActive"].ToString()))
                            {
                                content.IsActive = Convert.ToBoolean(httpRequest.Form["IsActive"]);
                            }
                            //if (!string.IsNullOrEmpty(httpRequest.Form["SrNo"].ToString()))
                            //{
                            //    content.SrNo = Convert.ToInt32(httpRequest.Form["SrNo"]);
                            //}
                            if (!string.IsNullOrEmpty(httpRequest.Form["TotalScore"].ToString()))
                            {
                                content.TotalScore = Convert.ToDouble(httpRequest.Form["TotalScore"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["PassingPercent"].ToString()))
                            {
                                content.PassingPercent = Convert.ToDouble(httpRequest.Form["PassingPercent"]);
                            }
                            if (!string.IsNullOrEmpty(httpRequest.Form["PassingScore"].ToString()))
                            {
                                content.PassingScore = Convert.ToDouble(httpRequest.Form["PassingScore"]);
                            }

                            if (isValidrequest == true)
                            {
                                var ds = ContentBL.ModifyContent(content);
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
                        data = "Invalid Request";
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
