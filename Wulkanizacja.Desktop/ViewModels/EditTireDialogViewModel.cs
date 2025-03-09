using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wulkanizacja.Desktop.Converters;
using Wulkanizacja.Desktop.Enums;
using Wulkanizacja.Desktop.Models;

namespace Wulkanizacja.Desktop.ViewModels
{
    public class EditTireDialogViewModel : INotifyPropertyChanged
    {
        private string? _brand;
        private string? _model;
        private string? _size;
        private string? _speedIndex;
        private string? _loadIndex;
        private TireType? _tireType;
        private string? _manufactureDate;
        private string? _comments;
        private int? _quantityInStock;
        private readonly WeekYearToDateConverter weekYearToDateConverter;

        public EditTireDialogViewModel(TireModel tireModel)
        {
            weekYearToDateConverter = new WeekYearToDateConverter();

            _brand = tireModel.Brand;
            _model = tireModel.Model;
            _size = tireModel.Size;
            _speedIndex = tireModel.SpeedIndex;
            _loadIndex = tireModel.LoadIndex;
            _tireType = tireModel.TireType;
            _manufactureDate = tireModel.ManufactureDate;
            _comments = tireModel.Comments;
            _quantityInStock = tireModel.QuantityInStock;
        }

        public string? Brand
        {
            get => _brand;
            set { _brand = value; OnPropertyChanged(); }
        }

        public string? Model
        {
            get => _model;
            set { _model = value; OnPropertyChanged(); }
        }

        public string? Size
        {
            get => _size;
            set { _size = value; OnPropertyChanged(); }
        }

        public string? SpeedIndex
        {
            get => _speedIndex;
            set { _speedIndex = value; OnPropertyChanged(); }
        }

        public string? LoadIndex
        {
            get => _loadIndex;
            set { _loadIndex = value; OnPropertyChanged(); }
        }

        public TireType? TireType
        {
            get => _tireType;
            set { _tireType = value; OnPropertyChanged(); }
        }

        public string? ManufactureDate
        {
            get => _manufactureDate;
            set { _manufactureDate = value; OnPropertyChanged(); }
        }

        public string? Comments
        {
            get => _comments;
            set { _comments = value; OnPropertyChanged(); }
        }

        public int? QuantityInStock
        {
            get => _quantityInStock;
            set { _quantityInStock = value; OnPropertyChanged(); }
        }

        public bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Brand) &&
                string.IsNullOrWhiteSpace(Model) &&
                string.IsNullOrWhiteSpace(Size) &&
                string.IsNullOrWhiteSpace(SpeedIndex) &&
                string.IsNullOrWhiteSpace(LoadIndex) &&
                TireType == 0 &&
                string.IsNullOrWhiteSpace(ManufactureDate) &&
                QuantityInStock <= 0)
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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
