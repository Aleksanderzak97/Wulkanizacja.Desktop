using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wulkanizacja.Desktop.Converters;
using Wulkanizacja.Desktop.Exceptions;
using Wulkanizacja.Desktop.Models;

namespace Wulkanizacja.Desktop.Services
{
    public class UserRepository
    {
        private readonly WebServiceClient _client;
        public UserRepository(WebServiceClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> LoginUserAsync(LoginModel loginModel)
        {
            try
            {
                return await _client.PostDataAsync("Auth/login", loginModel);
            }
            catch (ApiResponseException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", ex.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (HttpRequestException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd połączenia z serwerem, spróbuj ponownie później");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd serwera, spróbuj ponownie później");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<HttpResponseMessage> RegisterUserAsync(RegisterModel registerModel)
        {
            try
            {
                return await _client.PostDataAsync("Auth/register", registerModel);
            }
            catch (ApiResponseException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", ex.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (HttpRequestException ex)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd połączenia z serwerem, Użytkownik nie został utworzony");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await DialogService.ShowErrorDialogAsync("Błąd", "Błąd serwera, Użytkownik nie został utworzony");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
