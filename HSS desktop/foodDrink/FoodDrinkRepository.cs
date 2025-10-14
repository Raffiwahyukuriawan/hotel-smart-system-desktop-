using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HSS_desktop.foodDrink
{
    public class MakananMinumanRepository
    {
        public void AddMakananMinuman(string kategori, string foto, string nama, decimal harga)
        {
            using (var conn = Database.GetConnection()) // koneksi sudah terbuka
            {
                string sql = @"INSERT INTO makanan_minumans 
                               (kategori, foto, nama, harga) 
                               VALUES (@kategori, @foto, @nama, @harga)";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kategori", kategori);
                cmd.Parameters.AddWithValue("@foto", foto ?? "");
                cmd.Parameters.AddWithValue("@nama", nama ?? "");
                cmd.Parameters.AddWithValue("@harga", harga);
                cmd.ExecuteNonQuery();
            }
        }

        public List<MakananMinuman> GetMakananMinuman()
        {
            var result = new List<MakananMinuman>();
            using (var conn = Database.GetConnection())
            {
                string sql = "SELECT id, kategori, foto, nama, harga FROM makanan_minumans";
                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new MakananMinuman
                    {
                        Id = reader["id"] != DBNull.Value ? Convert.ToInt64(reader["id"]) : 0,
                        Kategori = reader["kategori"]?.ToString() ?? "",
                        Foto = reader["foto"]?.ToString() ?? "",
                        Nama = reader["nama"]?.ToString() ?? "",
                        Harga = reader["harga"] != DBNull.Value ? Convert.ToDecimal(reader["harga"]) : 0
                    });
                }
            }
            return result;
        }

        public void UpdateMakananMinuman(MakananMinuman mm)
        {
            using (var conn = Database.GetConnection())
            {
                string sql = @"UPDATE makanan_minumans 
                       SET kategori=@kategori, foto=@foto, nama=@nama, harga=@harga 
                       WHERE id=@id";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", mm.Id);
                cmd.Parameters.AddWithValue("@kategori", mm.Kategori ?? "");
                cmd.Parameters.AddWithValue("@foto", mm.Foto ?? "");
                cmd.Parameters.AddWithValue("@nama", mm.Nama ?? "");
                cmd.Parameters.AddWithValue("@harga", mm.Harga);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteMakananMinuman(long id)
        {
            using (var conn = Database.GetConnection())
            {
                string sql = "DELETE FROM makanan_minumans WHERE id=@id";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
