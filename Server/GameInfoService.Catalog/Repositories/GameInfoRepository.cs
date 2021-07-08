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

namespace GameInfoService.Catalog.Repositories
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
    }
}
