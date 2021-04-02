using System;
using System.Linq;
using System.Reflection;

namespace Task2
{
    public static class ObjectExtensions
    {
        public static void SetReadOnlyProperty(this object obj, string propertyName, object newValue)
        {
            var internalName = $"<{propertyName}>";
            var type = obj.GetType();
            var filed = FindReadOnlyProperty(type, internalName);

            filed?.SetValue(obj, newValue);
        }

        private static FieldInfo FindReadOnlyProperty(Type type, string propName)
        {
            var fields = type.GetRuntimeFields();
            var curField = fields.FirstOrDefault(x => x.Name.StartsWith(propName));
            return curField ?? FindReadOnlyProperty(type.BaseType, propName);
        }

        public static void SetReadOnlyField(this object obj, string filedName, object newValue)
        {
            var type = obj.GetType();
            var field = type.GetField(filedName);
            field?.SetValue(obj, newValue);
        }
    }
}
