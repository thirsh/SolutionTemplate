using NLog;
using SolutionTemplate.Core.Extensions;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SolutionTemplate.RestApi.Logging
{
    public class RequestLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var message = new StringBuilder();

            message.AppendLine("Request: " + actionContext.Request);
            message.AppendLine("Controller: " + actionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName);
            message.AppendLine("Action: " + actionContext.ActionDescriptor.ActionName);
            message.AppendLine("Arguments");

            foreach (var actionArgument in actionContext.ActionArguments)
            {
                message.AppendLine("  " + actionArgument.Key + ": " + actionArgument.Value.ToJson());
            }

            LogManager.GetCurrentClassLogger().Info(message.ToString());

            base.OnActionExecuting(actionContext);
        }
    }
}