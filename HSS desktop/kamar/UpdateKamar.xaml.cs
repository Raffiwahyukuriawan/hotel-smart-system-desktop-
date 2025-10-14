// file: HSS_desktop/kamar/UpdateKamar.xaml.cs
using System;
using System.Windows;
using System.Windows.Controls;
using Org.BouncyCastle.Asn1.Cmp;

namespace HSS_desktop.kamar
{
    public partial class UpdateKamar : Window
    {
        private readonly KamarRepository repo = new KamarRepository();
        private int kamarId;

        public UpdateKamar(HSS_desktop.KamarPage.Kamar kamar)
        {
            InitializeComponent();

            // --- Tambahkan ini untuk load ComboBox kategori ---
            var repoKategori = new KategoriRepository();
            var listKategori = repoKategori.GetAllKategori();

            cmbKategori.ItemsSource = listKategori;
            cmbKategori.DisplayMemberPath = "NamaKategori"; // tampil di ComboBox
            cmbKategori.SelectedValuePath = "Id";           // value yang dikirim ke DB

            // --- isi form dengan data kamar ---
            kamarId = kamar.Id;
            txtId.Text = kamar.Id.ToString();
            txtNamaKamar.Text = kamar.nama_kamar ?? string.Empty;

            // pilih kategori sesuai data kamar
            if (kamar.kategori_id.HasValue)
                cmbKategori.SelectedValue = kamar.kategori_id.Value;

            txtFoto.Text = kamar.foto_kamar ?? string.Empty;

            // pilih status di ComboBox (jika ada)
            if (!string.IsNullOrEmpty(kamar.status))
            {
                foreach (var item in cmbStatus.Items)
                {
                    if (item is ComboBoxItem cbi &&
                        string.Equals((cbi.Content as string) ?? "", kamar.status, StringComparison.OrdinalIgnoreCase))
                    {
                        cmbStatus.SelectedItem = cbi;
                        break;
                    }
                }
            }
            if (cmbStatus.SelectedItem == null && cmbStatus.Items.Count > 0)
                cmbStatus.SelectedIndex = 0;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // validasi sederhana
            if (string.IsNullOrWhiteSpace(txtNamaKamar.Text) ||
                cmbKategori.SelectedValue == null || // ganti txtKategoriId.Text
                cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Nama kamar, kategori, dan status wajib diisi.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int kategoriId = (int)cmbKategori.SelectedValue; // ambil langsung dari ComboBox
            string nama = txtNamaKamar.Text.Trim();
            string? foto = string.IsNullOrWhiteSpace(txtFoto.Text) ? null : txtFoto.Text.Trim();
            string status = ((ComboBoxItem)cmbStatus.SelectedItem).Content?.ToString() ?? "kosong";

            // panggil repository (foto boleh null)
            repo.UpdateKamar(kamarId, nama, kategoriId, foto, status);

            MessageBox.Show("Data kamar berhasil diupdate.", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

            this.DialogResult = true; // beri tahu pemanggil untuk refresh
            this.Close();
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
