using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.Models.Entities;
using GameInfoService.Rating.Domain.RepositoryInterfaces;
using GameInfoService.Rating.Infrastructure.Context;
using Mapster;

namespace GameInfoService.Rating.Infrastructure.Repositories
{
    public class GameInfoRatingRepository : IGameInfoRatingRepository
    {
        private readonly GameInfoRatingContext _gameInfoContext;

        public GameInfoRatingRepository(GameInfoRatingContext gameInfoContext)
        {
            _gameInfoContext = gameInfoContext;
        }
        public async Task CreateAsync(GameInfoRatingEntity ratingEntity)
        {
            ratingEntity.CreateDate = DateTime.Now; 
            _gameInfoContext.RatingEntities.Add(ratingEntity);
            await _gameInfoContext.SaveChangesAsync();
        }

        public IEnumerable<GameInfoRatingEntity> GetAll()
        {
            return _gameInfoContext.RatingEntities.ToList();
        }

        public async Task RemoveAsync(GameInfoRatingEntity ratingEntity)
        {
            _gameInfoContext.RatingEntities.Remove(ratingEntity);
            await _gameInfoContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var gameInfoRating = await _gameInfoContext.RatingEntities.FindAsync(id);
            _gameInfoContext.RatingEntities.Remove(gameInfoRating);
            await _gameInfoContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(GameInfoRatingEntity ratingEntity)
        {
            var gameInfoRating = await _gameInfoContext.RatingEntities.FindAsync(ratingEntity.Id);
            ratingEntity.Adapt(gameInfoRating);
            await _gameInfoContext.SaveChangesAsync();
        }
    }
}
