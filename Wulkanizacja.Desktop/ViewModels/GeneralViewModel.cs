using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Wulkanizacja.User.ViewModels
{
    public class GeneralViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }

        public GeneralViewModel()
        {
            Items = new ObservableCollection<Item>();
            SearchCommand = new RelayCommand(Search);
            LoadItems();
        }

        private void LoadItems()
        {
            Items.Add(new Item { Property1 = "Value1", Property2 = "Value2" });
            Items.Add(new Item { Property1 = "Value3", Property2 = "Value4" });
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
