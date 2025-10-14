using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static HSS_desktop.MejaPage;


namespace HSS_desktop
{
    public partial class TambahMeja : Window
    {
        private MejaRepository repo = new MejaRepository();
        private Meja currentMeja = null; // untuk update

        // mode tambah
        public TambahMeja()
        {
            InitializeComponent();
        }

        private void txtKapasitas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // hanya izinkan angka
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }

        // mode edit (menerima objek meja yang sudah ada)
        public TambahMeja(Meja meja)
        {
            InitializeComponent();
            currentMeja = meja;

            // isi data lama ke form
            txtNamaMeja.Text = meja.NamaMeja;
            txtKapasitas.Text = meja.Kapasitas.ToString();
            cmbStatus.SelectedItem = null;

            foreach (ComboBoxItem item in cmbStatus.Items)
            {
                if (item.Content.ToString().ToLower() == meja.Status.ToLower())
                {
                    cmbStatus.SelectedItem = item;
                    break;
                }
            }

            btnSimpan.Content = "Update"; // ubah teks tombol
            Title = "Edit Meja";
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            string namaMeja = txtNamaMeja.Text.Trim();
            string kapasitasText = txtKapasitas.Text.Trim();
            string status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(namaMeja) || string.IsNullOrWhiteSpace(kapasitasText) || string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show("Nama meja, kapasitas, dan status wajib diisi!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(kapasitasText, out int kapasitas))
            {
                MessageBox.Show("Kapasitas harus berupa angka!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (currentMeja == null)
                {
                    // mode tambah
                    repo.AddMeja(namaMeja, kapasitas, status);
                    MessageBox.Show("Meja berhasil disimpan!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // mode update
                    repo.UpdateMeja(currentMeja.Id, namaMeja, kapasitas, status);
                    MessageBox.Show("Data meja berhasil diupdate!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.Close(); // tutup window
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan data meja: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
