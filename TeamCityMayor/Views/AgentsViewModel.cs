using System.Reactive.Linq;
using Microsoft.Practices.Unity;
using Prism.Events;
using ReactiveUI;
using TeamCityAPI.Models;
using TeamCityMayor.Events;
using TeamCityMayor.Models;
using TeamCityMayor.Services;
using TeamCityMayor.Views.Base;

namespace TeamCityMayor.Views
{
    public class AgentsViewModel : ViewModelBase
    {
        private AgentCollection _agents;
        private Agent _selectedAgent;

        public AgentCollection Agents
        {
            get => _agents;
            set => this.RaiseAndSetIfChanged(ref _agents, value);   
        }

        public Agent SelectedAgent
        {
            get => _selectedAgent;
            set => this.RaiseAndSetIfChanged(ref _selectedAgent, value);
        }

        public AgentsViewModel(IUnityContainer container, IEventAggregator eventAggregator)
            : base(container, eventAggregator)
        {
            this.WhenAnyValue(x => x.Manager)
                .Subscribe(Initialize);

            this.WhenAnyValue(x => x.SelectedAgent)
                .Subscribe(agent => _eventAggregator.GetEvent<AgentSelectedSubEvent>().Publish(agent));
        }

        private async void Initialize(TeamCityManager manager)
        {
            if (manager == null)
                return;

            IsRunning = true;
            Agents = await manager.GetAgentsAsync();
            IsRunning = false;
        }
    }
}
