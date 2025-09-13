using System.Text.Json;
using System.Text;
using MobiShare.Models;

namespace MobiShare.Services
{
    public class CorsaService : ICorsaService
    {
        private readonly ApiClient _apiClient;
        public CorsaService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<Corsa> postCorsa(int utente, int mezzo, string token)
        {
            //CartaPrepagata c = new CartaPrepagata(valore);
            var dati = new { utente = utente, mezzo = mezzo };
            var json = JsonSerializer.Serialize(dati);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _apiClient.PostAsync<Corsa>("user/corse", content, token);
        }

        public async Task<Corsa> putCorsa(int idCorsa, string feedback,int parcheggio, string token)
        {
            var dati = new { feedback = feedback, parcheggio = parcheggio };
            var json = JsonSerializer.Serialize(dati);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _apiClient.PutAsync<Corsa>($"user/corse/{idCorsa}", content, token);
            
        }
    }
}
