using System.Collections.Generic;
using System.Linq;
using GameInfoService.Rating.App.MappingInterfaces;
using GameInfoService.Rating.Domain.Models.DTOs;
using GameInfoService.Rating.Domain.Models.UDMs;

namespace GameInfoService.Rating.App.MappingInterfaces
{
    public partial class GameInfoRatingMapper : IGameInfoRatingMapper
    {
        public GameInfoRatingUdm MapToUdm(GameInfoRatingDto p1)
        {
            return p1 == null ? null : new GameInfoRatingUdm()
            {
                Id = p1.Id,
                UserId = p1.UserId,
                GameInfoId = p1.GameInfoId,
                Rating = p1.Rating
            };
        }
        public GameInfoRatingUdm MapToUdm(GameInfoRatingUpdateDto p2)
        {
            return p2 == null ? null : new GameInfoRatingUdm()
            {
                Id = p2.Id,
                UserId = p2.UserId,
                GameInfoId = p2.GameInfoId,
                Rating = p2.Rating
            };
        }
        public GameInfoRatingUdm MapToUdm(GameInfoRatingCreateDto p3)
        {
            return p3 == null ? null : new GameInfoRatingUdm()
            {
                UserId = p3.UserId,
                GameInfoId = p3.GameInfoId,
                Rating = p3.Rating
            };
        }
        public GameInfoRatingDto MapToDto(GameInfoRatingUdm p4)
        {
            return p4 == null ? null : new GameInfoRatingDto()
            {
                Id = p4.Id,
                UserId = p4.UserId,
                GameInfoId = p4.GameInfoId,
                Rating = p4.Rating
            };
        }
        public IEnumerable<GameInfoRatingDto> MapToDto(IEnumerable<GameInfoRatingUdm> p5)
        {
            return p5 == null ? null : p5.Select<GameInfoRatingUdm, GameInfoRatingDto>(funcMain1);
        }
        
        private GameInfoRatingDto funcMain1(GameInfoRatingUdm p6)
        {
            return p6 == null ? null : new GameInfoRatingDto()
            {
                Id = p6.Id,
                UserId = p6.UserId,
                GameInfoId = p6.GameInfoId,
                Rating = p6.Rating
            };
        }
    }
}