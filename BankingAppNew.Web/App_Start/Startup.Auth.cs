using System;
using System.Web.Configuration;
using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Entities;
using BankingAppNew.Web.Providers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace BankingAppNew.Web
{
    public partial class Startup
    {
        static Startup()
        {
            UserManagerFactory = () => new BankUserManager(new UserStore<BankAccount>(new BankDbContext()));
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<BankUserManager> UserManagerFactory { get; set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            var issuer = WebConfigurationManager.AppSettings["Domain"];
            var audience = WebConfigurationManager.AppSettings["ClientId"];
            var secret = TextEncodings.Base64Url.Decode(WebConfigurationManager.AppSettings["ClientSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AllowedAudiences = new[] {audience},
                    IssuerSecurityTokenProviders = new[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer,secret)
                    }
                });
            ConfigureOAuthTokenGeneration(app);
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(BankDbContext.Create);
            app.CreatePerOwinContext<BankUserManager>(BankUserManager.Create);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                    AccessTokenFormat = new MyJwtFormat(),
                    Provider = new ApplicationOAuthProvider(),
                    #if DEBUG
                        AllowInsecureHttp = true
                    #endif
            };
            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}