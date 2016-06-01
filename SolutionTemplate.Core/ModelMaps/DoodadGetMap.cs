using SolutionTemplate.BusinessModel;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTemplate.Core.ModelMaps
{
    public static class DoodadGetMap
    {
        public static DoodadGet ToBusinessModel(this Doodad model)
        {
            if (model == null)
            {
                return null;
            }

            return new DoodadGet
            {
                Id = model.Id,
                WidgetId = model.WidgetId,
                Name = model.Name,
                Active = model.Active,
                Created = model.CreatedUtc,
                Updated = model.UpdatedUtc
            };
        }

        public static List<DoodadGet> ToBusinessModels(this IEnumerable<Doodad> models)
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