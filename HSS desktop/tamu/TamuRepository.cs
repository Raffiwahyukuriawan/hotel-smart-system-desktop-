using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace HSS_desktop.tamu
{
    public class TamuRepository
    {
        // CREATE
        public void AddTamu(long userId, string namaTamu, string noTelp, int jumlahTamu)
        {
            string sql = @"INSERT INTO tamus 
                           (user_id, nama_tamu, no_telp, jumlah_tamu) 
                           VALUES (@u, @n, @nt, @jt)";

            using (var conn = Database.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@u", userId);
                cmd.Parameters.AddWithValue("@n", namaTamu);
                cmd.Parameters.AddWithValue("@nt", noTelp);
                cmd.Parameters.AddWithValue("@jt", jumlahTamu);
                cmd.ExecuteNonQuery();
            }
        }

        // READ
        public List<Tamu> GetTamu()
        {
            var result = new List<Tamu>();
            string sql = "SELECT id, user_id, nama_tamu, no_telp, jumlah_tamu FROM tamus";

            using (var conn = Database.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                int idxId = reader.GetOrdinal("id");
                int idxUserId = reader.GetOrdinal("user_id");
                int idxNama = reader.GetOrdinal("nama_tamu");
                int idxTelp = reader.GetOrdinal("no_telp");
                int idxJumlah = reader.GetOrdinal("jumlah_tamu");

                while (reader.Read())
                {
                    result.Add(new Tamu
                    {
                        Id = reader.IsDBNull(idxId) ? (long?)null : reader.GetInt64(idxId),
                        UserId = reader.IsDBNull(idxUserId) ? (long?)null : reader.GetInt64(idxUserId),
                        NamaTamu = reader.IsDBNull(idxNama) ? "" : reader.GetString(idxNama),
                        NoTelp = reader.IsDBNull(idxTelp) ? "" : reader.GetString(idxTelp),
                        JumlahTamu = reader.IsDBNull(idxJumlah) ? (int?)null : reader.GetInt32(idxJumlah),
                    });
                }
            }

            return result;
        }

        // UPDATE
        public void UpdateTamu(Tamu tamu)
        {
            string sql = @"UPDATE tamus 
                           SET user_id=@userId, nama_tamu=@namaTamu, no_telp=@noTelp, 
                               jumlah_tamu=@jumlahTamu 
                           WHERE id=@id";

            using (var conn = Database.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", tamu.Id ?? 0);
                cmd.Parameters.AddWithValue("@userId", tamu.UserId ?? 0);
                cmd.Parameters.AddWithValue("@namaTamu", tamu.NamaTamu ?? "");
                cmd.Parameters.AddWithValue("@noTelp", tamu.NoTelp ?? "");
                cmd.Parameters.AddWithValue("@jumlahTamu", tamu.JumlahTamu ?? 0);
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE
        public void DeleteTamu(long id)
        {
            string sql = "DELETE FROM tamus WHERE id=@id";

            using (var conn = Database.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public InvoiceMakananMinuman GetInvoiceMakananMinumanById(long tamuId)
        {
            InvoiceMakananMinuman invoice = null;

            string sql = @"
        SELECT 
            t.id AS tamu_id, 
            t.nama_tamu, 
            t.no_telp, 
            t.jumlah_tamu,
            rpm.id AS pesanan_id, 
            rpm.makanan_minuman_id, 
            m.nama AS nama_makanan_minuman,
            m.harga,
            rpm.tanggal, 
            rpm.jam_makan, 
            rpm.meja_id, 
            rpm.jumlah_dipesan,            
            rpm.status AS status_pesanan
        FROM tamus t
        LEFT JOIN riwayat_pesanan_makanan_minuman rpm 
            ON t.id = rpm.nama_tamu_id
        LEFT JOIN makanan_minumans m 
            ON rpm.makanan_minuman_id = m.id
        WHERE t.id = @tamuId
    ";

            using (var conn = Database.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@tamuId", tamuId);

                using (var reader = cmd.ExecuteReader())
                {
                    ObservableCollection<InvoiceMakananMinuman> daftarPesanan = new ObservableCollection<InvoiceMakananMinuman>();
                    int totalHargaKeseluruhan = 0;

                    while (reader.Read())
                    {
                        if (invoice == null)
                        {
                            // Inisialisasi data tamu hanya sekali
                            invoice = new InvoiceMakananMinuman
                            {
                                Id = reader.GetInt64("tamu_id"),
                                NamaTamu = reader["nama_tamu"].ToString() ?? "",
                                JumlahTamu = reader.GetInt32("jumlah_tamu"),
                                Tanggal = reader.IsDBNull(reader.GetOrdinal("tanggal")) ? (DateTime?)null : reader.GetDateTime("tanggal"),
                                JamMakan = reader["jam_makan"].ToString(),
                                MejaId = reader.IsDBNull(reader.GetOrdinal("meja_id")) ? 0 : reader.GetInt64("meja_id"),
                                DaftarPesanan = new ObservableCollection<InvoiceMakananMinuman>()
                            };
                        }

                        // Kalau ada data pesanan
                        if (!reader.IsDBNull(reader.GetOrdinal("makanan_minuman_id")))
                        {
                            int harga = reader.GetInt32("harga");
                            int jumlah = reader.GetInt32("jumlah_dipesan");
                            int subtotal = harga * jumlah;

                            totalHargaKeseluruhan += subtotal;

                            invoice.DaftarPesanan.Add(new InvoiceMakananMinuman
                            {
                                MakananMinumanId = reader.GetInt64("makanan_minuman_id"),
                                NamaMakananMinuman = reader["nama_makanan_minuman"].ToString(),
                                Harga = harga,
                                JumlahDipesan = jumlah,
                                StatusPesanan = reader["status_pesanan"].ToString(),
                                TotalHarga = subtotal
                            });
                        }
                    }

                    if (invoice != null)
                    {
                        // 💸 Tambah perhitungan total keseluruhan dan biaya layanan
                        int biayaLayanan = (int)(totalHargaKeseluruhan * 0.1);
                        int totalBayar = totalHargaKeseluruhan + biayaLayanan;

                        invoice.TotalHarga = totalBayar;
                        invoice.BiayaLayanan = biayaLayanan;
                    }
                }
            }

            return invoice;
        }


        public InvoiceKamars GetInvoiceKamarById(long tamuId)
        {
            InvoiceKamars invoice = null;

            string sql = @"
        SELECT 
            t.id AS tamu_id, 
            t.nama_tamu, 
            t.no_telp, 
            t.jumlah_tamu,

            rk.id AS riwayat_id, 
            rk.kamar_id, 
            rk.catatan_khusus, 
            rk.check_in, 
            rk.check_out, 
            rk.status AS status_kamar,

            k.nama_kamar,
            kk.Harga AS harga_kamar,

            tk.total_harga AS total_tagihan,
            tk.status_pembayaran

        FROM tamus t
        LEFT JOIN riwayat_kamars rk ON t.id = rk.tamu_id
        LEFT JOIN kamars k ON rk.kamar_id = k.id
        LEFT JOIN kategori_kamars kk ON k.kategori_id = kk.id
        LEFT JOIN tagihan_kamars tk ON rk.id = tk.riwayat_kamar_id
        WHERE t.id = @tamuId
    ";

            using (var conn = Database.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@tamuId", tamuId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        invoice = new InvoiceKamars
                        {
                            Id = reader.GetInt64("tamu_id"),
                            NamaTamu = reader["nama_tamu"].ToString() ?? "",
                            NoTelp = reader["no_telp"].ToString() ?? "",
                            JumlahTamu = reader.GetInt32("jumlah_tamu"),

                            KamarId = reader.GetInt64("kamar_id"),
                            NamaKamar = reader["nama_kamar"].ToString() ?? "",
                            HargaKamar = reader.IsDBNull(reader.GetOrdinal("harga_kamar")) ? 0 : reader.GetDecimal("harga_kamar"),
                            CatatanKhusus = reader["catatan_khusus"].ToString() ?? "",
                            CheckIn = reader["check_in"] as DateTime?,
                            CheckOut = reader["check_out"] as DateTime?,
                            StatusKamar = reader["status_kamar"].ToString() ?? "",

                            TotalTagihan = reader.IsDBNull(reader.GetOrdinal("total_tagihan")) ? 0 : reader.GetDecimal("total_tagihan"),
                            StatusPembayaran = reader["status_pembayaran"].ToString() ?? ""
                        };
                    }
                }
            }

            return invoice;
        }

    }

}
