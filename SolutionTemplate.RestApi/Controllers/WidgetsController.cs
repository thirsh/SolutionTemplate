using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.RestApi.Authorization;
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

        [ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route]
        public IHttpActionResult Get()
        {
            var widgets = _widgetsService.GetWidgets();

            return Ok(widgets);
        }

        [ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var widget = _widgetsService.GetWidget(id);

            if (widget == null)
            {
                return NotFound();
            }

            return Ok(widget);
        }

        [ResourceAuthorize(Action.Write, Resource.Widgets)]
        [Route()]
        [HttpPost]
        public IHttpActionResult Post([FromBody]Widget widget)
        {
            if (widget == null)
            {
                return BadRequest("Widget not included in request.");
            }

            var result = _widgetsService.CreateWidget(widget);

            return Created(Request.RequestUri + "/" + result.Id, result);
        }
    }
}