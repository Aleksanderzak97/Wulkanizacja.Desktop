using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Wulkanizacja.Desktop.Services
{
    public class WebServiceClient
    {
        private readonly HttpClient _client;

        public WebServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> GetDataAsync<T>(string endpoint)
        {
            var result = await _client.GetFromJsonAsync<T>(endpoint);
            return result;
        }

        public async Task<HttpResponseMessage> PostDataAsync<T>(string endpoint, T data)
        {
            var response = await _client.PostAsJsonAsync(endpoint, data);
            return response;
        }

        public async Task<HttpResponseMessage> PutDataAsync<T>(string endpoint, T data)
        {
            var response = await _client.PutAsJsonAsync(endpoint, data);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteDataAsync(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);
            return response;
        }
    }
}
