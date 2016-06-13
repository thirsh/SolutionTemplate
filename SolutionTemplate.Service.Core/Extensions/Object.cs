using System.Reflection;

namespace SolutionTemplate.Service.Core.Extensions
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
    }
}