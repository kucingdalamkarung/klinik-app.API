using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using klinik.api2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace klinik.api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasienController : ControllerBase
    {
        PasienContext pasienContext = new PasienContext();

        [HttpGet]
        public ActionResult<IEnumerable<Pasien>> GetAllPasien()
        {
            //pasienContext = HttpContext.RequestServices.GetService(typeof(PasienContext)) as PasienContext;
            return pasienContext.GetAllPasien();
        }

        [HttpGet("{no_rm}", Name = "Get")]
        public ActionResult<IEnumerable<Pasien>> GetPasien(string no_rm)
        {
            return pasienContext.GetPasien(no_rm);
        }

        [HttpPost]
        public ActionResult PostPasien([FromBody] Pasien pasien)
        {
            if (pasienContext.PostPasien(pasien))
            {
                return Ok(pasien);
            }

            return BadRequest();
        }

        [HttpDelete("{no_rm}")]
        public int DeletePasien(string no_rm)
        {
            if (pasienContext.DeletePasien(no_rm))
            {
                return Ok().StatusCode;
            }

            return BadRequest().StatusCode;
        }

        [HttpGet("getnorm")]
        public int GetNoRekamMedis()
        {
            var rm = pasienContext.GetNoRekamMedis();
            string b = string.Empty;

            for (int i = 0; i < rm.Length; i++){
                if (Char.IsDigit(rm[i]))
                    b += rm[i];
            }

            //return int.Parse(b);
            if(b == null || string.IsNullOrEmpty(b.ToString()))
            {
                return 0;
            }
            else
            {
                return int.Parse(b);
            }
        }
    }
}