using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Task1.DoNotChange;

namespace Task1
{
    public class Container
    {
        private readonly IDictionary<Type, Type> containerDictionary;

        public Container()
        {
            containerDictionary = new Dictionary<Type, Type>();
        }

        public void AddAssembly(Assembly assembly)
        {
            var types = assembly.ExportedTypes;

            foreach (var type in types)
            {
                if (HasImportConstructor(type) || HasImportProperties(type))
                {
                    containerDictionary.Add(type, type);
                }
                
                foreach (var exportAttribute in GetExportAttributes(type))
                {
                    containerDictionary.Add(exportAttribute.Contract ?? type, type);
                }
            }
        }

        public void AddType(Type type)
        {
            containerDictionary.Add(type, type);
        }

        public void AddType(Type type, Type baseType)
        {
            containerDictionary.Add(baseType, type);
            containerDictionary.Add(type, type);
        }

        public T Get<T>()
        {
            var type = typeof(T);

            var instance = (T)GetInstanceOfType(type);

            return instance;
        }

        private object GetInstanceOfType(Type type)
        {
            var hasKey = containerDictionary.TryGetValue(type, out var implementType);

            if (!hasKey)
            {
                throw new Exception();
            }

            var constructor = GetConstructor(implementType);
            var initParams = GetConstructorParams(constructor);
            var resultInstance = constructor.Invoke(initParams.ToArray());

            SetValues(resultInstance, implementType);

            return resultInstance;
        }

        private void SetValues(object instance, Type implementType)
        {
            foreach (var prop in implementType.GetProperties())
            {
                prop.SetValue(instance, GetInstanceOfType(prop.PropertyType));
            }
        }

        private ConstructorInfo GetConstructor(Type implementType)
        {
            return implementType.GetConstructors().First();
        }

        private List<object> GetConstructorParams(ConstructorInfo constructor)
        {
            var constructorParameters = constructor.GetParameters();

            return constructorParameters.Select(parameter => GetInstanceOfType(parameter.ParameterType))
                .ToList();
        }

        private bool HasImportConstructor(Type type)
        {
            return type.GetCustomAttributes<ImportConstructorAttribute>().Any();
        }
        
        private bool HasImportProperties(Type type)
        {
            return GetProperties(type).Any();
        }

        private IEnumerable<ExportAttribute> GetExportAttributes(Type type)
        {
            return type.GetCustomAttributes<ExportAttribute>();
        }
        
        private IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties().Where(x => x.GetCustomAttributes<ImportAttribute>().Any());
        }
    }
}