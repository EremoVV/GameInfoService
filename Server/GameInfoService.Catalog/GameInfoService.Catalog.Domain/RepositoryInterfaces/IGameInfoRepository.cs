using System.Collections.Generic;
using GameInfoService.Catalog.Domain.Models.Entities;

namespace GameInfoService.Catalog.Domain.RepositoryInterfaces
{
    public interface IGameInfoRepository
    {
        IEnumerable<GameInfoEntity> GetAllGameInfos();
        GameInfoEntity GetGameInfoById(int id);
        void AddGameInfo(GameInfoEntity gameInfo);
        void RemoveGameInfo(GameInfoEntity gameInfo);
    }
}
