using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TeamCityAPI;
using TeamCityAPI.Locators;
using TeamCityAPI.Models;
using TeamCityMayor.Models;

namespace TeamCityMayor.Services
{
    public class TeamCityManager
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        public TeamCityManager(string host, string port, string token)
        {
            _baseUrl = $"http://{host}:{port}";

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AgentCollection> GetAgentsAsync()
        {
            var requestUrl = $"{_baseUrl}/app/rest/agents";

            try
            {
                var response = await _client.GetStringAsync(requestUrl);
                var agents = JsonConvert.DeserializeObject<AgentCollection>(response);
                return agents;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"API 호출 중 오류가 발생했습니다: {e.Message}");
                return null;
            }
        }

        public async Task<BuildConfigurationCollection> GetBuildsAsync() 
        {
            var requestUrl = $"{_baseUrl}/app/rest/buildTypes";

            try
            {
                var response = await _client.GetStringAsync(requestUrl);
                var builds = JsonConvert.DeserializeObject<BuildConfigurationCollection>(response);
                return builds;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"API 호출 중 오류가 발생했습니다: {e.Message}");
                return null;
            }
        }

        public async Task<BuildConfigurationCollection> GetBuildsAsync(Agent agent, int maxCount = 50)
        {
            var candidates = await GetBuildsAsync();
            var configs = candidates.BuildType.Where(b => b.Name == agent.Name).ToList();

            return new BuildConfigurationCollection()
            {
                Count = configs?.Count ?? 0,
                Href = candidates.Href,
                BuildType = configs ?? new List<BuildConfiguration>(),
            };
        }
    }
}
