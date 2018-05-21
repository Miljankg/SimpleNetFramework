using System;
using System.Collections.Generic;

namespace SimpleNetFramework.Patterns
{
    public interface IPattern
    {
        Dictionary<Type, Type> Load();
    }
}
