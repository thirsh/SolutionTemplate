using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.RestApi.Authorization;
using System.Security.Claims;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;

namespace SolutionTemplate.RestApi.Controllers
{
    [RoutePrefix("api/Widgets")]
    public class WidgetsController : ApiController
    {
        private readonly IWidgetService _widgetsService;

        public WidgetsController(IWidgetService widgetService)
        {
            _widgetsService = widgetService;
        }

        //[ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route]
        public IHttpActionResult GetWidget()
        {
            var identity = User.Identity as ClaimsIdentity;

            var widgets = _widgetsService.GetWidgets();

            return Ok(widgets);
        }
    }
}