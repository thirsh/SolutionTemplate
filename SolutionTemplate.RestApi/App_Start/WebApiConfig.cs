using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SolutionTemplate.RestApi.Logging;
using System.Web.Http;

namespace SolutionTemplate.RestApi
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            config.Filters.Add(new RequestLogAttribute());
            config.Filters.Add(new ExceptionLogAttribute());

            return config;
        }
    }
}