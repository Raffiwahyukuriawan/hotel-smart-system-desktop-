using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HSS_desktop
{
    public partial class AboutPage : Page
    {
        public ObservableCollection<About> AboutList { get; set; } = new ObservableCollection<About>();
        private AboutRepository repo = new AboutRepository();
        public event Action<string>? OnTitleChanged;

        public AboutPage()
        {
            InitializeComponent();
            Loaded += AboutPage_Loaded;
            LoadAbout();
        }

        private void LoadAbout()
        {
            AboutList = new ObservableCollection<About>(repo.GetAboutList());
            DataContext = this;
        }

        private void AboutPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Kirim title ke MainWindow
            OnTitleChanged?.Invoke("Tentang Hotel");
        }

        private void TambahAbout_Click(object sender, RoutedEventArgs e)
        {
            var modal = new TambahAbout();
            modal.Owner = Window.GetWindow(this);
            modal.ShowDialog();
            LoadAbout(); // refresh data setelah tambah
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (btn.DataContext is not About selected) return;

            var modal = new UpdateAbout(selected);
            modal.Owner = Window.GetWindow(this);
            if (modal.ShowDialog() == true)
            {
                LoadAbout();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var about = btn?.DataContext as About;

            if (about != null)
            {
                var result = MessageBox.Show(
                    $"Apakah yakin mau hapus data hotel '{about.NamaHotel}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        repo.DeleteAbout(about.Id);
                        LoadAbout();
                        MessageBox.Show($"Data '{about.NamaHotel}' berhasil dihapus!",
                                        "Sukses",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan: {ex.Message}",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
            }
        }

        public class About
        {
            public long Id { get; set; }
            public string NamaHotel { get; set; } = string.Empty;
            public string? Foto { get; set; }
            public string Alamat { get; set; } = string.Empty;
            public string NoTelp { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string? Kelas { get; set; }
            public string? Deskripsi { get; set; }
        }
    }
}
