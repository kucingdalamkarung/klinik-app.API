using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace klinik.api2.Models
{
    public class Antrian
    {
        public int id { get; set; }
        public string NoRM { get; set; }
        public string NoUrut { get; set; }
        public string TujuanAntrian { get; set; }
        public string Poliklinik { get; set; }
        public string NoResep { get; set; }
        public string Status { get; set; }
        public DateTime TglBerobat { get; set; }
    }
}
