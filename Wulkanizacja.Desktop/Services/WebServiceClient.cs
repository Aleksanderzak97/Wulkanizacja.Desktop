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
    public class WebServiceClient
    {
        private readonly HttpClient _client;

        public WebServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> GetDataAsync<T>(string endpoint)
        {
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

        //public async Task<HttpResponseMessage> PostDataAsync<T>(string endpoint, T data)
        //{
        //    var response = await _client.PostAsJsonAsync(endpoint, data);
        //    return response;
        //}
        public async Task<HttpResponseMessage> PostDataAsync<TRequest>(string endpoint, TRequest data)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = await _client.PostAsJsonAsync(endpoint, data, options);
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



        public async Task<HttpResponseMessage> PutDataAsync<T>(string endpoint, T data)
        {
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

    }
}
