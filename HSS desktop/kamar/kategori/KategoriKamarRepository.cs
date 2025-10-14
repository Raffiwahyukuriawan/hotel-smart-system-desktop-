using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HSS_desktop.kamar.kategori
{
    public class KategoriKamarRepository
    {
        public void AddKategori(string nama, int kapasitas, decimal harga)
        {
            using var conn = Database.GetConnection();

            string sql = "INSERT INTO kategori_kamars (nama, kapasitas, harga) VALUES (@n, @k, @h)";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@n", nama);
            cmd.Parameters.AddWithValue("@k", kapasitas);
            cmd.Parameters.AddWithValue("@h", harga);
            cmd.ExecuteNonQuery();
        }

        public List<KategoriKamar> GetKategori()
        {
            var result = new List<KategoriKamar>();

            using var conn = Database.GetConnection();

            string sql = "SELECT id, nama, kapasitas, harga FROM kategori_kamars";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new KategoriKamar
                {
                    Id = reader.GetInt32("id"),
                    Nama = reader.GetString("nama"),
                    Kapasitas = reader.GetInt32("kapasitas"),
                    Harga = reader.GetDecimal("harga")
                });
            }

            return result;
        }

        public void UpdateKategori(int id, string nama, int kapasitas, decimal harga)
        {
            using var conn = Database.GetConnection();

            string sql = "UPDATE kategori_kamars SET nama=@n, kapasitas=@k, harga=@h WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@n", nama);
            cmd.Parameters.AddWithValue("@k", kapasitas);
            cmd.Parameters.AddWithValue("@h", harga);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteKategori(int id)
        {
            using var conn = Database.GetConnection();

            string sql = "DELETE FROM kategori_kamars WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
