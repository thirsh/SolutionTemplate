using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpRepository.Repository;
using System.Collections.Generic;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Service.Test
{
    [TestClass]
    public class WidgetServiceTests
    {
        [TestMethod]
        public void ServiceShouldGetActiveWidgets()
        {
            var widgets = new List<Dm.Widget>
            {
                new Dm.Widget
                {
                    Id = (int)System.DateTime.Now.Ticks
                }
            };

            var widgetRepository = new Mock<IRepository<Dm.Widget, int>>();

            widgetRepository.Setup(x => x.FindAll(y => y.Active, null)).Returns(widgets);

            var service = new WidgetService(widgetRepository.Object);

            var results = service.GetActiveWidgets();

            widgetRepository.Verify(x => x.FindAll(y => y.Active, null), Times.Once);

            Assert.IsNotNull(results);
            Assert.AreEqual(widgets.Count, results.Count);
            Assert.AreEqual(widgets[0].Id, results[0].Id);
        }
    }
}