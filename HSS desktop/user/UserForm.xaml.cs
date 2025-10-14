using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HSS_desktop.user;

namespace HSS_desktop
{
    public partial class UserForm : Page
    {
        private UserRepository repo = new UserRepository();
        private int? userId;
        public event Action<string> OnTitleChanged;

        public UserForm(int? id = null)
        {
            InitializeComponent();
            userId = id;
            Loaded += UserForm_Loaded;

        }

        private void UserForm_Loaded(object sender, RoutedEventArgs e)
        {
            // kirim title ke MainWindow
            OnTitleChanged?.Invoke("Halaman Form User");
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string role = txtRole.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password wajib diisi!");
                return;
            }

            repo.AddUser(username, password, role);
            MessageBoxResult result = MessageBox.Show(
                "User berhasil disimpan",
                "Sukses",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );


            // reset form
            txtUsername.Clear();
            txtPassword.Clear();
            txtRole.Clear();

            NavigationService?.GoBack();
        }

        private void Kembali_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                // kalau tidak pakai Frame Navigation, bisa ganti manual
                // misalnya balik ke UserPage
                this.NavigationService?.Navigate(new UserPage());
            }
        }

    }
}
