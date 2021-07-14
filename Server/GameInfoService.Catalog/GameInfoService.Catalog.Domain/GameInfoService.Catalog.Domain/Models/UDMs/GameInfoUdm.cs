using System;
using System.ComponentModel.DataAnnotations;

namespace GameInfoService.Catalog.Domain.Models.UDMs
{
    public class GameInfoUdm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public double Rating { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
