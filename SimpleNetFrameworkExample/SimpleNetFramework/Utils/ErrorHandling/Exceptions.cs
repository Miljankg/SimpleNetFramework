using System;

namespace SimpleNetFramework.Utils.ErrorHandling
{
    public class BadConstructorException : Exception
    {
        public BadConstructorException(string message)
            : base(message)
        {
        }
    }
}
