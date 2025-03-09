using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wulkanizacja.Desktop.Converters;
using Wulkanizacja.Desktop.Dictionary;
using Wulkanizacja.Desktop.Enums;

namespace Wulkanizacja.Desktop.ViewModels
{
    public class AddTireDialogViewModel : INotifyPropertyChanged
    {
        private string _brand;
        private string _model;
        private string _size;
        private string _speedIndex;
        private string _loadIndex;
        private TireType _tireType;
        private string _manufactureDate;
        private string _comments;
        private int _quantityInStock;
        private string _selectedTranslatedTireType;
        private readonly Dictionary<string, TireType> _translatedToOriginalTireTypeMap;

        public AddTireDialogViewModel()
        {
            TireTypes = new ObservableCollection<TireType>(Enum.GetValues(typeof(TireType)).Cast<TireType>());
            _translatedToOriginalTireTypeMap = TireTypes.ToDictionary(t => TranslateTireType(t), t => t);
            TranslatedTireTypes = new ObservableCollection<string>(_translatedToOriginalTireTypeMap.Keys);
        }

        public ObservableCollection<TireType> TireTypes { get; }
        public ObservableCollection<string> TranslatedTireTypes { get; private set; }

        public string Brand
        {
            get => _brand;
            set { _brand = value; OnPropertyChanged(); }
        }

        public string Model
        {
            get => _model;
            set { _model = value; OnPropertyChanged(); }
        }

        public string Size
        {
            get => _size;
            set { _size = value; OnPropertyChanged(); }
        }

        public string SpeedIndex
        {
            get => _speedIndex;
            set { _speedIndex = value; OnPropertyChanged(); }
        }

        public string LoadIndex
        {
            get => _loadIndex;
            set { _loadIndex = value; OnPropertyChanged(); }
        }

        public TireType TireType
        {
            get => _tireType;
            set
            {
                if (_tireType != value)
                {
                    _tireType = value;
                    OnPropertyChanged(nameof(TireType));
                }
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
                    TireType = originalTireType;
                }
                OnPropertyChanged();
            }
        }

        public string ManufactureDate
        {
            get => _manufactureDate;
            set { _manufactureDate = value; OnPropertyChanged(); }
        }

        public string Comments
        {
            get => _comments;
            set { _comments = value; OnPropertyChanged(); }
        }

        public int QuantityInStock
        {
            get => _quantityInStock;
            set { _quantityInStock = value; OnPropertyChanged(); }
        }

        public bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Brand) ||
                string.IsNullOrWhiteSpace(Model) ||
                string.IsNullOrWhiteSpace(Size) ||
                string.IsNullOrWhiteSpace(SpeedIndex) ||
                string.IsNullOrWhiteSpace(LoadIndex) ||
                TireType == 0 ||
                string.IsNullOrWhiteSpace(ManufactureDate) ||
                QuantityInStock <= 0)
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                var converter = new WeekYearToDateConverter();
                DateTimeOffset convertedDate = converter.ConvertWeekYearToDate(ManufactureDate);
                if (convertedDate > DateTimeOffset.Now)
                {
                    MessageBox.Show("Data wynikająca z 'Tydzień produkcji' nie może być późniejsza od dzisiejszej daty.",
                                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd przy przeliczaniu tygodnia produkcji: {ex.Message}",
                                "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private string TranslateTireType(TireType tireType)
        {
            string languageCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return TranslationDictionary.Translate(tireType, languageCode);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
