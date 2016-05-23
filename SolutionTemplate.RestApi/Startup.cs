using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using SharpRepository.Ioc.Ninject;
using SharpRepository.Repository;
using SharpRepository.Repository.Ioc;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.DataAccess;
using SolutionTemplate.DataModel;
using SolutionTemplate.Service;
using System.Configuration;
using System.Data.Entity;

[assembly: OwinStartup(typeof(SolutionTemplate.RestApi.Startup))]

namespace SolutionTemplate.RestApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .UseNinjectMiddleware(CreateKernel)
                .UseNinjectWebApi(WebApiConfig.Register());
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            RegisterServices(kernel);

            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.BindSharpRepository();

            RepositoryDependencyResolver.SetDependencyResolver(new NinjectDependencyResolver(kernel));

            var connectionString = ConfigurationManager.ConnectionStrings["SolutionTemplate"].ConnectionString;

            kernel.Bind<DbContext>()
                .To<SolutionTemplateContext>()
                .InRequestScope()
                .WithConstructorArgument("connectionString", connectionString);

            kernel.Bind<IWidgetService>().To<WidgetService>();
        }
    }
}