using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Wulkanizacja.Desktop.Dialogs;
using Wulkanizacja.Desktop.Enums;
using Wulkanizacja.Desktop.Models;

namespace Wulkanizacja.Desktop.Services
{
    public static class DialogService
    {
        public static Window? CurrentDialog { get; private set; }
        private static Window _owner;
        private static bool _errorResult;
        private static TireModel tireModel;

        public static void SetOwner(Window owner)
    => _owner = owner;

        public static async Task<bool> ShowErrorDialogAsync(string title, string message, string? okButtonText = "OK")
        {
            try
            {
                var result = await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    var dialog = new ErrorDialog();
                    dialog.Title.Text = title.ToUpper();
                    dialog.BodyText.Text = message;
                    dialog.OkButton.Content = okButtonText;
                    CurrentDialog = dialog;
                    var dialogResult = dialog.ShowDialog() == true;
                    _errorResult = dialogResult;
                    return dialogResult;
                });
                CurrentDialog = null;
                return _errorResult;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public static async Task<bool> ShowInfoDialogAsync(string title, string message, string? okButtonText = "OK")
        {
            try
            {
                var result = await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    var dialog = new InformationDialog();
                    dialog.Title.Text = title.ToUpper();
                    dialog.BodyText.Text = message;
                    dialog.OkButton.Content = okButtonText;
                    CurrentDialog = dialog;
                    var dialogResult = dialog.ShowDialog() == true;
                    _errorResult = dialogResult;
                    return dialogResult;
                });
                CurrentDialog = null;
                return _errorResult;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public static async Task<bool> ShowQuestionDialogAsync(string title, string message, string? okButtonText = "OK", string? nokButtonText = "Anuluj")
        {
            try
            {
                var result = await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    var dialog = new QuestionDialog();
                    dialog.Title.Text = title.ToUpper();
                    dialog.BodyText.Text = message;
                    dialog.OkButton.Content = okButtonText;
                    CurrentDialog = dialog;
                    var dialogResult = dialog.ShowDialog() == true;
                    _errorResult = dialogResult;
                    return dialogResult;
                });
                CurrentDialog = null;
                return _errorResult;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public static async Task<TireModel> ShowAddDialogAsync(string title, string? okButtonText = "Dodaj", string? nokButtonText = "Anuluj")
        {
            try
            {
                var result = await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    var dialog = new AddTireDialog();
                    dialog.Title.Text = title.ToUpper();
                    dialog.AddButton.Content = okButtonText;
                    CurrentDialog = dialog;
                    var dialogResult = dialog.ShowDialog() == true;
                    if (dialogResult)
                    {
                        tireModel = new TireModel()
                        {
                            Brand = dialog.BrandTextBox.Text,
                            Model = dialog.ModelTextBox.Text,
                            Size = dialog.SizeTextBox.Text,
                            SpeedIndex = dialog.SpeedIndexTextBox.Text,
                            LoadIndex = dialog.LoadIndexTextBox.Text,
                            TireType = (TireType)Enum.Parse(typeof(TireType), ((ComboBoxItem)dialog.TireTypeComboBox.SelectedItem)?.Tag?.ToString() ?? string.Empty),
                            ManufactureDate = dialog.ManufactureWeekYearTextBox.Text,
                            Comments = dialog.CommentsTextBox.Text,
                            QuantityInStock = int.Parse(dialog.QuantityInStockTextBox.Text)
                        };
                        return tireModel;
                    }
                    return null;
                });
                CurrentDialog = null;
                return tireModel;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public static async Task<TireModel> ShowEditDialogAsync(string title, TireModel tire, string? okButtonText = "Edytuj", string? nokButtonText = "Anuluj")
        {
            try
            {
                var result = await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    var dialog = new EditTireDialog(tire);
                    dialog.Title.Text = title.ToUpper();
                    dialog.AddButton.Content = okButtonText;
                    CurrentDialog = dialog;
                    var dialogResult = dialog.ShowDialog() == true;
                    if (dialogResult)
                    {
                        tireModel = new TireModel()
                        {
                            Id = tire.Id,
                            Brand = dialog.BrandTextBox.Text,
                            Model = dialog.ModelTextBox.Text,
                            Size = dialog.SizeTextBox.Text,
                            SpeedIndex = dialog.SpeedIndexTextBox.Text,
                            LoadIndex = dialog.LoadIndexTextBox.Text,
                            TireType = (TireType)Enum.Parse(typeof(TireType), ((ComboBoxItem)dialog.TireTypeComboBox.SelectedItem)?.Tag?.ToString() ?? string.Empty),
                            ManufactureDate = dialog.ManufactureWeekYearTextBox.Text,
                            Comments = dialog.CommentsTextBox.Text,
                            QuantityInStock = int.Parse(dialog.QuantityInStockTextBox.Text)
                        };
                        return tireModel;
                    }
                    return null;
                });
                CurrentDialog = null;
                return tireModel;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }
    }
}
