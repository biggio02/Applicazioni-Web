
using MobiShare.Models;
using System.Text.Json;
using System.Text;

namespace MobiShare.Services
{
    public class ParcheggioService : IParcheggioService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;
        public ParcheggioService(HttpClient httpClient, IConfiguration config, ApiClient apiClient)
        {
            _configuration = config;
            _httpClient = httpClient;
            _apiClient = apiClient;
        }
        public async Task<Parcheggio> postParcheggio(string indirizzo, string token)
        {
            Parcheggio p = new Parcheggio(indirizzo);
            var json = JsonSerializer.Serialize(p);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _apiClient.PostAsync<Parcheggio>("admin/parcheggi", content,token);
        }

        public async Task<List<Parcheggio>> getParcheggio()
        {
            return await _apiClient.GetAsync<List<Parcheggio>>("parcheggi");
        }

        public async Task<List<Mezzo>> getAllMezziFromParcheggio(int idParcheggio)
        {
            return await _apiClient.GetAsync<List<Mezzo>>($"parcheggi/{idParcheggio}/mezzi");
        }

        public async Task<Messaggio> deleteParcheggio(string idParcheggio, string token)
        {
            return await _apiClient.DeleteAsync <Messaggio>($"admin/parcheggi/{idParcheggio}", token);
        }
    }
}
