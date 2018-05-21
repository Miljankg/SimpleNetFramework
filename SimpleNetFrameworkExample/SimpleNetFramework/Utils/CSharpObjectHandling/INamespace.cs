namespace SimpleNetFramework.Utils.CSharpObjectHandling
{
    public interface INamespace
    {
        string Name { get; }

        string Path { get; }

        Namespace Append(Namespace namespaceToAppend);
    }
}
