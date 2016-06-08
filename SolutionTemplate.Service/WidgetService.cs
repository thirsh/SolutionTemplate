﻿using SharpRepository.Repository;
using SharpRepository.Repository.FetchStrategies;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using SolutionTemplate.Core.Entities;
using SolutionTemplate.Core.Exceptions;
using SolutionTemplate.Core.Extensions;
using SolutionTemplate.Core.ModelMaps;
using SolutionTemplate.Core.ServiceInterfaces;
using SolutionTemplate.DataModel;

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

        public PageResult<WidgetGet> GetWidgets(string sort = "Id", int pageNumber = 1, int pageSize = 10)
        {
            var widgets = _widgetRepo.GetAll(sort.ToPagingOptions<Widget>(pageNumber, pageSize));
            var totalCount = _widgetRepo.Count();

            return new PageResult<WidgetGet>(pageNumber, pageSize, totalCount, widgets.ToBusinessModels());
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