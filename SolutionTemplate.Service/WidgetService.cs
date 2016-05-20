using SharpRepository.Repository;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.ModelMappings;
using SolutionTemplate.Core.ServiceInterfaces;
using System.Collections.Generic;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Service
{
    public class WidgetService : IWidgetService
    {
        private readonly IRepository<Dm.Widget> _widgetRepo;

        public WidgetService(IRepository<Dm.Widget> widgetRepo)
        {
            _widgetRepo = widgetRepo;
        }

        public List<Widget> GetActiveWidgets()
        {
            return _widgetRepo
                .FindAll(x => x.Active)
                .ToBusinessModels();
        }
    }
}