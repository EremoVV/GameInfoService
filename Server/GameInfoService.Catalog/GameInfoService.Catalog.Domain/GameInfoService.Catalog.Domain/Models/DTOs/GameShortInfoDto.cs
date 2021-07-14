using System.ComponentModel.DataAnnotations;

namespace GameInfoService.Catalog.Domain.Models.DTOs
{
    public class GameShortInfoDto
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        [Range(0, 10)]
        public double Rating { get; set; }
    }
}
