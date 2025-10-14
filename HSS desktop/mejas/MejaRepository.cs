using System.Collections.Generic;
using MySql.Data.MySqlClient;
using static HSS_desktop.MejaPage;

namespace HSS_desktop
{
    public class MejaRepository
    {
        public void AddMeja(string namaMeja, int kapasitas, string status)
        {
            using (var conn = Database.GetConnection()) // koneksi sudah terbuka
            {
                string sql = "INSERT INTO mejas (nama_meja, kapasitas, status, created_at, updated_at) VALUES (@n, @k, @s, NOW(), NOW())";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@n", namaMeja);
                cmd.Parameters.AddWithValue("@k", kapasitas);
                cmd.Parameters.AddWithValue("@s", status);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Meja> GetMejaList()
        {
            var result = new List<Meja>();
            using (var conn = Database.GetConnection()) // koneksi sudah terbuka
            {
                string sql = "SELECT id, nama_meja, kapasitas, status FROM mejas";
                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Meja
                    {
                        Id = (int)reader.GetInt64("id"),
                        NamaMeja = reader.GetString("nama_meja"),
                        Kapasitas = reader.GetInt32("kapasitas"),
                        Status = reader.GetString("status")
                    });
                }
            }
            return result;
        }

        public void UpdateMeja(long id, string namaMeja, int kapasitas, string status)
        {
            using (var conn = Database.GetConnection()) // koneksi sudah terbuka
            {
                string sql = "UPDATE mejas SET nama_meja=@n, kapasitas=@k, status=@s, updated_at=NOW() WHERE id=@id";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@n", namaMeja);
                cmd.Parameters.AddWithValue("@k", kapasitas);
                cmd.Parameters.AddWithValue("@s", status);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMeja(long id)
        {
            using (var conn = Database.GetConnection()) // koneksi sudah terbuka
            {
                string sql = "DELETE FROM mejas WHERE id=@id";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
