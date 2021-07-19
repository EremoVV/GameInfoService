using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.Models.DTOs;
using GameInfoService.Rating.Domain.Models.UDMs;
using Mapster;

namespace GameInfoService.Rating.App.MappingInterfaces
{
    [Mapper]
    public interface IGameInfoRatingMapper
    {
        public GameInfoRatingUdm MapToUdm(GameInfoRatingDto gameInfoRatingDto);
        public GameInfoRatingDto MapToDto(GameInfoRatingUdm gameInfoRatingUdm);
    }
}
