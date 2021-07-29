using GameInfoService.Catalog.Domain.Models.DTOs;
using GameInfoService.Catalog.Domain.Models.DTOs.GameInfo;
using GameInfoService.Catalog.Domain.Models.Entities;
using GameInfoService.Catalog.Domain.Models.UDMs;
using Mapster;

namespace GameInfoService.Catalog.Infrastructure.MappingInterfaces
{
    [Mapper]
    public interface IGameInfoMapper
    {
        GameInfoUdm MapToUdm(GameInfoEntity gameInfo);
        GameInfoUdm MapToUdm(GameFullInfoDto gameInfo);
        GameInfoUdm MapToUdm(GameInfoCreateDto gameInfo);
        GameInfoUdm MapToUdm(GameInfoUpdateDto gameInfo);
        GameFullInfoDto MapToFullInfoDto(GameInfoUdm gameInfo);
    }
}
