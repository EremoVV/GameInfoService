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
        public void AddGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm)
        {
            _gameInfoRatingRepository.Create(_gameInfoRatingServiceMapper.MapToEntity(gameInfoRatingUdm));
        }

        public async Task<IEnumerable<GameInfoRatingUdm>>GetAll()
        {
            return _gameInfoRatingServiceMapper.MapArrayToUdm(await _gameInfoRatingRepository.GetAll());
        }

        public void RemoveGameInfoRatingById(int id)
        {
            _gameInfoRatingRepository.Remove(id);
        }

        public void UpdateGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm)
        {
            _gameInfoRatingRepository.Update(_gameInfoRatingServiceMapper.MapToEntity(gameInfoRatingUdm));
        }
    }
}
