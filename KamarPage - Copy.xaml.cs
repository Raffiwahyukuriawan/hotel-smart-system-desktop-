using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HSS_desktop.kamar;

namespace HSS_desktop
{
    public partial class KamarPage : Page
    {
        public ObservableCollection<Kamar> Kamars { get; set; }
        private KamarRepository repo = new KamarRepository();
        public event Action<string> OnTitleChanged;

        public KamarPage()
        {
            InitializeComponent();
            Loaded += UserForm_Loaded;
            LoadKamar();
        }

        private void LoadKamar()
        {
            Kamars = new ObservableCollection<Kamar>(repo.GetKamar());
            DataContext = this;
        }

        private void UserForm_Loaded(object sender, RoutedEventArgs e)
        {
            // kirim title ke MainWindow
            OnTitleChanged?.Invoke("Data Kamar");
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public class Kamar
        {
            public int Id { get; set; }
            public string nama_kamar { get; set; }
            public string kategori_id { get; set; }
            public string foto_kamar { get; set; }
            public string status { get; set; }
        }
    }
}


