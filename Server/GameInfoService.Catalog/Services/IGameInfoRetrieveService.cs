using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Catalog.Models.DTOs;
using GameInfoService.Catalog.Models.UDMs;

namespace GameInfoService.Catalog.Services
{
    public interface IGameInfoRetrieveService
    {
        IEnumerable<GameInfoUdm> GetAllGameInfos();
        GameInfoUdm GetGameInfoByName(string name);
    }
}
