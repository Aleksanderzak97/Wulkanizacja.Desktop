using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Wulkanizacja.User.Services;
using Wulkanizacja.User.Views;

namespace Wulkanizacja.User.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeViewCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            ChangeViewCommand = new RelayCommand(ChangeView);
            navigationService.ViewModelChanged += vm => CurrentViewModel = vm;
            navigationService.NavigateTo("Login");
        }

        private void ChangeView(object view)
        {
            if (view is string viewKey)
            {
                var navigationService = App.Current.ServiceProvider.GetRequiredService<INavigationService>();
                navigationService.NavigateTo(viewKey);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
