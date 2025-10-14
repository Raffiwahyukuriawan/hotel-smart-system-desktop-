using System.Windows;

namespace HSS_desktop
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                // kalau login berhasil
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                // kalau login gagal atau user tutup window login
                Current.Shutdown();
            }
        }
    }
}
