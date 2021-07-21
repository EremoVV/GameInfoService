using System.Collections.Generic;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.Models.Entities;

namespace GameInfoService.Rating.Domain.RepositoryInterfaces
{
    public interface IGameInfoRatingRepository
    {
        public Task CreateAsync(GameInfoRatingEntity ratingEntity);
        public Task UpdateAsync(GameInfoRatingEntity ratingEntity);
        public Task RemoveAsync(GameInfoRatingEntity ratingEntity);
        public Task RemoveAsync(int id);
        public IEnumerable<GameInfoRatingEntity> GetAll();
    }
}
