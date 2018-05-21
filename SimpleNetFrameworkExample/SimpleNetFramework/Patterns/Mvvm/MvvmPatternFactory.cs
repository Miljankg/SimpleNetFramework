using System.Reflection;
using SimpleNetFramework.Patterns.Mvvm.Layers;
using SimpleNetFramework.Utils.CSharpObjectHandling;

namespace SimpleNetFramework.Patterns.Mvvm
{
    internal static class MvvmPatternFactory
    {
        public static IPattern Create(INamespace rootNamespace, Assembly assembly)
        {
            var logicLayer = new LogicLayer(rootNamespace, assembly);

            return new MvvmPattern(logicLayer);
        }
    }
}
