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
        IEnumerable<GameInfoUdm> GetAllGameInfos();
        GameInfoUdm GetGameInfoByName(string name);
        void AddGameInfo(GameInfoUdm gameInfo);
        void RemoveGameInfo(GameInfoUdm gameInfo);
    }
}
