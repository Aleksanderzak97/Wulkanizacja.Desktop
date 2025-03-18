using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wulkanizacja.Desktop.Converters;
using Wulkanizacja.Desktop.Enums;
using Wulkanizacja.Desktop.Models;
using Wulkanizacja.Desktop.Services;
using Wulkanizacja.User;
using Wulkanizacja.User.ViewModels;

namespace Wulkanizacja.Desktop.ViewModels
{
    public class RegisterDialogViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _lastName;
        private string _username;
        private string _password;
        private string _email;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Email.Contains("@"))
            {
                MessageBox.Show("Niepoprawny adres e-mail", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!IsValidPassword(Password))
            {
                MessageBox.Show("Hasło musi mieć co najmniej 8 znaków, zawierać wielką literę, cyfrę i znak specjalny.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if (!password.Any(char.IsDigit))
                return false;

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return false;

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
