using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Rating.Domain.Models.DTOs
{
    public class GameInfoRatingUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GameInfoId { get; set; }
        public int Rating { get; set; }
    }
}
