using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.APIs
{
    internal class SlackApiResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
