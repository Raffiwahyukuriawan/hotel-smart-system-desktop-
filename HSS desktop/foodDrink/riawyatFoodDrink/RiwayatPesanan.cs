using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HSS_desktop.makanan.riwayat
{
    public class RiwayatPesananRepository
    {
        // ❌ HAPUS ini
        // private MySqlConnection conn = Database.GetConnection();

        // ✅ tiap method bikin koneksi baru dan auto dispose dengan 'using'
        public List<RiwayatPesanan> GetRiwayat()
        {
            var list = new List<RiwayatPesanan>();

            using var conn = Database.GetConnection();

            string sql = @"
        SELECT 
            r.id,
            r.nama_tamu_id,
            t.nama_tamu AS nama_tamu,
            r.makanan_minuman_id,
            m.nama AS nama_menu,
            r.meja_id,
            mj.nama_meja,
            r.tanggal,
            r.jam_makan,
            r.jumlah_tamu,
            r.jumlah_dipesan,
            r.status
        FROM riwayat_pesanan_makanan_minuman r
        LEFT JOIN tamus t ON r.nama_tamu_id = t.id
        LEFT JOIN makanan_minumans m ON r.makanan_minuman_id = m.id
        LEFT JOIN mejas mj ON r.meja_id = mj.id
        ORDER BY r.id DESC;
    ";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new RiwayatPesanan
                {
                    Id = reader.GetInt64("id"),
                    TamuId = reader.GetInt64("nama_tamu_id"),
                    NamaTamu = reader["nama_tamu"].ToString(),
                    MakananMinumanId = reader.GetInt64("makanan_minuman_id"),
                    NamaMenu = reader["nama_menu"].ToString(),
                    MejaId = reader.GetInt64("meja_id"),
                    NamaMeja = reader["nama_meja"].ToString(),
                    Tanggal = reader.GetDateTime("tanggal"),
                    JamMakan = reader.GetTimeSpan("jam_makan"),
                    JumlahTamu = reader.GetInt32("jumlah_tamu"),
                    JumlahDipesan = reader.GetInt32("jumlah_dipesan"),
                    Status = reader.GetString("status")
                });
            }

            return list;
        }

        public void UpdateStatus(long id, string status)
        {
            using var conn = Database.GetConnection();
            string sql = "UPDATE riwayat_pesanan_makanan_minuman SET status=@status WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteRiwayat(long id)
        {
            using var conn = Database.GetConnection(); // koneksi baru
            string sql = "DELETE FROM riwayat_pesanan_makanan_minuman WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
