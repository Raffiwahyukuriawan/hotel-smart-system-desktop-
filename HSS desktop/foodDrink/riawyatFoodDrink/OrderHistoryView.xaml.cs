using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HSS_desktop.makanan.riwayat;

namespace HSS_desktop
{
    public partial class OrderHistoryView : UserControl
    {
        public event Action<string> OnTitleChanged;

        public ObservableCollection<RiwayatPesanan> RiwayatList { get; set; }
        private RiwayatPesananRepository repo = new RiwayatPesananRepository();

        public OrderHistoryView()
        {
            InitializeComponent();
            Loaded += OrderHistoryView_Loaded;
            LoadRiwayat();
            this.DataContext = this;
        }

        private void LoadRiwayat()
        {
            var dataFromDb = repo.GetRiwayat();

            int no = 1;
            foreach (var r in dataFromDb)
            {
                r.No = no++; // nomor urut di UI
            }

            RiwayatList = new ObservableCollection<RiwayatPesanan>(dataFromDb);
            DataContext = this;
        }

        private void OrderHistoryView_Loaded(object sender, RoutedEventArgs e)
        {
            OnTitleChanged?.Invoke("Halaman Riwayat Pesanan Makanan & Minuman");
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var riwayat = btn?.DataContext as RiwayatPesanan;

            if (riwayat != null)
            {
                var result = MessageBox.Show(
                    $"Apakah yakin mau hapus riwayat ID '{riwayat.Id}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        repo.DeleteRiwayat(riwayat.Id);
                        LoadRiwayat();
                        MessageBox.Show("Riwayat berhasil dihapus!",
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var riwayat = btn?.DataContext as RiwayatPesanan;

            if (riwayat == null) return;

            var window = new foodDrink.UpdateHistoryView(riwayat.Id, riwayat.Status);
            if (window.ShowDialog() == true)
            {
                try
                {
                    repo.UpdateStatus(riwayat.Id, window.StatusPesanan);
                    LoadRiwayat();
                    MessageBox.Show("Status berhasil diperbarui!",
                                    "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal update status: {ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }

    public class RiwayatPesanan
    {
        public int No { get; set; }
        public long Id { get; set; }
        public long TamuId { get; set; }
        public string NamaTamu { get; set; }   // ← TAMBAH INI
        public long MakananMinumanId { get; set; }
        public string NamaMenu { get; set; }   // ← TAMBAH INI
        public long MejaId { get; set; }
        public string NamaMeja { get; set; }   // ← TAMBAH INI
        public DateTime Tanggal { get; set; }
        public TimeSpan JamMakan { get; set; }
        public int JumlahTamu { get; set; }
        public int JumlahDipesan { get; set; }
        public string Status { get; set; } = "";

        public Brush StatusColor => Status.ToLower() switch
        {
            "dikirim" => Brushes.Green,
            "diproses" => Brushes.Orange,
            "dibatalkan" => Brushes.Red,
            _ => Brushes.Gray
        };
    }

}
