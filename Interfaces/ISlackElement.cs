using Newtonsoft.Json;

namespace Jaywapp.Slack.Interfaces
{
    // Element 인터페이스
    public interface ISlackElement
    {
        [JsonProperty("type")]
        SlackElementType Type { get; }
    }
}
