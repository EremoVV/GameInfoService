using System;
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
                Id = p1.Id,
                Name = p1.Name,
                Description = p1.Description,
                Rating = p1.Rating,
                PicturePath = p1.PicturePath,
                ReleaseDate = p1.ReleaseDate
            };
        }
        public GameInfoUdm MapToUdm(GameFullInfoDto p2)
        {
            return p2 == null ? null : new GameInfoUdm()
            {
                Id = p2.Id,
                Name = p2.Name,
                Description = p2.Description,
                Rating = p2.Rating,
                PicturePath = p2.PicturePath,
                ReleaseDate = p2.ReleaseDate
            };
        }
        public GameInfoUdm MapToUdm(GameInfoCreateDto p3)
        {
            return p3 == null ? null : new GameInfoUdm()
            {
                Id = p3.Id,
                Name = p3.Name,
                Description = p3.Description,
                Rating = p3.Rating,
                PicturePath = p3.PicturePath,
                ReleaseDate = p3.ReleaseDate
            };
        }
        public GameInfoUdm MapToUdm(GameInfoUpdateDto p4)
        {
            return p4 == null ? null : new GameInfoUdm()
            {
                Id = p4.Id,
                Name = p4.Name,
                Description = p4.Description,
                Rating = p4.Rating,
                PicturePath = p4.PicturePath,
                ReleaseDate = p4.ReleaseDate == null ? default(DateTime) : (DateTime)p4.ReleaseDate
            };
        }
        public GameFullInfoDto MapToFullInfoDto(GameInfoUdm p5)
        {
            return p5 == null ? null : new GameFullInfoDto()
            {
                Id = p5.Id,
                Name = p5.Name,
                Description = p5.Description,
                Rating = p5.Rating,
                PicturePath = p5.PicturePath,
                ReleaseDate = p5.ReleaseDate
            };
        }
    }
}