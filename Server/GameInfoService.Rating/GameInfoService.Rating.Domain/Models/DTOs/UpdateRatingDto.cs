using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Rating.Domain.Models.DTOs
{
    public class UpdateRatingDto
    {
        public int GameId { get; set; }
        public double GameRating { get; set; }
    }
}
