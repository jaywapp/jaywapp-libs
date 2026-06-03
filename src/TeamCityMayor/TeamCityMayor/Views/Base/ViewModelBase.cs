using Microsoft.Practices.Unity;
using Prism.Events;
using ReactiveUI;
using TeamCityMayor.Events;
using TeamCityMayor.Services;

namespace TeamCityMayor.Views.Base
{
    public class ViewModelBase : ReactiveObject
    {
        protected readonly IUnityContainer _container;
        protected readonly IEventAggregator _eventAggregator;

        protected TeamCityManager _manager;
        protected bool _isRunning;

        public TeamCityManager Manager
        {
            get => _manager;
            set => this.RaiseAndSetIfChanged(ref _manager, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => this.RaiseAndSetIfChanged(ref _isRunning, value);
        }


        public ViewModelBase(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<TeamCityManagerInitializedSubEvent>()
                .Subscribe(obj =>
                {
                    if (obj is TeamCityManager manager)
                        Manager = manager;
                });
        }
    }
}
