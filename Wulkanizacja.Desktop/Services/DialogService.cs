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
                    dialog.Owner = _owner;
                    var dialogResult = dialog.ShowDialog() == true;
                    _errorResult = dialogResult;
                    return dialogResult;
                });
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
                    dialog.Owner = _owner;
                    var dialogResult = dialog.ShowDialog() == true;
                    _errorResult = dialogResult;
                    return dialogResult;
                });
                return _errorResult;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public static async Task<bool> ShowSuccesDialogAsync(string title, string message, string? okButtonText = "OK")
        {
            try
            {
                var result = await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    var dialog = new SuccessDialog();
                    dialog.Title.Text = title.ToUpper();
                    dialog.BodyText.Text = message;
                    dialog.OkButton.Content = okButtonText;
                    dialog.Owner = _owner;
                    var dialogResult = dialog.ShowDialog() == true;
                    _errorResult = dialogResult;
                    return dialogResult;
                });
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
                    dialog.Owner = _owner;
                    var dialogResult = dialog.ShowDialog() == true;
                    _errorResult = dialogResult;
                    return dialogResult;
                });
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
                    dialog.Owner = _owner;
                    var dialogResult = dialog.ShowDialog() == true;
                    if (dialogResult)
                    {
                        var vm = dialog.ViewModel;
                        tireModel = new TireModel()
                        {
                            Brand = vm.Brand,
                            Model = vm.Model,
                            Size = vm.Size,
                            SpeedIndex = vm.SpeedIndex,
                            LoadIndex = vm.LoadIndex,
                            TireType = vm.TireType,
                            ManufactureDate = vm.ManufactureDate,
                            Comments = vm.Comments,
                            QuantityInStock = vm.QuantityInStock
                        };
                        return tireModel;
                    }
                    return null;
                });
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
                    dialog.Owner = _owner;
                    var dialogResult = dialog.ShowDialog() == true;
                    if (dialogResult)
                    {
                        var vm = dialog.ViewModel;
                        tireModel = new TireModel()
                        {
                            Id = tire.Id,
                            Brand = vm.Brand,
                            Model = vm.Model,
                            Size = vm.Size,
                            SpeedIndex = vm.SpeedIndex,
                            LoadIndex = vm.LoadIndex,
                            TireType = vm.TireType,
                            ManufactureDate = vm.ManufactureDate,
                            Comments = vm.Comments,
                            QuantityInStock = vm.QuantityInStock ?? 0
                        };
                        return tireModel;
                    }
                    return null;
                });
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
