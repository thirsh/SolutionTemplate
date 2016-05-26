using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpRepository.Repository;
using System.Collections.Generic;
using System.Security.Claims;
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

            var principal = new Mock<ClaimsPrincipal>();
            var widgetRepository = new Mock<IRepository<Dm.Widget>>();

            widgetRepository.Setup(x => x.GetAll()).Returns(widgets);

            var service = new WidgetService(principal.Object, widgetRepository.Object);

            var results = service.GetWidgets();

            widgetRepository.Verify(x => x.GetAll(), Times.Once);

            Assert.IsNotNull(results);
            Assert.AreEqual(widgets.Count, results.Count);
            Assert.AreEqual(widgets[0].Id, results[0].Id);
        }
    }
}