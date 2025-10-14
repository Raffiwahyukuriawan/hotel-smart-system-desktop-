using System;
using System.Collections.Generic;
using System.Windows;

namespace HSS_desktop
{
    public partial class Invoice : Window
    {
        public TamuInvoice InvoiceData { get; set; }

        public Invoice(TamuInvoice invoice)
        {
            InitializeComponent();
            InvoiceData = invoice;

            DataContext = InvoiceData;

            LoadInvoice();
        }

        private void LoadInvoice()
        {
            // Update bagian header
            txtInvoiceNumber.Text = "000" + InvoiceData.Id;
            txtInvoiceDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            // Update detail
            txtNamaTamu.Text = InvoiceData.NamaTamu;
            txtNoTelp.Text = InvoiceData.NoTelp;
            txtJumlahTamu.Text = InvoiceData.JumlahTamu.ToString();

            txtTanggal.Text = InvoiceData.Tanggal.ToString("MM/dd/yyyy");
            txtJamMakan.Text = InvoiceData.JamMakan?.ToString(@"hh\:mm") ?? "-";
            txtStatusPesanan.Text = InvoiceData.StatusPesanan;

            txtCatatanKhusus.Text = InvoiceData.CatatanKhusus;
            txtCheckIn.Text = InvoiceData.CheckIn?.ToString("MM/dd/yyyy") ?? "-";
            txtCheckOut.Text = InvoiceData.CheckOut?.ToString("MM/dd/yyyy") ?? "-";
            txtStatusKamar.Text = InvoiceData.StatusKamar;

            // Update tabel item pesanan (contoh statis, bisa diambil dari DB nanti)
            List<InvoiceItem> items = new List<InvoiceItem>
            {
                new InvoiceItem { Description = "Makanan", UnitCost = 50000, Quantity = InvoiceData.JumlahDipesan, Amount = 50000 * InvoiceData.JumlahDipesan },
                new InvoiceItem { Description = "Minuman", UnitCost = 20000, Quantity = InvoiceData.JumlahDipesan, Amount = 20000 * InvoiceData.JumlahDipesan }
            };
            dataGridItems.ItemsSource = items;

            // Hitung subtotal
            decimal subtotal = 0;
            foreach (var item in items)
                subtotal += item.Amount;
            txtSubtotal.Text = subtotal.ToString("C");

            // Hitung total (contoh tanpa pajak dan diskon)
            txtTotal.Text = subtotal.ToString("C");
        }
    }

    public class InvoiceItem
    {
        public string Description { get; set; }
        public decimal UnitCost { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }

        public class TamuInvoice
        {
            public long Id { get; set; }
            public string NamaTamu { get; set; } = string.Empty;
            public string NoTelp { get; set; } = string.Empty;
            public int JumlahTamu { get; set; }

            public long PesananId { get; set; }
            public long MakananMinumanId { get; set; }
            public DateTime Tanggal { get; set; }
            public TimeSpan? JamMakan { get; set; }
            public long MejaId { get; set; }
            public int JumlahDipesan { get; set; }
            public string StatusPesanan { get; set; } = string.Empty;

            public long KamarId { get; set; }
            public string CatatanKhusus { get; set; } = string.Empty;
            public DateTime? CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }
            public string StatusKamar { get; set; } = string.Empty;
        }
    }


