using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Thinktecture.IdentityModel.Tokens;

namespace BankingAppNew.Web.Providers
{
    public class MyJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public MyJwtFormat()
        {
            
        }

        public string SignatureAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"; }
        }

        public string DigestAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmlenc#sha256"; }
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var issuer = WebConfigurationManager.AppSettings["Domain"];
            var audience = WebConfigurationManager.AppSettings["ClientId"];
            var key = TextEncodings.Base64Url.Decode(WebConfigurationManager.AppSettings["ClientSecret"]);
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(TimeSpan.FromDays(1).TotalMinutes);
            var signingCredentials = new SigningCredentials(
                                        new InMemorySymmetricSecurityKey(key),
                                        SignatureAlgorithm,
                                        DigestAlgorithm);
            var token = new JwtSecurityToken(issuer, audience, data.Identity.Claims,
                                             now, expires, signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}