using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.Models.Entities;
using GameInfoService.Rating.Domain.RepositoryInterfaces;
using GameInfoService.Rating.Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GameInfoService.Rating.Infrastructure.Repositories
{
    public class GameInfoRatingRepository : IGameInfoRatingRepository
    {
        private readonly GameInfoRatingContext _gameInfoContext;

        public GameInfoRatingRepository(GameInfoRatingContext gameInfoContext)
        {
            _gameInfoContext = gameInfoContext;
        }
        public async void Create(GameInfoRatingEntity ratingEntity)
        {
            ratingEntity.CreateDate = DateTime.Now;
            await _gameInfoContext.RatingEntities.AddAsync(ratingEntity);
            await _gameInfoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameInfoRatingEntity>> GetAll()
        {
            return await _gameInfoContext.RatingEntities.ToListAsync();
        }

        public async void Remove(GameInfoRatingEntity ratingEntity)
        {
            _gameInfoContext.RatingEntities.Remove(ratingEntity);
            await _gameInfoContext.SaveChangesAsync();
        }

        public async void Remove(int id)
        {
            var gameInfoRating = await _gameInfoContext.RatingEntities.FindAsync(id);
            _gameInfoContext.RatingEntities.Remove(gameInfoRating);
            await _gameInfoContext.SaveChangesAsync();
        }

        public async void Update(GameInfoRatingEntity ratingEntity)
        {
            var gameInfoRating = await _gameInfoContext.RatingEntities.FindAsync(ratingEntity.Id);
            ratingEntity.Adapt(gameInfoRating);
            await _gameInfoContext.SaveChangesAsync();
        }
    }
}
