using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace TeamCityMayor.Events
{
    public class TeamCityManagerInitializedSubEvent : PubSubEvent<object> { }
    public class AgentSelectedSubEvent : PubSubEvent<object> { }
}
