using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace klinik.api2.Models
{
    public class AntrianContext
    {
        //public string connectionString = @"Data Source=sql5016.site4now.net;User ID=DB_A4BE38_fiqrikm15_admin;Password=@Rats12345";
        public string connectionString = @"Data Source=.;Initial Catalog=db_klinik;Persist Security Info=True;User ID=admin;Password=fkm1396";
        SqlConnection conn;

        public AntrianContext()
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

        public int GetNoUrut(string poli)
        {
            int res = 0;
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("select top 1 no_urut from tb_antrian where poliklinik = @poli and tgl_berobat = convert(varchar(10), getdate(), 111) order by 1 desc", conn);
                cmd.Parameters.AddWithValue("poli", poli);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res = reader.GetInt32(0);
                    }
                }

                CloseConnection();
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }

        public List<Antrian> GetAntrian(string no_rm)
        {
            List<Antrian> antrian = new List<Antrian>();
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("select top 1 * from tb_antrian where tgl_berobat=CONVERT(date, getdate(), 111) and no_rm=@rm order by 1 desc", conn);
                cmd.Parameters.AddWithValue("rm", no_rm);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        antrian.Add(new Antrian()
                        {
                            id = int.Parse(reader["id"].ToString()),
                            NoRM = reader["no_rm"].ToString(),
                            NoUrut = reader["no_urut"].ToString(),
                            TujuanAntrian = reader["tujuan_antrian"].ToString(),
                            Poliklinik = reader["poliklinik"].ToString(),
                            NoResep = reader["no_resep"].ToString(),
                            Status = reader["status"].ToString(),
                            TglBerobat = DateTime.Parse(reader["tgl_berobat"].ToString())
                        });
                    }
                }

                    CloseConnection();
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return antrian;
        }

        public bool CreateAntrian(Antrian antrian)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[tb_antrian] ([no_rm] ,[no_urut] ,[tujuan_antrian] ,[poliklinik] ,[no_resep] ,[status]) VALUES (@no_rm ,@no_urut ,@tujuan_antrian ,@poliklinik ,@no_resep ,@status)", conn);
                cmd.Parameters.AddWithValue("no_rm", antrian.NoRM);
                cmd.Parameters.AddWithValue("no_urut", antrian.NoUrut);
                cmd.Parameters.AddWithValue("tujuan_antrian", antrian.TujuanAntrian);
                cmd.Parameters.AddWithValue("poliklinik", antrian.Poliklinik);
                cmd.Parameters.AddWithValue("no_resep", antrian.NoResep);
                cmd.Parameters.AddWithValue("status", antrian.Status);

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

        public int GetAntrianPoli(string kode_poli)
        {
            int res = 0;
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 [no_urut] FROM [db_klinik].[dbo].[tb_antrian] where [poliklinik]=@poli and [tgl_berobat]=convert(date, getdate(), 111) and status='Panggil'", conn);
                cmd.Parameters.AddWithValue("poli", kode_poli);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res = reader.GetInt32(0);
                    }
                }

                    CloseConnection();
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return res;
        }
    }
}

