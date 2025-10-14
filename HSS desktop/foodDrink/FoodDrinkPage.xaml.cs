using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace HSS_desktop
{
    public partial class FoodDrinkPage : Page
    {
        public event Action<string>? OnTitleChanged;

        public FoodDrinkPage()
        {
            InitializeComponent();
            ShowFoodDrink(); // default tampil daftar makanan
        }

        private void FoodDrinkForm_Loaded(object sender, RoutedEventArgs e)
        {
            // kirim title ke MainWindow
            OnTitleChanged?.Invoke("Manajemen Makanan & Minuman");
        }

        private void BtnFoodDrink_Click(object sender, RoutedEventArgs e)
        {
            // aktifkan tab kiri
            BtnFoodDrink.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            BtnFoodDrink.Foreground = Brushes.White;

            // nonaktifkan tab kanan
            BtnHistory.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E5E7EB"));
            BtnHistory.Foreground = Brushes.Black;

            ShowFoodDrink();
        }

        private void BtnHistory_Click(object sender, RoutedEventArgs e)
        {
            // aktifkan tab kanan
            BtnHistory.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            BtnHistory.Foreground = Brushes.White;

            // nonaktifkan tab kiri
            BtnFoodDrink.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E5E7EB"));
            BtnFoodDrink.Foreground = Brushes.Black;

            ShowHistory();
        }

        private void ShowFoodDrink()
        {
            MainContent.Content = new FoodDrinkListView(); // ✅ ganti UserControl daftar makanan
        }

        private void ShowHistory()
        {
            MainContent.Content = new OrderHistoryView(); // ✅ ganti UserControl riwayat
        }
    }
}
