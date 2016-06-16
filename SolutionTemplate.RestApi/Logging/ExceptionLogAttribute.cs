using NLog;
using SolutionTemplate.Core.Extensions;
using SolutionTemplate.Service.Core.Exceptions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;

namespace SolutionTemplate.RestApi.Logging
{
    public class ExceptionLogAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var message = new StringBuilder();

            message.AppendLine("Request: " + actionExecutedContext.Request);
            message.AppendLine("Controller: " + actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName);
            message.AppendLine("Action: " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
            message.AppendLine("Arguments");

            foreach (var actionArgument in actionExecutedContext.ActionContext.ActionArguments)
            {
                message.AppendLine("  " + actionArgument.Key + ": " + actionArgument.Value.ToJson());
            }

            LogManager.GetCurrentClassLogger().Error(actionExecutedContext.Exception, message.ToString());

            var exceptionType = actionExecutedContext.Exception.GetType();

            if (exceptionType == typeof(NotFoundException))
            {
                throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound));
            }

            throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError));
        }
    }
}