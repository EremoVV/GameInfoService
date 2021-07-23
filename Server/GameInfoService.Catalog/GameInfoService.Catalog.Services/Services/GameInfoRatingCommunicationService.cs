using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Internals;
using GameInfoService.Catalog.Domain.Models.DTOs;
using GameInfoService.Catalog.Domain.RepositoryInterfaces;
using GameInfoService.Catalog.Infrastructure.Config;
using Microsoft.Extensions.Options;

namespace GameInfoService.Catalog.Services.Services
{
    public class GameInfoRatingCommunicationService
    {
        private readonly IGameInfoRepository _repository;
        private readonly IOptions<RabbitMqConfig> _rabbitConfig;
        private readonly IGameInfoRetrieveService _gameInfoRetrieveService;

        public GameInfoRatingCommunicationService(
            IGameInfoRepository repository, 
            IOptions<RabbitMqConfig> rabbitConfig, 
            IGameInfoRetrieveService gameInfoRetrieveService)
        {
            _repository = repository;
            _rabbitConfig = rabbitConfig;
            _gameInfoRetrieveService = gameInfoRetrieveService;
            SubscribeToRating();
        }

        private async void SubscribeToRating()
        {
            var brokerConnectionString = _rabbitConfig.Value.BrokerConnectionString;
            if (string.IsNullOrEmpty(brokerConnectionString)) brokerConnectionString = "host=localhost";
            try
            {
                using var bus = RabbitHutch.CreateBus(brokerConnectionString);
                await bus.PubSub.SubscribeAsync<RatingUpdateDto>("CatalogSub", UpdateRating);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task UpdateRating(RatingUpdateDto ratingUpdate)
        {
            var updatedGameInfo = await _gameInfoRetrieveService.GetGameInfo(ratingUpdate.GameId);
            updatedGameInfo.Rating = ratingUpdate.GameRating;
            await _gameInfoRetrieveService.UpdateGameInfo(updatedGameInfo);
        }

    }
}
