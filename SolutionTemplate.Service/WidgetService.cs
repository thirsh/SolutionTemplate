using SharpRepository.Repository;
using SolutionTemplate.Core.ModelMappings;
using SolutionTemplate.Core.ServiceInterfaces;
using System.Collections.Generic;
using Bm = SolutionTemplate.BusinessModel;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Service
{
    public class WidgetService : IWidgetService
    {
        private readonly IRepository<Dm.Widget, int> _widgetRepo;

        public WidgetService(IRepository<Dm.Widget, int> widgetRepo)
        {
            _widgetRepo = widgetRepo;
        }

        public List<Bm.Widget> GetActiveWidgets()
        {
            return _widgetRepo
                .FindAll(x => x.Active)
                .ToBusinessModels();
        }
    }
}