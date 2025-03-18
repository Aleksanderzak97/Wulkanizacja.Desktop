using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wulkanizacja.Desktop.Converters;
using Wulkanizacja.Desktop.Enums;
using Wulkanizacja.Desktop.Exceptions;
using Wulkanizacja.Desktop.Models;

namespace Wulkanizacja.Desktop.Services
{
    public class TireRepository : IDisposable
    {
        private readonly WebServiceClient _client;
        private readonly TireTypeToLocalizedStringConverter _tireTypeConverter;

        public TireRepository(WebServiceClient client)
        {
            _client = client;
            _tireTypeConverter = new TireTypeToLocalizedStringConverter();
        }

        public async Task<HttpResponseMessage> AddTireAsync(TireModel tireModel)
        {
            try
            {
                var tireRequest = new TireRequest { Tire = tireModel };
                return await _client.PostDataAsync("tires", tireRequest);
            }
            catch (ApiResponseException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", ex.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (HttpRequestException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd połączenia z serwerem, Opona nie została dodana");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd serwera, Opona nie została dodana");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<HttpResponseMessage> EditTireAsync(TireModel tireModel, Guid TireId)
        {
            try
            {
            return await _client.PutDataAsync($"tires/updateTire/{TireId}", tireModel);
            }
            catch (ApiResponseException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", ex.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (HttpRequestException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd połączenia z serwerem, Opona nie została edytowana");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd serwera, Opona nie została edytowana");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<IEnumerable<TireModel>> GetTireAsync(string size, TireType tireType)
        {
            try
            {
                var encodedSize = Uri.EscapeDataString(size);
                return await _client.GetDataAsync<IEnumerable<TireModel>>($"tires/size/{encodedSize}/TireType/{tireType}");
            }
            catch (ApiResponseException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", ex.Message);
                return Enumerable.Empty<TireModel>();
            }
            catch (HttpRequestException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd połączenia z serwerem");
                return Enumerable.Empty<TireModel>();
            }
            catch (Exception e)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd serwera");
                return Enumerable.Empty<TireModel>();
            }
        }

        public async Task<HttpResponseMessage> DeleteTireAsync(Guid guid)
        {
            try
            {
            return await _client.DeleteDataAsync($"tires/{guid}/removeTire");
            }
            catch (ApiResponseException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", ex.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (HttpRequestException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd połączenia z serwerem");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd serwera");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
