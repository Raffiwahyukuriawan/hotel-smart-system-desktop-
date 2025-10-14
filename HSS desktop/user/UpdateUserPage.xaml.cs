using System;
using System.Windows;
using System.Windows.Controls;
using HSS_desktop.user;

namespace HSS_desktop
{
    public partial class UpdateUser : Window
    {
        private UserRepository repo = new UserRepository();
        private int userId;

        public UpdateUser(User user)
        {
            InitializeComponent();

            // isi form dengan data user
            txtId.Text = user.Id.ToString();
            txtUsername.Text = user.Username;
            txtPassword.Password = user.Password;
            txtRole.SelectedItem = GetRoleItem(user.Role);

            userId = user.Id;
        }

        private ComboBoxItem GetRoleItem(string role)
        {
            foreach (ComboBoxItem item in txtRole.Items)
            {
                if (item.Content.ToString().Equals(role, StringComparison.OrdinalIgnoreCase))
                    return item;
            }
            return null;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string role = (txtRole.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Password) ||
                    string.IsNullOrWhiteSpace(role))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                repo.UpdateUser(
                    userId,
                    txtUsername.Text,
                    txtPassword.Password,
                    role
                );

                MessageBox.Show("Data berhasil diupdate!", "Sukses",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true; // kalau dipanggil pakai ShowDialog
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
