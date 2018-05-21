using LightInject;

namespace SimpleNetFramework.Utils.Services
{
    public class SfSvcLocator : ISfSvcLocator
    {
        private readonly IServiceContainer _svContainer;

        public static ISfSvcLocator Instance { get; private set; }

        public SfSvcLocator(IServiceContainer svContainer)
        {
            _svContainer = svContainer;

            Instance = this;
        }

        public T GetInstance<T>()
        {
            return _svContainer.GetInstance<T>();
        }
    }
}
