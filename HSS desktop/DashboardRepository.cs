using MySql.Data.MySqlClient;
using System;

namespace HSS_desktop.user
{
    public class DashboardRepository
    {
        public string GetBannerImagePath()
        {
            string imagePath = null;
            using (var conn = Database.GetConnection())
            {
                string sql = "SELECT foto FROM about_hsses ORDER BY id DESC LIMIT 1";
                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    imagePath = reader.GetString("foto");
                }
            }
            return imagePath;
        }

        public int GetTotalTamuAktif()
        {
            int total = 0;
            using (var conn = Database.GetConnection())
            {
                string sql = @"
                    SELECT SUM(jumlah_tamu) 
                    FROM tamus 
                    WHERE MONTH(created_at) = MONTH(CURRENT_DATE()) 
                      AND YEAR(created_at) = YEAR(CURRENT_DATE())";
                using var cmd = new MySqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    total = Convert.ToInt32(result);
            }
            return total;
        }

        public double GetPersentasePerubahan()
        {
            int bulanIni = 0, bulanLalu = 0;

            using (var conn = Database.GetConnection())
            {
                string sqlBulanIni = @"
                    SELECT SUM(jumlah_tamu) 
                    FROM tamus 
                    WHERE MONTH(created_at) = MONTH(CURRENT_DATE()) 
                      AND YEAR(created_at) = YEAR(CURRENT_DATE())";
                using var cmdBulanIni = new MySqlCommand(sqlBulanIni, conn);
                var resultIni = cmdBulanIni.ExecuteScalar();
                if (resultIni != DBNull.Value && resultIni != null)
                    bulanIni = Convert.ToInt32(resultIni);

                string sqlBulanLalu = @"
                    SELECT SUM(jumlah_tamu) 
                    FROM tamus 
                    WHERE MONTH(created_at) = MONTH(DATE_SUB(CURRENT_DATE(), INTERVAL 1 MONTH)) 
                      AND YEAR(created_at) = YEAR(DATE_SUB(CURRENT_DATE(), INTERVAL 1 MONTH))";
                using var cmdBulanLalu = new MySqlCommand(sqlBulanLalu, conn);
                var resultLalu = cmdBulanLalu.ExecuteScalar();
                if (resultLalu != DBNull.Value && resultLalu != null)
                    bulanLalu = Convert.ToInt32(resultLalu);
            }

            if (bulanLalu == 0) return 100;
            return ((double)(bulanIni - bulanLalu) / bulanLalu) * 100;
        }

        // ===== Tambahan untuk CARD 2 =====
        public int GetKamarTersedia()
        {
            int tersedia = 0;
            using (var conn = Database.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM kamars WHERE status = 'tersedia'";
                using var cmd = new MySqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    tersedia = Convert.ToInt32(result);
            }
            return tersedia;
        }

        public int GetTotalKamar()
        {
            int total = 0;
            using (var conn = Database.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM kamars";
                using var cmd = new MySqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    total = Convert.ToInt32(result);
            }
            return total;
        }

        public double GetPersentaseKamar()
        {
            int bulanIni = 0, bulanLalu = 0;

            using (var conn = Database.GetConnection())
            {
                string sqlBulanIni = @"
                    SELECT COUNT(*) 
                    FROM kamars 
                    WHERE status = 'tersedia' 
                      AND MONTH(updated_at) = MONTH(CURRENT_DATE()) 
                      AND YEAR(updated_at) = YEAR(CURRENT_DATE())";
                using var cmdBulanIni = new MySqlCommand(sqlBulanIni, conn);
                var resultIni = cmdBulanIni.ExecuteScalar();
                if (resultIni != DBNull.Value && resultIni != null)
                    bulanIni = Convert.ToInt32(resultIni);

                string sqlBulanLalu = @"
                    SELECT COUNT(*) 
                    FROM kamars 
                    WHERE status = 'tersedia' 
                      AND MONTH(updated_at) = MONTH(DATE_SUB(CURRENT_DATE(), INTERVAL 1 MONTH)) 
                      AND YEAR(updated_at) = YEAR(DATE_SUB(CURRENT_DATE(), INTERVAL 1 MONTH))";
                using var cmdBulanLalu = new MySqlCommand(sqlBulanLalu, conn);
                var resultLalu = cmdBulanLalu.ExecuteScalar();
                if (resultLalu != DBNull.Value && resultLalu != null)
                    bulanLalu = Convert.ToInt32(resultLalu);
            }

            if (bulanLalu == 0) return 100;
            return ((double)(bulanIni - bulanLalu) / bulanLalu) * 100;
        }

