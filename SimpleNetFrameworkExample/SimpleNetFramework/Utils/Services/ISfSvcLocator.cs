namespace SimpleNetFramework.Utils.Services
{
    public interface ISfSvcLocator
    {
        T GetInstance<T>();
    }
}
