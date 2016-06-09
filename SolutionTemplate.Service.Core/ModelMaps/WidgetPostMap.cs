using SolutionTemplate.BusinessModel;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTemplate.Service.Core.ModelMaps
{
    public static class WidgetPostMap
    {
        public static Widget ToDataModel(this WidgetPost model)
        {
            if (model == null)
            {
                return null;
            }

            return new Widget
            {
                Name = model.Name,
                Active = model.Active,

                Doodads = model.Doodads.ToDataModels()
            };
        }

        public static List<Widget> ToDataModels(this IEnumerable<WidgetPost> models)
        {
            return models?.Select(x => x.ToDataModel())
                .ToList();
        }
    }
}