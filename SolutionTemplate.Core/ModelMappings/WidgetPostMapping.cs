﻿using SolutionTemplate.BusinessModel;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTemplate.Core.ModelMappings
{
    public static class WidgetPostMapping
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
            if (models == null)
            {
                return null;
            }

            return models
                .Select(x => x.ToDataModel())
                .ToList();
        }
    }
}