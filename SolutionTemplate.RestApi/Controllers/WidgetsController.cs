using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.RestApi.Authorization;
using System.Net;
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
        [Route("{id}", Name = "GetWidget")]
        public IHttpActionResult Get(int id)
        {
            var widget = _widgetService.GetWidget(id);

            return Ok(widget);
        }

        [ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route]
        public IHttpActionResult Get(string sort = "Id", int pageNumber = 1, int pageSize = 10)
        {
            var widgets = _widgetService.GetWidgets(sort, pageNumber, pageSize);

            return Ok(widgets);
        }

        [ResourceAuthorize(Action.Write, Resource.Widgets)]
        [Route]
        [HttpPost]
        public IHttpActionResult Post([FromBody]WidgetPost widget)
        {
            if (widget == null)
            {
                return BadRequest("Widget not included in request.");
            }

            var result = _widgetService.CreateWidget(widget);

            return CreatedAtRoute("GetWidget", new { id = result.Id }, result);
        }

        [ResourceAuthorize(Action.Write, Resource.Widgets)]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]WidgetPut widget)
        {
            if (widget == null)
            {
                return BadRequest("Widget not included in request.");
            }

            var result = _widgetService.UpdateWidget(id, widget);

            return Ok(result);
        }

        [ResourceAuthorize(Action.Write, Resource.Widgets)]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _widgetService.DeleteWidget(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}