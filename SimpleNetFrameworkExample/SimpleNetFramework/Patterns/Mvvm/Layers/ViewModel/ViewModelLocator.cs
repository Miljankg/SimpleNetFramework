using LightInject;
using SimpleNetFramework.Utils.Services;

namespace SimpleNetFramework.Patterns.Mvvm.Layers.ViewModel
{
    public abstract class ViewModelLocator
    {
        protected readonly ISfSvcLocator SvcContainer;

        protected ViewModelLocator(ISfSvcLocator svcContainer)
        {
            SvcContainer = svcContainer;
        }
    }
}
