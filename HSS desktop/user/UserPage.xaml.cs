
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Navigation;
using HSS_desktop.user;
using MaterialDesignThemes.Wpf;

namespace HSS_desktop
{
    public partial class UserPage : Page
    {
        public event Action<string> OnTitleChanged;

        public ObservableCollection<User> Users { get; set; }
        private UserRepository repo = new UserRepository();
        public UserPage()
        {
            InitializeComponent();
            Loaded += UserPage_Loaded;
            LoadUsers();
            this.DataContext = this;

        }

        private void LoadUsers()
        {
            var usersFromDb = repo.GetUsers();

            int no = 1;
            foreach (var u in usersFromDb)
            {
                u.No = no++; // kasih nomor urut manual
            }

            Users = new ObservableCollection<User>(usersFromDb);
            DataContext = this;
        }


        private void UserPage_Loaded(object sender, RoutedEventArgs e)
        {
            // kirim title ke MainWindow
            OnTitleChanged?.Invoke("Halaman User");
        }

        private void TambahUser_Click(object sender, RoutedEventArgs e)
        {
            var modal = new TambahUser();
            modal.Owner = Window.GetWindow(this); // tetap bisa dipakai
            modal.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var user = btn?.DataContext as User;

            if (user != null)
            {
                var modal = new UpdateUser(user); // kirim user ke constructor
                modal.Owner = Window.GetWindow(this); // set parent window
                if (modal.ShowDialog() == true)
                {
                    // refresh DataGrid setelah update
                    LoadUsers();
                }
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as User; // ambil user dari baris yang diklik

            if (user != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Apakah yakin mau hapus user '{user.Username}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var repo = new UserRepository();
                        repo.DeleteUser(user.Id); // panggil fungsi repository

                        // refresh data grid
                        LoadUsers();

                        MessageBox.Show($"User '{user.Username}' berhasil dihapus!",
                                        "Sukses",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan: {ex.Message}",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
            }
        }

    }
    public class User
    {
        public int No { get; set; }      // nomor urut
        public int Id { get; set; }      // id asli dari database
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "";
    }


    public class RoleToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string role = value?.ToString()?.ToLower();

            switch (role)
            {
                case "admin":
                    return Brushes.Red;   // admin merah
                case "tamu":
                    return Brushes.Green; // tamu hijau
                case "resepsionis":
                    return Brushes.Blue;  // contoh resepsionis biru
                default:
                    return Brushes.Gray;  // fallback
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



}
