using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TeamCityMayor.Models.Agents
{
    public class Agent
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("connected")]
        public bool Connected { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("authorized")]
        public bool Authorized { get; set; }

        [JsonProperty("properties")]
        public List<AgentProperty> Properties { get; set; }
    }
}
