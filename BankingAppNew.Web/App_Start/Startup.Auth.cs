using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Jwt;
using System.Web.Http;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security;
using WebConfigurationManager = System.Web.Configuration.WebConfigurationManager;
using BankingAppNew.Web.Providers;
using BankingAppNew.DataAccess;
using BankingAppNew.DataAccess.Entities;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using Owin;

namespace BankingAppNew.Web
{
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";

            UserManagerFactory = () => new BankUserManager(new UserStore<BankAccount>(new BankDbContext()));
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<BankUserManager> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            var issuer = WebConfigurationManager.AppSettings["auth0:Domain"];
            var audience = WebConfigurationManager.AppSettings["auth0:ClientId"];
            var secret = TextEncodings.Base64Url.Decode(WebConfigurationManager.AppSettings["auth0:ClientSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    },
                });
            ConfigureOAuthTokenGeneration(app);
        }
        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(BankDbContext.Create);
            app.CreatePerOwinContext<BankUserManager>(BankUserManager.Create);
            
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat() 
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }
    }
}
