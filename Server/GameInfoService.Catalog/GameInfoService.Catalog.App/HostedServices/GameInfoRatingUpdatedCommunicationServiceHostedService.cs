using GameInfoService.Catalog.Domain.Models.ModuleCommunication.Rating;
using GameInfoService.Catalog.Services;
using GameInfoService.Catalog.Services.GameInfoRatingCommunicationServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.App.HostedServices
{
    public class GameInfoRatingUpdatedCommunicationServiceHostedService : BackgroundService
    {

        private readonly IGameInfoRatingUpdatedCommunicationService _gameInfoRatingUpdatedCommunicationService;
        public GameInfoRatingUpdatedCommunicationServiceHostedService(IGameInfoRatingUpdatedCommunicationService gameInfoRatingUpdatedCommunicationService)
        {
            _gameInfoRatingUpdatedCommunicationService = gameInfoRatingUpdatedCommunicationService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _gameInfoRatingUpdatedCommunicationService.ConsumeUpdatedRating();
            return Task.CompletedTask;
        }
    }
}
