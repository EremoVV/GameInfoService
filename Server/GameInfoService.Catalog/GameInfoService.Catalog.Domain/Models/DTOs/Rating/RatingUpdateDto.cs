using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Domain.Models.DTOs
{
    public class RatingUpdateDto
    {
        public int GameId { get; set; }
        public double GameRating { get; set; }
    }
}
