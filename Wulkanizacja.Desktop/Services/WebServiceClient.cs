using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wulkanizacja.Desktop.Exceptions;
using Wulkanizacja.Desktop.Models;

namespace Wulkanizacja.Desktop.Services
{
    public class WebServiceClient : IDisposable
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;

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
                    return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    ErrorResponse error = null;
                    try
                    {
                        error = JsonSerializer.Deserialize<ErrorResponse>(content);
                    }
                    catch
                    {
                        throw;
                    }

                    string errorMessage = error != null
                        ? $"{error.Reason}"
                        : $"Error: {response.StatusCode}, Content: {content}";

                    throw new ApiResponseException(errorMessage);
                }
        }

        public async Task<HttpResponseMessage> PostDataAsync<TRequest>(string endpoint, TRequest data)
        {
            AddAuthorizationHeader();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = await _client.PostAsJsonAsync(endpoint, data, options);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content, options);
                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
                {
                    _tokenService.SetToken(tokenResponse.Token);
                }

                return response;
            }
            else
            {
                ErrorResponse error = null;
                try
                {
                    error = JsonSerializer.Deserialize<ErrorResponse>(content, options);
                }
                catch
                {
                    throw;
                }

                string errorMessage = error != null
                    ? $"{error.Reason}"
                    : $"Error: {response.StatusCode}, Content: {content}";

                throw new ApiResponseException(errorMessage);
            }
        }



        public async Task<HttpResponseMessage> PutDataAsync<T>(string endpoint, T data)
        {
            AddAuthorizationHeader();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = await _client.PutAsJsonAsync(endpoint, data, options);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                ErrorResponse error = null;
                try
                {
                    error = JsonSerializer.Deserialize<ErrorResponse>(content, options);
                }
                catch
                {
                    throw;
                }

                string errorMessage = error != null
                    ? $"{error.Reason}"
                    : $"Error: {response.StatusCode}, Content: {content}";

                throw new ApiResponseException(errorMessage);
            }
        }

        public async Task<HttpResponseMessage> DeleteDataAsync(string endpoint)
        {
            AddAuthorizationHeader();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = await _client.DeleteAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                ErrorResponse error = null;
                try
                {
                    error = JsonSerializer.Deserialize<ErrorResponse>(content, options);
                }
                catch
                {
                    throw;
                }

                string errorMessage = error != null
                    ? $"{error.Reason}"
                    : $"Error: {response.StatusCode}, Content: {content}";

                throw new ApiResponseException(errorMessage);
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
