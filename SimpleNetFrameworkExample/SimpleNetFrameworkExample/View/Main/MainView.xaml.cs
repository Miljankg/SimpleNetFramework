using System.Windows;

namespace SimpleNetFrameworkExample.View.Main
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        public void Display()
        {
            this.ShowDialog();
        }
    }
}
