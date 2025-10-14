using System;
using System.Text.RegularExpressions;
using System.Windows;
using static HSS_desktop.AboutPage;

namespace HSS_desktop
{
    public partial class TambahAbout : Window
    {
        private readonly AboutRepository repo = new AboutRepository();
        private readonly About? currentAbout;

        public TambahAbout()
        {
            InitializeComponent();
        }

        public TambahAbout(About about)
        {
            InitializeComponent();
            currentAbout = about;

            txtNamaHotel.Text = about.NamaHotel;
            txtFoto.Text = about.Foto;
            txtAlamat.Text = about.Alamat;
            txtNoTelp.Text = about.NoTelp;
            txtEmail.Text = about.Email;
            txtKelas.Text = about.Kelas;
            txtDeskripsi.Text = about.Deskripsi;

            btnSimpan.Content = "Update";
            Title = "Edit Data Hotel";
        }

        private void txtNoTelp_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            string namaHotel = txtNamaHotel.Text.Trim();
            string foto = txtFoto.Text.Trim();
            string alamat = txtAlamat.Text.Trim();
            string noTelp = txtNoTelp.Text.Trim();
            string email = txtEmail.Text.Trim();
            string kelas = txtKelas.Text.Trim();
            string deskripsi = txtDeskripsi.Text.Trim();

            if (string.IsNullOrWhiteSpace(namaHotel) ||
                string.IsNullOrWhiteSpace(alamat) ||
                string.IsNullOrWhiteSpace(noTelp) ||
                string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Nama hotel, alamat, no. telp, dan email wajib diisi!",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (currentAbout == null)
                {
                    repo.AddAbout(namaHotel, foto, alamat, noTelp, email, kelas, deskripsi);
                    MessageBox.Show("Data hotel berhasil disimpan!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    repo.UpdateAbout(currentAbout.Id, namaHotel, foto, alamat, noTelp, email, kelas, deskripsi);
                    MessageBox.Show("Data hotel berhasil diupdate!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan data hotel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
