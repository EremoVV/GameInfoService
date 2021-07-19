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
        public GameInfoRatingDto MapToDto(GameInfoRatingUdm p2)
        {
            return p2 == null ? null : new GameInfoRatingDto()
            {
                Id = p2.Id,
                UserId = p2.UserId,
                GameInfoId = p2.GameInfoId,
                Rating = p2.Rating
            };
        }
    }
}