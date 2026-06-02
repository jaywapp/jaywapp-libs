using Jaywapp.Slack.Interfaces;
using Jaywapp.Slack.Models.Texts;
using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.Elements
{
    public class SlackButtonElement : ISlackElement
    {
        [JsonProperty("type")]
        public SlackElementType Type => SlackElementType.Button;

        [JsonProperty("text")]
        public SlackTextObject Text { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("action_id")]
        public string ActionId { get; set; }
    }
}
