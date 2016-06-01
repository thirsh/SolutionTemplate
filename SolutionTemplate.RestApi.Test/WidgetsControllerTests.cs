using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.RestApi.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;

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
                    Id = (int)DateTime.Now.Ticks,
                }
            };

            var widgetService = new Mock<IWidgetService>();

            widgetService.Setup(x => x.GetWidgets()).Returns(widgets);

            var controller = new WidgetsController(widgetService.Object);

            var actionResult = controller.Get();

            widgetService.Verify(x => x.GetWidgets(), Times.Once);

            var okResult = actionResult as OkNegotiatedContentResult<List<WidgetGet>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Content);
            Assert.AreEqual(widgets.Count, okResult.Content.Count);
            Assert.AreEqual(widgets[0].Id, okResult.Content[0].Id);
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
    }
}