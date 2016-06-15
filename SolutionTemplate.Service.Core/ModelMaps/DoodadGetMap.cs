using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Extensions;
using SolutionTemplate.DataModel;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SolutionTemplate.Service.Core.ModelMaps
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
            return models?.Select(x => x.ToBusinessModel()).ToList();
        }

        public static object ToShape(this DoodadGet model, IEnumerable<string> fields)
        {
            var modelFields = fields?.Where(x => !x.Contains("."));

            if (modelFields == null || !modelFields.Any())
            {
                return null;
            }

            var shape = new ExpandoObject();

            foreach (var modelField in modelFields)
            {
                ((IDictionary<string, object>)shape).Add(modelField, model.GetPropertyValue(modelField));
            }

            return shape;
        }

        public static List<object> ToShapes(this IEnumerable<DoodadGet> models, IEnumerable<string> fields)
        {
            return models.Select(x => x.ToShape(fields)).ToList();
        }
    }
}