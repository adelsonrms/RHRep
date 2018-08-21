//using Microsoft.Owin.Security.Google;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using RH.Infra.Data.DBContexts;
using System;
using System.Configuration;

[assembly: OwinStartup(typeof(ERP.Presentation.Api.App_Start.Startup))]

namespace ERP.Presentation.Api.App_Start
{
    public partial class Startup
    {

        // ID do Aplicativo cadastrado no Azure
        string clientId = ConfigurationManager.AppSettings["ClientId"];

        // RedirectUri : É a URL que será redirecionada após o login
        string redirectUrl = ConfigurationManager.AppSettings["redirectUrl"];

        // Tenant ID : É o ID da empresa
        static string tenant = ConfigurationManager.AppSettings["Tenant"];

        // Authority : É a url do serviço de autenticação da Microsoft. Ex : https://login.microsoftonline.com/contoso.onmicrosoft.com
        string authority = String.Format(System.Globalization.CultureInfo.InvariantCulture, ConfigurationManager.AppSettings["Authority"], tenant);

        /// <summary>
        /// Configure OWIN to use OpenIdConnect 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {

            //Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }


       
    }
}
