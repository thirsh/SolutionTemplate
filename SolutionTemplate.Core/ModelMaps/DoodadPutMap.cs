using SolutionTemplate.BusinessModel;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTemplate.Core.ModelMaps
{
    public static class DoodadPutMap
    {
        public static Doodad ToDataModel(this DoodadPut model, Doodad doodad)
        {
            if (model == null)
            {
                return null;
            }

            doodad.WidgetId = model.WidgetId;
            doodad.Name = model.Name;
            doodad.Active = model.Active;

            return doodad;
        }

        public static List<Doodad> ToDataModels(this IEnumerable<DoodadPut> models, IEnumerable<Doodad> doodads)
        {
            return models?.Select(x => x.ToDataModel(doodads.First(w => w.Id == x.Id)))
                .ToList();
        }
    }
}