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
            var typesToReturn = new Dictionary<Type, Type>();
            
            var namespaceToSearch = RootNamespace.Append(Namespace.FromString("ViewModel"));

            var allTypes = Assembly.GetTypes();

            var vmTypes = allTypes
                .Where(t => Namespace.FromString(t.Namespace).Path == namespaceToSearch.ToString() &&
                            !t.IsInterface)
                .ToList();

            if (!vmTypes.Any()) { throw new InvalidOperationException("There are no ViewModels defined."); }

            // Load ViewModelLocator
            var vmLocType = GetViewModelLocatorType(allTypes, namespaceToSearch);

            if (vmLocType != null)
            {
                typesToReturn.Add(vmLocType.Item1, vmLocType.Item2);    
            }

            // LoadViewModels
            var loadedVmTypes = this.LoadTypes(
                vmTypes,
                typeof(IViewModel),
                new List<Type>() { typeof(ILogic) });

            return typesToReturn
                .Concat(loadedVmTypes)
                .ToDictionary(k => k.Key, v => v.Value);
        }

        private Tuple<Type, Type> GetViewModelLocatorType(IEnumerable<Type> loadedTypes, INamespace rootNamespace)
        {
            var viewModelLocators = loadedTypes
                .Where(t => typeof(ViewModelLocator).IsAssignableFrom(t) && t.Namespace == rootNamespace.ToString())
                .ToList();

            if (viewModelLocators.Count > 1) { throw new InvalidOperationException("More than one ViewModelLocator detected."); }

            if (!viewModelLocators.Any()) { return null; }

            var vmLocType = viewModelLocators[0];

            var dedicatedInterface = GetDedicatedInterface(vmLocType);

            if (dedicatedInterface == null) { throw new InvalidOperationException("ViewModelLocator needs to have dedicated interface."); }

            return Tuple.Create(dedicatedInterface, vmLocType);
        }
    }
}
