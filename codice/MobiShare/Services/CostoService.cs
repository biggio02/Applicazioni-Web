using System.Text.Json;
using System.Text;
using MobiShare.Models;

namespace MobiShare.Services
{
    public class CostoService : ICostoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;
        public CostoService(HttpClient httpClient, IConfiguration config, ApiClient apiClient)
        {
            _configuration = config;
            _httpClient = httpClient;
            _apiClient = apiClient;
        }
        public async Task<List<Costo>> getCosto()
        {

            return await _apiClient.GetAsync<List<Costo>>("mezzi/costi");
        }

        public async Task<Costo> putCosto(string tipoMezzo,string token,double costoVariabile,double costoFisso)
        {
            Costo c = new Costo(costoFisso,costoVariabile);
            var json = JsonSerializer.Serialize(c);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _apiClient.PutAsync<Costo>($"mezzi/costi/{tipoMezzo}",content,token);
        }
    }
}
