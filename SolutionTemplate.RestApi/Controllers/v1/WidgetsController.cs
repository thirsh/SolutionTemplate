using NLog;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.RestApi.Authorization;
using SolutionTemplate.RestApi.Entities;
using SolutionTemplate.Service.Core.Interfaces;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;

namespace SolutionTemplate.RestApi.Controllers.V1
{
    [RoutePrefix("api/v1/widgets")]
    public class WidgetsController : ApiController
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IWidgetService _widgetService;

        public WidgetsController(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        [ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route("{id}", Name = "GetWidget")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var widget = _widgetService.GetWidget(id);

            return Ok(widget);
        }

        [ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route(Name = "GetWidgets")]
        [HttpGet]
        public HttpResponseMessage Get(string sort = "Id", int pageNumber = 1, int pageSize = 10)
        {
            _logger.Info("Getting Widgets!");

            var pageResult = _widgetService.GetWidgets(sort, pageNumber, pageSize);

            var responseMessage = Request.CreateResponse(HttpStatusCode.OK, pageResult.Items);

            responseMessage.Headers.Add("X-Pagination",
                new PaginationHeader(Request, "GetWidgets", sort, pageNumber, pageSize, pageResult.TotalCount).JsonSerialize());

            return responseMessage;
        }

        [ResourceAuthorize(Action.Read, Resource.Widgets)]
        [Route(Name = "GetWidgetsShape")]
        [HttpGet]
        public HttpResponseMessage Get(string fields, string sort = "Id", int pageNumber = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Fields not included in request."));
            }

            var pageResult = _widgetService.GetWidgets(sort, pageNumber, pageSize, fields.Split(','));

            var responseMessage = Request.CreateResponse(HttpStatusCode.OK, pageResult.Items);

            responseMessage.Headers.Add("X-Pagination",
                new PaginationHeader(Request, "GetWidgetsShape", sort, pageNumber, pageSize, pageResult.TotalCount, fields).JsonSerialize());

            return responseMessage;
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