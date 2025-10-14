using System;
using System.Windows;
using HSS_desktop.kamar.riwayat;
using Org.BouncyCastle.Asn1.Cmp;

namespace HSS_desktop.kamar.riwayat
{
    public partial class UpdateRiwayat : Window
    {
        private RiwayatKamarRepository repo = new RiwayatKamarRepository();
        private int riwayatId;

        public UpdateRiwayat(RiwayatKamarViewModel riwayat)
        {
            InitializeComponent();

            // isi form dengan data riwayat
            txtId.Text = riwayat.Id.ToString();
            txtKamarId.Text = riwayat.KamarId.ToString();
            txtTamuId.Text = riwayat.TamuId.ToString();
            dpCheckIn.SelectedDate = riwayat.CheckIn;
            dpCheckOut.SelectedDate = riwayat.CheckOut;
            cmbStatus.Text = riwayat.Status;

            riwayatId = riwayat.Id;

            Loaded += (s, e) =>
            {
                this.Left = (SystemParameters.WorkArea.Width - this.ActualWidth) / 2 + SystemParameters.WorkArea.Left;
                this.Top = (SystemParameters.WorkArea.Height - this.ActualHeight) / 2 + SystemParameters.WorkArea.Top;
            };
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtKamarId.Text) ||
                    string.IsNullOrWhiteSpace(txtTamuId.Text) ||
                    dpCheckIn.SelectedDate == null ||
                    dpCheckOut.SelectedDate == null ||
                    string.IsNullOrWhiteSpace(cmbStatus.Text))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtKamarId.Text, out int kamarId))
                {
                    MessageBox.Show("Kamar ID harus berupa angka!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtTamuId.Text, out int tamuId))
                {
                    MessageBox.Show("Tamu ID harus berupa angka!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime checkIn = dpCheckIn.SelectedDate.Value;
                DateTime checkOut = dpCheckOut.SelectedDate.Value;

                repo.UpdateRiwayat(
                    riwayatId,
                    kamarId,
                    tamuId,
                    checkIn,
                    checkOut,
                    cmbStatus.Text
                );

                MessageBox.Show("Data berhasil diupdate!", "Sukses",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
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