        public int GetPesananHariIni()
        {
            int total = 0;
            using (var conn = Database.GetConnection())
            {
                string sql = @"
                SELECT COUNT(*) 
                FROM riwayat_pesanan_makanan_minuman 
                WHERE tanggal = CURRENT_DATE()";
                using var cmd = new MySqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    total = Convert.ToInt32(result);
            }
            return total;
        }

        public double GetPersentasePesanan()
        {
            int hariIni = GetPesananHariIni();
            int bulanLalu = 0;

            using (var conn = Database.GetConnection())
            {
                string sql = @"
                SELECT COUNT(*) 
                FROM riwayat_pesanan_makanan_minuman 
                WHERE MONTH(tanggal) = MONTH(DATE_SUB(CURRENT_DATE(), INTERVAL 1 MONTH)) 
                  AND YEAR(tanggal) = YEAR(DATE_SUB(CURRENT_DATE(), INTERVAL 1 MONTH))";
                using var cmd = new MySqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    bulanLalu = Convert.ToInt32(result);
            }

            if (bulanLalu == 0) return 100;
            return ((double)(hariIni - bulanLalu) / bulanLalu) * 100;
        }

        public List<Aktivitas> GetAktivitasTerbaru()
        {
            var list = new List<Aktivitas>();
            using (var conn = Database.GetConnection())
            {
                // Aktivitas kamar
                string sqlKamar = @"
            SELECT rk.id, rk.catatan_khusus, rk.check_in, rk.check_out, rk.status, k.nama_kamar, t.nama_tamu AS nama
            FROM riwayat_kamars rk
            JOIN kamars k ON rk.kamar_id = k.id
            JOIN tamus t ON rk.tamu_id = t.id
            ORDER BY rk.created_at DESC
            LIMIT 5";
                using var cmdKamar = new MySqlCommand(sqlKamar, conn);
                using var reader = cmdKamar.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Aktivitas
                    {
                        Icon = "🏨",
                        Nama = $"{reader["nama"]} Kamar {reader["nama_kamar"]}",
                        Detail = $"Check-in ke kamar {reader["nama_kamar"]}",
                        Waktu = "Baru saja", // Bisa ditambah kalkulasi waktu
                        Status = reader["status"].ToString(),
                        StatusColor = reader["status"].ToString() == "selesai" ? "Green" :
                                      reader["status"].ToString() == "ditempati" ? "Blue" :
                                      "Orange"
                    });
                }

                reader.Close();

                // Aktivitas makanan/minuman
                string sqlMakanan = @"
            SELECT rpm.id, t.nama_tamu AS nama, k.nama AS nama_makanan, rpm.jumlah_dipesan, rpm.status, rpm.tanggal
            FROM riwayat_pesanan_makanan_minuman rpm
            JOIN tamus t ON rpm.nama_tamu_id = t.id
            JOIN makanan_minumans k ON rpm.makanan_minuman_id = k.id
            ORDER BY rpm.created_at DESC
            LIMIT 5";
                using var cmdMakanan = new MySqlCommand(sqlMakanan, conn);
                using var readerMakanan = cmdMakanan.ExecuteReader();
                while (readerMakanan.Read())
                {
                    list.Add(new Aktivitas
                    {
                        Icon = "🍽️",
                        Nama = $"{readerMakanan["nama"]}",
                        Detail = $"Memesan {readerMakanan["nama_makanan"]} ({readerMakanan["jumlah_dipesan"]})",
                        Waktu = "Baru saja", // Bisa ditambah kalkulasi waktu
                        Status = readerMakanan["status"].ToString(),
                        StatusColor = readerMakanan["status"].ToString() == "diproses" ? "#2563EB" :
                                      readerMakanan["status"].ToString() == "dikirim" ? "Green" :
                                      "Orange"
                    });
                }
            }

            return list.OrderByDescending(a => a.Waktu).ToList();
        }

        public int GetTotalMenu()
        {
            int total = 0;
            using (var conn = Database.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM makanan_minumans";
                using var cmd = new MySqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    total = Convert.ToInt32(result);
            }
            return total;
        }
    }
}
