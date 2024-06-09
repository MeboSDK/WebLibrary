using Core.Abstraction;
using Core.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApiClients
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<TResponse>> GetDataAsync<TResponse>(string endpoint, string? jwtToken = null)
        {
            if (jwtToken != null)
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.GetAsync($"api/{endpoint}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<TResponse>>(jsonResponse);
        }

        public async Task<ApiResponse<TResponse>> PostDataAsync<TResponse>(object data, string endpoint, string? jwtToken = null)
        {
            var content = new StringContent(
                content: Newtonsoft.Json.JsonConvert.SerializeObject(data),
                Encoding.UTF8,
                "application/json");

            if (jwtToken != null)
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.PostAsync($"api/{endpoint}", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<TResponse>>(jsonResponse);
        }
    }
}
