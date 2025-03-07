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
    /// Logika interakcji dla klasy AddTireDialog.xaml
    /// </summary>
    public partial class AddTireDialog : Window
    {
        public AddTireDialogViewModel ViewModel { get; }

        public AddTireDialog()
        {
            InitializeComponent();
            ViewModel = new AddTireDialogViewModel();
            DataContext = ViewModel;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
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
    }
}
