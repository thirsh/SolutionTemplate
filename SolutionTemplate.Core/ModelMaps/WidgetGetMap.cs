using SolutionTemplate.BusinessModel;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTemplate.Core.ModelMaps
{
    public static class WidgetGetMap
    {
        public static WidgetGet ToBusinessModel(this Widget model)
        {
            if (model == null)
            {
                return null;
            }

            return new WidgetGet
            {
                Id = model.Id,
                Name = model.Name,
                Active = model.Active,
                Created = model.CreatedUtc,
                Updated = model.UpdatedUtc,

                Doodads = model.Doodads.ToBusinessModels()
            };
        }

        public static List<WidgetGet> ToBusinessModels(this IEnumerable<Widget> models)
        {
            if (models == null)
            {
                return null;
            }

            return models
                .Select(x => x.ToBusinessModel())
                .ToList();
        }
    }
}