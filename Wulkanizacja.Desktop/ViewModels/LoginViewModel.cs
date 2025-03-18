using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wulkanizacja.Desktop.Models;
using Wulkanizacja.Desktop.Services;
using Wulkanizacja.User.Services;
using Wulkanizacja.User.Views;

namespace Wulkanizacja.User.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly UserRepository _userRepository;
        private ObservableCollection<RegisterModel> _registerModels;


        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }


        public event EventHandler LoggedIn;

        public LoginViewModel(INavigationService navigationService, UserRepository userRepository)
        {
            _userRepository = userRepository;

            _navigationService = navigationService;
            LoginCommand = new RelayCommand(ExecuteLogin);
            RegisterCommand = new RelayCommand(Register);
        }

        private async void ExecuteLogin(object parameter)
        {
            if (parameter is LoginViewModel viewModel)
            {
                var loginModel = new LoginModel { Username = viewModel.Username, Password = viewModel.Password};
                if(string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
                {
                    await DialogService.ShowErrorDialogAsync("Błąd", "Wprowadź poprawne dane logowania");
                    return;
                }
                BusyIndicatorService.Instance.IsBusy = true;
                var loginUser = await _userRepository.LoginUserAsync(loginModel);
                if (loginUser.IsSuccessStatusCode)
                {
                    await DialogService.ShowSuccesDialogAsync("Sukces", "Zalogowano użytkownika");
                    BusyIndicatorService.Instance.IsBusy = false;
                    _navigationService.NavigateTo("General");
                }
                BusyIndicatorService.Instance.IsBusy = false;
            }
        }

        private async void Register(object parameter)
        {
            if (parameter is LoginViewModel viewModel)
            {
                var registerUser = await DialogService.ShowUserRegisterDialogAsync("Rejestracja");
                if (registerUser != null)
                {
                    BusyIndicatorService.Instance.IsBusy = true;
                    var add = await _userRepository.RegisterUserAsync(registerUser);
                    if (add.IsSuccessStatusCode)
                    {
                        _registerModels.Add(registerUser);
                        await DialogService.ShowSuccesDialogAsync("Sukces", "Zarejestrowano użytkownika");
                        BusyIndicatorService.Instance.IsBusy = false;
                    }
                    BusyIndicatorService.Instance.IsBusy = false;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
