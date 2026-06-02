using Newtonsoft.Json;

namespace Jaywapp.Slack.Interfaces
{
    public interface ISlackBlock
    {
        [JsonProperty("type")]
        SlackBlockType Type { get; }
    }
}
