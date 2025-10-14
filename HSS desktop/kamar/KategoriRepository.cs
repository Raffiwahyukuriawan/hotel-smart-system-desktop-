using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HSS_desktop.kamar
{
    public class KategoriRepository
    {
        public List<Kategori> GetAllKategori()
        {
            var list = new List<Kategori>();
            string sql = "SELECT id, nama FROM kategori_kamars ORDER BY nama";

            using (var conn = Database.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Kategori
                    {
                        Id = reader.GetInt32("id"),
                        NamaKategori = reader.GetString("nama")
                    });
                }
            }

            return list;
        }
    }
}
