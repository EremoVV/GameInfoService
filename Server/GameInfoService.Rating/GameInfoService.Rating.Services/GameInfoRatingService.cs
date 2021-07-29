using GameInfoService.Rating.Domain.Models.UDMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameInfoService.Rating.Domain.RepositoryInterfaces;
using GameInfoService.Rating.Services.MappingInterfaces;
using GameInfoService.Rating.Services.ModuleCommunication;
using GameInfoService.Rating.Domain.Models.DTOs;

namespace GameInfoService.Rating.Services
{
    public class GameInfoRatingService : IGameInfoRatingService
    {
        private readonly IGameInfoRatingRepository _gameInfoRatingRepository;
        private readonly IGameInfoRatingServiceMapper _gameInfoRatingServiceMapper;
        private readonly IRatingUpdatedQueueSender _ratingUpdatedQueueSender;

        public GameInfoRatingService(IGameInfoRatingRepository gameInfoRatingRepository, 
            IGameInfoRatingServiceMapper gameInfoRatingServiceMapper, 
            IRatingUpdatedQueueSender ratingUpdatedQueueSender)
        {
            _gameInfoRatingRepository = gameInfoRatingRepository;
            _gameInfoRatingServiceMapper = gameInfoRatingServiceMapper;
            _ratingUpdatedQueueSender = ratingUpdatedQueueSender;
        }
        public async Task AddGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm)
        {
            await _gameInfoRatingRepository.CreateAsync(_gameInfoRatingServiceMapper.MapToEntity(gameInfoRatingUdm));
            var gameInfoRating = _gameInfoRatingRepository
                .GetAll()
                .First(x => x.UserId == gameInfoRatingUdm.UserId && x.GameInfoId == gameInfoRatingUdm.GameInfoId);
            HandleUpdateCommunication(gameInfoRatingUdm.GameInfoId);
        }

        public IEnumerable<GameInfoRatingUdm>GetAll()
        {
            return _gameInfoRatingServiceMapper
                .MapArrayToUdm(_gameInfoRatingRepository.GetAll());
        }

        public async Task RemoveGameInfoRatingById(int id)
        {
            await _gameInfoRatingRepository.RemoveAsync(id);
            //HandleUpdateCommunication(gameInfoRatingUdm.GameInfoId);
        }

        public async Task UpdateGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm)
        {
            await _gameInfoRatingRepository
                .UpdateAsync(_gameInfoRatingServiceMapper.MapToEntity(gameInfoRatingUdm));
            HandleUpdateCommunication(gameInfoRatingUdm.GameInfoId);
        }

        public Task Error()
        {
            throw new KeyNotFoundException();
        }

        private void HandleUpdateCommunication(int gameId)
        {
            double gameRating = CalculateGameRatingById(gameId);
            _ratingUpdatedQueueSender.SendUpdateRatingQueue(new UpdateRatingDto { GameId = gameId, GameRating=gameRating });
        }

        private double CalculateGameRatingById(int gameId)
        {
            double gameRating = 0;
            var gameInfoRatings = this
                .GetAll()
                .Where(x => x.GameInfoId == gameId);
            foreach (GameInfoRatingUdm gameInfoRating in gameInfoRatings)
            {
                gameRating += gameInfoRating.Rating;
            }
            return gameRating / gameInfoRatings.Count();
        }

        public async Task AppendGameInfoRating(GameInfoRatingUdm gameInfoRatingUdm)
        {
            var gameInfoRating = _gameInfoRatingRepository
                .GetAll()
                .Where(x => x.UserId == gameInfoRatingUdm.UserId && x.GameInfoId == gameInfoRatingUdm.GameInfoId).FirstOrDefault();
            if (gameInfoRating == null) await _gameInfoRatingRepository
                    .CreateAsync(_gameInfoRatingServiceMapper.MapToEntity(gameInfoRatingUdm));
            else
            {
                gameInfoRating.Rating = gameInfoRatingUdm.Rating;
                await _gameInfoRatingRepository
                    .UpdateAsync(gameInfoRating);
            }
            HandleUpdateCommunication(gameInfoRatingUdm.GameInfoId);
        }

        public async Task<GameInfoRatingUdm> GetGameInfoRating(string userId, int gameId)
        {
            var gameInfoRating = _gameInfoRatingServiceMapper.MapArrayToUdm(_gameInfoRatingRepository.GetAll())
                .FirstOrDefault(x => x.UserId == userId && x.GameInfoId == gameId);
            if (gameInfoRating == null) throw new KeyNotFoundException("No gameInfoRating with given userId");
            return gameInfoRating;
        }
    }
}
