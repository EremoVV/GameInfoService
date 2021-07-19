using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.Models.DTOs;
using GameInfoService.Rating.Domain.Models.Entities;
using GameInfoService.Rating.Domain.Models.UDMs;

namespace GameInfoService.Rating.Services.MappingInterfaces
{
    [Mapper]
    public interface IGameInfoRatingServiceMapper
    {
        public GameInfoRatingUdm MapToUdm(GameInfoRatingEntity gameInfoRatingEntity);
        public GameInfoRatingEntity MapToEntity(GameInfoRatingUdm gameInfoRatingUdm);
        public IEnumerable<GameInfoRatingUdm> MapArrayToUdm(IEnumerable<GameInfoRatingEntity> gameInfoRatingEntities);

    }
}
