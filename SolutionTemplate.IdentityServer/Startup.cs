using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;
using SolutionTemplate.IdentityServer.Config;
using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(SolutionTemplate.IdentityServer.Startup))]

namespace SolutionTemplate.IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idApp =>
            {
                idApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Solution Template Embedded Identity Server",
                    IssuerUri = ConfigurationManager.AppSettings["IssuerUri"],

                    Factory = new IdentityServerServiceFactory()
                        .UseInMemoryUsers(Users.Get())
                        .UseInMemoryClients(Clients.Get())
                        .UseInMemoryScopes(Scopes.Get()),

                    SigningCertificate = new X509Certificate2(
                        string.Format(@"{0}\bin\Certificates\idsrv3test.pfx",
                        AppDomain.CurrentDomain.BaseDirectory), "idsrv3test"),

                    RequireSsl = bool.Parse(ConfigurationManager.AppSettings["RequireSsl"])
                });
            });
        }
    }
}