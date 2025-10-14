using System.Windows;
using MySql.Data.MySqlClient;

namespace HSS_desktop
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan password wajib diisi!", "Peringatan",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    string sql = "SELECT * FROM users WHERE username = @username AND password = @password LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string role = reader["role"].ToString();

                                if (role != "admin")
                                {
                                    MessageBox.Show("Akses ditolak! Hanya admin yang bisa login.", "Error",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }

                                // Simpan data ke session
                                SessionManager.UserId = Convert.ToInt32(reader["id"]);
                                SessionManager.Username = reader["username"].ToString();
                                SessionManager.Role = role;

                                MessageBox.Show("Login berhasil!", "Sukses",
                                    MessageBoxButton.OK, MessageBoxImage.Information);

                                this.DialogResult = true;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Username atau password salah!", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Gagal terhubung ke database: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
