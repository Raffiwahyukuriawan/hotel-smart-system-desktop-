using System;
using System.Windows;

namespace HSS_desktop
{
    public class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.Startup += App_Startup;
            app.Run();
        }

        private static void App_Startup(object sender, StartupEventArgs e)
        {
            var loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
