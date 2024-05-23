using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmokeBall.Common.Helpers
{
    public class ApiClient
    {
        public ApiClient() 
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> Get(string uri)
        {
            var response = await _httpClient.GetStringAsync(uri);

            return response;
        }

        public async Task<T> Post<T>(string uri, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, data);
            
            var responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<T>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private HttpClient _httpClient;
    }
}
