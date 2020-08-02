using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace klinik.api2.Models
{
    public class Pasien
    {
        public string NoRekamMedis { get; set; }
        public string NoIdentitas { get; set; }
        public string JenisIdentitas { get; set; }
        public string Nama { get; set; }
        public string GolonganDarah { get; set; }
        public string TanggalLahir { get; set; }
        public string JenisKelamin { get; set; }
        public string NoTelp { get; set; }
        public string Alamat { get; set; }
        public DateTime TglDaftar { get; set; }
    }
}
