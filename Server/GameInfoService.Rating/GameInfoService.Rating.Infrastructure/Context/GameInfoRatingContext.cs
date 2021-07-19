using GameInfoService.Rating.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameInfoService.Rating.Infrastructure.Context
{
    public class GameInfoRatingContext : DbContext
    {
        public DbSet<GameInfoRatingEntity> RatingEntities { get; set; }

        public GameInfoRatingContext(DbContextOptions<GameInfoRatingContext> options) :
            base(options)
        {

        }
    }
}
