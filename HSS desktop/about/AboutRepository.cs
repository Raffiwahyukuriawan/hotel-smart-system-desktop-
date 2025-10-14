using System.Collections.Generic;
using MySql.Data.MySqlClient;
using static HSS_desktop.AboutPage;

namespace HSS_desktop
{
    public class AboutRepository
    {
        // Tambah data
        public void AddAbout(string namaHotel, string foto, string alamat, string noTelp, string email, string kelas, string deskripsi)
        {
            using (var conn = Database.GetConnection())
            {
                string sql = @"INSERT INTO about_hsses 
                               (nama_hotel, foto, alamat, no_telp, email, kelas, deskripsi) 
                               VALUES (@n, @f, @a, @t, @e, @k, @d)";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@n", namaHotel);
                cmd.Parameters.AddWithValue("@f", foto ?? "");
                cmd.Parameters.AddWithValue("@a", alamat);
                cmd.Parameters.AddWithValue("@t", noTelp);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@k", kelas ?? "");
                cmd.Parameters.AddWithValue("@d", deskripsi ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        // Ambil semua data
        public List<About> GetAboutList()
        {
            var result = new List<About>();
            using (var conn = Database.GetConnection())
            {
                string sql = "SELECT id, nama_hotel, foto, alamat, no_telp, email, kelas, deskripsi FROM about_hsses";
                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new About
                    {
                        Id = reader.GetInt64("id"),
                        NamaHotel = reader.GetString("nama_hotel"),
                        Foto = reader.IsDBNull(reader.GetOrdinal("foto")) ? null : reader.GetString("foto"),
                        Alamat = reader.GetString("alamat"),
                        NoTelp = reader.GetString("no_telp"),
                        Email = reader.GetString("email"),
                        Kelas = reader.IsDBNull(reader.GetOrdinal("kelas")) ? null : reader.GetString("kelas"),
                        Deskripsi = reader.IsDBNull(reader.GetOrdinal("deskripsi")) ? null : reader.GetString("deskripsi")
                    });
                }
            }
            return result;
        }

        // Update data
        public void UpdateAbout(long id, string namaHotel, string foto, string alamat, string noTelp, string email, string kelas, string deskripsi)
        {
            using (var conn = Database.GetConnection())
            {
                string sql = @"UPDATE about_hsses 
                               SET nama_hotel=@n, foto=@f, alamat=@a, no_telp=@t, 
                                   email=@e, kelas=@k, deskripsi=@d 
                               WHERE id=@id";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@n", namaHotel);
                cmd.Parameters.AddWithValue("@f", foto ?? "");
                cmd.Parameters.AddWithValue("@a", alamat);
                cmd.Parameters.AddWithValue("@t", noTelp);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@k", kelas ?? "");
                cmd.Parameters.AddWithValue("@d", deskripsi ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        // Hapus data
        public void DeleteAbout(long id)
        {
            using (var conn = Database.GetConnection())
            {
                string sql = "DELETE FROM about_hsses WHERE id=@id";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
