using System.Collections.Generic;
using System.Linq;
using Bm = SolutionTemplate.BusinessModel;
using Dm = SolutionTemplate.DataModel;

namespace SolutionTemplate.Core.ModelMappings
{
    public static class Widget
    {
        public static Bm.Widget ToBusinessModel(this Dm.Widget model)
        {
            if (model == null)
            {
                return null;
            }

            return new Bm.Widget
            {
                Id = model.Id,
                Name = model.Name,
                Active = model.Active,
                Created = model.Created,
                Updated = model.Updated,

                Doodads = model.Doodads.ToBusinessModels()
            };
        }

        public static List<Bm.Widget> ToBusinessModels(this IEnumerable<Dm.Widget> models)
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