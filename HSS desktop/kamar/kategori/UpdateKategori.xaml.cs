using System;
using System.Windows;
using HSS_desktop.kamar.kategori;

namespace HSS_desktop.kamar.kategori
{
    public partial class UpdateKategori : Window
    {
        private KategoriKamarRepository repo = new KategoriKamarRepository();
        private int kategoriId;

        public UpdateKategori(KategoriKamar kategori)
        {
            InitializeComponent();

            // isi form dengan data kategori
            txtId.Text = kategori.Id.ToString();
            txtNama.Text = kategori.Nama;
            txtKapasitas.Text = kategori.Kapasitas.ToString();
            txtHarga.Text = kategori.Harga.ToString("F2");

            kategoriId = kategori.Id;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                    string.IsNullOrWhiteSpace(txtKapasitas.Text) ||
                    string.IsNullOrWhiteSpace(txtHarga.Text))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtKapasitas.Text, out int kapasitas))
                {
                    MessageBox.Show("Kapasitas harus berupa angka!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtHarga.Text, out decimal harga))
                {
                    MessageBox.Show("Harga harus berupa angka!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                repo.UpdateKategori(
                    kategoriId,
                    txtNama.Text,
                    kapasitas,
                    harga
                );

                MessageBox.Show("Data berhasil diupdate!", "Sukses",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true; // kalau dipanggil pakai ShowDialog
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
