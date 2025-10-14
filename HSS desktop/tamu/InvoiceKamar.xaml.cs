using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace HSS_desktop
{
    public partial class InvoiceKamar : Window
    {
        public InvoiceKamars InvoiceData { get; set; }
        private readonly AboutRepository aboutRepo = new AboutRepository();

        public InvoiceKamar(InvoiceKamars invoice)
        {
            InitializeComponent();
            InvoiceData = invoice;
            DataContext = InvoiceData;

            LoadInvoice();
        }

        private void LoadInvoice()
        {
            // HEADER
            txtInvoiceNumber.Text = "000" + InvoiceData.Id;
            txtInvoiceDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            // DETAIL TAMU
            txtNamaTamu.Text = InvoiceData.NamaTamu;
            txtNoTelp.Text = InvoiceData.NoTelp;
            txtJumlahTamu.Text = InvoiceData.JumlahTamu.ToString();

            // DETAIL KAMAR
            txtNamaKamar.Text = InvoiceData.NamaKamar;
            txtHargaKamar.Text = $"Rp {InvoiceData.HargaKamar:N0}";
            txtCatatanKhusus.Text = InvoiceData.CatatanKhusus;
            txtCheckIn.Text = InvoiceData.CheckIn?.ToString("dd/MM/yyyy") ?? "-";
            txtCheckOut.Text = InvoiceData.CheckOut?.ToString("dd/MM/yyyy") ?? "-";
            txtLamaMenginap.Text = $"{InvoiceData.LamaMenginap} Hari";
            txtStatusKamar.Text = InvoiceData.StatusKamar;

            // DETAIL TAGIHAN
            txtTotalTagihan.Text = $"Rp {InvoiceData.TotalTagihan:N0}";
            txtStatusPembayaran.Text = InvoiceData.StatusPembayaran;

            // FOOTER - Info Hotel (NoTelp dan Email dari DB)
            var aboutList = aboutRepo.GetAboutList();
            if (aboutList.Any())
            {
                var hotelInfo = aboutList.First();

                // Deskripsi tetap statis
                txtDeskripsiHotel.Text = "Terima kasih telah menggunakan layanan kami.";

                // NoTelp & Email dinamis
                txtKontakHotel.Text = $"📞 {hotelInfo.NoTelp} | ✉️ {hotelInfo.Email}";
            }
            else
            {
                txtDeskripsiHotel.Text = "Terima kasih telah menggunakan layanan kami.";
                txtKontakHotel.Text = "📞 - | ✉️ -";
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();

                if (printDialog.ShowDialog() == true)
                {
                    // Ukuran halaman print
                    Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

                    // Ambil kontainer utama window untuk dicetak
                    var visual = this.Content as FrameworkElement;

                    // Atur ulang layout biar pas di halaman
                    visual.Measure(pageSize);
                    visual.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));

                    // Cetak visual
                    printDialog.PrintVisual(visual, "Invoice Kamar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencetak invoice: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

    public class InvoiceKamars
    {
        public long Id { get; set; }
        public string NamaTamu { get; set; } = string.Empty;
        public string NoTelp { get; set; } = string.Empty;
        public int JumlahTamu { get; set; }

        // Data Kamar
        public long KamarId { get; set; }
        public string NamaKamar { get; set; } = string.Empty;
        public decimal HargaKamar { get; set; }
        public string CatatanKhusus { get; set; } = string.Empty;
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string StatusKamar { get; set; } = string.Empty;

        // Data Tagihan
        public decimal TotalTagihan { get; set; }
        public string StatusPembayaran { get; set; } = string.Empty;

        // Hitungan otomatis
        public int LamaMenginap
        {
            get
            {
                if (CheckIn.HasValue && CheckOut.HasValue)
                {
                    return (CheckOut.Value - CheckIn.Value).Days;
                }
                return 0;
            }
        }
    }
}
