using System.Collections.Generic;
using Jaywapp.Slack.Interfaces;
using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.Messages
{
    public class SlackMessage
    {
        [JsonProperty("blocks")]
        public List<ISlackBlock> Blocks { get; set; } = new List<ISlackBlock>();
    }
}
