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
using System.Linq.Expressions;

namespace SolutionTemplate.Service.Test
{
    [TestClass]
    public class WidgetServiceTests
    {
        [TestMethod]
        public void GetWidgets()
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

            widgetRepository.Setup(x => x.GetAll(It.IsAny<PagingOptions<Widget>>())).Returns(widgets);

            var service = new WidgetService(claims.Object, widgetRepository.Object, null);

            var pageResult = service.GetWidgets();

            widgetRepository.Verify(x => x.GetAll(It.IsAny<PagingOptions<Widget>>()), Times.Once);
            widgetRepository.Verify(x => x.Count(), Times.Once);

            Assert.IsNotNull(pageResult);
            Assert.AreEqual(widgets.Count, pageResult.Items.Count);
            Assert.AreEqual(widgets[0].Id, pageResult.Items[0].Id);
        }

        [TestMethod]
        public void GetWidget()
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
        public void CreateWidget()
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
        public void UpdateWidget()
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
        public void DeleteWidget()
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