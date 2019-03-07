using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Keycloak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(SampleWebApi.Startup))]
namespace SampleWebApi
{
    public class Startup
    {
        const string persistentAuthType = "keycloak_cookies"; // Or name it whatever you want
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = persistentAuthType
            });

            // You may also use this method if you have multiple authentication methods below,
            // or if you just like it better:
            app.SetDefaultSignInAsAuthenticationType(persistentAuthType);

            app.UseKeycloakAuthentication(new KeycloakAuthenticationOptions
            {
                Realm = "prueba",
                ClientId = "webapi-example",
                ClientSecret = "2458bb7b-76c5-4ffc-b5fd-57b401f47ce6",
                KeycloakUrl = "http://artemis:8085/auth/",
                //ResponseType = "id_token token",
                AuthenticationType = persistentAuthType,
                //AuthenticationMode = AuthenticationMode.Active,
                SignInAsAuthenticationType = persistentAuthType, // Not required with SetDefaultSignInAsAuthenticationType
                                                                 //Token validation options - these are all set to defaults
                AllowUnsignedTokens = false,
                DisableIssuerSigningKeyValidation = false,
                DisableIssuerValidation = false,
                DisableAudienceValidation = false,

                // DisableRefreshTokenSignatureValidation = true, // Fix for Keycloak server v4.5
                // DisableAllRefreshTokenValidation = true, // Fix for Keycloak server v4.6-4.8,  overrides DisableRefreshTokenSignatureValidation. The content of Refresh token was changed. Refresh token should not be used by the client application other than sending it to the Keycloak server to get a new Access token (where Keycloak server will validate it) - therefore validation in client application can be skipped.

                // AuthResponseErrorRedirectUrl = "/keycloak-authenticaion-error.html", //Redirect (instead of exception) when Keycloak returns error during authentication. Will include "error" query parameter.
                EnableWebApiMode = true, //!Important
                TokenClockSkew = TimeSpan.FromSeconds(2)
            });
        }
    }
}