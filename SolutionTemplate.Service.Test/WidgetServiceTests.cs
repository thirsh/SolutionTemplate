using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpRepository.Repository;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using System.Collections.Generic;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Service.Test
{
    [TestClass]
    public class WidgetServiceTests
    {
        [TestMethod]
        public void ServiceShouldGetAllWidgets()
        {
            var widgets = new List<Dm.Widget>
            {
                new Dm.Widget
                {
                    Id = (int)System.DateTime.Now.Ticks
                }
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Dm.Widget>>();

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
            var widgetId = (int)System.DateTime.Now.Ticks;

            var widget = new Dm.Widget
            {
                Id = widgetId
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Dm.Widget>>();

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
            var widget = new Widget
            {
                Name = "New Widget",
                Active = true
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Dm.Widget>>();

            var service = new WidgetService(claims.Object, widgetRepository.Object);

            var result = service.CreateWidget(widget);

            widgetRepository.Verify(x => x.Add(It.IsAny<Dm.Widget>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(widget.Name, result.Name);
            Assert.AreEqual(widget.Active, result.Active);
        }
    }
}