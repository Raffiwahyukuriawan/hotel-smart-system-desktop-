using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HSS_desktop.kamar;

namespace HSS_desktop;
public partial class MainWindow : Window
{
    public event Action<string>? OnTitleChanged;
    public MainWindow()
    {
        InitializeComponent();
        // Buka DashboardPage saat pertama kali
        var page = new DashboardPage();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title;
        };
        MainFrame.Navigate(page);

        LoadUserInfo();
    }

    private void LoadUserInfo()
    {
        txtUsername.Text = SessionManager.Username;
        txtRole.Text = SessionManager.Role;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        // Format Indonesia
        var culture = new System.Globalization.CultureInfo("id-ID");
        DateTextBlock.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy", culture);
    }


    private void BtnDashboard_Click(object sender, RoutedEventArgs e)
    {
        var page = new DashboardPage();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title;
        };
        MainFrame.Navigate(page);
    }

    private void BtnTamu_Click(object sender, RoutedEventArgs e)
    {
        var page = new TamuPage();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title;
        };
        MainFrame.Navigate(page);
    }

    private void BtnKamar_Click(object sender, RoutedEventArgs e)
    {
        var page =new ManajemenKamar();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title;
        };
        MainFrame.Navigate(page);
    }

    private void BtnRiwayatKamar_Click(object sender, RoutedEventArgs e)
    {
        var page = new RiwayatKamarUserControl();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title;
        };
        MainFrame.Navigate (page);
    }

    private void BtnFoodDrink_Click(object sender, RoutedEventArgs e)
    {
        var page = new FoodDrinkPage();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title;
        };
        MainFrame.Navigate(page);
    }

    private void BtnMeja_Click(object sender, RoutedEventArgs e)
    {
        var page = new MejaPage();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title;
        };
        MainFrame.Navigate(page);
    }

    private void About_Click(object sender, RoutedEventArgs e)
    {
        var page = new AboutPage();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title;
        };
        MainFrame.Navigate(page);
    }

    private void BtnLogout_Click(object sender, RoutedEventArgs e)
    {
        // Konfirmasi logout
        var result = MessageBox.Show("Yakin ingin logout?", "Konfirmasi Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            // Hapus session
            SessionManager.ClearSession();

            // Tampilkan kembali halaman login
            var loginWindow = new LoginWindow(); // pastikan nama class login kamu sesuai
            loginWindow.Show();

            // Tutup window utama
            this.Close();
        }
    }

    private void BtnUser(object sender, RoutedEventArgs e)
    {
        var page = new UserPage();
        page.OnTitleChanged += (title) =>
        {
            NavbarTextBlock.Text = title; // isi text di navbar
        };
        MainFrame.Navigate(page);
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        base.OnClosing(e);
        this.DataContext = null;   // lepas binding
    }

    private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        // contoh: update judul navbar sesuai halaman yang aktif
        if (NavbarTextBlock != null && e.Content is Page page)
        {
            NavbarTextBlock.Text = page.Title ?? "Dashboard";
        }
    }

}
