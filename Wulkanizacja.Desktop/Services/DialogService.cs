using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Wulkanizacja.Desktop.Dialogs;

namespace Wulkanizacja.Desktop.Services
{
    public static class DialogService
    {
        public static Window? CurrentDialog { get; private set; }
        private static Window _owner;
        private static bool _errorResult;

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
    }
}
