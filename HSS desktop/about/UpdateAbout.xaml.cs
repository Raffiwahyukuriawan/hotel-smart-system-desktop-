using System;
using System.Windows;
using System.Windows.Media.Imaging;
using static HSS_desktop.AboutPage;

namespace HSS_desktop
{
    public partial class UpdateAbout : Window
    {
        private readonly AboutRepository repo = new AboutRepository();
        private readonly long aboutId;

        public UpdateAbout(About about)
        {
            InitializeComponent();

            aboutId = about.Id;
            txtNamaHotel.Text = about.NamaHotel;
            txtAlamat.Text = about.Alamat;
            txtNoTelp.Text = about.NoTelp;
            txtEmail.Text = about.Email;
            txtKelas.Text = about.Kelas;
            txtDeskripsi.Text = about.Deskripsi;
            txtFotoUrl.Text = about.Foto;

            // tampilkan gambar dari URL jika ada
            if (!string.IsNullOrEmpty(about.Foto))
            {
                try
                {
                    imgPreview.Source = new BitmapImage(new Uri(about.Foto));
                }
                catch
                {
                    imgPreview.Source = new BitmapImage(new Uri("https://via.placeholder.com/200x120?text=Invalid+URL"));
                }
            }
        }

        private void txtFotoUrl_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string url = txtFotoUrl.Text.Trim();
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                try
                {
                    imgPreview.Source = new BitmapImage(new Uri(url));
                }
                catch
                {
                    imgPreview.Source = new BitmapImage(new Uri("https://via.placeholder.com/200x120?text=Invalid+URL"));
                }
            }
            else
            {
                imgPreview.Source = new BitmapImage(new Uri("https://via.placeholder.com/200x120?text=No+Image"));
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nama = txtNamaHotel.Text.Trim();
                string alamat = txtAlamat.Text.Trim();
                string telp = txtNoTelp.Text.Trim();
                string email = txtEmail.Text.Trim();
                string kelas = txtKelas.Text.Trim();
                string deskripsi = txtDeskripsi.Text.Trim();
                string fotoUrl = txtFotoUrl.Text.Trim();

                if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(alamat) ||
                    string.IsNullOrWhiteSpace(telp) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                repo.UpdateAbout(aboutId, nama, fotoUrl, alamat, telp, email, kelas, deskripsi);
                MessageBox.Show("Data hotel berhasil diperbarui!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
