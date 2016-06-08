using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Entities;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.RestApi.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;

namespace SolutionTemplate.RestApi.Test
{
    [TestClass]
    public class WidgetsControllerTests
    {
        [TestMethod]
        public void GetReturnsWidgetsContentResult()
        {
            var widgets = new List<WidgetGet>
            {
                new WidgetGet
                {
                    Id = (int)DateTime.Now.Ticks
                }
            };

            var pageResult = new PageResult<WidgetGet>(1, 10, 20, widgets);

            var widgetService = new Mock<IWidgetService>();

            widgetService.Setup(x => x.GetWidgets("Id", 1, 10)).Returns(pageResult);

            var controller = new WidgetsController(widgetService.Object);

            controller.Request = new HttpRequestMessage { RequestUri = new Uri("http://localhost/api/widgets") };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute("GetWidgets", "api/{controller}");
            controller.RequestContext.RouteData = new HttpRouteData(new HttpRoute(), new HttpRouteValueDictionary { { "controller", "widgets" } });

            var responseMessage = controller.Get();

            widgetService.Verify(x => x.GetWidgets("Id", 1, 10), Times.Once);

            List<WidgetGet> responseWidgets;

            Assert.IsNotNull(responseMessage);
            Assert.IsTrue(responseMessage.IsSuccessStatusCode);
            Assert.IsTrue(responseMessage.Headers.Contains("X-Pagination"));
            Assert.IsTrue(responseMessage.TryGetContentValue(out responseWidgets));
            Assert.AreEqual(pageResult.Items.Count, responseWidgets.Count);
            Assert.AreEqual(pageResult.Items[0].Id, responseWidgets[0].Id);
        }

        [TestMethod]
        public void GetReturnsWidgetContentResultWithSameId()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var widget = new WidgetGet
            {
                Id = widgetId
            };

            var widgetService = new Mock<IWidgetService>();

            widgetService.Setup(x => x.GetWidget(widgetId)).Returns(widget);

            var controller = new WidgetsController(widgetService.Object);

            var actionResult = controller.Get(widgetId);

            widgetService.Verify(x => x.GetWidget(widgetId), Times.Once);

            var okResult = actionResult as OkNegotiatedContentResult<WidgetGet>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Content);
            Assert.AreEqual(widget.Id, okResult.Content.Id);
        }

        [TestMethod]
        public void PostSetsLocationHeader()
        {
            var widget = new WidgetPost
            {
                Name = "New Widget",
                Active = true
            };

            var resultWidget = new WidgetGet
            {
                Id = (int)DateTime.Now.Ticks,
                Name = widget.Name,
                Active = widget.Active
            };

            var widgetService = new Mock<IWidgetService>();

            widgetService.Setup(x => x.CreateWidget(widget)).Returns(resultWidget);

            var controller = new WidgetsController(widgetService.Object);

            var actionResult = controller.Post(widget);

            widgetService.Verify(x => x.CreateWidget(widget), Times.Once);

            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<WidgetGet>;

            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.IsNotNull(createdResult.RouteName);
            Assert.AreEqual(createdResult.RouteValues["id"], createdResult.Content.Id);

            Assert.AreEqual(widget.Name, createdResult.Content.Name);
            Assert.AreEqual(widget.Active, createdResult.Content.Active);
        }

        [TestMethod]
        public void PutReturnsContentResult()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var widget = new WidgetPut
            {
                Id = widgetId,
                Name = "New Widget",
                Active = true
            };

            var resultWidget = new WidgetGet
            {
                Id = widgetId,
                Name = widget.Name,
                Active = widget.Active
            };

            var widgetService = new Mock<IWidgetService>();

            widgetService.Setup(x => x.UpdateWidget(widgetId, widget)).Returns(resultWidget);

            var controller = new WidgetsController(widgetService.Object);

            var actionResult = controller.Put(widgetId, widget);

            widgetService.Verify(x => x.UpdateWidget(widgetId, widget), Times.Once);

            var okResult = actionResult as OkNegotiatedContentResult<WidgetGet>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Content);

            Assert.AreEqual(widget.Id, okResult.Content.Id);
            Assert.AreEqual(widget.Name, okResult.Content.Name);
            Assert.AreEqual(widget.Active, okResult.Content.Active);
        }

        [TestMethod]
        public void DeleteReturnsStatusCodeResult()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var widgetService = new Mock<IWidgetService>();

            var controller = new WidgetsController(widgetService.Object);

            var actionResult = controller.Delete(widgetId);

            widgetService.Verify(x => x.DeleteWidget(widgetId), Times.Once);

            var statusCodeResult = actionResult as StatusCodeResult;

            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(HttpStatusCode.NoContent, statusCodeResult.StatusCode);
        }
    }
}