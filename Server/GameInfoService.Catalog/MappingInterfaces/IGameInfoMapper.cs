using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using GameInfoService.Catalog.Models.Entities;
using GameInfoService.Catalog.Models.UDMs;
using GameInfoService.Catalog.Models.DTOs;

namespace GameInfoService.Catalog.MappingInterfaces
{
    [Mapper]
    public interface IGameInfoMapper
    {
        GameInfoUdm MapToUdm(GameInfoEntity gameInfo);
        GameInfoUdm MapToUdm(GameFullInfoDto gameInfo);
        GameFullInfoDto MapToFullInfoDto(GameInfoUdm gameInfo);
    }
}
