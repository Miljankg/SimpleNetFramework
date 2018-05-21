using System.Windows;
using SimpleNetFramework;
using SimpleNetFramework.Utils.CSharpObjectHandling;
using SimpleNetFrameworkExample.ViewModel;

namespace SimpleNetFrameworkExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var bootstrapp = BootstrapperFactory.Create(AvailablePatterns.Mvvm, Namespace.FromCurrentCaller());

            var testLogic = bootstrapp.Boot<IViewModelLocator>();

            testLogic.TestViewModel.Do();
        }
    }
}
