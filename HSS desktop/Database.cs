using MySql.Data.MySqlClient;

namespace HSS_desktop
{
    public static class Database
    {
        private static readonly string connString =
            "server=localhost;user id=root;password=;database=booking-hotel;";

        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(connString);
            conn.Open(); // buka koneksi
            return conn;
        }

    }
}
