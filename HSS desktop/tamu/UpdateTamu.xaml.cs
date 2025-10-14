using System;
using System.Windows;
using HSS_desktop.tamu;

namespace HSS_desktop.tamu
{
    public partial class UpdateTamu : Window
    {
        private TamuRepository repo = new TamuRepository();
        private long tamuId;

        public UpdateTamu(Tamu tamu)
        {
            InitializeComponent();

            // isi form dengan data tamu
            txtId.Text = tamu.Id.ToString();
            txtUserId.Text = tamu.UserId.ToString();
            txtNamaPemesan.Text = tamu.NamaTamu;
            txtNoTelp.Text = tamu.NoTelp;
            txtJumlahTamu.Text = tamu.JumlahTamu.ToString();
            tamuId = tamu.Id ?? 0;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNamaPemesan.Text) ||
                    string.IsNullOrWhiteSpace(txtNoTelp.Text) ||
                    string.IsNullOrWhiteSpace(txtJumlahTamu.Text))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!long.TryParse(txtUserId.Text, out long userId)) // pakai long
                {
                    MessageBox.Show("User ID harus berupa angka!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtJumlahTamu.Text, out int jumlahTamu))
                {
                    MessageBox.Show("Jumlah tamu harus berupa angka!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                repo.UpdateTamu(new Tamu
                {
                    Id = tamuId,
                    UserId = userId,
                    NamaTamu = txtNamaPemesan.Text,
                    NoTelp = txtNoTelp.Text,
                    JumlahTamu = jumlahTamu,
                });

                MessageBox.Show("Data tamu berhasil diupdate!", "Sukses",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true; // untuk modal
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
