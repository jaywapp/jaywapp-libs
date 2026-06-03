using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Events;
using ReactiveUI;
using TeamCityMayor.Events;
using TeamCityMayor.Services;

namespace TeamCityMayor
{
    public class ShellViewModel : ReactiveObject
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;


        public ShellViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;

            Initialize();
        }

        internal void Initialize()
        {
            var host = "192.168.0.101";
            var port = "9816";
            var token = "eyJ0eXAiOiAiVENWMiJ9.ci1wYzZQbk9zYVZ0U0ZhVWlOc2lJbWI3Z3Nz.YjBiMmMxNGMtNzI4Ny00ODVlLWJiNDEtN2U2NDM0YWYyMTZm";

            var manager = new TeamCityManager(host, port, token);

            _eventAggregator.GetEvent<TeamCityManagerInitializedSubEvent>().Publish(manager);
        }
    }
}
