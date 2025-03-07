using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wulkanizacja.Desktop.Models;
using Wulkanizacja.Desktop.Services;

namespace Wulkanizacja.User.ViewModels
{
    public class GeneralViewModel : INotifyPropertyChanged
    {
        private readonly WebServiceClient _webServiceClient;

        private ObservableCollection<TireModel> _tireModels;
        public ObservableCollection<TireModel> TireModels
        {
            get => _tireModels;
            set
            {
                _tireModels = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }

        public GeneralViewModel(WebServiceClient webServiceClient)
        {
            _webServiceClient = webServiceClient;
            TireModels = new ObservableCollection<TireModel>();
            SearchCommand = new RelayCommand(Search);
            
            LoadData("205/55 R16", 1);
        }

   
        private async void LoadData(string size, int type)
        {
            var encodedSize = Uri.EscapeDataString(size);
            var data = await _webServiceClient.GetDataAsync<IEnumerable<TireModel>>($"tires/size/{encodedSize}/TireType/{type}");
            TireModels = new ObservableCollection<TireModel>(data);
        }

        private void Search(object parameter)
        {
            // Logika wyszukiwania
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Item
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
