using System;
using System.Collections.Generic;
using SimpleNetFramework.Patterns.Mvvm.Layers;

namespace SimpleNetFramework.Patterns.Mvvm
{
    class MvvmPattern : Pattern
    {
        private readonly ILogicLayer _logicLayer;

        public MvvmPattern(ILogicLayer logicLayer)
        {
            _logicLayer = logicLayer;
        }

        public override Dictionary<Type, Type> Load()
        {
            return _logicLayer.Load();
        }
    }
}
