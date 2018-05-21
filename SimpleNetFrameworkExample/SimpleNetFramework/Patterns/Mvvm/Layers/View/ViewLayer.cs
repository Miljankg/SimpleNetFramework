using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleNetFramework.Utils.CSharpObjectHandling;

namespace SimpleNetFramework.Patterns.Mvvm.Layers.View
{
    class ViewLayer : Layer, IViewLayer
    {
        public ViewLayer(INamespace rootNamespace, Assembly assembly) 
            : base(rootNamespace, assembly)
        {
        }

        protected override void CheckType(Type type, Type interfaceToImplement, Type dedicatedInterface, IEnumerable<Type> dependencyInterfaces)
        {
            // Check dedicated interface
            if (!interfaceToImplement.IsAssignableFrom(dedicatedInterface)) { throw new InvalidOperationException($"Type {type.FullName} must have a dedicated interface, which implements proper ILogic."); }

            // Check constructors
            var constructors = type.GetConstructors().Where(c => c.IsPublic).ToList();

            // Check constructor parameters
            foreach (var ctor in constructors)
            {
                var parameters = ctor.GetParameters();

                if (parameters.Any()) { throw new InvalidOperationException("View cannot have dependencies"); }
            }
        }

        protected override Type GetDedicatedInterface(Type type)
        {
            return type.GetInterfaces().SingleOrDefault(i => i.Name == $"I{type.Name}");
        }

        public override Dictionary<Type, Type> Load()
        {
            var namespaceToSearch = RootNamespace.Append(Namespace.FromString("View"));

            var types = Assembly
                .GetTypes()
                .Where(t => Namespace.FromString(t.Namespace).Path == namespaceToSearch.ToString() &&
                            !t.IsInterface)
                .ToList();

            return this.LoadTypes(
                types,
                typeof(IView),
                new List<Type>());
        }
    }
}
