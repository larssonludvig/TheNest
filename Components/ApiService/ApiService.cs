using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace Components.ApiService
{
    public class ApiService
    {
        private HttpClient _httpClient = new HttpClient();
        public readonly string _baseUrl = "https://thenest.larssonludvig.com/";

        public Task Initialize(string? baseUrl = null)
        {
            _httpClient = new HttpClient();
            
            if (!string.IsNullOrEmpty(baseUrl))
                _httpClient.BaseAddress = new System.Uri(baseUrl);
            else    
                _httpClient.BaseAddress = new System.Uri(_baseUrl);
            
            return Task.CompletedTask;
        }

        public async Task<T> Get<T>(string endpoint, Dictionary<string, string>? headers = null)
        {
            return await Get<T>(endpoint, null, headers);
        }

        public async Task<T> Get<T>(string endpoint, string? baseUrl = null, Dictionary<string, string>? headers = null)
        {
            if (!string.IsNullOrEmpty(baseUrl))
                await Initialize(baseUrl);
            else
                await Initialize();

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                T? data = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data == null)
                    throw new System.Exception("Failed to deserialize response");

                return data;
            }
            throw new System.Exception("Failed to fetch data");
        }
    }
}