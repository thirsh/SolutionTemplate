using SharpRepository.Repository;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using SolutionTemplate.Core.Exceptions;
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
            var widgets = _widgetRepo.GetAll();

            return widgets.ToBusinessModels();
        }

        public Widget GetWidget(int id)
        {
            var widget = _widgetRepo.Get(id);

            return widget.ToBusinessModel();
        }

        public Widget CreateWidget(Widget widget)
        {
            var dataWidget = widget.ToDataModel();

            _widgetRepo.Add(dataWidget);

            var result = dataWidget.ToBusinessModel();

            return result;
        }

        public Widget UpdateWidget(int id, Widget widget)
        {
            var dataWidget = _widgetRepo.Get(id);

            if (dataWidget == null)
            {
                throw new NotFoundException();
            }

            dataWidget.Name = widget.Name;
            dataWidget.Active = widget.Active;

            _widgetRepo.Update(dataWidget);

            var result = dataWidget.ToBusinessModel();

            return result;
        }
    }
}