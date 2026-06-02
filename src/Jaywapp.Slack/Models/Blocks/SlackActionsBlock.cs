using System.Collections.Generic;
using Jaywapp.Slack.Interfaces;
using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.Blocks
{
    public class SlackActionsBlock : ISlackBlock
    {
        [JsonProperty("type")]
        public SlackBlockType Type => SlackBlockType.Actions;

        [JsonProperty("elements")]
        public List<ISlackElement> Elements { get; set; } = new List<ISlackElement>();
    }
}
