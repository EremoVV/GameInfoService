using GameInfoService.Catalog.Models.UDMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GameInfoService.Catalog.Contexts;
using GameInfoService.Catalog.Models.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace GameInfoService.Catalog.Repositories
{
    public class GameInfoRepository : IGameInfoRepository
    {
        private readonly GameInfoContext _gameInfoContext;
        private readonly DbSet<GameInfoEntity> _gameInfoEntities;

        public GameInfoRepository(GameInfoContext gameInfoContext)
        {
            _gameInfoContext = gameInfoContext;
            _gameInfoEntities = gameInfoContext.Set<GameInfoEntity>();
        }

        public IEnumerable<GameInfoEntity> GetAllGameInfos()
        {
            return _gameInfoEntities.ToList();
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

        public void RemoveGameInfo(GameInfoEntity gameInfo)
        {
            _gameInfoContext.GameInfoSet.Remove(gameInfo);
            _gameInfoContext.SaveChanges();
        }
    }
}
