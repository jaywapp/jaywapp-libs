using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Events;
using ReactiveUI;
using TeamCityAPI.Models;
using TeamCityMayor.Events;
using TeamCityMayor.Models;
using TeamCityMayor.Models.Agents;
using TeamCityMayor.Services;
using TeamCityMayor.Views.Base;
using Agent = TeamCityAPI.Models.Agent;

namespace TeamCityMayor.Views
{
    public class BuildConfigurationsViewModel : ViewModelBase
    {
        private Agent _selectedAgent;
        private BuildConfigurationCollection _builds;
        private Build _selectedBuild;

        public Agent SelectedAgent
        {
            get => _selectedAgent;
            set => this.RaiseAndSetIfChanged(ref _selectedAgent, value);
        }

        public BuildConfigurationCollection Builds
        {
            get => _builds;
            set => this.RaiseAndSetIfChanged(ref _builds, value);
        }

        public Build SelectedBuild
        {
            get => _selectedBuild;
            set => this.RaiseAndSetIfChanged(ref  _selectedBuild, value);
        }


        public BuildConfigurationsViewModel(IUnityContainer container, IEventAggregator eventAggregator)
            : base(container, eventAggregator) 
        {

            eventAggregator.GetEvent<AgentSelectedSubEvent>()
                .Subscribe(o => SelectedAgent = o is Agent agent ? agent : null);

            this.WhenAnyValue(x => x.SelectedAgent)
                 .Subscribe(OnSelectedAgentChanged);
        }

        private async void OnSelectedAgentChanged(Agent agent)
        {
            if (agent == null)
                return;

            IsRunning = true;
            Builds = await Manager.GetBuildsAsync(agent, 100);
            IsRunning = false;
        }
    }
}
