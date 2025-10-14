using System.Windows;
using System.Windows.Controls;
using HSS_desktop.user;

namespace HSS_desktop
{
    public partial class TambahUser : Window
    {
        private UserRepository repo = new UserRepository();

        public TambahUser()
        {
            InitializeComponent();
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string role = (txtRole.SelectedItem as ComboBoxItem)?.Content.ToString().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Username, Password, dan Role wajib diisi!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            repo.AddUser(username, password, role);
            MessageBox.Show("User berhasil disimpan", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

            // reset form
            txtUsername.Clear();
            txtPassword.Clear();
            txtRole.SelectedIndex = -1;

            this.Close(); // tutup window
        }


        private void Batal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}