using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<TireModel>> GetTireModelsAsync(string size, TireType tireType)
        {
            var encodedSize = Uri.EscapeDataString(size);
            return await _client.GetDataAsync<IEnumerable<TireModel>>($"tires/size/{encodedSize}/TireType/{(int)tireType}");
        }
    }
}
