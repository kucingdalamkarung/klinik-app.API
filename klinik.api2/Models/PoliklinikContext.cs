using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace klinik.api2.Models
{
    public class PoliklinikContext
    {
        //public string connectionString = @"Data Source=sql5016.site4now.net;User ID=DB_A4BE38_fiqrikm15_admin;Password=@Rats12345";
        public string connectionString = @"Data Source=.;Initial Catalog=db_klinik;Persist Security Info=True;User ID=admin;Password=fkm1396";
        private SqlConnection conn;

        public PoliklinikContext()
        {
            conn = new SqlConnection(connectionString);
        }

        private SqlConnection GetConnection()
        {
            return conn = new SqlConnection(connectionString);
        }

        private void OpenConnection()
        {
            if (conn.State.Equals(System.Data.ConnectionState.Closed))
            {
                conn.Open();
            }
        }

        private void CloseConnection()
        {
            if (conn.State.Equals(System.Data.ConnectionState.Open))
            {
                conn.Close();
            }
        }

        public List<Poliklinik> GetPoliklinik()
        {
            List<Poliklinik> poli = new List<Poliklinik>();

            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from tb_poliklinik", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        poli.Add(new Poliklinik()
                        {
                            KodePoli = reader["kode_poli"].ToString(),
                            NamaPoli = reader["nama_poli"].ToString()
                        });
                    }
                }
                CloseConnection();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return poli;
        }
    }
}
