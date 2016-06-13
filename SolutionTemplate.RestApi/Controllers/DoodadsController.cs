using SolutionTemplate.BusinessModel;
using SolutionTemplate.RestApi.Authorization;
using SolutionTemplate.Service.Core.Interfaces;
using System.Net;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;

namespace SolutionTemplate.RestApi.Controllers
{
    [RoutePrefix("api")]
    public class DoodadsController : ApiController
    {
        private readonly IDoodadService _doodadService;

        public DoodadsController(IDoodadService doodadService)
        {
            _doodadService = doodadService;
        }

        [ResourceAuthorize(Action.Read, Resource.Doodads)]
        [Route("widgets/{widgetId}/doodads")]
        [HttpGet]
        public IHttpActionResult Find(int widgetId)
        {
            var doodads = _doodadService.FindDoodads(widgetId);

            return Ok(doodads);
        }

        [ResourceAuthorize(Action.Read, Resource.Doodads)]
        [Route("doodads/{id}", Name = "GetDoodad")]
        [Route("widgets/{widgetId}/doodads/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id, int? widgetId = null)
        {
            var doodad = _doodadService.GetDoodad(id);

            if (widgetId == null)
            {
                return Ok(doodad);
            }

            if (doodad.WidgetId == widgetId)
            {
                return Ok(doodad);
            }

            return NotFound();
        }

        [ResourceAuthorize(Action.Write, Resource.Doodads)]
        [Route("doodads")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]DoodadPost doodad)
        {
            if (doodad == null)
            {
                return BadRequest("Doodad not included in request.");
            }

            var result = _doodadService.CreateDoodad(doodad);

            return CreatedAtRoute("GetDoodad", new { id = result.Id }, result);
        }

        [ResourceAuthorize(Action.Write, Resource.Doodads)]
        [Route("doodads/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]DoodadPut doodad)
        {
            if (doodad == null)
            {
                return BadRequest("Doodad not included in request.");
            }

            var result = _doodadService.UpdateDoodad(id, doodad);

            return Ok(result);
        }

        [ResourceAuthorize(Action.Write, Resource.Doodads)]
        [Route("doodads/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _doodadService.DeleteDoodad(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}