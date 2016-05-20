using System.Collections.Generic;
using System.Linq;
using Bm = SolutionTemplate.BusinessModel;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Core.ModelMappings
{
    public static class Doodad
    {
        public static Bm.Doodad ToBusinessModel(this Dm.Doodad model)
        {
            if (model == null)
            {
                return null;
            }

            return new Bm.Doodad
            {
                Id = model.Id,
                WidgetId = model.WidgetId,
                Widget = model.Widget.ToBusinessModel(),
                Name = model.Name,
                Active = model.Active,
                Created = model.Created,
                Updated = model.Updated
            };
        }

        public static List<Bm.Doodad> ToBusinessModels(this IEnumerable<Dm.Doodad> models)
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