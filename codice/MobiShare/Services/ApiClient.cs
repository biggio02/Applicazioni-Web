using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using MobiShare.Models;

namespace MobiShare.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public ApiClient(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _baseUrl = apiSettings.Value.BaseUrl;
        }

        //T:class per informare il compilatore che T deve essere una classe,
        //new() per far sapere che deve avere un costruttore senza parametri
        public async Task<T?> GetAsync<T>(string endpoint, string token = null) where T : class, new()
        {
            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync(_baseUrl + endpoint);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("body:"+json);
                var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result ?? new T();
            }

            return new T();
        }


        public async Task<T?> PostAsync<T>(string endpoint,StringContent content, string token = null) where T : class, new()
        {
            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.PostAsync(_baseUrl + endpoint, content);

            if (response.IsSuccessStatusCode)
            {

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + json);
                var result = JsonSerializer.Deserialize<T>(json);
                return result ?? new T();

            }
            return new T();
        }

        public async Task<T> PatchAsync<T>(string endpoint,string token) where T : class, new()
        {
            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.PatchAsync(_baseUrl + endpoint,null);

            if (response.IsSuccessStatusCode)
            {

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(json);

                return result ?? new T();

            }
            return new T();
        }

        public async Task<T?> PutAsync<T>(string endpoint, StringContent content, string token = null) where T : class, new()
        {
            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.PutAsync(_baseUrl + endpoint, content);

            if (response.IsSuccessStatusCode)
            {

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + json);
                var result = JsonSerializer.Deserialize<T>(json);
                return result ?? new T();

            }
            return new T();
        }

        public async Task<T?> DeleteAsync<T>(string endpoint, string token = null) where T : class, new()
        {
            if (!String.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.DeleteAsync(_baseUrl + endpoint);

            if (response.IsSuccessStatusCode)
            {

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + json);
                var result = JsonSerializer.Deserialize<T>(json);
                return result ?? new T();

            }
            return new T();
        }
    }
}

