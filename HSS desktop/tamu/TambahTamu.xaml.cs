using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using HSS_desktop.tamu;

namespace HSS_desktop.tamu
{
    public partial class TambahTamu : Window
    {
        private readonly TamuRepository repo = new TamuRepository();

        public TambahTamu()
        {
            InitializeComponent();
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            string namaPemesan = txtNamaPemesan.Text.Trim();
            string noTelp = txtNoTelp.Text.Trim();
            string jumlahTamuText = txtJumlahTamu.Text.Trim();

            // validasi wajib isi
            if (string.IsNullOrWhiteSpace(namaPemesan) ||
                string.IsNullOrWhiteSpace(noTelp) ||
                string.IsNullOrWhiteSpace(jumlahTamuText))
            {
                MessageBox.Show("Semua field wajib diisi!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // validasi angka jumlah tamu
            if (!int.TryParse(jumlahTamuText, out int jumlahTamu))
            {
                MessageBox.Show("Jumlah tamu harus berupa angka!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // nanti ambil dari session/login
            int userId = 1;

            // kasih default value biar ga pernah null
            string safeNama = string.IsNullOrWhiteSpace(namaPemesan) ? "-" : namaPemesan;
            string safeTelp = string.IsNullOrWhiteSpace(noTelp) ? "-" : noTelp;

            repo.AddTamu(
                userId,
                safeNama,
                safeTelp,
                jumlahTamu
            );

            MessageBox.Show("Data tamu berhasil disimpan", "Sukses",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // reset form
            txtNamaPemesan.Clear();
            txtNoTelp.Clear();
            txtJumlahTamu.Clear();

            this.Close();
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
