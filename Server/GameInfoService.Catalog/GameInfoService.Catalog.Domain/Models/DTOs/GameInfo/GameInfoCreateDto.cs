using GameInfoService.Catalog.Domain.Models.DTOs.GameDevs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Domain.Models.DTOs.GameInfo
{
    public class GameInfoCreateDto
    {
        [Required]
        public string Name { get; set; }
        public GameDeveloperDto Developer { get; set; }
        [Required]
        public string Description { get; set; }
        public string Picture { get; set; }
        [Range(0, 10)]
        public double? Rating { get; set; }
        public string PicturePath { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
