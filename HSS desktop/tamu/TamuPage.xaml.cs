using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HSS_desktop.tamu;
using Mysqlx.Crud;

namespace HSS_desktop
{
    public partial class TamuPage : Page
    {
        public event Action<string> OnTitleChanged;

        public ObservableCollection<Tamu> TamuList { get; set; }
        private TamuRepository repo = new TamuRepository();

        public TamuPage()
        {
            InitializeComponent();
            Loaded += TamuPage_Loaded;
            LoadTamu();
            this.DataContext = this;
        }

        private void LoadTamu()
        {
            var tamuFromDb = repo.GetTamu();

            int no = 1;
            foreach (var t in tamuFromDb)
            {
                t.No = no++; // nomor urut
            }

            TamuList = new ObservableCollection<Tamu>(tamuFromDb);
            DataContext = this;
        }

        private void TamuPage_Loaded(object sender, RoutedEventArgs e)
        {
            OnTitleChanged?.Invoke("Halaman Data Tamu");
        }

        private void TambahTamu_Click(object sender, RoutedEventArgs e)
        {
            var modal = new TambahTamu();
            modal.Owner = Window.GetWindow(this);
            modal.ShowDialog();
            LoadTamu();
        }

        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var tamu = button.DataContext as Tamu;
            if (tamu == null) return;

            var repo = new TamuRepository();

            var invoiceMakanan = repo.GetInvoiceMakananMinumanById(tamu.Id ?? 0);
            var invoiceKamar = repo.GetInvoiceKamarById(tamu.Id ?? 0);

            var result = MessageBox.Show(
                "Pilih jenis invoice yang ingin dilihat:\n\nYes = Makanan & Minuman\nNo = Kamar",
                "Pilih Invoice",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                if (invoiceMakanan != null)
                {
                    var window = new InvoiceMakanan(invoiceMakanan);
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tidak ada data makanan/minuman untuk tamu ini.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (result == MessageBoxResult.No)
            {
                if (invoiceKamar != null)
                {
                    var window = new InvoiceKamar(invoiceKamar);
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tidak ada data kamar untuk tamu ini.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tamu = btn?.DataContext as Tamu;

            if (tamu != null)
            {
                var modal = new UpdateTamu(tamu);
                modal.Owner = Window.GetWindow(this);
                if (modal.ShowDialog() == true)
                {
                    LoadTamu();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tamu = btn?.DataContext as Tamu;

            if (tamu != null)
            {
                var result = MessageBox.Show(
                    $"Apakah yakin mau hapus tamu '{tamu.NamaTamu}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        repo.DeleteTamu(tamu.Id ?? 0); // panggil repository
                        LoadTamu(); // refresh data
                        MessageBox.Show($"Tamu '{tamu.NamaTamu}' berhasil dihapus!",
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

    }

    public class Tamu
    {
        // UI-only ordinal (set in page, not from DB)
        public int No { get; set; }

        // DB fields (use long for BIGINT)
        public long? Id { get; set; }
        public long? UserId { get; set; }

        // these are strings (use ToString when reading)
        public string? NamaTamu { get; set; } = "";
        public string? NoTelp { get; set; } = "";

        public int? JumlahTamu { get; set; }

        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string? CatatanKhusus { get; set; } = "";
    }

   

}
