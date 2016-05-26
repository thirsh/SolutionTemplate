using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using SharpRepository.Ioc.Ninject;
using SharpRepository.Repository.Ioc;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.DataAccess;
using SolutionTemplate.RestApi.Authorization;
using SolutionTemplate.Service;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web;

[assembly: OwinStartup(typeof(SolutionTemplate.RestApi.Startup))]

namespace SolutionTemplate.RestApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app
                .UseResourceAuthorization(new ApiAuthorizationManager())
                .UseIdentityServerBearerTokenAuthentication(
                    new IdentityServerBearerTokenAuthenticationOptions
                    {
                        Authority = ConfigurationManager.AppSettings["IdentityServerAddress"],
                        RequiredScopes = new[] { "openid", "profile", "user-profile", "solution-template-api" }
                    });

            app
                .UseNinjectMiddleware(CreateKernel)
                .UseNinjectWebApi(WebApiConfig.Register());
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.BindSharpRepository();

            RepositoryDependencyResolver.SetDependencyResolver(new NinjectDependencyResolver(kernel));

            var connectionString = ConfigurationManager.ConnectionStrings["SolutionTemplate"].ConnectionString;

            // TODO: http://www.seankenny.me/blog/2014/07/25/custom-owin-iclaimsprincipal-with-ninject/
            kernel.Bind<ClaimsPrincipal>()
                .ToMethod(x => (ClaimsPrincipal)HttpContext.Current.User);

            kernel.Bind<DbContext>()
                .To<SolutionTemplateContext>()
                .InRequestScope()
                .WithConstructorArgument("connectionString", connectionString);

            kernel.Bind<IWidgetService>().To<WidgetService>();

            return kernel;
        }
    }
}