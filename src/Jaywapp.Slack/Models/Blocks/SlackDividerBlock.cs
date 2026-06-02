using Jaywapp.Slack.Interfaces;
using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.Blocks
{
    public class SlackDividerBlock : ISlackBlock
    {
        [JsonProperty("type")]
        public SlackBlockType Type => SlackBlockType.Divider;
    }
}
