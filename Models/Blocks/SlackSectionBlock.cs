using System.Collections.Generic;
using Jaywapp.Slack.Interfaces;
using Jaywapp.Slack.Models.Texts;
using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.Blocks
{
    public class SlackSectionBlock : ISlackBlock
    {
        [JsonProperty("type")]
        public SlackBlockType Type => SlackBlockType.Section;

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public SlackTextObject Text { get; set; }

        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<SlackTextObject> Fields { get; set; }

        [JsonProperty("accessory", NullValueHandling = NullValueHandling.Ignore)]
        public ISlackElement Accessory { get; set; }
    }
}
