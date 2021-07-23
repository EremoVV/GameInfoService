using GameInfoService.Catalog.Domain.Models.UDMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Services.Services
{
    public interface IGameInfoRetrieveService
    {
        Task<IEnumerable<GameInfoUdm>> GetAllGameInfos();
        Task<GameInfoUdm> GetGameInfoByName(string name);
        Task<GameInfoUdm> GetGameInfo(int id);
        Task AddGameInfo(GameInfoUdm gameInfo);
        Task RemoveGameInfo(string gameInfo);
        Task UpdateGameInfo(GameInfoUdm gameInfo);
    }
}
