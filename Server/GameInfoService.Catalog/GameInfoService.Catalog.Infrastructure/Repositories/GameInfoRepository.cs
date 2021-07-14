using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using GameInfoService.Catalog.Domain.Models.Entities;
using GameInfoService.Catalog.Domain.RepositoryInterfaces;
using GameInfoService.Catalog.Infrastructure.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GameInfoService.Catalog.Infrastructure.Repositories
{
    public class GameInfoRepository : IGameInfoRepository
    {
        private readonly GameInfoContext _gameInfoContext;

        public GameInfoRepository(GameInfoContext gameInfoContext)
        {
            _gameInfoContext = gameInfoContext;
        }

        public IEnumerable<GameInfoEntity> GetAllGameInfos()
        {
            return _gameInfoContext.GameInfoSet.ToList();
        }

        public GameInfoEntity GetGameInfoById(int id)
        {
            var gameInfo = _gameInfoContext.GameInfoSet.Find(id);

            return gameInfo;
        }

        public void AddGameInfo(GameInfoEntity gameInfo)
        {
            _gameInfoContext.GameInfoSet.Add(gameInfo);

            _gameInfoContext.SaveChanges();
        }

        public void RemoveGameInfo(string name)
        {
            var gameInfo = _gameInfoContext.GameInfoSet.FirstOrDefault(x => x.Name.Normalize().Equals(name));
            if (gameInfo == null) throw new SqlNullValueException("GameInfo object not found");
            _gameInfoContext.Remove(gameInfo);
            _gameInfoContext.SaveChanges();
        }
    }
}
