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
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();

        public event Action<object> ViewModelChanged;

        public void NavigateTo(string viewKey)
        {
            object viewModel = viewKey switch
            {
                "Login" => new LoginViewModel(this),
                "General" => new GeneralViewModel(),
                "Content" => new ContentControlViewModel(),
                _ => new LoginViewModel(this)
            };

            ViewModelChanged?.Invoke(viewModel);
        }
    }
}
