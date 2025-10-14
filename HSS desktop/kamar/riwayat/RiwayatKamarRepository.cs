using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HSS_desktop.kamar.riwayat
{
    public class RiwayatKamarViewModel
    {
        public int No { get; set; }
        public int Id { get; set; }
        public int KamarId { get; set; }
        public int TamuId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; }
        public string nama_kamar { get; set; }
        public string nama_tamu { get; set; }
    }


    public class RiwayatKamarRepository
    {
        public void AddRiwayat(int kamarId, int tamuId, DateTime checkIn, DateTime checkOut, string status)
        {
            using var conn = Database.GetConnection();

            string sql = @"INSERT INTO riwayat_kamars 
                           (kamar_id, tamu_id, check_in, check_out, status) 
                           VALUES (@k, @t, @ci, @co, @s)";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@k", kamarId);
            cmd.Parameters.AddWithValue("@t", tamuId);
            cmd.Parameters.AddWithValue("@ci", checkIn);
            cmd.Parameters.AddWithValue("@co", checkOut);
            cmd.Parameters.AddWithValue("@s", status);
            cmd.ExecuteNonQuery();
        }

        public List<RiwayatKamarViewModel> GetRiwayat()
        {
            var result = new List<RiwayatKamarViewModel>();

            using var conn = Database.GetConnection();

            string sql = @"
        SELECT r.id, r.kamar_id, r.tamu_id, r.check_in, r.check_out, r.status,
               k.nama_kamar AS nama_kamar,
               t.nama_tamu AS nama_tamu
        FROM riwayat_kamars r
        JOIN kamars k ON r.kamar_id = k.id
        JOIN tamus t ON r.tamu_id = t.id
        ORDER BY r.check_in DESC";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            int no = 1;
            while (reader.Read())
            {
                result.Add(new RiwayatKamarViewModel
                {
                    No = no++,
                    Id = reader.GetInt32("id"),
                    KamarId = reader.GetInt32("kamar_id"),
                    TamuId = reader.GetInt32("tamu_id"),
                    CheckIn = reader.GetDateTime("check_in"),
                    CheckOut = reader.GetDateTime("check_out"),
                    Status = reader.GetString("status"),
                    nama_kamar = reader.IsDBNull(reader.GetOrdinal("nama_kamar")) ? "" : reader.GetString("nama_kamar"),
                    nama_tamu = reader.IsDBNull(reader.GetOrdinal("nama_tamu")) ? "" : reader.GetString("nama_tamu")
                });
            }

            return result;
        }

        public void UpdateRiwayat(int id, int kamarId, int tamuId, DateTime checkIn, DateTime checkOut, string status)
        {
            using var conn = Database.GetConnection();

            string sql = @"UPDATE riwayat_kamars 
                           SET kamar_id=@k, tamu_id=@t, check_in=@ci, check_out=@co, status=@s 
                           WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@k", kamarId);
            cmd.Parameters.AddWithValue("@t", tamuId);
            cmd.Parameters.AddWithValue("@ci", checkIn);
            cmd.Parameters.AddWithValue("@co", checkOut);
            cmd.Parameters.AddWithValue("@s", status);
            cmd.ExecuteNonQuery();
        }

        public void DeleteRiwayat(int id)
        {
            using var conn = Database.GetConnection();

            string sql = "DELETE FROM riwayat_kamars WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
