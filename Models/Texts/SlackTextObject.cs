using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.Texts
{
    public class SlackTextObject
    {
        [JsonProperty("type")]
        public SlackTextType Type { get; set; } = SlackTextType.Mrkdwn;

        [JsonProperty("text")]
        public string Text { get; set; }

        // plain_text 타입에서만 유효
        [JsonProperty("emoji", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Emoji { get; set; }
    }
}
