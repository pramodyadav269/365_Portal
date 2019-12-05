using _365_Portal.Code;
using _365_Portal.Code.BL;
using _365_Portal.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace _365_Portal.Controllers
{
    public class OrganizationController : ApiController
    {
        [Route("API/User/GetAdminUsers")]
        [HttpPost]
        public IHttpActionResult GetAdminUsers()
        {
            var data = "";
            var identity = MyAuthorizationServerProvider.AuthenticateUser();
            if (identity != null)
            {
                UserBO objUser = new UserBO();

                if (identity.Role == ConstantMessages.Roles.companyadmin || identity.Role == ConstantMessages.Roles.superadmin)
                {
                    objUser.UserID = identity.UserID;
                    objUser.CompId = identity.CompId;
                    objUser.Role = identity.Role;                    

                    var ds = OrganizationBL.GetAdminUsers(objUser);
                    if (ds.Tables.Count > 0)
                    {
                        data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                        data = Utility.Successful(data);
                    }
                    else
                    {
                        data = Utility.API_Status("2", "No user found");
                    }
                }
                else
                {
                    data = Utility.API_Status("3", "You do not have access for this functionality");
                }
            }
            else
            {
                data = Utility.AuthenticationError();
            }
            return new APIResult(Request, data);
        }

        [Route("API/User/GetCountry")]
        [HttpPost]
        public IHttpActionResult GetCountry(JObject requestParams)
        {
            var data = "";
            var ds = OrganizationBL.GetCountry(0);
            if (ds.Tables.Count > 0)
            {
                data = Utility.ConvertDataSetToJSONString(ds.Tables[0]);
                data = Utility.Successful(data);
            }
            else
            {
                data = Utility.API_Status("2", "No data found");
            }

            return new APIResult(Request, data);
        }
    }
}