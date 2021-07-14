using System;

namespace GameInfoService.Catalog.Domain.Models.DTOs
{
    public class GameInfoUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public double Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
