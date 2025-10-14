using System;
using System.Windows;

namespace HSS_desktop.foodDrink
{
    public partial class UpdateMakananMinuman : Window
    {
        private MakananMinumanRepository repo = new MakananMinumanRepository();
        private long makananMinumanId;

        public UpdateMakananMinuman(MakananMinuman mm)
        {
            InitializeComponent();

            // isi form dengan data makanan/minuman
            txtId.Text = mm.Id.ToString();
            cmbKategori.SelectedItem = new System.Windows.Controls.ComboBoxItem { Content = mm.Kategori };
            txtFoto.Text = mm.Foto;
            txtNama.Text = mm.Nama;
            txtHarga.Text = mm.Harga?.ToString("F2");
            makananMinumanId = mm.Id ?? 0;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbKategori.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtNama.Text) ||
                    string.IsNullOrWhiteSpace(txtHarga.Text))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtHarga.Text, out decimal harga))
                {
                    MessageBox.Show("Harga harus berupa angka!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string kategori = (cmbKategori.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();

                repo.UpdateMakananMinuman(new MakananMinuman
                {
                    Id = makananMinumanId,
                    Kategori = kategori,
                    Foto = txtFoto.Text.Trim(),
                    Nama = txtNama.Text.Trim(),
                    Harga = harga
                });

                MessageBox.Show("Data makanan/minuman berhasil diupdate!", "Sukses",
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

        private void NumberOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            return decimal.TryParse(text, out _);
        }

    }
}
