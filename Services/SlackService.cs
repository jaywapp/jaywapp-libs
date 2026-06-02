using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Jaywapp.Slack.Models.APIs;
using Jaywapp.Slack.Models.Messages;
using System.IO;

namespace Jaywapp.Slack.Services
{
    public class SlackService
    {
        #region Const Field
        private const string END_POINT = "https://slack.com/api/chat.postMessage";
        #endregion

        #region Internal Fields
        private readonly HttpClient _client;
        private readonly string _token;
        #endregion

        #region Constructor
        public SlackService(string token, HttpClient client = null)
        {
            _token = token;
            _client = client ?? new HttpClient();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Slack 채널에 메시지를 전송합니다.
        /// </summary>
        public async Task<bool> Send(string channel, SlackMessage message)
        {
            try
            {
                var payload = new
                {
                    channel = channel,
                    blocks = message.Blocks
                };

                var json = JsonConvert.SerializeObject(payload);
                var request = new HttpRequestMessage(HttpMethod.Post, END_POINT)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                var response = await _client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // 로깅/예외 처리 가능
                    System.Console.WriteLine($"Slack API Error: {response.StatusCode}, {responseContent}");
                    return false;
                }

                // Slack API 응답 파싱
                var result = JsonConvert.DeserializeObject<SlackApiResponse>(responseContent);
                if (!result.Ok)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
