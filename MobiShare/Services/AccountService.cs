using MobiShare.Models;
using System.Text;
using System.Text.Json;

namespace MobiShare.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;
        public AccountService(HttpClient httpClient, IConfiguration config, ApiClient apiClient)
        {
            _configuration = config;
            _httpClient = httpClient;
            _apiClient = apiClient;
        }
        public async Task<Utente> postAccount(string email,string username, string password)
        {
            Utente u= new Utente(email, username, password);
            var json = JsonSerializer.Serialize(u);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _apiClient.PostAsync<Utente>("utenti", content);
        }

        public async Task<Ricarica> postRicaricaCarta(float importoCarta,string numeroCarta,string scadenza,string cvv,string token)
        {
            Console.WriteLine($"Paramentri {importoCarta}-{numeroCarta}");
            CartaCredito c = new CartaCredito(numeroCarta, cvv, scadenza,importoCarta);
            var json = JsonSerializer.Serialize(c);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _apiClient.PostAsync<Ricarica>("user/utenti/ricaricheCarta", content,token);
        }

        public async Task<Ricarica> postRicaricaPrepagata(string token ,string codice)
        {
            CartaPrepagata c = new CartaPrepagata(codice);
            var json = JsonSerializer.Serialize(c);
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _apiClient.PostAsync<Ricarica>("user/utenti/ricarichePrepagata", content,token);
        }
        public async Task<Messaggio> putUtente(string token, string password)
        {
            var json = JsonSerializer.Serialize(new {password=password});
            Console.WriteLine("Json inviato: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _apiClient.PutAsync<Messaggio>("user/utenti/aggiornaPassword",content,token);
        }
        public async Task<Utente> getAccount(string username,string token)
        {
            return await _apiClient.GetAsync<Utente>("user/utenti/"+username,token);
        }

        public async Task<List<CronologiaCorse>> getCorse(string username,string par ,string token)
        {
            return await _apiClient.GetAsync<List<CronologiaCorse>>("user/utenti/" + username+"/corse"+par, token);
        }

        public async Task<List<Utente>> getAllAccount(string token,string parametri)
        {
            return await _apiClient.GetAsync<List<Utente>>("admin/utenti"+parametri, token);
        }

        public async Task<Messaggio> patchUtente(string username,string token)
        {
            return await _apiClient.PatchAsync<Messaggio>("admin/utenti/"+username, token);
        }

        public async Task<List<Ricarica>> getRicariche(string? username,string par ,string token)
        {
            return await _apiClient.GetAsync<List<Ricarica>>("user/utenti/" + username+"/ricariche"+par, token);
        }
    }
}
