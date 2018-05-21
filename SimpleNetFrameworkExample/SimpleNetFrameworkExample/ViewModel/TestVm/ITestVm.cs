using SimpleNetFramework.Patterns.Mvvm.Layers.ViewModel;

namespace SimpleNetFrameworkExample.ViewModel.TestVm
{
    public interface ITestVm : IViewModel
    {
        string SomeText { get; }
    }
}
