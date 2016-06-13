using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.RestApi.Controllers.V1;
using SolutionTemplate.Service.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace SolutionTemplate.RestApi.Test
{
    [TestClass]
    public class DoodadsControllerTests
    {
        [TestMethod]
        public void FindReturnsDoodadsContentResultForWidgetId()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var doodads = new List<DoodadGet>
            {
                new DoodadGet
                {
                    Id = (int)DateTime.Now.Ticks,
                }
            };

            var doodadService = new Mock<IDoodadService>();

            doodadService.Setup(x => x.FindDoodads(widgetId)).Returns(doodads);

            var controller = new DoodadsController(doodadService.Object);

            var actionResult = controller.Find(widgetId);

            doodadService.Verify(x => x.FindDoodads(widgetId), Times.Once);

            var okResult = actionResult as OkNegotiatedContentResult<List<DoodadGet>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Content);
            Assert.AreEqual(doodads.Count, okResult.Content.Count);
            Assert.AreEqual(doodads[0].Id, okResult.Content[0].Id);
        }

        [TestMethod]
        public void GetReturnsDoodadContentResultWithSameId()
        {
            var doodadId = (int)DateTime.Now.Ticks;

            var doodad = new DoodadGet
            {
                Id = doodadId
            };

            var doodadService = new Mock<IDoodadService>();

            doodadService.Setup(x => x.GetDoodad(doodadId)).Returns(doodad);

            var controller = new DoodadsController(doodadService.Object);

            var actionResult = controller.Get(doodadId);

            doodadService.Verify(x => x.GetDoodad(doodadId), Times.Once);

            var okResult = actionResult as OkNegotiatedContentResult<DoodadGet>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Content);
            Assert.AreEqual(doodad.Id, okResult.Content.Id);
        }

        [TestMethod]
        public void PostSetsLocationHeader()
        {
            var doodad = new DoodadPost
            {
                Name = "New Doodad",
                Active = true
            };

            var resultDoodad = new DoodadGet
            {
                Id = (int)DateTime.Now.Ticks,
                Name = doodad.Name,
                Active = doodad.Active
            };

            var doodadService = new Mock<IDoodadService>();

            doodadService.Setup(x => x.CreateDoodad(doodad)).Returns(resultDoodad);

            var controller = new DoodadsController(doodadService.Object);

            var actionResult = controller.Post(doodad);

            doodadService.Verify(x => x.CreateDoodad(doodad), Times.Once);

            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<DoodadGet>;

            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.IsNotNull(createdResult.RouteName);
            Assert.AreEqual(createdResult.RouteValues["id"], createdResult.Content.Id);

            Assert.AreEqual(doodad.Name, createdResult.Content.Name);
            Assert.AreEqual(doodad.Active, createdResult.Content.Active);
        }

        [TestMethod]
        public void PutReturnsContentResult()
        {
            var doodadId = (int)DateTime.Now.Ticks;

            var doodad = new DoodadPut
            {
                Id = doodadId,
                Name = "New Doodad",
                Active = true
            };

            var resultDoodad = new DoodadGet
            {
                Id = doodadId,
                Name = doodad.Name,
                Active = doodad.Active
            };

            var doodadService = new Mock<IDoodadService>();

            doodadService.Setup(x => x.UpdateDoodad(doodadId, doodad)).Returns(resultDoodad);

            var controller = new DoodadsController(doodadService.Object);

            var actionResult = controller.Put(doodadId, doodad);

            doodadService.Verify(x => x.UpdateDoodad(doodadId, doodad), Times.Once);

            var okResult = actionResult as OkNegotiatedContentResult<DoodadGet>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Content);

            Assert.AreEqual(doodad.Id, okResult.Content.Id);
            Assert.AreEqual(doodad.Name, okResult.Content.Name);
            Assert.AreEqual(doodad.Active, okResult.Content.Active);
        }

        [TestMethod]
        public void DeleteReturnsStatusCodeResult()
        {
            var doodadId = (int)DateTime.Now.Ticks;

            var doodadService = new Mock<IDoodadService>();

            var controller = new DoodadsController(doodadService.Object);

            var actionResult = controller.Delete(doodadId);

            doodadService.Verify(x => x.DeleteDoodad(doodadId), Times.Once);

            var statusCodeResult = actionResult as StatusCodeResult;

            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(HttpStatusCode.NoContent, statusCodeResult.StatusCode);
        }
    }
}