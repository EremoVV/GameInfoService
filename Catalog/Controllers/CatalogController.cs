using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : Controller
    {
        [HttpGet("[action]")]
        [Authorize]
        public List<string> Index()
        {
            return new[] {"Game1", "Game2"}.ToList();
        }

        [HttpGet("[action]")]
        public RedirectResult UserInfo()
        {
            return Redirect("");
        }
    }
}
