using Newtonsoft.Json;

namespace TeamCityMayor.Models
{
    public class BuildConfigurationCollection
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("buildType")]
        public List<BuildConfiguration> BuildType { get; set; }
    }
}
