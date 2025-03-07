using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wulkanizacja.Desktop.Dictionary;
using Wulkanizacja.Desktop.Enums;
using Wulkanizacja.Desktop.Models;
using Wulkanizacja.Desktop.Services;

namespace Wulkanizacja.User.ViewModels
{
    public class GeneralViewModel : INotifyPropertyChanged
    {
        private readonly WebServiceClient _webServiceClient;
        private readonly TireRepository _tireRepository;
        private ObservableCollection<TireModel> _tireModels;
        private string _size;
        private TireType _selectedTireType;
        private string _selectedTranslatedTireType;
        private readonly Dictionary<string, TireType> _translatedToOriginalTireTypeMap;


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

        public ObservableCollection<TireType> TireTypes { get; }
        public ObservableCollection<string> TranslatedTireTypes { get; private set; }
        public ICommand SearchCommand { get; }

        public GeneralViewModel(WebServiceClient webServiceClient, TireRepository tireRepository)
        {
            _webServiceClient = webServiceClient;
            _tireRepository = tireRepository;

            TireModels = new ObservableCollection<TireModel>();
            TireTypes = new ObservableCollection<TireType>(Enum.GetValues(typeof(TireType)).Cast<TireType>());
            _translatedToOriginalTireTypeMap = TireTypes.ToDictionary(t => TranslateTireType(t), t => t);
            TranslatedTireTypes = new ObservableCollection<string>(_translatedToOriginalTireTypeMap.Keys);

            SearchCommand = new RelayCommand(Search);
        }
        private string TranslateTireType(TireType tireType)
        {
            string languageCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return TranslationDictionary.Translate(tireType, languageCode);
        }

        private async Task LoadDataAsync(string size, int type)
        {
            var encodedSize = Uri.EscapeDataString(size);
            var data = await _tireRepository.GetTireModelsAsync(size, (TireType)type);
            TireModels = new ObservableCollection<TireModel>(data);
        }

        private async void Search(object parameter)
        {
            if (parameter is GeneralViewModel viewModel)
            {
                var searchParameters = new SearchParameters
                {
                    Size = viewModel.Size,
                    Type = (int)viewModel.SelectedTireType
                };
               await LoadDataAsync(searchParameters.Size, searchParameters.Type);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
