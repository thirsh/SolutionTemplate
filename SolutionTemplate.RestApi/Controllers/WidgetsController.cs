using SharpRepository.Repository;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.DataModel;
using SolutionTemplate.Service;
using System.Web.Http;

namespace SolutionTemplate.RestApi.Controllers
{
    public class WidgetsController : ApiController
    {
        private readonly IWidgetService _widgetsService;

        //public WidgetsController(IWidgetService widgetService)
        //{
        //    _widgetsService = widgetService;
        //}

        public WidgetsController()
        {
            _widgetsService = new WidgetService(RepositoryFactory.GetInstance<Widget, int>());
        }

        public IHttpActionResult GetWidget()
        {
            var widgets = _widgetsService.GetWidgets();

            return Ok(widgets);
        }
    }
}