using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HSS_desktop.foodDrink
{
    public partial class TambahFoodDrink : Window
    {
        private readonly MakananMinumanRepository repo = new MakananMinumanRepository();

        public TambahFoodDrink()
        {
            InitializeComponent();
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            string kategori = (cmbKategori.SelectedItem as ComboBoxItem)?.Content.ToString();
            string foto = txtFoto.Text.Trim();
            string nama = txtNama.Text.Trim();
            string hargaText = txtHarga.Text.Trim();

            if (string.IsNullOrWhiteSpace(kategori) ||
                string.IsNullOrWhiteSpace(nama) ||
                string.IsNullOrWhiteSpace(hargaText))
            {
                MessageBox.Show("Semua field wajib diisi!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(hargaText, out decimal harga))
            {
                MessageBox.Show("Harga harus berupa angka!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            repo.AddMakananMinuman(kategori, foto, nama, harga);

            MessageBox.Show("Data makanan/minuman berhasil disimpan", "Sukses",
                MessageBoxButton.OK, MessageBoxImage.Information);

            txtFoto.Clear();
            txtNama.Clear();
            txtHarga.Clear();
            cmbKategori.SelectedIndex = -1;

            this.Close();
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
