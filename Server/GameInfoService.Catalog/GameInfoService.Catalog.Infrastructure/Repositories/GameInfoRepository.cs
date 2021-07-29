using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GameInfoService.Catalog.Domain.Models.Entities;
using GameInfoService.Catalog.Domain.RepositoryInterfaces;
using GameInfoService.Catalog.Infrastructure.Repositories.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace GameInfoService.Catalog.Infrastructure.Repositories
{
    public class GameInfoRepository : IGameInfoRepository
    {
        private readonly GameInfoContext _gameInfoContext;

        public GameInfoRepository(GameInfoContext gameInfoContext)
        {
            _gameInfoContext = gameInfoContext;
        }

        public async Task<IEnumerable<GameInfoEntity>> GetAllGameInfos()
        {
            return await _gameInfoContext.GameInfoSet.Include(x => x.Developer).ToListAsync();
        }

        public async Task<GameInfoEntity> GetGameInfoById(int id)
        {
            var gameInfo = await _gameInfoContext.GameInfoSet.FindAsync(id);

            return gameInfo;
        }

        public async Task AddGameInfo(GameInfoEntity gameInfo)
        {
            await _gameInfoContext.GameInfoSet.AddAsync(gameInfo);

            await _gameInfoContext.SaveChangesAsync();
        }

        public async Task RemoveGameInfo(string name)
        {
            var gameInfo = await _gameInfoContext.GameInfoSet.FirstOrDefaultAsync(x => x.Name.Equals(name));
            if (gameInfo == null) throw new SqlNullValueException("GameInfo object not found");
            _gameInfoContext.Remove(gameInfo);
            await _gameInfoContext.SaveChangesAsync();
        }

        public async Task UpdateGameInfo(GameInfoEntity gameInfo)
        {
            var gameInfoEntity = await _gameInfoContext.GameInfoSet.FindAsync(gameInfo.Id);
            gameInfo.Adapt(gameInfoEntity);
            await _gameInfoContext.SaveChangesAsync();
        }
    }
}
