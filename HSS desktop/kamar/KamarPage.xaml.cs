using System;
using System.Collections.ObjectModel;
using System.ComponentModel; // untuk INotifyPropertyChanged
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HSS_desktop.kamar;

namespace HSS_desktop
{
    public partial class KamarPage : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Kamar> Kamars { get; set; } = new ObservableCollection<Kamar>();
        private KamarRepository repo = new KamarRepository();
        public event Action<string>? OnTitleChanged;

        public string? nama_kategori { get; set; }

        // Statistik kamar dinamis
        public int JumlahKosong => Kamars.Count(k => k.status == "kosong");
        public int JumlahTerisi => Kamars.Count(k => k.status == "terisi");

        // Jumlah kategori unik kamar
        public int JumlahKategori => Kamars.Select(k => k.kategori_id).Distinct().Count();

        public int TotalKamar => Kamars.Count;

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

            // Notify UI untuk statistik
            OnPropertyChanged(nameof(Kamars));
            OnPropertyChanged(nameof(JumlahKosong));
            OnPropertyChanged(nameof(JumlahTerisi));
            OnPropertyChanged(nameof(JumlahKategori));
            OnPropertyChanged(nameof(TotalKamar));
        }

        private void UserForm_Loaded(object sender, RoutedEventArgs e)
        {
            OnTitleChanged?.Invoke("Data Kamar");
        }

        private void TambahKamar_Click(object sender, RoutedEventArgs e)
        {
            var modal = new TambahKamar();
            modal.Owner = Window.GetWindow(this);
            modal.ShowDialog();
            LoadKamar(); // refresh setelah tambah
        }

        private void EditKamar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (btn.DataContext is not Kamar selected) return;

            var modal = new UpdateKamar(selected);
            modal.Owner = Window.GetWindow(this);
            if (modal.ShowDialog() == true)
            {
                LoadKamar(); // refresh setelah update
            }
        }

        private void HapusKamar_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var kamar = btn?.DataContext as Kamar;

            if (kamar != null)
            {
                var result = MessageBox.Show(
                    $"Apakah yakin mau hapus kamar '{kamar.nama_kamar}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        repo.DeleteKamar(kamar.Id);
                        LoadKamar();
                        MessageBox.Show($"Kamar '{kamar.nama_kamar}' berhasil dihapus!",
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

        // Implementasi INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public class Kamar
        {
            public int Id { get; set; }
            public string? nama_kamar { get; set; }
            public int? kategori_id { get; set; }
            public string? nama { get; set; }
            public int Harga { get; set; }
            public int kapasitas { get; set; }
            public string? foto_kamar { get; set; }
            public string? status { get; set; }
            public string NamaKategori { get; set; }
        }
    }
}
