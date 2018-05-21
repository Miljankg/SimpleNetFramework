using SimpleNetFramework.Utils.Services;
using SimpleNetFrameworkExample.ViewModel.TestVm;
using SimpleNetFramework.Patterns.Mvvm.Layers.ViewModel;

namespace SimpleNetFrameworkExample.ViewModel
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        public ITestVm TestViewModel => SfSvcLocator.Instance.GetInstance<ITestVm>();
    }
}
