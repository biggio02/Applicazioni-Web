using System.Text.Json;
using System.Text;
using MobiShare.Models;

namespace MobiShare.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;
        public TokenService(HttpClient httpClient, IConfiguration config, ApiClient apiClient)
        {
            _configuration = config;
            _httpClient = httpClient;
            _apiClient = apiClient;
        }
        public async Task<Token> getToken(string username, string password)
        {
            var json = JsonSerializer.Serialize(new { username = username, password = password });
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _apiClient.PostAsync<Token>("login", content);
        }

        public async Task<Token> getTokenGoogle(string code)
        {

            return await _apiClient.GetAsync<Token>($"loginGoogle?code={code}");
        }
    }
}
