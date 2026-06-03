using Newtonsoft.Json;

namespace TeamCityMayor.Models
{
    public class BuildConfiguration
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("projectId")]
        public string ProjectId { get; set; }

        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty("paused")]
        public bool Paused { get; set; }
    }
}
