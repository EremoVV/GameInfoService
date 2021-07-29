using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.Models.UDMs;
using GameInfoService.Rating.Domain.RepositoryInterfaces;

namespace GameInfoService.Rating.Services
{
    public interface IGameInfoRatingService
    {
        public IEnumerable<GameInfoRatingUdm> GetAll();
        public Task<GameInfoRatingUdm> GetGameInfoRating(string userId, int gameId);
        public Task RemoveGameInfoRatingById(int id);
        public Task UpdateGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm);
        public Task AddGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm);
        public Task AppendGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm);
        public Task Error();
    }
}
