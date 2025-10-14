using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HSS_desktop.user
{
    public class UserRepository
    {
        public void AddUser(string username, string password, string role)
        {
            using (var conn = Database.GetConnection()) // koneksi sudah dibuka
            {
                string sql = "INSERT INTO users (username, password, role) VALUES (@u, @p, @r)";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@r", role);
                cmd.ExecuteNonQuery();
            }
        }

        public List<User> GetUsers()
        {
            var result = new List<User>();
            using (var conn = Database.GetConnection())
            {
                string sql = "SELECT id, username, password, role FROM users";
                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new User
                    {
                        Id = reader.GetInt32("id"),
                        Username = reader.GetString("username"),
                        Password = reader.GetString("password"),
                        Role = reader.GetString("role")
                    });
                }
            }
            return result;
        }

        public void UpdateUser(int id, string username, string password, string role)
        {
            using (var conn = Database.GetConnection())
            {
                string sql = "UPDATE users SET username=@u, password=@p, role=@r WHERE id=@id";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@r", role);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            using (var conn = Database.GetConnection())
            {
                string sql = "DELETE FROM users WHERE id=@id";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
