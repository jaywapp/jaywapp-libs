using System.Collections.Generic;
using System.Linq;
using Jaywapp.Slack.Interfaces;
using Jaywapp.Slack.Models.Blocks;
using Jaywapp.Slack.Models.Elements;
using Jaywapp.Slack.Models.Messages;
using Jaywapp.Slack.Models.Texts;
using Newtonsoft.Json;

namespace Jaywapp.Slack.Services
{
    public class SlackMessageBuilder
    {
        #region Internal Fields
        private IList<ISlackBlock> _blocks = new List<ISlackBlock>();
        #endregion

        #region Constructor
        public SlackMessageBuilder()
        {
            _blocks = new List<ISlackBlock>();
        }
        #endregion

        #region Functions
        public static SlackMessageBuilder Create() => new SlackMessageBuilder();

        public SlackMessage Build()
        {
            return new SlackMessage()
            {
                Blocks = _blocks.ToList(),
            };
        }

        public string BuildJson(bool indented = true)
        {
            return JsonConvert.SerializeObject(
                Build(), indented ? Formatting.Indented : Formatting.None);
        }

        public SlackMessageBuilder AddSection(string text, SlackTextType type = SlackTextType.Mrkdwn)
        {
            _blocks.Add(new SlackSectionBlock
            {
                Text = new SlackTextObject
                {
                    Type = type,
                    Text = text
                }
            });
            return this;
        }

        public SlackMessageBuilder AddDivider()
        {
            _blocks.Add(new SlackDividerBlock());
            return this;
        }

        public SlackMessageBuilder AddImage(string imageUrl, string altText)
        {
            _blocks.Add(new SlackImageBlock
            {
                ImageUrl = imageUrl,
                AltText = altText
            });
            return this;
        }

        public SlackMessageBuilder AddButton(string text, string actionId, string value = null, bool emoji = true)
        {
            var button = new SlackButtonElement
            {
                Text = new SlackTextObject
                {
                    Type = SlackTextType.PlainText,
                    Text = text,
                    Emoji = emoji
                },
                ActionId = actionId,
                Value = value
            };

            _blocks.Add(new SlackActionsBlock
            {
                Elements = new List<ISlackElement> { button }
            });

            return this;
        }

        public SlackMessageBuilder AddButtons(params (string text, string actionId, string value, bool emoji)[] buttons)
        {
            var actionBlock = new SlackActionsBlock
            {
                Elements = new List<ISlackElement>()
            };

            foreach (var btn in buttons)
            {
                actionBlock.Elements.Add(new SlackButtonElement
                {
                    Text = new SlackTextObject
                    {
                        Type = SlackTextType.PlainText,
                        Text = btn.text,
                        Emoji = btn.emoji
                    },
                    ActionId = btn.actionId,
                    Value = btn.value
                });
            }

            _blocks.Add(actionBlock);
            return this;
        }

        public SlackMessageBuilder AddHeader(string text, bool emoji = true)
        {
            _blocks.Add(new SlackHeaderBlock
            {
                Text = new SlackTextObject
                {
                    Type = SlackTextType.PlainText, // Header는 plain_text만 가능
                    Text = text,
                    Emoji = emoji
                }
            });
            return this;
        }

        public SlackMessageBuilder AddSectionFields(params string[] fields)
        {
            var section = new SlackSectionBlock
            {
                Fields = new List<SlackTextObject>()
            };

            foreach (var f in fields)
            {
                section.Fields.Add(new SlackTextObject
                {
                    Type = SlackTextType.Mrkdwn, // fields는 mrkdwn과 plain_text 둘 다 가능
                    Text = f
                });
            }

            _blocks.Add(section);
            return this;
        }

        #endregion
    }
}
