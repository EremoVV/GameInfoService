using System.Collections.Generic;
using System.Linq;
using GameInfoService.Rating.Domain.Models.Entities;
using GameInfoService.Rating.Domain.Models.UDMs;
using GameInfoService.Rating.Services.MappingInterfaces;

namespace GameInfoService.Rating.Services.MappingInterfaces
{
    public partial class GameInfoRatingServiceMapper : IGameInfoRatingServiceMapper
    {
        public GameInfoRatingUdm MapToUdm(GameInfoRatingEntity p1)
        {
            return p1 == null ? null : new GameInfoRatingUdm()
            {
                Id = p1.Id,
                UserId = p1.UserId,
                GameInfoId = p1.GameInfoId,
                Rating = p1.Rating
            };
        }
        public GameInfoRatingEntity MapToEntity(GameInfoRatingUdm p2)
        {
            return p2 == null ? null : new GameInfoRatingEntity()
            {
                Id = p2.Id,
                UserId = p2.UserId,
                GameInfoId = p2.GameInfoId,
                Rating = p2.Rating
            };
        }
        public IEnumerable<GameInfoRatingUdm> MapArrayToUdm(IEnumerable<GameInfoRatingEntity> p3)
        {
            return p3 == null ? null : p3.Select<GameInfoRatingEntity, GameInfoRatingUdm>(funcMain1);
        }
        
        private GameInfoRatingUdm funcMain1(GameInfoRatingEntity p4)
        {
            return p4 == null ? null : new GameInfoRatingUdm()
            {
                Id = p4.Id,
                UserId = p4.UserId,
                GameInfoId = p4.GameInfoId,
                Rating = p4.Rating
            };
        }
    }
}