using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.RestApi.Authorization;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;

namespace SolutionTemplate.RestApi.Controllers
{
    [RoutePrefix("api/widgets")]
    public class WidgetsController : ApiController
    {
        private readonly IWidgetService _widgetService;

        public WidgetsController(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        [ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route]
        public IHttpActionResult Get()
        {
            var widgets = _widgetService.GetWidgets();

            return Ok(widgets);
        }

        [ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route("{id}", Name = "GetWidget")]
        public IHttpActionResult Get(int id)
        {
            var widget = _widgetService.GetWidget(id);

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

            var result = _widgetService.CreateWidget(widget);

            return CreatedAtRoute("GetWidget", new { id = result.Id }, result);
        }
    }
}