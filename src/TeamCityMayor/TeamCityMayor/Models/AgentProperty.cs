using Newtonsoft.Json;

namespace TeamCityMayor.Models.Agents
{
    public class AgentProperty
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
