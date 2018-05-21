using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleNetFramework.Utils.CSharpObjectHandling;

namespace SimpleNetFramework.Patterns
{
    internal abstract class Layer : ILayer
    {            
        protected readonly Assembly Assembly;

        protected readonly INamespace RootNamespace;

        protected Layer(INamespace rootNamespace, Assembly assembly)
        {
            this.RootNamespace = rootNamespace ?? throw new ArgumentException("Root namespace must be set.");
            this.Assembly = assembly ?? throw new ArgumentException("Assembly must be set.");
        }

        protected abstract void CheckType(Type type, Type interfaceToImplement, Type dedicatedInterface, IEnumerable<Type> dependencyInterfaces);

        protected abstract Type GetDedicatedInterface(Type type);

        protected Dictionary<Type, Type> LoadTypes(
            IEnumerable<Type> types,
            Type interfaceToImplement,
            IEnumerable<Type> dependencyInterfaces)
        {
            var dependencIntsList = dependencyInterfaces.ToList();

            var loadedTypes = new Dictionary<Type, Type>();

            foreach (var type in types)
            {
                var dedicatedInterface = GetDedicatedInterface(type);

                if (dedicatedInterface == null) { continue; }

                CheckType(type, interfaceToImplement, dedicatedInterface, dependencIntsList);

                loadedTypes.Add(dedicatedInterface, type);
            }

            return loadedTypes;
        }

        public abstract Dictionary<Type, Type> Load();
    }
}
