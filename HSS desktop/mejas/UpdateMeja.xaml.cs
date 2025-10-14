using System;
using System.Windows;
using System.Windows.Controls;

namespace HSS_desktop
{
    public partial class UpdateMeja : Window
    {
        private MejaRepository repo = new MejaRepository();
        private int mejaId;

        public UpdateMeja(MejaPage.Meja meja)
        {
            InitializeComponent();

            // isi form dengan data meja
            txtId.Text = meja.Id.ToString();
            txtNamaMeja.Text = meja.NamaMeja;
            txtKapasitas.Text = meja.Kapasitas.ToString();
            cmbStatus.SelectedItem = GetStatusItem(meja.Status);

            mejaId = meja.Id;
        }

        private ComboBoxItem GetStatusItem(string status)
        {
            foreach (ComboBoxItem item in cmbStatus.Items)
            {
                if (item.Content.ToString().Equals(status, StringComparison.OrdinalIgnoreCase))
                    return item;
            }
            return null;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string namaMeja = txtNamaMeja.Text.Trim();
                string kapasitasText = txtKapasitas.Text.Trim();
                string status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (string.IsNullOrWhiteSpace(namaMeja) ||
                    string.IsNullOrWhiteSpace(kapasitasText) ||
                    string.IsNullOrWhiteSpace(status))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(kapasitasText, out int kapasitas))
                {
                    MessageBox.Show("Kapasitas harus berupa angka!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                repo.UpdateMeja(mejaId, namaMeja, kapasitas, status);

                MessageBox.Show("Data meja berhasil diupdate!", "Sukses",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true; // jika window dipanggil dengan ShowDialog()
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
