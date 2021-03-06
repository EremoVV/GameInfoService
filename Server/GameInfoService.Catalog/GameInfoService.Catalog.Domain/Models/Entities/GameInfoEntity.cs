using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameInfoService.Catalog.Domain.Models.Entities
{
    public class GameInfoEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public GameDeveloperEntity Developer { get; set; }
        [Required]
        public string Description { get; set; }
        public double? Rating { get; set; }
        public string PicturePath { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime ReleaseDate { get; set; }
    }
}
