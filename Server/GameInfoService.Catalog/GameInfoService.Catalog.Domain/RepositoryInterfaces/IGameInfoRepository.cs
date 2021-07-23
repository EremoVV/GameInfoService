using System.Collections.Generic;
using System.Threading.Tasks;
using GameInfoService.Catalog.Domain.Models.Entities;

namespace GameInfoService.Catalog.Domain.RepositoryInterfaces
{
    public interface IGameInfoRepository
    {
        Task<IEnumerable<GameInfoEntity>> GetAllGameInfos();
        Task<GameInfoEntity> GetGameInfoById(int id);
        Task AddGameInfo(GameInfoEntity gameInfo);
        Task RemoveGameInfo(string gameInfo);
        Task UpdateGameInfo(GameInfoEntity gameInfo);
    }
}
