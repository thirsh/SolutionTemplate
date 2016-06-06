using SharpRepository.Repository;
using SharpRepository.Repository.FetchStrategies;
using SharpRepository.Repository.Queries;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using SolutionTemplate.Core.Exceptions;
using SolutionTemplate.Core.ModelMaps;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Threading;

namespace SolutionTemplate.Service
{
    public class WidgetService : IWidgetService
    {
        private readonly IClaims _claims;
        private readonly IRepository<Widget, int> _widgetRepo;
        private readonly IRepository<Doodad, int> _doodadRepo;

        public WidgetService(IClaims claims, IRepository<Widget, int> widgetRepo, IRepository<Doodad, int> doodadRepo)
        {
            _claims = claims;
            _widgetRepo = widgetRepo;
            _doodadRepo = doodadRepo;
        }

        public List<WidgetGet> GetWidgets()
        {
            var widgets = _widgetRepo.GetAll();

            return widgets.ToBusinessModels();
        }

        public List<WidgetGet> GetWidgets(string sort)
        {
            var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;

            var sorting = sort.Split(',');

            //NOTE: business model fields need to be mapped to their data model fields.  might want to do sorting in memory using dynamic Linq
            var sortingOptions = new SortingOptions<Widget>(textInfo.ToTitleCase(sorting[0].TrimStart('-')), sorting[0].StartsWith("-"));

            if (sorting.Length > 1)
            {
                for (int i = 1; i < sorting.Length; i++)
                {
                    sortingOptions.ThenSortBy(textInfo.ToTitleCase(sorting[i].TrimStart('-')), sorting[i].StartsWith("-"));
                }
            }

            var widgets = _widgetRepo.GetAll(sortingOptions);

            return widgets.ToBusinessModels();
        }

        public WidgetGet GetWidget(int id)
        {
            var widget = _widgetRepo.Get(id,
                new GenericFetchStrategy<Widget>()
                    .Include(x => x.Doodads));

            if (widget == null)
            {
                throw new NotFoundException();
            }

            return widget.ToBusinessModel();
        }

        public WidgetGet CreateWidget(WidgetPost widget)
        {
            var dataWidget = widget.ToDataModel();

            _widgetRepo.Add(dataWidget);

            var result = dataWidget.ToBusinessModel();

            return result;
        }

        public WidgetGet UpdateWidget(int id, WidgetPut widget)
        {
            var dataWidget = _widgetRepo.Get(id);

            if (dataWidget == null)
            {
                throw new NotFoundException();
            }

            dataWidget = widget.ToDataModel(dataWidget);

            _widgetRepo.Update(dataWidget);

            var result = dataWidget.ToBusinessModel();

            return result;
        }

        public void DeleteWidget(int id)
        {
            if (!_widgetRepo.Exists(id))
            {
                throw new NotFoundException();
            }

            _doodadRepo.Delete(x => x.WidgetId == id);

            _widgetRepo.Delete(id);
        }
    }
}