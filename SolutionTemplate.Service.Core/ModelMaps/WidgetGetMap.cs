using SolutionTemplate.BusinessModel;
using SolutionTemplate.DataModel;
using SolutionTemplate.Service.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SolutionTemplate.Service.Core.ModelMaps
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
            return models?.Select(x => x.ToBusinessModel()).ToList();
        }

        public static object ToShape(this WidgetGet model, IEnumerable<string> fields)
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

            if (!fields.Contains("Doodads", StringComparer.InvariantCultureIgnoreCase))
            {
                var doodadFields = fields
                    .Where(x => x.StartsWith("Doodads.", StringComparison.InvariantCultureIgnoreCase))
                    .Select(x => x.Remove(0, ("Doodads.").Length));

                if (doodadFields.Any())
                {
                    ((IDictionary<string, object>)shape).Add("Doodads", model.Doodads.Select(x => x.ToShape(doodadFields)));
                }
            }

            return shape;
        }

        public static List<object> ToShapes(this IEnumerable<WidgetGet> models, IEnumerable<string> fields)
        {
            return models.Select(x => x.ToShape(fields)).ToList();
        }
    }
}