using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace HSS_desktop
{
    public partial class InvoiceMakanan : Window
    {
        // 🔹 Koleksi untuk tabel pesanan
        public ObservableCollection<InvoiceMakananMinuman> DaftarPesanan { get; set; } = new ObservableCollection<InvoiceMakananMinuman>();

        // 🔹 Data tamu
        public InvoiceMakananMinuman InvoiceData { get; set; }
        private readonly AboutRepository aboutRepo = new AboutRepository();

        public InvoiceMakanan(InvoiceMakananMinuman data)
        {
            InitializeComponent();
            InvoiceData = data;
            DataContext = InvoiceData;

            LoadInvoice();
        }

        private void LoadInvoice()
        {
            txtInvoiceNumber.Text = "INV-MM-" + InvoiceData.Id.ToString("D4");
            txtTanggal.Text = InvoiceData.Tanggal?.ToString("dd/MM/yyyy") ?? "-";
            txtJamMakan.Text = InvoiceData.JamMakan ?? "-";
            txtNamaTamu.Text = InvoiceData.NamaTamu;
            txtJumlahTamu.Text = InvoiceData.JumlahTamu.ToString();
            txtMejaId.Text = InvoiceData.MejaId.ToString();

            // 🔹 Ambil semua menu yang dipesan tamu ini dari database
            LoadPesananDariDatabase(InvoiceData.Id);

            // 🔹 Tampilkan daftar pesanan ke DataGrid
            dataGridPesanan.ItemsSource = DaftarPesanan;

            // 🔹 FOOTER - Info Hotel (NoTelp dan Email dari DB)
            var aboutList = aboutRepo.GetAboutList();
            if (aboutList.Any())
            {
                var hotelInfo = aboutList.First();
                txtDeskripsiHotel.Text = "Terima kasih telah menggunakan layanan kami.";
                txtKontakHotel.Text = $"📞 {hotelInfo.NoTelp} | ✉️ {hotelInfo.Email}";
            }
            else
            {
                txtDeskripsiHotel.Text = "Terima kasih telah menggunakan layanan kami.";
                txtKontakHotel.Text = "📞 - | ✉️ -";
            }
        }

        private void LoadPesananDariDatabase(long tamuId)
        {
            string sql = @"
                SELECT 
                    rpm.makanan_minuman_id,
                    m.nama,
                    m.harga,
                    rpm.jumlah_dipesan,
                    rpm.status AS status_pesanan
                FROM riwayat_pesanan_makanan_minuman rpm
                JOIN makanan_minumans m ON m.id = rpm.makanan_minuman_id
                WHERE rpm.nama_tamu_id = @tamuId";

            using (var conn = Database.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@tamuId", tamuId);

                int totalHargaSemuaMenu = 0;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int harga = Convert.ToInt32(reader["harga"]);
                        int jumlah = Convert.ToInt32(reader["jumlah_dipesan"]);
                        int subtotal = harga * jumlah;

                        DaftarPesanan.Add(new InvoiceMakananMinuman
                        {
                            NamaMakananMinuman = reader["nama"].ToString(),
                            Harga = harga,
                            JumlahDipesan = jumlah,
                            StatusPesanan = reader["status_pesanan"].ToString(),
                            TotalHarga = subtotal
                        });

                        totalHargaSemuaMenu += subtotal;
                    }
                }

                // 💸 Biaya layanan tetap Rp 15.000
                int biayaLayanan = 15000;
                int totalBayar = totalHargaSemuaMenu + biayaLayanan;

                // 💬 Tampilkan hasil ke UI
                txtTotalMenu.Text = $"Total Menu: {DaftarPesanan.Count}";
                txtBiayaLayanan.Text = $"Biaya Layanan: Rp {biayaLayanan:N0}";
                txtTotalHarga.Text = $"Total Bayar: Rp {totalBayar:N0}";

            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();

                if (printDialog.ShowDialog() == true)
                {
                    Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                    this.Measure(pageSize);
                    this.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
                    printDialog.PrintVisual(this, "Invoice Makanan & Minuman");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencetak invoice: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class InvoiceMakananMinuman
    {
        public long Id { get; set; }
        public string NamaTamu { get; set; } = string.Empty;
        public string? NoTelp { get; set; }   // kalau mau nampilin no telp
        public int JumlahTamu { get; set; }
        public long MakananMinumanId { get; set; }
        public string NamaMakananMinuman { get; set; } = string.Empty;
        public DateTime? Tanggal { get; set; }
        public string? JamMakan { get; set; }
        public long MejaId { get; set; }
        public int JumlahDipesan { get; set; }
        public string StatusPesanan { get; set; } = string.Empty;
        public int? TotalHarga { get; set; }
        public int Harga { get; set; }

        // 🔹 Tambahkan dua properti baru agar error hilang
        public int BiayaLayanan { get; set; }

        // 🔹 Daftar pesanan untuk binding (pakai ObservableCollection)
        public ObservableCollection<InvoiceMakananMinuman> DaftarPesanan { get; set; }
            = new ObservableCollection<InvoiceMakananMinuman>();
    }

}
