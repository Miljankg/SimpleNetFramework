using System;
using System.Collections.Generic;
using System.Linq;
using SimpleNetFramework.Patterns.Mvvm.Layers.Logic;
using SimpleNetFramework.Patterns.Mvvm.Layers.ViewModel;

namespace SimpleNetFramework.Patterns.Mvvm
{
    class MvvmPattern : Pattern
    {
        private readonly ILogicLayer _logicLayer;
        private readonly IViewModelLayer _viewModelLayer;

        public MvvmPattern(ILogicLayer logicLayer, IViewModelLayer viewModelLayer)
        {
            _logicLayer = logicLayer;
            _viewModelLayer = viewModelLayer;
        }

        public override Dictionary<Type, Type> Load()
        {
            var loadedLogicTypes = _logicLayer.Load();
            var loadedViewModelTypes = _viewModelLayer.Load();

            return loadedLogicTypes
                .Concat(loadedViewModelTypes)
                .ToDictionary(k => k.Key, v => v.Value);

        }
    }
}
