using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Domain.Models.DTOs.GameDevs
{
    public class GameDeveloperDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PicturePath { get; set; }
    }
}
