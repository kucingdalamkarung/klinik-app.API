using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using klinik.api2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace klinik.api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntrianController : ControllerBase
    {
        AntrianContext antrianContext = new AntrianContext();

        [HttpGet("{no_rm}", Name ="no_rm")]
        public ActionResult<IEnumerable<Antrian>> GetAntrian(string no_rm)
        {
            return antrianContext.GetAntrian(no_rm);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Antrian>> PostAntrian([FromBody]Antrian antrian)
        {
            var last = antrianContext.GetNoUrut(antrian.Poliklinik);
            if (last != 0) last += 1;
            else last = 1;

            antrian.NoUrut = last.ToString();

            if (antrianContext.CreateAntrian(antrian))
            {
                return Ok(antrian);
            }

            return BadRequest();
        }

        [HttpGet("poli/{kode_poli}", Name ="kode_poli")]
        public int GetAntrianPoli(string kode_poli)
        {
            return antrianContext.GetAntrianPoli(kode_poli);
        }
    }
}