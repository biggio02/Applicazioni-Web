using System.Text.Json;
using System.Text;
using MobiShare.Models;

namespace MobiShare.Services
{
    public class MezzoService : IMezzoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;
        public MezzoService(HttpClient httpClient, IConfiguration config, ApiClient apiClient)
        {
            _configuration = config;
            _httpClient = httpClient;
            _apiClient = apiClient;
        }
        public async Task<Mezzo> postMezzo(string token,string tipo_mezzo, int indirizzo, int prelevabile, int batteria)
        {
            Mezzo m = new Mezzo(tipo_mezzo, prelevabile,indirizzo, batteria);
            var json = JsonSerializer.Serialize(m);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _apiClient.PostAsync<Mezzo>("admin/mezzi", content,token);

        }

        public async Task<List<Mezzo>> getMezzi(string token,string paramentri)
        {
            return await _apiClient.GetAsync<List<Mezzo>>("mezzi"+paramentri,token);
        }
        
        public async Task<Mezzo> deleteMezzo(string token, string paramentri)
        {
            return await _apiClient.DeleteAsync<Mezzo>("admin/mezzi/" + paramentri, token);

        }

        public async Task<Mezzo> patchMezzo(string token, string paramentri)
        {
            return await _apiClient.PatchAsync<Mezzo>("admin/mezzi/" + paramentri, token);

        }
    }
}
