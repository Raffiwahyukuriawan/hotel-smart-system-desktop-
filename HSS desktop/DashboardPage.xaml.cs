using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HSS_desktop.foodDrink;
using HSS_desktop.kamar;
using HSS_desktop.user;

namespace HSS_desktop
{
    public partial class DashboardPage : Page
    {
        public event Action<string> OnTitleChanged;

        private DashboardRepository dashboardRepo = new DashboardRepository();

        public DashboardPage()
        {
            InitializeComponent();
            Loaded += UserForm_Loaded;
        }

        private void UserForm_Loaded(object sender, RoutedEventArgs e)
        {
            OnTitleChanged?.Invoke("Dashboard");

            // Load banner
            string bannerPath = dashboardRepo.GetBannerImagePath();
            if (!string.IsNullOrEmpty(bannerPath))
            {
                SetBannerImage(bannerPath);
            }

            // CARD 1: Total Tamu Aktif
            try
            {
                int totalTamu = dashboardRepo.GetTotalTamuAktif();
                double persenTamu = dashboardRepo.GetPersentasePerubahan();

                TotalTamuText.Text = totalTamu.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tamu data: {ex.Message}");
            }

            // CARD 2: Kamar
            try
            {
                int kamarTersedia = dashboardRepo.GetKamarTersedia();
                int totalKamar = dashboardRepo.GetTotalKamar();
                double persenKamar = dashboardRepo.GetPersentaseKamar();

                KamarText.Text = $"{kamarTersedia}/{totalKamar}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading kamar data: {ex.Message}");
            }

            // CARD 3: Pesanan Hari Ini
            try
            {
                int pesananHariIni = dashboardRepo.GetPesananHariIni();
                double persenPesanan = dashboardRepo.GetPersentasePesanan();

                TotalPesananText.Text = pesananHariIni.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pesanan data: {ex.Message}");
            }

            // CARD 4: Total Menu (dari tabel makanan_minumans)
            try
            {
                int totalMenu = dashboardRepo.GetTotalMenu();

                TotalMenuText.Text = totalMenu.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading total menu: {ex.Message}");
            }

            // Aktivitas Terbaru → dibagi kiri dan kanan
            try
            {
                var aktivitasList = dashboardRepo.GetAktivitasTerbaru();

                var aktivitasKiri = aktivitasList.Take(5).ToList();
                var aktivitasKanan = aktivitasList.Skip(5).ToList();

                AktivitasListKiri.ItemsSource = aktivitasKiri;
                AktivitasListKanan.ItemsSource = aktivitasKanan;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading aktivitas: {ex.Message}");
            }
        }

        private void TambahKamarBaru_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new KamarPage());
        }

        private void LihatMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FoodDrinkPage());
        }

        private void TambahMenuBaru_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MejaPage());
        }

        private void LihatInvoice_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TamuPage());
        }


        private void SetBannerImage(string imagePath)
        {
            try
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                brush.Stretch = Stretch.UniformToFill;

                BannerBorder.Background = brush;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading banner image: {ex.Message}");
            }
        }
    }

    public class Aktivitas
    {
        public string Icon { get; set; }
        public string Nama { get; set; }
        public string Detail { get; set; }
        public string Waktu { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
    }
}
