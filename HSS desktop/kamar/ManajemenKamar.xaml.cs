using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HSS_desktop
{
    /// <summary>
    /// Interaction logic for ManajemenKamar.xaml
    /// </summary>
    public partial class ManajemenKamar : Page
    {
        public event Action<string>? OnTitleChanged;
        public ManajemenKamar()
        {
            InitializeComponent();
            Loaded += UserForm_Loaded;
        }
        private void UserForm_Loaded(object sender, RoutedEventArgs e)
        {
            // kirim title ke MainWindow
            OnTitleChanged?.Invoke("Manajemen Kamar");
        }

        private void ResetButtonStyles()
        {
            // warna default: abu-abu
            BtnKamar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E5E7EB"));
            BtnKamar.Foreground = Brushes.Black;

            BtnKategori.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E5E7EB"));
            BtnKategori.Foreground = Brushes.Black;

            BtnRiwayat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E5E7EB"));
            BtnRiwayat.Foreground = Brushes.Black;
        }

        private void BtnKamar_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonStyles();
            BtnKamar.Background = Brushes.Black;
            BtnKamar.Foreground = Brushes.White;

            MainContent.Content = new KamarPage();
        }

        private void BtnKategori_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonStyles();
            BtnKategori.Background = Brushes.Black;
            BtnKategori.Foreground = Brushes.White;

            MainContent.Content = new KategoriKamarUserControl();
        }

        private void BtnRiwayat_Click(object sender, RoutedEventArgs e)
        {
            ResetButtonStyles();
            BtnRiwayat.Background = Brushes.Black;
            BtnRiwayat.Foreground = Brushes.White;

            MainContent.Content = new RiwayatKamarUserControl();
        }


    }
}
