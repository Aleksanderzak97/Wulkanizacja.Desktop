using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wulkanizacja.User.ViewModels;

namespace Wulkanizacja.User.Services
{
    public interface INavigationService
    {
        event Action<object> ViewModelChanged;
        void NavigateTo(string viewKey);
    }
}
