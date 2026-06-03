using Newtonsoft.Json;
using TeamCityMayor.Models.Agents;

namespace TeamCityMayor.Models
{
    public class AgentCollection
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("agent")]
        public List<Agent> Agent { get; set; }
    }
}
