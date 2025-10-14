using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace HSS_desktop.kamar.kategori
{
    public partial class TambahKategori : Window
    {
        private KategoriKamarRepository repo = new KategoriKamarRepository();

        public TambahKategori()
        {
            InitializeComponent();
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            string nama = txtNama.Text.Trim();
            string kapasitasText = txtKapasitas.Text.Trim();
            string hargaText = txtHarga.Text.Trim();

            if (string.IsNullOrWhiteSpace(nama) ||
                string.IsNullOrWhiteSpace(kapasitasText) ||
                string.IsNullOrWhiteSpace(hargaText))
            {
                MessageBox.Show("Nama, Kapasitas, dan Harga wajib diisi!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(kapasitasText, out int kapasitas))
            {
                MessageBox.Show("Kapasitas harus berupa angka!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(hargaText, out decimal harga))
            {
                MessageBox.Show("Harga harus berupa angka!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            repo.AddKategori(nama, kapasitas, harga);
            MessageBox.Show("Kategori berhasil disimpan", "Sukses",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // reset form
            txtNama.Clear();
            txtKapasitas.Clear();
            txtHarga.Clear();

            this.Close(); // tutup window
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); // hanya angka
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
