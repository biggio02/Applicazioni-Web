using System.Text.Json;
using System.Text;
using MobiShare.Models;

namespace MobiShare.Services
{
    public class CartaPrepagataService : ICartaPrepagataService
    {
        private readonly ApiClient _apiClient;
        public CartaPrepagataService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<CartaPrepagata> postCartaPrepagata(int valore, string token)
        {
            CartaPrepagata c = new CartaPrepagata(valore);
            var json = JsonSerializer.Serialize(c);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _apiClient.PostAsync<CartaPrepagata>("user/cartePrepagate", content, token);
        }
    }
}

