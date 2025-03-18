using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using Wulkanizacja.Desktop.Dictionary;
using Wulkanizacja.Desktop.Enums;
using Wulkanizacja.Desktop.Models;
using Wulkanizacja.Desktop.Services;
using Wulkanizacja.User.Services;

namespace Wulkanizacja.User.ViewModels
{
    public class GeneralViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly WebServiceClient _webServiceClient;
        private readonly TireRepository _tireRepository;
        private ObservableCollection<TireModel> _tireModels;
        private string _size;
        private TireType _selectedTireType;
        private string _selectedTranslatedTireType;
        private readonly Dictionary<string, TireType> _translatedToOriginalTireTypeMap;
        private TireModel _selectedTireModel;


        public ObservableCollection<TireModel> TireModels
        {
            get => _tireModels;
            set
            {
                _tireModels = value;
                OnPropertyChanged();
            }
        }

        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged();
            }
        }

        public TireType SelectedTireType
        {
            get => _selectedTireType;
            set
            {
                _selectedTireType = value;
                OnPropertyChanged();
            }
        }

        public string SelectedTranslatedTireType
        {
            get => _selectedTranslatedTireType;
            set
            {
                _selectedTranslatedTireType = value;
                if (_translatedToOriginalTireTypeMap.TryGetValue(value, out var originalTireType))
                {
                    SelectedTireType = originalTireType;
                }
                OnPropertyChanged();
            }
        }

        public TireModel SelectedTireModel
        {
            get => _selectedTireModel;
            set
            {
                _selectedTireModel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TireType> TireTypes { get; }
        public ObservableCollection<string> TranslatedTireTypes { get; private set; }
        public ICommand AddCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LogoutCommand { get; }

        public GeneralViewModel(INavigationService navigationService, WebServiceClient webServiceClient, TireRepository tireRepository)
        {
            _navigationService = navigationService;
            _webServiceClient = webServiceClient;
            _tireRepository = tireRepository;

            TireModels = new ObservableCollection<TireModel>();
            TireTypes = new ObservableCollection<TireType>(Enum.GetValues(typeof(TireType)).Cast<TireType>());
            _translatedToOriginalTireTypeMap = TireTypes.ToDictionary(t => TranslateTireType(t), t => t);
            TranslatedTireTypes = new ObservableCollection<string>(_translatedToOriginalTireTypeMap.Keys);

            AddCommand = new RelayCommand(Add);
            SearchCommand = new RelayCommand(Search);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            LogoutCommand = new RelayCommand(Logout);
        }
        private string TranslateTireType(TireType tireType)
        {
            string languageCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return TranslationDictionary.Translate(tireType, languageCode);
        }
        private async void Search(object parameter)
        {
            BusyIndicatorService.Instance.IsBusy = true;
            if (parameter is GeneralViewModel viewModel)
            {
                if (string.IsNullOrWhiteSpace(viewModel.Size))
                {
                    await DialogService.ShowErrorDialogAsync("Błąd", "Pole 'Rozmiar' nie może być puste.");
                    BusyIndicatorService.Instance.IsBusy = false;
                    return;
                }
                if (viewModel.SelectedTireType == 0)
                {
                    await DialogService.ShowErrorDialogAsync("Błąd", "Musisz wybrać typ opony.");
                    BusyIndicatorService.Instance.IsBusy = false;
                    return;
                }

                var searchParameters = new SearchParameters
                {
                    Size = viewModel.Size,
                    Type = (int)viewModel.SelectedTireType
                };
                var encodedSize = Uri.EscapeDataString(searchParameters.Size);
                var data = await _tireRepository.GetTireAsync(searchParameters.Size, (TireType)searchParameters.Type);
                if (data != null)
                    TireModels = new ObservableCollection<TireModel>(data);
            }
            BusyIndicatorService.Instance.IsBusy = false;
        }

        private async void Add(object parameter)
        {
            if (parameter is GeneralViewModel viewModel)
            {
                var addTire = await DialogService.ShowAddDialogAsync("Dodawanie Opony");
                if(addTire != null)
                {
                    BusyIndicatorService.Instance.IsBusy = true;
                    var add = await _tireRepository.AddTireAsync(addTire);
                    if (add.IsSuccessStatusCode)
                    {
                        TireModels.Add(addTire);
                        await DialogService.ShowSuccesDialogAsync("Sukces", "Opona została dodana pomyślnie");
                        BusyIndicatorService.Instance.IsBusy = false;
                    }
                    BusyIndicatorService.Instance.IsBusy = false;
                }
            }
        }

        private async void Edit(object parameter)
        {
            if (parameter is TireModel tireModel)
            {
                var editTire = await DialogService.ShowEditDialogAsync("Edytowanie opony", tireModel);
                if (editTire != null)
                {
                    BusyIndicatorService.Instance.IsBusy = true;
                    var edit = await _tireRepository.EditTireAsync(editTire, editTire.Id);
                    if (edit.IsSuccessStatusCode)
                    {
                        int index = TireModels.IndexOf(tireModel);
                        if (index != -1)
                        {
                            TireModels[index] = editTire;
                        }
                        await DialogService.ShowSuccesDialogAsync("Sukces", "Opona została edytowana pomyślnie");
                        BusyIndicatorService.Instance.IsBusy = false;
                    }
                    BusyIndicatorService.Instance.IsBusy = false;
                }
            }
        }

        private async void Delete(object parameter)
        {
            if (parameter is TireModel tireModel)
            {
                var Question = await DialogService.ShowQuestionDialogAsync("Pytanie", "Czy na pewno chcesz usunąć oponę?");
                if (Question)
                {
                    BusyIndicatorService.Instance.IsBusy = true;
                    var Guid = tireModel.Id;
                    var delete = await _tireRepository.DeleteTireAsync(Guid);
                    if (delete.IsSuccessStatusCode)
                    {
                        TireModels.Remove(tireModel);
                        await DialogService.ShowSuccesDialogAsync("Sukces", "Opona została usunięta pomyślnie");
                        BusyIndicatorService.Instance.IsBusy = false;
                    }
                }
                BusyIndicatorService.Instance.IsBusy = false;
            }
        }


        private async void Logout(object parameter)
        {
            if (parameter is GeneralViewModel viewModel)
            {
                var Question = await DialogService.ShowQuestionDialogAsync("Pytanie", "Czy chcesz sie wylogować?");
                if (Question)
                {
                    BusyIndicatorService.Instance.IsBusy = true;
                    DisposeResources();

                    _navigationService.NavigateTo("Login");
                    BusyIndicatorService.Instance.IsBusy = false;
                }
                BusyIndicatorService.Instance.IsBusy = false;
            }
        }

        private void DisposeResources()
        {
            _webServiceClient?.Dispose();
            _tireRepository?.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
