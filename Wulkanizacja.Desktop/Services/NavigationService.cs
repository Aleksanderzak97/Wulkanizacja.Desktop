using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wulkanizacja.User.ViewModels;

namespace Wulkanizacja.User.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public event Action<object> ViewModelChanged;

        public void NavigateTo(string viewKey)
        {
            object viewModel = viewKey switch
            {
                "Login" => _serviceProvider.GetRequiredService<LoginViewModel>(),
                "General" => _serviceProvider.GetRequiredService<GeneralViewModel>(),
                "Content" => _serviceProvider.GetRequiredService<ContentControlViewModel>(),
                _ => _serviceProvider.GetRequiredService<LoginViewModel>()
            };

            ViewModelChanged?.Invoke(viewModel);
        }
    }
}
