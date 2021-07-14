using GameInfoService.Catalog.Domain.Models.DTOs;
using GameInfoService.Catalog.Domain.Models.Entities;
using GameInfoService.Catalog.Domain.Models.UDMs;
using GameInfoService.Catalog.Infrastructure.MappingInterfaces;

namespace GameInfoService.Catalog.Infrastructure.MappingInterfaces
{
    public partial class GameInfoMapper : IGameInfoMapper
    {
        public GameInfoUdm MapToUdm(GameInfoEntity p1)
        {
            return p1 == null ? null : new GameInfoUdm()
            {
                Name = p1.Name,
                Description = p1.Description,
                Rating = p1.Rating,
                ReleaseDate = p1.ReleaseDate
            };
        }
        public GameInfoUdm MapToUdm(GameFullInfoDto p2)
        {
            return p2 == null ? null : new GameInfoUdm()
            {
                Name = p2.Name,
                Description = p2.Description,
                Rating = p2.Rating,
                ReleaseDate = p2.ReleaseDate
            };
        }
        public GameFullInfoDto MapToFullInfoDto(GameInfoUdm p3)
        {
            return p3 == null ? null : new GameFullInfoDto()
            {
                Name = p3.Name,
                Description = p3.Description,
                Rating = p3.Rating,
                ReleaseDate = p3.ReleaseDate
            };
        }
    }
}