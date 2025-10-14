using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HSS_desktop.kamar
{
    public partial class TambahKamar : Window
    {
        public string NamaKamar { get; private set; }
        public string Kategori { get; private set; }

        public TambahKamar()
        {
            InitializeComponent();

            try
            {
                var repoKategori = new KategoriRepository();
                var listKategori = repoKategori.GetAllKategori();

                cmbKategori.ItemsSource = listKategori;
                cmbKategori.DisplayMemberPath = "NamaKategori"; // tampil di ComboBox
                cmbKategori.SelectedValuePath = "Id";           // value yang dikirim ke DB
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal load kategori: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Tutup modal tanpa simpan
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaKamar.Text) ||
                cmbKategori.SelectedValue == null)
            {
                MessageBox.Show("Nama kamar dan kategori wajib diisi.",
                                "Validasi",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            string namaKamar = txtNamaKamar.Text.Trim();
            int kategoriId = (int)cmbKategori.SelectedValue; // ini yg masuk DB
            string fotoKamar = txtFotoKamar.Text.Trim();
            string status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            try
            {
                var repo = new KamarRepository();
                repo.AddKamar(namaKamar, kategoriId, fotoKamar, status);

                MessageBox.Show("Data kamar berhasil disimpan!",
                                "Sukses",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }



        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // hanya angka yang boleh
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
