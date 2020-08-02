using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace klinik.api2.Models
{
    public class AuthContext
    {
        //public string connectionString = @"Data Source=sql5016.site4now.net;User ID=DB_A4BE38_fiqrikm15_admin;Password=@Rats12345";
        public string connectionString = @"Data Source=.;Initial Catalog=db_klinik;Persist Security Info=True;User ID=admin;Password=fkm1396";
        SqlConnection conn;

        public AuthContext()
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

        public bool Login(Pasien pasien)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("select count(*) from tb_pasien where no_rekam_medis=@norm", conn);
                cmd.Parameters.AddWithValue("norm", pasien.NoRekamMedis);
                var res = int.Parse(cmd.ExecuteScalar().ToString());

                if(res == 1)
                {
                    return true;
                }
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }
    }
}
