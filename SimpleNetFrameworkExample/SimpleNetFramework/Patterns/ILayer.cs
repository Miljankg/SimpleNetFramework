using System;
using System.Collections.Generic;

namespace SimpleNetFramework.Patterns
{
    interface ILayer
    {
        Dictionary<Type, Type> Load();
    }
}
