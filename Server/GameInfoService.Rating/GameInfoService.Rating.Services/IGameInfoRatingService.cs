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
        public Task<IEnumerable<GameInfoRatingUdm>> GetAll();
        public void RemoveGameInfoRatingById(int id);
        public void UpdateGameInfoRating(GameInfoRatingUdm gameInfoRatingUdmUdm);
        public void AddGameInfoRating(GameInfoRatingUdm gameInfoRatingUdmUdm);
    }
}
