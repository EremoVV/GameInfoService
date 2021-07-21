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
        public Task RemoveGameInfoRatingById(int id);
        public Task UpdateGameInfoRating(GameInfoRatingUdm gameInfoRatingUdmUdm);
        public Task AddGameInfoRating(GameInfoRatingUdm gameInfoRatingUdmUdm);
        public Task Error();
    }
}
