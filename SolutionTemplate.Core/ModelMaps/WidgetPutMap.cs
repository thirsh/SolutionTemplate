using SolutionTemplate.BusinessModel;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTemplate.Core.ModelMaps
{
    public static class WidgetPutMap
    {
        public static Widget ToDataModel(this WidgetPut model, Widget widget)
        {
            if (model == null)
            {
                return null;
            }

            widget.Name = model.Name;
            widget.Active = model.Active;

            return widget;
        }

        public static List<Widget> ToDataModels(this IEnumerable<WidgetPut> models, IEnumerable<Widget> widgets)
        {
            if (models == null)
            {
                return null;
            }

            return models
                .Select(x => x.ToDataModel(widgets.First(w => w.Id == x.Id)))
                .ToList();
        }
    }
}