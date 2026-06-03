using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;

namespace TeamCityMayor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell(IUnityContainer container)
        {
            DataContext = container.Resolve<ShellViewModel>();
            InitializeComponent();

            ContentRendered += Initialize;
        }

        private void Initialize(object? sender, EventArgs e)
        {
            if (DataContext is ShellViewModel viewModel)
                viewModel.Initialize();
        }
    }
}