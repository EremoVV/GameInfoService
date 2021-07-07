using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Models.DTOs
{
    public class GameShortInfoDto
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        [Range(0, 10)]
        public double Rating { get; set; }
    }
}
