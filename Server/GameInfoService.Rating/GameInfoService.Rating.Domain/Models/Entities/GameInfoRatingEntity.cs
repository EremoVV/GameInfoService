using System;
using System.ComponentModel.DataAnnotations;

namespace GameInfoService.Rating.Domain.Models.Entities
{
    public class GameInfoRatingEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GameInfoId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
