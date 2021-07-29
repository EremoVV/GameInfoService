using GameInfoService.Catalog.Domain.Models.UDMs.GameDevs;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameInfoService.Catalog.Domain.Models.UDMs
{
    public class GameInfoUdm
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public GameDeveloperUdm Developer { get; set; }
        [Required]
        public string Description { get; set; }
        public double? Rating { get; set; }
        public string PicturePath { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
