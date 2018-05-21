using System.Reflection;
using SimpleNetFramework.Patterns.Mvvm.Layers.Logic;
using SimpleNetFramework.Patterns.Mvvm.Layers.ViewModel;
using SimpleNetFramework.Utils.CSharpObjectHandling;
using SimpleNetFramework.Utils.Services;

namespace SimpleNetFramework.Patterns.Mvvm
{
    internal static class MvvmPatternFactory
    {
        public static IPattern Create(INamespace rootNamespace, Assembly assembly)
        {
            var logicLayer = new LogicLayer(rootNamespace, assembly);
            var viewModelLayer = new ViewModelLayer(rootNamespace, assembly);

            return new MvvmPattern(logicLayer, viewModelLayer);
        }
    }
}
