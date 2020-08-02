using klinik.api2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace klinik.api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Pasien ps = new Pasien();
        private readonly AuthContext authContext = new AuthContext();

        [HttpGet]
        public string GetAuth()
        {
            return "asdf";
        }

        [HttpPost]
        public ActionResult PostAuth([FromBody]Login lg)
        {
            ps.NoRekamMedis = lg.NoRM;
            if (authContext.Login(ps))
            {
                return Ok(lg);
            }

            return BadRequest();
        }

        public class Login
        {
            public string NoRM { get; set; }
        }
    }
}