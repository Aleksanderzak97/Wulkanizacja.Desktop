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
using Wulkanizacja.Desktop.Models;
using Wulkanizacja.Desktop.ViewModels;

namespace Wulkanizacja.Desktop.Dialogs
{
    /// <summary>
    /// Logika interakcji dla klasy EditTireDialog.xaml
    /// </summary>
    public partial class EditTireDialog : Window
    {
        public EditTireDialogViewModel ViewModel { get; }

        public EditTireDialog(TireModel tireModel)
        {
            InitializeComponent();
            ViewModel = new EditTireDialogViewModel(tireModel);
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
