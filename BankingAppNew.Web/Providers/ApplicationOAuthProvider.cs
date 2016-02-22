using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace BankingAppNew.Web.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            
            var userManager = context.OwinContext.GetUserManager<BankUserManager>();
            BankAccount user = userManager.Find(context.UserName, context.Password);

            var identity = new ClaimsIdentity("otc");
            var username = context.OwinContext.Get<string>("otc:username");

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager);

            var ticket = new AuthenticationTicket(oAuthIdentity, null);

            context.Validated(ticket);
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                var username = context.Parameters["username"];
                var password = context.Parameters["password"];
                
                BankUserManager userManager = context.OwinContext.GetUserManager<BankUserManager>();

                var user = await userManager.FindAsync(username, password);

                if (user!=null)
                {
                    context.OwinContext.Set("otc:username", username);
                    context.Validated();
                }
                else
                {
                    context.SetError("Invalid credentials");
                    context.Rejected();
                }
            }
            catch
            {
                context.SetError("Server error");
                context.Rejected();
            }
        }
    }
}