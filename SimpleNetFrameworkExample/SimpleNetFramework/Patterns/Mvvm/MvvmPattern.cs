using System;
using System.Collections.Generic;
using System.Linq;
using SimpleNetFramework.Patterns.Mvvm.Layers.Logic;
using SimpleNetFramework.Patterns.Mvvm.Layers.View;
using SimpleNetFramework.Patterns.Mvvm.Layers.ViewModel;

namespace SimpleNetFramework.Patterns.Mvvm
{
    class MvvmPattern : Pattern
    {
        private readonly ILogicLayer _logicLayer;
        private readonly IViewModelLayer _viewModelLayer;
        private readonly IViewLayer _viewLayer;

        public MvvmPattern(ILogicLayer logicLayer, IViewModelLayer viewModelLayer, IViewLayer viewLayer)
        {
            _logicLayer = logicLayer;
            _viewModelLayer = viewModelLayer;
            _viewLayer = viewLayer;
        }

        public override Dictionary<Type, Type> Load()
        {
            var loadedLogicTypes = _logicLayer.Load();
            var loadedViewModelTypes = _viewModelLayer.Load();
            var loadedViewTypes = _viewLayer.Load();

            return loadedLogicTypes
                .Concat(loadedViewModelTypes)
                .Concat(loadedViewTypes)
                .ToDictionary(k => k.Key, v => v.Value);

        }
    }
}
