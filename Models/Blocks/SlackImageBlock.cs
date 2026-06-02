using Jaywapp.Slack.Interfaces;
using Newtonsoft.Json;

namespace Jaywapp.Slack.Models.Blocks
{
    public class SlackImageBlock : ISlackBlock
    {
        [JsonProperty("type")]
        public SlackBlockType Type => SlackBlockType.Image;

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("alt_text")]
        public string AltText { get; set; }
    }
}
