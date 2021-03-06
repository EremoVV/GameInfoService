using GameInfoService.Catalog.Domain.Models.DTOs.GameDevs;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameInfoService.Catalog.Domain.Models.DTOs.GameInfo
{
    public class GameFullInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameDeveloperDto Developer { get; set; }
        public string Description { get; set; }
        [Range(0, 10)]
        public double? Rating { get; set; }
        public string PicturePath { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
