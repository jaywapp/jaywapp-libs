using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using System.Windows;
using Prism.Unity;
using TeamCityMayor.Services;
using TeamCityMayor.Views;
using Prism.Mvvm;

namespace TeamCityMayor
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<AgentsView>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AgentsViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<BuildConfigurationsView>(new ContainerControlledLifetimeManager());
            Container.RegisterType<BuildConfigurationsViewModel>(new ContainerControlledLifetimeManager());

            ViewModelLocationProvider.Register<AgentsView, AgentsViewModel>();
            ViewModelLocationProvider.Register<BuildConfigurationsView, BuildConfigurationsViewModel>();    
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        // 셸이 생성된 후 표시합니다.
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
