﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wulkanizacja.Desktop.Converters;
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
        private DateTime? _manufactureDate;
        private string _manufactureWeekYear;
        private string _comments;
        private int _quantityInStock;

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
            set { _tireType = value; OnPropertyChanged(); }
        }

        public string ManufactureWeekYear
        {
            get => _manufactureWeekYear;
            set { _manufactureWeekYear = value; OnPropertyChanged(); }
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
                string.IsNullOrWhiteSpace(ManufactureWeekYear) ||
                QuantityInStock <= 0)
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                var converter = new WeekYearToDateConverter();
                DateTimeOffset convertedDate = converter.ConvertWeekYearToDate(ManufactureWeekYear);
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
