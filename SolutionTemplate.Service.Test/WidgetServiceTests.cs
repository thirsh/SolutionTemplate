using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpRepository.Repository;
using SharpRepository.Repository.FetchStrategies;
using SharpRepository.Repository.Queries;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using SolutionTemplate.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolutionTemplate.Service.Test
{
    [TestClass]
    public class WidgetServiceTests
    {
        [TestMethod]
        public void GetAllWidgets()
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

            var service = new WidgetService(claims.Object, widgetRepository.Object, null);

            var results = service.GetWidgets();

            widgetRepository.Verify(x => x.GetAll(), Times.Once);

            Assert.IsNotNull(results);
            Assert.AreEqual(widgets.Count, results.Count);
            Assert.AreEqual(widgets[0].Id, results[0].Id);
        }

        [TestMethod]
        public void GetAllWidgetsSorted()
        {
            var widgets = new List<Widget>
            {
                new Widget
                {
                    Name = "Widget 2"
                },
                new Widget
                {
                    Name = "Widget 1"
                }
            };

            var sort = "Name";

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Widget>>();

            widgetRepository.Setup(x => x.GetAll(It.IsAny<SortingOptions<Widget>>())).Returns(widgets.OrderBy(x => x.Name));

            var service = new WidgetService(claims.Object, widgetRepository.Object, null);

            var results = service.GetWidgets(sort);

            widgetRepository.Verify(x => x.GetAll(It.IsAny<SortingOptions<Widget>>()), Times.Once);

            Assert.IsNotNull(results);
            Assert.AreEqual(widgets.Count, results.Count);
            Assert.AreEqual(widgets[1].Name, results[0].Name);
            Assert.AreEqual(widgets[0].Name, results[1].Name);
        }

        [TestMethod]
        public void GetAWidget()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var widget = new Widget
            {
                Id = widgetId
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Widget>>();

            widgetRepository.Setup(x => x.Get(widgetId, It.IsAny<GenericFetchStrategy<Widget>>())).Returns(widget);

            var service = new WidgetService(claims.Object, widgetRepository.Object, null);

            var result = service.GetWidget(widgetId);

            widgetRepository.Verify(x => x.Get(widgetId, It.IsAny<GenericFetchStrategy<Widget>>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(widgetId, result.Id);
        }

        [TestMethod]
        public void CreateAWidget()
        {
            var widget = new WidgetPost
            {
                Name = "New Widget",
                Active = true
            };

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Widget>>();

            var service = new WidgetService(claims.Object, widgetRepository.Object, null);

            var result = service.CreateWidget(widget);

            widgetRepository.Verify(x => x.Add(It.IsAny<Widget>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(widget.Name, result.Name);
            Assert.AreEqual(widget.Active, result.Active);
        }

        [TestMethod]
        public void UpdateAWidget()
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

            var service = new WidgetService(claims.Object, widgetRepository.Object, null);

            var result = service.UpdateWidget(widgetId, widget);

            widgetRepository.Verify(x => x.Get(widgetId), Times.Once);
            widgetRepository.Verify(x => x.Update(dataWidget), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(widgetId, result.Id);
            Assert.AreEqual(widget.Name, result.Name);
            Assert.AreEqual(widget.Active, result.Active);
        }

        [TestMethod]
        public void DeleteAWidget()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var claims = new Mock<IClaims>();
            var widgetRepository = new Mock<IRepository<Widget>>();
            var doodadRepository = new Mock<IRepository<Doodad>>();

            widgetRepository.Setup(x => x.Exists(widgetId)).Returns(true);

            var service = new WidgetService(claims.Object, widgetRepository.Object, doodadRepository.Object);

            service.DeleteWidget(widgetId);

            widgetRepository.Verify(x => x.Exists(widgetId), Times.Once);
            doodadRepository.Verify(x => x.Delete(It.IsAny<Expression<Func<Doodad, bool>>>()));
            widgetRepository.Verify(x => x.Delete(widgetId), Times.Once);
        }
    }
}