using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace klinik.api2.Models
{
    public class PasienContext
    {
        //public string connectionString = @"Data Source=sql5016.site4now.net;User ID=DB_A4BE38_fiqrikm15_admin;Password=@Rats12345";
        public string connectionString = @"Data Source=.;Initial Catalog=db_klinik;Persist Security Info=True;User ID=admin;Password=fkm1396";
        private SqlConnection conn;

        public PasienContext()
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

        public List<Pasien> GetAllPasien()
        {
            List<Pasien> pasien = new List<Pasien>();

            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from tb_pasien", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pasien.Add(new Pasien()
                        {
                            NoRekamMedis = reader["no_rekam_medis"].ToString(),
                            NoIdentitas = reader["no_identitas"].ToString(),
                            JenisIdentitas = reader["jenis_identitas"].ToString(),
                            Nama = reader["nama"].ToString(),
                            GolonganDarah = reader["golongan_darah"].ToString(),
                            TanggalLahir = reader["tanggal_lahir"].ToString(),
                            JenisKelamin = reader["jenis_kelamin"].ToString(),
                            NoTelp = reader["no_telp"].ToString(),
                            Alamat = reader["alamat"].ToString(),
                            TglDaftar = DateTime.Parse(reader["tgl_daftar"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return pasien;
        }

        public List<Pasien> GetPasien(string noRekamMedis)
        {
            List<Pasien> ps = new List<Pasien>();
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("Select * from tb_pasien where no_rekam_medis=@norm", conn);
                cmd.Parameters.AddWithValue("norm", noRekamMedis);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ps.Add(new Pasien()
                        {
                            NoRekamMedis = reader["no_rekam_medis"].ToString(),
                            NoIdentitas = reader["no_identitas"].ToString(),
                            JenisIdentitas = reader["jenis_identitas"].ToString(),
                            Nama = reader["nama"].ToString(),
                            GolonganDarah = reader["golongan_darah"].ToString(),
                            TanggalLahir = reader["tanggal_lahir"].ToString(),
                            JenisKelamin = reader["jenis_kelamin"].ToString(),
                            NoTelp = reader["no_telp"].ToString(),
                            Alamat = reader["alamat"].ToString(),
                            TglDaftar = DateTime.Parse(reader["tgl_daftar"].ToString())
                        });
                    }
                }

                CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ps;
        }

        public bool PostPasien(Pasien pasien)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[tb_pasien] ([no_rekam_medis] ,[no_identitas] ,[jenis_identitas] ,[nama] ,[golongan_darah] ,[tanggal_lahir] ,[jenis_kelamin] ,[no_telp] ,[alamat]) VALUES (@no_rekam_medis,  @no_identitas,  @jenis_identitas,  @nama,  @golongan_darah,  @tanggal_lahir,  @jenis_kelamin,  @no_telp,  @alamat)", conn);
                cmd.Parameters.AddWithValue("no_rekam_medis", pasien.NoRekamMedis);
                cmd.Parameters.AddWithValue("no_identitas", pasien.NoIdentitas);
                cmd.Parameters.AddWithValue("jenis_identitas", pasien.JenisIdentitas);
                cmd.Parameters.AddWithValue("nama", pasien.Nama);
                cmd.Parameters.AddWithValue("golongan_darah", pasien.GolonganDarah);
                cmd.Parameters.AddWithValue("tanggal_lahir", DateTime.Parse(pasien.TanggalLahir).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("jenis_kelamin", pasien.JenisKelamin);
                cmd.Parameters.AddWithValue("no_telp", pasien.NoTelp);
                cmd.Parameters.AddWithValue("alamat", pasien.Alamat);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }

                CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        public bool DeletePasien(string no_rm)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("delete tb_pasien where no_rekam_medis=@no_rekam_medis", conn);
                cmd.Parameters.AddWithValue("no_rekam_medis", no_rm);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }

                CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        public string GetNoRekamMedis()
        {
            string res = "";
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("select top 1 no_rekam_medis from tb_pasien order by no_rekam_medis desc", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res = reader["no_rekam_medis"].ToString();
                    }
                }

                CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }
    }
}
