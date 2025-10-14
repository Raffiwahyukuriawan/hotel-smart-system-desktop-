using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HSS_desktop
{
    public partial class MejaPage : Page
    {
        public ObservableCollection<Meja> Mejas { get; set; } = new ObservableCollection<Meja>();
        private MejaRepository repo = new MejaRepository();
        public event Action<string>? OnTitleChanged;

        public MejaPage()
        {
            InitializeComponent();
            Loaded += UserForm_Loaded;
            LoadMeja();
        }

        private void LoadMeja()
        {
            Mejas = new ObservableCollection<Meja>(repo.GetMejaList());
            DataContext = this;
        }

        private void UserForm_Loaded(object sender, RoutedEventArgs e)
        {
            // kirim title ke MainWindow
            OnTitleChanged?.Invoke("Data Meja");
        }

        private void TambahMeja_Click(object sender, RoutedEventArgs e)
        {
            var modal = new TambahMeja();
            modal.Owner = Window.GetWindow(this);
            modal.ShowDialog();
            LoadMeja(); // refresh data setelah tambah
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (btn.DataContext is not Meja selected) return;

            var modal = new HSS_desktop.UpdateMeja(selected);
            modal.Owner = Window.GetWindow(this);
            if (modal.ShowDialog() == true)
            {
                LoadMeja();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var meja = btn?.DataContext as Meja;

            if (meja != null)
            {
                var result = MessageBox.Show(
                    $"Apakah yakin mau hapus meja '{meja.NamaMeja}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        repo.DeleteMeja(meja.Id);
                        LoadMeja();
                        MessageBox.Show($"Meja '{meja.NamaMeja}' berhasil dihapus!",
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

        public class Meja
        {
            public int Id { get; set; }
            public string? NamaMeja { get; set; }
            public int Kapasitas { get; set; }
            public string? Status { get; set; }
        }
    }
}
