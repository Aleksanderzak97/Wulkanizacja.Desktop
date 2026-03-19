using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Wulkanizacja.Desktop.Exceptions;
using Wulkanizacja.Desktop.Models;

namespace Wulkanizacja.Desktop.Services
{
    public class WebServiceClient
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public WebServiceClient(HttpClient client, TokenService tokenService)
        {
            _client = client;
            _tokenService = tokenService;

        }

        private void AddAuthorizationHeader()
        {
            var token = _tokenService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<T> GetDataAsync<T>(string endpoint)
        {
            AddAuthorizationHeader();
            var response = await _client.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<T>(content, JsonOptions);
            }

            throw CreateApiResponseException(response, content);
        }

        public async Task<HttpResponseMessage> PostDataAsync<TRequest>(string endpoint, TRequest data)
        {
            AddAuthorizationHeader();
            var response = await _client.PostAsJsonAsync(endpoint, data, JsonOptions);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content, JsonOptions);
                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
                {
                    _tokenService.SetToken(tokenResponse.Token);
                }

                return response;
            }

            throw CreateApiResponseException(response, content);
        }



        public async Task<HttpResponseMessage> PutDataAsync<T>(string endpoint, T data)
        {
            AddAuthorizationHeader();
            var response = await _client.PutAsJsonAsync(endpoint, data, JsonOptions);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            throw CreateApiResponseException(response, content);
        }

        public async Task<HttpResponseMessage> DeleteDataAsync(string endpoint)
        {
            AddAuthorizationHeader();
            var response = await _client.DeleteAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            throw CreateApiResponseException(response, content);
        }

        private static ApiResponseException CreateApiResponseException(HttpResponseMessage response, string content)
        {
            ErrorResponse? error = null;
            if (!string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    error = JsonSerializer.Deserialize<ErrorResponse>(content, JsonOptions);
                }
                catch
                {
                    // fallback below
                }
            }

            var errorMessage = !string.IsNullOrWhiteSpace(error?.Reason)
                ? error.Reason
                : $"Error: {response.StatusCode}, Content: {content}";

            return new ApiResponseException(errorMessage);
        }
    }
}
