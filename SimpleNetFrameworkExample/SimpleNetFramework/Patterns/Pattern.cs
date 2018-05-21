using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LightInject;
using SimpleNetFramework.Utils.CSharpObjectHandling;

namespace SimpleNetFramework.Patterns
{
    internal abstract class Pattern : IPattern
    {
        public abstract Dictionary<Type, Type> Load();
    }
}
