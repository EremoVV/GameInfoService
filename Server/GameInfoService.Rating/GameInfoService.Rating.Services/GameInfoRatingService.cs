using GameInfoService.Rating.Domain.Models.UDMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.RepositoryInterfaces;
using GameInfoService.Rating.Services.MappingInterfaces;

namespace GameInfoService.Rating.Services
{
    public class GameInfoRatingService : IGameInfoRatingService
    {
        private readonly IGameInfoRatingRepository _gameInfoRatingRepository;
        private readonly IGameInfoRatingServiceMapper _gameInfoRatingServiceMapper;

        public GameInfoRatingService(IGameInfoRatingRepository gameInfoRatingRepository, IGameInfoRatingServiceMapper gameInfoRatingServiceMapper)
        {
            _gameInfoRatingRepository = gameInfoRatingRepository;
            _gameInfoRatingServiceMapper = gameInfoRatingServiceMapper;
        }
        public async Task AddGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm)
        {
            await _gameInfoRatingRepository.CreateAsync(_gameInfoRatingServiceMapper.MapToEntity(gameInfoRatingUdm));
        }

        public IEnumerable<GameInfoRatingUdm>GetAll()
        {
            return _gameInfoRatingServiceMapper.MapArrayToUdm(_gameInfoRatingRepository.GetAll());
        }

        public async Task RemoveGameInfoRatingById(int id)
        {
            await _gameInfoRatingRepository.RemoveAsync(id);
        }

        public async Task UpdateGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm)
        {
            await _gameInfoRatingRepository.UpdateAsync(_gameInfoRatingServiceMapper.MapToEntity(gameInfoRatingUdm));
        }

        public Task Error()
        {
            throw new KeyNotFoundException();
        }
    }
}
