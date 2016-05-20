using SolutionTemplate.BusinessModel;
using System.Collections.Generic;
using System.Linq;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Core.ModelMappings
{
    public static class WidgetMapping
    {
        public static Widget ToBusinessModel(this Dm.Widget model)
        {
            if (model == null)
            {
                return null;
            }

            return new Widget
            {
                Id = model.Id,
                Name = model.Name,
                Active = model.Active,
                Created = model.Created,
                Updated = model.Updated,

                Doodads = model.Doodads.ToBusinessModels()
            };
        }

        public static List<Widget> ToBusinessModels(this IEnumerable<Dm.Widget> models)
        {
            if (models == null)
            {
                return null;
            }

            return models
                .Select(x => x.ToBusinessModel())
                .ToList();
        }

        public static Dm.Widget ToDataModel(this Widget model)
        {
            if (model == null)
            {
                return null;
            }

            return new Dm.Widget
            {
                Id = model.Id,
                Name = model.Name,
                Active = model.Active,
                Created = model.Created,
                Updated = model.Updated,

                Doodads = model.Doodads.ToDataModels()
            };
        }

        public static List<Dm.Widget> ToDataModels(this IEnumerable<Widget> models)
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