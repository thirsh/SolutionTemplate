﻿using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.RestApi.Authorization;
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
        public IHttpActionResult Get(int widgetId)
        {
            var doodads = _doodadService.GetDoodads(widgetId);

            return Ok(doodads);
        }

        [ResourceAuthorize(Action.Read, Resource.Doodads)]
        [Route("doodads/{id}", Name = "GetDoodad")]
        [Route("widgets/{widgetId}/doodads/{id}")]
        public IHttpActionResult Get(int id, int? widgetId = null)
        {
            var doodad = _doodadService.GetDoodad(id);

            if (widgetId == null)
            {
                return Ok(doodad);
            }
            else
            {
                if (doodad.WidgetId == widgetId)
                {
                    return Ok(doodad);
                }

                return NotFound();
            }
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