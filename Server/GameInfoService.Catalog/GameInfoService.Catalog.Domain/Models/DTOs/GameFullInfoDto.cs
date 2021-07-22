using System;
using System.ComponentModel.DataAnnotations;

namespace GameInfoService.Catalog.Domain.Models.DTOs
{
    public class GameFullInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        [Range(0, 10)]
        public double Rating { get; set; }
        public string PicturePath { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
