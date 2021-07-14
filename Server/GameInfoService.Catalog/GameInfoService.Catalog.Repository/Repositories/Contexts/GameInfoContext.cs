using GameInfoService.Catalog.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameInfoService.Catalog.Infrastructure.Repositories.Contexts
{
    public class GameInfoContext : DbContext
    {
        public DbSet<GameInfoEntity> GameInfoSet { get; set; }

        public GameInfoContext(DbContextOptions<GameInfoContext> options)
        : base(options)
        {

        }
    }
}
