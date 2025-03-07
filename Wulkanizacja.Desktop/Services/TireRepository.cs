using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wulkanizacja.Desktop.Enums;
using Wulkanizacja.Desktop.Models;

namespace Wulkanizacja.Desktop.Services
{
    public class TireRepository
    {
        private readonly WebServiceClient _client;

        public TireRepository(WebServiceClient client)
        {
            _client = client;
        }
        public async Task<HttpResponseMessage> AddTireAsync(TireModel tireModel)
        {
            var tireRequest = new TireRequest { Tire = tireModel };
            return await _client.PostDataAsync("tires", tireRequest);
        }

        public async Task<IEnumerable<TireModel>> GetTireAsync(string size, TireType tireType)
        {
            var encodedSize = Uri.EscapeDataString(size);
            return await _client.GetDataAsync<IEnumerable<TireModel>>($"tires/size/{encodedSize}/TireType/{(int)tireType}");
        }

        public async Task<HttpResponseMessage> DeleteTireAsync(Guid guid)
        {
            return await _client.DeleteDataAsync($"tires/{guid}/removeTire");
        }
    }
}
