using System.Text;
using System.Windows;

namespace Jaywapp.Test.PropertyTools.TreeListBox
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            var changelists = vm.Changelists;

            var builder = new StringBuilder();

            foreach(var  changelist in changelists)
            {
                builder.AppendLine($"{changelist.Id} {changelist.Description}");
                foreach(var file in changelist.Files)
                    builder.AppendLine($"- {file.Path}");
            }

            MessageBox.Show(builder.ToString());
        }
    }
}
