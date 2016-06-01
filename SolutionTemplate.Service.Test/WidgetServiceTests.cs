using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpRepository.Repository;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using SolutionTemplate.DataModel;
using System;
using System.Collections.Generic;

namespace SolutionTemplate.Service.Test
{
    [TestClass]
    public class WidgetServiceTests
    {
        [TestMethod]
        public void ServiceShouldGetAllWidgets()
        {
            var widgets = new List<Widget>
            {
                new Widget
                {
                    Id = (int)DateTime.Now.Ticks
                }
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Widget>>();

            widgetRepository.Setup(x => x.GetAll()).Returns(widgets);

            var service = new WidgetService(claims.Object, widgetRepository.Object);

            var results = service.GetWidgets();

            widgetRepository.Verify(x => x.GetAll(), Times.Once);

            Assert.IsNotNull(results);
            Assert.AreEqual(widgets.Count, results.Count);
            Assert.AreEqual(widgets[0].Id, results[0].Id);
        }

        [TestMethod]
        public void ServiceShouldGetAWidget()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var widget = new Widget
            {
                Id = widgetId
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Widget>>();

            widgetRepository.Setup(x => x.Get(widgetId)).Returns(widget);

            var service = new WidgetService(claims.Object, widgetRepository.Object);

            var result = service.GetWidget(widgetId);

            widgetRepository.Verify(x => x.Get(widgetId), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(widgetId, result.Id);
        }

        [TestMethod]
        public void ServiceShouldCreateAWidget()
        {
            var widget = new WidgetPost
            {
                Name = "New Widget",
                Active = true
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Widget>>();

            var service = new WidgetService(claims.Object, widgetRepository.Object);

            var result = service.CreateWidget(widget);

            widgetRepository.Verify(x => x.Add(It.IsAny<Widget>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(widget.Name, result.Name);
            Assert.AreEqual(widget.Active, result.Active);
        }

        [TestMethod]
        public void ServiceShouldUpdateAWidget()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var widget = new WidgetPut
            {
                Id = widgetId,
                Name = "Existing Widget",
                Active = true
            };

            var dataWidget = new Widget
            {
                Id = widgetId,
                Name = widget.Name,
                Active = widget.Active
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Widget>>();

            widgetRepository.Setup(x => x.Get(widgetId)).Returns(dataWidget);

            var service = new WidgetService(claims.Object, widgetRepository.Object);

            var result = service.UpdateWidget(widgetId, widget);

            widgetRepository.Verify(x => x.Get(widgetId), Times.Once);
            widgetRepository.Verify(x => x.Update(It.IsAny<Widget>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(widgetId, result.Id);
            Assert.AreEqual(widget.Name, result.Name);
            Assert.AreEqual(widget.Active, result.Active);
        }
    }
}