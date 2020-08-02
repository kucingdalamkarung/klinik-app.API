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
    public class PoliklinikController : ControllerBase
    {
        PoliklinikContext poliklinikContext = new PoliklinikContext();

        [HttpGet]
        public ActionResult<IEnumerable<Poliklinik>> GetPoliklinik()
        {
            return poliklinikContext.GetPoliklinik();
        }
    }
}