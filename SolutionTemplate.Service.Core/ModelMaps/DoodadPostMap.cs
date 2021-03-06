﻿using SolutionTemplate.BusinessModel;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTemplate.Service.Core.ModelMaps
{
    public static class DoodadPostMap
    {
        public static Doodad ToDataModel(this DoodadPost model)
        {
            if (model == null)
            {
                return null;
            }

            return new Doodad
            {
                WidgetId = model.WidgetId,
                Widget = model.Widget.ToDataModel(),
                Name = model.Name,
                Active = model.Active
            };
        }

        public static List<Doodad> ToDataModels(this IEnumerable<DoodadPost> models)
        {
            return models?.Select(x => x.ToDataModel())
                .ToList();
        }
    }
}