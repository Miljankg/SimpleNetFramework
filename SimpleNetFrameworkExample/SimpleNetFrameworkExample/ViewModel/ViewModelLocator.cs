using SimpleNetFramework.Utils.Services;
using SimpleNetFrameworkExample.ViewModel.TestVm;

namespace SimpleNetFrameworkExample.ViewModel
{
    class ViewModelLocator : SimpleNetFramework.Patterns.Mvvm.Layers.ViewModel.ViewModelLocator, IViewModelLocator
    {
        public ViewModelLocator(ISfSvcLocator sfSvcLocator) 
            : base(sfSvcLocator)
        {
        }

        public ITestVm TestViewModel => SvcContainer.GetInstance<ITestVm>();
    }
}
