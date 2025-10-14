using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using static HSS_desktop.KamarPage;

namespace HSS_desktop.kamar
{
    public class KamarRepository
    {
        // ✅ READ 
        public List<Kamar> GetKamar()
        {
            var result = new List<Kamar>();

            using var conn = Database.GetConnection();

            string sql = @"
            SELECT k.id, k.nama_kamar, k.kategori_id, 
                   c.nama, c.Harga, c.kapasitas,
                   k.foto_kamar, k.status
            FROM kamars k
            JOIN kategori_kamars c ON k.kategori_id = c.id";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new Kamar
                {
                    Id = reader.GetInt32("id"),
                    nama_kamar = reader.GetString("nama_kamar"),
                    kategori_id = reader.GetInt32("kategori_id"),
                    nama = reader.GetString("nama"), // nama kategori
                    Harga = reader.GetInt32("Harga"),
                    kapasitas = reader.GetInt32("kapasitas"),
                    foto_kamar = reader.IsDBNull("foto_kamar") ? null : reader.GetString("foto_kamar"),
                    status = reader.GetString("status")
                });
            }

            return result;
        }

        // ✅ CREATE
        public void AddKamar(string nama_kamar, int kategori_id, string foto_kamar, string status)
        {
            using var conn = Database.GetConnection();

            string sql = @"INSERT INTO kamars (nama_kamar, kategori_id, foto_kamar, status) 
                           VALUES (@nk, @ki, @fk, @s)";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nk", nama_kamar);
            cmd.Parameters.AddWithValue("@ki", kategori_id);
            cmd.Parameters.AddWithValue("@fk", foto_kamar ?? "");
            cmd.Parameters.AddWithValue("@s", status);
            cmd.ExecuteNonQuery();
        }

        // ✅ UPDATE
        public void UpdateKamar(int id, string nama_kamar, int kategori_id, string? foto_kamar, string status)
        {
            using var conn = Database.GetConnection();

            string sql = @"UPDATE kamars 
                           SET nama_kamar=@nk, kategori_id=@ki, foto_kamar=@fk, status=@s 
                           WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nk", nama_kamar);
            cmd.Parameters.AddWithValue("@ki", kategori_id);
            cmd.Parameters.AddWithValue("@fk", (object?)foto_kamar ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@s", status);
            cmd.ExecuteNonQuery();
        }

        // ✅ DELETE
        public void DeleteKamar(int id)
        {
            using var conn = Database.GetConnection();

            string sql = "DELETE FROM kamars WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
