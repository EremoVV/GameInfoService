using System.Collections.Generic;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.Models.Entities;

namespace GameInfoService.Rating.Domain.RepositoryInterfaces
{
    public interface IGameInfoRatingRepository
    {
        public void Create(GameInfoRatingEntity ratingEntity);
        public void Update(GameInfoRatingEntity ratingEntity);
        public void Remove(GameInfoRatingEntity ratingEntity);
        public void Remove(int id);
        public IEnumerable<GameInfoRatingEntity> GetAll();
    }
}
