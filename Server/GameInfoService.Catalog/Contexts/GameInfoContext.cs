using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Catalog.Models.Entities;

namespace GameInfoService.Catalog.Contexts
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
