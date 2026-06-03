using System.Configuration;
using System.Data;
using System.Windows;

namespace TeamCityMayor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 부트스트래퍼를 실행하여 Prism 애플리케이션을 시작합니다.
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }

}
