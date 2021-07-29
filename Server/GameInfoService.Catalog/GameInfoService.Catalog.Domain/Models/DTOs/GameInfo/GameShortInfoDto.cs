using GameInfoService.Catalog.Domain.Models.DTOs.GameDevs;
using System.ComponentModel.DataAnnotations;

namespace GameInfoService.Catalog.Domain.Models.DTOs.GameInfo
{
    public class GameShortInfoDto
    {
        public string Name { get; set; }
        public GameDeveloperDto Developer { get; set; }
        public string Picture { get; set; }
        [Range(0, 10)]
        public double? Rating { get; set; }
        public string PicturePath { get; set; }
    }
}
