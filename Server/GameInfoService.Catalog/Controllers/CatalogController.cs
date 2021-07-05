using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GameInfoService.Catalog.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<List<string>> Index()
        {
            return Ok(new[] {"Game1", "Game2"}.ToList());
        }

        [HttpGet]
        public ActionResult<string> Info()
        {
            return Ok("Catalog Info Action");
        }
    }
}
