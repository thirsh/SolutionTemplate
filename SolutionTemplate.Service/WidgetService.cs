using SharpRepository.Repository;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.ModelMappings;
using SolutionTemplate.Core.ServiceInterfaces;
using System.Collections.Generic;
using System.Security.Claims;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Service
{
    public class WidgetService : IWidgetService
    {
        private readonly ClaimsPrincipal _principal;
        private readonly IRepository<Dm.Widget, int> _widgetRepo;

        public WidgetService(ClaimsPrincipal principal, IRepository<Dm.Widget, int> widgetRepo)
        {
            _principal = principal;
            _widgetRepo = widgetRepo;
        }

        public List<Widget> GetWidgets()
        {
            return _widgetRepo
                .GetAll()
                .ToBusinessModels();
        }
    }
}