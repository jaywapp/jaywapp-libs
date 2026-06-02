using Jaywapp.Slack.Interfaces;
using Jaywapp.Slack.Models.Texts;
using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.Blocks
{
    public class SlackHeaderBlock : ISlackBlock
    {
        [JsonProperty("type")]
        public SlackBlockType Type => SlackBlockType.Header;

        [JsonProperty("text")]
        public SlackTextObject Text { get; set; }
    }
}
