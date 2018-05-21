using System;
using System.Reflection;
using LightInject;
using SimpleNetFramework.Patterns;
using SimpleNetFramework.Patterns.Mvvm;
using SimpleNetFramework.Utils.CSharpObjectHandling;
using SimpleNetFramework.Utils.Services;

namespace SimpleNetFramework
{
    public enum AvailablePatterns
    {
        Mvvm,
    }

    public static class BootstrapperFactory
    {
        public static IBootstrapper Create(AvailablePatterns pattern, INamespace rootNamespace)
        {
            if (rootNamespace == null) { throw new ArgumentException("Root namespace must be set."); }

            IPattern patternToUse;

            var assembly = Assembly.GetEntryAssembly();
            var svcContainer = new ServiceContainer();

            switch (pattern)
            {
                case AvailablePatterns.Mvvm:
                    patternToUse = MvvmPatternFactory.Create(rootNamespace, assembly);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported pattern.");
            }

            return new Bootstrapper(patternToUse, svcContainer);
        }
    }

    public sealed class Bootstrapper : IBootstrapper
    {
        private static bool _called;

        private readonly IPattern _pattern;
        private readonly IServiceContainer _svcContainer;

        internal Bootstrapper(IPattern pattern, IServiceContainer svcContainer)
        {
            _pattern = pattern;
            _svcContainer = svcContainer;
        }

        public T Boot<T>()
        {
            Boot();

            return _svcContainer.GetInstance<T>();
        }

        private void Boot()
        {
            if (_called) { throw new InvalidOperationException("Boostrapper called already."); }

            this.RegisterSystemInstances(_svcContainer);

            var loadedTypes = _pattern.Load();

            foreach (var loadedType in loadedTypes)
            {
                _svcContainer.Register(loadedType.Key, loadedType.Value);
            }

            _called = true;
        }

        private void RegisterSystemInstances(IServiceContainer svcContainer)
        {
            svcContainer.RegisterInstance(typeof(ISfSvcLocator), new SfSvcLocator(svcContainer));
        }
    }
}
