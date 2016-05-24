using SolutionTemplate.Core.ServiceInterfaces;
using System.Web.Http;

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

        [Route]
        public IHttpActionResult GetWidget()
        {
            var widgets = _widgetsService.GetWidgets();

            return Ok(widgets);
        }
    }
}