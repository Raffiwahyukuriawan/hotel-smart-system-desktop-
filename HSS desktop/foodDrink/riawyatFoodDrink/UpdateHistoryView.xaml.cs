using System;
using System.Windows;
using System.Windows.Controls;
using Org.BouncyCastle.Asn1.Cmp;

namespace HSS_desktop.foodDrink
{
    public partial class UpdateHistoryView : Window
    {
        public long RiwayatId { get; private set; }
        public string StatusPesanan { get; private set; }

        public UpdateHistoryView(long id, string currentStatus)
        {
            InitializeComponent();
            RiwayatId = id;
            txtId.Text = id.ToString();

            // set status awal sesuai data
            cmbStatus.SelectedItem = FindComboBoxItem(cmbStatus, currentStatus);
        }

        private ComboBoxItem FindComboBoxItem(ComboBox comboBox, string text)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (string.Equals(item.Content.ToString(), text, StringComparison.OrdinalIgnoreCase))
                    return item;
            }
            return null;
        }

        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(selectedStatus))
            {
                MessageBox.Show("Pilih status terlebih dahulu.",
                                "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StatusPesanan = selectedStatus;
            DialogResult = true;
        }
    }
}
