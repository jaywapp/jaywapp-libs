using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jaywapp.Slack
{
    // Block Type Enum
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SlackBlockType
    {
        [EnumMember(Value = "section")]
        Section,
        [EnumMember(Value = "divider")]
        Divider,
        [EnumMember(Value = "image")]
        Image,
        [EnumMember(Value = "actions")]
        Actions,
        [EnumMember(Value = "context")]
        Context,
        [EnumMember(Value = "input")]
        Input,
        [EnumMember(Value = "header")]
        Header
    }

    // Element Type Enum
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SlackElementType
    {
        [EnumMember(Value = "button")]
        Button,
        [EnumMember(Value = "image")]
        Image,
        [EnumMember(Value = "static_select")]
        StaticSelect,
        [EnumMember(Value = "plain_text_input")]
        PlainTextInput,
        [EnumMember(Value = "overflow")]
        Overflow
    }

    // Text Type Enum
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SlackTextType
    {
        [EnumMember(Value = "mrkdwn")]
        Mrkdwn,
        [EnumMember(Value = "plain_text")]
        PlainText
    }
}
