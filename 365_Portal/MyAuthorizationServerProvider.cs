using _365_Portal.Code.DAL;
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
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (identity != null)
            {
                UserBO objUser = new UserBO();
                objUser = UserDAL.GetUserDetailsByEmailID(context.UserName, null);

                identity.AddClaim(new Claim(UserClaim.CompId, objUser.CompId.ToString()));
                identity.AddClaim(new Claim(UserClaim.UserID, objUser.UserID.ToString()));
                identity.AddClaim(new Claim(UserClaim.RoleID, objUser.RoleID.ToString()));
                identity.AddClaim(new Claim(UserClaim.Role, objUser.Role.ToString()));
                //identity.AddClaim(new Claim(UserClaim.IsFirstLogin, objUser.IsFirstLogin.ToString()));
                //identity.AddClaim(new Claim(UserClaim.ProfilePicFileID, objUser.ProfilePicFileID));
                //identity.AddClaim(new Claim(UserClaim.FirstName, objUser.FirstName));
                //identity.AddClaim(new Claim(UserClaim.LastName, objUser.LastName));
                //identity.AddClaim(new Claim(UserClaim.EmailID, objUser.EmailID));
                //identity.AddClaim(new Claim(UserClaim.MobileNum, objUser.MobileNum));
                //identity.AddClaim(new Claim(UserClaim.Position, objUser.Position));

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
                    //_usrDetails.Id = Convert.ToInt32(claims.Where(c => c.Type == UserClaim.Id).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.CompId = Convert.ToInt32(claims.Where(c => c.Type == UserClaim.CompId).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.UserID = Convert.ToString(claims.Where(c => c.Type == UserClaim.UserID).Select(c => c.Value).FirstOrDefault().ToString());
                    _usrDetails.RoleID = Convert.ToString(claims.Where(c => c.Type == UserClaim.RoleID).Select(c => c.Value).FirstOrDefault());
                    _usrDetails.Role = Convert.ToString(claims.Where(c => c.Type == UserClaim.Role).Select(c => c.Value).FirstOrDefault());
                    //_usrDetails.ProfilePicFileID = Convert.ToString(claims.Where(c => c.Type == UserClaim.ProfilePicFileID).Select(c => c.Value).FirstOrDefault());
                    //_usrDetails.FirstName = Convert.ToString(claims.Where(c => c.Type == UserClaim.FirstName).Select(c => c.Value).FirstOrDefault());
                    //_usrDetails.LastName = Convert.ToString(claims.Where(c => c.Type == UserClaim.LastName).Select(c => c.Value).FirstOrDefault());
                    //_usrDetails.EmailID = Convert.ToString(claims.Where(c => c.Type == UserClaim.EmailID).Select(c => c.Value).FirstOrDefault());
                    //_usrDetails.MobileNum = Convert.ToString(claims.Where(c => c.Type == UserClaim.MobileNum).Select(c => c.Value).FirstOrDefault());
                    //_usrDetails.Position = Convert.ToString(claims.Where(c => c.Type == UserClaim.Position).Select(c => c.Value).FirstOrDefault());

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