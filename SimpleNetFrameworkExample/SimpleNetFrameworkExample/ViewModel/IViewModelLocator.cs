using SimpleNetFrameworkExample.ViewModel.TestVm;

namespace SimpleNetFrameworkExample.ViewModel
{
    interface IViewModelLocator
    {
        ITestVm TestViewModel { get; }
    }
}
