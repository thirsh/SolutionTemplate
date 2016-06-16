using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace SolutionTemplate.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetPropertyValue(this object source, string propertyName)
        {
            return source
                .GetType()
                .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                .GetValue(source, null);
        }

        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}