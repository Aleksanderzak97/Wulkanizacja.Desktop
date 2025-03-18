using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wulkanizacja.Desktop.ViewModels;

namespace Wulkanizacja.Desktop.Dialogs
{
    /// <summary>
    /// Logika interakcji dla klasy UserRegisterDialog.xaml
    /// </summary>
    public partial class UserRegisterDialog : Window
    {
        public RegisterDialogViewModel ViewModel { get; }

        public UserRegisterDialog()
        {
            InitializeComponent();
            ViewModel = new RegisterDialogViewModel();
            DataContext = ViewModel;
        }

        private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ValidateInputs())
            {
                DialogResult = true;
                Close();
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterDialogViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
