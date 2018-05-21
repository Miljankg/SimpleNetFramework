using System;
using System.Diagnostics;

namespace SimpleNetFramework.Utils.CSharpObjectHandling
{
    public sealed class Namespace : INamespace
    {
        public static Namespace FromString(string namespaceStr)
        {
            if (string.IsNullOrEmpty(namespaceStr)) { throw new ArgumentException("Namespace string must be not be null."); }

            var lastDot = string.IsNullOrEmpty(namespaceStr)
                ? -1 : namespaceStr.LastIndexOf(".", StringComparison.InvariantCulture);

            return new Namespace()
            {
                Name = lastDot == -1 ? namespaceStr : namespaceStr.Substring(lastDot, namespaceStr.Length - lastDot),
                Path = lastDot == -1 ? string.Empty : namespaceStr.Substring(0, lastDot),
            };
        }

        public static Namespace FromCurrentCaller()
        {
            var declaringType = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
            
            if (declaringType == null) { throw new InvalidOperationException("Namespace cannot be created from a caller without a Declaring Type."); }

            return FromString(declaringType.Namespace);
        }

        public string Name { get; private set; }

        public string Path { get; private set; }

        public Namespace Append(Namespace namespaceToAppend)
        {
            var path = string.IsNullOrEmpty(this.Path) ? string.Empty : $"{this.Path}.";
            var namespaceToAppendPath = string.IsNullOrEmpty(namespaceToAppend.Path) ? string.Empty : $".{namespaceToAppend.Path}";

            return new Namespace()
            {
                Name = namespaceToAppend.Name,
                Path = $"{path}{this.Name}{namespaceToAppendPath}",
            };
        }

        public override string ToString()
        {
            var path = string.IsNullOrEmpty(this.Path) ? string.Empty : $"{this.Path}.";

            return $"{path}{this.Name}";
        }
    }
}
