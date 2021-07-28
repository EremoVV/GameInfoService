using GameInfoService.Catalog.Domain.Models.ModuleCommunication.Rating;
using GameInfoService.Catalog.Services;
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

        private readonly IServiceScopeFactory _serviceScopeFactory;
        public GameInfoRatingUpdatedCommunicationServiceHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "UpdateRatingQueue", false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (ch, message) =>
            {
                using(var scope = _serviceScopeFactory.CreateScope())
                {
                    var content = Encoding.UTF8.GetString(message.Body.ToArray());
                    var updateInfo = JsonSerializer.Deserialize<UpdateRatingDto>(content);
                    var gameInfoRetrieveService = scope.ServiceProvider.GetRequiredService<IGameInfoRetrieveService>();
                    await gameInfoRetrieveService.UpdateGameInfoRating(updateInfo.GameId, updateInfo.GameRating);
                    Console.WriteLine($"Updated {updateInfo.GameId} with rating {updateInfo.GameRating}");
                }

                //channel.BasicAck(message.DeliveryTag, false);
            };

            channel.BasicConsume("UpdateRatingQueue", true, consumer);
            return Task.CompletedTask;
        }
    }
}
