using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Rating.Domain.Models.UDMs
{
    public class GameInfoRatingUdm
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GameInfoId { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
