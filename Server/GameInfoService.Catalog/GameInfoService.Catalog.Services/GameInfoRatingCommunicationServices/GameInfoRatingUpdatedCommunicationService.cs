using GameInfoService.Catalog.Domain.Models.ModuleCommunication.Rating;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameInfoService.Catalog.Services.GameInfoRatingCommunicationServices
{
    public class GameInfoRatingUpdatedCommunicationService : IGameInfoRatingUpdatedCommunicationService
    {
        private readonly IGameInfoRetrieveService _gameInfoRetrieveService;
        public GameInfoRatingUpdatedCommunicationService(IGameInfoRetrieveService gameInfoRetrieveService)
        {
            _gameInfoRetrieveService = gameInfoRetrieveService;
        }

        public async Task ConsumeUpdatedRating()
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "UpdateRatingQueue", false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (ch, message) =>
            {
                var content = Encoding.UTF8.GetString(message.Body.ToArray());
                var updateInfo = JsonSerializer.Deserialize<UpdateRatingDto>(content);
                await _gameInfoRetrieveService.UpdateGameInfoRating(updateInfo.GameId, updateInfo.GameRating);
                Console.WriteLine($"Updated {updateInfo.GameId} with rating {updateInfo.GameRating}");

                //channel.BasicAck(message.DeliveryTag, false);
            };

            channel.BasicConsume("UpdateRatingQueue", true, consumer);
        }

    }
}
