using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleNetFramework.Patterns.Mvvm.Layers.Logic;
using SimpleNetFramework.Utils.CSharpObjectHandling;
using SimpleNetFramework.Utils.ErrorHandling;

namespace SimpleNetFramework.Patterns.Mvvm.Layers.ViewModel
{
    class ViewModelLayer : Layer, IViewModelLayer
    {
        public ViewModelLayer(INamespace rootNamespace, Assembly assembly) 
            : base(rootNamespace, assembly)
        {
        }

        protected override void CheckType(Type type, Type interfaceToImplement, Type dedicatedInterface, IEnumerable<Type> dependencyInterfaces)
        {
            // Check dedicated interface
            if (!interfaceToImplement.IsAssignableFrom(dedicatedInterface)) { throw new InvalidOperationException($"Type {type.FullName} must have a dedicated interface, which implements proper ILogic."); }

            // Check constructors
            var constructors = type.GetConstructors().Where(c => c.IsPublic).ToList();

            if (constructors.Count != 1) { throw new InvalidOperationException($"Type {type.FullName} must have only one constructor."); }

            // Check constructor parameters
            var parameters = constructors[0].GetParameters();

            if (parameters.Any(p => !p.ParameterType.IsInterface)) { throw new InvalidOperationException("Logic dependencies must be interfaces."); }

            var notSupportedParamsPresent
                = parameters.Any(p =>
                    !dependencyInterfaces.Any(di =>
                        di.IsAssignableFrom(p.ParameterType)));

            if (notSupportedParamsPresent) { throw new BadConstructorException($"Type {type.FullName} has an invalid constructor."); }
        }

        protected override Type GetDedicatedInterface(Type type)
        {
            return type.GetInterfaces().SingleOrDefault(i => i.Name == $"I{type.Name}");
        }

        public override Dictionary<Type, Type> Load()
        {
            var namespaceToSearch = RootNamespace.Append(Namespace.FromString("ViewModel"));

            var types = Assembly
                .GetTypes()
                .Where(t => Namespace.FromString(t.Namespace).Path == namespaceToSearch.ToString() &&
                            !t.IsInterface)
                .ToList();

            if (!types.Any()) { throw new InvalidOperationException("There are no ViewModels defined."); }

            return this.LoadTypes(
                types,
                typeof(IViewModel),
                new List<Type>() { typeof(ILogic) });
        }
    }
}
