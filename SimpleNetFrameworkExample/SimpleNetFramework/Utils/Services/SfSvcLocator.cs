using LightInject;

namespace SimpleNetFramework.Utils.Services
{
    class SfSvcLocator : ISfSvcLocator
    {
        private readonly IServiceContainer _svContainer;

        public SfSvcLocator(IServiceContainer svContainer)
        {
            _svContainer = svContainer;
        }

        public T GetInstance<T>()
        {
            return _svContainer.GetInstance<T>();
        }
    }
}
