using _365_Portal.DAL;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace _365_Portal
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public object UserBO { get; private set; }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //context.Validated();
            //return Task.FromResult<object>(null);
            string id, secret;
            if (context.TryGetBasicCredentials(out id, out secret))
            {
                if (secret == "secret")
                {
                    // need to make the client_id available for later security checks
                    context.OwinContext.Set<string>("as:client_id", id);
                }
            }
            context.Validated();
            return Task.FromResult<object>(null);
        }
        //End by pramod on 3 Nov 2018

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Added by pramod on 3 Nov 2018
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            //End by pramod on 3 Nov 2018

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (identity != null)
            {
                UserBO objUser = new UserBO();
                objUser = UserDAL.GetUserDetails(UserDAL.UserID,null);

                //identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                //identity.AddClaim(new Claim("username", "admin"));
                //identity.AddClaim(new Claim(ClaimTypes.Name, "Sourav Mondal"));
                identity.AddClaim(new Claim(UserClaim.Id, objUser.Id.ToString()));
                identity.AddClaim(new Claim(UserClaim.UserId, objUser.UserId.ToString()));
                identity.AddClaim(new Claim(UserClaim.ProfilePicPath, objUser.ProfilePicPath));
                identity.AddClaim(new Claim(UserClaim.FirstName, objUser.FirstName));
                identity.AddClaim(new Claim(UserClaim.LastName, objUser.LastName));
                identity.AddClaim(new Claim(UserClaim.EmailId, objUser.EmailId));
                identity.AddClaim(new Claim(UserClaim.UserPwd, objUser.UserPwd));
                identity.AddClaim(new Claim(UserClaim.MobileNo, objUser.MobileNo));
                identity.AddClaim(new Claim(UserClaim.Position, objUser.Position));
                identity.AddClaim(new Claim(UserClaim.CreatedBy, objUser.CreatedBy.ToString()));
                identity.AddClaim(new Claim(UserClaim.CreatedOn, objUser.CreatedOn));
                identity.AddClaim(new Claim(UserClaim.ModifiedBy, objUser.ModifiedBy.ToString()));
                identity.AddClaim(new Claim(UserClaim.ModifiedOn, objUser.ModifiedOn));
                identity.AddClaim(new Claim(UserClaim.IsDeleted, objUser.IsDeleted.ToString()));
                identity.AddClaim(new Claim(UserClaim.DeletedBy, objUser.DeletedBy.ToString()));
                identity.AddClaim(new Claim(UserClaim.DeletedOn, objUser.DeletedOn));

                //context.Validated(identity);
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    }
                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
            }
        }



        public static UserBO AuthenticateUser()
        {
            try
            {
                UserBO _usrDetails = new UserBO();
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                if (_usrDetails != null)
                {
                    //_usrDetails.UserId = new Guid(claims.Where(c => c.Type == CommonBO.UserData.UserId).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.Id = Convert.ToInt32(claims.Where(c => c.Type == UserClaim.Id).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.UserId = Convert.ToInt32(claims.Where(c => c.Type == UserClaim.UserId).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.ProfilePicPath = Convert.ToString(claims.Where(c => c.Type == UserClaim.ProfilePicPath).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.FirstName = Convert.ToString(claims.Where(c => c.Type == UserClaim.FirstName).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.EmailId = Convert.ToString(claims.Where(c => c.Type == UserClaim.EmailId).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.UserPwd = Convert.ToString(claims.Where(c => c.Type == UserClaim.UserPwd).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.MobileNo = Convert.ToString(claims.Where(c => c.Type == UserClaim.MobileNo).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.Position = Convert.ToString(claims.Where(c => c.Type == UserClaim.Position).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.CreatedBy = Convert.ToInt32(claims.Where(c => c.Type == UserClaim.CreatedBy).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.CreatedOn = Convert.ToString(claims.Where(c => c.Type == UserClaim.CreatedOn).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.ModifiedBy = Convert.ToInt32(claims.Where(c => c.Type == UserClaim.ModifiedBy).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.ModifiedOn = Convert.ToString(claims.Where(c => c.Type == UserClaim.ModifiedOn).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.IsDeleted = Convert.ToBoolean(claims.Where(c => c.Type == UserClaim.IsDeleted).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.DeletedBy = Convert.ToInt32(claims.Where(c => c.Type == UserClaim.DeletedBy).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.DeletedOn = Convert.ToString(claims.Where(c => c.Type == UserClaim.DeletedOn).Select(c => c.Value).FirstOrDefault());

                    return _usrDetails;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        ////Added by pramod on 3 Nov 2018
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            //validate your client
            //var currentClient = context.ClientId;

            //if (Client does not match)
            //{
            //    context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
            //    return Task.FromResult<object>(null);
            //}

            // Change authentication ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}