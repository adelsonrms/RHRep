using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ERP.Presentation.Api.App_Start.Startup))]

namespace ERP.Presentation.Api.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           #pragma warning disable IDE0022 // Use expression body for methods
            ConfigureAuth(app);
        }
    }
}
