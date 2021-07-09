using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Catalog.Models.Entities;
using GameInfoService.Catalog.Models.UDMs;

namespace GameInfoService.Catalog.Repositories
{
    public interface IGameInfoRepository
    {
        IEnumerable<GameInfoEntity> GetAllGameInfos();
        GameInfoEntity GetGameInfoById(int id);
        void AddGameInfo(GameInfoEntity gameInfo);
        void RemoveGameInfo(GameInfoEntity gameInfo);
    }
}
