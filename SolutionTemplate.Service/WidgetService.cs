using SharpRepository.Repository;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using SolutionTemplate.Core.ModelMappings;
using SolutionTemplate.Core.ServiceInterfaces;
using System.Collections.Generic;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Service
{
    public class WidgetService : IWidgetService
    {
        private readonly IClaims _claims;
        private readonly IRepository<Dm.Widget, int> _widgetRepo;

        public WidgetService(IClaims claims, IRepository<Dm.Widget, int> widgetRepo)
        {
            _claims = claims;
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